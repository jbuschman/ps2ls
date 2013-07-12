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
            this.label8 = new System.Windows.Forms.Label();
            this.modelAxesPresetComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.leftAxisComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.upAxisComboBox = new System.Windows.Forms.ComboBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.modelFormatComboBox = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.exportFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.packageCheckBox = new System.Windows.Forms.CheckBox();
            this.modelsListBox = new System.Windows.Forms.ListBox();
            this.exportTexturesCheckBox = new System.Windows.Forms.CheckBox();
            this.textureFormatComboBox = new System.Windows.Forms.ComboBox();
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
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.modelAxesPresetComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.leftAxisComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.upAxisComboBox);
            this.groupBox1.Location = new System.Drawing.Point(11, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model Axes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Preset";
            // 
            // modelAxesPresetComboBox
            // 
            this.modelAxesPresetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelAxesPresetComboBox.FormattingEnabled = true;
            this.modelAxesPresetComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.modelAxesPresetComboBox.Location = new System.Drawing.Point(60, 19);
            this.modelAxesPresetComboBox.MaxDropDownItems = 3;
            this.modelAxesPresetComboBox.MaxLength = 1;
            this.modelAxesPresetComboBox.Name = "modelAxesPresetComboBox";
            this.modelAxesPresetComboBox.Size = new System.Drawing.Size(195, 21);
            this.modelAxesPresetComboBox.TabIndex = 8;
            this.modelAxesPresetComboBox.SelectedIndexChanged += new System.EventHandler(this.modelAxesPresetComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(116, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Left Axis";
            // 
            // leftAxisComboBox
            // 
            this.leftAxisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftAxisComboBox.Enabled = false;
            this.leftAxisComboBox.FormattingEnabled = true;
            this.leftAxisComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.leftAxisComboBox.Location = new System.Drawing.Point(169, 46);
            this.leftAxisComboBox.MaxDropDownItems = 3;
            this.leftAxisComboBox.MaxLength = 1;
            this.leftAxisComboBox.Name = "leftAxisComboBox";
            this.leftAxisComboBox.Size = new System.Drawing.Size(50, 21);
            this.leftAxisComboBox.TabIndex = 7;
            this.leftAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.leftAxisComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Up Axis";
            // 
            // upAxisComboBox
            // 
            this.upAxisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.upAxisComboBox.Enabled = false;
            this.upAxisComboBox.FormattingEnabled = true;
            this.upAxisComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.upAxisComboBox.Location = new System.Drawing.Point(60, 46);
            this.upAxisComboBox.MaxDropDownItems = 3;
            this.upAxisComboBox.MaxLength = 1;
            this.upAxisComboBox.Name = "upAxisComboBox";
            this.upAxisComboBox.Size = new System.Drawing.Size(50, 21);
            this.upAxisComboBox.TabIndex = 6;
            this.upAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.upAxisComboBox_SelectedIndexChanged);
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
            this.groupBox2.Location = new System.Drawing.Point(11, 303);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Model Scale";
            // 
            // scaleLinkAxesCheckBox
            // 
            this.scaleLinkAxesCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.scaleLinkAxesCheckBox.AutoSize = true;
            this.scaleLinkAxesCheckBox.Checked = true;
            this.scaleLinkAxesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleLinkAxesCheckBox.Image = global::ps2ls.Properties.Resources.chain_small;
            this.scaleLinkAxesCheckBox.Location = new System.Drawing.Point(234, 17);
            this.scaleLinkAxesCheckBox.Name = "scaleLinkAxesCheckBox";
            this.scaleLinkAxesCheckBox.Size = new System.Drawing.Size(22, 22);
            this.scaleLinkAxesCheckBox.TabIndex = 11;
            this.scaleLinkAxesCheckBox.TabStop = false;
            this.scaleLinkAxesCheckBox.UseVisualStyleBackColor = true;
            this.scaleLinkAxesCheckBox.CheckedChanged += new System.EventHandler(this.scaleLinkAxesCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z";
            // 
            // zScaleNumericUpDown
            // 
            this.zScaleNumericUpDown.DecimalPlaces = 3;
            this.zScaleNumericUpDown.Enabled = false;
            this.zScaleNumericUpDown.Location = new System.Drawing.Point(178, 18);
            this.zScaleNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.zScaleNumericUpDown.Name = "zScaleNumericUpDown";
            this.zScaleNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.zScaleNumericUpDown.TabIndex = 10;
            this.zScaleNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.zScaleNumericUpDown.ValueChanged += new System.EventHandler(this.zScaleNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y";
            // 
            // yScaleNumericUpDown
            // 
            this.yScaleNumericUpDown.DecimalPlaces = 3;
            this.yScaleNumericUpDown.Enabled = false;
            this.yScaleNumericUpDown.Location = new System.Drawing.Point(102, 18);
            this.yScaleNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.yScaleNumericUpDown.Name = "yScaleNumericUpDown";
            this.yScaleNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.yScaleNumericUpDown.TabIndex = 9;
            this.yScaleNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.yScaleNumericUpDown.ValueChanged += new System.EventHandler(this.yScaleNumericUpDown_ValueChanged);
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
            this.xScaleNumericUpDown.DecimalPlaces = 3;
            this.xScaleNumericUpDown.Location = new System.Drawing.Point(26, 18);
            this.xScaleNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.xScaleNumericUpDown.Name = "xScaleNumericUpDown";
            this.xScaleNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.xScaleNumericUpDown.TabIndex = 8;
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
            this.groupBox3.Location = new System.Drawing.Point(11, 141);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 70);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Model Components";
            // 
            // textureCoordinatesCheckBox
            // 
            this.textureCoordinatesCheckBox.AutoSize = true;
            this.textureCoordinatesCheckBox.Enabled = false;
            this.textureCoordinatesCheckBox.Location = new System.Drawing.Point(6, 43);
            this.textureCoordinatesCheckBox.Name = "textureCoordinatesCheckBox";
            this.textureCoordinatesCheckBox.Size = new System.Drawing.Size(121, 17);
            this.textureCoordinatesCheckBox.TabIndex = 2;
            this.textureCoordinatesCheckBox.Text = "Texture Coordinates";
            this.textureCoordinatesCheckBox.UseVisualStyleBackColor = true;
            this.textureCoordinatesCheckBox.CheckedChanged += new System.EventHandler(this.textureCoordinatesCheckBox_CheckedChanged);
            // 
            // normalsCheckBox
            // 
            this.normalsCheckBox.AutoSize = true;
            this.normalsCheckBox.Checked = true;
            this.normalsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.normalsCheckBox.Name = "normalsCheckBox";
            this.normalsCheckBox.Size = new System.Drawing.Size(64, 17);
            this.normalsCheckBox.TabIndex = 1;
            this.normalsCheckBox.Text = "Normals";
            this.normalsCheckBox.UseVisualStyleBackColor = true;
            this.normalsCheckBox.CheckedChanged += new System.EventHandler(this.normalsCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Model Format";
            // 
            // modelFormatComboBox
            // 
            this.modelFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelFormatComboBox.FormattingEnabled = true;
            this.modelFormatComboBox.Location = new System.Drawing.Point(88, 87);
            this.modelFormatComboBox.Name = "modelFormatComboBox";
            this.modelFormatComboBox.Size = new System.Drawing.Size(184, 21);
            this.modelFormatComboBox.TabIndex = 0;
            this.modelFormatComboBox.SelectedIndexChanged += new System.EventHandler(this.formatComboBox_SelectedIndexChanged);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(197, 358);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // packageCheckBox
            // 
            this.packageCheckBox.AutoSize = true;
            this.packageCheckBox.Checked = true;
            this.packageCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.packageCheckBox.Location = new System.Drawing.Point(12, 362);
            this.packageCheckBox.Name = "packageCheckBox";
            this.packageCheckBox.Size = new System.Drawing.Size(103, 17);
            this.packageCheckBox.TabIndex = 8;
            this.packageCheckBox.Text = "Package Assets";
            this.packageCheckBox.UseVisualStyleBackColor = true;
            this.packageCheckBox.CheckedChanged += new System.EventHandler(this.packageCheckBox_CheckedChanged);
            // 
            // modelsListBox
            // 
            this.modelsListBox.FormattingEnabled = true;
            this.modelsListBox.Location = new System.Drawing.Point(12, 12);
            this.modelsListBox.Name = "modelsListBox";
            this.modelsListBox.Size = new System.Drawing.Size(260, 69);
            this.modelsListBox.TabIndex = 10;
            // 
            // exportTexturesCheckBox
            // 
            this.exportTexturesCheckBox.AutoSize = true;
            this.exportTexturesCheckBox.Checked = true;
            this.exportTexturesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exportTexturesCheckBox.Location = new System.Drawing.Point(14, 116);
            this.exportTexturesCheckBox.Name = "exportTexturesCheckBox";
            this.exportTexturesCheckBox.Size = new System.Drawing.Size(100, 17);
            this.exportTexturesCheckBox.TabIndex = 11;
            this.exportTexturesCheckBox.Text = "Export Textures";
            this.exportTexturesCheckBox.UseVisualStyleBackColor = true;
            this.exportTexturesCheckBox.CheckedChanged += new System.EventHandler(this.exportTexturesCheckBox_CheckedChanged_1);
            // 
            // textureFormatComboBox
            // 
            this.textureFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textureFormatComboBox.FormattingEnabled = true;
            this.textureFormatComboBox.Items.AddRange(new object[] {
            "DirectDraw Surface (*.dds)",
            "Portal Network Graphics (*.png)",
            "Truevision TGA (*.tga)"});
            this.textureFormatComboBox.Location = new System.Drawing.Point(120, 114);
            this.textureFormatComboBox.Name = "textureFormatComboBox";
            this.textureFormatComboBox.Size = new System.Drawing.Size(152, 21);
            this.textureFormatComboBox.TabIndex = 12;
            // 
            // ModelExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 393);
            this.Controls.Add(this.exportTexturesCheckBox);
            this.Controls.Add(this.textureFormatComboBox);
            this.Controls.Add(this.modelsListBox);
            this.Controls.Add(this.packageCheckBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.modelFormatComboBox);
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
        private System.Windows.Forms.ComboBox leftAxisComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox modelFormatComboBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.FolderBrowserDialog exportFolderBrowserDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox modelAxesPresetComboBox;
        private System.Windows.Forms.CheckBox packageCheckBox;
        private System.Windows.Forms.ListBox modelsListBox;
        private System.Windows.Forms.CheckBox exportTexturesCheckBox;
        private System.Windows.Forms.ComboBox textureFormatComboBox;
    }
}