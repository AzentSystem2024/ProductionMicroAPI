namespace MicroApi.Models
{
    public class Schema
    {
        //public int ID { get; set; }
        public string SCHEMA_NAME { get; set; }
        public int DISCOUNT { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class SchemaUpdate
    {
        public int ID { get; set; }
        public string SCHEMA_NAME { get; set; }
        public int DISCOUNT { get; set; }
        public bool IS_INACTIVE { get; set; }

    }

    public class SchemaResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public SchemaUpdate Data { get; set; }
    }
    public class SchemaListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<SchemaUpdate> Data { get; set; }
    }
   
}
