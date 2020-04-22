﻿using System;
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
        public int Health { get; }
        public int Damage { get; }
        public Size Dimensions { get; }
        public int AttackRange { get; }
        public Direction Direction { get; }
        public MeleeCreature(
            CreatureType creatureType,
            int health, 
            int damage, 
            int attackRange, 
            Size dimensions,
            Direction direction)
        {
            CreatureType = creatureType;
            Health = health;
            Damage = damage;
            Dimensions = dimensions;
            AttackRange = attackRange;
            CurrHealth = health;
            Direction = direction;
        }

        public void Move()
        {
            var dPoint = new Point(0, 0);

            switch (Direction)
            {
                case Direction.Right:
                    dPoint.X = 1;
                    break;
                case Direction.Left:
                    dPoint.X = -1;
                    break;
            }


            Position = new Point(Position.X + dPoint.X, Position.Y + dPoint.Y);

            PositionChanged?.Invoke(this);
        }

        public event Action<ICreature> PositionChanged;

        public void Attack(ICreature enemy)
        {
            enemy.GetDamage(Damage);
            Attacked?.Invoke(this);
        }

        public event Action<ICreature> Attacked;

        public void GetDamage(int damage)
        {
            CurrHealth -= damage;

            //Do smth
            if (IsDead())
                return;
        }

        private bool IsDead()
        {
            return CurrHealth <= 0;
        }

        private bool IsEnemyInAttackRange(ICreature enemy)
        {
            switch (Direction)
            {
                case Direction.Right:
                    return Position.X + Dimensions.Width + AttackRange >= enemy.Position.X;
                case Direction.Left:
                    return Position.X - AttackRange <= enemy.Position.X + Dimensions.Width;
                default:
                    return false;
            }
        }

        private int GetDistanceToEnemy(ICreature enemy)
        {
            var result = 0;

            switch (Direction)
            {
                case Direction.Right:
                    result = enemy.Position.X - (Position.X + Dimensions.Width);
                    break;
                case Direction.Left:
                    return Position.X - (enemy.Position.X + enemy.Dimensions.Width);
                    break;
                default:
                    return 0;
            }

            return Math.Abs(result);
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
                if (IsEnemyInAttackRange(e) &&  distance < minDistance)
                {
                    minDistance = distance;
                    enemy = e;
                }
            }

            return enemy != null;
        }

        public void Act(HashSet<ICreature> enemies)
        {
            ICreature enemy = null;
            var IsAnyEnemyInAttackRange = TryGetEnemyInAttackRange(enemies, out enemy);

            if (IsAnyEnemyInAttackRange)
                Attack(enemy);
            else
                Move();
        }

        public ICreature CreateCreature(Player player)
        {
            return new MeleeCreature(
                CreatureType,
                Health,
                Damage,
                AttackRange,
                Dimensions,
                player.CreaturesDirection);
        }
    }
}
