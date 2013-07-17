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
        public enum Buttons
        {
            OK,
            OKCancel,
            YesNo
        }

        public enum Types
        {
            Default,
            Warning
        };

        private static Image[] typeImages =
        {
            Properties.Resources.logo_48,
            Properties.Resources.logo_48_warn
        };

        private Buttons buttons;

        private GenericDialog(string title, string message, Types type, Buttons buttons, string optionText, bool optionCheckedDefault)
        {
            InitializeComponent();

            Text = title;
            messageLabel.Text = message;
            imagePanel.BackgroundImage = typeImages[(int)type];

            bool hasOption = optionText != null && optionText != string.Empty;
            optionCheckBox.Visible = hasOption;

            if (hasOption)
            {
                optionCheckBox.Text = optionText;
                optionCheckBox.Checked = optionCheckedDefault;
            }

            this.buttons = buttons;

            switch (buttons)
            {
                case Buttons.OK:
                    {
                        button1.Visible = true;
                        button1.Text = "OK";
                        button2.Visible = false;
                    }
                    break;
                case Buttons.OKCancel:
                    {
                        button1.Visible = true;
                        button1.Text = "Cancel";
                        button2.Visible = true;
                        button2.Text = "OK";
                    }
                    break;
                case Buttons.YesNo:
                    {
                        button1.Visible = true;
                        button1.Text = "No";
                        button2.Visible = true;
                        button2.Text = "Yes";
                    }
                    break;
                default:
                    break;
            }
        }

        public static DialogResult ShowGenericDialog(string title, string message, Types type, Buttons buttons, string optionText, bool optionCheckedDefault, out bool optionChecked)
        {
            GenericDialog genericDialog = new GenericDialog(title, message, type, buttons, optionText, optionCheckedDefault);
            DialogResult dialogResult = genericDialog.ShowDialog();
            optionChecked = genericDialog.optionCheckBox.Checked;
            return dialogResult;
        }

        public static DialogResult ShowGenericDialog(string title, string message, Types type, Buttons buttons)
        {
            bool optionChecked;
            return ShowGenericDialog(title, message, type, buttons, null, false, out optionChecked);
        }

        public static DialogResult ShowGenericDialog(string title, string message, Types type)
        {
            bool optionChecked;
            return ShowGenericDialog(title, message, type, Buttons.OK, null, false, out optionChecked);
        }

        public static DialogResult ShowGenericDialog(string title, string message)
        {
            bool optionChecked;
            return ShowGenericDialog(title, message, Types.Default, Buttons.OK, null, false, out optionChecked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (buttons)
            {
                case Buttons.OK:
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    break;
                case Buttons.OKCancel:
                    {
                        DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    break;
                case Buttons.YesNo:
                    {
                        DialogResult = System.Windows.Forms.DialogResult.No;
                    }
                    break;
            }

            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (buttons)
            {
                case Buttons.OKCancel:
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    break;
                case Buttons.YesNo:
                    {
                        DialogResult = System.Windows.Forms.DialogResult.Yes;
                    }
                    break;
            }

            Hide();
        }
    }
}
