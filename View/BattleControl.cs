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
        public int TimeElapsedSinceLastPayment;
        public BattleControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.game = mainForm.Game;
            InitializeComponent();
        }

        public void Start()
        {
            cardsZone.RefreshCardsForGame();
            fieldArea.BackgroundImage = mainForm.GetLevelImage(game.CurrentLevel);
            spriteController = new SpriteController(fieldArea);
            spriteController.DestroyAllSprites();
            game.FirstPlayer.GoldChanged += ShowGoldAmount;

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

        public void RefreshCards()
        {
            cardsZone.RefreshCardsForGame();
        }

        public void Pause()
        {
            spriteController.Pause();
            game.CreaturePlacedOnField -= CreatureCardPlaced;
            game.CreatureDeletedFromField -= CreatureDeleted;
            Paused = true;
        }

        public void UnPause()
        {
            spriteController.UnPause();
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

            game.FirstPlayer.ChoosedCardsForGame.ForEach(card => card.TimeElapsed = 0);
            game.SecondPlayer.ChoosedCardsForGame.ForEach(card => card.TimeElapsed = 0);
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
            ShowGoldAmount(game.FirstPlayer.BattleGoldAmount);
        }

        public void ShowGoldAmount(int goldAmount)
        {
            goldText.Text = goldAmount.ToString();
        }

        private void LoadSprites()
        {
            var knightImage = Resources.KnightSprite;
            var darkKnightImage = Resources.DarkKnightSprite;
            var orcImage = Resources.OrcSprite;
            var trollImage = Resources.TrollSprite;
            var goldKnightImage = Resources.GoldKnightSprite;
            var goblinImage = Resources.GoblinSprite;
            var firstPlayerCastleImage = Resources.Castle_1;
            var secondPlayerCastleImage = Resources.Castle_2;
            var battleBeginsImage = Resources.BattleBegins;

            var knight = new Sprite(new Point(0, 0), spriteController, knightImage, 228, 181 , 200, 3);
            knight.AddAnimation(new Point(0, 181), knightImage, 210, 210, 200, 3);
            knight.SetName("Knight");

            var darkKnight = new Sprite(new Point(0, 0), spriteController, darkKnightImage, 251, 181, 200, 3);
            darkKnight.AddAnimation(new Point(0, 181), darkKnightImage, 275, 181, 200, 3);
            darkKnight.SetName("DarkKnight");

            var orc = new Sprite(new Point(0, 0), spriteController, orcImage, 309, 188, 200, 3);
            orc.AddAnimation(new Point(0, 188), orcImage, 293, 303, 200, 3);
            orc.SetName("Orc");

            var troll = new Sprite(new Point(0, 0), spriteController, trollImage, 317, 193, 200, 3);
            troll.AddAnimation(new Point(0, 193), trollImage, 297, 302, 200, 3);
            troll.SetName("Troll");

            var goblin = new Sprite(new Point(0, 0), spriteController, goblinImage, 315, 193, 200, 3);
            goblin.AddAnimation(new Point(0, 193), goblinImage, 307, 293, 200, 3);
            goblin.SetName("Goblin");

            var goldKnight = new Sprite(new Point(0, 0), spriteController, goldKnightImage, 219, 176, 200, 3);
            goldKnight.AddAnimation(new Point(0, 176), goldKnightImage, 209, 176, 200, 3);
            goldKnight.SetName("GoldKnight");

            var firstPlayerCastle = new Sprite(spriteController, firstPlayerCastleImage, firstPlayerCastleImage.Width, firstPlayerCastleImage.Height);
            firstPlayerCastle.SetName("FirstPlayerCastle");

            var secondPlayerCastle = new Sprite(spriteController, secondPlayerCastleImage, secondPlayerCastleImage.Width, secondPlayerCastleImage.Height);
            secondPlayerCastle.SetName("SecondPlayerCastle");
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
            TimeElapsedSinceLastPayment += tickIntervalInMs;

            if (TimeElapsedSinceLastPayment >= game.GameSettings.TimeReceivingCoinsInMs)
            {
                mainForm.Game.FirstPlayer.BattleGoldAmount += 1;
                mainForm.Game.SecondPlayer.BattleGoldAmount += 1;
                TimeElapsedSinceLastPayment = 0;
            }

            mainForm.Game.Act();
            mainForm.AI.Act(TimeElapsedSinceStart);

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
            var distanceToCastle = -10;

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
