namespace MicroApi.Models
{
    public class TrialBalanceAsOnDate
    {
        public int MainGroupId { get; set; }
        public string MainGroup { get; set; }

        public int SubGroupId { get; set; }
        public string SubGroup { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }

        public int HeadID { get; set; }

        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }

        // Dynamic Store Columns
        public Dictionary<string, decimal> Debit { get; set; }
            = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> Credit { get; set; }
            = new Dictionary<string, decimal>();
    }

    public class ReportRequest2
    {
        public int CompanyId { get; set; }
        public int FinId { get; set; }
        public string DateTo { get; set; }
    }
}