namespace MicroApi.Models
{
    public class ImportTemplateColoumns
    {
        public int ID { get; set; }
        public string COLUMN_NAME { get; set; }
        public string COLUMN_TITLE { get; set; }
        public bool IS_MANDATORY { get; set; }
    }
    public class ImportTemplateColoumnResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ImportTemplateColoumns> data { get; set; }
    }
}
