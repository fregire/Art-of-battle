﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;

namespace Art_of_battle.View
{
    partial class UserCardsControl : UserControl
    {
        private List<Panel> cardsOnPanel;
        private void InitializeComponent()
        {
            var cardsCount = mainForm.Game.FirstPlayer.Cards.Count;
            var table = new TableLayoutPanel();
            var cardsTable = GetCardsTable();

            SuspendLayout();
            cardsTable.Dock = DockStyle.Fill;
            Dock = DockStyle.Fill;
            table.Dock = DockStyle.Fill;

            table.RowCount = 1;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            table.ColumnCount = 3;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.Controls.Add(cardsTable, 1, 0);
            Controls.Add(table);
            ResumeLayout(false);
        }

        private TableLayoutPanel GetCardsTable()
        {
            var table = new TableLayoutPanel();
            var cards = mainForm.Game.FirstPlayer.Cards;
            cardsOnPanel = cards.Select(card => CreateCard(card)).ToList();

            table.RowCount = 1;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            table.ColumnCount = 2;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.Controls.Add(cardsOnPanel[0], 0, 0);
            table.Controls.Add(cardsOnPanel[1], 1, 0);

            return table;
        }

        private Panel CreateCard(Card card)
        {
            var cost = card.Cost;
            var creatureType = card.Creature.CreatureType;
            var image = new Bitmap(GetCardImage(card), 120, 80);
            var panel = new Panel();
            var pictBox = new PictureBox();
            var label = new Label();
            var creature = card.Creature.CreateCreature(mainForm.Game.FirstPlayer);

            label.Text = cost.ToString();
            label.Width = 20;
            pictBox.Image = image;
            pictBox.Dock = DockStyle.Fill;

            panel.Controls.Add(pictBox);
            panel.Controls.Add(label);

            pictBox.Click += (Object sender, EventArgs args) =>
            {
                mainForm.Game.PlaceCreatureOnField(creature);
            };

            label.Click += (Object sender, EventArgs args) => {
                mainForm.Game.PlaceCreatureOnField(creature);
            };

            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.Aqua;
            label.BringToFront();
            return panel;
        }

        private Image GetCardImage(Card card)
        {
            switch (card.Creature.CreatureType)
            {
                case CreatureType.Orc:
                    return Properties.Resources.Orc;
                case CreatureType.Knight:
                    return Properties.Resources.Knight;
                default:
                    return Properties.Resources.Image1;
            }
        }
    }
}