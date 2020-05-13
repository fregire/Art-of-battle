using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model.Creatures
{
    public class Building : ICreature
    {
        public Point Position { get; set; }
        public int CurrHealth { get; set; }
        public CreatureType CreatureType { get; }
        public int MaxHealth { get; }
        public Size Dimensions { get; }
        public Player Player { get; }
        public Building(
            CreatureType creatureType,
            int health,
            Size dimensions,
            Player player = null)
        {
            CreatureType = creatureType;
            MaxHealth = health;
            Dimensions = dimensions;
            CurrHealth = health;
            Player = player;
        }

        public void AcceptDamage(int damage)
        {
            CurrHealth -= damage;
            
            if (CurrHealth <= 0)
                Died?.Invoke();

            Damaged?.Invoke(this);
        }

        public event Action<ICreature> Damaged;
        public event Action Died;

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
                player);
        }
    }
}
