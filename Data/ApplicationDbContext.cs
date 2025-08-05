using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Models;

namespace WebAppBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; } = default!;
        public DbSet<Content> Contents { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Asset> Assets { get; set; } = default!;
        public DbSet<Color> Colors { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "Background Color", StartColor = "#000000", EndColor = "#000000", DarkStartColor = "#000000", DarkEndColor = "#000000" },
                new Color { Id = 2, Name = "Header Text", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" },
                new Color { Id = 3, Name = "Navbar Background Color", StartColor = "#ffff6a", EndColor = "#ffffff", DarkStartColor = "#052e16", DarkEndColor = "#ffffff" },
                new Color { Id = 4, Name = "Page Header Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 5, Name = "Content Header Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 6, Name = "Page Body Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 7, Name = "Content Body Text Color", StartColor = "#ffffff", EndColor = "#ffffff", DarkStartColor = "#000000", DarkEndColor = "#ffffff" },
                new Color { Id = 8, Name = "ScrollToTop Background Color", StartColor = "#052e16", EndColor = "#052e16", DarkStartColor = "#052e16", DarkEndColor = "#052e16" },
                new Color { Id = 9, Name = "Navbar Text Color", StartColor = "#000000", EndColor = "#000000", DarkStartColor = "#ffffff", DarkEndColor = "#ffffff" });


            modelbuilder.Entity<Page>().HasData(
                new Page { Id = 1, Title = "Home", Container = @"<p>Home</p>" },
                new Page { Id = 2, Title = "Production", Container = @"<p>Production</p>" },
                new Page { Id = 3, Title = "About", Container = @"<p>About</p>" },
                new Page { Id = 4, Title = "Contact", Container = @"<p>Email: <a href='mailto:info@__DOMAIN_NAME__'>info@__DOMAIN_NAME__</a></p>" },
                new Page
                {
                    Id = 5,
                    Title = "Privacy",
                    Container = ""
                }
            );

            modelbuilder.Entity<Content>().HasData(
                new Content { Id = 1, Title = "Welcome", Container = @"<p>Welcome</p>", PageId = 1 },
                new Content { Id = 2, Title = "This is what I'm working on", Container = @"<p>Process:</p>", PageId = 2 },
                new Content { Id = 3, Title = "Biography", Container = @"<p>Early Life</p>", PageId = 3 },
                new Content { Id = 4, Title = "Social Media", Container = @"<p>Faceberrk</p>", PageId = 4 },
                new Content { Id = 5, Title = "Cookie Policy", Container = @"<p>We don't use cookies</p>", PageId = 5 },
                new Content { Id = 6, Title = "Privacy Policy", Container = @"
            <p>At __DOMAIN_NAME__, we respect your privacy and are committed to protecting your personal data.</p>
            <h3>Cookies</h3>
            <p>Our website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.</p>
            <h3>Personal Data</h3>
            <p>We do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.</p>
            <h3>Data Security</h3>
            <p>The backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.</p>
            <h3>Your Rights</h3>
            <p>Since we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at <a href='mailto:info@__DOMAIN_NAME__'>info@__DOMAIN_NAME__</a></p>", PageId = 5, Date = new DateOnly(2025, 6, 3) }
            );

            modelbuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Film" },
                new Category { Id = 2, Name = "Liljevalchs 2001" },
                new Category { Id = 3, Name = "Stop-motion" },
                new Category { Id = 4, Name = "Press" },
                new Category { Id = 5, Name = "Foto" },
                new Category { Id = 6, Name = "Kontroversiell Konst" },
                new Category { Id = 7, Name = "Abstrakt Konst" },
                new Category { Id = 8, Name = "Akvarell Konst" }
            );

            modelbuilder.Entity<Asset>().HasData(
                new Asset { Id = 1, Name = "Dans under Vita Lakan", Author = "Fateme Gosheh", Description = "Inte sett än", FileUrl = "/Assets/Filmproduktion/image (287).jpg", CategoryId = 8, Type = 0 },
                new Asset { Id = 2, Name = "Aisha's Art", Author = "Fateme Gosheh", Description = "Gods butt from above", FileUrl = "/Assets/Kontroversiell Konst/image (3).jpg", CategoryId = 6, Type = 0 },
                new Asset { Id = 3, Name = "Kari - Jag är elak", Author = "Fateme Gosheh", Description = "Helt fantastiskt", FileUrl = "/Assets/Filmproduktion/image (304).jpg", CategoryId = 8, Type = 0 }
            );


            modelbuilder.Entity<Page>()
                .HasMany(c => c.Contents)
                .WithOne(p => p.Page)
                .HasForeignKey(p => p.PageId)
                .OnDelete(DeleteBehavior.SetNull);
            ;

            modelbuilder.Entity<Asset>()
               .HasMany(p => p.Categories)
               .WithMany(c => c.Assets)
               .UsingEntity(j => j.HasData(
                   new { CategoriesId = 8, AssetsId = 1 },
                   new { CategoriesId = 6, AssetsId = 2 },
                   new { CategoriesId = 8, AssetsId = 3 }
           ));

            modelbuilder.Entity<Asset>()
                .Property(e => e.Date)
                .HasColumnType("date");
            modelbuilder.Entity<Content>()
                .Property(e => e.Date)
                .HasColumnType("date");

        }
    }
}
