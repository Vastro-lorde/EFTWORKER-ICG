using EFTWORKER_ICG.Models;
using EFTWORKER_ICG.Services;

namespace EFTWORKER_ICG
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFolderMonitorService _folderMonitorService;
        private readonly ISerialPortService _dataConnections;

        public Worker(ILogger<Worker> logger, IFolderMonitorService folderMonitorService, ISerialPortService dataConnections)
        {
            _logger = logger;
            _folderMonitorService = folderMonitorService;
            _dataConnections = dataConnections;
            _dataConnections.DataReceived += HandleDataReceived;
            //_folderMonitorService.FileCreated += FolderMonitorService_FileCreated; //not needed when not calling startMonitoring
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_folderMonitorService.StartMonitoring(); //not working any more.
            while (!stoppingToken.IsCancellationRequested)
            {
                ( bool newFile, TransactionInfo fileContent, string filePath) = _folderMonitorService.CheckForFile();
                if (newFile)
                {
                    await FolderMonitorService_FileCreated( fileContent, filePath);
                }
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task FolderMonitorService_FileCreated(TransactionInfo fileContent, string filePath)
        {
            // Handle the file content here
            await _dataConnections.OpenPortAsync();

            XmlDeserialHelper transactXmlHelper = new();
            string requestToPosData = transactXmlHelper.SerializeRequestString(fileContent.Importe, fileContent.NumTicket.ToString(), fileContent.SerieTicket, fileContent.IdTransaccion.ToString());
            Console.WriteLine(requestToPosData);
            await _dataConnections.SendDataAsync(requestToPosData);
            
            await Task.Delay(5000);
            /*await _dataConnections.ClosePortAsync();*/
        }

        private void HandleDataReceived(object sender, string receivedData)
        {
            // Process the received data here
            _folderMonitorService.CreateResponseText(receivedData);
            Console.WriteLine($"Received data at worker: {receivedData}");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _folderMonitorService.StopMonitoring();
            await _dataConnections.ClosePortAsync();
            await base.StopAsync(cancellationToken);
        }
    }
}