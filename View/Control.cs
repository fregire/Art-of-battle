using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Art_of_battle.Properties;

namespace Art_of_battle.View
{
    public partial class Control : UserControl
    {
        protected MainForm mainForm;
        protected Panel contentZone;
        protected TableLayoutPanel mainTable;
        protected int paddingValue = 15;
        public Control(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            Init();

            this.ResumeLayout(false);
        }

        private void Init()
        {
            var paddingValue = 15;
            mainTable = GetMainTable(new Size(500, 400));
            mainTable.BackColor = Color.Transparent;
            contentZone = new Panel();
            contentZone.Padding = new Padding(paddingValue, paddingValue, paddingValue, paddingValue);

            mainTable.Controls.Add(contentZone, 1, 1);
            contentZone.Dock = DockStyle.Fill;
            mainTable.Dock = DockStyle.Fill;
            Dock = DockStyle.Fill;

            Controls.Add(mainTable);
        }

        private TableLayoutPanel GetMainTable(Size contentSize)
        {
            var mainTable = new TableLayoutPanel();

            mainTable.ColumnCount = 3;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, contentSize.Width));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            mainTable.RowCount = 3;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, contentSize.Height));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            return mainTable;
        }
    }
}
