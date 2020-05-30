using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;
using Art_of_battle.Properties;
using SpriteLibrary;

namespace Art_of_battle.View
{
    public partial class BattleControl: UserControl
    {
        private MainForm mainForm;
        private Game game;
        private SpriteController spriteController;
        private Dictionary<ICreature, Sprite> creaturesInGame = new Dictionary<ICreature, Sprite>();
        private int tickIntervalInMs = 10;
        public int TimeElapsedSinceStart;

        public BattleControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.game = mainForm.Game;
            InitializeComponent();

            fieldArea.BackgroundImage = Resources.game_background_3__2;
            fieldArea.BackgroundImageLayout = ImageLayout.Stretch;
            fieldArea.Parent = mainForm;
        }

        public void Start()
        {
            spriteController = new SpriteController(fieldArea);
            spriteController.DoTick += OnTick;
            spriteController.ChangeTickInterval(tickIntervalInMs);
            spriteController.DestroyAllSprites();

            LoadSprites();
            InitCastlesPositions();
            InitCreaturesSpawnPoint();

            CreatureCardPlaced(game.FirstPlayer.Castle);
            CreatureCardPlaced(game.SecondPlayer.Castle);

            game.CreaturePlacedOnField += CreatureCardPlaced;
            game.CreatureDeletedFromField += CreatureDeleted;
        }

        public void CreatureDeleted(ICreature creature)
        {
            if (creaturesInGame.ContainsKey(creature))
            {
                creaturesInGame[creature].Destroy();
                creaturesInGame.Remove(creature);
            }
        }

        public void CreatureCardPlaced(ICreature creature)
        {
            creaturesInGame.Add(creature, GetCreatureSprite(creature));

            //GoldControl
            goldText.Text = mainForm.Game.FirstPlayer.CurrentGold.ToString();
        }

        private void LoadSprites()
        {
            var knightImage = Resources.Knight1;
            var firstPlayerCastleImage = Resources.Castle_1;
            var secondPlayerCastleImage = Resources.Castle_2;

            var knight = new Sprite(new Point(0, 0), spriteController, knightImage, 165, 124, 200, 3);
            knight.SetName("Knight");
            knight.AddAnimation(new Point(0, 124), knightImage, 193, 124, 200, 2);

            var firstPlayerCastle = new Sprite(spriteController, firstPlayerCastleImage, firstPlayerCastleImage.Width, firstPlayerCastleImage.Height);
            firstPlayerCastle.SetName("FirstPlayerCastle");

            var secondPlayerCastle = new Sprite(spriteController, secondPlayerCastleImage, secondPlayerCastleImage.Width, secondPlayerCastleImage.Height);
            secondPlayerCastle.SetName("SecondPlayerCastle");
        }

        public void Stop()
        {
            spriteController.DoTick -= OnTick;
            ClearSprites();
            spriteController = null;

            game.CreaturePlacedOnField -= CreatureCardPlaced;
            game.CreatureDeletedFromField -= CreatureDeleted;
        }

        private void ClearSprites()
        {
            spriteController.DestroyAllSprites();
            creaturesInGame.Clear();
        }

        private void OnTick(Object sender, EventArgs e)
        {
            if (game.Stage != GameStage.Started)
                return;

            TimeElapsedSinceStart += tickIntervalInMs;

            mainForm.Game.Act();

            var firstPlayerCreatures = game.GetPlayerCreaturesInGame(game.FirstPlayer);
            var secondPlayerCreatures = game.GetPlayerCreaturesInGame(game.SecondPlayer);

            foreach (var creature in firstPlayerCreatures)
            {
                if (creaturesInGame.ContainsKey(creature))
                {
                    if (creature.Position.X != creaturesInGame[creature].BaseImageLocation.X)
                    {
                        if (creaturesInGame[creature].AnimationIndex == 1)
                            creaturesInGame[creature].ChangeAnimation(0);
                        creaturesInGame[creature].PutBaseImageLocation(creature.Position.X, creature.Position.Y);
                    }
                    else
                    {
                        if (creaturesInGame[creature].AnimationIndex == 0)
                            creaturesInGame[creature].ChangeAnimation(1);
                    }
                }
            }

            foreach (var creature in secondPlayerCreatures)
            {
                if (creaturesInGame.ContainsKey(creature))
                {
                    if (creature.Position.X != creaturesInGame[creature].BaseImageLocation.X)
                    {
                        if (creaturesInGame[creature].AnimationIndex == 1)
                            creaturesInGame[creature].ChangeAnimation(0);
                        creaturesInGame[creature].PutBaseImageLocation(creature.Position.X, creature.Position.Y);
                    }
                    else
                    {
                        if (creaturesInGame[creature].AnimationIndex == 0)
                            creaturesInGame[creature].ChangeAnimation(1);
                    }
                }
            }
        }

        private Sprite GetCreatureSprite(ICreature creature)
        {
            var spriteName = GetSpriteName(creature);
            var sprite = spriteController.DuplicateSprite(GetSpriteName(creature));
            sprite.AutomaticallyMoves = false;
            sprite.SetSize(creature.Dimensions);
            sprite.MovementSpeed = 0;

            if (creature.CreatureType != CreatureType.Castle)
                InitCreaturePosition(creature);

            if (creature.Player == game.SecondPlayer)
                sprite.MirrorHorizontally = true;

            sprite.PutBaseImageLocation(creature.Position);

            return sprite;
        }

        private string GetSpriteName(ICreature creature)
        {
            if (creature.CreatureType == CreatureType.Castle)
                return creature.Player == game.FirstPlayer ? "FirstPlayerCastle" : "SecondPlayerCastle";
            else
                return creature.CreatureType.ToString();
        }

        private int bottomMargin = 10;
        private void InitCastlesPositions()
        {
            var firstPlayerCastle = game.FirstPlayer.Castle;
            var secondPlayerCastle = game.SecondPlayer.Castle;
            var background = spriteController.BackgroundImage;

            firstPlayerCastle.Position = new Point(
                -190, 
                background.Height - firstPlayerCastle.Dimensions.Height - bottomMargin);
            secondPlayerCastle.Position = new Point(
                background.Width - secondPlayerCastle.Dimensions.Width + 160, 
                background.Height - secondPlayerCastle.Dimensions.Height - bottomMargin);
        }

        private void InitCreaturesSpawnPoint()
        {
            var firstPlayer = game.FirstPlayer;
            var secondPlayer = game.SecondPlayer;
            var firstCastleRightX = firstPlayer.Castle.Position.X 
                                    + firstPlayer.Castle.Dimensions.Width;
            var secondCastleLeftX = secondPlayer.Castle.Position.X;
            var distanceToCastle = 10;

            firstPlayer.CreaturesSpawnPoint = new Point(
                firstCastleRightX + distanceToCastle, spriteController.BackgroundImage.Height - bottomMargin);

            secondPlayer.CreaturesSpawnPoint = new Point(
                secondCastleLeftX - distanceToCastle, spriteController.BackgroundImage.Height - bottomMargin);
        }

        private void InitCreaturePosition(ICreature creature)
        {
            var player = creature.Player;
            var yCoord = creature.Player.CreaturesSpawnPoint.Y - creature.Dimensions.Height;
            var xCoord = 0;

            if (player.Equals(game.FirstPlayer))
                xCoord = player.CreaturesSpawnPoint.X;

            if (player.Equals(game.SecondPlayer))
                xCoord = player.CreaturesSpawnPoint.X - creature.Dimensions.Width;

            creature.Position = new Point(xCoord, yCoord);
        }
    }
}
