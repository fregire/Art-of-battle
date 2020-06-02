using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Art_of_battle.Model.Creatures
{
    public class MeleeCreature : ICreature
    {
        public Point Position { get; set; }
        public int CurrHealth { get; set; }
        public CreatureType CreatureType { get; }
        public int MaxHealth { get; }
        public int Damage { get; }
        public Size Dimensions { get; }
        public int AttackRange { get; }
        public Player Player { get; }
        public MeleeCreature(
            CreatureType creatureType,
            int health, 
            int damage, 
            int attackRange, 
            Size dimensions,
            Player player = null)
        {
            CreatureType = creatureType;
            MaxHealth = health;
            Damage = damage;
            Dimensions = dimensions;
            AttackRange = attackRange;
            CurrHealth = health;
            Player = player;

        }
        public void Act(HashSet<ICreature> enemies)
        {
            if (!IsAlive())
                return;

            ICreature enemy;

            var IsAnyEnemyInAttackRange = TryGetEnemyInAttackRange(enemies, out enemy);

            if (IsAnyEnemyInAttackRange && enemy.IsAlive())
                Attack(enemy);
            else
                Move();
        }

        public void Move()
        {
            var dPoint = new Point(0, 0);
            var distance = 10;

            switch (Player.CreaturesDirection)
            {
                case Direction.Right:
                    dPoint.X = distance;
                    break;
                case Direction.Left:
                    dPoint.X = -distance;
                    break;
            }

            Position = new Point(Position.X + dPoint.X, Position.Y + dPoint.Y);

            PositionChanged?.Invoke(this);
        }

        public event Action<ICreature> PositionChanged;

        public void Attack(ICreature enemy)
        {
            enemy.AcceptDamage(Damage);
            Attacked?.Invoke(this);
        }

        public event Action<ICreature> Attacked;

        public void AcceptDamage(int damage)
        {
            CurrHealth -= damage;

            if (CurrHealth <= 0)
            {
                CurrHealth = 0;
                Died?.Invoke(this);
            }

            AcceptedDamage?.Invoke(this);
        }

        public event Action<ICreature> AcceptedDamage;
        public event Action<ICreature> Died;

        public bool IsAlive()
        {
            return CurrHealth > 0;
        }

        private bool IsEnemyInAttackRange(ICreature enemy)
        {
            return GetDistanceToEnemy(enemy) <= AttackRange;
        }

        private int GetDistanceToEnemy(ICreature enemy)
        {
            var creatureBorders = GetBorders(this);
            var enemyBorders = GetBorders(enemy);
            var faceToFaceDist = Math.Abs(enemyBorders.Left - creatureBorders.Right);
            var backToBackDist = Math.Abs(creatureBorders.Left - enemyBorders.Right);

            return Math.Min(faceToFaceDist, backToBackDist);
        }

        private (int Left, int Right) GetBorders(ICreature creature)
        {
            return (Left: creature.Position.X, Right: creature.Position.X + creature.Dimensions.Width);
        }

        public bool TryGetEnemyInAttackRange(
            HashSet<ICreature> enemies,
            out ICreature enemy)
        {
            var minDistance = int.MaxValue;
            enemy = null;

            foreach (var e in enemies)
            {
                var distance = GetDistanceToEnemy(e);
                if (IsEnemyInAttackRange(e) && e.IsAlive() &&  distance < minDistance)
                {
                    minDistance = distance;
                    enemy = e;
                }
            }

            return enemy != null;
        }

        public ICreature CreateCreature(Player player)
        {
            return new MeleeCreature(
                CreatureType,
                MaxHealth,
                Damage,
                AttackRange,
                Dimensions,
                player);
        }
    }
}
