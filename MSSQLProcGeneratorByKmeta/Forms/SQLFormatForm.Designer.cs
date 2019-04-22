namespace MSSQLProcGeneratorByKmeta.Forms
{
    partial class SQLFormatForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLFormatForm));
            this.rtbInputSQL = new System.Windows.Forms.RichTextBox();
            this.rtbOutSQL = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btParseQuery = new System.Windows.Forms.Button();
            this.btFormat = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbFormatSettings = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btClearFilters = new System.Windows.Forms.Button();
            this.btClearSPtext = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAndLogic = new System.Windows.Forms.RadioButton();
            this.rbORLocgic = new System.Windows.Forms.RadioButton();
            this.gbSearchOption = new System.Windows.Forms.GroupBox();
            this.tbSQLFunction = new System.Windows.Forms.TextBox();
            this.tbTableName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbColumnName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFreeText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbParameterName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbIsNulable = new System.Windows.Forms.RadioButton();
            this.rbIsReadOnly = new System.Windows.Forms.RadioButton();
            this.cblParamDataType = new System.Windows.Forms.ComboBox();
            this.rbIsOutput = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btFindProcs = new System.Windows.Forms.Button();
            this.rtbProcText = new System.Windows.Forms.RichTextBox();
            this.dtgvResult = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbSearchOption.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbInputSQL
            // 
            this.rtbInputSQL.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rtbInputSQL.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.rtbInputSQL.Location = new System.Drawing.Point(5, 24);
            this.rtbInputSQL.Name = "rtbInputSQL";
            this.rtbInputSQL.Size = new System.Drawing.Size(1106, 156);
            this.rtbInputSQL.TabIndex = 0;
            this.rtbInputSQL.Text = "";
            // 
            // rtbOutSQL
            // 
            this.rtbOutSQL.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rtbOutSQL.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.rtbOutSQL.Location = new System.Drawing.Point(8, 198);
            this.rtbOutSQL.Name = "rtbOutSQL";
            this.rtbOutSQL.Size = new System.Drawing.Size(1103, 523);
            this.rtbOutSQL.TabIndex = 1;
            this.rtbOutSQL.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "String to format";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Formatted string";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1126, 792);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.btParseQuery);
            this.tabPage1.Controls.Add(this.btFormat);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.rtbInputSQL);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.rtbOutSQL);
            this.tabPage1.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1118, 760);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL Strings";
            // 
            // btParseQuery
            // 
            this.btParseQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btParseQuery.Location = new System.Drawing.Point(955, 727);
            this.btParseQuery.Name = "btParseQuery";
            this.btParseQuery.Size = new System.Drawing.Size(75, 23);
            this.btParseQuery.TabIndex = 8;
            this.btParseQuery.Text = "Parse string";
            this.btParseQuery.UseVisualStyleBackColor = true;
            this.btParseQuery.Click += new System.EventHandler(this.btParseQuery_Click);
            // 
            // btFormat
            // 
            this.btFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFormat.Location = new System.Drawing.Point(1036, 727);
            this.btFormat.Name = "btFormat";
            this.btFormat.Size = new System.Drawing.Size(75, 23);
            this.btFormat.TabIndex = 7;
            this.btFormat.Text = "Format string";
            this.btFormat.UseVisualStyleBackColor = true;
            this.btFormat.Click += new System.EventHandler(this.btFormat_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.gbFormatSettings);
            this.tabPage2.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1118, 760);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SQL Format Settings";
            // 
            // gbFormatSettings
            // 
            this.gbFormatSettings.BackColor = System.Drawing.Color.Black;
            this.gbFormatSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbFormatSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFormatSettings.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.gbFormatSettings.Location = new System.Drawing.Point(5, 6);
            this.gbFormatSettings.Name = "gbFormatSettings";
            this.gbFormatSettings.Size = new System.Drawing.Size(1106, 436);
            this.gbFormatSettings.TabIndex = 10;
            this.gbFormatSettings.TabStop = false;
            this.gbFormatSettings.Text = "Format settings";
            this.gbFormatSettings.Enter += new System.EventHandler(this.gbFormatSettings_Enter);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.btClearFilters);
            this.tabPage3.Controls.Add(this.btClearSPtext);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.gbSearchOption);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btFindProcs);
            this.tabPage3.Controls.Add(this.rtbProcText);
            this.tabPage3.Controls.Add(this.dtgvResult);
            this.tabPage3.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(1118, 760);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Procedures Search";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(468, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 53);
            this.label12.TabIndex = 35;
            this.label12.Text = "OR logic means any field have match form the non empty ";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(468, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(161, 46);
            this.label11.TabIndex = 34;
            this.label11.Text = "AND logic means all fields should have match";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // btClearFilters
            // 
            this.btClearFilters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFilters.Location = new System.Drawing.Point(635, 53);
            this.btClearFilters.Name = "btClearFilters";
            this.btClearFilters.Size = new System.Drawing.Size(75, 53);
            this.btClearFilters.TabIndex = 33;
            this.btClearFilters.Text = "Clear Filters";
            this.btClearFilters.UseVisualStyleBackColor = true;
            this.btClearFilters.Click += new System.EventHandler(this.btClearFilters_Click);
            // 
            // btClearSPtext
            // 
            this.btClearSPtext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btClearSPtext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearSPtext.Location = new System.Drawing.Point(636, 114);
            this.btClearSPtext.Name = "btClearSPtext";
            this.btClearSPtext.Size = new System.Drawing.Size(75, 43);
            this.btClearSPtext.TabIndex = 32;
            this.btClearSPtext.Text = "Clear SP Text";
            this.btClearSPtext.UseVisualStyleBackColor = true;
            this.btClearSPtext.Click += new System.EventHandler(this.btClearSPtext_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAndLogic);
            this.groupBox2.Controls.Add(this.rbORLocgic);
            this.groupBox2.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.groupBox2.Location = new System.Drawing.Point(315, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 115);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search logic";
            // 
            // rbAndLogic
            // 
            this.rbAndLogic.AutoSize = true;
            this.rbAndLogic.Location = new System.Drawing.Point(6, 23);
            this.rbAndLogic.Name = "rbAndLogic";
            this.rbAndLogic.Size = new System.Drawing.Size(53, 20);
            this.rbAndLogic.TabIndex = 1;
            this.rbAndLogic.TabStop = true;
            this.rbAndLogic.Text = "AND";
            this.rbAndLogic.UseVisualStyleBackColor = true;
            this.rbAndLogic.CheckedChanged += new System.EventHandler(this.rbAndLogic_CheckedChanged);
            // 
            // rbORLocgic
            // 
            this.rbORLocgic.AutoSize = true;
            this.rbORLocgic.Location = new System.Drawing.Point(96, 23);
            this.rbORLocgic.Name = "rbORLocgic";
            this.rbORLocgic.Size = new System.Drawing.Size(45, 20);
            this.rbORLocgic.TabIndex = 0;
            this.rbORLocgic.TabStop = true;
            this.rbORLocgic.Text = "OR";
            this.rbORLocgic.UseVisualStyleBackColor = true;
            this.rbORLocgic.CheckedChanged += new System.EventHandler(this.rbORLocgic_CheckedChanged);
            // 
            // gbSearchOption
            // 
            this.gbSearchOption.Controls.Add(this.tbSQLFunction);
            this.gbSearchOption.Controls.Add(this.tbTableName);
            this.gbSearchOption.Controls.Add(this.label5);
            this.gbSearchOption.Controls.Add(this.label10);
            this.gbSearchOption.Controls.Add(this.tbColumnName);
            this.gbSearchOption.Controls.Add(this.label9);
            this.gbSearchOption.Controls.Add(this.label8);
            this.gbSearchOption.Controls.Add(this.tbFreeText);
            this.gbSearchOption.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.gbSearchOption.Location = new System.Drawing.Point(5, 127);
            this.gbSearchOption.Name = "gbSearchOption";
            this.gbSearchOption.Size = new System.Drawing.Size(457, 116);
            this.gbSearchOption.TabIndex = 29;
            this.gbSearchOption.TabStop = false;
            this.gbSearchOption.Text = "Serarch Text";
            // 
            // tbSQLFunction
            // 
            this.tbSQLFunction.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbSQLFunction.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbSQLFunction.Location = new System.Drawing.Point(3, 77);
            this.tbSQLFunction.Name = "tbSQLFunction";
            this.tbSQLFunction.Size = new System.Drawing.Size(216, 24);
            this.tbSQLFunction.TabIndex = 25;
            // 
            // tbTableName
            // 
            this.tbTableName.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbTableName.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbTableName.Location = new System.Drawing.Point(3, 33);
            this.tbTableName.Name = "tbTableName";
            this.tbTableName.Size = new System.Drawing.Size(216, 24);
            this.tbTableName.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Table name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 16);
            this.label10.TabIndex = 24;
            this.label10.Text = "SQL Function Used";
            // 
            // tbColumnName
            // 
            this.tbColumnName.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbColumnName.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbColumnName.Location = new System.Drawing.Point(226, 33);
            this.tbColumnName.Name = "tbColumnName";
            this.tbColumnName.Size = new System.Drawing.Size(216, 24);
            this.tbColumnName.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(223, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Text inside procedure";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Column name";
            // 
            // tbFreeText
            // 
            this.tbFreeText.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbFreeText.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbFreeText.Location = new System.Drawing.Point(226, 77);
            this.tbFreeText.Name = "tbFreeText";
            this.tbFreeText.Size = new System.Drawing.Size(216, 24);
            this.tbFreeText.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbParameterName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rbIsNulable);
            this.groupBox1.Controls.Add(this.rbIsReadOnly);
            this.groupBox1.Controls.Add(this.cblParamDataType);
            this.groupBox1.Controls.Add(this.rbIsOutput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 115);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Parameter";
            // 
            // tbParameterName
            // 
            this.tbParameterName.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbParameterName.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.tbParameterName.Location = new System.Drawing.Point(6, 34);
            this.tbParameterName.Name = "tbParameterName";
            this.tbParameterName.Size = new System.Drawing.Size(186, 24);
            this.tbParameterName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Parameter name";
            // 
            // rbIsNulable
            // 
            this.rbIsNulable.AutoSize = true;
            this.rbIsNulable.Location = new System.Drawing.Point(198, 28);
            this.rbIsNulable.Name = "rbIsNulable";
            this.rbIsNulable.Size = new System.Drawing.Size(86, 20);
            this.rbIsNulable.TabIndex = 28;
            this.rbIsNulable.TabStop = true;
            this.rbIsNulable.Text = "Is Nullable";
            this.rbIsNulable.UseVisualStyleBackColor = true;
            this.rbIsNulable.CheckedChanged += new System.EventHandler(this.rbIsNullable_CheckedChanged);
            // 
            // rbIsReadOnly
            // 
            this.rbIsReadOnly.AutoSize = true;
            this.rbIsReadOnly.Location = new System.Drawing.Point(198, 80);
            this.rbIsReadOnly.Name = "rbIsReadOnly";
            this.rbIsReadOnly.Size = new System.Drawing.Size(101, 20);
            this.rbIsReadOnly.TabIndex = 27;
            this.rbIsReadOnly.TabStop = true;
            this.rbIsReadOnly.Text = "Is Read Only";
            this.rbIsReadOnly.UseVisualStyleBackColor = true;
            this.rbIsReadOnly.CheckedChanged += new System.EventHandler(this.rbIsReadOnly_CheckedChanged);
            // 
            // cblParamDataType
            // 
            this.cblParamDataType.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.cblParamDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cblParamDataType.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.cblParamDataType.FormattingEnabled = true;
            this.cblParamDataType.Location = new System.Drawing.Point(9, 76);
            this.cblParamDataType.Name = "cblParamDataType";
            this.cblParamDataType.Size = new System.Drawing.Size(183, 24);
            this.cblParamDataType.TabIndex = 0;
            this.cblParamDataType.SelectedIndexChanged += new System.EventHandler(this.cblParamDataType_SelectedIndexChanged);
            // 
            // rbIsOutput
            // 
            this.rbIsOutput.AutoSize = true;
            this.rbIsOutput.Location = new System.Drawing.Point(198, 54);
            this.rbIsOutput.Name = "rbIsOutput";
            this.rbIsOutput.Size = new System.Drawing.Size(79, 20);
            this.rbIsOutput.TabIndex = 26;
            this.rbIsOutput.TabStop = true;
            this.rbIsOutput.Text = "Is Output";
            this.rbIsOutput.UseVisualStyleBackColor = true;
            this.rbIsOutput.CheckedChanged += new System.EventHandler(this.rbIsOutput_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Parameter Data type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.label7.Location = new System.Drawing.Point(11, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "Found SQL Procedures";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.label6.Location = new System.Drawing.Point(716, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Procedure Text";
            // 
            // btFindProcs
            // 
            this.btFindProcs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btFindProcs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFindProcs.Location = new System.Drawing.Point(636, 18);
            this.btFindProcs.Name = "btFindProcs";
            this.btFindProcs.Size = new System.Drawing.Size(75, 27);
            this.btFindProcs.TabIndex = 11;
            this.btFindProcs.Text = "Find Procs";
            this.btFindProcs.UseVisualStyleBackColor = true;
            this.btFindProcs.Click += new System.EventHandler(this.btFindProcs_Click);
            // 
            // rtbProcText
            // 
            this.rtbProcText.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rtbProcText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbProcText.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.rtbProcText.Location = new System.Drawing.Point(716, 24);
            this.rtbProcText.Margin = new System.Windows.Forms.Padding(2);
            this.rtbProcText.Name = "rtbProcText";
            this.rtbProcText.Size = new System.Drawing.Size(399, 733);
            this.rtbProcText.TabIndex = 15;
            this.rtbProcText.Text = "";
            // 
            // dtgvResult
            // 
            this.dtgvResult.AllowUserToAddRows = false;
            this.dtgvResult.AllowUserToDeleteRows = false;
            this.dtgvResult.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.MediumTurquoise;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvResult.Location = new System.Drawing.Point(5, 265);
            this.dtgvResult.Name = "dtgvResult";
            this.dtgvResult.ReadOnly = true;
            this.dtgvResult.Size = new System.Drawing.Size(705, 488);
            this.dtgvResult.TabIndex = 14;
            this.dtgvResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvResult_CellClick);
            // 
            // SQLFormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1147, 812);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1163, 851);
            this.MinimumSize = new System.Drawing.Size(1163, 851);
            this.Name = "SQLFormatForm";
            this.Text = "SQLFormatForm";
            this.Load += new System.EventHandler(this.SQLFormatForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbSearchOption.ResumeLayout(false);
            this.gbSearchOption.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbInputSQL;
        private System.Windows.Forms.RichTextBox rtbOutSQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbProcText;
        private System.Windows.Forms.DataGridView dtgvResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btFindProcs;
        private System.Windows.Forms.TextBox tbTableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbParameterName;
        private System.Windows.Forms.ComboBox cblParamDataType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btParseQuery;
        private System.Windows.Forms.Button btFormat;
        private System.Windows.Forms.GroupBox gbFormatSettings;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbFreeText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbColumnName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbSQLFunction;
        private System.Windows.Forms.RadioButton rbIsOutput;
        private System.Windows.Forms.RadioButton rbIsReadOnly;
        private System.Windows.Forms.RadioButton rbIsNulable;
        private System.Windows.Forms.GroupBox gbSearchOption;
        private System.Windows.Forms.RadioButton rbAndLogic;
        private System.Windows.Forms.RadioButton rbORLocgic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btClearFilters;
        private System.Windows.Forms.Button btClearSPtext;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}