namespace MicroApi.Models
{
    public class EmployeeSalarySave
    {
        public int? ID { get; set; }
        public int? EMP_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public decimal? SALARY { get; set; }
        public String? EFFECT_FROM { get; set; }
        public List<SalaryHeadDetail> Details { get; set; }
        //public bool? IS_INACTIVE { get; set; }
    }
    public class SalaryHeadDetail
    {
       // public int? ID { get; set; }
        public int? HEAD_ID { get; set; }
        public string? HEAD_NATURE { get; set; }
        public string? HEAD_NAME { get; set; }
        public float? HEAD_PERCENT { get; set; }
        public float? HEAD_AMOUNT { get; set; }
       // public int? FIN_ID { get; set; }
        public String? EFFECT_FROM { get; set; }
        public bool? IS_INACTIVE { get; set; }
        //public bool Selected { get; set; }
    }
    public class EmployeeSalaryUpdate
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? EMP_CODE { get; set; }
        public string? EMP_NAME { get; set; }
        public string? DESG_NAME { get; set; }
        public decimal? SALARY { get; set; }
        public String? EFFECT_FROM { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public List<SalaryHeadDetail> Details { get; set; }
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

    public class EmployeeSalarySettingsList
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public string DESG_NAME { get; set; }
        public string? EFFECT_FROM { get; set; }
       // public bool? IS_INACTIVE { get; set; }
        public decimal SALARY { get; set; }

    }
    public class EmployeeSalarySettingsListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<EmployeeSalarySettingsList> Data { get; set; }
    }
    public class EmployeeSalaryRequest
    {
        public int EMP_ID { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class EmployeeSalaryFilterRequest
    {
        public int CompanyId { get; set; }
        public int FilterAction { get; set; }
    }
}
