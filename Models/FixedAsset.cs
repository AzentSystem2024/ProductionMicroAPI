namespace MicroApi.Models
{
    public class FixedAsset
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public int ASSET_TYPE_ID { get; set; }
        public string ASSET_TYPE { get; set; }
        public int ASSET_LEDGER_ID { get; set; }
        public float ASSET_VALUE {  get; set; }
        public int USEFUL_LIFE {  get; set; }
        public decimal RESIDUAL_VALUE { get; set; }
        public int DEPR_LEDGER_ID { get; set; }
        public float DEPR_PERCENT {  get; set; }
        public string PURCH_DATE { get; set; }
        public bool IS_INACTIVE { get; set; }
       // public bool IS_DELETED { get; set; }
    }
    public class FixedAssetList
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string ASSET_TYPE { get; set; }
        //public int ASSET_LEDGER_ID { get; set; }
        public float ASSET_VALUE { get; set; }
        public int USEFUL_LIFE { get; set; }
        //public decimal RESIDUAL_VALUE { get; set; }
       // public int DEPR_LEDGER_ID { get; set; }
       // public float DEPR_PERCENT { get; set; }
        public string PURCH_DATE { get; set; }
        //public string LAST_DEPR_DATE { get; set; }
        public float NET_DEPRECIATION {  get; set; }
        public float CURRENT_VALUE { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class FixedAssetSelect
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public int ASSET_TYPE_ID { get; set; }
        public string ASSET_TYPE { get; set; }
        public int ASSET_LEDGER_ID { get; set; }
        public float ASSET_VALUE { get; set; }
        public int USEFUL_LIFE { get; set; }
        public decimal RESIDUAL_VALUE { get; set; }
         public int DEPR_LEDGER_ID { get; set; }
         public float DEPR_PERCENT { get; set; }
        public string PURCH_DATE { get; set; }
        public string LAST_DEPR_DATE { get; set; }
        public float NET_DEPRECIATION { get; set; }
        public float CURRENT_VALUE { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class FixedAssetListResponse
    {
        public int flag { get; set; } = 1;
        public string Message { get; set; }
        public List<FixedAssetList> Data { get; set; }
    }
    public class FixedAssetSaveResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
    }
    public class FixedAssetSelectResponse
    {
        public int flag { get; set; } = 1;
        public string message { get; set; }
        public List<FixedAssetSelect> Data { get; set; }

    }
}
