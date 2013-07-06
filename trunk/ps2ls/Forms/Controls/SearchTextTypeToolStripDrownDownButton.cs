using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps2ls.Forms.Controls
{
    public class SearchTextTypeToolStripDrownDownButton : ToolStripDropDownButton
    {
        public enum SearchTextType
        {
            Textual,
            RegularExpression
        }

        SearchTextTypeToolStripDrownDownButton()
        {
            this.DropDownItems.Add("Textual", Properties.Resources.ui_label);
            this.DropDownItems.Add("Regular Expression", Properties.Resources.regular_expression);
        }
    }
}
