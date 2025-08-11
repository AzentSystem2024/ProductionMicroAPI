namespace MicroApi.Models
{
    public class MiscReceipt
    {
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public List<MiscReceiptDetail> DETAILS { get; set; }
    }
    public class MiscReceiptDetail
    {
        public int? HEAD_ID { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string? LEDGER_NAME { get; set; }
        public string? REMARKS { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
      
    }
    public class MiscReceiptResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

    }
    public class MiscReceiptUpdate
    {
        public int? TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }

        public List<MiscReceiptDetail> DETAILS { get; set; }
    }
    public class MiscReceiptListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<MiscReceiptListItem> Data { get; set; }
    }
    public class MiscReceiptListItem
    {
        public int? TRANS_ID { get; set; }
        public string? VOUCHER_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public string? PARTY_NAME { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? NARRATION { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public List<MiscListDetail> DETAILS { get; set; }
    }
    public class MiscListDetail
    {
        public int? SL_NO { get; set; }
        public int? HEAD_ID { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string LEDGER_NAME { get; set; }
        public string? REMARKS { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
    }
    public class MiscReceiptViewHeader
    {
        public int TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public string PARTY_NAME { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string NARRATION { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public List<MiscListDetail> DETAILS { get; set; }
    }
    public class LasrVoucherResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public string VOUCHER_NO { get; set; }
    }
}
