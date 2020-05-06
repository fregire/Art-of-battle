using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public class MainForm : Form
    {
        public MainForm()
        {
            var button = new Button();
            button.Text = "Hello, World!";
            
            Controls.Add(button);
        }
    }
}
