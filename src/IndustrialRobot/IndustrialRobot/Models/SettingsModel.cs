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
        public StopBits stopBits { get; set; }
        public int dataBits { get; set; }
        public int baudRate { get; set; }
        public static SerialPort serialPort { get; set; }
    }
}
