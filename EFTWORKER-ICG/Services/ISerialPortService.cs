using System.IO.Ports;

namespace EFTWORKER_ICG.Services
{
    public interface ISerialPortService
    {
        Task ClosePortAsync();
        Task OpenPortAsync();
        void ReceiveDataAsync(object sender, SerialDataReceivedEventArgs e);
        Task SendDataAsync(string data);
        event EventHandler<string> DataReceived;
    }
}