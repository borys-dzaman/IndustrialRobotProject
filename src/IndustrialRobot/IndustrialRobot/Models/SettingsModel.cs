using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialRobot.Models
{
    public class SettingsModel
    {
        public string Port { get; set; }
        public Parity Parity{ get; set; }        
        public StopBits StopBits { get; set; }
        public int DataBits { get; set; }
        public int BaudRate { get; set; }
        public SerialPort SerialPort { get; set; }
    }
}
