using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSSQLProcGeneratorByKmeta.Forms
{
    public partial class SQLFormatForm : Form
    {
        bool drawFromFile = false;
        string currentDBName = string.Empty;
        DataTable procsTable = new DataTable();
        DataColumn procName = new DataColumn("ProcedureName", System.Type.GetType("System.String"));
        DataColumn procChecks = new DataColumn("ProcedureChecks", System.Type.GetType("System.String"));
        DataColumn procParams = new DataColumn("ProcedureParameters", System.Type.GetType("System.String"));
        DataColumn procText = new DataColumn("ProcedureText", System.Type.GetType("System.String"));

        Database currentDB = null;
        SqlDataType currentParamDataType;
        SearchLogic currentSearchLogic;
        bool isOutputParam = false, isNullablePram = false, isReadOnlyParam = false;
        public enum SearchLogic
        {
            AND
           , OR
        }

        public SQLFormatForm(bool loadFormatConfigFromFile, string selectedDb)
        {
            InitializeComponent();
            drawFromFile = loadFormatConfigFromFile;
            currentDBName = selectedDb;
            procsTable.Columns.Add(procName);
            procsTable.Columns.Add(procChecks);
            procsTable.Columns.Add(procParams);
            procsTable.Columns.Add(procText);

        }

        /// <summary>
        /// get the Proc data table  for matching procs
        /// </summary>
        /// <param name="pramName">param name</param>
        /// <param name="paramType">param sql datatype</param>
        /// <param name="isOutput">Bool is the param output</param>
        /// <param name="isNulable">Bool is the param nullable</param>
        /// <param name="isReadOnly">Bool is the param readonly</param>
        /// <param name="columnName">Column name text to serarch</param>
        /// <param name="tableName">Table name text to serarch</param>
        /// <param name="freeText">free text to serarch</param>
        /// <param name="sqlFunction">text to serarch for sql func</param>
        /// <param name="searchLogic">enum search locgic And or OR logic</param>
        /// <returns></returns>
        private DataTable GetProcsByParamerterFilter(
                                                     string pramName, SqlDataType paramType, bool isOutput, bool isNulable, bool isReadOnly
                                                    , string columnName, string tableName, string freeText, string sqlFunction, SearchLogic searchLogic
                                                    )
        {
            procsTable.Clear();
            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();
            connStr.Authentication = SqlAuthenticationMethod.SqlPassword;
            connStr.ConnectTimeout = 0;
            connStr.UserID = ConfigurationManager.AppSettings["ServerUser"].ToString();
            connStr.Password = ConfigurationManager.AppSettings["ServerUserPassword"].ToString();
            connStr.DataSource = ConfigurationManager.AppSettings["ServerAddress"].ToString();
            connStr.InitialCatalog = currentDBName;
            connStr.ApplicationName = Application.ProductName + "ver. " + Application.ProductVersion;
            connStr.TrustServerCertificate = true;
            List<KeyValuePair<string, int>> procs = new List<KeyValuePair<string, int>>();
            SqlConnection sqlcon = new SqlConnection(connStr.ConnectionString);
            try
            {
                sqlcon.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT sp.[name] AS[Name],sp.[object_id] AS[ID] FROM sys.all_objects AS sp WHERE sp.[type] = 'P' AND is_ms_shipped = 0";
                command.CommandTimeout = 0;
                command.Connection = sqlcon;
                SqlDataReader sprd = command.ExecuteReader();
                while (sprd.Read())
                {
                    KeyValuePair<string, int> currentPair = new KeyValuePair<string, int>(sprd[0].ToString(), int.Parse(sprd[1].ToString()));
                    procs.Add(currentPair);
                }

                sqlcon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error geting Storer Procedures ID-s\nError message: " + ex.Message);
            }

            ServerConnection srvcon = new ServerConnection(sqlcon);
            Server srv = new Server(srvcon);
            List<KeyValuePair<string, bool>> spTextChecks = new List<KeyValuePair<string, bool>>();
            List<KeyValuePair<string, bool>> spParamChecks = new List<KeyValuePair<string, bool>>();

            var dbs = from Database db in srv.Databases
                      where db.Name == currentDBName
                      select db;
            currentDB = dbs.First();

            for (int i = 0; i < procs.Count; i++)
            {
                StoredProcedure currentProc = currentDB.StoredProcedures.ItemById(procs[i].Value);
                StringBuilder currentProcText = new StringBuilder();
                currentProcText.Append(currentProc.TextBody);
                List<StoredProcedureParameter> spParams = currentProc.Parameters.Cast<StoredProcedureParameter>().ToList();
                List<KeyValuePair<string, bool>> pramChecks = spPrameterChecks(spParams, pramName, paramType, isOutput, isNulable, isReadOnly);
                List<KeyValuePair<string, bool>> textCehcks = sptxtChecks(currentProcText, tableName, columnName, freeText, sqlFunction);
                AddProcToTable(procsTable, pramChecks, textCehcks, currentProc, searchLogic);
            }

            return procsTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentTbl">Data table that will be visualized in the grid</param>
        /// <param name="spPram">list of kvp that hold the parameter checks</param>
        /// <param name="spTxt">list of kvp that hold the text checks</param>
        /// <param name="procedure">StoredProcedure Object</param>
        /// <param name="logic">enum for what logic we need OR or AND</param>
        private void AddProcToTable(DataTable currentTbl, List<KeyValuePair<string, bool>> spPram, List<KeyValuePair<string, bool>> spTxt
                                   , StoredProcedure procedure, SearchLogic logic)
        {
            bool pramChecks = false;
            bool spChecks = false;
            bool currentCheck = false;
            switch (logic)
            {
                case SearchLogic.AND:
                    foreach (KeyValuePair<string, bool> chk in spPram)
                    {
                        currentCheck = chk.Value;
                        if (!currentCheck)
                        {
                            pramChecks = false;
                            break;
                        }
                        else
                        {
                            pramChecks = true;
                        }
                    }
                    currentCheck = false;
                    spChecks = currentCheck;
                    foreach (KeyValuePair<string, bool> chk in spTxt)
                    {
                        currentCheck = chk.Value;
                        if (!currentCheck)
                        {
                            spChecks = false;
                            break;
                        }
                        else
                        {
                            spChecks = true;
                        }
                    }
                    if (spChecks && pramChecks)
                    {
                        DataRow row = currentTbl.NewRow();
                        string paramsstring = string.Join("\r\n", spPram).Replace(',', ' ').Replace('[', ' ').Replace(']', ' ');
                        var spparams = from StoredProcedureParameter prm in procedure.Parameters
                                       select prm.Name + " " + prm.DataType.SqlDataType + " " + prm.DataType.MaximumLength + " " + prm.DefaultValue + "\r\n";
                        string paramsData = string.Join("\r\n", spparams);
                        row["ProcedureName"] = procedure.Name;
                        row["ProcedureChecks"] = paramsstring;
                        row["ProcedureParameters"] = paramsData;
                        row["ProcedureText"] = procedure.TextBody;
                        currentTbl.Rows.Add(row);
                    }
                    break;
                case SearchLogic.OR:
                    foreach (KeyValuePair<string, bool> chk in spPram)
                    {
                        currentCheck = chk.Value;
                        if (!currentCheck)
                        {
                            pramChecks = false;
                        }
                        else
                        {
                            pramChecks = true;
                            break;
                        }
                    }
                    currentCheck = false;
                    spChecks = currentCheck;
                    foreach (KeyValuePair<string, bool> chk in spTxt)
                    {
                        currentCheck = chk.Value;
                        if (!currentCheck)
                        {
                            spChecks = false;
                        }
                        else
                        {
                            spChecks = true;
                            break;
                        }
                    }
                    if (spChecks || pramChecks)
                    {
                        DataRow row = currentTbl.NewRow();
                        string paramsstring = string.Join("\r\n", spPram).Replace(',', ' ').Replace('[', ' ').Replace(']', ' ');
                        var spparams = from StoredProcedureParameter prm in procedure.Parameters
                                       select prm.Name + " " + prm.DataType.SqlDataType + " " + prm.DataType.MaximumLength + " " + prm.DefaultValue + "\r\n";
                        string paramsData = string.Join("\r\n", spparams);
                        row["ProcedureName"] = procedure.Name;
                        row["ProcedureChecks"] = paramsstring;
                        row["ProcedureParameters"] = paramsData;
                        row["ProcedureText"] = procedure.TextBody;
                        currentTbl.Rows.Add(row);
                    }
                    break;
            }
        }
        /// <summary>
        /// List ot bool values on checks for the SP
        /// </summary>
        /// <param name="procText">Stored Procedure text</param>
        /// <param name="tblName">Table Name to check</param>
        /// <param name="colName">Column Name to check</param>
        /// <param name="freetxt">Free text to check</param>
        /// <param name="sqlFunc">SQL FUNC Name to check</param>
        /// <returns></returns>
        private List<KeyValuePair<string, bool>> sptxtChecks(StringBuilder procText, string tblName, string colName, string freetxt, string sqlFunc)
        {
            List<KeyValuePair<string, bool>> spTxt = new List<KeyValuePair<string, bool>>();
            if (procText.ToString().Contains(tblName) && !tblName.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("tableExists", true);
                spTxt.Add(currentPair);
            }
            else if (!procText.ToString().Contains(tblName) && !tblName.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("tableExists", false);
                spTxt.Add(currentPair);
            }
            if (procText.ToString().Contains(colName) && !colName.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("columnExists", true);
                spTxt.Add(currentPair);
            }
            else if (!procText.ToString().Contains(colName) && !colName.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("columnExists", false);
                spTxt.Add(currentPair);
            }
            if (procText.ToString().Contains(freetxt) && !freetxt.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("freeTextExists", true);
                spTxt.Add(currentPair);
            }
            else if (!procText.ToString().Contains(freetxt) && !freetxt.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("freeTextExists", false);
                spTxt.Add(currentPair);
            }
            if (procText.ToString().Contains(sqlFunc) && !sqlFunc.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("SQLFuncExists", true);
                spTxt.Add(currentPair);
            }
            else if (!procText.ToString().Contains(sqlFunc) && !sqlFunc.Equals(string.Empty))
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("SQLFuncExists", false);
                spTxt.Add(currentPair);
            }
            return spTxt;
        }

        /// <summary>
        /// Method that returns boolean list of key value pairs for each check 
        /// based on what have found matches 
        /// </summary>
        /// <param name="spPrams">List of Type StoredProcedureParameter</param>
        /// <param name="paramName">SP Parameter Name</param>
        /// <param name="paramType">SP Prameter SqlDataType</param>
        /// <param name="isOutput">SP Parameter Is it Output</param>
        /// <param name="isNulable">SP Parameter Is = NULL</param>
        /// <param name="isReadOnly">SP Parameter Is it ReadOnly</param>
        /// <returns>List KeyValuePair string,bool  containing all the information about the requested checks</returns>
        private List<KeyValuePair<string, bool>> spPrameterChecks(List<StoredProcedureParameter> spPrams, string parName, SqlDataType parType
                                         , bool parisOutput, bool parisNulable, bool parisReadOnly)
        {
            List<KeyValuePair<string, bool>> spPramChecks = new List<KeyValuePair<string, bool>>();
            foreach (StoredProcedureParameter spParam in spPrams)
            {
                //if we want to check by param Type
                if (parType != SqlDataType.None)
                {
                    if (spParam.DataType.SqlDataType == parType)
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramType", true);
                        spPramChecks.Add(currentPair);
                    }
                    else if (parType != SqlDataType.None)
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramType", false);
                        spPramChecks.Add(currentPair);
                    }
                }
                //if we want to check by param Name
                if (parName != string.Empty)
                {
                    parName = "@" + parName;
                    if (spParam.Name.ToUpper() == parName.ToUpper())
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramName", true);
                        spPramChecks.Add(currentPair);
                    }
                    else
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramName", false);
                        spPramChecks.Add(currentPair);
                    }
                }
                //if we want to check by Otput param 
                if (parisOutput)
                {
                    if (spParam.IsOutputParameter)
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramisOutput", true);
                        spPramChecks.Add(currentPair);
                    }
                    else
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramisOutput", false);
                        spPramChecks.Add(currentPair);
                    }
                }
                //if we want to check by nullable param 
                if (parisNulable)
                {
                    if (spParam.DefaultValue.Equals("NULL"))
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramisNulable", true);
                        spPramChecks.Add(currentPair);
                    }
                    else
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramisNulable", false);
                        spPramChecks.Add(currentPair);
                    }
                }
                //if we want to check by ReadOnly param 
                if (parisReadOnly)
                {
                    if (spParam.IsReadOnly)
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramisReadOnly", true);
                        spPramChecks.Add(currentPair);
                    }
                    else
                    {
                        KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>("paramisReadOnly", false);
                        spPramChecks.Add(currentPair);
                    }
                }
            }
            return spPramChecks;
        }
        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Mehtod that fills the Gorup Box with Controls based on the settings in the Custom Format File Configuration
        /// </summary>
        /// <param name="confFilePath">Path CustomFormatConfiguration.xlsx from wich the text boxex will be loaded</param>
        /// <returns></returns>
        private bool fillGroupBox(string confFilePath)
        {
            DataTable formatConfiguration = ReadExcelFile.getExcellToDtbl(confFilePath);
            Point previousPoint = new Point(6, 0);
            Point firstRow = new Point(6, 0);
            int maxY = 407;
            int controlHeight = 25;
            int currentCol = 1;
            int columnMaxLen = 0;
            foreach (DataRow dr in formatConfiguration.Rows)
            {
                if (dr[3].ToString().ToUpper() == "TRUE OR FALSE")
                {
                    CheckBox myCb = new CheckBox();
                    myCb.Name = dr[1].ToString();
                    myCb.Text = dr[1].ToString();
                    myCb.BackColor = Color.Black;
                    myCb.ForeColor = Color.MediumTurquoise;

                    myCb.AutoSize = true;
                    if (((previousPoint.Y + controlHeight - 5) < maxY) && currentCol == 1)
                    {
                        previousPoint.X = previousPoint.X;
                        previousPoint.Y = previousPoint.Y + controlHeight;
                        myCb.Location = previousPoint;
                    }
                    else
                    {
                        previousPoint.X = firstRow.X + currentCol * 10 + 260;
                        previousPoint.Y = firstRow.Y + controlHeight;
                        myCb.Location = previousPoint;
                    }
                    if (drawFromFile)
                    {
                        if (int.Parse(dr[0].ToString()) == 1)
                        {
                            myCb.Checked = true;
                        }
                        else
                        {
                            myCb.Checked = false;
                        }
                    }
                    this.gbFormatSettings.Controls.Add(myCb);
                }
                else if (dr[3].ToString().ToUpper() == "SqlVersion".ToUpper() || dr[3].ToString().ToUpper() == "KeywordCasing".ToUpper())
                {
                    Label myComboLable = new Label();
                    myComboLable.Text = dr[1].ToString();
                    ComboBox myCombo = new ComboBox();
                    myCombo.Name = dr[1].ToString();
                    myCombo.AutoSize = true;
                    myCombo.DropDownStyle = ComboBoxStyle.DropDownList;
                    myCombo.BackColor = Color.Black;
                    myCombo.ForeColor = Color.MediumTurquoise;

                    if (dr[3].ToString().ToUpper() == "SqlVersion".ToUpper())
                    {
                        myCombo.Items.Add("SQL 2005");
                        myCombo.Items.Add("SQL 2000");
                        myCombo.Items.Add("SQL 2008");
                        myCombo.Items.Add("SQL 2012");
                        myCombo.Items.Add("SQL 2014");
                        myCombo.Items.Add("SQL 2016");
                    }
                    else
                    {
                        myCombo.Items.Add("Lowercase");
                        myCombo.Items.Add("Uppercase");
                        myCombo.Items.Add("PascalCase");
                    }
                    if (drawFromFile)
                    {
                        myCombo.SelectedIndex = int.Parse(dr[0].ToString());
                    }

                    if (((previousPoint.Y + controlHeight - 5) < maxY) && currentCol == 1)
                    {
                        previousPoint.X = previousPoint.X;
                        previousPoint.Y = previousPoint.Y + controlHeight;
                        myComboLable.Location = previousPoint;
                        myCombo.Location = new Point(previousPoint.X, previousPoint.Y + 25);
                        previousPoint.Y = previousPoint.Y + 25;
                    }
                    else
                    {
                        previousPoint.X = firstRow.X + currentCol * 10 + 260;
                        previousPoint.Y = firstRow.Y + controlHeight;
                        myComboLable.Location = previousPoint;
                        myCombo.Location = new Point(previousPoint.X, previousPoint.Y + 25);
                        previousPoint.Y = firstRow.Y + 25;
                    }
                    this.gbFormatSettings.Controls.Add(myComboLable);
                    this.gbFormatSettings.Controls.Add(myCombo);
                }
                else if (dr[3].ToString().ToUpper() == "Indent".ToUpper())
                {
                    Label myTextLable = new Label();
                    myTextLable.Text = dr[1].ToString();
                    TextBox mytext = new TextBox();
                    mytext.Name = dr[1].ToString();
                    mytext.BackColor = Color.Black;
                    mytext.ForeColor = Color.MediumTurquoise;

                    if (drawFromFile)
                    {
                        mytext.Text = dr[0].ToString();
                    }
                    mytext.AutoSize = true;
                    if (((previousPoint.Y + controlHeight - 5) < maxY) && currentCol == 1)
                    {
                        previousPoint.X = previousPoint.X;
                        previousPoint.Y = previousPoint.Y + controlHeight;
                        myTextLable.Location = previousPoint;
                        mytext.Location = new Point(previousPoint.X, previousPoint.Y + 25);
                        previousPoint.Y = previousPoint.Y + 25;
                    }
                    else
                    {
                        previousPoint.X = firstRow.X + currentCol * 10 + 260;
                        previousPoint.Y = firstRow.Y + controlHeight;
                        myTextLable.Location = previousPoint;
                        mytext.Location = new Point(previousPoint.X, previousPoint.Y + 25);
                        previousPoint.Y = firstRow.Y + 25;
                    }
                    this.gbFormatSettings.Controls.Add(myTextLable);
                    this.gbFormatSettings.Controls.Add(mytext);
                }
            }
            System.Diagnostics.Debug.WriteLine(columnMaxLen);
            return true;
        }
        /// <summary>
        /// Botton click Format Sql Script based on the interace selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btFormat_Click(object sender, EventArgs e)
        {
            if (rtbInputSQL.Text != string.Empty)
            {
                SQLFormater myfromater = new SQLFormater();
                rtbOutSQL.Text = myfromater.SQLScriptFormater(rtbInputSQL.Text, true, false, "");
            }
            else
            {
                MessageBox.Show("Please fill the input strings textbox");
            }
        }
        /// <summary>
        /// Button click parse query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btParseQuery_Click(object sender, EventArgs e)
        {
            if (rtbInputSQL.Text != string.Empty)
            {
                SQLFormater myfromater = new SQLFormater();
                string testparse = myfromater.SQLScriptFormater(rtbInputSQL.Text, true);
                MessageBox.Show("Passed " + (testparse == "true" ? "successfully" : " with errors.\n Look in the lower text box."));
                rtbOutSQL.Text = (testparse == "true" ? "" : testparse);
            }
            else
            {
                MessageBox.Show("Please fill the input strings textbox");
            }
        }
        /// <summary>
        /// Load the formating form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SQLFormatForm_Load(object sender, EventArgs e)
        {
            fillGroupBox(AppDomain.CurrentDomain.BaseDirectory + @"\Configurations\CustomFormatConfiguration.xlsx");
            fillSqlDataTypeCombo(cblParamDataType);
        }
        private void fillSqlDataTypeCombo(ComboBox dataTypeCombo)
        {
            // dataTypeCombo.Items.Add(SqlDataType.)
            foreach (SqlDataType sqdt in Enum.GetValues(typeof(SqlDataType)))
            {
                dataTypeCombo.Items.Add(sqdt);
            }
        }
        /// <summary>
        /// Set the bool value of isOutputParam based on the selection
        /// of the radio box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbIsOutput_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton myrb = sender as RadioButton;
            if (myrb.Checked)
            {
                isOutputParam = true;
                isReadOnlyParam = false;
                isNullablePram = false;
            }
        }
        /// <summary>
        /// Set the bool value of isReadOnlyParam based on the selection
        /// of the radio box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbIsReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton myrb = sender as RadioButton;
            if (myrb.Checked)
            {
                isReadOnlyParam = true;
                isNullablePram = false;
                isOutputParam = false;
            }
        }
        /// <summary>
        /// Set the bool value of isNullablePram based on the selection
        /// of the radio box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbIsNullable_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton myrb = sender as RadioButton;
            if (myrb.Checked)
            {
                isNullablePram = true;
                isOutputParam = false;
                isReadOnlyParam = false;
            }
        }
        /// <summary>
        /// Set the SqlDataType for param based on the selection in combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cblParamDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox myCombo = sender as ComboBox;
            if (myCombo.SelectedIndex != -1)
            {
                Enum.TryParse(myCombo.SelectedItem.ToString(), out currentParamDataType);
            }
            else
            {
                currentParamDataType = 0;
            }
        }
        /// <summary>
        /// set the enum for logic based on radio selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbORLocgic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbORLocgic.Checked)
            {
                currentSearchLogic = SearchLogic.OR;
            }
            else
            {
                currentSearchLogic = SearchLogic.AND;
            }
        }
        /// <summary>
        /// set the enum for logic based on radio selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAndLogic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAndLogic.Checked)
            {
                currentSearchLogic = SearchLogic.AND;
            }
            else
            {
                currentSearchLogic = SearchLogic.OR;
            }
        }
        /// <summary>
        /// Double click on proc text sets the richtb the text of the proc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex >= 2) && (e.RowIndex > 0))
            {
                rtbProcText.Text = dtgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }
        private void btClearSPtext_Click(object sender, EventArgs e)
        {
            rtbProcText.Text = string.Empty;
        }
        private void btClearFilters_Click(object sender, EventArgs e)
        {
            tbColumnName.Text = string.Empty;
            tbFreeText.Text = string.Empty;
            tbParameterName.Text = string.Empty;
            tbSQLFunction.Text = string.Empty;
            tbTableName.Text = string.Empty;
            cblParamDataType.SelectedIndex = -1;
            rbAndLogic.Checked = false;
            rbORLocgic.Checked = false;
            rbIsReadOnly.Checked = false;
            rbIsOutput.Checked = false;
            rbIsNulable.Checked = false;
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void gbFormatSettings_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Find procs and fill the Data grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btFindProcs_Click(object sender, EventArgs e)
        {

            dtgvResult.DataSource = GetProcsByParamerterFilter(tbParameterName.Text, currentParamDataType, isOutputParam, isNullablePram, isReadOnlyParam
                                      , tbColumnName.Text, tbTableName.Text, tbFreeText.Text, tbSQLFunction.Text, currentSearchLogic);
            dtgvResult.Columns[1].Visible = false;
            dtgvResult.Columns[0].Width = 350;
            dtgvResult.Columns[2].Width = 150;
            dtgvResult.Columns[3].Width = 150;
        }
    }
}
