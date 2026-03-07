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
        public decimal? CGST_PERC { get; set; }
        public decimal? SGST_PERC { get; set; }
        public decimal? IGST_PERC { get; set; }
        public int? CGST_INPUT_HEAD_ID { get; set; }
        public int? CGST_OUTPUT_HEAD_ID { get; set; }
        public int? SGST_INPUT_HEAD_ID { get; set; }
        public int? SGST_OUTPUT_HEAD_ID { get; set; }
        public int? IGST_INPUT_HEAD_ID { get; set; }
        public int? IGST_OUTPUT_HEAD_ID { get; set; }
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
