namespace MSSQLProcGeneratorByKmeta.GenerationClasses
{
    public class Operations
    {
        public Operations(string file, TableData.Operation oper, string fname, string operName)
        {
            sqlTemplatefile = file;
            templateOperation = oper;
            fileNameTemplate = fname;
            operationNameTamplate = operName;
        }
        public string sqlTemplatefile { get; set; }
        public TableData.Operation templateOperation { get; set; }
        public string fileNameTemplate { get; set; }
        public string operationNameTamplate { get; set; }
    }
}
