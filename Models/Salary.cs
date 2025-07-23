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
    public class PayrollViewRequest
    {
        public int PAYDETAIL_ID { get; set; }
    }
    public class PayrollViewResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int PAYDETAIL_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public string MONTH { get; set; }
        public decimal BASIC_SALARY { get; set; }
        public decimal WORKED_DAYS { get; set; }
        public decimal OT_HOURS { get; set; }
        public decimal LESS_HOURS { get; set; }
        public List<SalaryHeadData> DATA { get; set; }
    }

    public class SalaryHeadData
    {
        public int HEAD_ID { get; set; }
        public string HEAD_NAME { get; set; }
        public int HEAD_TYPE { get; set; }
        public decimal GROSS_AMOUNT { get; set; }    
        public decimal DEDUCTION_AMOUNT { get; set; }
    }
    public class SalaryItemUpdate
    {
        public int HEAD_ID { get; set; }
        public int LOAN_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public string? REMARKS { get; set; }
    }

    public class UpdateItemRequest
    {
        public int PAYDETAIL_ID { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public List<SalaryItemUpdate> SALARY { get; set; }
    }
    public class PayrollResponse
    {
        public int flag { get; set; }          
        public string Message { get; set; }   
    }
       public class SalaryApprove
    {
        public List<int> PAYDETAIL_ID { get; set; }
    }
    public class SalaryApproveResponse
    {
        public int flag { get; set; }        
        public string Message { get; set; }
    }
}
