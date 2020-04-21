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
        int Health { get; }
        void MoveTo();
        void Attack();
        ICreature CreateCreature();
    }
}
