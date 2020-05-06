using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Art_of_battle.View
{
    partial class MainForm
    {
        private StartControl startControl;
        private void InitializeComponent()
        {
            startControl = new StartControl();
            this.SuspendLayout();

            this.startControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startControl.Location = new System.Drawing.Point(0, 0);
            this.startControl.Name = "startControl";
            this.startControl.Size = new System.Drawing.Size(800, 600);
            this.startControl.TabIndex = 2;

            // Main Window Settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.startControl);
            this.MaximumSize = new System.Drawing.Size(816, 639);
            this.MinimumSize = new System.Drawing.Size(816, 639);
            this.Name = "MainForm";
            this.Text = "Морской бой";
            this.ResumeLayout(false);
        }
    }
}
