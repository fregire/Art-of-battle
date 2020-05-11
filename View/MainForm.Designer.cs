using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Art_of_battle.View
{
    partial class MainForm
    {
        private StartControl startControl;
        private SettingsControl settingsControl;
        private HeroesControl heroesControl;
        private BattleControl battleControl;
        

        private void InitializeComponent()
        {
            var width = 800;
            var height = 500;
            startControl = new StartControl(this);
            settingsControl = new SettingsControl();
            heroesControl = new HeroesControl(this);
            battleControl = new BattleControl(this);
            SuspendLayout();

            startControl.Dock = System.Windows.Forms.DockStyle.Fill;
            startControl.Location = new System.Drawing.Point(0, 0);
            startControl.Name = "startControl";
            startControl.Size = new System.Drawing.Size(width, height);
            startControl.TabIndex = 2;

            settingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsControl.Location = new System.Drawing.Point(0, 0);
            settingsControl.Name = "settingsControl";
            settingsControl.Size = new System.Drawing.Size(width, height);
            settingsControl.TabIndex = 2;

            heroesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            heroesControl.Location = new System.Drawing.Point(0, 0);
            heroesControl.Name = "heroesControl";
            heroesControl.Size = new System.Drawing.Size(width, height);
            heroesControl.TabIndex = 2;

            battleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            battleControl.Location = new System.Drawing.Point(0, 0);
            battleControl.Name = "battleControl";
            battleControl.Size = new System.Drawing.Size(width, height);
            battleControl.TabIndex = 2;

            // Main Window Settings
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(width, height);
            Controls.Add(startControl);
            Controls.Add(settingsControl);
            Controls.Add(heroesControl);
            Controls.Add(battleControl);
            Name = "MainForm";
            Text = "Морской бой";
            ResumeLayout(false);
        }
    }
}
