namespace MicroApi.Models
{
    public class MiscPayment_GST
    {
        public int? TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public int? STORE_ID { get; set; }
        public bool? IS_APPROVED { get; set; }
        public List<MiscPaymentGSTDetail> MISC_DETAIL { get; set; }
    }
    public class MiscPayment_GSTUpdate
    {
        public int TRANS_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public int? DEPT_ID { get; set; }
        public int? STORE_ID { get; set; }
        public List<MiscPaymentGSTDetail> MISC_DETAIL { get; set; }

    }
    public class MiscPaymentGSTDetail
    {
        public int? SL_NO { get; set; }
        public int? HEAD_ID { get; set; }
        public string? REMARKS { get; set; }
        public double? AMOUNT { get; set; }
        public double? VAT_AMOUNT { get; set; }
        public string? VAT_REGN { get; set; }
        public float? VAT_PERCENT { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string? LEDGER_NAME { get; set; }
    }
    public class MiscpaymentGSTResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class MiscPaymentListGSTItem
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string TRANS_DATE { get; set; }
        public string VOUCHER_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public string NARRATION { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public int TRANS_STATUS { get; set; }
        //public float AMOUNT { get; set; }
        //public int HEAD_ID { get; set; }
        //public string REMARKS { get; set; }
        //public float VAT_AMOUNT { get; set; }
        //public float VAT_PERCENT { get; set; }
        //public string VAT_REGN { get; set; }
        public int DEPT_ID { get; set; }

    }
    public class MiscPaymentListGSTResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<MiscPaymentListGSTItem> Data { get; set; }
    }
    public class MiscPaymentGSTSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string TRANS_DATE { get; set; }
        public string VOUCHER_NO { get; set; }
        public int PAY_HEAD_ID { get; set; }
        public int PAY_TYPE_ID { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public float AMOUNT { get; set; }
        public string NARRATION { get; set; }
        public int TRANS_STATUS { get; set; }
        public string LEDGER_CODE { get; set; }
        public string LEDGER_NAME { get; set; }
        public string VAT_REGN { get; set; }
        public int DEPT_ID { get; set; }
        public List<MiscPaymentGSTDetail> DetailList { get; set; }
    }
    public class MiscPaymentGSTSelectedView
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public MiscPaymentGSTSelect Data { get; set; }
    }
    public class MiscGSTLastDocno
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int PAYMENT_NO { get; set; }
    }
}
