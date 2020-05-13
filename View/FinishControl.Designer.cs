using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Art_of_battle.Model;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    partial class FinishControl
    {
        private void InitializeComponent()
        {
            var label = new Label();
            label.Text = GetWinnerName();
            label.Location = new Point((Width - label.Width) / 2, (Height - label.Height) / 2);

            SuspendLayout();
            Dock = DockStyle.Fill;
            BackColor = Color.Black;
            Controls.Add(label);
            ResumeLayout(false);
        }

        private string GetWinnerName()
        {
            return mainForm.Game.GetWinner().Name;
        }
    }
}
