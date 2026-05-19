namespace MicroApi.Models
{
    public class TrialBalanceDimensionReport
    {
        public int HeadID { get; set; }

        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string Category { get; set; }

        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }

        public string? BU { get; set; }
        public string? LEDGER { get; set; }
        public string? DEPT_GROUP { get; set; }
        public string? DEPARTMENT { get; set; }
        public string? CLINICIAN { get; set; }

        public decimal OpeningBalanceDebit { get; set; }
        public decimal OpeningBalanceCredit { get; set; }

        public decimal DuringThePeriodDebit { get; set; }
        public decimal DuringThePeriodCredit { get; set; }

        public decimal ClosingBalanceDebit { get; set; }
        public decimal ClosingBalanceCredit { get; set; }
    }
    public class ReportRequest1
    {
        public int CompanyId { get; set; }
        public int FinId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string DimensionCode { get; set; }
    }
}