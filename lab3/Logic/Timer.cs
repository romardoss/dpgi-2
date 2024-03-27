using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace lab3.Logic
{
    public class Timer
    {
        private readonly System.Timers.Timer timer;
        private TimeSpan duration;
        private readonly int minutes;
        private readonly int seconds;

        public System.Timers.Timer TTimer
        {
            get { return timer; }
        }
        //private TimeSpan externalTimer;
        //private TextBlock textBlock;

        public string Duration
        {
            get
            {
                return $"{duration:mm\\:ss}";
            }
        }

        public Timer(int minutes, int seconds)
        {
            timer = new System.Timers.Timer(1000);
            this.minutes = minutes;
            this.seconds = seconds;
            duration = new TimeSpan(0, minutes, seconds);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            //externalTimer = ext;
            //this.textBlock = textBlock;
        }

        public void ResetTimer()
        {
            duration = new TimeSpan(0, minutes, seconds);
        }

        public void RestartTimer()
        {
            ResetTimer();
            timer.Enabled = true;
        }


        public bool IsFinished()
        {
            return duration.Seconds == 0 && duration.Minutes == 0;
        }

        public void StartTimer()
        {
            timer.Enabled = true;
        }

        private void DisposeTimer()
        {
            timer.Stop();
            timer.Dispose();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (IsFinished()) { timer.Stop(); }
            //Console.WriteLine($"Time left: {duration:mm\\:ss}");
            //textBlock.Text = $"{duration:mm\\:ss}";
            //MessageBox.Show($"{duration:mm\\:ss}");
            
            duration = duration.Subtract(new TimeSpan(0, 0, 1));
            //textBlock.Text = $"{duration:mm\\:ss}";
            //externalTimer = externalTimer.Subtract(new TimeSpan(0, 0, 1));

        }
    }
}
