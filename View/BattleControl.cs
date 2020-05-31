using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
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
        public bool Paused;

        public BattleControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.game = mainForm.Game;
            InitializeComponent();
        }

        private void ShowBattleBeginsImage()
        {
            var sprite = spriteController.DuplicateSprite("BattleBegins");
            sprite.AutomaticallyMoves = false;
            sprite.PutBaseImageLocation(sprite.GetSize.Width, sprite.GetSize.Height + 200);
            Pause();
        }

        public void Start()
        {
            fieldArea.BackgroundImage = mainForm.GetLevelImage(game.CurrentLevel);
            spriteController = new SpriteController(fieldArea);
            spriteController.DestroyAllSprites();

            LoadSprites();
            InitCastlesPositions();
            InitCreaturesSpawnPoint();

            spriteController.DoTick += OnTick;
            spriteController.ChangeTickInterval(tickIntervalInMs);

            CreatureCardPlaced(game.FirstPlayer.Castle);
            CreatureCardPlaced(game.SecondPlayer.Castle);

            game.CreaturePlacedOnField += CreatureCardPlaced;
            game.CreatureDeletedFromField += CreatureDeleted;
        }

        public void Pause()
        {
            spriteController.Pause();
            spriteController.DoTick -= OnTick;
            game.CreaturePlacedOnField -= CreatureCardPlaced;
            game.CreatureDeletedFromField -= CreatureDeleted;
            Paused = true;
        }

        public void UnPause()
        {
            spriteController.UnPause();
            spriteController.DoTick += OnTick;
            game.CreaturePlacedOnField += CreatureCardPlaced;
            game.CreatureDeletedFromField += CreatureDeleted;
            Paused = false;
        }

        public void Stop()
        {
            spriteController.DoTick -= OnTick;
            ClearSprites();
            spriteController = null;

            game.CreaturePlacedOnField -= CreatureCardPlaced;
            game.CreatureDeletedFromField -= CreatureDeleted;
        }

        public void ShowPausedControl()
        {
            pausedControl.Show();
            pausedControl.BringToFront();
        }

        public void HidePausedControl()
        {
            pausedControl.Hide();
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
            goldText.Text = mainForm.Game.FirstPlayer.BattleGoldAmount.ToString();
        }

        private void LoadSprites()
        {
            var knightImage = Resources.Knight1;
            var firstPlayerCastleImage = Resources.Castle_1;
            var secondPlayerCastleImage = Resources.Castle_2;
            var battleBeginsImage = Resources.BattleBegins;

            var knight = new Sprite(new Point(0, 0), spriteController, knightImage, 165, 124, 200, 3);
            knight.SetName("Knight");
            knight.AddAnimation(new Point(0, 124), knightImage, 193, 124, 200, 2);

            var firstPlayerCastle = new Sprite(spriteController, firstPlayerCastleImage, firstPlayerCastleImage.Width, firstPlayerCastleImage.Height);
            firstPlayerCastle.SetName("FirstPlayerCastle");

            var secondPlayerCastle = new Sprite(spriteController, secondPlayerCastleImage, secondPlayerCastleImage.Width, secondPlayerCastleImage.Height);
            secondPlayerCastle.SetName("SecondPlayerCastle");

            var battleBegins = new Sprite(spriteController, battleBeginsImage, battleBeginsImage.Width,
                battleBeginsImage.Height);
            battleBegins.SetName("BattleBegins");
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
            sprite.AutomaticallyMoves = true;
            sprite.SetSize(creature.Dimensions);
            sprite.MovementSpeed = 5;

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
