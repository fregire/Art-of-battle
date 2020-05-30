using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Art_of_battle.Properties;
using Color = System.Drawing.Color;

namespace Art_of_battle.View
{
    partial class PausedControl
    {
        private void InitComponent()
        {
            SetBackground();
            CreateButtons();
        }

        private void SetBackground()
        {
            BackColor = Color.Transparent;
            mainTable.BackColor = Color.Transparent;
            contentZone.BackColor = Color.Transparent;
            contentZone.BackgroundImage = Resources.menus_bg;
            contentZone.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void CreateButtons()
        {
            var padding = 15;
            var btnHeight = 70;
            var btnWidth = 400;
            var btnSize = new Size(contentZone.Width - padding * 2, 90);
            var continueBtn = mainForm.CreateMainButton("Продолжить");
            var backToMainMenuBtn = mainForm.CreateMainButton("В главное меню");
            var againBtn = mainForm.CreateMainButton("Заново");

            continueBtn.Size = btnSize;
            againBtn.Size = btnSize;
            backToMainMenuBtn.Size = btnSize;

            continueBtn.Location = new Point(padding, padding);
            againBtn.Location = new Point(padding, continueBtn.Bottom + padding);
            backToMainMenuBtn.Location = new Point(padding, againBtn.Bottom + padding);

            continueBtn.Click += (sender, args) =>
            {
                battleControl.HidePausedControl();
                battleControl.UnPause();
            };

            contentZone.Controls.Add(continueBtn);
            contentZone.Controls.Add(againBtn);
            contentZone.Controls.Add(backToMainMenuBtn);
        }
    }
}
