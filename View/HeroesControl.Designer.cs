using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

            table.Dock = DockStyle.Fill;
            heroesTable.Dock = DockStyle.Fill;

            table.ColumnCount = 3;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 980));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.RowCount = 3;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 660));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            //TODO: Edit for more than 4 cards
            for (var i = 0; i < mainForm.Game.Cards.Count; i++)
            {
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

            table.Controls.Add(heroesTable, 1, 1);
            Controls.Add(table);
            this.Dock = DockStyle.Fill;
            this.Name = "HeroesControl";
            this.ResumeLayout(false);
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
