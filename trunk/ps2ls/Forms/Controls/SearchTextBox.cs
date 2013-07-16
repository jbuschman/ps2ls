using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ps2ls.Forms.Controls
{
    public class SearchTextBox : TextBox
    {
        private const int TIMER_INTERVAL = 500;

        private Timer timer = new Timer();
        private Color backColorWhenTextEmpty = Color.White;
        private Color backColorWhenTextNotEmpty = Color.Yellow;

        public Color BackColorWhenTextEmpty
        {
            get { return backColorWhenTextEmpty; }
            set { backColorWhenTextEmpty = value; }
        }
        public Color BackColorWhenTextNotEmpty
        {
            get { return backColorWhenTextNotEmpty; }
            set { backColorWhenTextNotEmpty = value; }
        }

        public event EventHandler CustomTextChanged;

        public SearchTextBox()
        {
            timer.Interval = TIMER_INTERVAL;
            timer.Tick += new EventHandler(timer_Tick);

            TextChanged += new EventHandler(CustomSearchTextBox_TextChanged);
        }

        private void timer_Tick(object sender, EventArgs args)
        {
            if (Text.Length > 0)
                BackColor = BackColorWhenTextNotEmpty;
            else
                BackColor = BackColorWhenTextEmpty;

            if(CustomTextChanged != null)
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
