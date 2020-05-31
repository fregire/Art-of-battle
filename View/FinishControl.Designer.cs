using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Art_of_battle.Model;
using System.Windows.Forms;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    partial class FinishControl
    {
        private void SetBackground()
        {
            BackColor = Color.Transparent;
            contentZone.BackgroundImage = Resources.menus_bg;
            contentZone.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
