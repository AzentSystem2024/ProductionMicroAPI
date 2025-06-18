namespace MicroApi.Models
{
    public class Distributor
    {
        public string CODE { get; set; }
        public string DISTRIBUTOR_NAME { get; set; }
        public string ADDRESS { get; set; }
        public int COUNTRY_ID { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string FLAG_URL { get; set; }
        public bool IS_INACTIVE { get; set; }
        public int STATE_ID { get; set; }
        public string STATE_NAME { get; set; }
        public int DISTRICT_ID { get; set; }
        public string DISTRICT_NAME { get; set; }
        public int CITY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public int STD_CODE { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public bool IS_ACTIVE { get; set; }
        public string SALESMAN_EMAIL { get; set; }
        public int WAREHOUSE_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public decimal DISC_PERCENT { get; set; }
        public decimal CREDIT_LIMIT { get; set; }
        public int CREDIT_DAYS { get; set; }
        public int ZONE_ID { get; set; }
        public int PARENT_ID { get; set; }
    }
    public class DistributorResponse
    {
        public int flag { get; set; }          
        public string Message { get; set; }
    }

}
