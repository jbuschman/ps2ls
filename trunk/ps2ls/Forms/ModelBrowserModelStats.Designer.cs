namespace ps2ls.Forms
{
    partial class ModelBrowserModelStats
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.meshCountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.modelVertexCountLabel = new System.Windows.Forms.Label();
            this.modelTriangleCountLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.materialCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.modelUnknown0Label = new System.Windows.Forms.Label();
            this.modelUnknown1Label = new System.Windows.Forms.Label();
            this.mdoelUnknown2Label = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.meshesComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.meshVertexCountLabel = new System.Windows.Forms.Label();
            this.meshTriangleCountLabel = new System.Windows.Forms.Label();
            this.meshBytesPerVertexLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 161);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 135);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Model";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(470, 129);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.meshCountLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.modelVertexCountLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.modelTriangleCountLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.materialCount, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(229, 123);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(85, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mesh Count";
            // 
            // meshCountLabel
            // 
            this.meshCountLabel.AutoSize = true;
            this.meshCountLabel.Location = new System.Drawing.Point(85, 20);
            this.meshCountLabel.Name = "meshCountLabel";
            this.meshCountLabel.Size = new System.Drawing.Size(13, 13);
            this.meshCountLabel.TabIndex = 3;
            this.meshCountLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Vertex Count";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Triangle Count";
            // 
            // modelVertexCountLabel
            // 
            this.modelVertexCountLabel.AutoSize = true;
            this.modelVertexCountLabel.Location = new System.Drawing.Point(85, 40);
            this.modelVertexCountLabel.Name = "modelVertexCountLabel";
            this.modelVertexCountLabel.Size = new System.Drawing.Size(13, 13);
            this.modelVertexCountLabel.TabIndex = 6;
            this.modelVertexCountLabel.Text = "0";
            // 
            // modelTriangleCountLabel
            // 
            this.modelTriangleCountLabel.AutoSize = true;
            this.modelTriangleCountLabel.Location = new System.Drawing.Point(85, 60);
            this.modelTriangleCountLabel.Name = "modelTriangleCountLabel";
            this.modelTriangleCountLabel.Size = new System.Drawing.Size(13, 13);
            this.modelTriangleCountLabel.TabIndex = 7;
            this.modelTriangleCountLabel.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Material Count";
            // 
            // materialCount
            // 
            this.materialCount.AutoSize = true;
            this.materialCount.Location = new System.Drawing.Point(85, 80);
            this.materialCount.Name = "materialCount";
            this.materialCount.Size = new System.Drawing.Size(13, 13);
            this.materialCount.TabIndex = 9;
            this.materialCount.Text = "0";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.modelUnknown0Label, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.modelUnknown1Label, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.mdoelUnknown2Label, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(238, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(229, 123);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Unknown 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Unknown 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Unknown 3";
            // 
            // modelUnknown0Label
            // 
            this.modelUnknown0Label.AutoSize = true;
            this.modelUnknown0Label.Location = new System.Drawing.Point(117, 0);
            this.modelUnknown0Label.Name = "modelUnknown0Label";
            this.modelUnknown0Label.Size = new System.Drawing.Size(13, 13);
            this.modelUnknown0Label.TabIndex = 3;
            this.modelUnknown0Label.Text = "0";
            // 
            // modelUnknown1Label
            // 
            this.modelUnknown1Label.AutoSize = true;
            this.modelUnknown1Label.Location = new System.Drawing.Point(117, 20);
            this.modelUnknown1Label.Name = "modelUnknown1Label";
            this.modelUnknown1Label.Size = new System.Drawing.Size(13, 13);
            this.modelUnknown1Label.TabIndex = 4;
            this.modelUnknown1Label.Text = "0";
            // 
            // mdoelUnknown2Label
            // 
            this.mdoelUnknown2Label.AutoSize = true;
            this.mdoelUnknown2Label.Location = new System.Drawing.Point(117, 40);
            this.mdoelUnknown2Label.Name = "mdoelUnknown2Label";
            this.mdoelUnknown2Label.Size = new System.Drawing.Size(13, 13);
            this.mdoelUnknown2Label.TabIndex = 5;
            this.mdoelUnknown2Label.Text = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(476, 135);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Meshes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.meshesComboBox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(470, 129);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // meshesComboBox
            // 
            this.meshesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.meshesComboBox.FormattingEnabled = true;
            this.meshesComboBox.Location = new System.Drawing.Point(3, 3);
            this.meshesComboBox.Name = "meshesComboBox";
            this.meshesComboBox.Size = new System.Drawing.Size(121, 21);
            this.meshesComboBox.TabIndex = 0;
            this.meshesComboBox.SelectedIndexChanged += new System.EventHandler(this.meshesComboBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label99, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label88, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.meshVertexCountLabel, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.meshTriangleCountLabel, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.meshBytesPerVertexLabel, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(130, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(337, 123);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Vertex Count";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(3, 20);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(76, 13);
            this.label99.TabIndex = 1;
            this.label99.Text = "Triangle Count";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(3, 40);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(85, 13);
            this.label88.TabIndex = 2;
            this.label88.Text = "Bytes Per Vertex";
            // 
            // meshVertexCountLabel
            // 
            this.meshVertexCountLabel.AutoSize = true;
            this.meshVertexCountLabel.Location = new System.Drawing.Point(94, 0);
            this.meshVertexCountLabel.Name = "meshVertexCountLabel";
            this.meshVertexCountLabel.Size = new System.Drawing.Size(13, 13);
            this.meshVertexCountLabel.TabIndex = 4;
            this.meshVertexCountLabel.Text = "0";
            // 
            // meshTriangleCountLabel
            // 
            this.meshTriangleCountLabel.AutoSize = true;
            this.meshTriangleCountLabel.Location = new System.Drawing.Point(94, 20);
            this.meshTriangleCountLabel.Name = "meshTriangleCountLabel";
            this.meshTriangleCountLabel.Size = new System.Drawing.Size(13, 13);
            this.meshTriangleCountLabel.TabIndex = 5;
            this.meshTriangleCountLabel.Text = "0";
            // 
            // meshBytesPerVertexLabel
            // 
            this.meshBytesPerVertexLabel.AutoSize = true;
            this.meshBytesPerVertexLabel.Location = new System.Drawing.Point(94, 40);
            this.meshBytesPerVertexLabel.Name = "meshBytesPerVertexLabel";
            this.meshBytesPerVertexLabel.Size = new System.Drawing.Size(13, 13);
            this.meshBytesPerVertexLabel.TabIndex = 6;
            this.meshBytesPerVertexLabel.Text = "0";
            // 
            // ModelBrowserModelStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ModelBrowserModelStats";
            this.Size = new System.Drawing.Size(484, 161);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label meshCountLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label modelVertexCountLabel;
        private System.Windows.Forms.Label modelTriangleCountLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label materialCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label modelUnknown0Label;
        private System.Windows.Forms.Label modelUnknown1Label;
        private System.Windows.Forms.Label mdoelUnknown2Label;
        private System.Windows.Forms.ComboBox meshesComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label meshVertexCountLabel;
        private System.Windows.Forms.Label meshTriangleCountLabel;
        private System.Windows.Forms.Label meshBytesPerVertexLabel;

    }
}
