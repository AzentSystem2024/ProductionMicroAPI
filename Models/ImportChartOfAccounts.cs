using Newtonsoft.Json;

namespace MicroApi.Models
{
    public class ImportChartOfAccountsData
    {
        public int? ID { get; set; }
        public int? LogID { get; set; }
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string Category { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public string? CostType { get; set; }
    }

    public class ImportAccountsLog
    {
        public int ID { get; set; }
        public int DocNo { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public DateTime ImportedTime { get; set; }
        public string ImportedBy { get; set; }
    }


    public class ImportAccountsInput
    {
        public int? CompanyID { get; set; }
        public int? UserID { get; set; }
        public string? BatchNo { get; set; }
        public int? Action { get; set; }
        public List<ImportChartOfAccountsData>? data { get; set; }
    }

    public class ImportAccountsResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ImportAccountsLog> data { get; set; }
    }

    public class viewImportAccountsDataResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ImportChartOfAccountsData> data { get; set; }
    }

    public class viewImportAccountsInput
    {
        public int LogID { get; set; }
    }
}
