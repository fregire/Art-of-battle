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
        private Button backBtn;
        private void InitializeComponent()
        {
            var label = new Label();
            backBtn = new Button();
            backBtn.Text = "Return to main menu";
            backBtn.Width = 80;
            backBtn.BackColor = Color.AliceBlue;
            label.BackColor = Color.AliceBlue;
            label.Width = 100;
            label.Text = String.Format("{0} is winner", GetWinnerName());

            SuspendLayout();
            Dock = DockStyle.Fill;

            label.Location = new Point((Width - label.Width) / 2, (Height - label.Height) / 2);
            backBtn.Location = new Point(label.Left, label.Bottom + 10);
            BackColor = Color.Black;
            Controls.Add(label);
            Controls.Add(backBtn);
            ResumeLayout(false);
        }

        private string GetWinnerName()
        {
            return mainForm.Game.GetWinner().Name;
        }
    }
}
