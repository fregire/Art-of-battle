using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
        public bool debug;
        public bool debug2;

        public MainForm()
        {
            var cards = GetMainPlayerCards();
            var lvls = GetGameLevels();
            Game = new Game(cards, lvls);
            DoubleBuffered = true;
            InitalizeGame();
            InitializeComponent();
        }

        private void InitalizeGame()
        {
            var gameSettings = new GameSettings();
            gameSettings.CardsCountInPlayerHand = 1;
            Game.GameSettings = gameSettings;
            var mainPlayer = new Player("Daniil", Direction.Right, GetMainPlayerCards());
            var secPlayer = new Player("AI", Direction.Left, GetAICards());

            AI = new AI(Game);
            mainPlayer.CurrentGold = 100;
            secPlayer.CurrentGold = 100;
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
                new Level(LevelName.Mountains, 10, 34, 1)
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

            return new List<Card>
            {
                new Card(knight, 10, 1000)
            };
        }

        public List<Card> GetAICards()
        {
            var orc = new MeleeCreature(
                CreatureType.Orc,
                200, 
                10,
                10,
                new Size(150, 150));

            return new List<Card>() { new Card(orc, 10, 1000) };
        }

        public void ShowStartScreen()
        {
            HideScreens();
            startControl.Show();
        }

        public void ShowHeroesScreen()
        {
            HideScreens();
            heroesControl.Show();
        }

        public void ShowBattleScreen()
        {
            HideScreens();
            battleControl.Show();
            battleControl.Start();
        }

        public void ShowLevelsScreen()
        {
            HideScreens();
            levelsControl.Show();
        }

        private void ShowFinishScreen()
        {
            battleControl.Stop();
            HideScreens();

            finishControl = new FinishControl(this);
            Controls.Add(finishControl);
            finishControl.Show();
            finishControl.BringToFront();
        }
        public void HideScreens()
        {
            startControl.Hide();
            heroesControl.Hide();
            battleControl.Hide();
            levelsControl.Hide();
            battleControl.Hide();
            finishControl?.Hide();
        }

        public Image GetCreatureImage(CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.Knight:
                    return Properties.Resources.Knight;
                case CreatureType.Troll:
                    return Properties.Resources.Troll;
                case CreatureType.Goblin:
                    return Properties.Resources.Goblin;
                case CreatureType.Orc:
                    return Properties.Resources.Orc;
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