using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class Card
    {
        public Character Character { get; set; }
        public int Cost { get; set; }
        public bool IsReady { get; set; }

        public Card()
        {
            IsReady = true;
        }
    }
}
