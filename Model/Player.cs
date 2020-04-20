using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    class Player
    {
        public string Name { get; }
        public Card[] Cards { get; }

        public Player(string name, Card[] defaultCards)
        {
            Name = name;
            Cards = defaultCards;
        }
    }
}
