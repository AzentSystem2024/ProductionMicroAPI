namespace MicroApi.Models
{
    public class PhysicalStock
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? PHYSICAL_NO { get; set; }
        public string? PHYSICAL_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public string? REASON_NAME { get; set; }
        public string? REFERENCE_NO { get; set; }
        public int? TRANS_ID { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? STATUS { get; set; }
        public List<PhysicalStockDetail> Details { get; set; }
    }
    public class PhysicalStockDetail
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? PHYSICAL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? ITEM_NAME { get; set; }
        public float? QTY_OH { get; set; }
        public float? COST { get; set; }
        public float? QTY_COUNT { get; set; }
       // public DateTime? COUNT_TIME { get; set; }
        public decimal? ADJUSTED_QTY { get; set; }
        public string BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
    public class PhysicalStockSelectDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public PhysicalStockSelect Data { get; set; }
    }
    public class PhysicalStockSelect
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? PHYSICAL_NO { get; set; }
        public string? PHYSICAL_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public string? REASON_NAME { get; set; }
        public string? REFERENCE_NO { get; set; }
        public int? TRANS_ID { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? STATUS { get; set; }
        public List<PhysicalStockSelectDetail> Details { get; set; }
    }
    public class PhysicalStockSelectDetail
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? PHYSICAL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? ITEM_NAME { get; set; }
        public float? QTY_OH { get; set; }
        public float? COST { get; set; }
        public float? QTY_COUNT { get; set; }
        public DateTime? COUNT_TIME { get; set; }
        public decimal? ADJUSTED_QTY { get; set; }
        public string BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }

    public class PhysicalStockDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public PhysicalStock Data { get; set; }
    }

    public class PhysicalStockResponse
    {
        public string Flag { get; set; }
        public string Message { get; set; }
    }

    public class PhysicalStockList
    {
        public int? ID { get; set; }
        public string? PHYSICAL_DATE { get; set; }
        public string? PHYSICAL_NO { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public string? MATRIX_CODE { get; set; }
        public int? REASON_ID { get; set; }
        public string REASON_DESCRIPTION { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? NARRATION { get; set; }
    }

    public class PhysicalStockListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PhysicalStockList> Data { get; set; }
    }

    public class PhysicalStockUpdate
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public string? PHYSICAL_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public string? REFERENCE_NO { get; set; }
        public string? NARRATION { get; set; }
        public List<PhysicalStockDetail> Details { get; set; }
    }

    public class PhysicalStockRequest
    {
        public int STORE_ID { get; set; }
    }

    public class PhysicalStockApproval
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public string? PHYSICAL_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public string? NARRATION { get; set; }
        public List<PhysicalStockDetail> Details { get; set; }
    }
    public class StockItems
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string MatrixCode { get; set; }
        public decimal Cost { get; set; }
        public int StoreId { get; set; }
        public decimal StockQty { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int CatId { get; set; }
        public string CatName { get; set; }
        public int Brand_Id { get; set; }
        public string BrandName { get; set; }
        public int SuppId { get; set; }
        public string Supp_Name { get; set; }
    }
    public class FilteredItemsRequest
    {
        public List<FilterId> FilterIds { get; set; }
    }
    public class FilterId
    {
        public string FilterType { get; set; } // e.g., "Store", "Dept", "Cat", "Brand", "Supplier"
        public int Id { get; set; }
    }

    public class ItemDetailsModel
    {
        public int? ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MatrixCode { get; set; }
        public string Barcode { get; set; }
        public int? CatId { get; set; }
        public string CatName { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public int? DeptId { get; set; }
        public string DeptName { get; set; }
        public decimal QtyStock { get; set; }
    }
    public class HistoryModel
    {
        public int Action { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public int DocTypeId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
    public class ItemCodeRequest
    {
        public List<string> BarCodes { get; set; }
    }
    public class ItemCodeListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ItemDetailsModel> Data { get; set; }
    }
    public class PhysicalStockLatestVocherNO
    {
        public string? VOCHERNO { get; set; }
        public int? TRANS_ID { get; set; }
    }
    public class PhysicalStockLatestVocherNOResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PhysicalStockLatestVocherNO> Data { get; set; }

    }


}
