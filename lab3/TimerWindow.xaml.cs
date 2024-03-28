using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using lab3.Effects;
using lab3.Logic;
using lab3.Setup;

namespace lab3
{
    /// <summary>
    /// Interaction logic for TimerWindow.xaml
    /// </summary>
    public partial class TimerWindow : Window
    {
        private readonly Logic.Timer TimerWork;
        private readonly Logic.Timer TimerRest;
        private readonly Logic.Timer TimerPreparation;
        private Logic.Timer CurrentTimer;
        //private readonly System.Timers.Timer timer;
        public string CurrentTime = "00:00";
        private bool IsPaused;
        private int Sets;

        public TimerWindow()
        {
            InitializeComponent();
            Sets = SetupParameters.Sets;
            TimerWork = new Logic.Timer(SetupParameters.Work);
            TimerWork.TTimer.Elapsed += UpdateTextBox;
            TimerWork.TTimer.Elapsed += SwitchTimer;
            TimerWork.TTimer.Elapsed += PlaySound;
            TimerRest = new Logic.Timer(SetupParameters.Rest);
            TimerRest.TTimer.Elapsed += UpdateTextBox;
            TimerRest.TTimer.Elapsed += SwitchTimer;
            TimerRest.TTimer.Elapsed += PlaySound;
            TimerPreparation = new Logic.Timer(0, 5);
            TimerPreparation.TTimer.Elapsed += UpdateTextBox;
            TimerPreparation.TTimer.Elapsed += SwitchTimer;
            TimerPreparation.TTimer.Elapsed += PlaySound;

            CurrentTimer = TimerPreparation;
            TimeTextBox.Text = CurrentTimer.duration.ToString();
            TimerPreparation.StartTimer();
            IsPaused = false;

            UpdateGridBackground();
            UpdateSetsBlock();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Sound.PlayButtonSound();
            TimerWork.DisposeTimer();
            TimerRest.DisposeTimer();
            TimerPreparation.DisposeTimer();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void PausePlayButton_Click(object sender, RoutedEventArgs e)
        {
            Sound.PlayButtonSound();
            if (!IsPaused)
            {
                MainGrid.Background = new SolidColorBrush(Color.FromRgb(250, 163, 0));
                CurrentTimer.TTimer.Stop();
                PlayImg.Source = new BitmapImage(new Uri(@"/icons/play.png", UriKind.Relative));
                IsPaused = true;
            }
            else
            {
                UpdateGridBackground();
                CurrentTimer.StartTimer();
                PlayImg.Source = new BitmapImage(new Uri(@"/icons/pause.png", UriKind.Relative));
                IsPaused = false;
            }
        }


        private void UpdateTextBox(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                TimeTextBox.Text = CurrentTimer.duration.ToString();
            });
        }

        private void PlaySound(object sender, ElapsedEventArgs e)
        {
            if(CurrentTimer.Duration.TotalSeconds < 3)
            {
                Sound.PlayAttentionSound();
            }
        }

        private void UpdateActivityBox()
        {
            Dispatcher.Invoke(() =>
            {
                ActivityTextBox.Text = DetermineCurrentActivity();
            });
        }

        private string DetermineCurrentActivity()
        {
            if (CurrentTimer.Equals(TimerPreparation))
            {
                return "PREPARE";
            }
            else if (CurrentTimer.Equals(TimerWork))
            {
                return "WORK";
            }
            else if (CurrentTimer.Equals(TimerRest))
            {
                return "REST";
            }
            else
            {
                return "ERROR";
            }
        }

        private void UpdateGridBackground()
        {
            Dispatcher.Invoke(() =>
            {
                string act = DetermineCurrentActivity().ToString();
                if (act == "PREPARE")
                {
                    MainGrid.Background = new SolidColorBrush(Color.FromRgb(244, 83, 138));
                }
                else if (act == "WORK")
                {
                    MainGrid.Background = new SolidColorBrush(Color.FromRgb(165, 221, 155));
                }
                else if (act == "REST")
                {
                    MainGrid.Background = new SolidColorBrush(Color.FromRgb(89, 213, 224));
                }
            });

        }

        private void UpdateSetsBlock()
        {
            Dispatcher.Invoke(() =>
            {
                SetsBlock.Text = Sets.ToString();
            });

        }

        private void SwitchTimer(object sender, ElapsedEventArgs e)
        {
            if (CurrentTimer.IsFinished()) 
            {
                Sound.PlayAttentionSound();
                System.Threading.Thread.Sleep(1000);
                
                if (CurrentTimer.Equals(TimerPreparation))
                {
                    CurrentTimer = TimerWork;
                }
                else if(CurrentTimer.Equals(TimerWork)) 
                {
                    CurrentTimer = TimerRest;
                }
                else if (CurrentTimer.Equals(TimerRest))
                {
                    Sets--;
                    UpdateSetsBlock();
                    CurrentTimer = TimerWork;
                }

                if (!FinishSessionIfEnd())
                {
                    UpdateGridBackground();
                    UpdateActivityBox();
                    CurrentTimer.RestartTimer();
                    UpdateTextBox(sender, e);
                    CurrentTimer.StartTimer();
                    Sound.PlayStartSound();
                }
                else
                {
                    Sound.PlayFinishSound();
                    //System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("You`ve finished!");
                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.Show();
                    //Close();
                }
                
            }
        }

        private bool FinishSessionIfEnd()
        {
            if(Sets <= 0)
            {
                TimerRest.TTimer.Stop();
                TimerWork.TTimer.Stop();
                return true;
            }
            return false;
        }
    }
}
