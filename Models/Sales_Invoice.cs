namespace MicroApi.Models
{
    public class Sales_Invoice
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
        public int? CUST_ID { get; set; }
        public int? FIN_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? GST_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }

        // FOR TROUT SUMMARY UPDATE
        public List<SaleDetails> SALE_DETAILS { get; set; }
    }
    public class SaleDetails
    {
        public int? DELIVERY_NOTE_ID { get; set; }
        public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
    }
    public class SalesInvoiceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class Sale_InvoiceUpdate
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
        public int? CUST_ID { get; set; }
        public int? FIN_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public List<SalesDetailUpdate> SALE_DETAILS { get; set; }
    }
    public class SalesDetailUpdate
    {
        public int? DELIVERY_NOTE_ID { get; set; }
        public string? TRANSFER_NO { get; set; }
        public string? TRANSFER_DATE { get; set; }
        public string? ARTICLE { get; set; }
        public double? TOTAL_PAIR_QTY { get; set; }
        //public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
    }
    public class DeliveryGridResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DeliveryGridItem> Data { get; set; }
    }
    public class DeliveryGridItem
    {
        public int DELIVERY_NOTE_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DELIVERY_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public double QUANTITY { get; set; }

    }
    public class DeliveryInvoiceRequest
    {
        public int CUST_ID { get; set; }
    }
    public class SalesInvoiceHeader
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string SALE_NO { get; set; }
        public string SALE_DATE { get; set; }
        public int CUST_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public string CUST_NAME { get; set; }

    }
    public class SalesInvoiceHeaderResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalesInvoiceHeader> Data { get; set; }
    }
    public class SalesInvoiceHeaderSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string SALE_NO { get; set; }
        public string SALE_DATE { get; set; }
        public int CUST_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public float TAX_AMOUNT { get; set; }
        public string? REF_NO { get; set; }
        public List<SalesInvoiceDetailUpdate> SALE_DETAILS { get; set; }

    }
    public class SalesInvoiceDetailUpdate
    {
        public int? DELIVERY_NOTE_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DELIVERY_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public double QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }

    }
    public class SalesInvselectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalesInvoiceHeaderSelect> Data { get; set; }
    }
}
