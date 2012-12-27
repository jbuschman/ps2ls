using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ps2ls.Controls
{
    public class CustomListBox : ListBox
    {
        public System.Drawing.Image Image { get; set; }

        public CustomListBox()
        {
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CustomListBox_DrawItem);

            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        }

        private void CustomListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            String text = ((ListBox)sender).Items[e.Index].ToString();
            System.Drawing.Point point = new System.Drawing.Point(0, e.Bounds.Y);

            if (Image != null)
            {
                e.Graphics.DrawImage(Image, point);
                point.X += Image.Width;
            }

            e.Graphics.DrawString(text, e.Font, new System.Drawing.SolidBrush(System.Drawing.Color.Black), point);
            e.DrawFocusRectangle();
        } 
    }
}
