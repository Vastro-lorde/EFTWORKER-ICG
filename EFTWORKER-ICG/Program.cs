using EFTWORKER_ICG;
using EFTWORKER_ICG.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IFolderMonitorService, FolderMonitorService>();
builder.Services.AddSingleton<ISerialPortService, SerialPortService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
