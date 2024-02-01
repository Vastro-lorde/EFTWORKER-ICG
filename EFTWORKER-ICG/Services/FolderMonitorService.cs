using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFTWORKER_ICG.Models;

namespace EFTWORKER_ICG.Services
{
    public class FolderMonitorService : IFolderMonitorService
    {
        private readonly IConfiguration _configuration;
        private readonly string? _folderPath;
        private readonly string _targetFileName;
        private FileSystemWatcher? _watcher;

        public event EventHandler<FileCreatedEventArgs>? FileCreated;

        public FolderMonitorService(IConfiguration configuration)
        {
            _configuration = configuration;
            _folderPath = _configuration["RequestPath"]; // folder path to monitor
            _targetFileName = "REQUEST.txt"; // target file name
        }

        public void StartMonitoring()
        {
            if (!string.IsNullOrEmpty(_folderPath))
            {
                _watcher = new FileSystemWatcher(_folderPath, _targetFileName);
                _watcher.EnableRaisingEvents = true;
                _watcher.Changed += OnFileCreated;
            }
            else
            {
                Console.WriteLine("Folder path is not configured.");
            }
        }

        public void StopMonitoring()
        {
            _watcher?.Dispose();
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            if (e.Name == _targetFileName)
            {
                try
                {
                    string filePath = Path.Combine(_folderPath, e.Name);
                    string fileContents = File.ReadAllText(filePath);
                    FileCreated?.Invoke(this, new FileCreatedEventArgs { FilePath = filePath, FileContent = fileContents });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading file: " + ex.Message);
                }
            }
        }
    }
}
