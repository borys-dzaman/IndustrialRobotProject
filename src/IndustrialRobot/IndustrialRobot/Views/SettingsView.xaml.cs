using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IndustrialRobot.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();

            string[] portsList;
            portsList = SerialPort.GetPortNames();
            foreach (string ports in portsList) this.PortComboBox.Items.Add(ports);
        }
        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
