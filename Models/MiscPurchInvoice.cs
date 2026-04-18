namespace MicroApi.Models
{
    public class MiscPurchInvoice
    {
        public int? TRANS_ID { get; set; }
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? PURCH_DATE { get; set; }
        public int? SUPP_ID { get; set; }
        public int? FIN_ID { get; set; }
        public short? PURCH_TYPE { get; set; }
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public bool? IS_APPROVED { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? REF_NO { get; set; }
        public string? PURCH_NO { get; set; }
        public List<MiscPurchDetail>? Details { get; set; }
    }
    public class MiscPurchDetail
    {
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? VAT_PERC { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? HEAD_ID { get; set; }
        public string? REMARKS { get; set; }
    }
    
    public class MiscPurchList
    {
        public int ID { get; set; }
        public int TRANS_ID { get; set; }
        public string PURCH_NO { get; set; }
        public DateTime? PURCH_DATE { get; set; }
        public int SUPP_ID { get; set; }
        public int STORE_ID { get; set; }
        public string SUPP_NAME { get; set; }
        public string STORE { get; set; }
        public float NET_AMOUNT { get; set; }
        public string NARRATION { get; set; }
        public string STATUS { get; set; }
        public string PO_NO { get; set; }
    }

    public class MiscPurchResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public MiscPurchInvoice? Data { get; set; }
        public List<MiscPurchList>? List { get; set; }
    }
}
