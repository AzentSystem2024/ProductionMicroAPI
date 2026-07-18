namespace MicroApi.Models
{
    public class PayrollReportRequest
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int DepartmentId { get; set; }      // 0 = All

        public int PaymentMode { get; set; }       // 0 = Both, 1 = WPS, 2 = Internal CBD
    }

    public class PayrollReport
    {
        public string SalaryMonth { get; set; }

        public string EmployeeNo { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }
        public decimal BasicPay { get; set; }

        public decimal Allowance { get; set; }

        public decimal TotalDue { get; set; }

        public decimal Overtime { get; set; }

        public decimal Advance { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetPayable { get; set; }

       // public string PaymentMode { get; set; }
    }

    public class PayrollReportResponse
    {
        public int flag { get; set; }

        public string Message { get; set; }

        public List<PayrollReport> Data { get; set; } = new List<PayrollReport>();
    }

    public class PaymentModeModel
    {
        public int ID { get; set; }

        public string DESCRIPTION { get; set; }
    }
    public class PayrollOT
    {
        public string SalaryMonth { get; set; }

        public string EmployeeCode { get; set; }
        public string Name { get; set; }

        
        public string Designation { get; set; }
        public string Department { get; set; }
        public decimal BasicPay { get; set; }

        public decimal Allowance { get; set; }

        public decimal Salary { get; set; }

        public decimal NormalOT { get; set; }

        public decimal HolidayOT { get; set; }

        public decimal NormalOTAmount { get; set; }

        public decimal HolidayOTAmount { get; set; }

        public decimal TotalOT { get; set; }

        public decimal TotalOTAmount { get; set; }
    }

    public class PayrollOTResponse
    {
        public int flag { get; set; }

        public string Message { get; set; }

        public List<PayrollOT> Data { get; set; } = new();
    }
}