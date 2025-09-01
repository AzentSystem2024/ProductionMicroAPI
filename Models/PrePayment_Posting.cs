namespace MicroApi.Models
{
    public class PrePayment_Posting
    {
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public List<PrepaymentPostingDetail> PREPAY_DETAIL { get; set; }
    }
    public class PrepaymentPostingDetail
    {
        public int? ID { get; set; }              
        public DateTime? DUE_DATE { get; set; }
        public double? DUE_AMOUNT { get; set; }
    }
    public class PrePayment_PostingEdit
    {
        public int TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public List<PrepaymentPostingEditDetail>? PREPAY_DETAIL { get; set; }
    }
    public class PrepaymentPostingEditDetail
    {
        public int? ID { get; set; }
        public DateTime? DUE_DATE { get; set; }
        public double? DUE_AMOUNT { get; set; }
        public decimal? DR_AMOUNT { get; set; }
        public decimal? CR_AMOUNT { get; set; }
    }
    public class PrepaymentPostingResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class PrePayment_RequestList
    {
        public int ID { get; set; }
        public string INVOICE_NO { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public string DR_LEDGER { get; set; }
        public string CR_LEDGER { get; set; }
        public double DUE_AMOUNT { get; set; }
        public string NARRATION { get; set; }
    }
    public class PrePayment_RequestResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PrePayment_RequestList> Data { get; set; }
    }
    public class PrePayment_PostingRequest
    {
        public DateTime DUE_DATE { get; set; }
    }
    public class PrePayment_PostingListHeader
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string DOC_NO { get; set; }
        public string INVOICE_NO { get; set; }
        public string? TRANS_DATE { get; set; }
        public string TRANS_STATUS { get; set; }
        public string? NARRATION { get; set; }
        public string SUPP_NAME { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public List<PrePayment_PostingListDetail> Details { get; set; }
    }

    public class PrePayment_PostingListDetail
    {
        public string DUE_DATE { get; set; }
        public decimal DUE_AMOUNT { get; set; }
    }
    public class PrePayment_PostingListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PrePayment_PostingListHeader> Data { get; set; }
    }
    public class PostingSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string DOC_NO { get; set; }
        public string INVOICE_NO { get; set; }
        public string? TRANS_DATE { get; set; }
        public string TRANS_STATUS { get; set; }
        public string? NARRATION { get; set; }
        public string SUPP_NAME { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public List<PostingSelectDetail> PREPAY_DETAIL { get; set; }
    }
    public class PostingSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PostingSelect> Data { get; set; }
    }
    public class PostingSelectDetail
    {
        public int ID { get; set; }
        public string DUE_DATE { get; set; }
        public decimal DUE_AMOUNT { get; set; }
        public decimal DR_AMOUNT { get; set; }
        public decimal CR_AMOUNT { get; set; }
    }
}
