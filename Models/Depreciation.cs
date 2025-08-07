namespace MicroApi.Models
{
    public class Depreciation
    {
    }
    public class FixedAssetLists
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string ASSET_TYPE { get; set; }
        //public int ASSET_LEDGER_ID { get; set; }
        public float ASSET_VALUE { get; set; }
        public int USEFUL_LIFE { get; set; }
        public decimal RESIDUAL_VALUE { get; set; }
        public string PURCH_DATE { get; set; }
        public string LAST_DEPR_DATE { get; set; }
        public float? DEPR_PERCENT { get; set; }
        public float NET_DEPRECIATION { get; set; }
        public float CURRENT_VALUE { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class DepreciationResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<FixedAssetLists> Data { get; set; }
    }
}
