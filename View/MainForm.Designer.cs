using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    partial class MainForm
    {
        private StartControl startControl;
        private HeroesControl heroesControl;
        private BattleControl battleControl;
        private FinishControl finishControl;
        private LevelsControl levelsControl;
        

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
            Margin = Padding.Empty;
            Padding = Padding.Empty;
            InitializeFont();
            Name = "mainForm";
            Text = "Морской бой";
            SetBackgroundImage();

            battleControl = new BattleControl(this);
            Controls.Add(battleControl);
            battleControl.Dock = DockStyle.Fill;
        }

        private void SetBackgroundImage()
        {
            BackgroundImage = Properties.Resources.mainmenubg1;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void InitializeFont()
        {
            Font = new Font(FontFamily.GenericMonospace, 10);
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

            return btn;
        }
    }
}
