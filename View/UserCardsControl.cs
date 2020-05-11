using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class UserCardsControl : UserControl
    {
        private MainForm mainForm;
        public UserCardsControl(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

        }
    }
}
