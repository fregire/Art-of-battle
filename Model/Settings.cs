using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.Model
{
    class Settings
    {
        public int Volume = 10;
        public Size WindowSize = new Size(1280, 720);
        public Slot[] Slots = new Slot[4];

        public Dictionary<Slot, Keys> BindedSlotKeys;

        private Keys[] defaultKeys = new Keys[] { Keys.Q, Keys.W, Keys.E, Keys.R };

        public Settings()
        {
            BindedSlotKeys = new Dictionary<Slot, Keys>();

            for (var i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new Slot();
                BindedSlotKeys.Add(Slots[i], defaultKeys[i]);
            }
        }

        public void ChangSlotKey(Slot slotToChange, Keys newKey)
        {
            BindedSlotKeys[slotToChange] = newKey;
        }
    }
}
