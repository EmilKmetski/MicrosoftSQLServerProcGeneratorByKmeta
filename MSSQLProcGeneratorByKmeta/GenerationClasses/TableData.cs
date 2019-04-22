using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSSQLProcGeneratorByKmeta.GenerationClasses
{
    /// <summary>
    ///  Class that holds information about tables and list of ColumnsData Type for all columns
    ///  that exist in the table and Table Name and PK column in table.
    /// </summary>                    
    public class TableData
    {
        public enum Operation
        {
            Insert,
            Update,
            Delete,
            GetAll,
            GetByFilter,
            GetByFK,
            GetByID,
            NonClusteredCoveringIndexesTables,
            NonClusteredIndexesFKAllTables,
            NonClusteredIndexesFKAllColumnsAllTables,
            UniqueIndexFK
        }

        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string PrimaryKeyDataType { get; set; }
        public List<ColumnData> Columns { get; set; }
        public TableData()
        {
            TableName = string.Empty;
            PrimaryKey = string.Empty;
            PrimaryKeyDataType = string.Empty;
            Columns = new List<ColumnData>();
        }
        /// <summary>
        /// Gets Managed Column form the list of Managed Columns based on the name passed
        /// </summary>
        /// <param name="colData">List of ManagedColumns Type</param>
        /// <param name="ColName">Column Name to check if this column is manged and if its to return it</param>
        /// <returns>ManagedColumns</returns>
        public ManagedColumns GetCustomColumnData(List<ManagedColumns> colData, string ColName)
        {
            ManagedColumns customColumnData = new ManagedColumns();
            for (int x = 0; x < colData.Count; x++)
            {
                customColumnData = colData[x];
                if (ColName == customColumnData.ColumnName)
                {
                    break;
                }
            }
            return customColumnData;
        }
        /// <summary>
        /// Generates the Tested Strings Code part string that will be replaced in the template file
        /// </summary>
        /// <param name="returnAllCol">If we need to return all clolumns after update,insert as result</param>
        /// <param name="tb">Table data that holds the columns </param>
        /// <param name="procName">Procedure name</param>
        /// <param name="customColumnsGen">If true will read from the CustomColumsMappings.xlsx file to do appropriate 
        /// columns insert and update</param>
        /// <param name="typeOperation">Type of the Operation to wich this will have to Apply "Insert","Update","Delete" TableData.Operation enum</param>
        /// <param name="managedColumns">All the CustomColumns list from the CustomColumsMappings.xlsx</param>
        /// <returns>StringBuilder</returns>         
        public StringBuilder TestedStrings(bool returnAllCol, TableData tb, string procName, bool customColumnsGen, Operation typeOperation, List<ManagedColumns> managedColumns)
        {
            StringBuilder testeStrings = new StringBuilder();
            switch (typeOperation)
            {
                case Operation.Insert:

                    if (!returnAllCol)
                    {
                        testeStrings.Append("DECLARE @" + tb.PrimaryKey + "    " + tb.PrimaryKeyDataType + "\n");
                    }
                    string testRowdata = "";

                    for (int i = 0; i < tb.Columns.Count(); i++)
                    {
                        if (customColumnsGen)
                        {

                            ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                            if (currentCustomCollumn.UsedForParam)
                            {
                                testRowdata += ", <" + tb.Columns[i].ColumnDataType + ">";
                                continue;
                            }
                            else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                            {
                                continue;
                            }
                            else
                            {
                                testRowdata += ", <" + tb.Columns[i].ColumnDataType + ">";
                            }
                        }
                        else
                        {
                            testRowdata += ", <" + tb.Columns[i].ColumnDataType + ">";
                        }
                    }

                    if (!returnAllCol)
                    {
                        testRowdata += ",@" + tb.PrimaryKey + " OUTPUT \n";
                    }
                    testeStrings.Append("EXEC " + procName + " " + testRowdata.Remove(0, 1) + "\n");
                    if (!returnAllCol)
                    {
                        testeStrings.Append("SELECT @" + tb.PrimaryKey + "\n");
                    }
                    break;
                case Operation.Update:
                    string testRowdataupd = "<" + tb.PrimaryKeyDataType + ">";
                    for (int i = 0; i < tb.Columns.Count(); i++)
                    {
                        if (customColumnsGen)
                        {
                            ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                            if (currentCustomCollumn.UsedForParam)
                            {
                                testRowdataupd += ", <" + tb.Columns[i].ColumnDataType + ">";
                                continue;
                            }
                            else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                            {
                                continue;
                            }
                            else
                            {
                                testRowdataupd += ", <" + tb.Columns[i].ColumnDataType + ">";
                            }
                        }
                        else
                        {
                            testRowdataupd += ", <" + tb.Columns[i].ColumnDataType + ">";
                        }
                    }
                    testeStrings.Append("EXEC " + procName + " " + testRowdataupd + "\n");
                    break;
                case Operation.Delete:
                    testeStrings.Append("EXEC " + procName + " " + "<" + tb.PrimaryKeyDataType + ">" + "\n");
                    break;
                case Operation.GetAll:
                    testeStrings.Append("EXEC " + procName + " \n");
                    break;
            }
            return testeStrings;
        }

        /// <summary>
        /// Generates the Parameters Code part string that will be replaced in the template file
        /// </summary>
        /// <param name="returnAllCol">If we need to return all clolumns after update,insert as result</param>
        /// <param name="tb">Table data that holds the columns </param>
        /// <param name="procName">Procedure name</param>
        /// <param name="customColumnsGen">If true will read from the CustomColumsMappings.xlsx file to do appropriate 
        /// columns insert and update</param>
        /// <param name="typeOperation">Type of the Operation to wich this will have to Apply "Insert","Update","Delete" TableData.Operation enum</param>
        /// <param name="managedColumns">All the CustomColumns list from the CustomColumsMappings.xlsx</param>
        /// <returns>StringBuilder</returns>  
        public StringBuilder ParameterStrings(bool returnAllCol, TableData tb, string procName, bool customColumnsGen, Operation typeOperation, List<ManagedColumns> managedColumns)
        {
            StringBuilder parameters = new StringBuilder();
            switch (typeOperation)
            {
                case Operation.Insert:
                    OperationInsertParams(ref parameters, returnAllCol, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.Update:
                    OperationUpdateParams(ref parameters, returnAllCol, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.Delete:
                    OperationDeleteParams(ref parameters, tb);
                    break;
                case Operation.GetAll:
                    parameters.Append("");
                    break;
                case Operation.GetByID:
                    OperationGetByIDParams(ref parameters, tb);
                    break;
                case Operation.GetByFilter:
                    OperationGetByFilterParams(ref parameters, returnAllCol, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.GetByFK:
                    OperationGetByFKParams(ref parameters, returnAllCol, customColumnsGen, tb, managedColumns);
                    break;
            }
            return parameters;
        }
        /// <summary>
        /// Generates the Script Code part string that will be replaced in the template file
        /// </summary>
        /// <param name="returnAllCol">If we need to return all clolumns after update,insert as result</param>
        /// <param name="tb">Table data that holds the columns </param>
        /// <param name="procName">Procedure name</param>
        /// <param name="customColumnsGen">If true will read from the CustomColumsMappings.xlsx file to do appropriate 
        /// columns insert and update</param>
        /// <param name="typeOperation">Type of the Operation to wich this will have to Apply "Insert","Update","Delete" TableData.Operation enum</param>
        /// <param name="managedColumns">All the CustomColumns list from the CustomColumsMappings.xlsx</param>
        /// <returns>StringBuilder</returns>                          
        public StringBuilder SqlScriptCodeStrings(bool returnAllCol, TableData tb, string procName, bool customColumnsGen, Operation typeOperation, List<ManagedColumns> managedColumns)
        {
            StringBuilder sqlScriptCode = new StringBuilder();
            switch (typeOperation)
            {
                case Operation.Insert:
                    OperatioInsertScript(ref sqlScriptCode, customColumnsGen, returnAllCol, tb, managedColumns);
                    break;
                case Operation.Update:
                    OpetationUpdateScript(ref sqlScriptCode, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.Delete:
                    OpetationDeleteScript(ref sqlScriptCode, tb);
                    break;
                case Operation.GetAll:
                    OperationGetAllScript(ref sqlScriptCode, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.GetByID:
                    OperationGetByIDScript(ref sqlScriptCode, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.GetByFilter:
                    OperationGetByFilterScript(ref sqlScriptCode, customColumnsGen, tb, managedColumns);
                    break;
                case Operation.GetByFK:
                    OperationGetByFKScript(ref sqlScriptCode, customColumnsGen, tb, managedColumns);
                    break;
            }
            return sqlScriptCode;
        }

        public void CleanTable()
        {
            TableName = string.Empty;
            PrimaryKey = string.Empty;
            PrimaryKeyDataType = string.Empty;
            Columns = new List<ColumnData>();
        }

        #region Prams Operations

        private void OperationInsertParams(ref StringBuilder parameters, bool returnAllCol, bool customColumnsGen, TableData tb, List<ManagedColumns> managedColumns)
        {
            bool firstparameterAdded = false;
            List<string> param = new List<string>();
            List<string> dataType = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> dataValues = new List<string>();
            param.Clear();
            dataType.Clear();
            equalSign.Clear();
            dataValues.Clear();

            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (!firstparameterAdded)
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam)
                        {
                            param.Add("	 @" + tb.Columns[i].ColumnName);
                            dataType.Add(tb.Columns[i].ColumnDataType);
                            equalSign.Add(tb.Columns[i].equalsSign);
                            dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                            firstparameterAdded = true;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                        {
                            continue;
                        }
                        else
                        {
                            param.Add("	 @" + tb.Columns[i].ColumnName);
                            dataType.Add(tb.Columns[i].ColumnDataType);
                            equalSign.Add(tb.Columns[i].equalsSign);
                            dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                            firstparameterAdded = true;
                        }
                    }
                    else
                    {
                        param.Add("	 @" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                        firstparameterAdded = true;
                    }
                }
                else
                {
                    if (customColumnsGen)
                    {

                        ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam)
                        {
                            param.Add("	,@" + tb.Columns[i].ColumnName);
                            dataType.Add(tb.Columns[i].ColumnDataType);
                            equalSign.Add(tb.Columns[i].equalsSign);
                            dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                        {
                            continue;
                        }
                        else
                        {
                            param.Add("	,@" + tb.Columns[i].ColumnName);
                            dataType.Add(tb.Columns[i].ColumnDataType);
                            equalSign.Add(tb.Columns[i].equalsSign);
                            dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                        }
                    }
                    else
                    {
                        param.Add("	,@" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                    }
                }
            }

            var longestParamInsert = param.Max(w => w.Length) > ("	,@" + tb.PrimaryKey).Length ? param.Max(w => w.Length) : ("	,@" + tb.PrimaryKey).Length;
            var longestDatatypeInsert = dataType.Max(w => w.Length) > tb.PrimaryKeyDataType.Length ? dataType.Max(w => w.Length) : tb.PrimaryKeyDataType.Length;
            var longestEqualSignInsert = equalSign.Max(w => w.Length);
            var longestDataValuesInsert = dataValues.Max(w => w.Length);

            for (int i = 0; i < param.Count(); i++)
            {
                parameters.Append(string.Format("{0,-" + longestParamInsert + "} {1,-" + longestDatatypeInsert + "} {2,-" + longestEqualSignInsert + "} {3,-" + longestDataValuesInsert + "}\n"
                                                , param[i], dataType[i], equalSign[i], dataValues[i]));
            }
            if (!returnAllCol)
            {
                parameters.Append(string.Format("{0,-" + longestParamInsert + "} {1,-" + longestDatatypeInsert + "} {2,-" + longestEqualSignInsert + "} {3,-" + longestDataValuesInsert + "} {4,-6}\n"
                                               , ("	,@" + tb.PrimaryKey), tb.PrimaryKeyDataType, "", " = 0", "OUTPUT"));
            }
        }

        private void OperationUpdateParams(ref StringBuilder parameters, bool returnAllCol, bool customColumnsGen, TableData tb, List<ManagedColumns> managedColumns)
        {
            List<string> param = new List<string>();
            List<string> dataType = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> dataValues = new List<string>();
            param.Clear();
            dataType.Clear();
            equalSign.Clear();
            dataValues.Clear();

            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (customColumnsGen)
                {
                    ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                    if (currentCustomCollumn.UsedForParam)
                    {
                        param.Add("	,@" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                        continue;
                    }
                    else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                    {
                        continue;
                    }
                    else
                    {
                        param.Add("	,@" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                    }
                }
                else
                {
                    param.Add("	,@" + tb.Columns[i].ColumnName);
                    dataType.Add(tb.Columns[i].ColumnDataType);
                    equalSign.Add(tb.Columns[i].equalsSign);
                    dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                }
            }

            var longestParamUpdate = param.Max(w => w.Length) > ("	 @" + tb.PrimaryKey).Length ? param.Max(w => w.Length) : ("	 @" + tb.PrimaryKey).Length;
            var longestDatatypeUpdate = dataType.Max(w => w.Length) > tb.PrimaryKeyDataType.Length ? dataType.Max(w => w.Length) : tb.PrimaryKeyDataType.Length;
            var longestEqualSignUpdate = equalSign.Max(w => w.Length);
            var longestDataValuesUpdate = dataValues.Max(w => w.Length);

            parameters.Append(string.Format("{0,-" + longestParamUpdate + "} {1,-" + longestDatatypeUpdate + "} {2,-" + longestEqualSignUpdate + "} {3,-" + longestDataValuesUpdate + "}\n"
                                       , ("	 @" + tb.PrimaryKey), tb.PrimaryKeyDataType, "", ""));
            for (int i = 0; i < param.Count(); i++)
            {
                parameters.Append(string.Format("{0,-" + longestParamUpdate + "} {1,-" + longestDatatypeUpdate + "} {2,-" + longestEqualSignUpdate + "} {3,-" + longestDataValuesUpdate + "}\n"
                                                , param[i], dataType[i], equalSign[i], dataValues[i]));
            }
        }

        private void OperationGetByIDParams(ref StringBuilder parameters, TableData tb)
        {
            parameters.Append("	@" + tb.PrimaryKey + "	" + tb.PrimaryKeyDataType);
        }

        private void OperationDeleteParams(ref StringBuilder parameters, TableData tb)
        {
            parameters.Append("	@" + tb.PrimaryKey + "	" + tb.PrimaryKeyDataType);
        }
        private void OperationGetByFilterParams(ref StringBuilder parameters, bool returnAllCol, bool customColumnsGen, TableData tb, List<ManagedColumns> managedColumns)
        {
            List<string> param = new List<string>();
            List<string> dataType = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> dataValues = new List<string>();
            param.Clear();
            dataType.Clear();
            equalSign.Clear();
            dataValues.Clear();

            for (int i = 0; i < tb.Columns.Count(); i++)
            {

                if (customColumnsGen)
                {
                    ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                    if (currentCustomCollumn.UsedForParam)
                    {
                        param.Add("	,@" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                        continue;
                    }
                    else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                    {
                        continue;
                    }
                    else
                    {
                        param.Add("	,@" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                    }
                }
                else
                {
                    param.Add("	,@" + tb.Columns[i].ColumnName);
                    dataType.Add(tb.Columns[i].ColumnDataType);
                    equalSign.Add(tb.Columns[i].equalsSign);
                    dataValues.Add((tb.Columns[i].isNullable == true ? " = NULL" : ""));
                }

            }

            var longestParamUpdate = param.Max(w => w.Length);
            var longestDatatypeUpdate = dataType.Max(w => w.Length) > tb.PrimaryKeyDataType.Length ? dataType.Max(w => w.Length) : tb.PrimaryKeyDataType.Length;
            var longestEqualSignUpdate = equalSign.Max(w => w.Length);
            var longestDataValuesUpdate = dataValues.Max(w => w.Length);

            for (int i = 0; i < param.Count(); i++)
            {
                if (i == 0 && param[0].Contains(",") == true)
                {
                    param[0] = param[0].Replace(",", " ");
                }
                parameters.Append(string.Format("{0,-" + longestParamUpdate + "} {1,-" + longestDatatypeUpdate + "} {2,-" + longestEqualSignUpdate + "} {3,-" + longestDataValuesUpdate + "}\n"
                                                , param[i], dataType[i], equalSign[i], dataValues[i]));
            }
        }
        private void OperationGetByFKParams(ref StringBuilder parameters, bool returnAllCol, bool customColumnsGen, TableData tb, List<ManagedColumns> managedColumns)
        {
            List<string> param = new List<string>();
            List<string> dataType = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> dataValues = new List<string>();
            param.Clear();
            dataType.Clear();
            equalSign.Clear();
            dataValues.Clear();

            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (tb.Columns[i].isFK)
                {

                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(managedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam)
                        {
                            param.Add("	,@" + tb.Columns[i].ColumnName);
                            dataType.Add(tb.Columns[i].ColumnDataType);
                            equalSign.Add(tb.Columns[i].equalsSign);
                            dataValues.Add(" = NULL");
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName)
                        {
                            continue;
                        }
                        else
                        {
                            param.Add("	,@" + tb.Columns[i].ColumnName);
                            dataType.Add(tb.Columns[i].ColumnDataType);
                            equalSign.Add(tb.Columns[i].equalsSign);
                            dataValues.Add(" = NULL");
                        }
                    }
                    else
                    {
                        param.Add("	,@" + tb.Columns[i].ColumnName);
                        dataType.Add(tb.Columns[i].ColumnDataType);
                        equalSign.Add(tb.Columns[i].equalsSign);
                        dataValues.Add(" = NULL");
                    }
                }
            }
            if (param.Count > 0)
            {
                var longestParamUpdate = param.Max(w => w.Length);
                var longestDatatypeUpdate = dataType.Max(w => w.Length) > tb.PrimaryKeyDataType.Length ? dataType.Max(w => w.Length) : tb.PrimaryKeyDataType.Length;
                var longestEqualSignUpdate = equalSign.Max(w => w.Length);
                var longestDataValuesUpdate = dataValues.Max(w => w.Length);

                for (int i = 0; i < param.Count(); i++)
                {
                    if (i == 0 && param[0].Contains(",") == true)
                    {
                        param[0] = param[0].Replace(",", " ");
                    }
                    parameters.Append(string.Format("{0,-" + longestParamUpdate + "} {1,-" + longestDatatypeUpdate + "} {2,-" + longestEqualSignUpdate + "} {3,-" + longestDataValuesUpdate + "}\n"
                                                    , param[i], dataType[i], equalSign[i], dataValues[i]));
                }
            }
        }

        #endregion

        #region Script Operations

        private void OperatioInsertScript(ref StringBuilder sqlScriptCode, bool customColumnsGen, bool returnAllCol, TableData tb, List<ManagedColumns> InsertmanagedColumns)
        {
            //Add INSERT INTO WITH COLUMNS
            sqlScriptCode.Append("                 INSERT INTO [dbo].[" + tb.TableName + "] \n");
            sqlScriptCode.Append("                 ( \n");
            //Add the columns for insert statement
            bool firstparameterAdded = false;
            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (!firstparameterAdded)
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(InsertmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						   [" + tb.Columns[i].ColumnName + "] \n");
                            firstparameterAdded = true;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                               && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else
                        {
                            sqlScriptCode.Append("						   [" + tb.Columns[i].ColumnName + "] \n");
                            firstparameterAdded = true;
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						   [" + tb.Columns[i].ColumnName + "] \n");
                        firstparameterAdded = true;
                    }
                }
                else
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(InsertmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						  ,[" + tb.Columns[i].ColumnName + "] \n");
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                               && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else
                        {
                            sqlScriptCode.Append("						  ,[" + tb.Columns[i].ColumnName + "] \n");
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						  ,[" + tb.Columns[i].ColumnName + "] \n");
                    }
                }
            }

            //ADD SELECT on the PARAMETERS THAT HOLD THE VALUES
            sqlScriptCode.Append("                 ) \n");
            sqlScriptCode.Append("                 SELECT \n");
            firstparameterAdded = false;
            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (!firstparameterAdded)
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(InsertmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						   @" + tb.Columns[i].ColumnName + " \n");
                            firstparameterAdded = true;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						    " + currentCustomCollumn.ColumnValue.Trim() + " \n");
                            firstparameterAdded = true;
                        }
                        else
                        {
                            sqlScriptCode.Append("						   @" + tb.Columns[i].ColumnName + " \n");
                            firstparameterAdded = true;
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						   @" + tb.Columns[i].ColumnName + " \n");
                        firstparameterAdded = true;
                    }
                }
                else
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(InsertmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						  ,@" + tb.Columns[i].ColumnName + " \n");
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                        && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						  ," + currentCustomCollumn.ColumnValue.Trim() + " \n");
                        }
                        else
                        {
                            sqlScriptCode.Append("						  ,@" + tb.Columns[i].ColumnName + " \n");
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						  ,@" + tb.Columns[i].ColumnName + " \n");
                    }
                }
            }
            sqlScriptCode.Append("						  ;\n");
            // Do we want to return all columns form the insert 
            if (returnAllCol)
            {
                sqlScriptCode.Append("                 SELECT \n");
                sqlScriptCode.Append("					       SCOPE_IDENTITY()  AS [" + tb.PrimaryKey + "] \n");

                for (int i = 0; i < tb.Columns.Count(); i++)
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(InsertmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						  ,@" + tb.Columns[i].ColumnName + " \n");
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						  ," + currentCustomCollumn.ColumnValue.Trim() + " \n");
                        }
                        else
                        {
                            sqlScriptCode.Append("						  ,@" + tb.Columns[i].ColumnName + " \n");
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						  ,@" + tb.Columns[i].ColumnName + " \n");
                    }
                }
                sqlScriptCode.Append("						  ;");
            }
            //Return only the Pk columnt new ID 
            else
            {
                sqlScriptCode.Append("                  SET    @" + tb.PrimaryKey + " = SCOPE_IDENTITY();");
            }
        }

        private void OpetationUpdateScript(ref StringBuilder sqlScriptCode, bool customColumnsGen, TableData tb, List<ManagedColumns> UpdatemanagedColumns)
        {

            List<string> column = new List<string>();
            List<string> param = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> columnNull = new List<string>();
            List<string> dataValues = new List<string>();

            sqlScriptCode.Append("                 UPDATE	 [dbo].[" + tb.TableName + "] \n");
            sqlScriptCode.Append("                    SET \n");
            bool firstparameterAdded = false;

            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (!firstparameterAdded)
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(UpdatemanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            param.Add("@" + tb.Columns[i].ColumnName);
                            column.Add("						 [" + tb.Columns[i].ColumnName + "]");
                            equalSign.Add(" = ");
                            dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                            columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                            firstparameterAdded = true;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                            && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && currentCustomCollumn.UsedForColumn)
                        {

                            param.Add(currentCustomCollumn.ColumnValue.Trim());
                            column.Add("						 [" + tb.Columns[i].ColumnName + "]");
                            equalSign.Add(" = ");
                            dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                            columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                            firstparameterAdded = true;
                        }
                        else
                        {
                            param.Add("@" + tb.Columns[i].ColumnName);
                            column.Add("						 [" + tb.Columns[i].ColumnName + "]");
                            equalSign.Add(" = ");
                            dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                            columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                            firstparameterAdded = true;
                        }
                    }
                    else
                    {
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("						 [" + tb.Columns[i].ColumnName + "]");
                        equalSign.Add(" = ");
                        dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                        columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                        firstparameterAdded = true;
                    }
                }
                else
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(UpdatemanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            param.Add("@" + tb.Columns[i].ColumnName);
                            column.Add("						,[" + tb.Columns[i].ColumnName + "]");
                            equalSign.Add(" = ");
                            dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                            columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                && currentCustomCollumn.UsedForColumn)
                        {
                            param.Add(currentCustomCollumn.ColumnValue.Trim());
                            column.Add("						,[" + tb.Columns[i].ColumnName + "]");
                            equalSign.Add(" = ");
                            dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                            columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));


                        }
                        else
                        {
                            param.Add("@" + tb.Columns[i].ColumnName);
                            column.Add("						,[" + tb.Columns[i].ColumnName + "]");
                            equalSign.Add(" = ");
                            dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                            columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                        }
                    }
                    else
                    {
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("						,[" + tb.Columns[i].ColumnName + "]");
                        equalSign.Add(" = ");
                        dataValues.Add((tb.Columns[i].isNullable == true ? "ISNULL( " : ""));
                        columnNull.Add((tb.Columns[i].isNullable == true ? ("[" + tb.Columns[i].ColumnName + "]") : ""));
                    }
                }
            }

            var longestParamUpdate = param.Max(w => w.Length);
            var longestcolumnUpdate = column.Max(w => w.Length);
            var longestcolumnNullUpdate = columnNull.Max(w => w.Length);
            var longestEqualSignUpdate = equalSign.Max(w => w.Length);
            var longestDataValuesUpdate = dataValues.Max(w => w.Length);

            for (int i = 0; i < column.Count(); i++)
            {
                sqlScriptCode.Append(string.Format(
                 "{0,-" + longestcolumnUpdate + "}{1,-" + longestEqualSignUpdate + "}{2,-" + longestParamUpdate + "}{3,-" + longestParamUpdate + "}{4,-3}{5,-" + longestcolumnNullUpdate + "}{6,-2}\n"
                 , column[i] //0
                 , equalSign[i] //1
                 , (dataValues[i] == string.Empty ? param[i] : dataValues[i]) //2
                 , (dataValues[i] == string.Empty ? "" : param[i]) //3
                 , (dataValues[i] == string.Empty ? "" : " , ") //4
                 , (dataValues[i] == string.Empty ? "" : columnNull[i]) //5
                 , (dataValues[i] == string.Empty ? "" : " )") //6
                                                  ));
            }

            sqlScriptCode.Append("				 WHERE [" + tb.PrimaryKey + "] = " + "@" + tb.PrimaryKey + "\n");
            sqlScriptCode.Append("				   AND (\n");

            for (int i = 0; i < column.Count(); i++)
            {

                sqlScriptCode.Append(string.Format(
                 "				        ({0,-" + longestParamUpdate + "}{1,-" + 18 + "}{2,-" + (longestcolumnUpdate) + "}{3,-" + 13 + "}{4,-" + (longestcolumnUpdate) + "}{5,-" + 17 + "}{6,-" + (longestcolumnUpdate) + "}{7,-4}{8,-" + longestParamUpdate + "}{9,-3}\n"
                 , param[i]                                                                     //0
                 , " IS NOT NULL AND ("                                                         //1
                 , column[i].Replace(",", "").Trim()                                            //2 
                 , " IS NULL OR ("                                                              //3
                 , column[i].Replace(",", "").Trim()                                            //4
                 , " IS NOT NULL AND "                                                          //5
                 , column[i].Replace(",", "").Trim()                                            //6
                 , " <> "                                                                       //7   
                 , param[i]                                                                     //8
                 , ")))"                                                                        //9
                                                  ));

                if ((i + 1) != column.Count())
                {
                    sqlScriptCode.Append("				        OR \n");
                }
            }
            sqlScriptCode.Append("				       )\n");
            sqlScriptCode.Append("				SELECT @@ROWCOUNT AS [RowCount];");
        }

        private void OpetationDeleteScript(ref StringBuilder sqlScriptCode, TableData tb)
        {
            sqlScriptCode.Append("                 DELETE \n");
            sqlScriptCode.Append("                   FROM [dbo].[" + tb.TableName + "] \n");
            sqlScriptCode.Append("                  WHERE [" + tb.PrimaryKey + "] = @" + tb.PrimaryKey + "; \n");
            sqlScriptCode.Append("                 SELECT @@ROWCOUNT AS [RowCount];");
        }

        private void OperationGetAllScript(ref StringBuilder sqlScriptCode, bool customColumnsGen, TableData tb, List<ManagedColumns> GetAllmanagedColumns)
        {
            bool firstparameterAdded = false;
            sqlScriptCode.Append("                 SELECT \n");
            firstparameterAdded = false;
            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (!firstparameterAdded)
                {

                    sqlScriptCode.Append("						 [" + tb.PrimaryKey + "] \n");
                    firstparameterAdded = true;
                }
                else
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(GetAllmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                        && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						,[" + currentCustomCollumn.ColumnName.Trim() + "] \n");
                        }
                        else
                        {
                            sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                    }
                }
            }
            sqlScriptCode.Append("                   FROM  [dbo].[" + tb.TableName + "];\n");
        }
        private void OperationGetByIDScript(ref StringBuilder sqlScriptCode, bool customColumnsGen, TableData tb, List<ManagedColumns> GetByIDmanagedColumns)
        {
            bool firstparameterAdded = false;
            sqlScriptCode.Append("                 SELECT \n");
            firstparameterAdded = false;
            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (!firstparameterAdded)
                {
                    sqlScriptCode.Append("						 [" + tb.PrimaryKey + "] \n");
                    firstparameterAdded = true;
                }
                else
                {
                    if (customColumnsGen)
                    {
                        ManagedColumns currentCustomCollumn = GetCustomColumnData(GetByIDmanagedColumns, tb.Columns[i].ColumnName);
                        if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                 && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                        && !currentCustomCollumn.UsedForColumn)
                        {
                            continue;
                        }
                        else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                                && currentCustomCollumn.UsedForColumn)
                        {
                            sqlScriptCode.Append("						,[" + currentCustomCollumn.ColumnName.Trim() + "] \n");
                        }
                        else
                        {
                            sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                        }
                    }
                    else
                    {
                        sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                    }
                }
            }
            sqlScriptCode.Append("                   FROM  [dbo].[" + tb.TableName + "]\n");
            sqlScriptCode.Append("                  WHERE  [dbo].[" + tb.TableName + "].[" + tb.PrimaryKey + "] = @" + tb.PrimaryKey + ";\n");
        }
        private void OperationGetByFilterScript(ref StringBuilder sqlScriptCode, bool customColumnsGen, TableData tb, List<ManagedColumns> GetByFiltermanagedColumns)
        {
            List<string> column = new List<string>();
            List<string> param = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> columnNull = new List<string>();
            List<string> dataValues = new List<string>();

            sqlScriptCode.Append("                 SELECT \n");

            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (i == 0)
                {
                    sqlScriptCode.Append("						 [" + tb.PrimaryKey + "] \n");
                }

                if (customColumnsGen)
                {
                    ManagedColumns currentCustomCollumn = GetCustomColumnData(GetByFiltermanagedColumns, tb.Columns[i].ColumnName);
                    if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                    {
                        sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("[" + tb.Columns[i].ColumnName + "]");

                        continue;
                    }
                    else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                             && !currentCustomCollumn.UsedForColumn)
                    {
                        continue;
                    }
                    else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                    && !currentCustomCollumn.UsedForColumn)
                    {
                        continue;
                    }
                    else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                            && currentCustomCollumn.UsedForColumn)
                    {
                        sqlScriptCode.Append("						,[" + currentCustomCollumn.ColumnName.Trim() + "] \n");
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("[" + tb.Columns[i].ColumnName + "]");
                    }
                    else
                    {
                        sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("[" + tb.Columns[i].ColumnName + "]");
                    }
                }
                else
                {
                    sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                    param.Add("@" + tb.Columns[i].ColumnName);
                    column.Add("[" + tb.Columns[i].ColumnName + "]");
                }

            }
            sqlScriptCode.Append("                   FROM  [dbo].[" + tb.TableName + "]\n");
            sqlScriptCode.Append("                  WHERE");

            var longestParamUpdate = param.Max(w => w.Length);
            var longestcolumnUpdate = column.Max(w => w.Length);

            for (int i = 0; i < column.Count(); i++)
            {
                if (i == 0)
                {
                    sqlScriptCode.Append(string.Format(
                     " ({0,-" + longestParamUpdate + "}{1,-" + 13 + "}{2,-" + (longestcolumnUpdate) + "}{3,-" + 3 + "}{4,-" + (longestParamUpdate) + "}{5,-" + 1 + "}"
                     , param[i]                                                                     //0
                     , " IS NULL  OR "                                                              //1
                     , column[i]                                                                    //2 
                     , " = "                                                                        //3
                     , param[i]                                                                     //4
                     , ")"                                                                          //5
                                                      ));
                }
                else
                {
                    sqlScriptCode.Append(string.Format(
                  "\n				    AND ({0,-" + longestParamUpdate + "}{1,-" + 13 + "}{2,-" + (longestcolumnUpdate) + "}{3,-" + 3 + "}{4,-" + (longestParamUpdate) + "}{5,-" + 1 + "}"
                  , param[i]                                                                     //0
                  , " IS NULL  OR "                                                              //1
                  , column[i]                                                                    //2 
                  , " = "                                                                        //3
                  , param[i]                                                                     //4
                  , ")"                                                                          //5
                                                   ));

                }

            }
            sqlScriptCode.Append(";");
        }

        private void OperationGetByFKScript(ref StringBuilder sqlScriptCode, bool customColumnsGen, TableData tb, List<ManagedColumns> GetByFKmanagedColumns)
        {
            List<string> column = new List<string>();
            List<string> param = new List<string>();
            List<string> equalSign = new List<string>();
            List<string> columnNull = new List<string>();
            List<string> dataValues = new List<string>();

            sqlScriptCode.Append("                 SELECT \n");

            for (int i = 0; i < tb.Columns.Count(); i++)
            {
                if (i == 0)
                {
                    sqlScriptCode.Append("						 [" + tb.PrimaryKey + "] \n");
                }

                if (customColumnsGen)
                {
                    ManagedColumns currentCustomCollumn = GetCustomColumnData(GetByFKmanagedColumns, tb.Columns[i].ColumnName);
                    if (currentCustomCollumn.UsedForParam && currentCustomCollumn.UsedForColumn)
                    {
                        sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("[" + tb.Columns[i].ColumnName + "]");

                        continue;
                    }
                    else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                             && !currentCustomCollumn.UsedForColumn)
                    {
                        continue;
                    }
                    else if (currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                    && !currentCustomCollumn.UsedForColumn)
                    {
                        continue;
                    }
                    else if (!currentCustomCollumn.UsedForParam && currentCustomCollumn.ColumnName == tb.Columns[i].ColumnName
                            && currentCustomCollumn.UsedForColumn)
                    {
                        sqlScriptCode.Append("						,[" + currentCustomCollumn.ColumnName.Trim() + "] \n");
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("[" + tb.Columns[i].ColumnName + "]");
                    }
                    else
                    {
                        sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                        param.Add("@" + tb.Columns[i].ColumnName);
                        column.Add("[" + tb.Columns[i].ColumnName + "]");
                    }
                }
                else
                {
                    sqlScriptCode.Append("						,[" + tb.Columns[i].ColumnName + "] \n");
                    param.Add("@" + tb.Columns[i].ColumnName);
                    column.Add("[" + tb.Columns[i].ColumnName + "]");
                }

            }
            sqlScriptCode.Append("                   FROM  [dbo].[" + tb.TableName + "]\n");
            sqlScriptCode.Append("                  WHERE");

            var longestParamUpdate = param.Max(w => w.Length);
            var longestcolumnUpdate = column.Max(w => w.Length);
            bool firstAdded = false;
            for (int i = 0; i < column.Count(); i++)
            {
                if (tb.Columns[i].isFK)
                {

                    if (!firstAdded)
                    {
                        sqlScriptCode.Append(string.Format(
                         " ({0,-" + longestParamUpdate + "}{1,-" + 13 + "}{2,-" + (longestcolumnUpdate) + "}{3,-" + 3 + "}{4,-" + (longestParamUpdate) + "}{5,-" + 1 + "}"
                         , param[i]                                                                     //0
                         , " IS NULL  OR "                                                              //1
                         , column[i]                                                                    //2 
                         , " = "                                                                        //3
                         , param[i]                                                                     //4
                         , ")"                                                                          //5
                                                          ));
                        firstAdded = true;
                    }
                    else
                    {
                        sqlScriptCode.Append(string.Format(
                      "\n				    AND ({0,-" + longestParamUpdate + "}{1,-" + 13 + "}{2,-" + (longestcolumnUpdate) + "}{3,-" + 3 + "}{4,-" + (longestParamUpdate) + "}{5,-" + 1 + "}"
                      , param[i]                                                                     //0
                      , " IS NULL  OR "                                                              //1
                      , column[i]                                                                    //2 
                      , " = "                                                                        //3
                      , param[i]                                                                     //4
                      , ")"                                                                          //5
                                                       ));

                    }
                }
            }
            sqlScriptCode.Append(";");
        }

        #endregion
    }
}
