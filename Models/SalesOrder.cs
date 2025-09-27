namespace MicroApi.Models
{
    public class SalesOrder
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? SO_NO { get; set; }
        public string? SO_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_EMAIL { get; set; }
        public int? QTN_ID { get; set; }
       // public List<int> QTN_ID_LIST { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_ID { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public List<SalesOrderDetail> Details { get; set; }
    }

    public class SalesOrderDetail
    {
        public int? ID { get; set; }
        public int? SO_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? UOM { get; set; }
        public float? QUANTITY { get; set; }
        public float? PRICE { get; set; }
        public float? DISC_PERCENT { get; set; }
        public float? AMOUNT { get; set; }
        public float? TAX_PERCENT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? TOTAL_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
        public float? DN_QTY { get; set; }
    }

    public class SalesOrderSelect
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? SO_NO { get; set; }
        public string? SO_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public string? CUST_NAME { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_EMAIL { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public string? PAY_TERM_NAME { get; set; }
        public string? DELIVERY_TERM_NAME { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_ID { get; set; }
        public string? NARRATION { get; set; }
        public List<SalesOrderDetailSelect> Details { get; set; }
    }

    public class SalesOrderDetailSelect
    {
        public int? ID { get; set; }
        public int? SO_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? ITEM_NAME { get; set; }
        public string? UOM { get; set; }
        public float? QUANTITY { get; set; }
        public float? PRICE { get; set; }
        public float? DISC_PERCENT { get; set; }
        public float? AMOUNT { get; set; }
        public float? TAX_PERCENT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? TOTAL_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
        public float? DN_QTY { get; set; }
    }

    public class SalesOrderDetailSelectResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public SalesOrderSelect Data { get; set; }
    }

    public class SalesOrderResponse
    {
        public string Flag { get; set; }
        public string Message { get; set; }
    }

    public class SalesOrderList
    {
        public int? ID { get; set; }
        public string? SO_DATE { get; set; }
        public string? SO_NO { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public int? CUST_ID { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? REF_NO { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? NARRATION { get; set; }
    }

    public class SalesOrderListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SalesOrderList> Data { get; set; }
    }
    public class ITEMS
    {
        public int? ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string? MATRIX_CODE { get; set; }
        public string? UOM { get; set; }
        public float? COST { get; set; }
        public float? STOCK_QTY { get; set; }
        public decimal? VAT_PERC { get; set; }
    }

    public class ItemListsResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ITEMS> Data { get; set; }
    }

    public class SalesOrderUpdate
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? SO_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_EMAIL { get; set; }
        public int? QTN_ID { get; set; }
        ///public List<int> QTN_ID_LIST { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_ID { get; set; }
        public string? NARRATION { get; set; }
        public List<SalesOrderDetail> Details { get; set; }
    }

    public class SalesOrderRequest
    {
        public int STORE_ID { get; set; }
    }
    public class SOQUOTATIONLIST
    {
        public int? ID { get; set; }
        public int? QTN_ID { get; set; }
        public int? CUST_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string ? ITEM_NAME { get; set; }
        public string? MATRIX_CODE { get; set; }
        public string? UOM {  get; set; }
        public float? QUANTITY { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? PRICE { get; set; }
        public float? DISC_PERCENT { get; set; }
        public float? AMOUNT { get; set; }
        public float? TAX_PERCENT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? TOTAL_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
    }
    public class SOQUOTATIONLISTResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SOQUOTATIONLIST> Data { get; set; }
    }
    public class SOQUOTATIONRequest
    {
        public int CUST_ID { get; set; }
    }
    public class LatestVocherNO
    {
        public string? VOCHERNO { get; set; }
        public int? TRANS_ID { get; set; }
    }
    public class LatestVocherNOResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<LatestVocherNO> Data { get; set; }

    }

}
    

