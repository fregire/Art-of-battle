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
        private Button pauseBtn;
        private Label coinsCountLabel;
        private TableLayoutPanel table;
        private PictureBox fieldArea;
        private UserCardsControl cardsZone;
        private Label goldText;
        private PausedControl pausedControl;
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
            this.pausedControl = new PausedControl(mainForm, this);
            this.pauseBtn = GetPauseBtn();
            coinsCountLabel = new Label();

            this.BackColor = Color.Transparent;
            this.Margin = Padding.Empty;
            this.Padding = Padding.Empty;
            this.Name = "BattleControl";
            this.Size = new System.Drawing.Size(797, 387);
            mainForm.Resize += SetComponentsSizes;
            pauseBtn.Click += OnPauseBtnClick;
            SetComponentsSizes(null, null);

            pauseBtn.Location = new Point(fieldArea.Right - pauseBtn.Width - 20, 0);
            fieldArea.Controls.Add(pauseBtn);
            fieldArea.Controls.Add(GetInitedGoldControl());
            fieldArea.BackgroundImageLayout = ImageLayout.Stretch;
            fieldArea.Parent = mainForm;
            cardsZone.BackColor = Color.Green;

            Controls.Add(fieldArea);
            Controls.Add(cardsZone);
            Controls.Add(pausedControl);
        }

        private void SetComponentsSizes(Object sender, EventArgs args)
        {
            var cardsZoneHeight = 150;
            fieldArea.Size = new Size(mainForm.Width, mainForm.Height - cardsZoneHeight);
            cardsZone.Location = new Point(0, fieldArea.Bottom);
            cardsZone.Size = new Size(mainForm.Width, cardsZoneHeight);
            pauseBtn.Location = new Point(fieldArea.Right - pauseBtn.Width - 20, 0);
        }

        private Button GetPauseBtn()
        {
            var button = new Button
            {
                BackgroundImage = Resources.Pause,
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = new Size(30, 30)
            };

            return button;
        }

        private void OnPauseBtnClick(Object sender, EventArgs args)
        {
            ShowPausedControl();
            Pause();
        }

        private Panel GetInitedGoldControl()
        {
            var goldControl = new Panel
            {
                Location = new Point(0, 5),
                Size = new Size(100, 30),
                BackColor = Color.Transparent
            };

            goldText = new Label
            {
                Size = new Size(45, 30),
                Location = new Point(2, 3),
                Text = mainForm.Game.FirstPlayer.BattleGoldAmount.ToString(),
                Font = new Font(mainForm.Font.Name, 14)
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
    }
}
