namespace MicroApi.Models
{
    public class SalaryHead
    {
        public int? ID { get; set; }  

        public string? HEAD_NAME { get; set; }
        public string? PAYSLIP_TITLE { get; set; }
        public bool? HEAD_ACTIVE { get; set; }
        public int? HEAD_TYPE { get; set; }
        public bool? AFFECT_LEAVE { get; set; }
        public int? HEAD_ORDER { get; set; }
        public int? HEAD_NATURE { get; set; }
        public double? FIXED_AMOUNT { get; set; }
        public double? HEAD_PERCENT { get; set; }
        public bool? RANGE_EXISTS { get; set; }
        public double? RANGE_FROM { get; set; }
        public double? RANGE_TO { get; set; }
        public bool? HEAD_PERCENT_INCLUDE_OT { get; set; }
        public bool? INSTALLMENT_RECOVERY { get; set; }
        public bool IS_INACTIVE { get; set; } = false;
        public int? AC_HEAD_ID { get; set; }
        public int? PERCENT_HEAD_ID { get; set; }


    }
    public class SalaryHeadUpdate
    {
        public int ID { get; set; }
        public string? HEAD_NAME { get; set; }
        public string? PAYSLIP_TITLE { get; set; }
        public bool? HEAD_ACTIVE { get; set; }
        public int? HEAD_TYPE { get; set; }
        public bool? AFFECT_LEAVE { get; set; }
        public int? HEAD_ORDER { get; set; }
        public int? HEAD_NATURE { get; set; }
        public double? FIXED_AMOUNT { get; set; }
        public double? HEAD_PERCENT { get; set; }
        public bool? RANGE_EXISTS { get; set; }
        public double? RANGE_FROM { get; set; }
        public double? RANGE_TO { get; set; }
        public bool? HEAD_PERCENT_INCLUDE_OT { get; set; }
        public bool? INSTALLMENT_RECOVERY { get; set; }
        public bool IS_INACTIVE { get; set; } = false;
        public int? AC_HEAD_ID { get; set; }
        public int? PERCENT_HEAD_ID { get; set; }
        public List<int> PERCENT_HEAD_IDS { get; set; } = new List<int>();


    }
    public class SalaryHeadResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public SalaryHeadUpdate data { get; set; }
    }
    public class SalaryHeadListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalaryHeadUpdate> Data { get; set; }
    }
}
