using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.Model
{
    public class GameSettings
    {
        public int CardsCountInPlayerHand = 2;
        public int GoldPerTick = 5;
        public int GoldAmountPerPlayer = 100;
        public int TimeReceivingCoinsInMs = 1000;

        public Dictionary<int, int> PlayerLevelsInfo = new Dictionary<int, int>
        {
            {1, 0},
            {2, 10},
            {3, 40},
            {4, 100}
        };
    }
}
