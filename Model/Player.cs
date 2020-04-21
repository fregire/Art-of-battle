using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class Player
    {
        public string Name { get; }
        public Card[] Cards { get; }
        public Point CreaturesSpawnPoint;

        public Player(string name, params Card[] defaultCards)
        {
            Name = name;
            Cards = defaultCards;
        }
    }
}
