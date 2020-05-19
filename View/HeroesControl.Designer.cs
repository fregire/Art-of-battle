using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model.Creatures;
using NUnit.Framework.Constraints;

namespace Art_of_battle.View
{
    partial class HeroesControl
    {
        private void InitializeComponent()
        {
            var table = new TableLayoutPanel();
            var heroesTable = GetInitializedHeroesTable();
            var cards = mainForm.Game.Cards;
            var paddingValue = 15;
            var backBtn = mainForm.CreateMainButton("Back");

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
            backBtn.Anchor = AnchorStyles.None;
            backBtn.Click += OnBackBtnClick;

            this.Dock = DockStyle.Fill;
            this.Name = "HeroesControl";
            SetControlBackground();
            this.ResumeLayout(false);
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

        private void AddCards(TableLayoutPanel heroesTable)
        {
            var cards = mainForm.Game.Cards;

            for (var i = 0; i < mainForm.Game.Cards.Count; i++)
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

                group.Controls.Add(placeholder);
                group.Controls.Add(checkbox);

                heroesTable.Controls.Add(group, i, 0);
            }
        }

        private TableLayoutPanel GetInitializedHeroesTable()
        {
            var heroesTable = new TableLayoutPanel();
            var cardsCount = mainForm.Game.Cards.Count;

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
