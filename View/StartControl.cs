using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class StartControl : UserControl
    {
        private MainForm mainForm;
        public StartControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            heroesBtn.Click += (Object sender, EventArgs args) => mainForm.ShowHeroesScreen();
            startBtn.Click += (Object sender, EventArgs args) => mainForm.ShowLevelsScreen();
        }
    }
}
