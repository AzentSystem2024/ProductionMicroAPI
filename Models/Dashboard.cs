namespace MicroApi.Models
{
    public class Dashboard
    {
    }
    public class DashboardRequest
    {
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }

    }
    public class DashboardResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public DashboardData data { get; set; }
    }

    public class DashboardData
    {
        public List<GrossSaleData> GrossSale { get; set; }

        public List<TopMovingItemData> TopMovingItems { get; set; }

        public List<TenderSummaryStoreData> TenderSummary { get; set; }
    }

    public class GrossSaleData
    {
        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public decimal AMOUNT { get; set; }
    }

    public class TopMovingItemData
    {
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal QTY_SOLD { get; set; }
        public int TIMES_SOLD { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
    }

    public class TenderSummaryStoreData
    {
        public int STORE_ID { get; set; }

        public string STORE_NAME { get; set; }

        public List<TenderTypeData> TenderTypes { get; set; }
    }

    public class TenderTypeData
    {
        public string TENDER { get; set; }

        public decimal AMOUNT { get; set; }
    }
}
