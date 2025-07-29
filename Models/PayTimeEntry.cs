namespace MicroApi.Models
{
    public class PayTimeEntry
    {
        public int? ID { get; set; }
        public int? COMP_ID { get; set; }
        public string SAL_MONTH { get; set; }
        public int EMP_ID { get; set; }
        public int HEAD_ID { get; set; }
        public float AMOUNT { get; set; }
        public int EXG_TEAM_ID { get; set; }
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
        public string flag { get; set; }
        public string message { get; set; }
       // public PayTimeEntryUpdate data { get; set; }
    }
}
