using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class FieldControl : UserControl
    {
        private MainForm mainForm;
        public FieldControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }
    }
}
