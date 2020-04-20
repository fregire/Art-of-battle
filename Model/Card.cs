using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class Card
    {
        public Creature Character { get; set; }
        public int Cost { get; set; }
        public int TimeReload { get; set; }
    }
}
