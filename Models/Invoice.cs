namespace MicroApi.Models
{
    public class Invoice
    {
        public int TRANS_TYPE { get; set; }
        public int COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
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
        public string? BILL_NO { get; set; }
        public int? STORE_AUTO_ID { get; set; }
        public int? JOB_ID { get; set; }

        // SALE HEADER FIELDS
        public string? SALE_DATE { get; set; }
        public string? SALE_REF_NO { get; set; }
        public int? UNIT_ID { get; set; }
        public int? DISTRIBUTOR_ID { get; set; }
        public int? FIN_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? GST_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public List<SaleDetail> SALE_DETAILS { get; set; }
    }
    public class SaleDetail
    {
        public int? TRANSFER_SUMMARY_ID { get; set; }
        public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? DN_DETAIL_ID { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
    }
    public class InvoiceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class TransferGridResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<TransferGridItem> Data { get; set; }
    }
    public class TransferGridItem
    {
        public int DN_DETAIL_ID { get; set; }
        public string TRANSFER_NO { get; set; }
        public string TRANSFER_DATE { get; set; }
        public string ARTICLE { get; set; }
        public double TOTAL_PAIR_QTY { get; set; }
        public decimal PRICE { get; set; }
        public decimal GST_PERC { get; set; }
        public string HSN_CODE { get; set; }

    }
    public class InvoiceHeader
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string DOC_NO { get; set; }
        public string SALE_DATE { get; set; }
        public int UNIT_ID { get; set; }
        public int DISTRIBUTOR_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float GST_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public double PRICE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal GST { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public string CUST_NAME { get; set; }
        public string VEHICLE_NO { get; set; }
        public List<SaleDetailUpdate> SALE_DETAILS { get; set; } = new List<SaleDetailUpdate>();


    }
    public class InvoiceHeaderResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<InvoiceHeader> Data { get; set; }
    }
    public class InvoiceUpdate
    {
        public int? TRANS_ID { get; set; }
        public int? TRANS_TYPE { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
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
        public string? BILL_NO { get; set; }
        public int? STORE_AUTO_ID { get; set; }
        public int? JOB_ID { get; set; }

        public string? SALE_DATE { get; set; }
        public string? SALE_REF_NO { get; set; }
        public int? UNIT_ID { get; set; }
        public int? DISTRIBUTOR_ID { get; set; }
        public int? FIN_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public List<SaleDetailUpdate> SALE_DETAILS { get; set; }
    }
    public class CommitInvoiceRequest
    {
        public long TRANS_ID { get; set; }
        public bool IS_APPROVED { get; set; }
    }
    public class InvoiceHeaderSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string DOC_NO { get; set; }
        public string SALE_DATE { get; set; }
        public int DISTRIBUTOR_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float TAX_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public string? REF_NO { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? CUST_NAME { get; set; }
        public string? COMPANY_NAME { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? COMPANY_CODE { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public string? CUST_CODE { get; set; }
        public string? CUST_ADDRESS1 { get; set; }
        public string? CUST_ADDRESS2 { get; set; }
        public string? CUST_ADDRESS3 { get; set; }
        public string? CUST_ZIP { get; set; }
        public string? CUST_CITY { get; set; }
        public string? CUST_STATE { get; set; }
        public string? CUST_PHONE { get; set; }
        public string? CUST_EMAIL { get; set; }
        public string VEHICLE_NO { get; set; }
        public bool ROUND_OFF { get; set; }
        public string GST_NO { get; set; }
        public string PAN_NO { get; set; }
        public string CIN { get; set; }
        public string DELIVERY_ADD1 { get; set; }
        public string DELIVERY_ADD2 { get; set; }
        public string DELIVERY_ADD3 { get; set; }
        public string MOBILE { get; set; }
        public List<SaleDetailUpdate> SALE_DETAILS { get; set; }

    }
    public class SaleDetailUpdate
    {
        public int? TRANSFER_SUMMARY_ID { get; set; }
        public string? TRANSFER_NO { get; set; }
        public string? TRANSFER_DATE { get; set; }
        public string? ARTICLE { get; set; }
        public double?   TOTAL_PAIR_QTY { get; set; }
        public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? DN_DETAIL_ID { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
        public string? HSN_CODE { get; set; }
    }
    public class InvoiceHeaderSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<InvoiceHeaderSelect> Data { get; set; }
    }
    public class InvResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int INVOICE_NO { get; set; }
    }
    public class TransferInvoiceRequest
    {
        public int CUST_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string DATE_FROM { get; set; }
        public string DATE_TO { get; set; }
    }
    public class InvoiceCust_stateName
    {
        public int ID { get; set; }
        public string? DESCRIPTION { get; set; }
        public int STATE_ID { get; set; }
        public string? STATE_NAME { get; set; }
    }
    public class InvoiceListRequest
    {
        public int COMPANY_ID { get; set; }
        public string? DATE_FROM { get; set; }
        public string? DATE_TO { get; set; }

    }
}
