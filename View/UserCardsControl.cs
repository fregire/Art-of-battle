using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;

namespace Art_of_battle.View
{
    public partial class UserCardsControl : UserControl
    {
        private MainForm mainForm;
        private BattleControl battleControl;
        public UserCardsControl(MainForm mainForm, BattleControl battleControl)
        {
            this.mainForm = mainForm;
            this.battleControl = battleControl;

            InitializeComponent();
        }
    }
}
