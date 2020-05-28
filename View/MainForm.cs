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
            DoubleBuffered = true;
            var gameSettings = new GameSettings();
            gameSettings.CardsCountInPlayerHand = 1;
            DoubleBuffered = true;
            var cards = GetCards();
            Game = new Game(cards);
            Game.GameSettings = gameSettings;
            var mainPlayer = new Player("Daniil", Direction.Right, Game.DefaultPlayerCards);
            var secPlayer = new Player("AI", Direction.Left, Game.DefaultPlayerCards);

            AI = new AI(Game);
            mainPlayer.CurrentGold = 100;
            secPlayer.CurrentGold = 100;
            Game.AddPlayer(mainPlayer);
            Game.AddPlayer(secPlayer);
            Game.StateChanged += Game_OnStageChanged;
            InitializeComponent();
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

        public List<Card> GetCards()
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
                new Size(70, 50));

            var goblin = new MeleeCreature(
                CreatureType.Goblin,
                200,
                10,
                10,
                new Size(70, 50));

            var troll = new MeleeCreature(
                CreatureType.Troll,
                200,
                10,
                10,
                new Size(70, 50));

            return new List<Card>
            {
                new Card(knight, 10, 10), 
                new Card(orc, 10, 10),
                new Card(goblin, 10, 10),
                new Card(troll, 10, 23)
            };
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
    }
}