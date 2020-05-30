using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Properties;

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
            coinsCountLabel = new Label();

            this.Margin = Padding.Empty;
            this.Padding = Padding.Empty;
            this.Name = "BattleControl";
            this.Size = new System.Drawing.Size(797, 387);
            mainForm.Resize += SetComponentsSizes;
            SetComponentsSizes(null, null);
            fieldArea.Controls.Add(GetInitedGoldControl());
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

        private Panel GetInitedGoldControl()
        {
            var goldControl = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(100, 30),
                BackColor = Color.Transparent
            };

            goldText = new Label
            {
                Size = new Size(30, 30),
                Location = new Point(0, 0),
                Text = mainForm.Game.FirstPlayer.CurrentGold.ToString()
            };

            var goldIcon = new PictureBox
            {
                BackgroundImage = Resources.Sword,
                Size = new Size(30, 30),
                BackgroundImageLayout = ImageLayout.Stretch
            };

            goldControl.Controls.Add(goldText);
            goldControl.Controls.Add(goldIcon);

            goldIcon.Location = new Point(goldText.Right, 0);

            return goldControl;
        }

        private Button pauseBtn;
        private Label coinsCountLabel;
        private TableLayoutPanel table;
        private PictureBox fieldArea;
        private UserCardsControl cardsZone;
        private Label goldText;
    }
}
