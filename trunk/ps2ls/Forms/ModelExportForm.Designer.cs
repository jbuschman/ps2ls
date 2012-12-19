namespace ps2ls.Forms
{
    partial class ModelExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelExportForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.leftAxisComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.upAxisComboBox = new System.Windows.Forms.ComboBox();
            this.flipZCheckBox = new System.Windows.Forms.CheckBox();
            this.flipYCheckBox = new System.Windows.Forms.CheckBox();
            this.flipXCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scaleLinkAxesCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.zScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.yScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textureCoordinatesCheckBox = new System.Windows.Forms.CheckBox();
            this.normalsCheckBox = new System.Windows.Forms.CheckBox();
            this.exportDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.exportFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openExportFolderBrowserDialogButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zScaleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yScaleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xScaleNumericUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.leftAxisComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.upAxisComboBox);
            this.groupBox1.Controls.Add(this.flipZCheckBox);
            this.groupBox1.Controls.Add(this.flipYCheckBox);
            this.groupBox1.Controls.Add(this.flipXCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Axes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Left Axis";
            // 
            // leftAxisComboBox
            // 
            this.leftAxisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftAxisComboBox.FormattingEnabled = true;
            this.leftAxisComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.leftAxisComboBox.Location = new System.Drawing.Point(60, 113);
            this.leftAxisComboBox.MaxDropDownItems = 3;
            this.leftAxisComboBox.MaxLength = 1;
            this.leftAxisComboBox.Name = "leftAxisComboBox";
            this.leftAxisComboBox.Size = new System.Drawing.Size(69, 21);
            this.leftAxisComboBox.TabIndex = 5;
            this.leftAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.leftAxisComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Up Axis";
            // 
            // upAxisComboBox
            // 
            this.upAxisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.upAxisComboBox.FormattingEnabled = true;
            this.upAxisComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.upAxisComboBox.Location = new System.Drawing.Point(60, 86);
            this.upAxisComboBox.MaxDropDownItems = 3;
            this.upAxisComboBox.MaxLength = 1;
            this.upAxisComboBox.Name = "upAxisComboBox";
            this.upAxisComboBox.Size = new System.Drawing.Size(69, 21);
            this.upAxisComboBox.TabIndex = 3;
            this.upAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.upAxisComboBox_SelectedIndexChanged);
            // 
            // flipZCheckBox
            // 
            this.flipZCheckBox.AutoSize = true;
            this.flipZCheckBox.Location = new System.Drawing.Point(6, 65);
            this.flipZCheckBox.Name = "flipZCheckBox";
            this.flipZCheckBox.Size = new System.Drawing.Size(52, 17);
            this.flipZCheckBox.TabIndex = 2;
            this.flipZCheckBox.Text = "Flip Z";
            this.flipZCheckBox.UseVisualStyleBackColor = true;
            // 
            // flipYCheckBox
            // 
            this.flipYCheckBox.AutoSize = true;
            this.flipYCheckBox.Location = new System.Drawing.Point(6, 42);
            this.flipYCheckBox.Name = "flipYCheckBox";
            this.flipYCheckBox.Size = new System.Drawing.Size(52, 17);
            this.flipYCheckBox.TabIndex = 1;
            this.flipYCheckBox.Text = "Flip Y";
            this.flipYCheckBox.UseVisualStyleBackColor = true;
            // 
            // flipXCheckBox
            // 
            this.flipXCheckBox.AutoSize = true;
            this.flipXCheckBox.Location = new System.Drawing.Point(6, 19);
            this.flipXCheckBox.Name = "flipXCheckBox";
            this.flipXCheckBox.Size = new System.Drawing.Size(52, 17);
            this.flipXCheckBox.TabIndex = 0;
            this.flipXCheckBox.Text = "Flip X";
            this.flipXCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scaleLinkAxesCheckBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.zScaleNumericUpDown);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.yScaleNumericUpDown);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.xScaleNumericUpDown);
            this.groupBox2.Location = new System.Drawing.Point(177, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(141, 144);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scale";
            // 
            // scaleLinkAxesCheckBox
            // 
            this.scaleLinkAxesCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.scaleLinkAxesCheckBox.AutoSize = true;
            this.scaleLinkAxesCheckBox.Checked = true;
            this.scaleLinkAxesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleLinkAxesCheckBox.Image = global::ps2ls.Properties.Resources.chain_small;
            this.scaleLinkAxesCheckBox.Location = new System.Drawing.Point(107, 17);
            this.scaleLinkAxesCheckBox.Name = "scaleLinkAxesCheckBox";
            this.scaleLinkAxesCheckBox.Size = new System.Drawing.Size(22, 22);
            this.scaleLinkAxesCheckBox.TabIndex = 11;
            this.scaleLinkAxesCheckBox.UseVisualStyleBackColor = true;
            this.scaleLinkAxesCheckBox.CheckedChanged += new System.EventHandler(this.scaleLinkAxesCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z";
            // 
            // zScaleNumericUpDown
            // 
            this.zScaleNumericUpDown.DecimalPlaces = 4;
            this.zScaleNumericUpDown.Enabled = false;
            this.zScaleNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zScaleNumericUpDown.Location = new System.Drawing.Point(26, 70);
            this.zScaleNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.zScaleNumericUpDown.Name = "zScaleNumericUpDown";
            this.zScaleNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.zScaleNumericUpDown.TabIndex = 9;
            this.zScaleNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y";
            // 
            // yScaleNumericUpDown
            // 
            this.yScaleNumericUpDown.DecimalPlaces = 4;
            this.yScaleNumericUpDown.Enabled = false;
            this.yScaleNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.yScaleNumericUpDown.Location = new System.Drawing.Point(26, 44);
            this.yScaleNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.yScaleNumericUpDown.Name = "yScaleNumericUpDown";
            this.yScaleNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.yScaleNumericUpDown.TabIndex = 7;
            this.yScaleNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X";
            // 
            // xScaleNumericUpDown
            // 
            this.xScaleNumericUpDown.DecimalPlaces = 4;
            this.xScaleNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.xScaleNumericUpDown.Location = new System.Drawing.Point(26, 18);
            this.xScaleNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.xScaleNumericUpDown.Name = "xScaleNumericUpDown";
            this.xScaleNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.xScaleNumericUpDown.TabIndex = 5;
            this.xScaleNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.xScaleNumericUpDown.ValueChanged += new System.EventHandler(this.xScaleNumericUpDown_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textureCoordinatesCheckBox);
            this.groupBox3.Controls.Add(this.normalsCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 39);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(306, 70);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Components";
            // 
            // textureCoordinatesCheckBox
            // 
            this.textureCoordinatesCheckBox.AutoSize = true;
            this.textureCoordinatesCheckBox.Enabled = false;
            this.textureCoordinatesCheckBox.Location = new System.Drawing.Point(6, 43);
            this.textureCoordinatesCheckBox.Name = "textureCoordinatesCheckBox";
            this.textureCoordinatesCheckBox.Size = new System.Drawing.Size(121, 17);
            this.textureCoordinatesCheckBox.TabIndex = 1;
            this.textureCoordinatesCheckBox.Text = "Texture Coordinates";
            this.textureCoordinatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // normalsCheckBox
            // 
            this.normalsCheckBox.AutoSize = true;
            this.normalsCheckBox.Checked = true;
            this.normalsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.normalsCheckBox.Name = "normalsCheckBox";
            this.normalsCheckBox.Size = new System.Drawing.Size(64, 17);
            this.normalsCheckBox.TabIndex = 0;
            this.normalsCheckBox.Text = "Normals";
            this.normalsCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportDirectoryTextBox
            // 
            this.exportDirectoryTextBox.Location = new System.Drawing.Point(12, 278);
            this.exportDirectoryTextBox.Name = "exportDirectoryTextBox";
            this.exportDirectoryTextBox.ReadOnly = true;
            this.exportDirectoryTextBox.Size = new System.Drawing.Size(278, 20);
            this.exportDirectoryTextBox.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Format";
            // 
            // formatComboBox
            // 
            this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Items.AddRange(new object[] {
            "Wavefront OBJ (*.obj)"});
            this.formatComboBox.Location = new System.Drawing.Point(57, 12);
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.Size = new System.Drawing.Size(261, 21);
            this.formatComboBox.TabIndex = 4;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(243, 304);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 6;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Directory";
            // 
            // openExportFolderBrowserDialogButton
            // 
            this.openExportFolderBrowserDialogButton.Image = global::ps2ls.Properties.Resources.folder_small_horizontal;
            this.openExportFolderBrowserDialogButton.Location = new System.Drawing.Point(296, 277);
            this.openExportFolderBrowserDialogButton.Name = "openExportFolderBrowserDialogButton";
            this.openExportFolderBrowserDialogButton.Size = new System.Drawing.Size(22, 22);
            this.openExportFolderBrowserDialogButton.TabIndex = 1;
            this.openExportFolderBrowserDialogButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.openExportFolderBrowserDialogButton.UseVisualStyleBackColor = true;
            this.openExportFolderBrowserDialogButton.Click += new System.EventHandler(this.openExportFolderBrowserDialogButton_Click);
            // 
            // ModelExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 334);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.openExportFolderBrowserDialogButton);
            this.Controls.Add(this.exportDirectoryTextBox);
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelExportForm";
            this.ShowInTaskbar = false;
            this.Text = "Model Export";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ModelExportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zScaleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yScaleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xScaleNumericUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox flipZCheckBox;
        private System.Windows.Forms.CheckBox flipYCheckBox;
        private System.Windows.Forms.CheckBox flipXCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown yScaleNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xScaleNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown zScaleNumericUpDown;
        private System.Windows.Forms.CheckBox scaleLinkAxesCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox normalsCheckBox;
        private System.Windows.Forms.CheckBox textureCoordinatesCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox upAxisComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox leftAxisComboBox;
        private System.Windows.Forms.Button openExportFolderBrowserDialogButton;
        private System.Windows.Forms.TextBox exportDirectoryTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox formatComboBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog exportFolderBrowserDialog;
    }
}