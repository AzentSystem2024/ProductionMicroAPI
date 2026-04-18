namespace MicroApi.Models
{
    public class ImportTemplate
    {
        public int ID { get; set; }
        public string TEMPLATE_NAME { get; set; }
        public string REMARKS { get; set; }
        public int UserID { get; set; }
        public List<ImportTemplateEntry> import_entry { get; set; }
    }
    public class ImportTemplateEntry
    {
        public int? ID { get; set; }
        public int? COLUMN_ID { get; set; }
        public string? COLUMN_NAME { get; set; }
        public string? COLUMN_TITLE { get; set; }
        public bool? SELECTED { get; set; }
        public int? MAX_LENGTH { get; set; }
        public bool? IS_MANDATORY { get; set; }
        public string? LIST { get; set; }

        //public List<LIST> LIST { get; set; }
    }

    public class LIST
    {
        public int ID { get; set; }

    }
    public class ImportTemplateResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ImportTemplate> data { get; set; }
    }
}
