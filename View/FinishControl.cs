using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class FinishControl: UserControl
    {
        private MainForm mainForm;
        public FinishControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            backBtn.Click += OnBackBtn_Click;
        }

        private void OnBackBtn_Click(Object sender, EventArgs args)
        {
            mainForm.ShowStartScreen();
        }
    }
}
