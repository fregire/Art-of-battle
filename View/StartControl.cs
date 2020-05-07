using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public partial class StartControl : UserControl
    {
        public StartControl(MainForm mainForm)
        {
            InitializeComponent();

            settingsBtn.Click += (Object sender, EventArgs args) => mainForm.ShowSettingsScreen();
        }
    }
}
