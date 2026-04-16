namespace MicroApi.Models
{
    public class SalesInvoice
    {
        public int ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string BARCODE { get; set; }
        public string UOM { get; set; }
        public decimal VAT_PERC { get; set; }
        public decimal COST { get; set; }
        public string HSN_CODE { get; set; }
    }
    public class SalesInvoiceRequest
    {
        public int ITEM_ID { get; set; }
    }
    public class SalesResponse
    {
        public string Message { get; set; }
        public int Flag { get; set; }
        public List<SalesInvoice> Data { get; set; }
    }
    public class SalesInvoiceInsertRequest
    {
        public int? TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public string? REF_NO { get; set; }
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public bool IS_APPROVED { get; set; }
        public int USER_ID { get; set; }

        public List<SalesInvoiceDetail>? Details { get; set; }
    }

    public class SalesInvoiceDetail
    {
        public int? ITEM_ID { get; set; }
        public float? QUANTITY { get; set; }
        public float? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? TAX_PERC { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
    }

    public class SalesInvoiceInsertResponse
    {
        public string Message { get; set; }
        public int Flag { get; set; }
    }
    public class SalesInvoiceListHeader
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string VOUCHER_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public int CUSTOMER_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float GST_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public string CUST_NAME { get; set; }
    }
    public class SalesInvoiceListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SalesInvoiceListHeader> Data { get; set; }
    }
    public class SalesInvoiceView
    {
        // Header
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string SALE_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public int CUSTOMER_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float TAX_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public string REF_NO { get; set; }
        public string PARTY_NAME { get; set; }

        // Customer
        public string CUST_NAME { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_ADDRESS1 { get; set; }
        public string CUST_ADDRESS2 { get; set; }
        public string CUST_ADDRESS3 { get; set; }
        public string CITY { get; set; }
        public string ZIP { get; set; }
        public string CUST_PHONE { get; set; }
        public string CUST_EMAIL { get; set; }
        public string STATE_NAME { get; set; }

        // Company
        public string COMPANY_NAME { get; set; }
        public string COMPANY_CODE { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string GST_NO { get; set; }
        public string PAN_NO { get; set; }
        public string CIN { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }

        public List<SalesInvoiceItem> Details { get; set; }
    }
    public class SalesInvoiceItem
    {
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string HSN_CODE { get; set; }
        public string UOM { get; set; }
        public decimal COST { get; set; }

        public double QUANTITY { get; set; }
        public double PRICE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal TAX_PERC { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
    }
    public class SalesInvoiceViewResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public SalesInvoiceView Data { get; set; }
    }


}
