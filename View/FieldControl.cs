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

            game.CreaturePlacedOnField += InitCreaturePosition;
            game.Acted += Invalidate;
        }

        private void InitCastlesPositions()
        {
            var firstPlayerCastle = game.FirstPlayer.Castle;
            var secondPlayerCastle = game.SecondPlayer.Castle;

            // TODO: Why it isnt in the right corner
            firstPlayerCastle.Position = new Point(-100, Height - firstPlayerCastle.Dimensions.Height);
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

            foreach (var creature in game.GetPlayerCreaturesInGame(firstPlayer))
            {
                if (creature.IsAlive())
                    g.DrawImage(
                        GetCreatureImage(creature), 
                        creature.Position.X,
                        creature.Position.Y,
                        creature.Dimensions.Width,
                        creature.Dimensions.Height);
            }

            foreach (var creature in game.GetPlayerCreaturesInGame(secondPlayer))
            {
                if (creature.IsAlive())
                    g.DrawImage(
                        GetCreatureImage(creature),
                        creature.Position.X,
                        creature.Position.Y,
                        creature.Dimensions.Width,
                        creature.Dimensions.Height);
            }
        }


        private Image GetCreatureImage(ICreature creature)
        {   
            switch (creature.CreatureType)
            {
                case CreatureType.Orc:
                    return Properties.Resources.Orc;
                case CreatureType.Castle:
                    return creature.Player.CreaturesDirection == Direction.Right
                        ? Properties.Resources.Castle_2
                        : Properties.Resources.Castle_1;
                case CreatureType.Knight:
                    return Properties.Resources.Knight;
                default:
                    return Properties.Resources.Image1;
            }
        }

    }
}
