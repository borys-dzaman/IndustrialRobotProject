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

namespace IndustrialRobot.Views
{
    /// <summary>
    /// Interaction logic for ControlPanelView.xaml
    /// </summary>
    public partial class ControlPanelView : Window
    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeftWaistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 1,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 1," + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 2,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 2," + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 3,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 3," + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 4,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 4," + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 5,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 5," + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 6,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("SP " + JogSpeedSlider.Value.ToString() + "\r");
                main.serialPort.WriteLine("DJ 6," + TurningAngleSlider.Value.ToString() + "\r");
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
                main.serialPort.WriteLine("GO" + "\r");
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
                main.serialPort.WriteLine("GC" + "\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
