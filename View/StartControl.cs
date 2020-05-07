using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class StartControl : UserControl
    {
        public StartControl(MainForm mainForm)
        {
            InitializeComponent();

            var startButton = new Button
            {
                Text = "Start"
            };

            var heroesButton = new Button
            {
                Text = "Heroes"
            };

            heroesButton.Location = new Point(0, startButton.Bottom);

            var settingsButton = new Button()
            {
                Text = "Settings"
            };

            settingsButton.Location = new Point(0, heroesButton.Bottom);
            settingsButton.Click += (Object sender, EventArgs args) => mainForm.ShowSettingsScreen();

        }
    }
}
