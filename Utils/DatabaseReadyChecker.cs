using Microsoft.EntityFrameworkCore;
using WebAppBackend.Data;

namespace WebAppBackend.Utils
{
    public static class DatabaseReadyChecker
    {
        public static async Task<bool> WaitForIdentityTablesAsync(
            ApplicationDbContext dbContext,
            int maxRetries = 5,
            int delayMs = 500)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var result = await dbContext.Database.ExecuteSqlRawAsync(
                        @"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AspNetRoles') SELECT 1 ELSE SELECT 0"
                    );

                    if (result == 1)
                        return true;
                }
                catch
                {
                    // Swallow and retry
                }

                await Task.Delay(delayMs);
            }

            return false;
        }
    }
}
