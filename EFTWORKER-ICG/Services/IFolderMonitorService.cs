using EFTWORKER_ICG.Models;

namespace EFTWORKER_ICG.Services
{
    public interface IFolderMonitorService
    {
        event EventHandler<FileCreatedEventArgs> FileCreated;
        void StartMonitoring();
        void StopMonitoring();
        (bool, TransactionInfo, string) CheckForFile();
        Task CreateResponseText(string response);
    }
}