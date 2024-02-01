using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTWORKER_ICG.Models
{
    public class FileCreatedEventArgs : EventArgs
    {
        public string FilePath { get; set; }
        public string FileContent { get; set; }
    }
}
