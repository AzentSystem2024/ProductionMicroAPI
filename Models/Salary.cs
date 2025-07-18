namespace MicroApi.Models
{
    public class Salary
    {
        public int TS_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int USER_ID { get; set; }

    }
    public class GenerateSalaryResponse
    {
        public int flag { get; set; }            
        public string Message { get; set; }
      
    }
    public class SalaryLookupRequest
    {
        public int COMPANY_ID { get; set; }
    }
    public class SalaryLookup
    {
        public int SALARY_BILL_NO { get; set; }
        public DateTime SAL_MONTH { get; set; }
        public string EMPLOYEE_NO { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public decimal WORKED_DAYS { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }
    public class SalaryLookupResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalaryLookup> Data { get; set; }
    }
}
