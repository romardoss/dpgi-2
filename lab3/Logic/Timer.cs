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
        public TimeSpan _duration;
        private readonly int minutes;
        private readonly int seconds;

        public System.Timers.Timer TTimer
        {
            get { return timer; }
        }
        //private TimeSpan externalTimer;
        //private TextBlock textBlock;

        public string duration
        {
            get
            {
                return $"{_duration:mm\\:ss}";
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
        }

        public Timer(int minutes, int seconds) : this(new TimeSpan(0, minutes, seconds))
        {
        }

        public Timer(TimeSpan duration)
        {
            minutes = duration.Minutes;
            seconds = duration.Seconds;
            this._duration = duration;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
        }

        public void ResetTimer()
        {
            _duration = new TimeSpan(0, minutes, seconds);
        }

        public void RestartTimer()
        {
            ResetTimer();
            timer.Enabled = true;
        }


        public bool IsFinished()
        {
            return _duration.Seconds == 0 && _duration.Minutes == 0;
        }

        public void StartTimer()
        {
            timer.Enabled = true;
        }

        public void DisposeTimer()
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
            
            _duration = _duration.Subtract(new TimeSpan(0, 0, 1));
            //textBlock.Text = $"{duration:mm\\:ss}";
            //externalTimer = externalTimer.Subtract(new TimeSpan(0, 0, 1));

        }
    }
}
