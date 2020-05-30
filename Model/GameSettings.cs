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
        public int MaxGoldAmount = 100;

        public Dictionary<int, int> PlayerLevelsInfo = new Dictionary<int, int>
        {
            {0, 0},
            {1, 100},
            {2, 300},
            {3, 500}
        };
    }
}
