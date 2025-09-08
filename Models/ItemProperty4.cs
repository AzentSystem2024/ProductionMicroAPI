namespace MicroApi.Models
{
    public class ItemProperty4
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string DESCRIPTION { get; set; }

        public int COMPANY_ID { get; set; }

        public string? IS_DELETED { get; set; }
        public string? COMPANY_NAME { get; set; }
    }
    public class ItemProperty4Response
    {
        public string flag { get; set; }
        public string message { get; set; }
        public ItemProperty4 data { get; set; }
    }
}
