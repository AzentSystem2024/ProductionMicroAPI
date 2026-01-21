namespace MicroApi.Models
{
    public class StockMovementRpt
    {
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string MATRIX_CODE { get; set; }
        public string COLOR { get; set; }
        public string SIZE { get; set; }
        public string STYLE { get; set; }
        public decimal OPENING_QTY { get; set; }
        public decimal GRN_QTY { get; set; }
        public decimal PURCHASE_RETURN_QTY { get; set; }
        public decimal PRODUCTION_QTY { get; set; }
        public decimal CONSUMPTION_QTY { get; set; }
        public decimal DELIVERY_QTY { get; set; }
        public decimal DELIVERY_RETURN_QTY { get; set; }
        public decimal SALE_QTY { get; set; }
        public decimal SALE_RETURN_QTY { get; set; }
        public decimal ADJUSTED { get; set; }
        public decimal BALANCE_STOCK { get; set; }
    }
    public class StockMovementRequest
    {
        public int COMPANY_ID { get; set; }
      //  public int STORE_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public int? ITEM_TYPE { get; set; } = 0; 
    }
    public class StockMovementRptResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<StockMovementRpt> data { get; set; }
    }
}
