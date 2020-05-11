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
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.fieldControl = new FieldControl(mainForm);
            this.cardsZone = new UserCardsControl(mainForm);
            var pauseBtn = new Button
            {
                Text ="Pause"
            };
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.fieldControl, 0, 0);
            this.table.Controls.Add(this.cardsZone, 0, 1);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 2;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.table.Size = new System.Drawing.Size(797, 387);
            this.table.TabIndex = 0;
            // 
            // fieldControl
            // 
            this.fieldControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldControl.Location = new System.Drawing.Point(3, 3);
            this.fieldControl.Name = "fieldControl";
            this.fieldControl.Size = new System.Drawing.Size(791, 281);
            this.fieldControl.TabIndex = 0;
            // 
            // cardsZone
            // 
            this.cardsZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsZone.Location = new System.Drawing.Point(3, 290);
            this.cardsZone.Name = "cardsZone";
            this.cardsZone.Size = new System.Drawing.Size(791, 94);
            this.cardsZone.TabIndex = 1;
            // 
            // BattleControl
            // 
            this.Controls.Add(pauseBtn);
            this.Controls.Add(this.table);
            this.Name = "BattleControl";
            this.Size = new System.Drawing.Size(797, 387);
            pauseBtn.Location = new Point(this.Size.Width - pauseBtn.Size.Width, 0);
            this.ResumeLayout(false);

        }

        private TableLayoutPanel table;
        private FieldControl fieldControl;
        private UserCardsControl cardsZone;
    }
}
