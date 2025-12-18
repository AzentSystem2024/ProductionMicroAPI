namespace MicroApi.Models
{
    public class Trout_Invoice
    {
        public int COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? REF_NO { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? CUST_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public List<TroutSaleDetail> SALE_DETAILS { get; set; }
    }
    public class TroutSaleDetail
    {
        public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? TAXABLE_AMOUNT { get; set; }
        public decimal? TAX_PERC { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? DN_DETAIL_ID { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
    }
    public class Trout_InvoiceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class Trout_InvoiceUpdate
    {
        public int TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public string? REF_NO { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? CUST_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public List<TroutSaleDetail>? SALE_DETAILS { get; set; }
    }
    public class PendingDeliverydataRequest
    {
        public int CUST_ID { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class Trout_InvoiceListRequest
    {
        public int COMPANY_ID { get; set; }
    }
    public class PendingDeliverydataResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PendingDeliverydata> Data { get; set; }
    }
    public class PendingDeliverydata
    {
        public int DN_DETAIL_ID { get; set; }
        public string ART_NO { get; set; }
        public string DN_DATE { get; set; }
        public string ARTICLE { get; set; }
        public double TOTAL_PAIR_QTY { get; set; }

    }
    public class Trout_InvoiceList
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string DOC_NO { get; set; }
        public string INVOICE_DATE { get; set; }
        public int CUST_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float TAX_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public string CUST_NAME { get; set; }

    }
    public class Trout_InvoiceListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<Trout_InvoiceList> Data { get; set; }
    }
    public class Trout_InvoiceSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string DOC_NO { get; set; }
        public string TRANS_DATE { get; set; }
        public int CUST_ID { get; set; }
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
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public string GST_NO { get; set; }
        public string PAN_NO { get; set; }
        public string CIN { get; set; }
        public string DELIVERY_ADD1 { get; set; }
        public string DELIVERY_ADD2 { get; set; }
        public string DELIVERY_ADD3 { get; set; }
        public string MOBILE { get; set; }
        public List<TroutSaleDetailSelect> SALE_DETAILS { get; set; }

    }
    public class TroutSaleDetailSelect
    {
        public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? TAXABLE_AMOUNT { get; set; }
        public decimal? TAX_PERC { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? DN_DETAIL_ID { get; set; }
        public string ART_NO { get; set; }
        public string DN_DATE { get; set; }
        public string ARTICLE { get; set; }
        public double TOTAL_PAIR_QTY { get; set; }
        public decimal? CGST {  get; set; }
        public decimal? SGST { get; set; }

    }
    public class Trout_InvoiceSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<Trout_InvoiceSelect> Data { get; set; }
    }
    public class TroutInvResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int INVOICE_NO { get; set; }
    }
    public class TroutCust_stateName
    {
        public int ID { get; set; }
        public string? DESCRIPTION { get; set; }
        public int STATE_ID { get; set; }
        public string? STATE_NAME { get; set; }
    }
}
