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

        private static string[] searchTextTypeNames =
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
            Text = searchTextTypeNames[(int)searchTextType];
            Image = searchTextTypeImages[(int)searchTextType];

            DropDownItems.Add(searchTextTypeNames[0], searchTextTypeImages[0], TextualOnClick);
            DropDownItems.Add(searchTextTypeNames[1], searchTextTypeImages[1], RegularExpressionOnClick);
        }

        private void TextualOnClick(object sender, EventArgs args)
        {
            this.SearchTextType = SearchTextTypes.Textual;
        }

        private void RegularExpressionOnClick(object sender, EventArgs args)
        {
            this.SearchTextType = SearchTextTypes.RegularExpression;
        }
    }
}
