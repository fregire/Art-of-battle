using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class HeroesControl : UserControl
    {
        private MainForm mainForm;
        public HeroesControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

    }
}
