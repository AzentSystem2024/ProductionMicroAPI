namespace MicroApi.Models
{
    public class Country
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string? FLAG_URL { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class CountryResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Country data { get; set; }
    }
}
