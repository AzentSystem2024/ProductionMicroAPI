namespace MicroApi.Models
{
    public class ESIReport
    {
        public string ESI_No { get; set; }
        public string Staff_Name { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Employee_Share { get; set; }
        public decimal? Employer_Share { get; set; }
    }
    public class ESIReportRequest
    {
        public string Month { get; set; }
    }

    public class ESIReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ESIReport> ESIDetails { get; set; }
    }
}
