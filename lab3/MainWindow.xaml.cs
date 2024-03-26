using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AudioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ = new MediaElement
            {
                Volume = (double)e.NewValue
            };
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //var path = Environment.CurrentDirectory + "Sound\\The_xx_-_Intro.mp3";
            var path = "E:\\all_projects\\6sem\\interfaces\\dgpi-2\\lab3\\Sound\\alarm_beep.wav";
            SoundPlayer sound = new SoundPlayer(path);
            
            sound.Load();
            sound.Play();
        }
    }
}
