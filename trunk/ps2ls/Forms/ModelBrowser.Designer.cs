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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelBrowser));
            ps2ls.Cameras.ArcBallCamera arcBallCamera1 = new ps2ls.Cameras.ArcBallCamera();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.modelsListBox = new ps2ls.Forms.Controls.CustomListBox();
            this.modelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.modelsCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.searchTextTypeToolStripDrownDownButton = new ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton();
            this.searchModelsText = new ps2ls.Forms.Controls.SearchToolStripTextBox();
            this.clearSearchModelsText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showAutoLODModelsButton = new System.Windows.Forms.ToolStripButton();
            this.modelBrowserGLControl = new ps2ls.Forms.ModelBrowserGLControl();
            this.modelBrowserModelStats = new ps2ls.Forms.ModelBrowserModelStats();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.drawAxesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.snapCameraToModelButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.modelContextMenuStrip.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.modelsListBox);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.modelBrowserGLControl);
            this.splitContainer1.Panel2.Controls.Add(this.modelBrowserModelStats);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 319;
            this.splitContainer1.TabIndex = 1;
            // 
            // modelsListBox
            // 
            this.modelsListBox.ContextMenuStrip = this.modelContextMenuStrip;
            this.modelsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.modelsListBox.FormattingEnabled = true;
            this.modelsListBox.Image = global::ps2ls.Properties.Resources.tree_small;
            this.modelsListBox.Items.AddRange(new object[] {
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "default",
            "CustomListBox"});
            this.modelsListBox.Location = new System.Drawing.Point(0, 25);
            this.modelsListBox.Name = "modelsListBox";
            this.modelsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.modelsListBox.Size = new System.Drawing.Size(319, 553);
            this.modelsListBox.TabIndex = 3;
            this.modelsListBox.SelectedIndexChanged += new System.EventHandler(this.modelsListBox_SelectedIndexChanged);
            // 
            // modelContextMenuStrip
            // 
            this.modelContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem});
            this.modelContextMenuStrip.Name = "modelContextMenuStrip";
            this.modelContextMenuStrip.Size = new System.Drawing.Size(119, 26);
            this.modelContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.modelContextMenuStrip_Opening);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Image = global::ps2ls.Properties.Resources.drive_download;
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.extractToolStripMenuItem.Text = "Extract...";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelsCountToolStripStatusLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 578);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(319, 22);
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
            this.toolStripLabel2,
            this.searchTextTypeToolStripDrownDownButton,
            this.searchModelsText,
            this.clearSearchModelsText,
            this.toolStripSeparator2,
            this.showAutoLODModelsButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(319, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel2.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripLabel2.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel2.Text = "toolStripLabel2";
            // 
            // searchTextTypeToolStripDrownDownButton
            // 
            this.searchTextTypeToolStripDrownDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchTextTypeToolStripDrownDownButton.Image = ((System.Drawing.Image)(resources.GetObject("searchTextTypeToolStripDrownDownButton.Image")));
            this.searchTextTypeToolStripDrownDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchTextTypeToolStripDrownDownButton.Name = "searchTextTypeToolStripDrownDownButton";
            this.searchTextTypeToolStripDrownDownButton.SearchTextType = ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton.SearchTextTypes.Textual;
            this.searchTextTypeToolStripDrownDownButton.Size = new System.Drawing.Size(29, 22);
            this.searchTextTypeToolStripDrownDownButton.Text = "Textual";
            this.searchTextTypeToolStripDrownDownButton.SearchTextTypeChanged += new System.EventHandler(this.searchTextTypeToolStripDrownDownButton_SearchTextTypeChanged);
            // 
            // searchModelsText
            // 
            this.searchModelsText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchModelsText.Name = "searchModelsText";
            this.searchModelsText.Size = new System.Drawing.Size(100, 25);
            this.searchModelsText.CustomTextChanged += new System.EventHandler(this.searchModelsText_CustomTextChanged);
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
            // showAutoLODModelsButton
            // 
            this.showAutoLODModelsButton.CheckOnClick = true;
            this.showAutoLODModelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAutoLODModelsButton.Image = global::ps2ls.Properties.Resources.eye_gear;
            this.showAutoLODModelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAutoLODModelsButton.Name = "showAutoLODModelsButton";
            this.showAutoLODModelsButton.Size = new System.Drawing.Size(23, 22);
            this.showAutoLODModelsButton.Text = "Show automatically generated LOD models";
            this.showAutoLODModelsButton.CheckedChanged += new System.EventHandler(this.showAutoLODModelsButton_CheckedChanged);
            // 
            // modelBrowserGLControl
            // 
            this.modelBrowserGLControl.BackColor = System.Drawing.Color.Black;
            arcBallCamera1.AspectRatio = 1.152174F;
            arcBallCamera1.Distance = 10F;
            arcBallCamera1.FarPlaneDistance = 65536F;
            arcBallCamera1.FieldOfView = 1.291544F;
            arcBallCamera1.NearPlaneDistance = 0.00390625F;
            arcBallCamera1.Pitch = 0.7853982F;
            arcBallCamera1.Position = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.Position")));
            arcBallCamera1.Target = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.Target")));
            arcBallCamera1.Yaw = -0.7853982F;
            this.modelBrowserGLControl.Camera = arcBallCamera1;
            this.modelBrowserGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelBrowserGLControl.DrawAxes = true;
            this.modelBrowserGLControl.Location = new System.Drawing.Point(0, 25);
            this.modelBrowserGLControl.Model = null;
            this.modelBrowserGLControl.Name = "modelBrowserGLControl";
            this.modelBrowserGLControl.RenderMode = ps2ls.Forms.ModelBrowserGLControl.RenderModes.Smooth;
            this.modelBrowserGLControl.Size = new System.Drawing.Size(477, 414);
            this.modelBrowserGLControl.SnapCameraToModelOnModelChange = true;
            this.modelBrowserGLControl.TabIndex = 1;
            this.modelBrowserGLControl.VSync = false;
            // 
            // modelBrowserModelStats
            // 
            this.modelBrowserModelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.modelBrowserModelStats.Location = new System.Drawing.Point(0, 439);
            this.modelBrowserModelStats.Model = null;
            this.modelBrowserModelStats.Name = "modelBrowserModelStats";
            this.modelBrowserModelStats.Size = new System.Drawing.Size(477, 161);
            this.modelBrowserModelStats.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawAxesButton,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.snapCameraToModelButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(477, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // drawAxesButton
            // 
            this.drawAxesButton.CheckOnClick = true;
            this.drawAxesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawAxesButton.Image = global::ps2ls.Properties.Resources.axes;
            this.drawAxesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawAxesButton.Name = "drawAxesButton";
            this.drawAxesButton.Size = new System.Drawing.Size(23, 22);
            this.drawAxesButton.Text = "Show Axes";
            this.drawAxesButton.CheckedChanged += new System.EventHandler(this.showAxesButton_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wireframeToolStripMenuItem,
            this.smoothToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::ps2ls.Properties.Resources.smooth;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.Image = global::ps2ls.Properties.Resources.wireframe;
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.wireframeToolStripMenuItem.Text = "Wireframe";
            this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.wireframeToolStripMenuItem_Click);
            // 
            // smoothToolStripMenuItem
            // 
            this.smoothToolStripMenuItem.Image = global::ps2ls.Properties.Resources.smooth;
            this.smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
            this.smoothToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.smoothToolStripMenuItem.Text = "Smooth";
            this.smoothToolStripMenuItem.Click += new System.EventHandler(this.smoothToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // snapCameraToModelButton
            // 
            this.snapCameraToModelButton.Checked = true;
            this.snapCameraToModelButton.CheckOnClick = true;
            this.snapCameraToModelButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.snapCameraToModelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snapCameraToModelButton.Image = ((System.Drawing.Image)(resources.GetObject("snapCameraToModelButton.Image")));
            this.snapCameraToModelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snapCameraToModelButton.Name = "snapCameraToModelButton";
            this.snapCameraToModelButton.Size = new System.Drawing.Size(23, 22);
            this.snapCameraToModelButton.Text = "Snap Camera To Model";
            this.snapCameraToModelButton.CheckedChanged += new System.EventHandler(this.snapCameraToModelButton_CheckedChanged);
            // 
            // ModelBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ModelBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.modelContextMenuStrip.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private Controls.SearchToolStripTextBox searchModelsText;
        private System.Windows.Forms.ToolStripButton clearSearchModelsText;
        private ps2ls.Forms.Controls.CustomListBox modelsListBox;
        private System.Windows.Forms.ToolStripStatusLabel modelsCountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private ModelBrowserGLControl modelBrowserGLControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton drawAxesButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private ModelBrowserModelStats modelBrowserModelStats;
        private System.Windows.Forms.ToolStripButton showAutoLODModelsButton;
        private System.Windows.Forms.ContextMenuStrip modelContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private Controls.SearchTextTypeToolStripDrownDownButton searchTextTypeToolStripDrownDownButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton snapCameraToModelButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}
