namespace MicroApi.Models
{
    public class Insurance
    {
        //public int ID { get; set; }
        public string INSURANCE_NAME { get; set; } = string.Empty;
        public bool IS_INACTIVE { get; set; }

    }
    public class InsuranceListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<InsuranceUpdate> Data { get; set; }
    }
    public class InsuranceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public InsuranceUpdate Data { get; set; }
    }
    public class InsuranceUpdate
    {
        public int ID { get; set; }
       
        public string INSURANCE_NAME { get; set; } = string.Empty;
        public bool IS_INACTIVE { get; set; }


    }
}
