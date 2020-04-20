using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    class Creature
    {
        public string Name;
        public int Health;
        public int CurrHealth;
        public Point Position;

        public Creature(string name, int health, Point startedPosition)
        {
            Name = name;
            Health = health;
            Position = startedPosition;
        }
        public Creature GenerateSameCreature()
        {
            return new Creature(Name, Health, Position);
        }
    }
}
