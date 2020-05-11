using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class BattleControl: UserControl
    {
        private MainForm mainForm;
        private Timer timer;
        public BattleControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.DrawLine(new Pen(Color.Black, 5), new Point(0, 0), new Point(50, 50) );
           
        }
    }
}
