using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    partial class StartControl
    {
        private int bottomMarginBtns = 30;
        private int btnHeight = 50;
        private TableLayoutPanel mainTable;
        private Button startBtn;
        private Button settingsBtn;
        private Button heroesBtn;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            Init();

            this.ResumeLayout(false);
        }

        private void Init()
        {
            var logo = new PictureBox
            {
                Image = new Bitmap(Resources.GameLogo, 150, 150),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Dock = DockStyle.Fill
            };

            var btnsTable = InitBtnsTable();
            btnsTable.BackColor = Color.Transparent;
            startBtn = mainForm.CreateMainButton("Начать");
            heroesBtn = mainForm.CreateMainButton("Герои");
            mainTable = new TableLayoutPanel();

            mainTable.Dock = DockStyle.Fill;
            btnsTable.Dock = DockStyle.Fill;
            startBtn.Dock = DockStyle.Fill;
            heroesBtn.Dock = DockStyle.Fill;

            mainTable.ColumnCount = 3;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            mainTable.RowCount = 3;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 370));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            
            btnsTable.Controls.Add(logo, 0, 0);
            btnsTable.Controls.Add(startBtn, 0, 2);
            btnsTable.Controls.Add(heroesBtn, 0, 4);
            mainTable.Controls.Add(btnsTable, 1, 1);

            Controls.Add(mainTable);

            SetBackground();
            this.Dock = DockStyle.Fill;
            this.Controls.Add(mainTable);
            this.Name = "StartControl";
        }

        private TableLayoutPanel InitBtnsTable()
        {
            var btnsTable = new TableLayoutPanel();

            btnsTable.ColumnCount = 1;
            btnsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            btnsTable.RowCount = 5;
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, bottomMarginBtns));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, bottomMarginBtns));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25));

            return btnsTable;
        }

        private void SetBackground()
        {
            BackColor = Color.Transparent;
        }
    }
}
