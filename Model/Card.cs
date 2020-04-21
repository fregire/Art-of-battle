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
        public int TimeReload { get; }

        public Card(ICreature creature, int cost, int timeReload)
        {
            Creature = creature;
            Cost = cost;
            TimeReload = timeReload;
        }
    }
}
