using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    public partial class FinishControl: Control
    {
        public FinishControl(MainForm mainForm) : base(mainForm)
        {
            SetBackground();
            ShowWinText();
            contentZone.Padding = new Padding(15);
            var winner = mainForm.Game.GetWinner();

            if (winner == mainForm.Game.FirstPlayer)
                ShowWinText();
            else
                ShowGameOverText();

            AddButtons();
        }

        private void ShowWinText()
        {
            var winLabel = new Label
            {
                Text = "Вы выиграли!",
                Size = new Size(contentZone.Width, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var goldRecvControl = GetReceivedInfoControl(
                new Bitmap(Resources.Coin, new Size(40, 40)),
                "+" + mainForm.Game.CurrentLevel.ReceivedGoldAmount);
            goldRecvControl.Size = new Size(200, 40);
            goldRecvControl.Location = new Point(30, winLabel.Bottom + 30);

            var expRecvControl = GetReceivedInfoControl(
                new Bitmap(Resources.Star, new Size(40, 40)),
                "+" + mainForm.Game.CurrentLevel.ReceivedExperienceAmount);
            expRecvControl.Location = new Point(30, goldRecvControl.Bottom + 15);

            contentZone.Controls.Add(winLabel);
            contentZone.Controls.Add(goldRecvControl);
            contentZone.Controls.Add(expRecvControl);
        }

        private Panel GetReceivedInfoControl(Image icon, string text)
        {
            var panel = new Panel();
            var iconControl = new PictureBox { Image = icon, Size = icon.Size};
            var textControl = new Label {Text = text, Size = new Size(100, 40)};

            textControl.Location = new Point(iconControl.Right, iconControl.Top);

            panel.Controls.Add(iconControl);
            panel.Controls.Add(textControl);

            return panel;
        }

        private void ShowGameOverText()
        {
            var gameOverLabel = new Label
            {
                Text = "Вы проиграли!",
                Size = new Size(contentZone.Width, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            contentZone.Controls.Add(gameOverLabel);
        }

        private void AddButtons()
        {
            var backToMainMenuBtn = mainForm.CreateMainButton("В главное меню");
            backToMainMenuBtn.Anchor = AnchorStyles.None;
            backToMainMenuBtn.Size = new Size(200, 50);
            backToMainMenuBtn.Font = new Font(backToMainMenuBtn.Font.Name, 12);
            backToMainMenuBtn.Click += (sender, args) => mainForm.ShowStartScreen();

            mainTable.Controls.Add(backToMainMenuBtn, 1, 2);
        }

    }
}
