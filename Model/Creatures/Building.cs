using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model.Creatures
{
    class Building : ICreature
    {
        public CreatureType CreatureType { get; }
        public Point Position { get; set; }
        public int Health { get; }
        public Size Dimensions { get; }
        public Building(
            CreatureType creatureType,
            int health,
            Size dimensions)
        {
            CreatureType = creatureType;
            Health = health;
            Dimensions = dimensions;
        }

        public void Attack(ICreature creature)
        {
            return;
        }

        public void Move()
        {
            return;
        }

        public ICreature CreateCreature()
        {
            return new Building(
                CreatureType,
                Health,
                Dimensions);
        }
    }
}
