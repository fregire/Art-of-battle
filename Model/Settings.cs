using System;
using System.Drawing;

namespace Art_of_battle.Model
{
    public class Settings
    {
        public Size ScreenSize
        {
            get { return screenSize; }
            set
            {
                screenSize = value;
                ScreenSizeChanged?.Invoke(value);
            }
        }

        public double Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                VolumeChanged?.Invoke(value);
            }
        }
        public event Action<Size> ScreenSizeChanged;
        public event Action<double> VolumeChanged;
        private Size screenSize;
        private double volume;
    }
}