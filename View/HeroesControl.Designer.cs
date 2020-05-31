using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            backBtn = mainForm.CreateMainButton("Back");
            InitWarningLabel();

            heroesTable.Dock = DockStyle.Fill;
            heroesTable.Padding = new Padding(paddingValue);
            AddCards(heroesTable);

            Controls.Add(warningLabel);
            warningLabel.BringToFront();

            backBtn.Size = new Size(150, 50);
            backBtn.Anchor = AnchorStyles.None;
            backBtn.Click += OnBackBtnClick;

            contentZone.Controls.Add(heroesTable);
            mainTable.Controls.Add(backBtn, 1, 2);
        }

        public void InitWarningLabel()
        {
            var warningPhrase = "You have to choose";
            warningLabel = new Label();

            warningLabel.Text = String.Format(
                "***{0} {1} cards to play",
                warningPhrase,
                mainForm.Game.GameSettings.CardsCountInPlayerHand);
            warningLabel.Location = new Point(10, 10);
            warningLabel.Size = new Size(400, 50);
            warningLabel.Visible = false;
        }

        private void AddCards(TableLayoutPanel heroesTable)
        {
            var cards = mainForm.Game.FirstPlayer.Cards;

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var pictureSize = new Size(110, 80);

                if (card.Creature.CreatureType == CreatureType.Castle)
                    continue;

                var group = new Panel();
                group.Height = 150;
                var creatureName = new Label();
                creatureName.Font = new Font(mainForm.Font.Name, 12);
                var checkbox = new CheckBox();
                var picture = new PictureBox
                {
                    Location = new Point(0, checkbox.Bottom),
                    Image = new Bitmap(mainForm.GetCreatureImage(card.Creature.CreatureType), pictureSize),
                    Size = pictureSize
                };

                checkbox.BackColor = Color.Transparent;
                creatureName.Text = cards[i].Creature.CreatureType.ToString();
                creatureName.Location = new Point(0, picture.Bottom);
                creatureName.TextAlign = ContentAlignment.MiddleCenter;

                if (i < mainForm.Game.GameSettings.CardsCountInPlayerHand)
                    checkbox.Checked = true;

                checkbox.CheckedChanged += (sender, args) => { OnCheckboxChecked((CheckBox) sender, card); };

                creatureName.Click += (sender, args) => OnCardClicked(checkbox);
                picture.Click += (sender, args) => OnCardClicked(checkbox);
                group.Controls.Add(creatureName);
                group.Controls.Add(checkbox);
                group.Controls.Add(picture);
                heroesTable.Controls.Add(group, i, 0);

            }

            choosedCardsCount = mainForm.Game.GameSettings.CardsCountInPlayerHand;
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
                player.Cards.Add(card);
            }
            else
            {
                choosedCardsCount--;
                player.Cards.Remove(card);
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
                heroesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));

            return heroesTable;
        }
    }
}
