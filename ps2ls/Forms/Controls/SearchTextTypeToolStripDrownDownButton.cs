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

        private static Bitmap[] searchTextTypeImages =
        {
            Properties.Resources.ui_label,
            Properties.Resources.regular_expression
        };

        private static String[] searchTextTypeNames =
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

                this.Text = searchTextTypeNames[(int)searchTextType];
                this.Image = searchTextTypeImages[(int)searchTextType];

                if(SearchTextTypeChanged != null)
                    SearchTextTypeChanged.Invoke(null, EventArgs.Empty);
            }
        }

        public SearchTextTypeToolStripDrownDownButton()
        {
            this.Text = searchTextTypeNames[(int)searchTextType];
            this.Image = searchTextTypeImages[(int)searchTextType];

            this.DropDownItems.Add(searchTextTypeNames[0], searchTextTypeImages[0], TextualOnClickHandler);
            this.DropDownItems.Add(searchTextTypeNames[1], searchTextTypeImages[1], RegularExpressionOnClickHandler);
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
