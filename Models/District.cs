namespace MicroApi.Models
{
    public class District
    {
        public int? ID { get; set; }
        public string? DISTRICT_NAME { get; set; }
        public int? COUNTRY_ID { get; set; }
        public int? STATE_ID { get; set; }

    }
    public class DistrictResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public DistrictList Data { get; set; }

    }
    public class DistrictListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DistrictList> Data { get; set; }
    }
    public class DistrictList
    {
        public int ID { get; set; }
        public int COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public int STATE_ID { get; set; }
        public string STATE_NAME { get; set; }
        public string DISTRICT_NAME { get; set; }
    }
}
