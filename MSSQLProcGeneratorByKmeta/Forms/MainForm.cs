using MSSQLProcGeneratorByKmeta.GenerationClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace MSSQLProcGeneratorByKmeta.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }
        private SQLConn sqlConn = new SQLConn();
        private string serverAddress = string.Empty;
        private string serverUser = string.Empty;
        private string serverUserPassword = string.Empty;
        private string currentDB = string.Empty;
        private bool outputAll = false, customColumns = false;
        private string configurationFolderPath = System.Environment.CurrentDirectory + @"\Configurations\";
        private string configurationSQLPath = System.Environment.CurrentDirectory + @"\Configurations\SQLTemplates\";
        private const string templateConfigFileName = "TemplateConfiguration.xlsx";
        private const string customFormatFileName = "CustomFormatConfiguration.xlsx";
        private const string customColumnMappingFileName = "CustomColumsMappings.xlsx";
        private const string columnsDataSql = "GetColumnsData.sql";
        private List<Operations> currentAppOperaitons = new List<Operations>();
        private List<string> activeDbs = new List<string>();
        private DatabaseData dbData = new DatabaseData();
        private DataTable procGenData = new DataTable();

        /// <summary>
        /// Main Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "MSSQL Procedures Generator By Kmet@ Version " + typeof(MainForm).Assembly.GetName().Version;
            this.btSQLFormat.Enabled = false;

            try
            {
                currentAppOperaitons = GetOperationFromConfig(configurationFolderPath + templateConfigFileName);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n" + ex.FileName);
                Application.Exit();
            }
            try
            {
                if (!File.Exists(configurationFolderPath + customColumnMappingFileName))
                {
                    throw new FileNotFoundException("File not found in App directory" + configurationFolderPath, customColumnMappingFileName);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n" + ex.FileName);
                Application.Exit();
            }
            try
            {
                if (!File.Exists(configurationFolderPath + customFormatFileName))
                {
                    throw new FileNotFoundException("File not found in App directory" + configurationFolderPath, customFormatFileName);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n" + ex.FileName);
                Application.Exit();
            }
            GetSettings();

            if (sqlConn.ServerConnectionCeck(serverAddress, serverUser, serverUserPassword))
            {
                activeDbs = sqlConn.LoadDataBases(serverAddress, serverUser, serverUserPassword);
                foreach (string db in activeDbs)
                {
                    comboDbName.Items.Add(db);
                }
            }
        }

        /// <summary>
        /// Read settings from the configuration file
        /// </summary>
        private void GetSettings()
        {
            serverAddress = ConfigurationManager.AppSettings["ServerAddress"].ToString().Trim();
            serverUser = ConfigurationManager.AppSettings["ServerUser"].ToString().Trim();
            serverUserPassword = ConfigurationManager.AppSettings["ServerUserPassword"].ToString().Trim();
        }

        /// <summary>
        /// Returns all available operations with their sql templates
        /// </summary>
        /// <param name="confiFilePath">path to the excel configuration file for operations and templates</param>
        /// <returns></returns>
        private List<Operations> GetOperationFromConfig(string confiFilePath)
        {
            List<Operations> curentOpers = new List<Operations>();
            if (!File.Exists(configurationFolderPath + templateConfigFileName))
            {
                throw new FileNotFoundException("File not found in App Configuration directory" + configurationFolderPath, templateConfigFileName);
            }

            DataTable currentData = ReadExcelFile.getExcellToDtbl(configurationFolderPath + templateConfigFileName);
            for (int i = 0; i < currentData.Rows.Count; i++)
            {
                if (!File.Exists(configurationSQLPath + currentData.Rows[i][0].ToString()))
                {
                    throw new FileNotFoundException("File not found in App SQL directory" + configurationSQLPath, currentData.Rows[i][0].ToString());
                }

                Operations currentOper = new Operations(
                                                         currentData.Rows[i][0].ToString()
                                                       , (TableData.Operation)Enum.Parse(typeof(TableData.Operation), currentData.Rows[i][1].ToString())
                                                       , currentData.Rows[i][2].ToString()
                                                       , currentData.Rows[i][3].ToString()
                                                       );
                curentOpers.Add(currentOper);
            }
            return curentOpers;
        }

        /// <summary>
        /// Generates all procedures in the selected folder. Creates folder with DB name and sub folders with Operation Name      
        private void ParseProcs(DatabaseData dbs, bool returnAllData, List<Operations> operations, string destinationDir, string creator, bool useSpecificDates)
        {
            string finalDir = destinationDir + "\\" + dbs.DatabaseName;
            // SQLFormater sqlFormat = new SQLFormater();
            //To do implement setting that uses the SQLFormater to format every file based on the format settings 

            if (!Directory.Exists(finalDir))
            {
                Directory.CreateDirectory(finalDir);
            }

            foreach (Operations currentOperation in operations)
            {
                TableData.Operation oper = currentOperation.templateOperation;
                string temlateSQLScriptPath = "";
                temlateSQLScriptPath = configurationSQLPath + currentOperation.sqlTemplatefile;

                switch (oper)
                {
                    case TableData.Operation.Insert:
                        OperationsManage.OperationInsert(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.Update:
                        OperationsManage.OperationUpdate(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.Delete:
                        OperationsManage.OperationDelete(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.GetAll:
                        OperationsManage.OperationGetAll(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.GetByID:
                        OperationsManage.OperationGetByID(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.GetByFilter:
                        OperationsManage.OperationGetByFilter(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.GetByFK:
                        OperationsManage.OperationGetByFK(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.NonClusteredCoveringIndexesTables:
                        OperationsManage.OperationNonClusteredCoveringIndexesBridgeTables(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.NonClusteredIndexesFKAllTables:
                        OperationsManage.OperationNonClusteredIndexesFKAllTables(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.NonClusteredIndexesFKAllColumnsAllTables:
                        OperationsManage.OperationNonClusteredIndexesFKAllColumnsAllTables(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                    case TableData.Operation.UniqueIndexFK:
                        OperationsManage.OperationUniqueIndexFK(dbs, currentOperation.operationNameTamplate, currentOperation.fileNameTemplate, returnAllData, creator, tbDateFormat.Text, useSpecificDates, currentOperation.templateOperation, temlateSQLScriptPath, finalDir);
                        break;
                }
            }
            MessageBox.Show("Procedures Generated!");
        }
        /// <summary>
        /// Fill  the Database Data based on Information that`s extracted of SQL Server Selected Database
        /// in the Interface of the app
        /// </summary>
        /// <param name="customColumnsFileName"> Full path file for CustomMappings.xlsx file </param>
        private void GetDBData(string customColumnsFileName)
        {
            dbData.DatabaseName = currentDB;
            List<ManagedColumns> customInsertColumns = new List<ManagedColumns>();
            List<ManagedColumns> customUpdateColumns = new List<ManagedColumns>();
            List<ManagedColumns> customSelectColumns = new List<ManagedColumns>();

            if (File.Exists(customColumnsFileName) && customColumns)
            {
                DataTable configTbl = ReadExcelFile.getExcellToDtbl(customColumnsFileName);

                for (int i = 0; i < configTbl.Rows.Count; i++)
                {
                    if (configTbl.Rows[i][0].ToString() != string.Empty)
                    {
                        ManagedColumns managedInsertColumns = new ManagedColumns();
                        managedInsertColumns.ColumnName = configTbl.Rows[i][0].ToString();
                        managedInsertColumns.ColumnValue = configTbl.Rows[i][1].ToString();
                        managedInsertColumns.UsedForParam = Convert.ToBoolean(int.Parse(configTbl.Rows[i][2].ToString()));
                        managedInsertColumns.UsedForColumn = Convert.ToBoolean(int.Parse(configTbl.Rows[i][3].ToString()));
                        customInsertColumns.Add(managedInsertColumns);
                        ManagedColumns managedUpdateColumns = new ManagedColumns();
                        managedUpdateColumns.ColumnName = configTbl.Rows[i][0].ToString();
                        managedUpdateColumns.ColumnValue = configTbl.Rows[i][1].ToString();
                        managedUpdateColumns.UsedForParam = Convert.ToBoolean(int.Parse(configTbl.Rows[i][4].ToString()));
                        managedUpdateColumns.UsedForColumn = Convert.ToBoolean(int.Parse(configTbl.Rows[i][5].ToString()));
                        customUpdateColumns.Add(managedUpdateColumns);
                        ManagedColumns managedSelectColumns = new ManagedColumns();
                        managedSelectColumns.ColumnName = configTbl.Rows[i][0].ToString();
                        managedSelectColumns.ColumnValue = configTbl.Rows[i][1].ToString();
                        managedSelectColumns.UsedForParam = Convert.ToBoolean(int.Parse(configTbl.Rows[i][6].ToString()));
                        managedSelectColumns.UsedForColumn = Convert.ToBoolean(int.Parse(configTbl.Rows[i][7].ToString()));
                        customSelectColumns.Add(managedSelectColumns);
                    }
                }
                dbData.ManagedInsertColumns = customInsertColumns;
                dbData.ManagedUpdateColumns = customUpdateColumns;
                dbData.ManagedSelectColumns = customSelectColumns;
            }
            else if (customColumns)
            {
                MessageBox.Show("File CustomColumsMappings.xlsx does not exist!\nNot found in the Application Configuration dir!");
            }

            int startvalue = 1;
            //1 RANK 2 tableName, 3 columnname, 4 primarikey chek, 5 columndatatype , 6 null , 7 IsFK
            procGenData = sqlConn.GetProcTablesData(serverAddress, serverUser, serverUserPassword, currentDB, configurationFolderPath, columnsDataSql);
            string currentPrimaryKey = string.Empty;
            string currentPrimaryKeyDataType = string.Empty;
            List<ColumnData> currentColumns = new List<ColumnData>();

            for (int i = 0; i < procGenData.Rows.Count; i++)
            {
                if ((startvalue == int.Parse(procGenData.Rows[i][0].ToString()) - 1) || ((i + 1) == procGenData.Rows.Count))
                {
                    TableData currentTbl = new TableData();
                    currentTbl.Columns = currentColumns;
                    currentTbl.TableName = procGenData.Rows[i - 1][1].ToString();
                    currentTbl.PrimaryKey = currentPrimaryKey;
                    currentTbl.PrimaryKeyDataType = currentPrimaryKeyDataType;
                    dbData.Tables.Add(currentTbl);
                    currentColumns = new List<ColumnData>();
                    startvalue = int.Parse(procGenData.Rows[i][0].ToString());
                }

                if (bool.Parse(procGenData.Rows[i][3].ToString()))
                {
                    currentPrimaryKey = procGenData.Rows[i][2].ToString();
                    currentPrimaryKeyDataType = procGenData.Rows[i][4].ToString();
                }

                if (bool.Parse(procGenData.Rows[i][3].ToString()) == false)
                {
                    ColumnData currentColumn = new ColumnData();
                    currentColumn.ColumnName = procGenData.Rows[i][2].ToString();
                    currentColumn.ColumnDataType = procGenData.Rows[i][4].ToString();
                    currentColumn.isNullable = bool.Parse(procGenData.Rows[i][5].ToString());
                    currentColumn.isFK = bool.Parse(procGenData.Rows[i][6].ToString());
                    currentColumns.Add(currentColumn);
                }
            }
        }
    }
}
