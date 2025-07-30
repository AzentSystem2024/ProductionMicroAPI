namespace MicroApi.Models
{
    public class OpeningBalance
    {
        public int? SL_NO { get; set; }
        public int HEAD_ID { get; set; }
        public decimal? DR_AMOUNT { get; set; }
        public decimal? CR_AMOUNT { get; set; }
        public string? BILL_NO { get; set; }
        public int? OPP_HEAD_ID { get; set; }
        public string? OPP_HEAD_NAME { get; set; }
        public int? STORE_ID { get; set; }
        public string? REMARKS { get; set; }
        public int? JOB_ID { get; set; }
        public int? CREATED_STORE_ID { get; set; }
        public int? STORE_AUTO_ID { get; set; }
    }
    public class AcOpeningBalanceInsertRequest
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public List<OpeningBalance> Details { get; set; }
    }

    public class OpeningBalanceSelect
    {
        public int? HEAD_ID { get; set; }
        public string? LEDGER_CODE { get; set; }
        public string? LEDGER_NAME { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
        public int? TRANS_ID { get; set; }
    }
    public class OBRequest
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
    }
    public class OBResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
    public class OBCommitRequest
    {
        public int TRANS_ID { get; set; }
    }
}
