namespace MicroApi.Models
{
    public class EosReason
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? COMPANY_NAME { get; set; }
        public bool? IS_SYSTEM { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public bool? IS_DELETED { get; set; }
    }
    public class EosReasonResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public EosReason data { get; set; }
        public List<EosReason> datas { get; set; }
    }
}
