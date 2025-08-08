namespace MicroApi.Models
{
    public class MiscReceipt
    {
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; } = 1;
        public string? REF_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? RECON_DATE { get; set; }
        public int? PDC_ID { get; set; }
        public bool? IS_CLOSED { get; set; }
        public int? PARTY_ID { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? PARTY_REF_NO { get; set; }
        public bool? IS_PASSED { get; set; }
        public int? SCHEDULE_NO { get; set; }
        public string NARRATION { get; set; }
        public int? USER_ID { get; set; }
        public int? VERIFY_USER_ID { get; set; }
        public int? APPROVE1_USER_ID { get; set; }
        public int? APPROVE2_USER_ID { get; set; }
        public int? APPROVE3_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public string? ADD_TIME { get; set; }
        public int? CREATED_STORE_ID { get; set; }

        public List<MiscReceiptDetail> DETAILS { get; set; }
    }
    public class MiscReceiptDetail
    {
        public int? SL_NO { get; set; }
        public string? BILL_NO { get; set; }
        public int? HEAD_ID { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string? LEDGER_NAME { get; set; }
        public string? PARTICULARS { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
        public int? JOB_ID { get; set; }
        public string? CREATED_STORE_ID { get; set; }
        public string? STORE_AUTO_ID { get; set; }
    }
}
