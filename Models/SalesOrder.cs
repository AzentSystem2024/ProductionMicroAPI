namespace MicroApi.Models
{
    public class SalesOrder
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? SO_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? USER_ID { get; set; }
        public string? REMARKS { get; set; }
        public int? DELIVERY_ADDRESS { get; set; }
        public int? WAREHOUSE { get; set; }
        public float? TOTAL_QTY { get; set; }
        public int? SUBDEALER_ID { get; set; }
        public List<SalesOrderDetail> Details { get; set; }
    }

    public class SalesOrderDetail
    {
        public string? PACKING_ID { get; set; }
        public int? BRAND_ID { get; set; }
        public int? ARTICLE_TYPE { get; set; }
        public string? CATEGORY_ID { get; set; }
        public string? ART_NO { get; set; }
        public string? COLOR_ID { get; set; }
        public string? CONTENT { get; set; }
        public float? QUANTITY { get; set; }

    }
    public class SalesOrderSelect
    {
        public int ID { get; set; }
        public int STORE_ID { get; set; }
        public string SO_NO { get; set; }
        public string SO_DATE { get; set; }
        public int CUST_ID { get; set; }
        public string CUST_NAME { get; set; }
        public int TRANS_ID { get; set; }
        public int DELIVERY_ADDRESS { get; set; }
        public int WAREHOUSE { get; set; }
        public string REMARKS { get; set; }
        public float TOTAL_QTY { get; set; }
        public int TRANS_STATUS { get; set; }
        //public string DELIVERY_ADDRESS { get; set; }
        public string ADDRESS { get; set; }
        public int? SUBDEALER_ID { get; set; }
        public List<SalesOrderDetailSelect> Details { get; set; }
    }

    public class SalesOrderDetailSelect
    {
        public string? PACKING_ID { get; set; }
        public int? BRAND_ID { get; set; }
        public int? ARTICLE_TYPE { get; set; }
        public string? CATEGORY_ID { get; set; }
        public string? ART_NO { get; set; }
        public string? COLOR_ID { get; set; }
        public string? CONTENT { get; set; }
        public float? QUANTITY { get; set; }

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
        public string? SO_NO { get; set; }
        public string? SO_DATE { get; set; }
        public int? STORE_ID { get; set; }
        public int? CUST_ID { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public int? DELIVERY_ADDRESS_ID { get; set; }
        public int? WAREHOUSE { get; set; }
        public string? REMARKS { get; set; }
        public float? TOTAL_QTY { get; set; }
        public int? TRANS_ID { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? DELIVERY_ADDRESS { get; set; }
        public string? ADDRESS { get; set; }
        public int? SUBDEALER_ID { get; set; }
    }


    public class SalesOrderListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SalesOrderList> Data { get; set; }
    }

    public class ITEMS
    {
        public int? ARTICLE_ID { get; set; }
        public int? PACKING_ID { get; set; }
        public string DESCRIPTION { get; set; }
       
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
        public int? USER_ID { get; set; }
        public string? REMARKS { get; set; }
        public int? DELIVERY_ADDRESS { get; set; }
        public int? WAREHOUSE { get; set; }
        public float? TOTAL_QTY { get; set; }
        public int? SUBDEALER_ID { get; set; }
        public List<SalesOrderDetail> Details { get; set; }
    }

    public class SalesOrderRequest
    {
        public string BRAND_ID { get; set; }
        public string? ARTICLE_TYPE { get; set; }
        public string? CATEGORY_ID { get; set; }
        public string? ARTNO_ID { get; set; }
        public string? COLOR { get; set; }
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
    }
    public class LatestVocherNOResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<LatestVocherNO> Data { get; set; }

    }
    public class PackingPairRequest
    {
        public int PACKING_ID { get; set; }
    }

    public class PairDtl
    {
        public int? ID { get; set; }
        public decimal? PAIR_QTY { get; set; }
        public string? COMBINATION { get; set; }
    }

    public class PairResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PairDtl>? Data { get; set; }
    }
    public class WarehouseResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<WarehouseDtl> Data { get; set; }
    }

    public class WarehouseDtl
    {
        public int? ID { get; set; }
        public string WAREHOUSE_NAME { get; set; }
    }
    public class Subdealers
    {
        public int ID { get; set; }
        public string? SUBDEALER { get; set; }
    }
    public class SubdealerRequest
    {
        public int? DEALER_ID { get; set; }
    }
}
    

