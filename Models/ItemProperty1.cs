namespace MicroApi.Models
{
    public class ItemProperty1
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string DESCRIPTION { get; set; }

        public int COMPANY_ID { get; set; }

        public bool? IS_DELETED { get; set; }
        public string? COMPANY_NAME { get; set; }
    }
    public class ItemProperty1Response
    {
        public string flag { get; set; }
        public string message { get; set; }
        public ItemProperty1 data { get; set; }
    }
}
