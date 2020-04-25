using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model.Creatures
{
    class Building : ICreature
    {
        public Point Position { get; set; }
        public int CurrHealth { get; set; }
        public CreatureType CreatureType { get; }
        public int MaxHealth { get; }
        public Size Dimensions { get; }
        public Direction Direction { get; }
        public Building(
            CreatureType creatureType,
            int health,
            Size dimensions,
            Direction direction)
        {
            CreatureType = creatureType;
            MaxHealth = health;
            Dimensions = dimensions;
            CurrHealth = health;
            Direction = direction;
        }

        public void AcceptDamage(int damage)
        {
            CurrHealth -= damage;
            
            if (CurrHealth <= 0)
                Died?.Invoke(this);

            Damaged?.Invoke(this);
        }

        public event Action<ICreature> Damaged;
        public event Action<ICreature> Died;

        public bool IsAlive()
        {
            return CurrHealth > 0;
        }

        public void Attack(ICreature creature)
        {
            return;
        }

        public void Move()
        {
            return;
        }

        public void Act(HashSet<ICreature> enemies)
        {
            return;
        }

        public ICreature CreateCreature(Player player)
        {
            return new Building(
                CreatureType,
                MaxHealth,
                Dimensions,
                player.CreaturesDirection);
        }
    }
}
