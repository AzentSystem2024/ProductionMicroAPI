namespace MicroApi.Models
{
    public class AC_Report
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public int HEAD_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
    }
    public class LedgerStatementItem
    {
        public int TRANS_ID { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? TRANS_TYPE_ID { get; set; }  
        public string TRANS_TYPE_NAME { get; set; }
        public string VOUCHER_NO { get; set; }
        public string PARTICULARS { get; set; }
        public decimal DR_AMOUNT { get; set; }
        public decimal CR_AMOUNT { get; set; }
        public string BALANCE { get; set; }
    }

    public class LedgerStatementResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<LedgerStatementItem> data { get; set; } = new List<LedgerStatementItem>(); 
    }
    public class InitDataRequest
    {
        public int COMPANY_ID { get; set; }
    }
    public class DropDownItem
    {
        public int ID { get; set; }
        public string NAME { get; set; }
    }
    public class LedgerReportInitData
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<DropDownItem> LEDGER_HEADS { get; set; }
    }
    public class ArticleProductionFilter
    {
        public DateTime DATE_FROM { get; set; } = DateTime.Today;
        public DateTime DATE_TO { get; set; } = DateTime.Today;
        public string COMPANY_ID{ get; set; }
    }

    public class ArticleProductionItem
    {
        public string ART_NO { get; set; }
        public string COLOR { get; set; }
        public string CATEGORY { get; set; }
        public string BRAND { get; set; }
        public string SIZE { get; set; }
        public string PRODUCTION_UNIT { get; set; }
        public int QUANTITY { get; set; }
    }

    public class ArticleProductionResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ArticleProductionItem> data { get; set; }
    }
    public class BoxProductionFilter
    {
        public DateTime DATE_FROM { get; set; } = DateTime.Today;
        public DateTime DATE_TO { get; set; } = DateTime.Today;
        public string COMPANY_ID { get; set; }
    }

    public class BoxProductionItem
    {
        public string ART_NO { get; set; }
        public string COLOR { get; set; }
        public string CATEGORY { get; set; }
        public string BRAND { get; set; }
        public string SIZE { get; set; }
        public string PRODUCTION_UNIT { get; set; }
        public int QUANTITY { get; set; }
    }

    public class BoxProductionResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<BoxProductionItem> data { get; set; }
    }
    public class ProfitlossReport
    {
        public int TRANS_ID { get; set; }
        public DateTime TRANS_DATE { get; set; }
        public int TRANS_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string VOUCHER_NO { get; set; }
        public string OPP_HEAD_NAME { get; set; }
        public string REMARKS { get; set; }
        public decimal DR_AMOUNT { get; set; }
        public decimal CR_AMOUNT { get; set; }
    }
    public class ProfitlossReportRequest
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
    }
    public class ProfitlossReportResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ProfitlossReport> data { get; set; } = new List<ProfitlossReport>();
    }
}
