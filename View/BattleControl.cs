﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class BattleControl: UserControl
    {
        private MainForm mainForm;
        private Game game;
        private Timer timer;
        public BattleControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.game = mainForm.Game;
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 100;

            timer.Tick += OnTick;

            timer.Start();
        }

        private void OnTick(Object sender, EventArgs e)
        {
            mainForm.Game.Act();
        }
    }
}