namespace MicroApi.Models
{
    public class AC_CreditNote
    {
            public int? TRANS_TYPE { get; set; }
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
            public string INVOICE_NO { get; set; }
            public int? UNIT_ID { get; set; }
             public int? DISTRIBUTOR_ID { get; set; }

        public List<CreditNoteDetail> NOTE_DETAIL { get; set; }
        }
        public class CreditNoteDetail
        {
            public int SL_NO { get; set; }
            public int HEAD_ID { get; set; }
            public double AMOUNT { get; set; }
            public double GST_AMOUNT { get; set; }
            public string REMARKS { get; set; }
         }

        public class CreditNoteResponse
        {
            public int flag { get; set; }
            public string Message { get; set; }
        }
    public class AC_CreditNoteUpdate
    {
        public int TRANS_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
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
        public int? UNIT_ID { get; set; }
        public int? DISTRIBUTOR_ID { get; set; }

        public List<CreditNoteDetailUpdate> NOTE_DETAIL { get; set; }
    }

    public class CreditNoteDetailUpdate
    {
        public int SL_NO { get; set; }
        public int HEAD_ID { get; set; }
        public float AMOUNT { get; set; }
        public float GST_AMOUNT { get; set; }
        public string REMARKS { get; set; }
    }
    public class CreditNoteListItem
    {
        public int TRANS_ID { get; set; }
        public string TRANS_DATE { get; set; }
        public string INVOICE_NO { get; set; }
        public int TRANS_TYPE { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float GST_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public string NARRATION { get; set; }
        public int TRANS_STATUS { get; set; }
        public int UNIT_ID { get; set; }      
        public int DISTRIBUTOR_ID { get; set; }
        public string CUST_NAME { get; set; }
    }
    public class CreditNoteListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<CreditNoteListItem> Data { get; set; }
    }
    public class AC_CreditNoteSelect
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<CreditNoteSelectedView> Data { get; set; }
    }

    public class CreditNoteSelectedView
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string NARRATION { get; set; }
        public int UNIT_ID { get; set; }           
        public int DISTRIBUTOR_ID { get; set; }
        public float NET_AMOUNT { get; set; }
        public int DOC_NO { get; set; }
        public float INVOICE_NET_AMOUNT { get; set; }
        public float ADJUSTED_AMOUNT { get; set; }
        public float RECEIVED_AMOUNT { get; set; }
        public float DUE_AMOUNT { get; set; }
        public string? PARTY_NAME { get; set; }
        public List<CreditNoteDetailUpdate> NOTE_DETAIL { get; set; }
    }
    public class CreditNoteCommitRequest
    {
        public int TRANS_ID { get; set; }
        public bool IS_APPROVED { get; set; }
    }
    public class DocResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int DOC_NO { get; set; }
    }

    public class CreditNoteInvlist
    {
        public int TRANS_ID { get; set; }
        public int INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string DATE { get; set; }
        public float NET_AMOUNT { get; set; }
        public float ADJUSTED_AMOUNT { get; set; }
        public float RECEIVED_AMOUNT { get; set; }
        public float BALANCE_AMOUNT { get; set; }
    }
    public class CreditNoteInvoiceListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<CreditNoteInvlist> Data { get; set; }
    }
    public class Pendingrequest
    {
        public int CUST_ID { get; set; }
    }
}

