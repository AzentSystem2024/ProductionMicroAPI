namespace MicroApi.Models
{
    public class VatClass
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? CODE { get; set; }

        public string? VAT_NAME { get; set; }

        public decimal? VAT_PERC { get; set; }

        public string? IS_DELETED { get; set; }
    }
    public class VatClassResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public VatClass data { get; set; }
    }
    public class VatClassList
    {
        public int COMPANY_ID { get; set; }
    }
}
