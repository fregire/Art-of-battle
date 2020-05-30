using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;
using NUnit.Framework.Constraints;

namespace Art_of_battle.View
{
    partial class HeroesControl
    {
        private int choosedCardsCount;
        private Label warningLabel;
        private Button backBtn;

        private void InitializeComponent()
        {
            var warningPhrase = "You have to choose";
            var table = new TableLayoutPanel();
            var heroesTable = GetInitializedHeroesTable();
            var paddingValue = 15;
            backBtn = mainForm.CreateMainButton("Back");
            warningLabel = new Label();

            warningLabel.Text = String.Format(
                "***{0} {1} cards to play", 
                warningPhrase,
                mainForm.Game.GameSettings.CardsCountInPlayerHand);
            warningLabel.Location = new Point(10, 10);
            warningLabel.Size = new Size(400, 50);
            warningLabel.Visible = false;
            backBtn.Size = new Size(150, 50);

            table.ColumnCount = 3;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.RowCount = 3;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 400));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            heroesTable.Padding = new Padding(paddingValue, paddingValue, paddingValue, paddingValue);
            SetHeroestableBg(heroesTable);
            AddCards(heroesTable);
            table.Dock = DockStyle.Fill;
            heroesTable.Dock = DockStyle.Fill;

            table.Controls.Add(heroesTable, 1, 1);
            table.Controls.Add(backBtn, 1, 2);
            Controls.Add(table);
            Controls.Add(warningLabel);
            warningLabel.BringToFront();
            backBtn.Anchor = AnchorStyles.None;
            backBtn.Click += OnBackBtnClick;

            this.Dock = DockStyle.Fill;
            this.Name = "HeroesControl";
            SetControlBackground();
            this.ResumeLayout(false);
        }

        private void AddCards(TableLayoutPanel heroesTable)
        {
            var cards = mainForm.Game.FirstPlayer.Cards;

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];

                if (card.Creature.CreatureType == CreatureType.Castle)
                    continue;

                var group = new Panel();
                var placeholder = new Label();
                var checkbox = new CheckBox();
                checkbox.Dock = DockStyle.Top;
                placeholder.Text = cards[i].Creature.CreatureType.ToString();
                placeholder.Dock = DockStyle.Top;

                if (i < mainForm.Game.GameSettings.CardsCountInPlayerHand)
                    checkbox.Checked = true;

                checkbox.CheckedChanged += (sender, args) => { OnCheckboxChecked((CheckBox) sender, card); };
                group.Controls.Add(placeholder);
                group.Controls.Add(checkbox);
                heroesTable.Controls.Add(group, i, 0);

            }

            choosedCardsCount = mainForm.Game.GameSettings.CardsCountInPlayerHand;
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

        private void SetControlBackground()
        {
            this.BackgroundImage = mainForm.BackgroundImage;
            this.BackgroundImageLayout = mainForm.BackgroundImageLayout;
            this.BackColor = Color.Transparent;
        }

        private void SetHeroestableBg(TableLayoutPanel heroesTable)
        {
            heroesTable.BackgroundImage = Properties.Resources.menus_bg;
            heroesTable.BackgroundImageLayout = ImageLayout.Stretch;
        }


        private TableLayoutPanel GetInitializedHeroesTable()
        {
            var heroesTable = new TableLayoutPanel();
            var cardsCount = mainForm.Game.FirstPlayer.Cards.Count;

            heroesTable.ColumnCount = 4;
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            heroesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            heroesTable.RowCount = cardsCount / 4 + 1;
            for (var i = 0; i < heroesTable.RowCount; i++)
                heroesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));

            return heroesTable;
        }
    }
}
