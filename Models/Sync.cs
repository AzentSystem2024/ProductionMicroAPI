namespace MicroApi.Models
{
    public class Sync
    {
        public int UNIT_ID { get; set; }
        public int USER_ID { get; set; }
        public int FIN_ID { get; set; }
        public List<SyncArticleProduction> Articles { get; set; }
    }
    public class SyncArticleProduction
    {
        public long ARTICLE_PRODUCTION_ID { get; set; }
        public long ARTICLE_ID { get; set; }
        public int PAIRS { get; set; }
        public int BOX_ID { get; set; }
        public string BARCODE { get; set; }
        public float PRICE { get; set; }
    }
    public class SyncResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
    public class PackProductionSync
    {
        public int UNIT_ID { get; set; }
        public int USER_ID { get; set; }
        public int FIN_ID { get; set; }

        public List<PackProductionItem> PackItems { get; set; }
    }

    public class PackProductionItem
    {
        public long PACK_PRODUCTION_ID { get; set; }
        public long PACKING_ID { get; set; }
        public string BOX_SERIAL { get; set; }
        public float QTY { get; set; }
        public int SMALL_BOX_ID { get; set; }
        public int MASTER_CARTON_ID { get; set; }
        public string BARCODE { get; set; }
        public float BOX_PRICE { get; set; }
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
        public double NET_AMOUNT { get; set; }
        public List<ProductionTransferInItem> Items { get; set; }
    }

    public class ProductionTransferInItem
    {
        public int ITEM_ID { get; set; }
        public string? UOM { get; set; }
        public decimal QUANTITY { get; set; }
        public double COST { get; set; }
        public string? BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }

}
