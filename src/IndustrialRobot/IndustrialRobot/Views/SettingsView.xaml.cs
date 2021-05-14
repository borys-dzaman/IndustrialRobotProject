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
//using IndustrialRobot.Models;

namespace IndustrialRobot.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        string[] portsList;
        string[] paritiesList;
        string[] stopBitsList;
        SerialPort serialPort = new SerialPort();
        public SettingsView()
        {
            InitializeComponent();
            GetSettings();
            Uri iconUri = new Uri("rve2.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            portsList = SerialPort.GetPortNames();
            foreach (string ports in portsList) PortComboBox.Items.Add(ports);

            paritiesList = Enum.GetNames(typeof(Parity));
            foreach (string parities in paritiesList) ParityComboBox.Items.Add(parities);

            BaudRateComboBox.Items.Add("4800");
            BaudRateComboBox.Items.Add("9600");
            BaudRateComboBox.Items.Add("14400");

            DataBitsComboBox.Items.Add("5");
            DataBitsComboBox.Items.Add("6");
            DataBitsComboBox.Items.Add("7");
            DataBitsComboBox.Items.Add("8");

            stopBitsList = Enum.GetNames(typeof(StopBits));
            foreach (string stopBits in stopBitsList) StopBitsComboBox.Items.Add(stopBits);
        }
        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(PortComboBox.Text) && string.IsNullOrWhiteSpace(ParityComboBox.Text) && 
                string.IsNullOrWhiteSpace(BaudRateComboBox.Text) && string.IsNullOrWhiteSpace(DataBitsComboBox.Text) && 
                string.IsNullOrWhiteSpace(StopBitsComboBox.Text)) == false)
            {
                if (serialPort.IsOpen == false)
                {
                    SaveSettings();
                    GetSettings();
                }
            }
            Close();
        }

        public void GetSettings()
        {
            PortComboBox.SelectedItem = Properties.Settings.Default.Port;
            ParityComboBox.SelectedItem = Convert.ToString(Properties.Settings.Default.Parity);
            BaudRateComboBox.SelectedItem = Convert.ToString(Properties.Settings.Default.BaudRate);
            DataBitsComboBox.SelectedItem = Convert.ToString(Properties.Settings.Default.DataBits);
            StopBitsComboBox.SelectedItem = Convert.ToString(Properties.Settings.Default.StopBits);
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Port = (string)PortComboBox.SelectedItem;
            Properties.Settings.Default.Parity = (Parity)Enum.Parse(typeof(Parity), (string)ParityComboBox.SelectedItem);
            Properties.Settings.Default.BaudRate = Convert.ToInt32(BaudRateComboBox.SelectedItem);
            Properties.Settings.Default.DataBits = Convert.ToInt32(DataBitsComboBox.SelectedItem);
            Properties.Settings.Default.StopBits = (StopBits)Enum.Parse(typeof(StopBits), (string)StopBitsComboBox.SelectedItem);
            Properties.Settings.Default.Save();           
        }
    }
}
