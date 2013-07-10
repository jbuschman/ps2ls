namespace ps2ls.Forms
{
    partial class ZoneBrowser
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
            ps2ls.Cameras.ArcBallCamera arcBallCamera1 = new ps2ls.Cameras.ArcBallCamera();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoneBrowser));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.zonesListBox = new ps2ls.Forms.Controls.CustomListBox();
            this.customListBox1 = new ps2ls.Forms.Controls.CustomListBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.glControl = new ps2ls.Forms.ModelBrowserGLControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.searchTextTypeToolStripDrownDownButton1 = new ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton();
            this.searchText = new ps2ls.Forms.Controls.SearchToolStripTextBox();
            this.clearSearchTextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.filesMaxComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.zoneCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip3.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel1MinSize = 300;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.glControl);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer2.Size = new System.Drawing.Size(800, 600);
            this.splitContainer2.SplitterDistance = 300;
            this.splitContainer2.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip3);
            this.splitContainer1.Panel1.Controls.Add(this.zonesListBox);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.customListBox1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(300, 600);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 0;
            // 
            // statusStrip3
            // 
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoneCountLabel});
            this.statusStrip3.Location = new System.Drawing.Point(0, 78);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Size = new System.Drawing.Size(300, 22);
            this.statusStrip3.SizingGrip = false;
            this.statusStrip3.TabIndex = 2;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // zonesListBox
            // 
            this.zonesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zonesListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.zonesListBox.FormattingEnabled = true;
            this.zonesListBox.Image = global::ps2ls.Properties.Resources.map;
            this.zonesListBox.Items.AddRange(new object[] {
            "default",
            "default"});
            this.zonesListBox.Location = new System.Drawing.Point(0, 25);
            this.zonesListBox.Name = "zonesListBox";
            this.zonesListBox.Size = new System.Drawing.Size(300, 75);
            this.zonesListBox.TabIndex = 1;
            // 
            // customListBox1
            // 
            this.customListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.customListBox1.FormattingEnabled = true;
            this.customListBox1.Image = global::ps2ls.Properties.Resources.map_medium;
            this.customListBox1.Items.AddRange(new object[] {
            "default",
            "default"});
            this.customListBox1.Location = new System.Drawing.Point(0, 25);
            this.customListBox1.Name = "customListBox1";
            this.customListBox1.Size = new System.Drawing.Size(300, 449);
            this.customListBox1.TabIndex = 1;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip2.Location = new System.Drawing.Point(0, 474);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(300, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 2;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.searchTextTypeToolStripDrownDownButton1,
            this.searchText,
            this.clearSearchTextButton,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.filesMaxComboBox});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(300, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            arcBallCamera1.AspectRatio = 0.9446367F;
            arcBallCamera1.Distance = 10F;
            arcBallCamera1.FarPlaneDistance = 256F;
            arcBallCamera1.FieldOfView = 1.308997F;
            arcBallCamera1.NearPlaneDistance = 0.00390625F;
            arcBallCamera1.Pitch = 0.7853982F;
            arcBallCamera1.Position = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.Position")));
            arcBallCamera1.Target = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.Target")));
            arcBallCamera1.Yaw = -0.7853982F;
            this.glControl.Camera = arcBallCamera1;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.DrawAxes = false;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Model = null;
            this.glControl.Name = "glControl";
            this.glControl.RenderMode = ps2ls.Forms.ModelBrowserGLControl.RenderModes.Smooth;
            this.glControl.Size = new System.Drawing.Size(496, 578);
            this.glControl.SnapCameraToModelOnModelChange = false;
            this.glControl.TabIndex = 2;
            this.glControl.VSync = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(496, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = global::ps2ls.Properties.Resources.document_search_result;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel1.Text = "0/0";
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
            // 
            // searchText
            // 
            this.searchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(100, 25);
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
            // filesMaxComboBox
            // 
            this.filesMaxComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filesMaxComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.filesMaxComboBox.Items.AddRange(new object[] {
            "100",
            "1000",
            "10000",
            "∞"});
            this.filesMaxComboBox.Name = "filesMaxComboBox";
            this.filesMaxComboBox.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // zoneCountLabel
            // 
            this.zoneCountLabel.Image = global::ps2ls.Properties.Resources.document_search_result;
            this.zoneCountLabel.Name = "zoneCountLabel";
            this.zoneCountLabel.Size = new System.Drawing.Size(40, 17);
            this.zoneCountLabel.Text = "0/0";
            // 
            // ZoneBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "ZoneBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer1;
        private Controls.CustomListBox zonesListBox;
        private Controls.CustomListBox customListBox1;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private ModelBrowserGLControl glControl;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private Controls.SearchTextTypeToolStripDrownDownButton searchTextTypeToolStripDrownDownButton1;
        private Controls.SearchToolStripTextBox searchText;
        private System.Windows.Forms.ToolStripButton clearSearchTextButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox filesMaxComboBox;
        private System.Windows.Forms.ToolStripStatusLabel zoneCountLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
