namespace MicroApi.Models
{
    public class SalaryWPS
    {
        public string? MOL_NUMBER { get; set; }
        public string? BANK_CODE { get; set; }
        public string? BANK_AC_NO { get; set; }
        public int WORKED_DAYS { get; set; }
        public decimal? FIXED_SALARY { get; set; }
        public decimal? VARIABLE_SALARY { get; set; }
        public int? LEAVE_DAYS { get; set; }
    }
    public class SalaryWPSResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<SalaryWPS> data { get; set; }
    }
    public class SalaryWPSRequest
    {
        public int COMPANY_ID { get; set; }
        public string? DEPARTMENT_ID { get; set; }
        public DateTime SAL_MONTH { get; set; }
    }

}
