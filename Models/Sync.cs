namespace MicroApi.Models
{
    public class Sync
    {
        public int COMPANY_ID { get; set; }
        public int USER_ID { get; set; }
        public int FIN_ID { get; set; }
        public double ADDL_COST { get; set; }
        public string ADDL_DESCRIPTION { get; set; }
        public List<SyncArticleProduction> Articles { get; set; }
    }
    public class SyncArticleProduction
    {
        public long ARTICLE_PRODUCTION_ID { get; set; }
        public long ARTICLE_ID { get; set; }
        //public int PAIRS { get; set; }
        public int BOX_ID { get; set; }
        public string BARCODE { get; set; }
        public float PRICE { get; set; }
        public DateTime PRODUCTION_DATE { get; set; }
    }
    public class SyncResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
    public class PackProductionSync
    {
        public int COMPANY_ID { get; set; }
        public int USER_ID { get; set; }
        public int FIN_ID { get; set; }
        public List<PackProductionItem> PackItems { get; set; }
        public List<SyncArticleProduction> Articles { get; set; }

    }

    public class PackProductionItem
    {
        public long PACK_PRODUCTION_ID { get; set; }
        public long PACKING_ID { get; set; }
        public int BOX_SERIAL { get; set; }
        public string BARCODE { get; set; }
        public float BOX_PRICE { get; set; }
        public DateTime PRODUCTION_DATE { get; set; }
    }
    public class ProductionTransferOut
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }              // From store (Production)
        public DateTime TRANSFER_DATE { get; set; }
        public int DEST_STORE_ID { get; set; }         // To store (Warehouse)
        public double NET_AMOUNT { get; set; }
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }
        public string NARRATION { get; set; }
        public int? REASON_ID { get; set; }

        public List<ProductionTransferItem> TransferItems { get; set; }
    }
    public class ProductionTransferItem
    {
        public int? ITEM_ID { get; set; }
        public string? UOM { get; set; }
        public decimal? QUANTITY { get; set; }
        public double? COST { get; set; }
        public string? BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
        public int? PACKING_ID { get; set; }
    }
    public class ProductionTransferIn
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }           // Warehouse (Receiving)
        public DateTime REC_DATE { get; set; }
        public int ORIGIN_STORE_ID { get; set; }    // Production
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? ISSUE_ID { get; set; }
        public int? REASON_ID { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public List<ProductionTransferInItem> Items { get; set; }
    }

    public class ProductionTransferInItem
    {
        public int? ISSUE_DETAIL_ID { get; set; }
        public int ITEM_ID { get; set; }
        public string? UOM { get; set; }
        public decimal? QUANTITY { get; set; }
        public double? QUANTITY_ISSUED { get; set; }
        public decimal? COST { get; set; }
        public string? BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
        public int? PACKING_ID { get; set; }
    }
    public class ProductionDN
    {
        public int COMPANY_ID { get; set; }
        public DateTime DN_DATE { get; set; }
        public int CUST_ID { get; set; }
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }
        public List<ProductionDNItem> Items { get; set; }
    }

    public class ProductionDNItem
    {
        public int PACKING_ID { get; set; }
        public string? PO_NO { get; set; }
        public int ORDER_ENTRY_ID { get; set; }
    }
    public class ProductionDR
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime DR_DATE { get; set; }
        public string? REF_NO { get; set; }
        public int CUST_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_FAX { get; set; }
        public string? CONTACT_MOBILE { get; set; }
        public int SALESMAN_ID { get; set; }
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }
        public string? NARRATION { get; set; }

        public List<ProductionDRItem> Items { get; set; }
    }

    public class ProductionDRItem
    {
        public int SO_DETAIL_ID { get; set; }
        public int DN_DETAIL_ID { get; set; }
        public int ITEM_ID { get; set; }
        public string? REMARKS { get; set; }
        public string? UOM { get; set; }
        public decimal QUANTITY { get; set; }
        public int PACKING_ID { get; set; }
    }

    public class DNList
    {
        public int? ID { get; set; }
        public DateTime? DN_DATE { get; set; }
        public string? DN_NO { get; set; }
        public double? TOTAL_QTY { get; set; }
        public string? STATUS { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
    }
    public class DNListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<DNList> Data { get; set; }
    }
    public class DNViewResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public DNViewHeader Data { get; set; }
    }

    public class DNViewHeader
    {
        public int ID { get; set; }
        public string DN_NO { get; set; }
        public DateTime DN_DATE { get; set; }
        public double TOTAL_QTY { get; set; }
        public int COMPANY_ID { get; set; }
        public int CUST_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_PHONE { get; set; }
        public string CONTACT_MOBILE { get; set; }
        public string CONTACT_FAX { get; set; }
        public string COMPANY_NAME { get; set; }
        public List<DNViewDetail> Details { get; set; }
    }

    public class DNViewDetail
    {
        public int DETAIL_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal QUANTITY { get; set; }
        public string UOM { get; set; }
    }
    public class ProductionTransferInGRN
    {
        public int COMPANY_ID { get; set; }
        public int SUPP_ID { get; set; }   
        public DateTime GRN_DATE { get; set; }
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }

        public List<ProductionTransferInGRNItem> Items { get; set; }
    }
    public class ProductionTransferInGRNItem
    {
        public int PACKING_ID { get; set; }
    }
    public class GRNUploadResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public int GRN_ID { get; set; }
    }
    public class DispatchSave
    {
        public int FIN_ID { get; set; }
        public int UNIT_ID { get; set; }

        public int? DISTRIBUTOR_ID { get; set; }
        public int? SUBDEALER_ID { get; set; }
        public int? LOCATION_ID { get; set; }
        public int? BRAND_ID { get; set; }

        public bool IS_SHIPPED { get; set; }
        public string? SO_NO { get; set; }
        public string? TRANSPORTATION { get; set; }

        public DateTime? DISPATCH_TIME { get; set; }
        public DateTime? SHIPPED_TIME { get; set; }

        public int USER_ID { get; set; }

        public bool? IS_RETURN { get; set; }
        public DateTime? RETURN_TIME { get; set; }
        public int? RETURN_USER_ID { get; set; }
        public string? RETURN_REASON { get; set; }

        public bool? IS_COMMITTED { get; set; }
        public DateTime? UPDATED_DATE { get; set; }

        public List<DispatchBoxItem> Boxes { get; set; }
    }
    public class DispatchBoxItem
    {
        public int BOX_ID { get; set; }
        public int? ORDER_DETAIL_ID { get; set; }
    }
}
