using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps2ls.Forms.Controls
{
    public class SearchTextTypeToolStripDrownDownButton : ToolStripDropDownButton
    {
        public enum SearchTextTypes
        {
            Textual,
            RegularExpression
        }

        private static Bitmap[] SearchTextTypeImages =
        {
            Properties.Resources.ui_label,
            Properties.Resources.regular_expression
        };

        private static String[] SearchTextTypeNames =
        {
            "Textual",
            "Regular Expression"
        };

        public event EventHandler SearchTextTypeChanged;
        private SearchTextTypes searchTextType = SearchTextTypes.Textual;
        public SearchTextTypes SearchTextType
        {
            get { return searchTextType; }
            set
            {
                if(value == searchTextType)
                    return;

                searchTextType = value;

                this.Text = SearchTextTypeNames[(int)searchTextType];
                this.Image = SearchTextTypeImages[(int)searchTextType];

                if(SearchTextTypeChanged != null)
                    SearchTextTypeChanged.Invoke(null, EventArgs.Empty);
            }
        }

        public SearchTextTypeToolStripDrownDownButton()
        {
            this.Text = SearchTextTypeNames[(int)searchTextType];
            this.Image = SearchTextTypeImages[(int)searchTextType];

            this.DropDownItems.Add(SearchTextTypeNames[0], SearchTextTypeImages[0], TextualOnClickHandler);
            this.DropDownItems.Add(SearchTextTypeNames[1], SearchTextTypeImages[1], RegularExpressionOnClickHandler);
        }

        private void TextualOnClickHandler(object sender, EventArgs args)
        {
            this.SearchTextType = SearchTextTypes.Textual;
        }

        private void RegularExpressionOnClickHandler(object sender, EventArgs args)
        {
            this.SearchTextType = SearchTextTypes.RegularExpression;
        }
    }
}
