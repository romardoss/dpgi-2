using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab3.Effects
{
    public static class Sound
    {
        private static string pathBlip1 = Environment.CurrentDirectory + @"\Sound\blip-1.wav";
        private static SoundPlayer soundBlip1 = new SoundPlayer(pathBlip1);

        private static string pathBeep1 = Environment.CurrentDirectory + @"\Sound\beep-1.wav";
        private static SoundPlayer soundBeep1 = new SoundPlayer(pathBeep1);

        private static string pathBeep3 = Environment.CurrentDirectory + @"\Sound\beep-3.wav";
        private static SoundPlayer soundBeep3 = new SoundPlayer(pathBeep3);

        private static string pathBeep10 = Environment.CurrentDirectory + @"\Sound\beep-10.wav";
        private static SoundPlayer soundBeep10 = new SoundPlayer(pathBeep10);

        public static void PlayStartSound()
        {
            soundBeep1.Load();
            soundBeep1.Play();
        }

        public static void PlayButtonSound() 
        {
            soundBlip1.Load();
            soundBlip1.Play();
        }

        public static void PlayFinishSound()
        {
            soundBeep3.Load();
            soundBeep3.Play();
        }

        public static void PlayAttentionSound()
        {
            soundBeep10.Load();
            soundBeep10.Play();
        }
    }
}
