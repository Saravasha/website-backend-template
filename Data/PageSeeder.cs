using Microsoft.EntityFrameworkCore;
using WebAppBackend.Models;

namespace WebAppBackend.Data
{
    public class PageSeeder
    {
        public static async Task SeedPagesAsync(ApplicationDbContext context)
        {
            if (await context.Pages.AnyAsync())
            {
                return;
            }

            var home = new Page
            {
                Title = "Home",
                Body = "<p>Home</p>",
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var production = new Page
            {
                Title = "Production",
                Body = "<p>Production</p>",
                SortOrder = 1,
                Visible = true,
                IsOrphan = false
            };

            var about = new Page
            {
                Title = "About",
                Body = "<p>About</p>",
                SortOrder = 2,
                Visible = true,
                IsOrphan = false
            };

            var contact = new Page
            {
                Title = "Contact",
                Body = "<p>Email: <a href='mailto:info@__DOMAIN_NAME__'>info@__DOMAIN_NAME__</a></p>",
                SortOrder = 3,
                Visible = true,
                IsOrphan = false
            };

            var privacy = new Page
            {
                Title = "Privacy",
                Body = null,
                SortOrder = 4,
                Visible = true,
                IsOrphan = false
            };

            var welcome = new Page
            {
                Title = "Welcome",
                Body = "<p>Welcome</p>",
                Parent = home,
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var productionWork = new Page
            {
                Title = "This is what I'm working on",
                Body = "<p>Process:</p>",
                Parent = production,
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var biography = new Page
            {
                Title = "Biography",
                Body = "<p>Early Life</p>",
                Parent = about,
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var socialMedia = new Page
            {
                Title = "Social Media",
                Body = "<p>Faceberrk</p>",
                Parent = contact,
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var cookiePolicy = new Page
            {
                Title = "Cookie Policy",
                Body = "<p>We don't use cookies</p>",
                Parent = privacy,
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var privacyPolicy = new Page
            {
                Title = "Privacy Policy",
                Body = @"
                    <p>At __DOMAIN_NAME__, we respect your privacy and are committed to protecting your personal data.</p>
                    <h3>Cookies</h3>
                    <p>Our website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only by the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.</p>
                    <h3>Personal Data</h3>
                    <p>We do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.</p>
                    <h3>Data Security</h3>
                    <p>The backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.</p>
                    <h3>Your Rights</h3>
                    <p>Since we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at <a href='mailto:info@__DOMAIN_NAME__'>info@__DOMAIN_NAME__</a></p>",
                Parent = privacy,
                Date = DateOnly.FromDateTime(DateTime.Today),
                SortOrder = 1,
                Visible = true,
                IsOrphan = false
            };

            var policy1 = new Page
            {
                Title = "Policy 1",
                Body = "<p>Welcome</p>",
                Parent = welcome,
                SortOrder = 0,
                Visible = true,
                IsOrphan = false
            };

            var policy2 = new Page
            {
                Title = "Policy 2",
                Body = "<p>Process:</p>",
                Parent = welcome,
                SortOrder = 1,
                Visible = true,
                IsOrphan = false
            };

            var policy3 = new Page
            {
                Title = "Policy 3",
                Body = "<p>Early Life</p>",
                Parent = welcome,
                SortOrder = 2,
                Visible = true,
                IsOrphan = false
            };

            var policy4 = new Page
            {
                Title = "Policy 4",
                Body = "<p>Faceberrk</p>",
                Parent = welcome,
                SortOrder = 3,
                Visible = true,
                IsOrphan = false
            };

            var policy5 = new Page
            {
                Title = "Policy 5",
                Body = "<p>We don't use cookies</p>",
                Parent = welcome,
                SortOrder = 4,
                Visible = true,
                IsOrphan = false
            };

            context.Pages.AddRange(
                home,
                production,
                about,
                contact,
                privacy,
                welcome,
                productionWork,
                biography,
                socialMedia,
                cookiePolicy,
                privacyPolicy,
                policy1,
                policy2,
                policy3,
                policy4,
                policy5
            );

            await context.SaveChangesAsync();
        }
    }
}