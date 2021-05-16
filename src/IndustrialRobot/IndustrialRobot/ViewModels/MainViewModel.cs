using Caliburn.Micro;
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
using IndustrialRobot.Views;
using GalaSoft.MvvmLight.Command;
using IndustrialRobot.Views.Interfaces;

namespace IndustrialRobot.ViewModels
{
    public class MainViewModel
    {
        public SerialPort serialPort = new SerialPort();
        public ICommand StartButtonCommand { get; set; }        
        public ICommand SettingsButtonCommand { get; set; }
        public RelayCommand<ICloseable> ExitButtonCommand { get; private set; }

        public MainViewModel()
        {
            StartButtonCommand = new RelayCommand(ExecuteStartButton, CanExecuteStartButton);
            SettingsButtonCommand = new RelayCommand(ExecuteSettingsButton, CanExecuteSettingsButton);
            ExitButtonCommand = new RelayCommand<ICloseable>(CloseWindow);     
        }
        private bool CanExecuteStartButton(object parameter)
        {
            return true;
        }

        private void ExecuteStartButton(object parameter)
        {
            serialPort.Close();
            try
            {
                if (serialPort.IsOpen == false)
                {
                    serialPort.PortName = Properties.Settings.Default.Port;
                    serialPort.BaudRate = Properties.Settings.Default.BaudRate;
                    serialPort.Parity = Properties.Settings.Default.Parity;
                    serialPort.DataBits = Properties.Settings.Default.DataBits;
                    serialPort.StopBits = Properties.Settings.Default.StopBits;
                    serialPort.Open();
                }
                ControlPanelView controlPanelView = new ControlPanelView(serialPort);
                serialPort.Write("SP 1" + "\r");
                serialPort.Write("WH" + "\r");
                controlPanelView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanExecuteSettingsButton(object parameter)
        {
            return true;
        }

        private void ExecuteSettingsButton(object parameter)
        {
            SettingsView settingsWindow = new SettingsView();
            settingsWindow.Show();
        }
        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                serialPort.Close();
                window.Close();
                Application.Current.Shutdown();
            }
        }
    }
    
}
