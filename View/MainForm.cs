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
    public partial class MainForm : Form
    {
        public Game Game { get; }
        public MainForm()
        {
            Game = new Game(GetCards());
            Game.StateChanged += Game_OnStageChanged;
            var mainPlayer = new Player("Daniil", Direction.Right, Game.DefaultPlayerCards);
            var secPlayer = new Player("Roman", Direction.Left, Game.DefaultPlayerCards);

            Game.FirstPlayer = mainPlayer;
            Game.SecondPlayer = secPlayer;

            InitializeComponent();
        }
        public void Game_OnStageChanged(GameStage stage)
        {
            switch (stage)
            {
                case GameStage.Started:
                    ShowBattleScreen();
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
                new Size(20, 20));
            var orc = new MeleeCreature(
                CreatureType.Orc, 
                200, 
                10, 
                10, 
                new Size(20, 20));

            var test = new MeleeCreature(
                CreatureType.Castle,
                200,
                10,
                10,
                new Size(20, 20));

            return new List<Card>
            {
                new Card(knight, 10, 10), 
                new Card(orc, 10, 10),
                new Card(test, 10, 10)
            };
        }

        public void ShowStartScreen()
        {
            HideScreens();
            
            startControl.Show();
        }

        public void ShowSettingsScreen()
        {
            HideScreens();
            settingsControl.Show();
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
        }
        public void HideScreens()
        {
            startControl.Hide();
            settingsControl.Hide();
            heroesControl.Hide();
            battleControl.Hide();
        }
    }

    public class OptionsForm : Form
    {
        public OptionsForm()
        {
            var backToMainMenuFromOptions = new Button
            {
                Text = "Назад"
            };
            Controls.Add(backToMainMenuFromOptions);

            var soundTextBox = new TextBox
            {
                Location = new Point(),//set x set y
                Size = new Size()//set x set y
            };
            var soundTrackBar = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                TickFrequency = 1
            };
            Controls.AddRange(new Control[] { soundTextBox, soundTrackBar });
            soundTrackBar.Scroll += new EventHandler(SoundTrackBarScroll);

            void SoundTrackBarScroll(object sender, EventArgs e)
            {
                soundTextBox.Text = "" + soundTrackBar.Value;
            }
        }

    }
}