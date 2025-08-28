namespace MicroApi.Models
{
    public class Receipt
    {

        public int? TRANS_TYPE { get; set; }
        public string? REC_NO { get; set; }
        public string? REC_DATE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? RECEIPT_NO { get; set; }
        public bool? IS_DIRECT { get; set; }
        public string? REF_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? RECON_DATE { get; set; }
        public int? PDC_ID { get; set; }
        public bool? IS_CLOSED { get; set; }
        public int? UNIT_ID { get; set; }
        public int? DISTRIBUTOR_ID { get; set; }
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
        public double? NET_AMOUNT { get; set; }

        public List<CustomerReceiptDetail> REC_DETAIL { get; set; }
    }
    public class CustomerReceiptDetail
    {
        public int BILL_ID { get; set; }
        public double AMOUNT { get; set; }
    }
    public class ReceiptResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class PendingInvoiceListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PendingInvoiceItem> Data { get; set; }
    }

    public class PendingInvoiceItem
    {
        public string INVOICE_NO { get; set; }
        public string INVOICE_DATE { get; set; }
        public string REF_NO { get; set; }
        public string NARRATION { get; set; }
        public double NET_AMOUNT { get; set; }
        public double SETTLED_TILL_DATE { get; set; }
        public double PENDING_AMOUNT { get; set; }
        public int BILL_ID { get; set; }
    }
    public class ReceiptListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<ReceiptListItem> Data { get; set; }
    }

    public class ReceiptListItem
    {
        public int TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string REC_DATE { get; set; }
        public int UNIT_ID { get; set; }
        public int DISTRIBUTOR_ID { get; set; }
        public string NARRATION { get; set; }
        public int PAY_TYPE_ID { get; set; }
        public string PAY_TYPE_NAME { get; set; }
        public int PAY_HEAD_ID { get; set; }
        public double NET_AMOUNT { get; set; }
        public double RECEIVED_AMOUNT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public string CUST_NAME { get; set; }
        public int? PDC_ID { get; set; }

    }
    public class ReceiptUpdate
    {
        public int? TRANS_ID { get; set; }
        public int? REC_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? REC_NO { get; set; }
        public string? REC_DATE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int? RECEIPT_NO { get; set; }
        public bool? IS_DIRECT { get; set; }
        public string? REF_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? RECON_DATE { get; set; }
        public int? PDC_ID { get; set; }
        public bool? IS_CLOSED { get; set; }
        public int? UNIT_ID { get; set; }
        public int? DISTRIBUTOR_ID { get; set; }
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
        public double? NET_AMOUNT { get; set; }

        public List<ReceiptDetail> REC_DETAIL { get; set; }
    }

    public class ReceiptDetail
    {
        public int BILL_ID { get; set; }
        public double AMOUNT { get; set; }
        public int? SL_NO { get; set; }
        public string? INVOICE_NO { get; set; }
        public string? INVOICE_DATE { get; set; }
        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public double? NET_AMOUNT { get; set; }
        public double? SETTLED_TILL_DATE { get; set; }
        public double? PENDING_AMOUNT { get; set; }
    }

    public class ReceiptSelect
    {
        public int TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public int TRANS_TYPE { get; set; }
        public string REC_DATE { get; set; }
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int FIN_ID { get; set; }
        public int TRANS_STATUS { get; set; }
        public string REF_NO { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public int UNIT_ID { get; set; }
        public int DISTRIBUTOR_ID { get; set; }
        public string NARRATION { get; set; }
        public int PAY_TYPE_ID { get; set; }
        public int PAY_HEAD_ID { get; set; }
        public string ADD_TIME { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public int? PDC_ID { get; set; }
        public List<ReceiptDetail> REC_DETAIL { get; set; }

    }
    public class ReceiptSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<ReceiptSelect> Data { get; set; }
    }
    public class CommitReceiptRequest
    {
        public int TRANS_ID { get; set; }
        public bool IS_APPROVED { get; set; }
    }
    public class RecResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int RECEIPT_NO { get; set; }
    }
    public class ReceiptLedgerList
    {
        public int HEAD_ID { get; set; }
        public string HEAD_NAME { get; set; }
        public int GROUP_ID { get; set; }
    }
    public class ReceiptLedgerListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<ReceiptLedgerList> Data { get; set; }
    }
    public class InvoicependingRequest
    {
        public int CUST_ID { get; set; }
    }
    public class PDCListResponses
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PDCListItems> Data { get; set; }
    }
    public class PDCListItems
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int CUST_ID { get; set; }
        public int LEDGER_ID { get; set; }
        public string BENEFICIARY_NAME { get; set; }
        public string ENTRY_NO { get; set; }
        public string ENTRY_DATE { get; set; }
        public string CHEQUE_NO { get; set; }
        public string DUE_DATE { get; set; }
        public double AMOUNT { get; set; }
        public string REMARKS { get; set; }
        public string ENTRY_STATUS { get; set; }
        public string BANK_NAME { get; set; }
    }
    public class CustomerIdRequest
    {
        public int CUST_ID { get; set; }
        public int LEDGER_ID { get; set; }
    }
}
