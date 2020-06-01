using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;
using Art_of_battle.Properties;
using NUnit.Framework.Constraints;

namespace Art_of_battle.View
{
    partial class HeroesControl
    {
        private int choosedCardsCount;
        private Label warningLabel;
        private Button backBtn;

        private void SetBackground()
        {
            BackgroundImage = Resources.mainmenubg1;
            BackgroundImageLayout = ImageLayout.Stretch;
            contentZone.BackgroundImage = Resources.menus_bg;
            contentZone.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void InitContentZone()
        {
            var heroesTable = GetInitializedHeroesTable();
            var paddingValue = 15;
            backBtn = mainForm.CreateMainButton("Назад");
            InitWarningLabel();

            heroesTable.Dock = DockStyle.Fill;
            heroesTable.Padding = new Padding(paddingValue);
            AddCards(heroesTable);

            Controls.Add(warningLabel);
            warningLabel.BringToFront();

            backBtn.Size = new Size(150, 50);
            backBtn.Font = new Font(mainForm.Font.Name, 12);
            backBtn.Anchor = AnchorStyles.None;
            backBtn.Click += OnBackBtnClick;

            contentZone.Controls.Add(heroesTable);
            mainTable.Controls.Add(backBtn, 1, 2);
        }

        public void InitWarningLabel()
        {
            var warningPhrase = "Вам необходимо выбрать";
            warningLabel = new Label();

            warningLabel.Text = String.Format(
                "{0} {1} карты для начала игры",
                warningPhrase,
                mainForm.Game.GameSettings.CardsCountInPlayerHand);
            warningLabel.Location = new Point(10, 10);
            warningLabel.Size = new Size(400, 50);
            warningLabel.Visible = false;
            warningLabel.Font = new Font(mainForm.Font.Name, 12);
            warningLabel.ForeColor = Color.Black;
        }

        private void AddCards(TableLayoutPanel heroesTable)
        {
            var cards = mainForm.Game.FirstPlayer.Cards;
            var player = mainForm.Game.FirstPlayer;
            player.ChoosedCardsForGame.Clear();

            for (var i = 0; i < cards.Count; i++)
            {
                var group = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };

                SetDoubleBuffer(group);

                var checkbox = new CheckBox
                {
                    Size = new Size(20, 20)
                };
                var card = cards[i];
                var pictureSize = new Size(110, 80);

                if (card.Creature.CreatureType == CreatureType.Castle)
                    continue;

                var picture = new PictureBox
                {
                    Location = new Point(0, checkbox.Bottom),
                    Image = new Bitmap(mainForm.GetCreatureImage(card.Creature.CreatureType), pictureSize),
                    Size = pictureSize
                };

                var creatureName = new Label
                {
                    Font = new Font(mainForm.Font.Name, 12),
                    Text = cards[i].Creature.CreatureType.ToString(),
                    Location = new Point(0, picture.Bottom),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                if (i < mainForm.Game.GameSettings.CardsCountInPlayerHand)
                {
                    checkbox.Checked = true;
                    player.ChoosedCardsForGame.Add(cards[i]);
                }

                checkbox.CheckedChanged += (sender, args) => { OnCheckboxChecked((CheckBox) sender, card); };
                creatureName.Click += (sender, args) => OnCardClicked(checkbox);
                picture.Click += (sender, args) => OnCardClicked(checkbox);

                var isShowedInfo = false;
                var infoPanel = GetInfoPanel(card);
                infoPanel.Dock = DockStyle.Fill;
                var infoButton = GetInfoButton();
                infoButton.Cursor = Cursors.Hand;
                infoButton.Location = new Point(90, 0);
                infoButton.Click += (sender, args) =>
                {
                    if (isShowedInfo)
                    {
                        infoPanel.Hide();
                        isShowedInfo = false;
                    }
                    else
                    {
                        infoPanel.Show();
                        isShowedInfo = true;
                    }
                    infoButton.BringToFront();
                };

                group.Controls.Add(creatureName);
                group.Controls.Add(checkbox);
                group.Controls.Add(picture);
                group.Controls.Add(infoPanel);
                group.Controls.Add(infoButton);

                infoPanel.Hide();
                infoPanel.BringToFront();

                heroesTable.Controls.Add(group, i, 0);
            }

            choosedCardsCount = mainForm.Game.GameSettings.CardsCountInPlayerHand;
        }

        private Panel GetInfoPanel(Card card)
        {
            var panel = new Panel();
            var goldInfoIconSize = new Size(30, 30);
            var hammerIconSize = new Size(30, 40);
            var goldInfo = GetCardInfo(
                card.Cost.ToString(),
                new Bitmap(Resources.Sword, goldInfoIconSize));
            var hpInfo = GetCardInfo(
                card.Creature.MaxHealth.ToString(),
                new Bitmap(Resources.Heart, new Size(30, 30)));
            var damageInfo = GetCardInfo(
                card.Creature.Damage.ToString(),
                new Bitmap(Resources.Hammer, hammerIconSize));

            hpInfo.Location = new Point(0, goldInfo.Bottom);
            damageInfo.Location = new Point(0, hpInfo.Bottom);

            panel.Controls.Add(goldInfo);
            panel.Controls.Add(hpInfo);
            panel.Controls.Add(damageInfo);
            return panel;
        }

        private Panel GetCardInfo(string info, Image icon)
        {
            var group = new Panel {BackColor = Color.Transparent};
            var iconControl = new PictureBox
            {
                Image = icon,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = icon.Size
            };
            var name = new Label
            {
                Text = info,
                Location = new Point(iconControl.Right, 5),
                BackColor = Color.Transparent,
                Font = new Font(mainForm.Font.Name, 12)
            };

            group.Controls.Add(iconControl);
            group.Controls.Add(name);

            group.Size = new Size(70, 35);
            return group;
        }

        private Button GetInfoButton()
        {
            var buttonSize = new Size(20, 20);
            var button = new Button
            {
                Size = buttonSize,
                BackgroundImage = new Bitmap(Resources.InfoIcon, buttonSize),
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };

            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private void OnCardClicked(CheckBox checkBox)
        {
            checkBox.Checked = !checkBox.Checked;
        }

        private void OnCheckboxChecked(CheckBox checkbox, Card card)
        {
            var player = mainForm.Game.FirstPlayer;

            if (checkbox.Checked)
            {
                choosedCardsCount++;
                player.ChoosedCardsForGame.Add(card);
            }
            else
            {
                choosedCardsCount--;
                player.ChoosedCardsForGame.Remove(card);
            }

            if (choosedCardsCount != mainForm.Game.GameSettings.CardsCountInPlayerHand)
            {
                warningLabel.Visible = true;
                backBtn.Enabled = false;
            }
            else
            {
                warningLabel.Visible = false;
                backBtn.Enabled = true;
            }
        }

        private void SetDoubleBuffer(Panel panel)
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panel, new object[] { true });
        }

        private void OnBackBtnClick(Object sender, EventArgs args)
        {
            mainForm.ShowStartScreen();
        }

        private TableLayoutPanel GetInitializedHeroesTable()
        {
            var heroesTable = new TableLayoutPanel();
            var cardsCount = mainForm.Game.FirstPlayer.Cards.Count;

            heroesTable.ColumnCount = 3;
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            heroesTable.RowCount = cardsCount / 4 + 1;
            for (var i = 0; i < heroesTable.RowCount; i++)
                heroesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));

            return heroesTable;
        }
    }
}
