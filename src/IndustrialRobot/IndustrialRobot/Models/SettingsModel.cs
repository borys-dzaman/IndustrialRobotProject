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
        //private SerialPort CurrentSerialPort { get { return $"{} {LastName}"; } }
        public static string Port { get; set; }
        //public Parity Parity{ get; set; }

        SerialPort CurrentSerialPort = new SerialPort { PortName = Port };

    }
}
