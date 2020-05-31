using System;
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
            SuspendLayout();

            InitComponent();

            ResumeLayout(false);
        }

        private void InitComponent()
        {
            var cardsCount = mainForm.Game.FirstPlayer.Cards.Count;
            var table = new TableLayoutPanel();
            var cardsTable = GetCardsTable();
            cardsTable.Dock = DockStyle.Fill;
            table.Dock = DockStyle.Fill;

            table.RowCount = 1;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            table.ColumnCount = 3;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.Controls.Add(cardsTable, 1, 0);
            Controls.Add(table);
        }

        private TableLayoutPanel GetCardsTable()
        {
            var game = mainForm.Game;
            var table = new TableLayoutPanel();
            var cards = game.FirstPlayer.Cards;
            var columnsCount = game.GameSettings.CardsCountInPlayerHand;
            
            cardsOnPanel = cards.Select(card => CreateCard(card)).ToList();

            table.RowCount = 1;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 150));

            table.ColumnCount = mainForm.Game.GameSettings.CardsCountInPlayerHand;

            for (var i = 0; i < columnsCount; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnsCount));

            for(var i = 0; i < columnsCount; i++)
                table.Controls.Add(cardsOnPanel[i], i, 0);

            return table;
        }

        private Panel CreateCard(Card card)
        {
            var game = mainForm.Game;
            var cost = card.Cost;
            var creatureType = card.Creature.CreatureType;
            var image = new Bitmap(GetCardImage(card), 100, 80);
            var panel = new Panel();
            var label = new Label();

            label.Text = cost.ToString();
            label.Width = 30;
            panel.Height = 100;

            panel.Click += (Object sender, EventArgs args) =>
            {
                bool isEnoughGold;
                var isEnoughTime = battleControl.TimeElapsedSinceStart - card.TimeElapsed >= card.TimeReloadInMs || card.TimeElapsed == 0;

                if (isEnoughTime)
                {
                    isEnoughGold = game.PlaceCardCreatureOnField(card, game.FirstPlayer);

                    if (isEnoughGold)
                        card.TimeElapsed = battleControl.TimeElapsedSinceStart;
                }
                
                //Do smth if not isEnoughGold
            };

            panel.Margin = new Padding(15, 0, 15, 0);
            panel.BackgroundImage = image;
            panel.BackgroundImageLayout = ImageLayout.Center;
            panel.Controls.Add(label);
            label.BringToFront();

            return panel;
        }

        private Image GetCardImage(Card card)
        {
            return mainForm.GetCreatureImage(card.Creature.CreatureType);
        }
    }
}
