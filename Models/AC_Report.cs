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
}
