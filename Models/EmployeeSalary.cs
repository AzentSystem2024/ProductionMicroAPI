namespace MicroApi.Models
{
    public class EmployeeSalarySave
    {
        public int? COMPANY_ID { get; set; }
        public int? EMP_ID { get; set; }
        public int? HEAD_ID { get; set; }
        public float? HEAD_PERCENT { get; set; }
        public float? HEAD_AMOUNT { get; set; }
        public int? FIN_ID { get; set; }
        public string? EFFECT_FROM { get; set; }
        public bool? IS_INACTIVE { get; set; }
    }
    public class EmployeeSalaryUpdate
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? EMP_ID { get; set; }
        public int? HEAD_ID { get; set; }
        public float? HEAD_PERCENT { get; set; }
        public float? HEAD_AMOUNT { get; set; }
        public int? FIN_ID { get; set; }
        public string? EFFECT_FROM { get; set; }
        public bool? IS_INACTIVE { get; set; }
    }
    public class EmployeeSalaryResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public EmployeeSalaryUpdate data { get; set; }
    }
    public class EmployeeListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<EmployeeSalaryUpdate> Data { get; set; }
    }
}
