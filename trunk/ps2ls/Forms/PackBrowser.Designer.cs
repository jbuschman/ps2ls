namespace ps2ls.Forms
{
    partial class PackBrowser
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.packsListBox = new System.Windows.Forms.ListBox();
            this.packContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.filesDataGridView = new System.Windows.Forms.DataGridView();
            this.FileIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LengthInKilobytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.filesMaxComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.packFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.searchFilesTimer = new System.Windows.Forms.Timer(this.components);
            this.packOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.packFileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.extractPacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPacksButton = new System.Windows.Forms.ToolStripButton();
            this.removeSelectedPacksButton = new System.Windows.Forms.ToolStripButton();
            this.extractSelectedPacksButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.clearSearchButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.extractSelectedFilesButton = new System.Windows.Forms.ToolStripButton();
            this.packCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.packContextMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.packFileContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.filesDataGridView);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer2.Size = new System.Drawing.Size(800, 600);
            this.splitContainer2.SplitterDistance = 132;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.packsListBox);
            this.splitContainer3.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer3.Size = new System.Drawing.Size(132, 600);
            this.splitContainer3.SplitterDistance = 418;
            this.splitContainer3.TabIndex = 4;
            // 
            // packsListBox
            // 
            this.packsListBox.ContextMenuStrip = this.packContextMenuStrip;
            this.packsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.packsListBox.FormattingEnabled = true;
            this.packsListBox.Location = new System.Drawing.Point(0, 25);
            this.packsListBox.Name = "packsListBox";
            this.packsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.packsListBox.Size = new System.Drawing.Size(132, 393);
            this.packsListBox.TabIndex = 0;
            this.packsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.packsListBox_DrawItem);
            this.packsListBox.SelectedIndexChanged += new System.EventHandler(this.packsListBox_SelectedIndexChanged);
            this.packsListBox.SelectedValueChanged += new System.EventHandler(this.packsListBox_SelectedValueChanged);
            // 
            // packContextMenuStrip
            // 
            this.packContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractPacksToolStripMenuItem,
            this.toolStripSeparator5,
            this.removePacksToolStripMenuItem});
            this.packContextMenuStrip.Name = "packContextMenuStrip";
            this.packContextMenuStrip.Size = new System.Drawing.Size(169, 54);
            this.packContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.packContextMenuStrip_Opening);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(165, 6);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPacksButton,
            this.removeSelectedPacksButton,
            this.toolStripSeparator4,
            this.extractSelectedPacksButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(132, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(132, 178);
            this.propertyGrid.TabIndex = 4;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // filesDataGridView
            // 
            this.filesDataGridView.AllowUserToAddRows = false;
            this.filesDataGridView.AllowUserToDeleteRows = false;
            this.filesDataGridView.AllowUserToOrderColumns = true;
            this.filesDataGridView.AllowUserToResizeRows = false;
            this.filesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.filesDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.filesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileIcon,
            this.FileName,
            this.FileType,
            this.LengthInKilobytes});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.filesDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.filesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesDataGridView.Location = new System.Drawing.Point(0, 25);
            this.filesDataGridView.Name = "filesDataGridView";
            this.filesDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.filesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.filesDataGridView.RowHeadersVisible = false;
            this.filesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.filesDataGridView.Size = new System.Drawing.Size(664, 553);
            this.filesDataGridView.TabIndex = 1;
            this.filesDataGridView.SelectionChanged += new System.EventHandler(this.filesDataGridView_SelectionChanged);
            // 
            // FileIcon
            // 
            this.FileIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FileIcon.FillWeight = 20F;
            this.FileIcon.HeaderText = "";
            this.FileIcon.Name = "FileIcon";
            this.FileIcon.ReadOnly = true;
            this.FileIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileIcon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FileIcon.Width = 20;
            // 
            // FileName
            // 
            this.FileName.FillWeight = 96.01353F;
            this.FileName.HeaderText = "File Name";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // FileType
            // 
            this.FileType.FillWeight = 96.01353F;
            this.FileType.HeaderText = "Type";
            this.FileType.Name = "FileType";
            this.FileType.ReadOnly = true;
            // 
            // LengthInKilobytes
            // 
            this.LengthInKilobytes.FillWeight = 96.01353F;
            this.LengthInKilobytes.HeaderText = "Length (kb)";
            this.LengthInKilobytes.Name = "LengthInKilobytes";
            this.LengthInKilobytes.ReadOnly = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.searchTextBox,
            this.clearSearchButton,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.filesMaxComboBox,
            this.toolStripSeparator3,
            this.extractSelectedFilesButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(664, 25);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(150, 25);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.filesMaxComboBox.SelectedIndexChanged += new System.EventHandler(this.filesMaxComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packCountLabel,
            this.fileCountLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(664, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // searchFilesTimer
            // 
            this.searchFilesTimer.Interval = 500;
            this.searchFilesTimer.Tick += new System.EventHandler(this.searchFilesTimer_Tick);
            // 
            // packOpenFileDialog
            // 
            this.packOpenFileDialog.Filter = "PACK files|*.pack|All files|*.*";
            this.packOpenFileDialog.Multiselect = true;
            // 
            // packFileContextMenuStrip
            // 
            this.packFileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeToolStripMenuItem});
            this.packFileContextMenuStrip.Name = "packFileContextMenuStrip";
            this.packFileContextMenuStrip.Size = new System.Drawing.Size(118, 54);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(114, 6);
            // 
            // extractPacksToolStripMenuItem
            // 
            this.extractPacksToolStripMenuItem.Image = global::ps2ls.Properties.Resources.box_arrow_drive;
            this.extractPacksToolStripMenuItem.Name = "extractPacksToolStripMenuItem";
            this.extractPacksToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.extractPacksToolStripMenuItem.Text = "Extract 0 Pack(s)...";
            this.extractPacksToolStripMenuItem.Click += new System.EventHandler(this.extractPacksToolStripMenuItem_Click);
            // 
            // removePacksToolStripMenuItem
            // 
            this.removePacksToolStripMenuItem.Image = global::ps2ls.Properties.Resources.box__minus;
            this.removePacksToolStripMenuItem.Name = "removePacksToolStripMenuItem";
            this.removePacksToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.removePacksToolStripMenuItem.Text = "Remove 0 Pack(s)";
            this.removePacksToolStripMenuItem.Click += new System.EventHandler(this.removePacksToolStripMenuItem_Click);
            // 
            // addPacksButton
            // 
            this.addPacksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addPacksButton.Image = global::ps2ls.Properties.Resources.box__plus;
            this.addPacksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addPacksButton.Name = "addPacksButton";
            this.addPacksButton.Size = new System.Drawing.Size(23, 22);
            this.addPacksButton.Text = "Add Packs...";
            this.addPacksButton.Click += new System.EventHandler(this.addPacksButton_Click);
            // 
            // removeSelectedPacksButton
            // 
            this.removeSelectedPacksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeSelectedPacksButton.Enabled = false;
            this.removeSelectedPacksButton.Image = global::ps2ls.Properties.Resources.box__minus;
            this.removeSelectedPacksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeSelectedPacksButton.Name = "removeSelectedPacksButton";
            this.removeSelectedPacksButton.Size = new System.Drawing.Size(23, 22);
            this.removeSelectedPacksButton.Text = "Remove Packs";
            this.removeSelectedPacksButton.Click += new System.EventHandler(this.removeSelectedPacksButton_Click);
            // 
            // extractSelectedPacksButton
            // 
            this.extractSelectedPacksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.extractSelectedPacksButton.Enabled = false;
            this.extractSelectedPacksButton.Image = global::ps2ls.Properties.Resources.box_arrow_drive;
            this.extractSelectedPacksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.extractSelectedPacksButton.Name = "extractSelectedPacksButton";
            this.extractSelectedPacksButton.Size = new System.Drawing.Size(23, 22);
            this.extractSelectedPacksButton.Text = "Extract Selected Packs...";
            this.extractSelectedPacksButton.Click += new System.EventHandler(this.extractSelectedPacksButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            this.toolStripLabel1.ToolTipText = "Search";
            // 
            // clearSearchButton
            // 
            this.clearSearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearSearchButton.Enabled = false;
            this.clearSearchButton.Image = global::ps2ls.Properties.Resources.ui_text_field_clear_button;
            this.clearSearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearSearchButton.Name = "clearSearchButton";
            this.clearSearchButton.Size = new System.Drawing.Size(23, 22);
            this.clearSearchButton.Text = "Clear Search Text";
            this.clearSearchButton.Click += new System.EventHandler(this.clearSearchButton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel2.Image = global::ps2ls.Properties.Resources.counter;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel2.Text = "File Count Max";
            this.toolStripLabel2.ToolTipText = "File Count Maximum";
            // 
            // extractSelectedFilesButton
            // 
            this.extractSelectedFilesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.extractSelectedFilesButton.Enabled = false;
            this.extractSelectedFilesButton.Image = global::ps2ls.Properties.Resources.document_arrow_drive;
            this.extractSelectedFilesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.extractSelectedFilesButton.Name = "extractSelectedFilesButton";
            this.extractSelectedFilesButton.Size = new System.Drawing.Size(23, 22);
            this.extractSelectedFilesButton.Text = "Extract Selected Files...";
            this.extractSelectedFilesButton.Click += new System.EventHandler(this.extractSelectedFilesButton_Click);
            // 
            // packCountLabel
            // 
            this.packCountLabel.Image = global::ps2ls.Properties.Resources.box_search_result;
            this.packCountLabel.Name = "packCountLabel";
            this.packCountLabel.Size = new System.Drawing.Size(40, 17);
            this.packCountLabel.Text = "0/0";
            // 
            // fileCountLabel
            // 
            this.fileCountLabel.Image = global::ps2ls.Properties.Resources.document_search_result;
            this.fileCountLabel.Name = "fileCountLabel";
            this.fileCountLabel.Size = new System.Drawing.Size(40, 17);
            this.fileCountLabel.Text = "0/0";
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Image = global::ps2ls.Properties.Resources.box_arrow_drive;
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.extractToolStripMenuItem.Text = "Extract";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::ps2ls.Properties.Resources.box__minus;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // PackBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "PackBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.PackBrowserUserControl_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.packContextMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.packFileContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox packsListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addPacksButton;
        private System.Windows.Forms.ToolStripButton removeSelectedPacksButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton extractSelectedPacksButton;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.DataGridView filesDataGridView;
        private System.Windows.Forms.DataGridViewImageColumn FileIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LengthInKilobytes;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox searchTextBox;
        private System.Windows.Forms.ToolStripButton clearSearchButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox filesMaxComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton extractSelectedFilesButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel packCountLabel;
        private System.Windows.Forms.ToolStripStatusLabel fileCountLabel;
        private System.Windows.Forms.FolderBrowserDialog packFolderBrowserDialog;
        private System.Windows.Forms.Timer searchFilesTimer;
        private System.Windows.Forms.OpenFileDialog packOpenFileDialog;
        private System.Windows.Forms.ContextMenuStrip packContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip packFileContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractPacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem removePacksToolStripMenuItem;
    }
}
