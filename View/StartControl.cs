using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public class StartControl : UserControl
    {
        public StartControl()
        {
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

            Controls.Add(startButton);
            Controls.Add(heroesButton);
            Controls.Add(settingsButton);
        }
    }
}
