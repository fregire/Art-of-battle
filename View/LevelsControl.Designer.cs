using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Art_of_battle.Model;
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

        private void CreateLvlsNamePlates(List<Level> lvls)
        {
            var paddingValue = 15;
            var yCoord = paddingValue;
            var xCoord = paddingValue;

            foreach (var lvl in lvls)
            {
                var lvlPanel = GetLvlPanel(lvl);
                lvlPanel.Location = new Point(xCoord, yCoord);
                yCoord += lvlPanel.Height + paddingValue;

                contentZone.Controls.Add(lvlPanel);
            }
        }

        private Panel GetLvlPanel(Level lvl)
        {
            var panel = new Panel
            {
                BackgroundImage = Resources.Panel,
                BackgroundImageLayout = ImageLayout.Stretch,
                Width = contentZone.Width - paddingValue * 2,
                Height = 95
            };

            if (lvl.IsLocked)
            {
                panel.Controls.Add(new Label
                {
                    Size = new Size(300, 50),
                    Text = "This level is locked.\nYou need " + lvl.LevelToUnlock + " lvl to unlock it",
                    Location = new Point(30, 30)
                });

                return panel;
            }

            var lvlPicture = new PictureBox
            {
                Image = new Bitmap(mainForm.GetLevelImage(lvl), new Size(100, 65)),
                Location = new Point(50, 15)
            };
            var lvlLabel = new Label
            {
                Size = new Size(70, 20),
                Text = lvl.LevelName.ToString(),
                Location = new Point(lvlPicture.Right + paddingValue, lvlPicture.Top)
            };
            var lvlExp = GetLvlPanelInfo(lvl.ReceivedExperienceAmount.ToString(),  Resources.Star);
            var lvlGold = GetLvlPanelInfo(lvl.ReceivedGoldAmount.ToString(), Resources.Coin);

            var startButton = GetStartButton();
            startButton.Location = new Point(panel.Width - 100, panel.Height - 74);
            startButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            startButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            startButton.FlatAppearance.BorderSize = 0;
            startButton.Click += (sender, args) => OnStartButtonClick(lvl);

            lvlPicture.Size = lvlPicture.Image.Size;
            lvlExp.Location = new Point(lvlPicture.Right + paddingValue, lvlLabel.Bottom);
            lvlGold.Location = new Point(lvlExp.Left, lvlExp.Bottom + 5);

            panel.Controls.Add(lvlLabel);
            panel.Controls.Add(lvlExp);
            panel.Controls.Add(lvlGold);
            panel.Controls.Add(startButton);
            panel.Controls.Add(lvlPicture);

            return panel;
        }

        private void OnStartButtonClick(Level lvl)
        {
            mainForm.Game.Start(lvl);
        }

        private Panel GetLvlPanelInfo(string infoName, Image icon)
        {
            var elem = new Panel { Size = new Size(100, 20)};
            var iconSize = new Size(20, 20);

            elem.Controls.Add(new PictureBox
            {
                Image = new Bitmap(icon, iconSize),
                Size = iconSize
            });
            elem.Controls.Add(new Label
            {
                Text = infoName,
                Location = new Point(iconSize.Width + 5, 3)
            });

            return elem;
        }

        private Button GetStartButton()
        {
            var startButton = new Button
            {
                Size = new Size(50, 50),
                BackgroundImage = new Bitmap(Resources.PlayButton, 50, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };

            startButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            startButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            startButton.FlatAppearance.BorderSize = 0;

            return startButton;
        }
    }
}
