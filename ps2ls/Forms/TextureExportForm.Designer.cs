namespace ps2ls.Forms
{
    partial class TextureExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureExportForm));
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.modelsListBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // formatComboBox
            // 
            this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Items.AddRange(new object[] {
            "DirectDraw Surface (*.dds)",
            "Portal Network Graphics (*.png)",
            "Truevision TGA (*.tga)"});
            this.formatComboBox.Location = new System.Drawing.Point(149, 87);
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.Size = new System.Drawing.Size(123, 21);
            this.formatComboBox.TabIndex = 13;
            // 
            // modelsListBox
            // 
            this.modelsListBox.FormattingEnabled = true;
            this.modelsListBox.Location = new System.Drawing.Point(12, 12);
            this.modelsListBox.Name = "modelsListBox";
            this.modelsListBox.Size = new System.Drawing.Size(260, 69);
            this.modelsListBox.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TextureExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.modelsListBox);
            this.Controls.Add(this.formatComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextureExportForm";
            this.ShowInTaskbar = false;
            this.Text = "Texture Export";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox formatComboBox;
        private System.Windows.Forms.ListBox modelsListBox;
        private System.Windows.Forms.Button button1;
    }
}