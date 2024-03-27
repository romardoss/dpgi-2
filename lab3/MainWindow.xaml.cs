using lab3.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //var path = Environment.CurrentDirectory + "Sound\\The_xx_-_Intro.mp3";
            /*var path = "E:\\all_projects\\6sem\\interfaces\\dgpi-2\\lab3\\Sound\\alarm_beep.wav";
            SoundPlayer sound = new SoundPlayer(path);
            
            sound.Load();
            sound.Play();*/

            TimeSpan work = new TimeSpan(0, int.Parse(WorkMinBox.Text), int.Parse(WorkSecBox.Text));
            TimeSpan rest = new TimeSpan(0, int.Parse(RestMinBox.Text), int.Parse(RestSecBox.Text));
            SetupParameters.Work = work; 
            SetupParameters.Rest = rest;
            SetupParameters.Sets = int.Parse(SetsBox.Text);
            TimerWindow timerWindow = new TimerWindow();
            timerWindow.Show();
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetTextBoxAfterTextChanged(sender, "99", "1", 100, 1);
        }

        private void TextBoxMinutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetTextBoxAfterTextChanged(sender, "99", "00", 100, 0);
        }

        private void TextBoxSeconds_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetTextBoxAfterTextChanged(sender, "59", "00", 60, 0);
        }

        private void SetTextBoxAfterTextChanged(object sender, string upValue, string downValue, int upLimit, int downLimit)
        {
            string text = ((TextBox)sender).Text;

            if (text.Length > 2)
            {
                ((TextBox)sender).Text = upValue;
            }

            if (text.Length > 0)
            {
                int parse = int.Parse(text);
                if (parse >= upLimit)
                {
                    ((TextBox)sender).Text = upValue;
                }
                else if (parse < downLimit)
                {
                    ((TextBox)sender).Text = downValue;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
            {
                ((TextBox)sender).Text = "0" + ((TextBox)sender).Text;
            }
        }

        private void SetsPlusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusMinusButton(SetsBox, true);
        }

        private void SetsMinusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusMinusButton(SetsBox, false);
        }

        private void WorkMinusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusMinusButton(WorkSecBox, false);
            TextBox_LostFocus(WorkSecBox, e);
        }

        private void WorkPlusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusMinusButton(WorkSecBox, true);
            TextBox_LostFocus(WorkSecBox, e);
        }

        private void RestMinusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusMinusButton(RestSecBox, false);
            TextBox_LostFocus(RestSecBox, e);
        }

        private void RestPlusButton_Click(object sender, RoutedEventArgs e)
        {
            PlusMinusButton(RestSecBox, true);
            TextBox_LostFocus(RestSecBox, e);
        }

        private void PlusMinusButton(TextBox boxName, bool isAdd)
        {
            string text = boxName.Text;
            if (text.Length == 0)
            {
                text = "0";
            }

            int num = int.Parse(text);
            if (isAdd) { num++; }
            else { num--; }

            boxName.Text = num.ToString();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Автор: Берегун Роман, ТР-11");
        }
    }
}
