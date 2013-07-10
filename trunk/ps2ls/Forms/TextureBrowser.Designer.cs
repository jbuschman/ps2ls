namespace ps2ls.Forms
{
    partial class TextureBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureBrowser));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.searchTextTypeToolStripDrownDownButton1 = new ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton();
            this.searchText = new ps2ls.Forms.Controls.SearchToolStripTextBox();
            this.clearSearchTextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.texturesMaxComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.pictureWindow = new System.Windows.Forms.PictureBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.texturesCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textureListbox = new ps2ls.Forms.Controls.CustomListBox();
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
            this.toolStripLabel1,
            this.searchTextTypeToolStripDrownDownButton1,
            this.searchText,
            this.clearSearchTextButton,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.texturesMaxComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripLabel1.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // searchTextTypeToolStripDrownDownButton1
            // 
            this.searchTextTypeToolStripDrownDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchTextTypeToolStripDrownDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("searchTextTypeToolStripDrownDownButton1.Image")));
            this.searchTextTypeToolStripDrownDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchTextTypeToolStripDrownDownButton1.Name = "searchTextTypeToolStripDrownDownButton1";
            this.searchTextTypeToolStripDrownDownButton1.SearchTextType = ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton.SearchTextTypes.Textual;
            this.searchTextTypeToolStripDrownDownButton1.Size = new System.Drawing.Size(29, 22);
            this.searchTextTypeToolStripDrownDownButton1.Text = "Textual";
            this.searchTextTypeToolStripDrownDownButton1.SearchTextTypeChanged += new System.EventHandler(this.searchTextTypeToolStripDrownDownButton1_SearchTextTypeChanged);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel2.Image = global::ps2ls.Properties.Resources.counter;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripLabel2.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel2.Text = "File Count Max";
            this.toolStripLabel2.ToolTipText = "File Count Maximum";
            // 
            // texturesMaxComboBox
            // 
            this.texturesMaxComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.texturesMaxComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.texturesMaxComboBox.Items.AddRange(new object[] {
            "100",
            "1000",
            "10000",
            "∞"});
            this.texturesMaxComboBox.Name = "texturesMaxComboBox";
            this.texturesMaxComboBox.Size = new System.Drawing.Size(75, 25);
            this.texturesMaxComboBox.SelectedIndexChanged += new System.EventHandler(this.texturesMaxComboBox_SelectedIndexChanged);
            // 
            // pictureWindow
            // 
            this.pictureWindow.BackColor = System.Drawing.Color.Black;
            this.pictureWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureWindow.InitialImage = null;
            this.pictureWindow.Location = new System.Drawing.Point(0, 0);
            this.pictureWindow.Name = "pictureWindow";
            this.pictureWindow.Size = new System.Drawing.Size(496, 600);
            this.pictureWindow.TabIndex = 2;
            this.pictureWindow.TabStop = false;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.texturesCountLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 578);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(300, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 4;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // imagesCountLabel
            // 
            this.texturesCountLabel.Image = global::ps2ls.Properties.Resources.document_search_result;
            this.texturesCountLabel.Name = "imagesCountLabel";
            this.texturesCountLabel.Size = new System.Drawing.Size(40, 17);
            this.texturesCountLabel.Text = "0/0";
            // 
            // textureListbox
            // 
            this.textureListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textureListbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.textureListbox.FormattingEnabled = true;
            this.textureListbox.Image = global::ps2ls.Properties.Resources.image;
            this.textureListbox.Items.AddRange(new object[] {
            "default",
            "default",
            "default",
            "default",
            "default",
            "ImageListBox"});
            this.textureListbox.Location = new System.Drawing.Point(0, 25);
            this.textureListbox.Name = "textureListbox";
            this.textureListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.textureListbox.Size = new System.Drawing.Size(300, 553);
            this.textureListbox.TabIndex = 0;
            this.textureListbox.SelectedIndexChanged += new System.EventHandler(this.textureListbox_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textureListbox);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureWindow);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 5;
            // 
            // TextureBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TextureBrowser";
            this.Size = new System.Drawing.Size(800, 600);
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

        private Controls.CustomListBox textureListbox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.SearchToolStripTextBox searchText;
        private System.Windows.Forms.ToolStripButton clearSearchTextButton;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel texturesCountLabel;
        private System.Windows.Forms.PictureBox pictureWindow;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.SearchTextTypeToolStripDrownDownButton searchTextTypeToolStripDrownDownButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox texturesMaxComboBox;
    }
}