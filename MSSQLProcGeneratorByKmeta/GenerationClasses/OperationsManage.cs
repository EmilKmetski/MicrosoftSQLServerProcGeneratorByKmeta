using System;
using System.IO;
using System.Text;

namespace MSSQLProcGeneratorByKmeta.GenerationClasses
{
    public static class OperationsManage
    {
        public static void OperationInsert(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + "\\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + "\\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder insertScript = new StringBuilder();
                insertScript.Append(SqlScriptReader.ReadToEnd());
                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                insertScript.Replace("!pr0cNam3!", procName);
                insertScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                insertScript.Replace("!cr3ator!", creator);
                insertScript.Replace("!Operati0n!", currentOper.ToString());
                insertScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings                           
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Insert, dbs.ManagedInsertColumns);
                insertScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion

                #region scriptParameters  
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Insert, dbs.ManagedInsertColumns);
                insertScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion

                #region scriptCode                                
                StringBuilder sqlScriptCode = tb.SqlScriptCodeStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Insert, dbs.ManagedInsertColumns);
                insertScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion

                //save file for table
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", insertScript);
            }
        }

        public static void OperationUpdate(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + @"\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + @"\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder updateScript = new StringBuilder();
                updateScript.Append(SqlScriptReader.ReadToEnd());
                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                updateScript.Replace("!pr0cNam3!", procName);
                updateScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                updateScript.Replace("!cr3ator!", creator);
                updateScript.Replace("!Operati0n!", currentOper.ToString().ToUpper());
                updateScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings                                
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Update, dbs.ManagedUpdateColumns);
                updateScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion

                #region scriptParameters 
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Update, dbs.ManagedUpdateColumns);
                updateScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion

                #region scriptCode
                StringBuilder sqlScriptCode = tb.SqlScriptCodeStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Update, dbs.ManagedUpdateColumns);
                updateScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", updateScript);
            }
        }

        public static void OperationDelete(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + "\\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + "\\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder deleteScript = new StringBuilder();
                deleteScript.Append(SqlScriptReader.ReadToEnd());

                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                deleteScript.Replace("!pr0cNam3!", procName);
                deleteScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                deleteScript.Replace("!cr3ator!", creator);
                deleteScript.Replace("!Operati0n!", currentOper.ToString().ToUpper());
                deleteScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Delete, null);
                deleteScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion

                #region scriptParameters            
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.Delete, null);
                deleteScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion

                #region scriptCode
                StringBuilder sqlScriptCode = new StringBuilder();
                sqlScriptCode.Append("                 DELETE \n");
                sqlScriptCode.Append("                   FROM [dbo].[" + tb.TableName + "] \n");
                sqlScriptCode.Append("                  WHERE [" + tb.PrimaryKey + "] = @" + tb.PrimaryKey + "; \n");
                sqlScriptCode.Append("                 SELECT @@ROWCOUNT AS [RowCount];");
                deleteScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", deleteScript);
            }
        }

        public static void OperationGetAll(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + "\\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + "\\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder getAllScript = new StringBuilder();
                getAllScript.Append(SqlScriptReader.ReadToEnd());
                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                getAllScript.Replace("!pr0cNam3!", procName);
                getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                getAllScript.Replace("!cr3ator!", creator);
                getAllScript.Replace("!Operati0n!", currentOper.ToString().ToUpper());
                getAllScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings                                
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetAll, dbs.ManagedSelectColumns);
                getAllScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion
                #region scriptParameters 
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetAll, dbs.ManagedSelectColumns);
                getAllScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion
                #region scriptCode
                StringBuilder sqlScriptCode = tb.SqlScriptCodeStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetAll, dbs.ManagedSelectColumns);
                getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
            }
        }
        public static void OperationGetByID(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + "\\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + "\\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder getAllScript = new StringBuilder();
                getAllScript.Append(SqlScriptReader.ReadToEnd());
                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                getAllScript.Replace("!pr0cNam3!", procName);
                getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                getAllScript.Replace("!cr3ator!", creator);
                getAllScript.Replace("!Operati0n!", currentOper.ToString().ToUpper());
                getAllScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings                                
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByID, dbs.ManagedSelectColumns);
                getAllScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion
                #region scriptParameters 
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByID, dbs.ManagedSelectColumns);
                getAllScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion
                #region scriptCode
                StringBuilder sqlScriptCode = tb.SqlScriptCodeStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByID, dbs.ManagedSelectColumns);
                getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
            }
        }

        public static void OperationGetByFilter(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + "\\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + "\\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder getAllScript = new StringBuilder();
                getAllScript.Append(SqlScriptReader.ReadToEnd());
                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                getAllScript.Replace("!pr0cNam3!", procName);
                getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                getAllScript.Replace("!cr3ator!", creator);
                getAllScript.Replace("!Operati0n!", currentOper.ToString().ToUpper());
                getAllScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings                                
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByFilter, dbs.ManagedSelectColumns);
                getAllScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion
                #region scriptParameters 
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByFilter, dbs.ManagedSelectColumns);
                getAllScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion
                #region scriptCode
                StringBuilder sqlScriptCode = tb.SqlScriptCodeStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByFilter, dbs.ManagedSelectColumns);
                getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
            }
        }

        public static void OperationGetByFK(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + @"\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + @"\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder getAllScript = new StringBuilder();
                getAllScript.Append(SqlScriptReader.ReadToEnd());
                string procName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                getAllScript.Replace("!pr0cNam3!", procName);
                getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                getAllScript.Replace("!cr3ator!", creator);
                getAllScript.Replace("!Operati0n!", currentOper.ToString().ToUpper());
                getAllScript.Replace("!tblNAM3!", tb.TableName);

                #region TestedStrings                                
                StringBuilder testedStrings = tb.TestedStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByFK, dbs.ManagedSelectColumns);
                getAllScript.Replace("!T3sTStrings!", testedStrings.ToString());
                #endregion
                #region scriptParameters 
                StringBuilder parameters = tb.ParameterStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByFK, dbs.ManagedSelectColumns);
                getAllScript.Replace("!param3t3rs!", parameters.ToString());
                #endregion
                #region scriptCode
                StringBuilder sqlScriptCode = tb.SqlScriptCodeStrings(returnAllData, tb, procName, questarSpecificDates, TableData.Operation.GetByFK, dbs.ManagedSelectColumns);
                getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                #endregion
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
            }
        }
        public static void OperationNonClusteredCoveringIndexesBridgeTables(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + @"\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + @"\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                foreach (ColumnData col in tb.Columns)
                {
                    if (col.isFK)
                    {
                        FileInfo file = new FileInfo(temlateSQLScriptPath);
                        StreamReader SqlScriptReader = file.OpenText();
                        StringBuilder getAllScript = new StringBuilder();
                        getAllScript.Append(SqlScriptReader.ReadToEnd());
                        string indexName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!C0lumn!", col.ColumnName.ToString());
                        string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!C0lumn!", col.ColumnName.ToString());

                        getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                        getAllScript.Replace("!cr3ator!", creator);
                        getAllScript.Replace("!tblNAM3!", tb.TableName);

                        #region scriptCode
                        StringBuilder sqlScriptCode = new StringBuilder();
                        sqlScriptCode.Append(
                                              "\n CREATE NONCLUSTERED INDEX [" + indexName + "] ON [dbo].[" + tb.TableName + "]                        "
                                            + "\n (                                                                                                                              "
                                            + "\n  [" + col.ColumnName + "]                                                                                                   "
                                            + "\n )"
                                            + "\nINCLUDE"
                                            + "\n (");
                        bool firstAdded = false;
                        foreach (ColumnData colIncl in tb.Columns)
                        {
                            if (colIncl.isFK && colIncl.ColumnName != col.ColumnName)
                            {
                                sqlScriptCode.Append("\n " + (firstAdded == true ? "," : "") + " [" + colIncl.ColumnName + "]");
                                firstAdded = true;
                            }
                        }
                        sqlScriptCode.Append("\n )"
                                            + "\n GO"
                                            );
                        if (firstAdded)
                        {
                            getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                            #endregion
                            CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
                        }
                    }
                }
            }
        }
        public static void OperationNonClusteredIndexesFKAllTables(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + @"\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + @"\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                foreach (ColumnData col in tb.Columns)
                {
                    if (col.isFK)
                    {
                        FileInfo file = new FileInfo(temlateSQLScriptPath);
                        StreamReader SqlScriptReader = file.OpenText();
                        StringBuilder getAllScript = new StringBuilder();
                        getAllScript.Append(SqlScriptReader.ReadToEnd());

                        string indexName = procNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!C0lumn!", col.ColumnName.ToString());
                        string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString()).Replace("!C0lumn!", col.ColumnName.ToString());

                        getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                        getAllScript.Replace("!cr3ator!", creator);
                        getAllScript.Replace("!tblNAM3!", tb.TableName);

                        #region scriptCode
                        StringBuilder sqlScriptCode = new StringBuilder();
                        sqlScriptCode.Append(
                                              "\n CREATE NONCLUSTERED INDEX [" + indexName + "] ON [dbo].[" + tb.TableName + "]                        "
                                            + "\n (                                                                                                                              "
                                            + "\n  [" + col.ColumnName + "]                                                                                                   "
                                            + "\n ) "
                                            + "\n GO"
                                            );

                        getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                        #endregion
                        CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
                    }
                }
            }
        }

        public static void OperationNonClusteredIndexesFKAllColumnsAllTables(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + @"\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + @"\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder getAllScript = new StringBuilder();
                getAllScript.Append(SqlScriptReader.ReadToEnd());
                string indexName = procNameTemplate.Replace("!tblNAM3!", tb.TableName);
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName);
                getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                getAllScript.Replace("!cr3ator!", creator);
                getAllScript.Replace("!tblNAM3!", tb.TableName);
                StringBuilder sqlScriptCode = new StringBuilder();
                sqlScriptCode.Append(
                                       "\n CREATE NONCLUSTERED INDEX  [!idxN@me!] ON [dbo].[" + tb.TableName + "]                        "
                                     + "\n ( ");
                bool firstAdded = false;
                foreach (ColumnData col in tb.Columns)
                {
                    if (col.isFK)
                    {
                        indexName += "_" + col.ColumnName;
                        procFileName += "_" + col.ColumnName;
                        sqlScriptCode.Append("\n  " + (firstAdded == true ? "," : " ") + "[" + col.ColumnName + "]                                                                                                   ");
                        firstAdded = true;
                    }
                }
                if (firstAdded)
                {
                    sqlScriptCode.Replace("!idxN@me!", indexName);
                    sqlScriptCode.Append("\n ) " + "\n GO");
                    getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                    CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);
                }
            }
        }

        public static void OperationUniqueIndexFK(DatabaseData dbs, string procNameTemplate, string procFileNameTemplate, bool returnAllData, string creator, string dateformat, bool questarSpecificDates, TableData.Operation currentOper, string temlateSQLScriptPath, string finalDir)
        {
            if (!Directory.Exists(finalDir + @"\" + currentOper.ToString().ToUpper()))
            {
                Directory.CreateDirectory(finalDir + @"\" + currentOper.ToString().ToUpper());
            }

            foreach (TableData tb in dbs.Tables)
            {
                FileInfo file = new FileInfo(temlateSQLScriptPath);
                StreamReader SqlScriptReader = file.OpenText();
                StringBuilder getAllScript = new StringBuilder();
                getAllScript.Append(SqlScriptReader.ReadToEnd());
                string indexName = procNameTemplate.Replace("!tblNAM3!", tb.TableName);
                string procFileName = procFileNameTemplate.Replace("!tblNAM3!", tb.TableName).Replace("!Operati0n!", currentOper.ToString());
                getAllScript.Replace("!cr3ationDAT3!", DateTime.Now.ToString(dateformat.Replace("/", "\\/")));
                getAllScript.Replace("!cr3ator!", creator);
                getAllScript.Replace("!tblNAM3!", tb.TableName);
                StringBuilder sqlScriptCode = new StringBuilder();
                sqlScriptCode.Append(
                                       "\n CREATE UNIQUE NONCLUSTERED INDEX  [!idxN@me!] ON [dbo].[" + tb.TableName + "]                        "
                                     + "\n ( ");
                bool firstAdded = false;
                foreach (ColumnData col in tb.Columns)
                {
                    if (col.isFK)
                    {
                        indexName += "_" + col.ColumnName;
                        procFileName += "_" + col.ColumnName;
                        sqlScriptCode.Append("\n  " + (firstAdded == true ? "," : "") + "[" + col.ColumnName + "]                                                                                                   ");
                        firstAdded = true;
                    }
                }
                sqlScriptCode.Replace("!idxN@me!", indexName);
                sqlScriptCode.Append("\n ) " + "\n GO");
                getAllScript.Replace("!pr0cC0d3!", sqlScriptCode.ToString());
                CreateNewFile(finalDir + @"\" + currentOper.ToString().ToUpper() + @"\" + procFileName + ".sql", getAllScript);

            }
        }
        /// <summary>
        /// Write the string for procedure in file    
        /// </summary>
        /// <param name="fileName">file name only file name whitout extensions</param>
        /// <param name="script">StringBuilder object containing the file string</param>
        public static void CreateNewFile(string fileName, StringBuilder script)
        {
            Encoding utf8 = Encoding.GetEncoding(UTF8Encoding.UTF8.CodePage);
            byte[] output = utf8.GetBytes(script.ToString());
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length);
            bw.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
