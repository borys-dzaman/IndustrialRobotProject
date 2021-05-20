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
using REghZyFramework.Themes;
using System.Threading;
using IndustrialRobot.ViewModels;
using System.Windows.Threading;
using System.Data;

namespace IndustrialRobot.Views
{
    /// <summary>
    /// Interaction logic for ControlPanelView.xaml
    /// </summary>
    public partial class ControlPanelView
    {
        //public MainView mainView;
        public SerialPort serialPort;
        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer bTimer;
        private bool blockAllButtons = false;
        private bool blockDownload = false;
        public string response = string.Empty;
        delegate void PrintToResponseTextBox(string text);
        private Thickness myThickness;
        private ushort positionNumber = 1;
        public string[] positionArray = new string[1000];
        private string modified_response;
        bool sendCommandsLogs = true;

        public ControlPanelView(SerialPort sP)
        {
            serialPort = sP;
            //mainView.Hide();
            InitializeComponent();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(IncomingDataEvent);
            PositionNumberTextBox.Text = positionNumber.ToString();
            UltraSafeMenuItem.Click += new RoutedEventHandler(UltraSafeModeRadioButton_Checked);
            SafeMenuItem.Click += new RoutedEventHandler(SafeModeRadioButton_Checked);
            LudicrousMenuItem.Click += new RoutedEventHandler(LudicrousModeRadioButton_Click);
        }

        private void UnlockButtonEvent(object sender, ElapsedEventArgs e)
        {
            blockAllButtons = false;
        }

        private void UnlockDownloadEvent(object sender, ElapsedEventArgs e)
        {
            blockDownload = false;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((MenuItem)sender).Uid))
            {
                case 0: ThemesController.SetTheme(ThemesController.ThemeTypes.Light); break;
                case 1: ThemesController.SetTheme(ThemesController.ThemeTypes.ColourfulLight); break;
                case 2: ThemesController.SetTheme(ThemesController.ThemeTypes.Dark); break;
                case 3: ThemesController.SetTheme(ThemesController.ThemeTypes.ColourfulDark); break;
            }
            e.Handled = true;
        }

        #region Emergency Buttons:
        private void StopJointsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serialPort.Write("HLT" + "\r");
                if (sendCommandsLogs) AddSendLog("HLT" + "\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddSendLog(string log)
        {
            LogRichTextBox.CaretPosition.Paragraph.FontSize = 12;
            LogRichTextBox.CaretPosition.Paragraph.Inlines.Add(new Run(DateTime.Now.ToString("HH:mm:ss ") + log) { Foreground = Brushes.Red });
            LogRichTextBox.LineDown();
        }

        private void AddReceivedLog(string log)
        {
            LogRichTextBox.CaretPosition.Paragraph.FontSize = 12;
            LogRichTextBox.CaretPosition.Paragraph.Inlines.Add(new Run(DateTime.Now.ToString("HH:mm:ss ") + log) { Foreground = Brushes.Blue });
            LogRichTextBox.LineDown();
        }

        private void ResetJointsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serialPort.Write("RS 0" + "\r");
                if (sendCommandsLogs) AddSendLog("RS 0" + "\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StopXYZButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serialPort.Write("HLT" + "\r");
                if (sendCommandsLogs) AddSendLog("HLT" + "\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetXYZButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serialPort.Write("RS 0" + "\r");
                if (sendCommandsLogs) AddSendLog("RS 0" + "\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Safety Modes:

        private void UltraSafeModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            aTimer = new System.Timers.Timer(2000);
            JogSpeedSlider.Value = 1;
            TurningAngleSlider.Value = 1;
            JogSpeedSlider.IsEnabled = false;
            TurningAngleSlider.IsEnabled = false;
            JogSpeedSlider.Maximum = 1;
            myThickness = new Thickness
            {
                Bottom = 28,
                Left = 0,
                Right = 89,
                Top = 20
            };
            JogSpeedSlider.Margin = myThickness;
            TurningAngleSlider.Maximum = 1;
            myThickness.Bottom = 0;
            myThickness.Left = 0;
            myThickness.Right = 89;
            myThickness.Top = 0;
            TurningAngleSlider.Margin = myThickness;
        }

        private void SafeModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            aTimer = new System.Timers.Timer(1000);
            JogSpeedSlider.IsEnabled = true;
            TurningAngleSlider.IsEnabled = true;
            JogSpeedSlider.Maximum = 10;
            myThickness = new Thickness
            {
                Bottom = 28,
                Left = 0,
                Right = 89,
                Top = 20
            };
            JogSpeedSlider.Margin = myThickness;
            TurningAngleSlider.Maximum = 10;
            myThickness.Bottom = 0;
            myThickness.Left = 0;
            myThickness.Right = 89;
            myThickness.Top = 0;
            TurningAngleSlider.Margin = myThickness;
        }

        private void LudicrousModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            blockAllButtons = false;
            aTimer.Enabled = false;
            JogSpeedSlider.IsEnabled = true;
            TurningAngleSlider.IsEnabled = true;
            JogSpeedSlider.Maximum = 30;
            myThickness = new Thickness
            {
                Bottom = 28,
                Left = 0,
                Right = 31,
                Top = 20
            };
            JogSpeedSlider.Margin = myThickness;
            TurningAngleSlider.Maximum = 30;
            myThickness.Bottom = 0;
            myThickness.Left = 0;
            myThickness.Right = 31;
            myThickness.Top = 0;
            TurningAngleSlider.Margin = myThickness;
        }
        private void UltraSafeModeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (UltraSafeMenuItem.IsChecked == false) UltraSafeMenuItem.IsChecked = true;
            if (SafeMenuItem.IsChecked == true) SafeMenuItem.IsChecked = false;
            if (LudicrousMenuItem.IsChecked == true) LudicrousMenuItem.IsChecked = false;
            LinearSpeedSlider.Value = 1;
            LinearSpeedSlider.IsEnabled = false;
        }

        private void SafeModeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (UltraSafeMenuItem.IsChecked == true) UltraSafeMenuItem.IsChecked = false;
            if (SafeMenuItem.IsChecked == false) SafeMenuItem.IsChecked = true;
            if (LudicrousMenuItem.IsChecked == true) LudicrousMenuItem.IsChecked = false;
            LinearSpeedSlider.IsEnabled = true;
            LinearSpeedSlider.Value = 1;
            LinearSpeedSlider.Maximum = 20;
        }

        private void LudicrousModeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to enable ludicrous mode?\n(you will be able to send an unlimited amount of commands)\n(you will also get access to forbidden speed)", "Attention!",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (UltraSafeMenuItem.IsChecked == true) UltraSafeMenuItem.IsChecked = false;
                if (SafeMenuItem.IsChecked == true) SafeMenuItem.IsChecked = false;
                if (LudicrousMenuItem.IsChecked == false) LudicrousMenuItem.IsChecked = true;
                LinearSpeedSlider.IsEnabled = true;
                LinearSpeedSlider.Maximum = 650;
            }

            else
            {
                UltraSafeModeRadioButton.IsChecked = true;
                if (UltraSafeMenuItem.IsChecked == false) UltraSafeMenuItem.IsChecked = true;
                if (SafeMenuItem.IsChecked == true) SafeMenuItem.IsChecked = false;
                if (LudicrousMenuItem.IsChecked == true) LudicrousMenuItem.IsChecked = false;
                LinearSpeedSlider.Value = 1;
                LinearSpeedSlider.IsEnabled = false;
            }
        }

        private void UltraSafeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (UltraSafeMenuItem.IsChecked == false) UltraSafeMenuItem.IsChecked = true;
            if (SafeMenuItem.IsChecked == true) SafeMenuItem.IsChecked = false;
            if (LudicrousMenuItem.IsChecked == true) LudicrousMenuItem.IsChecked = false;
            UltraSafeModeRadioButton.IsChecked = true;
            LinearSpeedSlider.Value = 1;
            LinearSpeedSlider.IsEnabled = false;
        }

        private void SafeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (UltraSafeMenuItem.IsChecked == true) UltraSafeMenuItem.IsChecked = false;
            if (SafeMenuItem.IsChecked == false) SafeMenuItem.IsChecked = true;
            if (LudicrousMenuItem.IsChecked == true) LudicrousMenuItem.IsChecked = false;
            SafeModeRadioButton.IsChecked = true;
        }

        private void LudicrousMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LudicrousModeRadioButton.IsChecked = true;
        }

        private void UltraSafeMenuItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SafeMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            LinearSpeedSlider.IsEnabled = true;
            LinearSpeedSlider.Value = 1;
            LinearSpeedSlider.Maximum = 20;
        }

        private void LudicrousMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            LinearSpeedSlider.IsEnabled = true;
            LinearSpeedSlider.Maximum = 650;
        }

        #endregion:

        #region Command Tool:

        private void IncomingDataEvent(object sender, SerialDataReceivedEventArgs e)
        {
            response = serialPort.ReadExisting();
            if (response != string.Empty) Dispatcher.BeginInvoke(new PrintToResponseTextBox(TextToResponse), new object[] { response });
        }

        private void TextToResponse(string text)
        {
            ResponseTextBox.Clear();
            ResponseTextBox.Text += text;
            AddReceivedLog(ResponseTextBox.Text);
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseTextBox.Clear();
            try
            {
                serialPort.Write("SP " + SpeedSlider.Value.ToString() + "\r");
                if (sendCommandsLogs) AddSendLog("SP " + SpeedSlider.Value.ToString() + "\r");
                serialPort.Write(CommandTextBox.Text.ToString() + "\r");
                if (sendCommandsLogs) AddSendLog(CommandTextBox.Text.ToString() + "\r");
                CommandTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #endregion

        #region Jog Joints Buttons:

        private void LeftWaistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 1,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 1,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 1," + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 1," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 2,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 2,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 2," + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 2," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 3,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 3,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 3," + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 3," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 4,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 4,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 4," + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 4," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 5,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 5,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 5," + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 5," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 6,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 6,-" + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    serialPort.Write("DJ 6," + TurningAngleSlider.Value.ToString() + "\r");
                    if (sendCommandsLogs) AddSendLog("DJ 6," + TurningAngleSlider.Value.ToString() + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        #endregion
        #region Other Jog Joints Buttons:

        private void Window_Closed(object sender, EventArgs e)
        {
            //mainView.Show();
        }

        private void OpenGripButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("GO" + "\r");
                    if (sendCommandsLogs) AddSendLog("GO" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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
                if (blockAllButtons == false)
                {
                    serialPort.Write("GC" + "\r");
                    if (sendCommandsLogs) AddSendLog("GC" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        #endregion

        #region Jog XYZ Buttons:

        private void UpXButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("DS " + XYZIncrement.Text + ",0,0" + "\r");
                    if (sendCommandsLogs) AddSendLog("DS " + XYZIncrement.Text + ",0,0" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void DownXButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("DS -" + XYZIncrement.Text + ",0,0" + "\r");
                    if (sendCommandsLogs) AddSendLog("DS -" + XYZIncrement.Text + ",0,0" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void UpYButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("DS 0," + XYZIncrement.Text + ",0" + "\r");
                    if (sendCommandsLogs) AddSendLog("DS 0," + XYZIncrement.Text + ",0" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void DownYButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("DS 0,-" + XYZIncrement.Text + ",0" + "\r");
                    if (sendCommandsLogs) AddSendLog("DS 0,-" + XYZIncrement.Text + ",0" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void UpZButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("DS 0,0," + XYZIncrement.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("DS 0,0," + XYZIncrement.Text + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void DownZButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("DS 0,0,-" + XYZIncrement.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("DS 0,0,-" + XYZIncrement.Text + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void UpAButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("WH" + "\r");
                    if (sendCommandsLogs) AddSendLog("WH" + "\r");
                    Thread.Sleep(50);
                    Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response }); // synchro. reads new WH response (idk but it reads 2 times)
                    response = ResponseTextBox.Text; // takes new response
                    double coordinate = 0;
                    string[] values = response.Split(',');
                    coordinate = Convert.ToDouble(values[3]);
                    string increment = ABCIncrement.Text.Replace('.', ',');
                    coordinate += Convert.ToDouble(increment);
                    values[3] = coordinate.ToString();
                    modified_response = string.Join(",", values);
                    serialPort.Write("PD 999 " + modified_response); // modified response already has "\r"
                    if (sendCommandsLogs) AddSendLog("PD 999 " + modified_response);
                    serialPort.Write("MS 999" + "\r");
                    if (sendCommandsLogs) AddSendLog("MS 999" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void DownAButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("WH" + "\r");
                    if (sendCommandsLogs) AddSendLog("WH" + "\r");
                    Thread.Sleep(50);
                    Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response }); // synchro. reads new WH response (idk but it reads 2 times)
                    response = ResponseTextBox.Text;
                    double coordinate = 0;
                    string[] values = response.Split(',');
                    coordinate = Convert.ToDouble(values[3]);
                    string increment = ABCIncrement.Text.Replace('.', ',');
                    coordinate -= Convert.ToDouble(increment);
                    values[3] = coordinate.ToString();
                    modified_response = string.Join(",", values);
                    serialPort.Write("PD 999 " + modified_response); // modified response already has "\r"
                    if (sendCommandsLogs) AddSendLog("PD 999 " + modified_response);
                    serialPort.Write("MS 999" + "\r");
                    if (sendCommandsLogs) AddSendLog("MS 999" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void UpBButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("WH" + "\r");
                    if (sendCommandsLogs) AddSendLog("WH" + "\r");
                    Thread.Sleep(50);
                    Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response }); // synchro. reads new WH response (idk but it reads 2 times)
                    response = ResponseTextBox.Text;
                    double coordinate = 0;
                    string[] values = response.Split(',');
                    coordinate = Convert.ToDouble(values[4]);
                    string increment = ABCIncrement.Text.Replace('.', ',');
                    coordinate += Convert.ToDouble(increment);
                    values[4] = coordinate.ToString();
                    modified_response = string.Join(",", values);
                    serialPort.Write("PD 999 " + modified_response); // modified response already has "\r"
                    if (sendCommandsLogs) AddSendLog("PD 999 " + modified_response);
                    serialPort.Write("MS 999" + "\r");
                    if (sendCommandsLogs) AddSendLog("MS 999" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void DownBButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("WH" + "\r");
                    if (sendCommandsLogs) AddSendLog("WH" + "\r");
                    Thread.Sleep(50);
                    Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response }); // synchro. reads new WH response (idk but it reads 2 times)
                    response = ResponseTextBox.Text;
                    double coordinate = 0;
                    string[] values = response.Split(',');
                    coordinate = Convert.ToDouble(values[4]);
                    string increment = ABCIncrement.Text.Replace('.', ',');
                    coordinate -= Convert.ToDouble(increment);
                    values[4] = coordinate.ToString();
                    modified_response = string.Join(",", values);
                    serialPort.Write("PD 999 " + modified_response); // modified response already has "\r"
                    if (sendCommandsLogs) AddSendLog("PD 999 " + modified_response);
                    serialPort.Write("MS 999" + "\r");
                    if (sendCommandsLogs) AddSendLog("MS 999" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void RightCButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("WH" + "\r");
                    if (sendCommandsLogs) AddSendLog("WH" + "\r");
                    Thread.Sleep(50);
                    Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response }); // synchro. reads new WH response (idk but it reads 2 times)
                    response = ResponseTextBox.Text;
                    double coordinate = 0;
                    string[] values = response.Split(',');
                    coordinate = Convert.ToDouble(values[5]);
                    string increment = ABCIncrement.Text.Replace('.', ',');
                    coordinate += Convert.ToDouble(increment);
                    values[5] = coordinate.ToString();
                    modified_response = string.Join(",", values);
                    serialPort.Write("PD 999 " + modified_response); // modified response already has "\r"
                    if (sendCommandsLogs) AddSendLog("PD 999 " + modified_response);
                    serialPort.Write("MS 999" + "\r");
                    if (sendCommandsLogs) AddSendLog("MS 999" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        private void LeftCButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    serialPort.Write("SD " + LinearSpeedTextBox.Text + "\r");
                    if (sendCommandsLogs) AddSendLog("SD " + LinearSpeedTextBox.Text + "\r");
                    serialPort.Write("WH" + "\r");
                    if (sendCommandsLogs) AddSendLog("WH" + "\r");
                    Thread.Sleep(50);
                    Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response }); // synchro. reads new WH response (idk but it reads 2 times)
                    response = ResponseTextBox.Text;
                    double coordinate = 0;
                    string[] values = response.Split(',');
                    coordinate = Convert.ToDouble(values[5]);
                    string increment = ABCIncrement.Text.Replace('.', ',');
                    coordinate -= Convert.ToDouble(increment);
                    values[5] = coordinate.ToString();
                    modified_response = string.Join(",", values);
                    serialPort.Write("PD 999 " + modified_response); // modified response already has "\r"
                    if (sendCommandsLogs) AddSendLog("PD 999 " + modified_response);
                    serialPort.Write("MS 999" + "\r");
                    if (sendCommandsLogs) AddSendLog("MS 999" + "\r");
                    if (SafeModeRadioButton.IsChecked == true || UltraSafeModeRadioButton.IsChecked == true)
                    {
                        blockAllButtons = true;
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

        #endregion
        #region Other Jog XYZ Buttons:



        #endregion

        #region Positions:

        class Position
        {
            public int Number { get; set; }
            public string Coordinates { get; set; }
        }

        private void UpPositionNumberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (positionNumber < 1000)
                {
                    positionNumber = (ushort)(Convert.ToUInt16(PositionNumberTextBox.Text) + 1);
                    PositionNumberTextBox.Text = positionNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DownPositionNumberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (positionNumber > 1)
                {
                    positionNumber = (ushort)(Convert.ToUInt16(PositionNumberTextBox.Text) - 1);
                    PositionNumberTextBox.Text = positionNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SavePositionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockAllButtons == false)
                {
                    if (Convert.ToUInt16(PositionNumberTextBox.Text) > 0 && Convert.ToUInt16(PositionNumberTextBox.Text) < 1000)
                    {
                        serialPort.Write("HE " + PositionNumberTextBox.Text + "\r");
                        if (sendCommandsLogs) AddSendLog("HE " + PositionNumberTextBox.Text + "\r");
                    }
                    else
                    {
                        MessageBox.Show("Position number must be at range: 0 < number < 1000", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PositionDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockDownload == false)
                {
                    blockDownload = true;
                    bTimer = new System.Timers.Timer(2000);
                    if (Convert.ToUInt16(StartReadTextBox.Text) > 0 && Convert.ToUInt16(StartReadTextBox.Text) < Convert.ToUInt16(FinishReadTextBox.Text) && Convert.ToUInt16(FinishReadTextBox.Text) < 1000)
                    {
                        for (int i = Convert.ToInt32(StartReadTextBox.Text); i <= Convert.ToInt32(FinishReadTextBox.Text); i++)
                        {
                            ResponseTextBox.Clear();
                            response = "";

                            serialPort.Write("PR " + $"{i}" + "\r");
                            if (sendCommandsLogs) AddSendLog("PR " + $"{i}" + "\r");
                            Thread.Sleep(50);
                            Dispatcher.Invoke(new PrintToResponseTextBox(TextToResponse), new object[] { response });
                            positionArray[i] = response;

                        }
                        var positions = new List<Position>();
                        for (int i = Convert.ToInt32(StartReadTextBox.Text); i <= Convert.ToInt32(FinishReadTextBox.Text); i++)
                        {
                            positions.Add(new Position { Number = i, Coordinates = positionArray[i] });
                        }
                        PositionDataGrid.ItemsSource = positions;

                        bTimer.Start();
                        bTimer.Elapsed += UnlockDownloadEvent;
                    }
                    else
                    {
                        MessageBox.Show("Position number must be at range: 0 < number < 1000", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearAllPositionsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serialPort.Write("PC 1,999" + "\r");
                if (sendCommandsLogs) AddSendLog("PC 1,999" + "\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
