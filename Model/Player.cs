using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    class Player
    {
        public string Name { get; }
        public Creature[] Cards { get; }
        public Point CreaturesStartPosition;

        public Player(string name, Creature[] defaultCreatures)
        {
            Name = name;
            Cards = defaultCreatures;
        }
    }
}
