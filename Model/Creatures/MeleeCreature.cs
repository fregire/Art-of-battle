using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model.Creatures
{
    public class MeleeCreature : ICreature
    {
        public CreatureType CreatureType { get; }
        public Point Position { get; set; }
        public int Health { get; }
        public int Damage { get; }
        public Size Dimensions { get; }
        public int AttackRange { get; }
        public MeleeCreature(
            CreatureType creatureType,
            int health, 
            int damage, 
            int attackRange, 
            Size dimensions)
        {
            CreatureType = creatureType;
            Health = health;
            Damage = damage;
            Dimensions = dimensions;
            AttackRange = attackRange;
        }

        public void Attack(ICreature creature)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public ICreature CreateCreature()
        {
            return new MeleeCreature(
                CreatureType,
                Health,
                Damage,
                AttackRange,
                Dimensions);
        }
    }
}
