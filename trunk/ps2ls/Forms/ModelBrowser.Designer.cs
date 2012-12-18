namespace ps2ls.Forms
{
    partial class ModelBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.modelsCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.searchModelsText = new System.Windows.Forms.ToolStripTextBox();
            this.clearSearchModelsText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportSelectedModelsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.searchModelsTimer = new System.Windows.Forms.Timer(this.components);
            this.modelsListBox = new ps2ls.CustomListBox();
            this.glControl1 = new ps2ls.CustomGLControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.modelsListBox);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.glControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelsCountToolStripStatusLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 578);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(250, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 2;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // modelsCountToolStripStatusLabel
            // 
            this.modelsCountToolStripStatusLabel.Image = global::ps2ls.Properties.Resources.document_search_result;
            this.modelsCountToolStripStatusLabel.Name = "modelsCountToolStripStatusLabel";
            this.modelsCountToolStripStatusLabel.Size = new System.Drawing.Size(40, 17);
            this.modelsCountToolStripStatusLabel.Text = "0/0";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.searchModelsText,
            this.clearSearchModelsText,
            this.toolStripSeparator2,
            this.exportSelectedModelsToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(250, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel3.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel3.Text = "toolStripLabel1";
            this.toolStripLabel3.ToolTipText = "Search";
            // 
            // searchModelsText
            // 
            this.searchModelsText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchModelsText.Name = "searchModelsText";
            this.searchModelsText.Size = new System.Drawing.Size(150, 25);
            this.searchModelsText.TextChanged += new System.EventHandler(this.searchModelsText_TextChanged);
            // 
            // clearSearchModelsText
            // 
            this.clearSearchModelsText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearSearchModelsText.Enabled = false;
            this.clearSearchModelsText.Image = global::ps2ls.Properties.Resources.ui_text_field_clear_button;
            this.clearSearchModelsText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearSearchModelsText.Name = "clearSearchModelsText";
            this.clearSearchModelsText.Size = new System.Drawing.Size(23, 22);
            this.clearSearchModelsText.Text = "Clear Search Text";
            this.clearSearchModelsText.Click += new System.EventHandler(this.clearSearchModelsText_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // exportSelectedModelsToolStripButton
            // 
            this.exportSelectedModelsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportSelectedModelsToolStripButton.Image = global::ps2ls.Properties.Resources.drive_download;
            this.exportSelectedModelsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportSelectedModelsToolStripButton.Name = "exportSelectedModelsToolStripButton";
            this.exportSelectedModelsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.exportSelectedModelsToolStripButton.Text = "Export Selected Models...";
            this.exportSelectedModelsToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // searchModelsTimer
            // 
            this.searchModelsTimer.Interval = 500;
            this.searchModelsTimer.Tick += new System.EventHandler(this.searchModelsTimer_Tick);
            // 
            // modelsListBox
            // 
            this.modelsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.modelsListBox.FormattingEnabled = true;
            this.modelsListBox.Image = global::ps2ls.Properties.Resources.tree_small;
            this.modelsListBox.Items.AddRange(new object[] {
            "CustomListBox"});
            this.modelsListBox.Location = new System.Drawing.Point(0, 25);
            this.modelsListBox.Name = "modelsListBox";
            this.modelsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.modelsListBox.Size = new System.Drawing.Size(250, 553);
            this.modelsListBox.TabIndex = 3;
            this.modelsListBox.SelectedIndexChanged += new System.EventHandler(this.modelsListBox_SelectedIndexChanged);
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(546, 600);
            this.glControl1.TabIndex = 1;
            this.glControl1.VSync = false;
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // ModelBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ModelBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ModelBrowserControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox searchModelsText;
        private System.Windows.Forms.ToolStripButton clearSearchModelsText;
        private System.Windows.Forms.Timer searchModelsTimer;
        private CustomListBox modelsListBox;
        private System.Windows.Forms.ToolStripStatusLabel modelsCountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportSelectedModelsToolStripButton;
        private CustomGLControl glControl1;
    }
}
