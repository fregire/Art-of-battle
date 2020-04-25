using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public interface ICreature
    {
        Point Position { get; set; }
        Size Dimensions { get; }
        int MaxHealth { get; }
        int CurrHealth { get; set; }
        void Act(HashSet<ICreature> enemies);
        ICreature CreateCreature(Player player);
        void AcceptDamage(int damage);
        bool IsAlive();
    }
}
