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
        public List<RevenueData> Revenue { get; set; }
        public List<ExpenseData> Expense { get; set; }
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
    public class TopMovingArticle
    {
        public string ART_NO { get; set; }
        public string ARTICLE_NAME { get; set; }
        public decimal TOTAL_QTY { get; set; }
        public int TOTAL_TRANSACTIONS { get; set; }
    }
    public class LastMonthSale
    {
        public string SALE_NO { get; set; }
        public DateTime SALE_DATE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }
    public class RegionWiseSale
    {
        public string STATE_NAME { get; set; }
        public string SALE_NO { get; set; }
        public DateTime SALE_DATE { get; set; }
        public int TOTAL_INVOICES { get; set; }
        public decimal TOTAL_SALES { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
    }
    public class SalesmanWiseSale
    {
        public int SALESMAN_ID { get; set; }
        public string SALESMAN_NAME { get; set; }
        public int TOTAL_INVOICES { get; set; }
        public decimal TOTAL_SALES { get; set; }
    }
    public class GetDashboardData
    {
        public List<TopMovingArticle> TopMovingArticles { get; set; }
        public List<LastMonthSale> LastMonthSales { get; set; }
        public List<RegionWiseSale> RegionWiseSales { get; set; }
        public List<SalesmanWiseSale> SalesmanWiseSales { get; set; }
    }
    public class DashboarddataResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public GetDashboardData data { get; set; }
    }
    public class RevenueData
    {
        public int STORE_ID { get; set; }
        public string STORE { get; set; }
        public decimal REVENUE { get; set; }
    }
    public class ExpenseData
    {
        public int STORE_ID { get; set; }
        public string STORE { get; set; }
        public decimal EXPENSE { get; set; }
    }
}
