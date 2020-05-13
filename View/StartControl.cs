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
        private MainForm mainForm;
        public StartControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            SetBackground();
            SetBtnsBackground();
            
            heroesBtn.Click += (Object sender, EventArgs args) => mainForm.ShowHeroesScreen();
            startBtn.Click += (Object sender, EventArgs args) => mainForm.Game.Start();
        }

        private void SetBackground()
        {
            BackColor = Color.Transparent;
        }

        private void SetBtnsBackground()
        {
            SetBtnBackground(startBtn);
            SetBtnBackground(heroesBtn);
            SetBtnBackground(settingsBtn);
        }

        private void SetBtnBackground(Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.BackgroundImage = Properties.Resources.MainMenu_btn;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
