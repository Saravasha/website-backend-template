using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Models;
using WebAppBackend.Models.SettingsModels;

namespace WebAppBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Page> Pages { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Asset> Assets { get; set; } = default!;
        public DbSet<Color> Colors { get; set; } = default!;
        public DbSet<Font> Fonts { get; set; } = default!;
        public DbSet<Settings> Settings { get; set; } = default!;

        public DbSet<Branding> Branding { get; set; } = default!;
        public DbSet<SocialMedia> SocialMedia { get; set; }


        public async Task SeedPagesAsync()
        {
            await PageSeeder.SeedPagesAsync(this);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "Background Color", StartColor = "#000000", EndColor = "#000000", DarkStartColor = "#000000", DarkEndColor = "#000000" },
                new Color { Id = 2, Name = "Header Text", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" },
                new Color { Id = 3, Name = "Navbar Background Color", StartColor = "#ffff6a", EndColor = "#ffffff", DarkStartColor = "#052e16", DarkEndColor = "#ffffff" },
                new Color { Id = 4, Name = "Page-depth-0 Page Header Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 5, Name = "Page-depth-1 Chapter Header Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 6, Name = "Page-depth-2 Content Header Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 7, Name = "Page-depth-0 Page Body Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 8, Name = "Page-depth-1 Chapter Body Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 9, Name = "Page-depth-2 Content Body Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 10, Name = "ScrollToTop Background Color", StartColor = "#052e16", EndColor = "#052e16", DarkStartColor = "#052e16", DarkEndColor = "#052e16" },
                new Color { Id = 11, Name = "Navbar Text Color", StartColor = "#000000", EndColor = "#000000", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" },
                new Color { Id = 12, Name = "Instagram Background Icon Color", StartColor = "#15803D", EndColor = "#15803D", DarkStartColor = "#15803D", DarkEndColor = "#15803D" },
                new Color { Id = 13, Name = "Facebook Background Icon Color", StartColor = "#15803D", EndColor = "#15803D", DarkStartColor = "#15803D", DarkEndColor = "#15803D" },
                new Color { Id = 14, Name = "Twitter Background Icon Color", StartColor = "#15803D", EndColor = "#15803D", DarkStartColor = "#15803D", DarkEndColor = "#15803D" },
                new Color { Id = 15, Name = "Instagram Fill Icon Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" },
                new Color { Id = 16, Name = "Facebook Fill Icon Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" },
                new Color { Id = 17, Name = "Twitter Fill Icon Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" },
                new Color { Id = 18, Name = "Social Media Header Text", StartColor = "#15803D", EndColor = "#15803D", DarkStartColor = "#15803D", DarkEndColor = "#15803D" },
                new Color { Id = 19, Name = "Footer Text", StartColor = "#15803D", EndColor = "#15803D", DarkStartColor = "#15803D", DarkEndColor = "#15803D" });


            modelBuilder.Entity<Font>().HasData(
                new Font
                {
                    Id = 1,
                    Name = "Website Title Header Text Font",
                    Style = "normal",
                    Weight = 100
                },
                new Font
                {
                    Id = 2,
                    Name = "Page-depth-0 Page Header Text Font",
                    Style = "normal",
                    Weight = 100
                },
                new Font
                {
                    Id = 3,
                    Name = "Social Media Header Text",
                    Style = "normal",
                    Weight = 100
                },
                new Font
                {
                    Id = 4,
                    Name = "Website Title Footer Text Font",
                    Style = "normal",
                    Weight = 100
                }
            );





        modelBuilder.Entity<Settings>().HasData(
                new Settings
                {
            Id = 1
                }
            );

            modelBuilder.Entity<Branding>().HasData(
                new Branding
                {
            Id = 1,
                    SettingsId = 1,
                    AppName = "__PROJECT_NAME__",
                    Description = "__PROJECT_NAME__ - web application for managing productions, assets, and content."
                }
            );

            modelBuilder.Entity<SocialMedia>().HasData(
                new SocialMedia
                {
            Id = 1,
                    SettingsId = 1,

                    HeaderText = "Follow me @",

                    InstagramVisible = true,
                    InstagramUrl = "https://www.instagram.com/__INSTAGRAM_USERNAME__",

                    FacebookVisible = true,
                    FacebookUrl = "https://www.facebook.com/__FACEBOOK_USERNAME__",

                    TwitterVisible = true,
                    TwitterUrl = "https://x.com/__TWITTER_USERNAME__"
                }
            );



            modelBuilder.Entity<Branding>(entity =>
            {

                entity.Property(x => x.AppName)
                .IsRequired();

        entity.Property(x => x.Description).HasMaxLength(100);

        entity.HasOne(x => x.LoginImageAsset)
                .WithMany()
                .HasForeignKey(x => x.LoginImageAssetId)
                .OnDelete(DeleteBehavior.NoAction);

        entity.HasOne(x => x.FaviconAsset)
                  .WithMany()
                  .HasForeignKey(x => x.FaviconAssetId)
                  .OnDelete(DeleteBehavior.NoAction);
    });

            modelBuilder.Entity<Settings>()
                .HasOne(x => x.Branding)
                .WithOne(x => x.Settings)
                .HasForeignKey<Branding>(x => x.SettingsId)
                .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Settings>()
                .HasOne(x => x.SocialMedia)
                .WithOne(x => x.Settings)
                .HasForeignKey<SocialMedia>(x => x.SettingsId)
                .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Page>()
                .HasOne(page => page.Parent)
                .WithMany(page => page.Children)
                .HasForeignKey(page => page.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Page>()
                .Property(page => page.Date)
                .HasColumnType("date");


    modelBuilder.Entity<Asset>()
               .HasMany(p => p.Categories)
               .WithMany(c => c.Assets);

    modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

    modelBuilder.Entity<Asset>()
                .Property(e => e.Date)
                .HasColumnType("date");
    modelBuilder.Entity<Font>()
                .HasOne(f => f.Asset)
                .WithMany(a => a.Fonts)
                .HasForeignKey(f => f.AssetId)
                .OnDelete(DeleteBehavior.Restrict);
}
    }
}
