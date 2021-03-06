using System;
using System.Collections.Generic;
using System.Drawing;
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
    public partial class MainForm : Form
    {
        public Game Game { get; }
        public AI AI;

        public MainForm()
        {
            var cards = GetMainPlayerCards();
            var lvls = GetGameLevels();
            Game = new Game(lvls);
            DoubleBuffered = true;
            InitalizeGame();
            InitializeComponent();

        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void InitalizeGame()
        {
            var gameSettings = new GameSettings();
            gameSettings.CardsCountInPlayerHand = 4;
            Game.GameSettings = gameSettings;
            var AIChoosedCards = GetAICards();
            var mainPlayer = new Player("Daniil", Direction.Right, GetMainPlayerCards(), gameSettings.PlayerLevelsInfo);
            var secPlayer = new Player("AI", Direction.Left, AIChoosedCards, gameSettings.PlayerLevelsInfo);
            secPlayer.ChoosedCardsForGame = AIChoosedCards;
            AI = new AI(Game);
            
            mainPlayer.BattleGoldAmount = 100;
            secPlayer.BattleGoldAmount = 100;
            Game.AddPlayer(mainPlayer);
            Game.AddPlayer(secPlayer);
            Game.StateChanged += Game_OnStageChanged;
        }

        public void Game_OnStageChanged(GameStage stage)
        {
            switch (stage)
            {
                case GameStage.Started:
                    ShowBattleScreen();
                    break;
                case GameStage.Finished:
                    ShowFinishScreen();
                    break;
                case GameStage.NotStarted:
                default:
                    ShowStartScreen();
                    break;
            }
        }

        public List<Level> GetGameLevels()
        {
            return new List<Level>
            {
                new Level(LevelName.Forest, 10, 20, 1),
                new Level(LevelName.Mountains, 20, 34, 2)
            };
        }

        public List<Card> GetMainPlayerCards()
        {
            var knight = new MeleeCreature(
                CreatureType.Knight, 
                200, 
                10, 
                10, 
                new Size(150, 150));

            var orc = new MeleeCreature(
                CreatureType.Orc,
                200,
                10,
                10,
                new Size(200, 200));

            var troll = new MeleeCreature(
                CreatureType.Troll,
                200,
                10,
                10,
                new Size(200, 200));

            var darkKnight = new MeleeCreature(
                CreatureType.DarkKnight,
                200,
                10,
                10,
                new Size(150, 150));

            var goblin = new MeleeCreature(
                CreatureType.Goblin,
                200,
                10,
                10,
                new Size(200, 200));

            var goldKnight = new MeleeCreature(
                CreatureType.GoldKnight,
                200,
                10,
                10,
                new Size(150, 150));

            return new List<Card>
            {
                new Card(knight, 10, 1000),
                new Card(orc, 10, 1000),
                new Card(troll, 10, 1000),
                new Card(darkKnight, 10, 1000),
                new Card(goldKnight, 10, 1000),
                new Card(goblin, 10, 1000),
            };
        }

        public List<Card> GetAICards()
        {
            var knight = new MeleeCreature(
                CreatureType.Knight,
                150, 
                15,
                10,
                new Size(150, 150));

            var goldKnight = new MeleeCreature(
                CreatureType.GoldKnight,
                250,
                15,
                10,
                new Size(150, 150));

            return new List<Card>()
            {
                new Card(knight, 15, 1500),
                new Card(goldKnight, 10, 1000)
            };
        }

        public void ShowStartScreen()
        {
            startControl.BringToFront();
            ShowUpdatedPlayerInfo();
        }

        public void ShowHeroesScreen()
        {
            heroesControl.BringToFront();
            ShowUpdatedPlayerInfo();
        }

        public void ShowBattleScreen()
        {
            battleControl.BringToFront();
            battleControl.Start();
        }

        public void ShowLevelsScreen()
        {
            levelsControl.RefreshLevels();
            levelsControl.BringToFront();
            ShowUpdatedPlayerInfo();
        }

        private void ShowFinishScreen()
        {
            battleControl.Stop();

            finishControl = new FinishControl(this);
            Controls.Add(finishControl);
            finishControl.BringToFront();
            ShowUpdatedPlayerInfo();
        }

        public void ShowPlayerInfo()
        {
            ShowUpdatedPlayerInfo();
            playerInfo.Show();
        }

        public void HidePlayerInfo()
        {
            playerInfo.Hide();
        }

        public Image GetCreatureImage(CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.Goblin:
                    return Resources.GoblinPreview;
                case CreatureType.GoldKnight:
                    return Resources.GoldKnightPreview;
                case CreatureType.DarkKnight:
                    return Resources.DarkKnightPreview;
                case CreatureType.Knight:
                    return Properties.Resources.KnightPreview;
                case CreatureType.Troll:
                    return Properties.Resources.TrollPreview;
                case CreatureType.Orc:
                    return Properties.Resources.OrcPreview;
                default:
                    return null;
            }
        }

        public Image GetLevelImage(Level lvl)
        {
            switch (lvl.LevelName)
            {
                case LevelName.Forest:
                    return Resources.game_background_3__2;
                default:
                    return Resources.game_background_2;
            }
        }
    }
}