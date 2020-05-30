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

        public Dictionary<int, int> PlayerLevelsInfo = new Dictionary<int, int>
        {
            {1, 0},
            {2, 100},
            {3, 300},
            {4, 500}
        };
    }
}
