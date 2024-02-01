using EFTWORKER_ICG.Models;
using EFTWORKER_ICG.Services;

namespace EFTWORKER_ICG
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFolderMonitorService _folderMonitorService;

        public Worker(ILogger<Worker> logger, IFolderMonitorService folderMonitorService)
        {
            _logger = logger;
            _folderMonitorService = folderMonitorService;
            _folderMonitorService.FileCreated += FolderMonitorService_FileCreated;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _folderMonitorService.StartMonitoring();
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async void FolderMonitorService_FileCreated(object sender, FileCreatedEventArgs e)
        {
            // Handle the file content here
            Console.WriteLine("File created: " + e.FilePath);
            Console.WriteLine("File content: " + e.FileContent);
            await Task.Delay(10000);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _folderMonitorService.StopMonitoring();
            await base.StopAsync(cancellationToken);

        }
    }
}