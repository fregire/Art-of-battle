using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    public partial class LevelsControl: Control
    {
        private Button backBtn;
        public LevelsControl(MainForm mainForm) : base(mainForm)
        {
            SetBackground();
            AddBackButton();
        }
    }
}
