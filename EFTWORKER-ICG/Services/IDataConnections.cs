using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTWORKER_ICG.Services
{
    public interface IDataConnections
    {
        Task SendDataAsync(string data);
        void ReceiveDataAsync(object sender, SerialDataReceivedEventArgs e);
    }
}
