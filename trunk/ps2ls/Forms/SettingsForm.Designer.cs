namespace ps2ls.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadAssetsOnStartCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.assetDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.assetDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.assetDirectoryAutoDetectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.noButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 206);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 180);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadAssetsOnStartCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.assetDirectoryTextBox);
            this.groupBox1.Controls.Add(this.assetDirectoryBrowseButton);
            this.groupBox1.Controls.Add(this.assetDirectoryAutoDetectButton);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Assets";
            // 
            // loadAssetsOnStartCheckBox
            // 
            this.loadAssetsOnStartCheckBox.AutoSize = true;
            this.loadAssetsOnStartCheckBox.Location = new System.Drawing.Point(9, 47);
            this.loadAssetsOnStartCheckBox.Name = "loadAssetsOnStartCheckBox";
            this.loadAssetsOnStartCheckBox.Size = new System.Drawing.Size(182, 17);
            this.loadAssetsOnStartCheckBox.TabIndex = 5;
            this.loadAssetsOnStartCheckBox.Text = "Automatically load assets on start";
            this.loadAssetsOnStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Assets Directory";
            // 
            // assetDirectoryTextBox
            // 
            this.assetDirectoryTextBox.Location = new System.Drawing.Point(89, 21);
            this.assetDirectoryTextBox.Name = "assetDirectoryTextBox";
            this.assetDirectoryTextBox.Size = new System.Drawing.Size(394, 20);
            this.assetDirectoryTextBox.TabIndex = 3;
            // 
            // assetDirectoryBrowseButton
            // 
            this.assetDirectoryBrowseButton.Image = global::ps2ls.Properties.Resources.folder_small_horizontal;
            this.assetDirectoryBrowseButton.Location = new System.Drawing.Point(489, 19);
            this.assetDirectoryBrowseButton.Name = "assetDirectoryBrowseButton";
            this.assetDirectoryBrowseButton.Size = new System.Drawing.Size(23, 23);
            this.assetDirectoryBrowseButton.TabIndex = 2;
            this.assetDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.assetDirectoryBrowseButton.Click += new System.EventHandler(this.assetDirectoryBrowseButton_Click);
            // 
            // assetDirectoryAutoDetectButton
            // 
            this.assetDirectoryAutoDetectButton.Location = new System.Drawing.Point(518, 19);
            this.assetDirectoryAutoDetectButton.Name = "assetDirectoryAutoDetectButton";
            this.assetDirectoryAutoDetectButton.Size = new System.Drawing.Size(75, 23);
            this.assetDirectoryAutoDetectButton.TabIndex = 1;
            this.assetDirectoryAutoDetectButton.Text = "Auto-Detect";
            this.assetDirectoryAutoDetectButton.UseVisualStyleBackColor = true;
            this.assetDirectoryAutoDetectButton.Click += new System.EventHandler(this.assetDirectoryAutoDetectButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.noButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 156);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 50);
            this.panel1.TabIndex = 5;
            // 
            // noButton
            // 
            this.noButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.noButton.Location = new System.Drawing.Point(537, 15);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 1;
            this.noButton.Text = "Apply";
            this.noButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(456, 15);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 206);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button assetDirectoryAutoDetectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox assetDirectoryTextBox;
        private System.Windows.Forms.Button assetDirectoryBrowseButton;
        private System.Windows.Forms.CheckBox loadAssetsOnStartCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button saveButton;


    }
}