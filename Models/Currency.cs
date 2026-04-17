namespace MicroApi.Models
{
    public class Currency
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string SYMBOL { get; set; }
        public string DESCRIPTION { get; set; }

        public string? EXCHANGE { get; set; }
        public string? FRACTION_UNIT { get; set; }

        public int COMPANY_ID { get; set; }

        public string? COMPANY_NAME { get; set; }

        public bool IS_INACTIVE { get; set; }

        public string? IS_DELETED { get; set; }
    }
    public class CurrencyResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Currency? data { get; set; }
    }
}
