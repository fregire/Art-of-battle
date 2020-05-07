using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art_of_battle.View
{
    public class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            var placeHolder = new TextBox();
            placeHolder.Location = new Point(0,0);
            placeHolder.Text = "Testing area";
            var button = new Button() {Text = "Hello, World"};
            button.Location = new Point(0, placeHolder.Bottom);

            Controls.Add(button);
            Controls.Add(placeHolder);
        }
    }
}
