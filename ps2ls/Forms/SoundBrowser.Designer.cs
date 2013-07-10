namespace ps2ls.Forms
{
    partial class SoundBrowser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundBrowser));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.SearchBoxClear = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.soundsListed = new System.Windows.Forms.ToolStripStatusLabel();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlayPause = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.soundListBox = new ps2ls.Forms.Controls.CustomListBox();
            this.searchTextTypeToolStripDrownDownButton1 = new ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.soundListBox);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.StopButton);
            this.splitContainer1.Panel2.Controls.Add(this.PlayPause);
            this.splitContainer1.Panel2.Controls.Add(this.StatusLabel);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.searchTextTypeToolStripDrownDownButton1,
            this.searchBox,
            this.SearchBoxClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(250, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // searchBox
            // 
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(100, 25);
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // SearchBoxClear
            // 
            this.SearchBoxClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchBoxClear.Image = global::ps2ls.Properties.Resources.ui_text_field_clear_button;
            this.SearchBoxClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchBoxClear.Name = "SearchBoxClear";
            this.SearchBoxClear.Size = new System.Drawing.Size(23, 22);
            this.SearchBoxClear.Text = "toolStripButton2";
            this.SearchBoxClear.Click += new System.EventHandler(this.SearchBoxClear_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundsListed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(250, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // filesListed
            // 
            this.soundsListed.Image = global::ps2ls.Properties.Resources.music;
            this.soundsListed.Name = "filesListed";
            this.soundsListed.Size = new System.Drawing.Size(40, 17);
            this.soundsListed.Text = "0/0";
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(199, 172);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlayPause
            // 
            this.PlayPause.Location = new System.Drawing.Point(199, 143);
            this.PlayPause.Name = "PlayPause";
            this.PlayPause.Size = new System.Drawing.Size(75, 23);
            this.PlayPause.TabIndex = 1;
            this.PlayPause.Text = "Play/Pause";
            this.PlayPause.UseVisualStyleBackColor = true;
            this.PlayPause.Click += new System.EventHandler(this.PlayPause_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(172, 198);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(135, 13);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "No Song 0:00:00 / 0:00:00";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // soundListBox
            // 
            this.soundListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.soundListBox.Image = global::ps2ls.Properties.Resources.music;
            this.soundListBox.Items.AddRange(new object[] {
            "default",
            "Input a search term to continue"});
            this.soundListBox.Location = new System.Drawing.Point(0, 25);
            this.soundListBox.Name = "soundListBox";
            this.soundListBox.Size = new System.Drawing.Size(250, 553);
            this.soundListBox.TabIndex = 0;
            this.soundListBox.SelectedIndexChanged += new System.EventHandler(this.soundListBox_SelectedIndexChanged);
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
            // 
            // SoundBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SoundBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.SoundBrowser_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.CustomListBox soundListBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel soundsListed;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.ToolStripButton SearchBoxClear;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button PlayPause;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private Controls.SearchTextTypeToolStripDrownDownButton searchTextTypeToolStripDrownDownButton1;
    }
}