using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Art_of_battle.Model.Creatures;

namespace Art_of_battle.Model
{
    public interface ICreature
    {
        Point Position { get; set; }
        Size Dimensions { get; }
        int MaxHealth { get; }
        int CurrHealth { get; set; }
        Player Player { get; }
        void Act(HashSet<ICreature> enemies);
        void Attack(ICreature enemy);
        ICreature CreateCreature(Player player);
        void AcceptDamage(int damage);
        bool IsAlive();
        CreatureType CreatureType { get; }
    }
}
