namespace MicroApi.Models
{
    // Used for Insert
    public class JournalHeader
    {
        public int COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string TRANS_DATE { get; set; } 
        public int? TRANS_STATUS { get; set; } = 1;
        public string REF_NO { get; set; }
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

        public List<JournalDetail> DETAILS { get; set; }
    }


    public class JournalDetail
    {
        public int? SL_NO { get; set; }
        public string BILL_NO { get; set; }
        public string LEDGER_CODE { get; set; }
        public string? LEDGER_NAME { get; set; }
        public string PARTICULARS { get; set; }
        public decimal DEBIT_AMOUNT { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public string? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
        public string? JOB_ID { get; set; }
        public string? STORE_AUTO_ID { get; set; }
    }

    public class JournalUpdateHeader
    {
        public int TRANS_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? JOURNAL_NO { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? REF_NO { get; set; }
        public int? TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
         public bool? IS_APPROVED { get; set; }
        public string? NARRATION { get; set; }
        public int? USER_ID { get; set; }
        public string? CHEQUE_NO { get; set; }
        public DateTime? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public DateTime? RECON_DATE { get; set; }
        public bool? IS_PASSED { get; set; }
        public int? PARTY_ID { get; set; }
        public string? PARTY_REF_NO { get; set; }
        public int? SCHEDULE_NO { get; set; }
        public bool? IS_CLOSED { get; set; }
        public int? VERIFY_USER_ID { get; set; }
        public int? APPROVE1_USER_ID { get; set; }
        public int? APPROVE2_USER_ID { get; set; }
        public int? APPROVE3_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? CREATED_STORE_ID { get; set; }
        public int? RECEIPT_NO { get; set; }
        public int? IS_DIRECT { get; set; }
        public int? PDC_ID { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public string? ADD_TIME { get; set; }
        public List<JournalUpdateDetail> DETAILS { get; set; }
    }

    public class JournalUpdateDetail
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? SL_NO { get; set; }
        public int? HEAD_ID { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
        public string? BILL_NO { get; set; }
        public int? JOB_ID { get; set; }
        public int? CREATED_STORE_ID { get; set; }
        public int? STORE_AUTO_ID { get; set; }
    }

    public class JournalResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
      
        public object Data { get; set; }

    }

    public class JournalListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<JournalListItem> Data { get; set; }
    }
    public class JournalListItem
    {
        public int TRANS_ID { get; set; }
        public string JOURNAL_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public string PARTY_NAME { get; set; }
        public string REF_NO { get; set; }
        public int TRANS_TYPE { get; set; }
        public string NARRATION { get; set; }
        public int TRANS_STATUS { get; set; }
        public List<JournalListDetail> DETAILS { get; set; }
    }

    public class JournalListDetail
    {
        public int ID { get; set; }
        public int? SL_NO { get; set; }
        public string BILL_NO { get; set; }
        public string LEDGER_CODE { get; set; }
        public string LEDGER_NAME { get; set; }
        public string PARTICULARS { get; set; }
        public decimal DEBIT_AMOUNT { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
    }
    public class JournalViewHeader
    {
        public int TRANS_ID { get; set; }
        public string JOURNAL_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public string PARTY_NAME { get; set; }
        public string REF_NO { get; set; }
        public int TRANS_TYPE { get; set; }
        public string NARRATION { get; set; }
        public bool IS_APPROVED { get; set; }

        public List<JournalListDetail> DETAILS { get; set; }
    }

    public class VoucherResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public string VoucherNo { get; set; }
    }
    public class JournalDetailUDT
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int? SL_NO { get; set; }
        public int? HEAD_ID { get; set; }
        public decimal? DR_AMOUNT { get; set; }
        public decimal? CR_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
        public string? BILL_NO { get; set; }
        public int? JOB_ID { get; set; }
        public int? CREATED_STORE_ID { get; set; }
        public int? STORE_AUTO_ID { get; set; }
    }
    public class ApprovalRequest
    {
        public int TRANS_ID { get; set; }
        public bool? IS_APPROVED { get; set; }
    }

}
