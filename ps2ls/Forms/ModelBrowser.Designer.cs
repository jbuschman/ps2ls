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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.searchTextTypeToolStripDrownDownButton = new ps2ls.Forms.Controls.SearchTextTypeToolStripDrownDownButton();
            this.searchModelsText = new ps2ls.Forms.Controls.SearchToolStripTextBox();
            this.clearSearchModelsText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showAutoLODModelsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lodFilterComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ModelBrowserModelStats1 = new ps2ls.Forms.ModelBrowserModelStats();
            this.modelBrowserGLControl = new ps2ls.Forms.ModelBrowserGLControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showAxesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.materialSelectionComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.splitContainer1.Panel2.Controls.Add(this.ModelBrowserModelStats1);
            this.splitContainer1.Panel2.Controls.Add(this.modelBrowserGLControl);
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
            this.toolStripButton1,
            this.searchTextTypeToolStripDrownDownButton,
            this.searchModelsText,
            this.clearSearchModelsText,
            this.toolStripSeparator2,
            this.showAutoLODModelsButton,
            this.toolStripLabel1,
            this.lodFilterComboBox});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(319, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::ps2ls.Properties.Resources.eye_half;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripLabel1.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            this.toolStripLabel1.ToolTipText = "LOD";
            // 
            // lodFilterComboBox
            // 
            this.lodFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lodFilterComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.lodFilterComboBox.Items.AddRange(new object[] {
            "All",
            "LOD 0",
            "LOD 1",
            "LOD 2"});
            this.lodFilterComboBox.Name = "lodFilterComboBox";
            this.lodFilterComboBox.Size = new System.Drawing.Size(75, 25);
            this.lodFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.lodFilterComboBox_SelectedIndexChanged);
            // 
            // ModelBrowserModelStats1
            // 
            this.ModelBrowserModelStats1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ModelBrowserModelStats1.Location = new System.Drawing.Point(0, 439);
            this.ModelBrowserModelStats1.Model = null;
            this.ModelBrowserModelStats1.Name = "ModelBrowserModelStats1";
            this.ModelBrowserModelStats1.Size = new System.Drawing.Size(477, 161);
            this.ModelBrowserModelStats1.TabIndex = 3;
            // 
            // modelBrowserGLControl
            // 
            this.modelBrowserGLControl.BackColor = System.Drawing.Color.Black;
            arcBallCamera1.AspectRatio = 0F;
            arcBallCamera1.DesiredDistance = 10F;
            arcBallCamera1.DesiredPitch = 0.7853982F;
            arcBallCamera1.DesiredTarget = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.DesiredTarget")));
            arcBallCamera1.DesiredYaw = -0.7853982F;
            arcBallCamera1.FarPlaneDistance = 65536F;
            arcBallCamera1.FieldOfView = 1.291544F;
            arcBallCamera1.NearPlaneDistance = 0.00390625F;
            arcBallCamera1.Pitch = 0.7853982F;
            arcBallCamera1.Position = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.Position")));
            arcBallCamera1.Yaw = -0.7853982F;
            this.modelBrowserGLControl.Camera = arcBallCamera1;
            this.modelBrowserGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelBrowserGLControl.DrawAxes = false;
            this.modelBrowserGLControl.Location = new System.Drawing.Point(0, 25);
            this.modelBrowserGLControl.Model = null;
            this.modelBrowserGLControl.Name = "modelBrowserGLControl";
            this.modelBrowserGLControl.RenderMode = ps2ls.Forms.ModelBrowserGLControl.RenderModes.Smooth;
            this.modelBrowserGLControl.Size = new System.Drawing.Size(477, 575);
            this.modelBrowserGLControl.TabIndex = 1;
            this.modelBrowserGLControl.VSync = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAxesButton,
            this.toolStripSeparator3,
            this.materialSelectionComboBox,
            this.toolStripSeparator5,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(477, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // showAxesButton
            // 
            this.showAxesButton.Checked = true;
            this.showAxesButton.CheckOnClick = true;
            this.showAxesButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAxesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAxesButton.Image = global::ps2ls.Properties.Resources.axes;
            this.showAxesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAxesButton.Name = "showAxesButton";
            this.showAxesButton.Size = new System.Drawing.Size(23, 22);
            this.showAxesButton.Text = "Show Axes";
            this.showAxesButton.CheckedChanged += new System.EventHandler(this.showAxesButton_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // materialSelectionComboBox
            // 
            this.materialSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialSelectionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.materialSelectionComboBox.Name = "materialSelectionComboBox";
            this.materialSelectionComboBox.Size = new System.Drawing.Size(200, 25);
            this.materialSelectionComboBox.Sorted = true;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wireframeToolStripMenuItem,
            this.smoothToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
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
        private System.Windows.Forms.ToolStripButton showAxesButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private ModelBrowserModelStats ModelBrowserModelStats1;
        private System.Windows.Forms.ToolStripButton showAutoLODModelsButton;
        private System.Windows.Forms.ToolStripComboBox materialSelectionComboBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripComboBox lodFilterComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ContextMenuStrip modelContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private Controls.SearchTextTypeToolStripDrownDownButton searchTextTypeToolStripDrownDownButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothToolStripMenuItem;
    }
}
