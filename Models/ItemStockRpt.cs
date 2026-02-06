namespace MicroApi.Models
{
    public class ItemStockRpt
    {
        public string ITEM_CODE { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string DEPARTMENT { get; set; }
        public string CATEGORY { get; set; }
        public string SUB_CATEGORY { get; set; }
        public string BRAND { get; set; }
        public double CURRENT_STOCK { get; set; }
        public string? ITEM_TYPE { get; set; }
    }
    public class ItemStockViewResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ItemStockRpt> Data { get; set; }
    }
    public class ItemStockRptRequest
    {
        public int COMPANY_ID { get; set; }
       
    }

}
