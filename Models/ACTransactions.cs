namespace MicroApi.Models
{
    public class JournalHeader
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
        public int? DEPT_ID { get; set; }
        public bool? IS_APPROVED { get; set; }
        public List<JournalDetail> DETAILS { get; set; }
    }


    public class JournalDetail
    {
        public int? SL_NO { get; set; }
        public string? BILL_NO { get; set; }
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

    public class JournalUpdateHeader
    {
        public int? TRANS_ID { get; set; }
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
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? RECON_DATE { get; set; }
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
        public int? DEPT_ID { get; set; }
        public List<JournalUpdateDetail> DETAILS { get; set; }
    }

    public class JournalUpdateDetail
    {
        public int? ID { get; set; }
        public int? SL_NO { get; set; }
        public string? BILL_NO { get; set; }
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
        public int? TRANS_ID { get; set; }
        public string? DOC_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public string? PARTY_NAME { get; set; }
        public string REF_NO { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? NARRATION { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? DEPT_ID { get; set; }
        public List<JournalListDetail> DETAILS { get; set; }
    }

    public class JournalListDetail
    {
        public int ID { get; set; }
        public int? SL_NO { get; set; }
        public string? BILL_NO { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string LEDGER_NAME { get; set; }
        public string? PARTICULARS { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
    }
    public class JournalViewHeader
    {
        public int TRANS_ID { get; set; }
        public string DOC_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public string PARTY_NAME { get; set; }
        public string REF_NO { get; set; }
        public int TRANS_TYPE { get; set; }
        public string NARRATION { get; set; }
        public bool IS_APPROVED { get; set; }
        public int? DEPT_ID { get; set; }
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
    public class JVlistRequest
    {
        public int COMPANY_ID { get; set; }
        public string? DATE_FROM { get; set; }
        public string? DATE_TO { get; set; }
    }
    //-------------------END---------------------------//

    public class AC_DebitNote
    {
        public int TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? RECEIPT_NO { get; set; }
        public int? IS_DIRECT { get; set; }
        public string? REF_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? RECON_DATE { get; set; }
        public int? PDC_ID { get; set; }
        public bool? IS_CLOSED { get; set; }
        public int? PARTY_ID { get; set; }
        public int? SUPP_ID { get; set; }   
        public string? PARTY_NAME { get; set; }
        public string? PARTY_REF_NO { get; set; }
        public bool? IS_PASSED { get; set; }
        public int? SCHEDULE_NO { get; set; }
        public string NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? VERIFY_USER_ID { get; set; }
        public int? APPROVE1_USER_ID { get; set; }
        public int? APPROVE2_USER_ID { get; set; }
        public int? APPROVE3_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public string? ADD_TIME { get; set; }
        public int? CREATED_STORE_ID { get; set; }
        public int? INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string? BILL_NO { get; set; }
        public int? JOB_ID { get; set; }
        public int? STORE_AUTO_ID { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public List<DebitNoteDetail> NOTE_DETAIL { get; set; }
    }

    public class DebitNoteDetail
    {
        public int SL_NO { get; set; }
        public int HEAD_ID { get; set; }
        public double AMOUNT { get; set; }
        public double GST_AMOUNT { get; set; }
        public string REMARKS { get; set; }
        public float? GST_PERC { get; set; }
        public string? HSN_CODE { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
    }
    public class DebitNoteResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class AC_DebitNoteUpdate
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? RECEIPT_NO { get; set; }
        public int? IS_DIRECT { get; set; }
        public string? REF_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? RECON_DATE { get; set; }
        public int? PDC_ID { get; set; }
        public bool? IS_CLOSED { get; set; }
        public int? PARTY_ID { get; set; }
        public int? SUPP_ID { get; set; }    
        public string? PARTY_NAME { get; set; }
        public string? PARTY_REF_NO { get; set; }
        public bool? IS_PASSED { get; set; }
        public int? SCHEDULE_NO { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? VERIFY_USER_ID { get; set; }
        public int? APPROVE1_USER_ID { get; set; }
        public int? APPROVE2_USER_ID { get; set; }
        public int? APPROVE3_USER_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public string? ADD_TIME { get; set; }
        public int? CREATED_STORE_ID { get; set; }
        public int? INVOICE_ID { get; set; }
        public string? INVOICE_NO { get; set; }
        public string? BILL_NO { get; set; }
        public int? JOB_ID { get; set; }
        public int? STORE_AUTO_ID { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public List<DebitNoteDetail> NOTE_DETAIL { get; set; }
    }
    public class DebitNoteListItem
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string TRANS_DATE { get; set; }
        public string INVOICE_NO { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float GST_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public string NARRATION { get; set; }
        public int SUPP_ID { get; set; }
        public int TRANS_STATUS { get; set; }
        public string SUPP_NAME { get; set; }
        public string DOC_NO { get; set; }
    }
    public class DebitNoteListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DebitNoteListItem> Data { get; set; }
    }
    public class DebitNoteSelectedView
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string NARRATION { get; set; }
        public int SUPP_ID { get; set; }
        public float NET_AMOUNT { get; set; }
        public float DUE_AMOUNT { get; set; }
        public string DOC_NO { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? COMPANY_NAME { get; set; } //for select
        public string? ADDRESS1 { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public string? COMPANY_CODE { get; set; }
        public string? SUPP_NAME { get; set; }
        public string? SUPP_ADDRESS1 { get; set; }
        public string? SUPP_ADDRESS2 { get; set; }
        public string? SUPP_ADDRESS3 { get; set; }
        public string? SUPP_EMAIL { get; set; }
        public string? SUPP_PHONE { get; set; }
        public string? SUPP_CODE { get; set; }
        public string? SUPP_STATE_NAME { get; set; }
        public string? SUPP_ZIP { get; set; }
        public string? SUPP_CITY { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public string? GST_NO { get; set; }
        public string? PAN_NO { get; set; }
        public string? CIN { get; set; }
        public List<DebitNoteDetail> NOTE_DETAIL { get; set; }
    }
    public class AC_DebitNoteSelect
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DebitNoteSelectedView> Data { get; set; }
    }
    public class DebitNoteCommitRequest
    {
        public int TRANS_ID { get; set; }
        public bool IS_APPROVED { get; set; }
    }
    public class DocNoResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int DOC_NO { get; set; }
    }
    public class JournalListFilter
    {
        public int COMPANY_ID { get; set; }
        public int TRANS_TYPE { get; set; }
    }
    public class DebitInvoiceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DebitInvoicelist> Data { get; set; }
    }

    public class DebitInvoicelist
    {
        public int BILL_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string PURCH_DATE { get; set; }
        public string SUPP_INV_DATE { get; set; }
        public string SUPP_INV_NO { get; set; }
        public double NET_AMOUNT { get; set; }
        public double PENDING_AMOUNT { get; set; }
        public string HSN_CODE { get; set; }
        public decimal GST_PERC { get; set; }

    }
    public class DebitInvoiceRequest
    {
        public int SUPP_ID { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class DebitlistRequest
    {
        public int COMPANY_ID { get; set; }
        public string? DATE_FROM { get; set; }
        public string? DATE_TO { get; set; }
    }
}
