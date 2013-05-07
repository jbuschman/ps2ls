﻿namespace ps2ls.Forms
{
    partial class ImageBrowser
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.searchText = new ps2ls.Forms.Controls.SearchToolStripTextBox();
            this.clearSearchTextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.pictureWindow = new System.Windows.Forms.PictureBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.imagesCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageListbox = new ps2ls.Forms.Controls.CustomListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindow)).BeginInit();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.searchText,
            this.clearSearchTextButton,
            this.toolStripSeparator1,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(213, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // searchText
            // 
            this.searchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(100, 25);
            this.searchText.CustomTextChanged += new System.EventHandler(this.searchText_CustomTextChanged);
            // 
            // clearSearchTextButton
            // 
            this.clearSearchTextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearSearchTextButton.Enabled = false;
            this.clearSearchTextButton.Image = global::ps2ls.Properties.Resources.ui_text_field_clear_button;
            this.clearSearchTextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearSearchTextButton.Name = "clearSearchTextButton";
            this.clearSearchTextButton.Size = new System.Drawing.Size(23, 22);
            this.clearSearchTextButton.Text = "clearSearchText";
            this.clearSearchTextButton.Click += new System.EventHandler(this.clearSearchTextButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::ps2ls.Properties.Resources.drive_download;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // pictureWindow
            // 
            this.pictureWindow.BackColor = System.Drawing.Color.Black;
            this.pictureWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureWindow.InitialImage = null;
            this.pictureWindow.Location = new System.Drawing.Point(0, 0);
            this.pictureWindow.Name = "pictureWindow";
            this.pictureWindow.Size = new System.Drawing.Size(583, 600);
            this.pictureWindow.TabIndex = 2;
            this.pictureWindow.TabStop = false;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imagesCountLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 578);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(213, 22);
            this.statusStrip2.TabIndex = 4;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // imagesCountLabel
            // 
            this.imagesCountLabel.Image = global::ps2ls.Properties.Resources.image;
            this.imagesCountLabel.Name = "imagesCountLabel";
            this.imagesCountLabel.Size = new System.Drawing.Size(40, 17);
            this.imagesCountLabel.Text = "0/0";
            // 
            // imageListbox
            // 
            this.imageListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imageListbox.FormattingEnabled = true;
            this.imageListbox.Image = global::ps2ls.Properties.Resources.image;
            this.imageListbox.Items.AddRange(new object[] {
            "ImageListBox"});
            this.imageListbox.Location = new System.Drawing.Point(0, 25);
            this.imageListbox.Name = "imageListbox";
            this.imageListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.imageListbox.Size = new System.Drawing.Size(213, 553);
            this.imageListbox.TabIndex = 0;
            this.imageListbox.SelectedIndexChanged += new System.EventHandler(this.imageListbox_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.imageListbox);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureWindow);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 5;
            // 
            // ImageBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ImageBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ImageBrowser_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindow)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CustomListBox imageListbox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Controls.SearchToolStripTextBox searchText;
        private System.Windows.Forms.ToolStripButton clearSearchTextButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel imagesCountLabel;
        private System.Windows.Forms.PictureBox pictureWindow;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}