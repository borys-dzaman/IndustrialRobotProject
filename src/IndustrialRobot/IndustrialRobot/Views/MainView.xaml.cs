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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        SerialPort serialPort = new SerialPort();
        public MainView()
        {
            InitializeComponent();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsView settingsWindow = new SettingsView();
            settingsWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            serialPort.Close();
            Close();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            serialPort.Close();
            if (serialPort.IsOpen == false)
            {
                serialPort.PortName = Properties.Settings.Default.Port;
                serialPort.BaudRate = Properties.Settings.Default.BaudRate;
                serialPort.Parity = Properties.Settings.Default.Parity;
                serialPort.DataBits = Properties.Settings.Default.DataBits;
                serialPort.StopBits = Properties.Settings.Default.StopBits;
                serialPort.Open();
                //serialPort.DtrEnable = true; //do terminala
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("Test" + "\r");
        }
    }
}
