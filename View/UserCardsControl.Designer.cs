using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    partial class UserCardsControl : UserControl
    {
        private List<Panel> cardsOnPanel;
        private TableLayoutPanel cardsTable;
        private void InitializeComponent()
        {
            //SuspendLayout();

            InitComponent();

            //ResumeLayout(false);
        }

        private void InitComponent()
        {
            var cardsCount = mainForm.Game.FirstPlayer.Cards.Count;
            var table = new TableLayoutPanel();
            cardsTable = GetCardsTable();
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

            Font = new Font(mainForm.Font.Name, 12);
        }

        private TableLayoutPanel GetCardsTable()
        {
            var game = mainForm.Game;
            var table = new TableLayoutPanel();
            var cards = game.FirstPlayer.ChoosedCardsForGame;
            var columnsCount = game.GameSettings.CardsCountInPlayerHand;
            
            cardsOnPanel = cards.Select(card => CreateCardPanel(card)).ToList();

            table.RowCount = 1;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 150));

            table.ColumnCount = mainForm.Game.GameSettings.CardsCountInPlayerHand;

            for (var i = 0; i < columnsCount; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnsCount));

            for (var i = 0; i < columnsCount; i++)
                table.Controls.Add(cardsOnPanel[i], i, 0);

            return table;
        }

        public void RefreshCardsForGame()
        {
            var playerCards = mainForm.Game.FirstPlayer.ChoosedCardsForGame;

            foreach (var cardPanel in cardsOnPanel)
                cardsTable.Controls.Remove(cardPanel);

            cardsOnPanel.Clear();
            for (var i = 0; i < playerCards.Count; i++)
            {
                var cardPanel = CreateCardPanel(playerCards[i]);
                cardsTable.Controls.Add(cardPanel, i, 0);
                cardsOnPanel.Add(cardPanel);
            }

            cardsTable.Invalidate();
        }

        private Panel CreateCardPanel(Card card)
        {
            var game = mainForm.Game;
            var cost = card.Cost;
            var creatureType = card.Creature.CreatureType;
            var image = new Bitmap(GetCardImage(card), 100, 80);
            var panel = new Panel();
            var goldInfo = GetCardInfoPanel(
                card.Cost.ToString(),
                Resources.Sword
            );

            goldInfo.Size = new Size(50, 20);
            goldInfo.BackColor = Color.Transparent;
            panel.Height = 120;

            panel.Click += (Object sender, EventArgs args) =>
            {
                bool isEnoughGold;
                var isEnoughTime = battleControl.TimeElapsedSinceStart - card.TimeElapsed >= card.TimeReloadInMs 
                                   || card.TimeElapsed == 0;

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
            panel.Controls.Add(goldInfo);
            panel.Cursor = Cursors.Hand;

            return panel;
        }

        private Panel GetCardInfoPanel(string textInfo, Image icon)
        {
            var iconSize = new Size(20, 20);
            var iconControl = new PictureBox
            {
                Image = new Bitmap(icon, iconSize),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = iconSize
            };

            var label = new Label
            {
                Text = textInfo,
                BackColor = Color.Transparent,
                Location = new Point(iconControl.Right, 0)
            };

            var panel = new Panel();

            panel.Controls.Add(label);
            panel.Controls.Add(iconControl);

            return panel;

        }

        private Image GetCardImage(Card card)
        {
            return mainForm.GetCreatureImage(card.Creature.CreatureType);
        }
    }
}
