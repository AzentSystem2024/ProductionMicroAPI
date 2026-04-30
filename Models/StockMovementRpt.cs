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
        public decimal TRANSFER_OUT_QTY { get; set; }
        public decimal TRANSFER_IN_QTY { get; set; }
        public decimal BALANCE_STOCK { get; set; }
    }
    public class StockMovementRequest
    {
        public int COMPANY_ID { get; set; }
      //  public int STORE_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public int? ITEM_TYPE { get; set; } = 0;
        public int FIN_ID { get; set; }
    }
    public class StockMovementRptResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<StockMovementRpt> data { get; set; }
    }
    public class StockMovementDrilldown
    {
        public string TRANS_TYPE { get; set; } 
        public string DOC_NO { get; set; }
        public DateTime DOC_DATE { get; set; }
        public decimal QUANTITY { get; set; }     
        public long TRANS_ID { get; set; }
        public int ID { get; set; }
        public int PRODUCTION_TYPE { get; set; }
        public string NAME { get; set; }
    }
    public class StockMovementDrilldownResponse
    {
        public int flag { get; set; }
        public string message { get; set; } 
        public List<StockMovementDrilldown> data { get; set; }
    }
    public class StockMovementDrillDownRequest
    {
        public int? COMPANY_ID { get; set; }
          public int? ITEM_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
        public string? TRANS_TYPE { get; set; }
    }
    public class StoreWiseStockRequest
    {
        public int? FIN_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        //public string DEPT_ID { get; set; }
        //public string CAT_ID { get; set; }
        //public string BRAND_ID { get; set; }
        //public string SUPP_ID { get; set; }
        public string? STORE_ID { get; set; }
        public string? ITEM_ID { get; set; }
    }

    public class StoreWiseStockRow
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }

        // Dynamic store columns
        public Dictionary<string, decimal> StoreStock { get; set; } = new Dictionary<string, decimal>();
    }

    public class StoreWiseStockResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<StoreWiseStockRow> data { get; set; }
    }
}
