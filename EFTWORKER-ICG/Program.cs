using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EFTWORKER_ICG.Services;
using EFTWORKER_ICG; // Adjust the namespace as needed

namespace MyWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService() // Enable Windows Service functionality
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IFolderMonitorService, FolderMonitorService>();
                    services.AddSingleton<ISerialPortService, SerialPortService>();
                    services.AddHostedService<Worker>();
                });
    }
}



/*using EFTWORKER_ICG;
using EFTWORKER_ICG.Services;

var builder = Host.CreateApplicationBuilder(args).UseWindowsService();
builder.Services.AddSingleton<IFolderMonitorService, FolderMonitorService>();
builder.Services.AddSingleton<ISerialPortService, SerialPortService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
*/