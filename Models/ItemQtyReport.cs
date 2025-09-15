namespace MicroApi.Models
{
    public class ItemQtyReport
    {
        public int ITEM_ID { get; set; }
        public string MATRIXCODE { get; set; }
        public string ITEMCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string DEPARTMENT { get; set; }
        public string CATEGORY { get; set; }
        public string SUBCATEGORY { get; set; }
        public string BRAND { get; set; }
        public decimal QUANTITY_AVAILABLE { get; set; }
    }
    public class ItemQuantityReportRequest
    {
        public DateTime ASONDATE { get; set; }
        public int STORE_ID { get; set; }
    }
    public class ItemQuantityReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ItemQtyReport> ItemQuantityDetails { get; set; }
    }
}
