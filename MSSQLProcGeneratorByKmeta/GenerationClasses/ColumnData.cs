namespace MSSQLProcGeneratorByKmeta.GenerationClasses
{
    /// <summary>
    /// Holds infomation about column in table
    /// </summary>
    public class ColumnData
    {
        public string ColumnName { get; set; }
        public string ColumnDataType { get; set; }
        public bool isNullable { get; set; }
        public string equalsSign { get; set; }
        public bool isFK { get; set; }

        public ColumnData()
        {
            ColumnName = string.Empty;
            ColumnDataType = string.Empty;
            isNullable = false;
            if (!isNullable)
            {
                equalsSign = "";
            }
            else
            {
                equalsSign = " = ";
            }
        }
    }
}