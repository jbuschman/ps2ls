using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ps2ls.Forms
{
    public partial class GenericDialog : Form
    {
        public GenericDialog()
        {
            InitializeComponent();
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
            Hide();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            Hide();
        }

        public bool OptionVisible
        {
            set { optionCheckBox.Visible = value; }
        }

        public bool OptionChecked
        {
            get { return optionCheckBox.Checked; }
            set { optionCheckBox.Checked = value; }
        }

        public String OptionText
        {
            set { optionCheckBox.Text = value; }
        }

        public String Message
        {
            set { messageLabel.Text = value; }
        }

        public Image Image
        {
            set { imagePanel.BackgroundImage = value; }
        }
    }
}
