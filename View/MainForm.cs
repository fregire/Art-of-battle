using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ShowStartScreen();
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

        public void HideScreens()
        {
            startControl.Hide();
            settingsControl.Hide();
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