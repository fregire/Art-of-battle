using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    partial class LevelsControl
    {
        private void SetBackground()
        {
            BackgroundImage = Resources.mainmenubg1;
            BackgroundImageLayout = ImageLayout.Stretch;
            contentZone.BackgroundImage = Resources.menus_bg;
            contentZone.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void AddBackButton()
        {
            backBtn = mainForm.CreateMainButton("Back");
            backBtn.Size = new Size(150, 50);
            backBtn.Anchor = AnchorStyles.None;

            backBtn.Click += OnBackBtnClick;

            mainTable.Controls.Add(backBtn, 1, 2);
        }

        private void OnBackBtnClick(Object sender, EventArgs args)
        {
            mainForm.ShowStartScreen();
        }
    }
}
