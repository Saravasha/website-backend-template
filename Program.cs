using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using WebAppBackend.Data;
using WebAppBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
builder.Configuration.AddEnvironmentVariables();

// Set connection string based on environment
string connectionString;

if (builder.Environment.IsDevelopment())
{
    var baseConnStr = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    // Generate a dev-only DB name (e.g., from machine or folder name)
    string devDbName = $"DevDb_{Path.GetFileName(Environment.CurrentDirectory)}";

    // Replace catalog in connection string
    var sqlBuilder = new SqlConnectionStringBuilder(baseConnStr)
    {
        InitialCatalog = devDbName
    };

    connectionString = sqlBuilder.ToString();

    Console.WriteLine($"🛠️ Development DB: {sqlBuilder.InitialCatalog}");
}
else
{
    connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
        ?? throw new InvalidOperationException("Connection string 'CONNECTION_STRING' not found.");
}


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

// Add services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDirectoryBrowser();
builder.Services.AddScoped<FilePathProvider>();
builder.Services.AddScoped<VideoThumbnailProvider>();
builder.Services.AddScoped<AssetTypeProvider>();
builder.Services.AddHostedService<OrphanAnnihilatorBackgroundService>();
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

// CORS setup
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
    options.AddPolicy("corsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
    })
);


// Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 400 * 1024 * 1024; // 400 MB
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");

// Error handling pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Apply EF Core migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var config = services.GetRequiredService<IConfiguration>();

    try
    {
        // Apply migrations
        Console.WriteLine("Applying EF Core migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied.");

        Console.WriteLine("Seeding database...");
        await SeedData.InitializeAsync(services, config);
        Console.WriteLine("Seeding complete.");

        // Add robots.txt for staging
        var filePathProvider = services.GetRequiredService<FilePathProvider>();
        var robotsPath = filePathProvider.RobotsTxtPath;

        if (!File.Exists(robotsPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(robotsPath)!);
            File.WriteAllText(robotsPath, "User-agent: *\nDisallow: /");
            Console.WriteLine("Created staging robots.txt to block crawlers.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error during migration or seeding:");
        Console.WriteLine(ex);
        throw;
    }
}

// SMTP validation
var smtpSettings = builder.Configuration.GetSection("Smtp").Get<SmtpSettings>();
if (smtpSettings == null ||
    string.IsNullOrWhiteSpace(smtpSettings.Host) ||
    string.IsNullOrWhiteSpace(smtpSettings.Username) ||
    string.IsNullOrWhiteSpace(smtpSettings.Password) ||
    string.IsNullOrWhiteSpace(smtpSettings.From) ||
    smtpSettings.Port == 0)
{
    throw new InvalidOperationException("Missing required SMTP configuration values.");
}

// Serve static files
app.UseStaticFiles();

// Mount Uploads and Assets recursively
void DirectoryAsserter(string path, string requestPath)
{
    if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(path),
        RequestPath = requestPath
    });
}

void AddStaticFilesRecursively(string directory, WebApplication app)
{
    DirectoryAsserter(directory, "/" + Path.GetFileName(directory));
    foreach (var subdirectory in Directory.GetDirectories(directory))
    {
        AddStaticFilesRecursively(subdirectory, app);
    }
}

using (var scope = app.Services.CreateScope())
{
    var filePathProvider = scope.ServiceProvider.GetRequiredService<FilePathProvider>();
    DirectoryAsserter(filePathProvider.UploadsRoot, "/Uploads");
    AddStaticFilesRecursively(filePathProvider.WebAssetsRoot, app);
}

app.UseRouting();
app.UseCors("corsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();  
