using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    partial class MainForm
    {
        private StartControl startControl;
        private HeroesControl heroesControl;
        private BattleControl battleControl;
        private FinishControl finishControl;
        private LevelsControl levelsControl;
        private PausedControl pausedControl;
        private Label playerInfo;
        

        private void InitializeComponent()
        {
            SuspendLayout();

            Init();

            ResumeLayout(false);
        }

        private void Init()
        {
            DoubleBuffered = true;
            var width = 800;
            var height = 500;
            startControl = new StartControl(this);
            heroesControl = new HeroesControl(this);
            levelsControl = new LevelsControl(this);

            levelsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            levelsControl.Location = new System.Drawing.Point(0, 0);
            levelsControl.Name = "levelsControl";
            levelsControl.Size = new System.Drawing.Size(width, height);

            startControl.Dock = System.Windows.Forms.DockStyle.Fill;
            startControl.Location = new System.Drawing.Point(0, 0);
            startControl.Name = "startControl";
            startControl.Size = new System.Drawing.Size(width, height);

            heroesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            heroesControl.Location = new System.Drawing.Point(0, 0);
            heroesControl.Name = "heroesControl";
            heroesControl.Size = new System.Drawing.Size(width, height);

            // Main Window Settings
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(width, height);
            Controls.Add(startControl);
            Controls.Add(heroesControl);
            Controls.Add(levelsControl);
            Controls.Add(pausedControl);
            Margin = Padding.Empty;
            Padding = Padding.Empty;
            InitializeFont();
            SetBackgroundImage();

            battleControl = new BattleControl(this);
            battleControl.Dock = DockStyle.Fill;
            Controls.Add(battleControl);

            WindowState = FormWindowState.Maximized;
            InitGameInfo();

            InitPlayerInfoLabel();
        }

        private void InitGameInfo()
        {
            Name = "mainForm";
            Text = "Art of Battle";
            Icon = new Icon(Application.StartupPath + "\\GameLogo.ico");
        }

        private void SetBackgroundImage()
        {
            BackgroundImage = Properties.Resources.mainmenubg1;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        public FontFamily GetMainFontFamily()
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            var path = Application.StartupPath;
            fontCollection.AddFontFile(path + "\\Cambria-Bold.ttf");
            FontFamily family = fontCollection.Families[0];

            return family;
        }
        private void InitializeFont()
        {
            Font = new Font(FontFamily.GenericSansSerif, 30);
            ForeColor = Color.White;
        }

        public Button CreateMainButton(string text)
        {
            var btn = new Button();
            btn.Text = text;
            btn.TabStop = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.BackgroundImage = Properties.Resources.MainMenu_btn;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (sender, args) => btn.ForeColor = Color.LightGreen;
            btn.MouseLeave += (sender, args) => btn.ForeColor = Color.White;

            return btn;
        }

        private void ShowUpdatedPlayerInfo()
        {
            playerInfo.Text = GetFormattedPlayerInfo();
            playerInfo.BringToFront();
        }

        private void InitPlayerInfoLabel()
        {
            playerInfo = new Label
            {
                Text = GetFormattedPlayerInfo(),
                Location = new Point(0, 0),
                Size = new Size(300, 50),
                Font = new Font(Font.Name, 10),
                BackColor = Color.Transparent
            };

            Controls.Add(playerInfo);
            playerInfo.BringToFront();
        }

        private string GetFormattedPlayerInfo()
        {
            var levelInfo = Game.FirstPlayer.PlayerLevelInfo;
            var currLevel = levelInfo.CurrentLevel;
            var requiredExp = levelInfo.RequiredExperienceForEachLevel[currLevel + 1] -
                              levelInfo.RequiredExperienceForEachLevel[currLevel];

            return "Уровень игрока: " + currLevel + "\nДо следующего уровня: " + requiredExp + " опыта";
        }
    }
}
