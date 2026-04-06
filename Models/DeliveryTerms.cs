namespace MicroApi.Models
{
    public class DeliveryTerms
    {
        public int? ID { get; set; }

        public string? CODE { get; set; }

        public string? DESCRIPTION { get; set; }

        public string? IS_DELETED { get; set; }
    }
    public class DeliveryTermsResponse
    {
        public string flag { get; set; }

        public string message { get; set; }

        public DeliveryTerms data { get; set; }
    }
}
