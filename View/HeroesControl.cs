using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class HeroesControl : Control
    {
        private MainForm mainForm;
        public HeroesControl(MainForm mainForm) : base(mainForm)
        {
            this.mainForm = mainForm;
            SetBackground();
            InitContentZone();
        }

    }
}
