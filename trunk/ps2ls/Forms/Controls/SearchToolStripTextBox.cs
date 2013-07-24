using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ps2ls.Forms.Controls
{
    public class SearchToolStripTextBox : ToolStripTextBox
    {
        private Timer timer = new Timer();
        public event EventHandler CustomTextChanged;

        private int TIMER_INTERVAL = 500;

        public SearchToolStripTextBox()
        {
            timer.Interval = TIMER_INTERVAL;
            timer.Tick += new EventHandler(timer_Tick);
            TextChanged += new EventHandler(CustomSearchTextBox_TextChanged);
        }

        private void timer_Tick(object sender, EventArgs args)
        {
            if (Text.Length > 0)
                BackColor = Color.Yellow;
            else
                BackColor = Color.White;

            if (CustomTextChanged != null)
                CustomTextChanged(sender, args);

            timer.Stop();
        }

        private void CustomSearchTextBox_TextChanged(object sender, EventArgs args)
        {
            timer.Stop();
            timer.Start();
        }
    }
}
