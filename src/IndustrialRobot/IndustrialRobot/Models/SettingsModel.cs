using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialRobot.Models
{
    class SettingsModel
    {
        //private SerialPort CurrentSerialPort { get { return $"{} {LastName}"; } }
        public string Port { get; set; }
        public Parity Parity{ get; set; }

    }
}
