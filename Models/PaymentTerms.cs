namespace MicroApi.Models
{
    public class PaymentTerms
    {
        public int? ID { get; set; }

        public string? CODE { get; set; }

        public string? DESCRIPTION { get; set; }

        public string? IS_DELETED { get; set; }

    }
    public class PaymentTermsResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public PaymentTerms data { get; set; }
    }
}
