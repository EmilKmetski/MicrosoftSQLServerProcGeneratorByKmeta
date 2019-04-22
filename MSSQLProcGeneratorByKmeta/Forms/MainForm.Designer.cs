namespace MSSQLProcGeneratorByKmeta.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btExit = new System.Windows.Forms.Button();
            this.btGenProcs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDestDir = new System.Windows.Forms.TextBox();
            this.btDestDir = new System.Windows.Forms.Button();
            this.templateFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.procsDestDir = new System.Windows.Forms.FolderBrowserDialog();
            this.tbCreatorInitials = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.returnID = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.comboDbName = new System.Windows.Forms.ComboBox();
            this.cbCustomColumnsMapping = new System.Windows.Forms.CheckBox();
            this.tbDateFormat = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.customMappingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.btSQLFormat = new System.Windows.Forms.Button();
            this.cbFormatSQL = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btExit
            // 
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Location = new System.Drawing.Point(451, 159);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(65, 23);
            this.btExit.TabIndex = 0;
            this.btExit.Text = "Exit";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btGenProcs
            // 
            this.btGenProcs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGenProcs.Location = new System.Drawing.Point(326, 159);
            this.btGenProcs.Name = "btGenProcs";
            this.btGenProcs.Size = new System.Drawing.Size(119, 23);
            this.btGenProcs.TabIndex = 2;
            this.btGenProcs.Text = "Gen Procs";
            this.btGenProcs.UseVisualStyleBackColor = true;
            this.btGenProcs.Click += new System.EventHandler(this.btGenProcs_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Destination dir";
            // 
            // tbDestDir
            // 
            this.tbDestDir.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbDestDir.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbDestDir.Location = new System.Drawing.Point(158, 38);
            this.tbDestDir.Name = "tbDestDir";
            this.tbDestDir.Size = new System.Drawing.Size(287, 20);
            this.tbDestDir.TabIndex = 7;
            this.tbDestDir.Text = "C:\\";
            // 
            // btDestDir
            // 
            this.btDestDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDestDir.Location = new System.Drawing.Point(451, 36);
            this.btDestDir.Name = "btDestDir";
            this.btDestDir.Size = new System.Drawing.Size(65, 23);
            this.btDestDir.TabIndex = 8;
            this.btDestDir.Text = "Browse";
            this.btDestDir.UseVisualStyleBackColor = true;
            this.btDestDir.Click += new System.EventHandler(this.btDestDir_Click);
            // 
            // templateFileDialog
            // 
            this.templateFileDialog.FileName = "ProcTemplate";
            this.templateFileDialog.Filter = "Microsoft SQL Script File|*.sql";
            this.templateFileDialog.InitialDirectory = "C:\\";
            // 
            // tbCreatorInitials
            // 
            this.tbCreatorInitials.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbCreatorInitials.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbCreatorInitials.Location = new System.Drawing.Point(158, 61);
            this.tbCreatorInitials.Name = "tbCreatorInitials";
            this.tbCreatorInitials.Size = new System.Drawing.Size(287, 20);
            this.tbCreatorInitials.TabIndex = 15;
            this.tbCreatorInitials.Text = "EBK";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Creator Initials";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(158, 114);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(147, 17);
            this.radioButton1.TabIndex = 17;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Return ID + Inserted Data";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // returnID
            // 
            this.returnID.AutoSize = true;
            this.returnID.Location = new System.Drawing.Point(374, 114);
            this.returnID.Name = "returnID";
            this.returnID.Size = new System.Drawing.Size(71, 17);
            this.returnID.TabIndex = 18;
            this.returnID.TabStop = true;
            this.returnID.Text = "Return ID";
            this.returnID.UseVisualStyleBackColor = true;
            this.returnID.CheckedChanged += new System.EventHandler(this.returnID_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Database";
            // 
            // comboDbName
            // 
            this.comboDbName.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.comboDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDbName.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.comboDbName.FormattingEnabled = true;
            this.comboDbName.Location = new System.Drawing.Point(158, 12);
            this.comboDbName.Name = "comboDbName";
            this.comboDbName.Size = new System.Drawing.Size(287, 21);
            this.comboDbName.TabIndex = 20;
            this.comboDbName.SelectedIndexChanged += new System.EventHandler(this.comboDbName_SelectedIndexChanged);
            // 
            // cbCustomColumnsMapping
            // 
            this.cbCustomColumnsMapping.AutoSize = true;
            this.cbCustomColumnsMapping.Location = new System.Drawing.Point(158, 136);
            this.cbCustomColumnsMapping.Margin = new System.Windows.Forms.Padding(2);
            this.cbCustomColumnsMapping.Name = "cbCustomColumnsMapping";
            this.cbCustomColumnsMapping.Size = new System.Drawing.Size(148, 17);
            this.cbCustomColumnsMapping.TabIndex = 22;
            this.cbCustomColumnsMapping.Text = "Custom Columns Mapping";
            this.cbCustomColumnsMapping.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbCustomColumnsMapping.UseVisualStyleBackColor = true;
            this.cbCustomColumnsMapping.CheckedChanged += new System.EventHandler(this.cbCustomColumnsMapping_CheckedChanged);
            // 
            // tbDateFormat
            // 
            this.tbDateFormat.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbDateFormat.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbDateFormat.Location = new System.Drawing.Point(158, 85);
            this.tbDateFormat.Name = "tbDateFormat";
            this.tbDateFormat.Size = new System.Drawing.Size(287, 20);
            this.tbDateFormat.TabIndex = 23;
            this.tbDateFormat.Text = "MM/dd/yyyy";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Creation Date Format";
            // 
            // customMappingsDialog
            // 
            this.customMappingsDialog.FileName = "CustomColumsMappings.xlsx";
            this.customMappingsDialog.Filter = "Microsoft Excel 2007 File|*.xlsx";
            // 
            // btSQLFormat
            // 
            this.btSQLFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSQLFormat.Location = new System.Drawing.Point(158, 159);
            this.btSQLFormat.Name = "btSQLFormat";
            this.btSQLFormat.Size = new System.Drawing.Size(147, 23);
            this.btSQLFormat.TabIndex = 28;
            this.btSQLFormat.Text = "SQL Formater";
            this.btSQLFormat.UseVisualStyleBackColor = true;
            this.btSQLFormat.Click += new System.EventHandler(this.btSQLFormat_Click);
            // 
            // cbFormatSQL
            // 
            this.cbFormatSQL.AutoSize = true;
            this.cbFormatSQL.Location = new System.Drawing.Point(326, 136);
            this.cbFormatSQL.Name = "cbFormatSQL";
            this.cbFormatSQL.Size = new System.Drawing.Size(119, 17);
            this.cbFormatSQL.TabIndex = 29;
            this.cbFormatSQL.Text = "Use Format settings";
            this.cbFormatSQL.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Return from Insert Prc";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Additional Settings";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(547, 200);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbFormatSQL);
            this.Controls.Add(this.btSQLFormat);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbDateFormat);
            this.Controls.Add(this.cbCustomColumnsMapping);
            this.Controls.Add(this.comboDbName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.returnID);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbCreatorInitials);
            this.Controls.Add(this.btDestDir);
            this.Controls.Add(this.tbDestDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btGenProcs);
            this.Controls.Add(this.btExit);
            this.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MSSQL Procedures Generator By Kmet@ ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btGenProcs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDestDir;
        private System.Windows.Forms.Button btDestDir;
        private System.Windows.Forms.OpenFileDialog templateFileDialog;
        private System.Windows.Forms.FolderBrowserDialog procsDestDir;
        private System.Windows.Forms.TextBox tbCreatorInitials;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton returnID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboDbName;
        private System.Windows.Forms.CheckBox cbCustomColumnsMapping;
        private System.Windows.Forms.TextBox tbDateFormat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog customMappingsDialog;
        private System.Windows.Forms.Button btSQLFormat;
        private System.Windows.Forms.CheckBox cbFormatSQL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

