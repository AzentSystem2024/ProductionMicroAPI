namespace MicroApi.Models
{
    public class TrialBalanceReport
    {
        public int HeadID { get; set; }
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string Category { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public long TransId { get; set; } 
        public decimal OpeningBalanceDebit { get; set; }
        public decimal OpeningBalanceCredit { get; set; }
        public decimal DuringThePeriodDebit { get; set; }
        public decimal DuringThePeriodCredit { get; set; }
        public decimal ClosingBalanceDebit { get; set; }
        public decimal ClosingBalanceCredit { get; set; }
    }
    public class ReportRequest
    {
        public int CompanyId { get; set; }
        public int FinId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
