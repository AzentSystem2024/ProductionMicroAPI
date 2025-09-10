namespace MicroApi.Models
{
    public class PaySlipReport
    {
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public string PP_NO { get; set; }
        public string DAMAN_NO { get; set; }
        public string BANK_AC_NO { get; set; }
        public float BASIC_SALARY { get; set; }
        public DateTime TS_MONTH { get; set; }
        public float TOTAL_DAYS { get; set; }
        public float OT_HOURS { get; set; }
        public float LESS_HOURS { get; set; }
        public int SALARY_ID { get; set; }
        public List<PaySlipReportData> SalaryHeads { get; set; } = new List<PaySlipReportData>();
    }
    public class PaySlipReportData
    {
        public int HEAD_ID { get; set; }
        public string HEAD_NAME { get; set; }
        public int HEAD_TYPE { get; set; }
        public decimal HEAD_AMOUNT { get; set; }
        public decimal SALARY { get; set; }
    }
    public class PayslipReportRequest
    {
        public DateTime Month { get; set; }
        public List<int> EmployeeIDs { get; set; }
    }
    public class PayslipReportResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<PaySlipReport> PaySlipDetails { get; set; }
       // public List<PaySlipReportData> Details { get; set; }
        //public List<PaySlipReport> SalaryHeadDetails { get; set; }
    }
}
