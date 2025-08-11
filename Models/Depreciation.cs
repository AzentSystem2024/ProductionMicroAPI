namespace MicroApi.Models
{
    public class Depreciation
    {
        public string DOC_NO { get; set; }
        public string DEPR_DATE { get; set; }
        public string NARRATION { get; set; }
        public int COMPANY_ID { get; set; }
        public int TRANS_ID { get; set; }
        public int? TRANS_STATUS { get; set; }
        public int FIN_ID { get; set; }
        public int ASSET_ID { get; set; }
        public string LAST_DEPR_DATE { get; set; }
        public float CURRENT_VALUE { get; set; }
        public int DAYS { get; set; }
        public float DEPR_AMOUNT { get; set; }
        public int STORE_ID { get; set; }
    }
    public class DepreciationInsertRequest
    {
        public string? DEPR_DATE { get; set; }
        public string? NARRATION { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public List<AssetDepreciationDetail> ASSET_IDS { get; set; }
    }
    public class DepreciationUpdateRequest
    {
        public int? ID { get; set; }
        public int? TRANS_ID { get; set; }
        public string? DEPR_DATE { get; set; }
        public string? NARRATION { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public List<AssetDepreciationDetail> ASSET_IDS { get; set; }
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
    public class DepreciationList
    {
        public int? ID { get; set; }
        public int ? TRANS_ID { get; set; }
        public string DOC_NO { get; set; }
        public string DEPR_DATE { get; set; }
        public string NARRATION {  get; set; }
        public Decimal AMOUNT { get; set; }
        public string VOUCHER_NO { get; set; }
        public string TRANS_STATUS { get; set; }
    }
    public class DepreciationDetails
    {
        public int ID { get; set; }
        public string? DOC_NO { get; set; }
        public string? DEPR_DATE { get; set; }
        public string? NARRATION { get; set; }
        public decimal? AMOUNT { get; set; }
        public string? VOUCHER_NO { get; set; }
        public string? TRANS_STATUS { get; set; }
        public int? TRANS_ID { get; set; }
        public List<AssetDepreciationDetail> ASSET_IDS { get; set; }
    }
    public class AssetDepreciationDetail
    {
        public int Asset_ID { get; set; }
        public int? Days { get; set; }
        public float? Depr_Amount { get; set; }
    }
    public class DepreciationDetailsResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public DepreciationDetails Data { get; set; }
        //public List<AssetDepreciationDetail> AssetDetails { get; set; }
    }
    public class DepreciationSaveResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<Depreciation> Data { get; set; }

    }
    public class DepreciationResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<FixedAssetLists> Data { get; set; }
    }
    public class DepreciationListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<DepreciationList> Data { get; set; }
    }
    public class DepreciationApproveData
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<Depreciation> Data { get; set; }
    }
}
