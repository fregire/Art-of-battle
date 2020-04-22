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
        int Health { get; }
        int CurrHealth { get; set; }
        void Act(Game game, Player player);
        ICreature CreateCreature(Player player);
        void GetDamage(int damage);
    }
}
