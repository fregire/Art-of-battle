using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

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

            var btnsTable = InitBtnsTable();
            btnsTable.BackColor = Color.Transparent;
            startBtn = mainForm.CreateMainButton("Start");
            heroesBtn = mainForm.CreateMainButton("Heroes");
            mainTable = new TableLayoutPanel();

            mainTable.Dock = DockStyle.Fill;
            btnsTable.Dock = DockStyle.Fill;
            startBtn.Dock = DockStyle.Fill;
            heroesBtn.Dock = DockStyle.Fill;

            mainTable.ColumnCount = 3;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            mainTable.RowCount = 3;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            mainTable.ResumeLayout(false);

            btnsTable.Controls.Add(startBtn, 0, 0);
            btnsTable.Controls.Add(heroesBtn, 0, 2);
            mainTable.Controls.Add(btnsTable, 1, 1);

            Controls.Add(mainTable);

            SetBackground();
            this.Dock = DockStyle.Fill;
            this.Controls.Add(mainTable);
            this.Name = "StartControl";
            this.ResumeLayout(false);
        }

        private TableLayoutPanel InitBtnsTable()
        {
            var btnsTable = new TableLayoutPanel();

            btnsTable.ColumnCount = 1;
            btnsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            btnsTable.RowCount = 3;
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, bottomMarginBtns));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            return btnsTable;
        }

        private void SetBackground()
        {
            BackColor = Color.Transparent;
        }
    }
}
