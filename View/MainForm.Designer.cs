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
        

        private void InitializeComponent()
        {
            var width = 800;
            var height = 500;
            startControl = new StartControl(this);
            heroesControl = new HeroesControl(this);
            SuspendLayout();

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
            InitializeFont();
            Name = "MainForm";
            Text = "Морской бой";
            SetBackgroundImage();
            ResumeLayout(false);
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
