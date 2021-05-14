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
        private Thickness myThickness;
        UInt16 positionNumber = 1;
        string[] positionArray = new string[999];
        //List<string> positionList = new List<string>();
        delegate void CheckModesRadioButtons();

        public ControlPanelView(MainView mainView)
        {
            main = mainView;
            mainView.Hide();
            InitializeComponent();
            main.serialPort.DataReceived += new SerialDataReceivedEventHandler(IncomingDataEvent);
            Uri iconUri = new Uri("rve2.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            PositionNumberTextBox.Text = positionNumber.ToString();
            //for (int i = 1; i < 1000; i++) positionList.Insert(i, "");
            //for (int i = 1; i < 1000; i++) positionNumberList.Insert(i, $"Pos. {i}:");
            UltraSafeMenuItem.Click += new RoutedEventHandler(UltraSafeModeRadioButton_Checked);  
            SafeMenuItem.Click += new RoutedEventHandler(SafeModeRadioButton_Checked);
            //LudicrousMenuItem.Click += new RoutedEventHandler(LudicrousModeRadioButton_Checked);
            LudicrousMenuItem.Click += new RoutedEventHandler(LudicrousModeRadioButton_Click);
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
                main.serialPort.Write("SP " + SpeedSlider.Value.ToString() + "\r");
                main.serialPort.Write(CommandTextBox.Text.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 1,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 1," + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 2,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 2," + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 3,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 3," + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 4,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 4," + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 5,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 5," + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 6,-" + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("SP " + JogSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DJ 6," + TurningAngleSlider.Value.ToString() + "\r");
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
                    main.serialPort.Write("GO" + "\r");
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
                    main.serialPort.Write("GC" + "\r");
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
            JogSpeedSlider.Maximum = 1;
            myThickness = new Thickness();
            myThickness.Bottom = 28;
            myThickness.Left = 0;
            myThickness.Right = 89;
            myThickness.Top = 20;
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
            aTimer = new Timer(1000);
            JogSpeedSlider.IsEnabled = true;
            TurningAngleSlider.IsEnabled = true;
            JogSpeedSlider.Maximum = 10;
            myThickness = new Thickness();
            myThickness.Bottom = 28;
            myThickness.Left = 0;
            myThickness.Right = 89;
            myThickness.Top = 20;
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
            blockButton = false;
            aTimer.Enabled = false;
            JogSpeedSlider.IsEnabled = true;
            TurningAngleSlider.IsEnabled = true;
            JogSpeedSlider.Maximum = 30;
            myThickness = new Thickness();
            myThickness.Bottom = 28;
            myThickness.Left = 0;
            myThickness.Right = 31;
            myThickness.Top = 20;
            JogSpeedSlider.Margin = myThickness;
            TurningAngleSlider.Maximum = 30;
            myThickness.Bottom = 0;
            myThickness.Left = 0;
            myThickness.Right = 31;
            myThickness.Top = 0;
            TurningAngleSlider.Margin = myThickness;                  
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
                if (blockButton == false)
                {
                    if (Convert.ToUInt16(PositionNumberTextBox.Text) > 0 && Convert.ToUInt16(PositionNumberTextBox.Text) < 1000)
                    {
                        main.serialPort.Write("HE " + PositionNumberTextBox.Text + "\r");
                        //positionList.Insert(Convert.ToUInt16(PositionNumberTextBox.Text), ResponseTextBox.Text);
                        positionArray[Convert.ToUInt16(PositionNumberTextBox.Text) - 1] = ResponseTextBox.Text;
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

        private void UpXButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.Write("SD " + LinearSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DS " + XYZIncrement.Text + ",0,0" + "\r");
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

        private void DownXButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.Write("SD " + LinearSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DS -" + XYZIncrement.Text + ",0,0" + "\r");
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

        private void UpYButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.Write("SD " + LinearSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DS 0," + XYZIncrement.Text + ",0" + "\r");
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

        private void DownYButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.Write("SD " + LinearSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DS 0,-" + XYZIncrement.Text + ",0" + "\r");
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

        private void UpZButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.Write("SD " + LinearSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DS 0,0," + XYZIncrement.Text + "\r");
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

        private void DownZButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (blockButton == false)
                {
                    main.serialPort.Write("SD " + LinearSpeedSlider.Value.ToString() + "\r");
                    main.serialPort.Write("DS 0,0,-" + XYZIncrement.Text + "\r");
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

        private void UpAButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DownAButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpBButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownBButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RightCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LeftCButton_Click(object sender, RoutedEventArgs e)
        {

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

        private void UltraSafeModeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            LinearSpeedSlider.Value = 1;
            LinearSpeedSlider.IsEnabled = false;
        }

        private void SafeModeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            LinearSpeedSlider.IsEnabled = true;
            LinearSpeedSlider.Value = 1;
            LinearSpeedSlider.Maximum = 20;
        }
    }
}
