namespace MicroApi.Models
{
    public class ItemProperty5
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string DESCRIPTION { get; set; }

        public int COMPANY_ID { get; set; }

        public string? IS_DELETED { get; set; }
        public string? COMPANY_NAME { get; set; }
    }
    public class ItemProperty5Response
    {
        public string flag { get; set; }
        public string message { get; set; }
        public ItemProperty5 data { get; set; }
    }
}
