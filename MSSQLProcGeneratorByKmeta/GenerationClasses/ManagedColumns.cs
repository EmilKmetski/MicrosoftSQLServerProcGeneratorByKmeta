namespace MSSQLProcGeneratorByKmeta.GenerationClasses
{
    /// <summary>
    /// Class that holds information for the columns which are either of Inser or Update
    /// will they be a paramether or will they be used in Columns for Insert and Columns for Update
    /// </summary>
    public class ManagedColumns
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public bool UsedForParam { get; set; }
        public bool UsedForColumn { get; set; }

        public ManagedColumns()
        {
            ColumnName = string.Empty;
            ColumnValue = string.Empty;
            UsedForParam = false;
            UsedForColumn = false;
        }
    }
}
