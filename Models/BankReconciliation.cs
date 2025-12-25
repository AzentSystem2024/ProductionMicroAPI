namespace MicroApi.Models
{
    public class BankReconciliation
    {
     public int HEAD_ID { get; set; }
     public DateTime DATE_TO { get; set; }
     public int COMPANY_ID { get; set; }
    }
    public class BankReconciliationReport
    {
     public int TRANS_ID { get; set; }
     public string VOUCHER_NO { get; set; }
     public DateTime? TRANS_DATE { get; set; }
     public string REF_NO { get; set; }
     public string PARTY_NAME { get; set; }
     public decimal DR_AMOUNT { get; set; }
     public decimal CR_AMOUNT { get; set; }
     public decimal RUNNING_BALANCE { get; set; }

    }
    public class BankReconciliationReportResponse
    {
            public int flag { get; set; }
            public string message { get; set; }
            public List<BankReconciliationReport> Data { get; set; }
    }
    public class BankReconciliationInput
    {
        public string RECON_DATE { get; set; }
        public List<BankReconciliationSave> ReconciliationList { get; set; } 
    }
    public class BankReconciliationSave
    {
        public int? TRANS_ID { get; set; }

    }
    // Response class
    public class BankReconciliationSaveResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
    }
}

