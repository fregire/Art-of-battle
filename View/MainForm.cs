using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();

            ShowStartScreen();
        }

        private void ShowStartScreen()
        {
            HideScreens();
            startControl.Show();
        }

        private void HideScreens()
        {
            startControl.Hide();
        }
    }
}
