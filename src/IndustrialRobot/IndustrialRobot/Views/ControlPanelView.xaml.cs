using System;
using System.Collections.Generic;
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
using System.IO.Ports;
using System.Timers;

namespace IndustrialRobot.Views
{
    /// <summary>
    /// Interaction logic for ControlPanelView.xaml
    /// </summary>
    public partial class ControlPanelView : Window
    {
        private static Timer aTimer;
        private bool blockButton = false;
        string response = string.Empty;
        delegate void PrintToResponseTextBox(string text);
        private MainView main;
        public ControlPanelView(MainView mainView)
        {
            main = mainView;
            mainView.Hide();
            InitializeComponent();
            main.serialPort.DataReceived += new SerialDataReceivedEventHandler(IncomingDataEvent);
        }

        private void IncomingDataEvent(object sender, SerialDataReceivedEventArgs e)
        {
            response = main.serialPort.ReadExisting();
            if (response != string.Empty) Dispatcher.BeginInvoke(new PrintToResponseTextBox(TextToResponse), new object[] { response });        
        }

        private void TextToResponse(string text)
        {
            ResponseTextBox.Text += text;
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseTextBox.Clear();
            try
            {             
                main.serialPort.WriteLine("SP " + SpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine(CommandTextBox.Text.ToString() + "\r");
                CommandTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UnlockButtonEvent(object sender, ElapsedEventArgs e)
        {
            blockButton = false;
        }

        private void LeftWaistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 1,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void RightWaistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 1," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeftShoulderButton_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 2,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RightShoulderButton_Click(object sender, RoutedEventArgs e)
        {          
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 2," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeftElbowButton_Click(object sender, RoutedEventArgs e)
        {         
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 3,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RightElbowButton_Click(object sender, RoutedEventArgs e)
        {          
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 3," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeftTwistButton_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 4,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RightTwistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 4," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeftPitchButton_Click(object sender, RoutedEventArgs e)
        {         
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 5,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RightPitchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 5," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeftRollButton_Click(object sender, RoutedEventArgs e)
        {         
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 6,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RightRollButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.WriteLine("DJ 6," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            main.Show();
        }

        private void OpenGripButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("GO" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseGripButton_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.WriteLine("GC" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockButton = true;
                        aTimer.Start();
                        aTimer.Elapsed += UnlockButtonEvent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UltraSafeModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            aTimer = new Timer(2000);
            JogSpeedSlider.Value = 1;
            TurningAngleSlider.Value = 1;
            JogSpeedSlider.IsEnabled = false;
            TurningAngleSlider.IsEnabled = false;
        }

        private void SafeModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            aTimer = new Timer(1000);
            JogSpeedSlider.IsEnabled = true;
            TurningAngleSlider.IsEnabled = true;
        }

        private void LudicrousModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            blockButton = false;
            aTimer.Enabled = false;
            JogSpeedSlider.IsEnabled = true;
            TurningAngleSlider.IsEnabled = true;
        }

        private void LudicrousModeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to enable ludicrous mode?\n(you will be able to send an unlimited amount of commands)", "Attention!",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes) LudicrousModeRadioButton.IsChecked = true;
            else
            {
                LudicrousModeRadioButton.IsChecked = false;
                UltraSafeModeRadioButton.IsChecked = true;
            }                      
        }
    }
}
