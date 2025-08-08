namespace MicroApi.Models
{
    public class MiscPayment
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
        public List<MiscPaymentDetail> MISC_DETAIL { get; set; }

    }
    public class MiscPaymentUpdate
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
        public List<MiscPaymentDetail> MISC_DETAIL { get; set; }

    }
    public class MiscpaymentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class MiscPaymentDetail
    {
        public int? SL_NO { get; set; }
        public int? HEAD_ID { get; set; }
        public string? REMARKS { get; set; }
        public double? AMOUNT { get; set; }
        public double? VAT_AMOUNT { get; set; }
        public double? VAT_REGN { get; set; }
        public float? VAT_PERCENT { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string? LEDGER_NAME { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }

    }
    public class MiscPaymentListItem
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
        public float AMOUNT { get; set; }
        public int HEAD_ID { get; set; }
        public string REMARKS  { get; set; }
        public float VAT_AMOUNT { get; set; }
        public float VAT_PERCENT { get; set; }
        public float VAT_REGN { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }


    }
    public class MiscPaymentListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<MiscPaymentListItem> Data { get; set; }
    }
    public class MiscPaymentSelect
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
        public double VAT_REGN { get; set; }

        public List<MiscPaymentDetail> DetailList { get; set; }
    }
    public class MiscPaymentSelectedView
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public MiscPaymentSelect Data { get; set; }
    }
    public class MiscLastDocno
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int PAYMENT_NO { get; set; }
    }

}
