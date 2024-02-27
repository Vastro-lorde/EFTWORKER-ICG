using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTWORKER_ICG.Services
{
    public class SerialPortService : IDataConnections, ISerialPortService
    {
        private readonly IConfiguration _configuration;
        private readonly SerialPort _serialPort;
        public event EventHandler<string> DataReceived;

        public SerialPortService(IConfiguration configuration)
        {
            _configuration = configuration;
            _serialPort = new SerialPort(_configuration["COMPort"], Convert.ToInt32(_configuration["BauRate"]), Parity.None, 8, StopBits.One);
            _serialPort.ReadBufferSize = 8192;
            _serialPort.DataReceived += ReceiveDataAsync;
        }

        public async Task OpenPortAsync()
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening port: {ex.Message}");
            }
        }

        public async Task ClosePortAsync()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing port: {ex.Message}");
            }
        }

        public async Task SendDataAsync(string data)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write(data);
                }
                else
                {
                    await OpenPortAsync();
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Write(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
            }
        }

        public void ReceiveDataAsync(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var serialPort = (SerialPort)sender;
                string receivedData = string.Empty;
                // Read all available data until there's no more data
                while (serialPort.BytesToRead > 0)
                {
                    receivedData += serialPort.ReadExisting();
                }
                // Process the received data here
                DataReceived.Invoke(this, receivedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
            }
        }
    }
}
