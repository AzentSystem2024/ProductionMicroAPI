namespace MicroApi.Models
{
    public class DepreciationReport
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string ASSET_TYPE { get; set; }
        public DateTime PURCH_DATE { get; set; }
        public string STORE_NAME { get; set; }
        public string LOCATION { get; set; }
        public decimal ASSET_VALUE { get; set; }
        public int USEFUL_LIFE { get; set; }
        public decimal OPENING_DEPR { get; set; }
        public decimal DURING_DEPR { get; set; }
        public decimal CLOSING_DEPR { get; set; }
        public decimal CURRENT_VALUE { get; set; }
    }
    public class DepreciationReportRequest
    {
        public string DATE_FROM { get; set; }
        public string DATE_TO { get; set; }
        public int COMPANY_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
    }
    public class DepreciationReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<DepreciationReport> DepreciationDetails { get; set; }
    }
}
