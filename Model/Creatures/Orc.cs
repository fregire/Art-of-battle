using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model.Creatures
{
    public class Orc : ICreature
    {
        public Point Position { get; set; }
        public int Health { get; }

        public Orc(int health)
        {
            Health = health;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void MoveTo()
        {
            throw new NotImplementedException();
        }
        public ICreature CreateCreature()
        {
            return new Orc(Health);
        }
    }
}
