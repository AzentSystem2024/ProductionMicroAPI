namespace MicroApi.Models
{
    public class WageRegReport
    {
        public int SL_NO { get; set; }
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public string DESIGNATION { get; set; }
        public float? TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE { get; set; }
        public float? OVERTIME_WORKED { get; set; }
        public float? MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC { get; set; }
        public float? MINIMUM_RATE_OF_WAGES_PAYABLE_DA { get; set; }
        public float? RATE_OF_WAGES_ACTUALLY_PAID_BASIC { get; set; }
        public float? RATE_OF_WAGES_ACTUALLY_PAID_DA { get; set; }
        public float? GROSS_WAGES_PAYABLE { get; set; }
        public float? EMPLOYEE_CONTRIBUTION_TO_PF { get; set; }
        public float? HR { get; set; }
        public float? OTHER_DEDUCTIONS { get; set; }
        public float? TOTAL_DEDUCTIONS { get; set; }
        public float? WAGES_PAID { get; set; }
        public DateTime? DATE_OF_PAYMENT { get; set; }
        public string? SIGNATURE { get; set; }

    }
    public class WageReportRequest
    {
        public DateTime Month { get; set; }
        public string ReportType { get; set; }
    }
    public class WageReportResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<WageRegReport> WageDetails { get; set; }
        public List<SalaryReport> SalaryDetails { get; set; }
    }
    public class SalaryReport
    {
        public int SL_NO { get; set; }
        public string EMP_NAME { get; set; }
        public string DESIGNATION { get; set; }
        public float MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC { get; set; }
        public float MINIMUM_RATE_OF_WAGES_PAYABLE_DA { get; set; }
        public float RATE_OF_WAGES_ACTUALLY_PAID_BASIC { get; set; }
        public float RATE_OF_WAGES_ACTUALLY_PAID_DA { get; set; }
        public float TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE { get; set; }
        public float OVERTIME_WORKED { get; set; }
        public float GROSS_WAGES_PAYABLE { get; set; }
        public float EMPLOYEE_CONTRIBUTION_TO_PF { get; set; }
        public float HR { get; set; }
        public float OTHER_DEDUCTIONS { get; set; }
        public float TOTAL_DEDUCTIONS { get; set; }
        public float WAGES_PAID { get; set; }
    }
}
