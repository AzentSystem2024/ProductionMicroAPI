namespace MicroApi.Models
{
    public class PrePayment
    {
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        //public double? TAX_PERCENT { get; set; }
        //public double? TAX_AMOUNT { get; set; }
        //public double? NET_AMOUNT { get; set; }
        public int? SUPP_ID { get; set; }
        public int? EXP_HEAD_ID { get; set; }
        public int? PREPAY_HEAD_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
        public int? NO_OF_DAYS { get; set; }
        public double? EXPENSE_AMOUNT { get; set; }
        public int? NO_OF_MONTHS { get; set; }
        public string? PARTY_NAME { get; set; }
        public int? STORE_ID { get; set; }
        public bool? IS_APPROVED { get; set; }
        public List<PrepayDetail>? PREPAY_DETAIL { get; set; }

    }
    public class PrepayDetail
    {
        public DateTime? DUE_DATE { get; set; }
        public decimal? DUE_AMOUNT { get; set; }
    }
    public class PrePaymentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class PrePaymentUpdate
    {
        public int TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        //public double? TAX_PERCENT { get; set; }
        //public double? TAX_AMOUNT { get; set; }
        //public double? NET_AMOUNT { get; set; }
        public int? SUPP_ID { get; set; }
        public int? EXP_HEAD_ID { get; set; }
        public int? PREPAY_HEAD_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
        public int? NO_OF_DAYS { get; set; }
        public double? EXPENSE_AMOUNT { get; set; }
        public int? NO_OF_MONTHS { get; set; }
        public string? PARTY_NAME { get; set; }
        public int? STORE_ID { get; set; }
        public List<PrepayDetail>? PREPAY_DETAIL { get; set; }

    }
   
    public class PrePaymentListResponse
    {
        public int flag { get; set; }       
        public string Message { get; set; }
        public List<PrePaymentListHeader> Data { get; set; }
    }
    public class PrePaymentListHeader
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string DOC_NO { get; set; }
        public string? TRANS_DATE { get; set; }
        public string TRANS_STATUS { get; set; }
        public int ID { get; set; }
        public int SUPP_ID { get; set; }
        public string SUPP_NAME { get; set; }
        public int EXP_HEAD_ID { get; set; }
        public int PREPAY_HEAD_ID { get; set; }
        public string? DATE_FROM { get; set; }
        public int NO_OF_MONTHS { get; set; }
        public int NO_OF_DAYS { get; set; }
        public string? DATE_TO { get; set; }
        public decimal EXPENSE_AMOUNT { get; set; }
        //public double TAX_PERCENT { get; set; }
        //public decimal TAX_AMOUNT { get; set; }
        //public decimal NET_AMOUNT { get; set; }
        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public string? PARTY_NAME { get; set; }
        public List<PrePaymentListDetail> Details { get; set; }
    }

    public class PrePaymentListDetail
    {
        public string DUE_DATE { get; set; }
        public decimal DUE_AMOUNT { get; set; }
    }
    public class PrePaymentListHeaderResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public PrePaymentListHeader Data { get; set; }
    }
    public class PrePaymentLastDocno
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int INVOICE_NO { get; set; }
    }
}
