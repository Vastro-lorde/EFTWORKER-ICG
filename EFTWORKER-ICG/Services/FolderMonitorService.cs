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
        private XmlDeserialHelper _deserialHelper = new();

        public event EventHandler<FileCreatedEventArgs>? FileCreated;

        public FolderMonitorService(IConfiguration configuration)
        {
            _configuration = configuration;
            _folderPath = _configuration["RequestPath"]; // folder path to monitor
            _targetFileName = "REQUEST.txt"; // target file name
        }

        public void StartMonitoring()
        {
            /*if (!string.IsNullOrEmpty(_folderPath))
            {
                Console.WriteLine(_folderPath, _targetFileName);
                _watcher = new FileSystemWatcher(_folderPath, _targetFileName);
                _watcher.EnableRaisingEvents = true;
                _watcher.Created += OnFileCreated;
            }
            else
            {
                Console.WriteLine("Folder path is not configured.");
            }*/
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

        public (bool, TransactionInfo, string) CheckForFile()
        {
            try
            {
                TransactionInfo transactionInfoRequest = new();
                if (_folderPath == null)
                {
                    throw new Exception("null folder: " + _folderPath);
                }
                string filePath = Path.Combine(_folderPath, _targetFileName);
                bool fileExists = File.Exists(filePath);
                if (fileExists)
                {
                    string fileContents = File.ReadAllText(filePath);
                    transactionInfoRequest = _deserialHelper.ParseXmlString(fileContents);
                    FileCreated?.Invoke(this, new FileCreatedEventArgs { FilePath = filePath, FileContent = fileContents });
                    return (fileExists, transactionInfoRequest, filePath);
                }
                return (fileExists, transactionInfoRequest, filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateResponseText(string response)
        {
            string filePath = Path.Combine(_folderPath, "RESPONSE.txt");
            try
            {
                // Write the response content to the file
                await File.WriteAllTextAsync(filePath, response);

                Console.WriteLine("Response file created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating response file: {ex.Message}");
            }
        }
    }
}
