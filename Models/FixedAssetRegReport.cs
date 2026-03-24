namespace MicroApi.Models
{
    public class FixedAssetRegReport
    {
        public string CODE { get; set; }
        public string ASSET_NAME { get; set; }
        public string STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public int ASSET_TYPE_ID { get; set; }
        public DateTime TRANS_DATE { get; set; }
        public string LOCATION { get; set; }
        public decimal PURCH_VALUE { get; set; }
        public string USEFUL_LIFE { get; set; }
        public decimal NET_DEPRECIATION { get; set; }
        public decimal CURRENT_ASSETVALUE { get; set; }
    }
    public class FixedAssetReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<FixedAssetRegReport> FixedAssetDetails { get; set; }
    }
    public class FixedAssetReportRequest
    {
        public int COMPANY_ID { get; set; }
        public int DEPARTMENT_ID { get; set; } 
        //public int STORE_ID { get; set; }
    }
}
