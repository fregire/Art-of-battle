using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Model;

namespace Art_of_battle.View
{
    public partial class BattleControl: UserControl
    {
        private MainForm mainForm;
        private Game game;
        private Timer timer;
        private int tickCounter;
        public BattleControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.game = mainForm.Game;
            var isPaused = false;
            var fps = 100;

            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000 / fps;

            timer.Tick += OnTick;
            game.FirstPlayer.GoldChanged += OnGoldChanged;
            
            pauseBtn.Click += (Object sender, EventArgs args) =>
            {
                if (isPaused)
                {
                    timer.Start();
                    pauseBtn.Text = "Pause";
                    isPaused = false;
                }
                else
                {
                    timer.Stop();
                    pauseBtn.Text = "Continue";
                    isPaused = true;
                }
            };

            timer.Start();

            mainForm.AI.Act();
            mainForm.Game.StateChanged += OnGame_Finished;
        }

        private void OnGame_Finished(GameStage stage)
        {
            if (stage == GameStage.Finished)
                timer.Stop();
        }
        private void OnGoldChanged(int newAmount)
        {
            coinsCountLabel.Text = newAmount.ToString();
        }

        private void OnTick(Object sender, EventArgs e)
        {
            if (tickCounter >= 20)
            {
                game.FirstPlayer.CurrentGold++;
                game.SecondPlayer.CurrentGold++;

                tickCounter = 0;
            }
            mainForm.Game.Act();
            tickCounter++;
        }
    }
}
