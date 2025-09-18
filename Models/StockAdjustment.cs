namespace MicroApi.Models
{
    public class StockAdjustment
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? ADJ_NO { get; set; }
        public string? ADJ_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? STATUS { get; set; }
        public List<StockAdjustmentDetail> Details { get; set; }
    }
    public class StockAdjustmentDetail
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? ADJ_ID { get; set; }
        //public string ADJ_NO { get; set; }
        //public string? ADJ_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public float? NET_AMOUNT { get; set; }
        //public int? DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? ITEM_NAME { get; set; }
        public float? COST { get; set; }
        public float? STOCK_QTY { get; set; }
        public float? NEW_QTY { get; set; }
        public float? ADJ_QTY { get; set; }
        public float? AMOUNT { get; set; }
        public string BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
    public class StockAdjustmentDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public StockAdjustment Data { get; set; }
    }

    public class StockAdjustmentResponse
    {
        public string Flag { get; set; }
        public string Message { get; set; }
    }
    public class StockAdjustmentList
    {
        public int? ID { get; set; }
        public string? ADJ_DATE { get; set; }
        public string? ADJ_NO { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public int? REASON_ID { get; set; }
        public string REASON_DESCRIPTION { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? NARRATION { get; set; }
    }


    public class StockAdjustmentListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<StockAdjustmentList> Data { get; set; }
    }

    public class StockAdjustmentUpdate
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        //public string? ADJ_NO { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public string? ADJ_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public float? NET_AMOUNT { get; set; }
        public string? NARRATION { get; set; }
       // public bool? STATUS { get; set; }
        public List<StockAdjustmentDetail> Details { get; set; }
    }

    public class StockAdjustmentRequest
    {
        public int STORE_ID { get; set; }
       // public int COMPANY_ID { get; set; }
    }
    public class StockAdjustmentSelectRequest
    {
        public int ADJ_ID { get; set; }
    }

    public class StockAdjustmentListRequest
    {
        public int STORE_ID { get; set; }
        public int COMPANY_ID { get; set; }
    }

    public class StockAdjustmentDeleteRequest
    {
        public int ADJ_ID { get; set; }
    }

    public class StockItem
    {
        public int? ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string? MATRIX_CODE { get; set; }
        public float? COST { get; set; }
        public float? CURRENT_STOCK { get; set; }
    }

    public class StockItemListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<StockItem> Data { get; set; }
    }
    public class StockAdjustmentApproval
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        //public string? ADJ_NO { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public string? ADJ_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public float? NET_AMOUNT { get; set; }
        public string? NARRATION { get; set; }
       // public int? STATUS { get; set; }
        public List<StockAdjustmentDetail> Details { get; set; }
    }
}
