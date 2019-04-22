using System.Collections.Generic;

namespace MSSQLProcGeneratorByKmeta.GenerationClasses
{
    /// <summary>
    /// Database Data contains all Tables in Database in list ot TableDaata Type
    /// All Insert Managed Columns in list of ManagedColumns Type
    /// All Update Managed Columns in list of ManagedColumns Type
    /// </summary>
    public class DatabaseData
    {
        public string DatabaseName { get; set; }
        public List<TableData> Tables { get; set; }
        public List<ManagedColumns> ManagedInsertColumns { get; set; }
        public List<ManagedColumns> ManagedUpdateColumns { get; set; }
        public List<ManagedColumns> ManagedSelectColumns { get; set; }
        public DatabaseData()
        {
            DatabaseName = string.Empty;
            Tables = new List<TableData>();
        }
    }
}