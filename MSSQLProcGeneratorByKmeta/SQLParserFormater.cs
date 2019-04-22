using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MSSQLProcGeneratorByKmeta
{
    class SQLFormater
    {
        //Enumeration that will hold all available settings
        enum ScriptOptions
        {
            AlignClauseBodies
            , AlignColumnDefinitionFields
            , AlignSetClauseItem
            , AsKeywordOnOwnLine
            , IncludeSemicolons
            , IndentationSize
            , IndentSetClause
            , IndentViewBody
            , KeywordCasing
            , MultilineInsertSourcesList
            , MultilineInsertTargetsList
            , MultilineSelectElementsList
            , MultilineSetClauseItems
            , MultilineViewColumnsList
            , MultilineWherePredicatesList
            , NewLineBeforeCloseParenthesisInMultilineList
            , NewLineBeforeFromClause
            , NewLineBeforeGroupByClause
            , NewLineBeforeHavingClause
            , NewLineBeforeJoinClause
            , NewLineBeforeOffsetClause
            , NewLineBeforeOpenParenthesisInMultilineList
            , NewLineBeforeOrderByClause
            , NewLineBeforeOutputClause
            , NewLineBeforeWhereClause
            , SqlVersion
        }
        /// <summary>
        /// Get and assign format settings based on interface or DataTable from excel file
        /// If Control == null then we use it in file mode and vice versa
        /// </summary>
        /// <param name="opt"> reference to SqlScriptGeneratorOptions</param>
        /// <param name="ctrl"> passed Control if we will use it in Interface mode</param>
        /// <param name="settingRow">passed DataRow which contain the settings</param>        
        private void setScriptOptions(ref SqlScriptGeneratorOptions opt, Control ctrl, DataRow settingRow)
        {
            CheckBox cbk = new CheckBox();
            ComboBox combo = new ComboBox();
            TextBox tb = new TextBox();
            ScriptOptions currentScriptSetting = ScriptOptions.AlignClauseBodies;
            if (ctrl != null)
            {
                currentScriptSetting = ctrl.Name.ToEnum<ScriptOptions>();
            }
            else
            {
                currentScriptSetting = settingRow[1].ToString().ToEnum<ScriptOptions>();
            }
            switch (currentScriptSetting)
            {
                case ScriptOptions.AlignClauseBodies:
                    opt.AlignClauseBodies = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.AlignColumnDefinitionFields:
                    opt.AlignColumnDefinitionFields = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.AlignSetClauseItem:
                    opt.AlignSetClauseItem = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.AsKeywordOnOwnLine:
                    opt.AsKeywordOnOwnLine = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.IncludeSemicolons:
                    cbk = ctrl as CheckBox;
                    opt.IncludeSemicolons = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.IndentationSize:
                    opt.IndentationSize = GetValueInt(ctrl, settingRow);
                    break;
                case ScriptOptions.IndentSetClause:
                    opt.IndentSetClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.IndentViewBody:
                    opt.IndentViewBody = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.KeywordCasing:
                    ComboBox comboCasing = ctrl as ComboBox;
                    // 0 - Lowercase,1 - Uppercase,2 - PascalCase
                    switch (GetValueInt(ctrl, settingRow))
                    {
                        case 0:
                            opt.KeywordCasing = KeywordCasing.Lowercase;
                            break;
                        case 1:
                            opt.KeywordCasing = KeywordCasing.Uppercase;
                            break;
                        case 2:
                            opt.KeywordCasing = KeywordCasing.PascalCase;
                            break;
                    }
                    break;
                case ScriptOptions.MultilineInsertSourcesList:
                    opt.MultilineInsertSourcesList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.MultilineInsertTargetsList:
                    opt.MultilineInsertTargetsList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.MultilineSelectElementsList:
                    opt.MultilineSelectElementsList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.MultilineSetClauseItems:
                    opt.MultilineSetClauseItems = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.MultilineViewColumnsList:
                    opt.MultilineViewColumnsList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.MultilineWherePredicatesList:
                    opt.MultilineWherePredicatesList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeCloseParenthesisInMultilineList:
                    opt.NewLineBeforeCloseParenthesisInMultilineList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeFromClause:
                    opt.NewLineBeforeFromClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeGroupByClause:
                    opt.NewLineBeforeGroupByClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeHavingClause:
                    opt.NewLineBeforeHavingClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeJoinClause:
                    opt.NewLineBeforeJoinClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeOffsetClause:
                    opt.NewLineBeforeOffsetClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeOpenParenthesisInMultilineList:
                    opt.NewLineBeforeOpenParenthesisInMultilineList = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeOrderByClause:
                    opt.NewLineBeforeOrderByClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeOutputClause:
                    opt.NewLineBeforeOutputClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.NewLineBeforeWhereClause:
                    opt.NewLineBeforeWhereClause = GetValueBool(ctrl, settingRow);
                    break;
                case ScriptOptions.SqlVersion:
                    //Sql2005 = 0,
                    //Sql2000 = 1,
                    //Sql2008 = 2,
                    //Sql2012 = 3,
                    //Sql2014 = 4,
                    //Sql2016 = 5,
                    //Sql2017 = 6
                    switch (GetValueInt(ctrl, settingRow))
                    {
                        case 0:
                            opt.SqlVersion = SqlVersion.Sql90;
                            break;
                        case 1:
                            opt.SqlVersion = SqlVersion.Sql80;
                            break;
                        case 2:
                            opt.SqlVersion = SqlVersion.Sql100;
                            break;
                        case 3:
                            opt.SqlVersion = SqlVersion.Sql110;
                            break;
                        case 4:
                            opt.SqlVersion = SqlVersion.Sql120;
                            break;
                        case 5:
                            opt.SqlVersion = SqlVersion.Sql130;
                            break;
                        case 6:
                            opt.SqlVersion = SqlVersion.Sql140;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        //Used to return int Value for either control or datarow based on wich one is null
        private int GetValueInt(Control ctrlInt, DataRow dtrInt)
        {
            ComboBox combo = new ComboBox();
            TextBox tb = new TextBox();
            int intValue = 0;
            if (ctrlInt != null)
            {
                if (ctrlInt.GetType() == typeof(TextBox))
                {
                    tb = ctrlInt as TextBox;
                    intValue = int.Parse(tb.Text);
                }
                else
                {
                    combo = ctrlInt as ComboBox;
                    intValue = combo.SelectedIndex;
                }
            }
            else
            {
                intValue = int.Parse(dtrInt[0].ToString());
            }
            return intValue;
        }
        //Used to return bool Value for either control or datarow based on wich one is null
        private bool GetValueBool(Control ctrlBool, DataRow dtrBool)
        {
            CheckBox cbk = new CheckBox();
            bool boolValue = false;
            if (ctrlBool != null)
            {
                cbk = ctrlBool as CheckBox;
                if (cbk != null)
                {
                    if (cbk.Checked)
                    {
                        boolValue = true;
                    }
                    else
                    {
                        boolValue = false;
                    }
                }
            }
            else
            {
                if (bool.Parse(dtrBool[0].ToString()))
                {
                    boolValue = true;
                }
                else
                {
                    boolValue = false;
                }
            }
            return boolValue;
        }
        //Add format settings to the Script Generator
        private void addScriptOptionsToScriptGen(SqlScriptGeneratorOptions scriptOptions, ref Sql130ScriptGenerator srcGenScript)
        {
            if (scriptOptions != null)
            {
                srcGenScript.Options.AlignClauseBodies = scriptOptions.AlignClauseBodies;
                srcGenScript.Options.AlignColumnDefinitionFields = scriptOptions.AlignColumnDefinitionFields;
                srcGenScript.Options.AlignSetClauseItem = scriptOptions.AlignSetClauseItem;
                srcGenScript.Options.AsKeywordOnOwnLine = scriptOptions.AsKeywordOnOwnLine;
                srcGenScript.Options.IncludeSemicolons = scriptOptions.IncludeSemicolons;
                srcGenScript.Options.IndentationSize = scriptOptions.IndentationSize;
                srcGenScript.Options.IndentSetClause = scriptOptions.IndentSetClause;
                srcGenScript.Options.IndentViewBody = scriptOptions.IndentViewBody;
                srcGenScript.Options.KeywordCasing = scriptOptions.KeywordCasing;
                srcGenScript.Options.MultilineInsertSourcesList = scriptOptions.MultilineInsertSourcesList;
                srcGenScript.Options.MultilineInsertTargetsList = scriptOptions.MultilineInsertTargetsList;
                srcGenScript.Options.MultilineSelectElementsList = scriptOptions.MultilineSelectElementsList;
                srcGenScript.Options.MultilineSetClauseItems = scriptOptions.MultilineSetClauseItems;
                srcGenScript.Options.MultilineViewColumnsList = scriptOptions.MultilineViewColumnsList;
                srcGenScript.Options.MultilineWherePredicatesList = scriptOptions.MultilineWherePredicatesList;
                srcGenScript.Options.NewLineBeforeCloseParenthesisInMultilineList = scriptOptions.NewLineBeforeCloseParenthesisInMultilineList;
                srcGenScript.Options.NewLineBeforeFromClause = scriptOptions.NewLineBeforeFromClause;
                srcGenScript.Options.NewLineBeforeGroupByClause = scriptOptions.NewLineBeforeGroupByClause;
                srcGenScript.Options.NewLineBeforeHavingClause = scriptOptions.NewLineBeforeHavingClause;
                srcGenScript.Options.NewLineBeforeJoinClause = scriptOptions.NewLineBeforeJoinClause;
                srcGenScript.Options.NewLineBeforeOffsetClause = scriptOptions.NewLineBeforeOffsetClause;
                srcGenScript.Options.NewLineBeforeOpenParenthesisInMultilineList = scriptOptions.NewLineBeforeOpenParenthesisInMultilineList;
                srcGenScript.Options.NewLineBeforeOrderByClause = scriptOptions.NewLineBeforeOrderByClause;
                srcGenScript.Options.NewLineBeforeOutputClause = scriptOptions.NewLineBeforeOutputClause;
                srcGenScript.Options.NewLineBeforeWhereClause = scriptOptions.NewLineBeforeWhereClause;
                srcGenScript.Options.SqlVersion = scriptOptions.SqlVersion;
                srcGenScript.Options.SqlVersion = scriptOptions.SqlVersion;
            }
        }
        /// <summary>
        /// SQL Query string parse and format
        /// </summary>
        /// <param name="sqlScript">Unformated and unparsed SQL Query String</param>        
        /// <param name="useScriptOptions">Do we use special format options or by Default. If set to useScriptOptions true and useConfFile false 
        /// gets the settings from Form.</param>
        /// <param name="useConfFile">Do we use the configuration for formats from CustomFormatConfiguration.xlsx file?
        /// Both useScriptOptions and useConfFile should be true!</param>
        /// <param name="confFilePath">The full path to the file "CustomFormatConfiguration.xlsx" holding the custom format logic </param>
        /// <returns></returns>
        public string SQLScriptFormater(string sqlScript, bool useScriptOptions, bool useConfFile, string confFilePath)
        {
            try
            {
                string strFormattedSQL = null;
                using (TextReader rdr = new StringReader(sqlScript))
                {
                    TSql130Parser parser = new TSql130Parser(true);
                    IList<ParseError> errors = null;
                    TSqlFragment tree = parser.Parse(rdr, out errors);

                    if (errors.Count > 0)
                    {
                        foreach (ParseError err in errors)
                        {
                            strFormattedSQL = strFormattedSQL + err.Message + "\rn";
                        }
                        return strFormattedSQL;
                    }
                    Sql130ScriptGenerator srcGen = new Sql130ScriptGenerator();
                    if (useScriptOptions)
                    {
                        //if we use the Excel ConfigFile
                        if (useConfFile)
                        {
                            SqlScriptGeneratorOptions optionsConfig = new SqlScriptGeneratorOptions();
                            //string confFilePath = System.Environment.CurrentDirectory + "\\" + "CustomFormattingSettings.xlsx";
                            try
                            {
                                if (!File.Exists(confFilePath))
                                {
                                    throw new FileNotFoundException("File not found in App directory", "CustomFormatConfiguration.xlsx");
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                MessageBox.Show("Error: " + ex.Message + "\n" + ex.FileName);
                            }

                            DataTable formatConfiguration = ReadExcelFile.getExcellToDtbl(confFilePath);
                            foreach (DataRow dr in formatConfiguration.Rows)
                            {
                                setScriptOptions(ref optionsConfig, null, dr);
                            }
                            addScriptOptionsToScriptGen(optionsConfig, ref srcGen);
                        }
                        //If we use the interface 
                        else
                        {
                            //Search for our GroupBox in SQLFormatForm
                            GroupBox formatSettings = new GroupBox();
                            IEnumerable<Control> listControls = null;
                            foreach (System.Windows.Forms.Form form in Application.OpenForms)
                            {
                                if (form.Name == "SQLFormatForm")
                                {
                                    listControls = GetAll(form, "gbFormatSettings");
                                }
                            }
                            var control = listControls.First();
                            formatSettings = control as GroupBox;
                            //Loop trought all controls in the group box and set format Settings
                            SqlScriptGeneratorOptions optionsConfig = new SqlScriptGeneratorOptions();
                            foreach (Control ctrl in formatSettings.Controls)
                            {
                                if ((ctrl is TextBox) || (ctrl is ComboBox) || (ctrl is CheckBox))
                                {
                                    setScriptOptions(ref optionsConfig, ctrl, null);
                                }
                            }
                            addScriptOptionsToScriptGen(optionsConfig, ref srcGen);
                        }
                    }
                    srcGen.GenerateScript(tree, out strFormattedSQL);
                    return strFormattedSQL;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Script formater only for parsing the query
        public string SQLScriptFormater(string sqlScript, bool onlyParse)
        {
            try
            {
                string strFormattedSQL = string.Empty;
                using (TextReader rdr = new StringReader(sqlScript))
                {
                    TSql130Parser parser = new TSql130Parser(true);
                    IList<ParseError> errors = null;
                    TSqlFragment tree = parser.Parse(rdr, out errors);

                    if (errors.Count > 0)
                    {
                        foreach (ParseError err in errors)
                        {
                            strFormattedSQL = strFormattedSQL + err.Message + "\r\n";
                        }
                        return strFormattedSQL;
                    }
                    else
                    {
                        return "true";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Retursn collection of controls for choosen name and root control
        public IEnumerable<Control> GetAll(Control control, string controlName)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, controlName))
                                      .Concat(controls)
                                      .Where(c => c.Name == controlName);
        }
    }
}

