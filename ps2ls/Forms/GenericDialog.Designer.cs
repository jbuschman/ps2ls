namespace ps2ls.Forms
{
    partial class GenericDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericDialog));
            this.optionCheckBox = new System.Windows.Forms.CheckBox();
            this.noButton = new System.Windows.Forms.Button();
            this.yesButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.messageLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionCheckBox
            // 
            this.optionCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.optionCheckBox.AutoSize = true;
            this.optionCheckBox.Location = new System.Drawing.Point(12, 19);
            this.optionCheckBox.Name = "optionCheckBox";
            this.optionCheckBox.Size = new System.Drawing.Size(148, 17);
            this.optionCheckBox.TabIndex = 2;
            this.optionCheckBox.Text = "Always perform this action";
            this.optionCheckBox.UseVisualStyleBackColor = true;
            // 
            // noButton
            // 
            this.noButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.noButton.Location = new System.Drawing.Point(397, 15);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 1;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // yesButton
            // 
            this.yesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yesButton.Location = new System.Drawing.Point(316, 15);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 0;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.optionCheckBox);
            this.panel1.Controls.Add(this.noButton);
            this.panel1.Controls.Add(this.yesButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 50);
            this.panel1.TabIndex = 4;
            // 
            // imagePanel
            // 
            this.imagePanel.BackgroundImage = global::ps2ls.Properties.Resources.logo_48;
            this.imagePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imagePanel.Location = new System.Drawing.Point(12, 12);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(64, 64);
            this.imagePanel.TabIndex = 1;
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.Location = new System.Drawing.Point(82, 12);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(390, 64);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imagePanel);
            this.panel2.Controls.Add(this.messageLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(484, 88);
            this.panel2.TabIndex = 5;
            // 
            // GenericDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 138);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenericDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox optionCheckBox;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.Panel panel2;
    }
}