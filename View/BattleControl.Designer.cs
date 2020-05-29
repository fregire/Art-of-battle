using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    partial class BattleControl
    {
        private void InitializeComponent()
        {

            this.SuspendLayout();

            Init();

            this.ResumeLayout(false);
        }

        private void Init()
        {
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.cardsZone = new UserCardsControl(mainForm, this);
            this.fieldArea = new PictureBox();
            this.goldControl = new Label() { Text = mainForm.Game.FirstPlayer.CurrentGold.ToString() };
            InitGoldControl();
            coinsCountLabel = new Label();

            this.Margin = Padding.Empty;
            this.Padding = Padding.Empty;
            this.Name = "BattleControl";
            this.Size = new System.Drawing.Size(797, 387);
            mainForm.Resize += SetComponentsSizes;
            SetComponentsSizes(null, null);
            fieldArea.Controls.Add(goldControl);
            Controls.Add(fieldArea);
            Controls.Add(cardsZone);
        }

        private void SetComponentsSizes(Object sender, EventArgs args)
        {
            var cardsZoneHeight = 150;
            fieldArea.Size = new Size(mainForm.Width, mainForm.Height - cardsZoneHeight);
            cardsZone.Location = new Point(0, fieldArea.Bottom);
            cardsZone.Size = new Size(mainForm.Width, cardsZoneHeight);
        }

        private void InitGoldControl()
        {
            goldControl.Location = new Point(0, 0);
            goldControl.Size = new Size(50, 30);
        }

        private Button pauseBtn;
        private Label coinsCountLabel;
        private TableLayoutPanel table;
        private PictureBox fieldArea;
        private UserCardsControl cardsZone;
        private Label goldControl;
    }
}
