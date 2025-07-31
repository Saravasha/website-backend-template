using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppBackend.Data;

namespace WebAppBackend.Services
{
    public class OrphanAnnihilatorBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OrphanAnnihilatorBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromHours(24); // Run every 6 hours

        public OrphanAnnihilatorBackgroundService(IServiceProvider serviceProvider, ILogger<OrphanAnnihilatorBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrphanAnnihilatorBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var filePathProvider = scope.ServiceProvider.GetRequiredService<FilePathProvider>();

                    var annihilator = new OrphanAnnihilator(context, filePathProvider);
                    await annihilator.RemoveUnusedFilesAsync();

                    _logger.LogInformation("Orphan cleanup executed at: {Time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during orphan file cleanup.");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("OrphanAnnihilatorBackgroundService is stopping.");
        }
    }
}
