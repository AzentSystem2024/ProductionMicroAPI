namespace MicroApi.Models
{
    public class SupplierPayment
    {
        public int? TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? TRANS_DATE { get; set; }
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
        public int? SUPPLIER_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public List<SupplierPaymentDetail> SUPP_DETAIL { get; set; }
    }
    public class SupplierPaymentDetail
    {
        public int BILL_ID { get; set; }
        public double AMOUNT { get; set; }
    }
    public class SupplierPaymentUpdate
    {
        public int? TRANS_ID { get; set; }
        //public int? PAY_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? TRANS_DATE { get; set; }
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
        public int? SUPPLIER_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public List<SupplierPaymentDetail> SUPP_DETAIL { get; set; }
    }
    public class SupplierPaymentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class SupplierPaymentListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SupplierPaymentListItem> Data { get; set; }
    }

    public class SupplierPaymentListItem
    {
        public int TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string PAY_DATE { get; set; }
        public int SUPP_ID { get; set; }
        public string NARRATION { get; set; }
        public int PAY_TYPE_ID { get; set; }
        public string PAY_TYPE_NAME { get; set; }
        public int PAY_HEAD_ID { get; set; }
        public double NET_AMOUNT { get; set; }
        public double RECEIVED_AMOUNT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
    }
    public class SupplierPaymentSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string PAY_DATE { get; set; }
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int FIN_ID { get; set; }
        public int TRANS_STATUS { get; set; }
        public string REF_NO { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public int SUPP_ID { get; set; }
        public string NARRATION { get; set; }
        public int PAY_TYPE_ID { get; set; }
        public int PAY_HEAD_ID { get; set; }
        public string ADD_TIME { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public List<SupplierDetail> PAY_DETAIL { get; set; }

    }
    public class SupplierDetail
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
    public class SupplierSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SupplierPaymentSelect> Data { get; set; }
    }
}
