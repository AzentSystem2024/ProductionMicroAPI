namespace MicroApi.Models
{
    public class EPFReport
    {
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public string? PFAccountNo { get; set; }
        public decimal? Salary { get; set; }
        public decimal? EmployeeShare { get; set; }
        public decimal? A_C_01 { get; set; }
        public decimal? A_C_10 { get; set; }
        public decimal? EPFContributionOfEmployer { get; set; }
        public decimal? EmployeesPensionFund { get; set; }
        public decimal? A_C_No_21 { get; set; }
        public decimal? AdministrativeChargesA_C_2 { get; set; }
        public decimal? A_C_No_22 { get; set; }
    }
    public class EPFReportRequest
    {
        public string Month { get; set; }
    }
    public class EPFReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<EPFReport> EPFDetails { get; set; }

    }
}
