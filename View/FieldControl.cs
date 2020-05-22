using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;
using Art_of_battle.Properties;
using NUnit.Framework.Constraints;

namespace Art_of_battle.View
{
    public partial class FieldControl : UserControl
    {
        private MainForm mainForm;
        private Game game;

        public FieldControl(MainForm mainForm)
        {
            DoubleBuffered = true;
            InitializeComponent();
            this.mainForm = mainForm;
            this.game = mainForm.Game;

            InitCreaturesSpawnPoint();
            InitCastlesPositions();
            SetBackground();
            game.CreaturePlacedOnField += InitCreaturePosition;
            game.Acted += Invalidate;
        }

        private void SetBackground()
        {
            var image = Properties.Resources.game_background_3__2;
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.Transparent;
        }

        private void InitCastlesPositions()
        {
            var firstPlayerCastle = game.FirstPlayer.Castle;
            var secondPlayerCastle = game.SecondPlayer.Castle;

            firstPlayerCastle.Position = new Point(-80, Height - firstPlayerCastle.Dimensions.Height);
            secondPlayerCastle.Position = new Point(Width - secondPlayerCastle.Dimensions.Width + 100, Height - secondPlayerCastle.Dimensions.Height);
        }

        private void InitCreaturesSpawnPoint()
        {
            var firstPlayer = game.FirstPlayer;
            var secondPlayer = game.SecondPlayer;
            var distanceToCastle = 10;

            firstPlayer.CreaturesSpawnPoint = new Point(
                firstPlayer.Castle.Dimensions.Width + distanceToCastle,
                Height);

            secondPlayer.CreaturesSpawnPoint = new Point(
                Width - secondPlayer.Castle.Dimensions.Width - distanceToCastle,
                Height);
        }

        private void InitCreaturePosition(ICreature creature)
        {
            var player = creature.Player;
            var yCoord = player.CreaturesSpawnPoint.Y - creature.Dimensions.Height;
            var xCoord = 0;

            if (player.Equals(game.FirstPlayer))
                xCoord = player.CreaturesSpawnPoint.X;

            if (player.Equals(game.SecondPlayer))
                xCoord = player.CreaturesSpawnPoint.X - creature.Dimensions.Width;

            creature.Position = new Point(xCoord, yCoord);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawCreatures(g);
        }

        private void DrawCreatures(Graphics g)
        {
            var firstPlayer = game.FirstPlayer;
            var secondPlayer = game.SecondPlayer;
            var firstPlayerCreatures = game.GetPlayerCreaturesInGame(firstPlayer);
            var secondPlayerCreatures = game.GetPlayerCreaturesInGame(secondPlayer);
            var creaturesToDelete = new HashSet<ICreature>();

            foreach (var creature in firstPlayerCreatures)
            {
                if (creature.IsAlive())
                {
                    g.DrawImage(
                        GetCreatureImage(creature),
                        creature.Position.X,
                        creature.Position.Y,
                        creature.Dimensions.Width,
                        creature.Dimensions.Height);
                    DrawHealthBarForCreature(creature, g);
                }
            }

            foreach (var creature in secondPlayerCreatures)
            {
                if (creature.IsAlive())
                {
                    var image = GetCreatureImage(creature);
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(
                        image,
                        creature.Position.X,
                        creature.Position.Y,
                        creature.Dimensions.Width,
                        creature.Dimensions.Height);
                    DrawHealthBarForCreature(creature, g);
                }
            }
        }

        private void DrawHealthBarForCreature(ICreature creature, Graphics g)
        {
            var maxHealth = creature.MaxHealth;
            var currHealth = creature.CurrHealth;
            var barWidth = 50;
            var barHeight = 10;
            var posX = creature.Position.X + (creature.Dimensions.Width / 2) - (barWidth / 2);
            var posY = creature.Position.Y - 50;
            var bgBar = new Rectangle(posX, posY, barWidth, barHeight);
            var mainBar = new Rectangle(posX, posY, currHealth * barWidth / maxHealth, barHeight);

            g.FillRectangle(Brushes.White, bgBar);
            g.FillRectangle(Brushes.Red, mainBar);
            g.DrawRectangle(new Pen(Color.Black, 2), bgBar);
            g.DrawRectangle(new Pen(Color.Red), mainBar);
        }

        private Image GetCreatureImage(ICreature creature)
        {   
            switch (creature.CreatureType)
            {
                case CreatureType.Castle:
                    return creature.Player.CreaturesDirection == Direction.Right
                        ? Properties.Resources.Castle_2
                        : Properties.Resources.Castle_1;
                default:
                    return mainForm.GetCreatureImage(creature.CreatureType);
            }
        }

    }
}
