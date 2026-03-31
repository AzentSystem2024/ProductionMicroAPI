namespace MicroApi.Models
{
    public class Report
    {
    }
    public class PDCListReportRequest
    {
        public int COMPANY_ID { get; set; }
        public string DATE_FROM { get; set; }
        public string DATE_TO { get; set; }
    }
    public class PDCListReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PDCListReport> PDCDetails { get; set; }
    }
    public class PDCListReport
    {
        public int ID { get; set; }
        public string CHEQUE_NO { get; set; }
        public DateTime CHEQUE_DATE { get; set; }
        public string BENEFICIARY_NAME { get; set; }
        public decimal RECEIVED { get; set; }
        public decimal PAID { get; set; }
        public string BANK { get; set; }
        public string REMARKS { get; set; }
        public string PDC_STATUS { get; set; }
        public string TRANS_TYPE { get; set; }
    }
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
    public class PrepaymentReportRequest
    {
        public int COMPANY_ID { get; set; }
        public string DEPARTMENT_ID { get; set; } = "";
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
    }
    public class PrepaymentReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PrepaymentReport> PrepaymentDetails { get; set; }
    }

    public class PrepaymentReport
    {
        public int ID { get; set; }
        public string INV_NO { get; set; }
        public string SUPP_CODE { get; set; }
        public string SUPP_NAME { get; set; }
        public string DEPT_NAME { get; set; }
        public string EXPENSE_LEDGER { get; set; }
        public string GROUP_NAME { get; set; }
        public string PREPAYMENT_LEDGER { get; set; }
        public DateTime TRANSACTON_DATE { get; set; }
        public int NO_OF_MONTHS { get; set; }
        public int NO_OF_DAYS { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public decimal PURCH_VALUE { get; set; }
        public decimal OPENING_ACCUR { get; set; }
        public decimal CURRENT_ACCUR { get; set; }
        public decimal TOTAL_ACCUR { get; set; }
        public decimal BALANCE_ACCUR { get; set; }
    }
    public class ProfitLossBranchRequest
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public string DATE_FROM { get; set; }
        public string DATE_TO { get; set; }
        public string STORE_ID { get; set; } 
    }
    public class ProfitLossBranchResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ProfitLossBranch> ProfitLossDetails { get; set; }
    }
    public class ProfitLossBranch
    {
        public int TYPE_ID { get; set; }
        public string TYPE_NAME { get; set; }
        public int GROUP_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int HEAD_ID { get; set; }
        public string HEAD_CODE { get; set; }
        public string HEAD_NAME { get; set; }

        // Dynamic store columns
        public Dictionary<string, decimal?> StoreAmounts { get; set; }
    }
}
