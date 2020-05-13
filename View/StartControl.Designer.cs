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

        private TableLayoutPanel InitBtnsTable()
        {
            var btnsTable = new TableLayoutPanel();

            btnsTable.ColumnCount = 1;
            btnsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            btnsTable.RowCount = 5;
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, bottomMarginBtns));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, bottomMarginBtns));
            btnsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));

            return btnsTable;
        }
        private void InitializeComponent()
        {

            var btnsTable = InitBtnsTable();
            btnsTable.BackColor = Color.Transparent;

            startBtn = new Button
            {
                Text = "Start",
                Dock = DockStyle.Fill
            };

            settingsBtn = new Button
            {
                Text = "Settings",
                Dock = DockStyle.Fill
            };

            heroesBtn = new Button
            {
                Text = "Heroes",
                Dock = DockStyle.Fill
            };

            mainTable = new TableLayoutPanel();

            mainTable.Dock = DockStyle.Fill;
            btnsTable.Dock = DockStyle.Fill;

            mainTable.ColumnCount = 3;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            mainTable.RowCount = 3;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 300));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            mainTable.ResumeLayout(false);

            btnsTable.Controls.Add(startBtn, 0, 0);
            btnsTable.Controls.Add(heroesBtn, 0, 2);
            btnsTable.Controls.Add(settingsBtn, 0, 4);
            mainTable.Controls.Add(btnsTable, 1, 1);

            Controls.Add(mainTable);

            this.Dock = DockStyle.Fill;
            this.Controls.Add(mainTable);
            this.Name = "StartControl";
            this.ResumeLayout(false);
        }
    }
}
