using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class Card
    {
        public ICreature Creature { get; }
        public int Cost { get; }
        public int TimeReloadInMs { get; }
        public int TimeElapsed { get; set; }

        public Card(ICreature creature, int cost, int timeReloadInMs)
        {
            Creature = creature;
            Cost = cost;
            TimeReloadInMs = timeReloadInMs;
        }
    }
}
