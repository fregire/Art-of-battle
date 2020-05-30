using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_of_battle.View
{
    public partial class PausedControl : Control
    {
        private BattleControl battleControl;
        public PausedControl(MainForm mainForm, BattleControl battleControl) : base(mainForm)
        {
            this.battleControl = battleControl;
            InitComponent();
        }
    }
}
