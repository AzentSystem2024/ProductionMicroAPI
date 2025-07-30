namespace MicroApi.Models
{
    public class PayTimeEntry
    {
        public int EMP_ID { get; set; }
        public float AMOUNT { get; set; }
        public int? DAYS { get; set; }
    }
    public class PayTimeEntryInsert
    {
        public int COMPANY_ID { get; set; }
        public int HEAD_ID { get; set; }
        public string SAL_MONTH { get; set; }
        public List<PayTimeEntry> PAY_ENTRIES { get; set; }
    }
    public class PayTimeEntryUpdate
    {
        public int? ID { get; set; }
        public int? COMP_ID { get; set; }
        public string? SAL_MONTH { get; set; }
        public int? EMP_ID { get; set; }
        public int? HEAD_ID { get; set; }
        public float? AMOUNT { get; set; }
        public int? EXG_TEAM_ID { get; set; }
    }
    public class PayTimeResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
       // public PayTimeEntryUpdate data { get; set; }
    }
    public class PayTimeEntryRequest
    {
        public int COMAPNY_ID { get; set; }
        public int HEAD_ID { get; set; }
        public string SAL_MONTH { get; set; }
    }
    public class PayTimeEntryResult
    {
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public int? DAYS { get; set; }  
        public float? AMOUNT { get; set; }
    }

    public class PayTimeSelectResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<PayTimeEntryResult> data { get; set; }  
    }
}
