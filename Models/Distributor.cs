namespace MicroApi.Models
{
    public class Distributor
    {
        public string CODE { get; set; }
        public string DISTRIBUTOR_NAME { get; set; }
        public string ADDRESS { get; set; }

        public int COUNTRY_ID { get; set; }
        public int STATE_ID { get; set; }
        public int DISTRICT_ID { get; set; }
        public int CITY_ID { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
        public string MOBILE { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string EMAIL { get; set; }
        public bool IS_INACTIVE { get; set; }
        public string SALESMAN_EMAIL { get; set; }
        public int WAREHOUSE_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public decimal DISC_PERCENT { get; set; }
        public decimal CREDIT_LIMIT { get; set; }
        public int CREDIT_DAYS { get; set; }
        public int ZONE_ID { get; set; }
        public string LOGIN_NAME { get; set; }
        public string LOGIN_PASSWORD { get; set; }
        public int PARENT_ID { get; set; }

        public List<DistributorLocation> LOCATIONS { get; set; }
    }

    public class DistributorUpdate
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DISTRIBUTOR_NAME { get; set; }
        public string ADDRESS { get; set; }
        public int COUNTRY_ID { get; set; }          
        public bool IS_INACTIVE { get; set; }
        public int STATE_ID { get; set; }
        public string? STATE_NAME { get; set; }
        public int DISTRICT_ID { get; set; }
        public int CITY_ID { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
        public string MOBILE { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string EMAIL { get; set; }
       // public bool? IS_ACTIVE { get; set; }
        public string SALESMAN_EMAIL { get; set; }
        public int WAREHOUSE_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public decimal DISC_PERCENT { get; set; }
        public decimal CREDIT_LIMIT { get; set; }
        public int CREDIT_DAYS { get; set; }
        public int ZONE_ID { get; set; }
        public string LOGIN_NAME { get; set; }
        public string LOGIN_PASSWORD { get; set; }
        public int? PARENT_ID { get; set; }
        public List<DistributorLocationUpdate> LOCATIONS { get; set; }

    }
    public class DistributorResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public DistributorUpdate Data { get; set; }
    }
    public class DistributorListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DistributorUpdate> Data { get; set; }
    }
    public class DistributorLocation
    {
        public int DISTRIBUTOR_ID { get; set; }
        public string LOCATION { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE { get; set; }
        public string TELEPHONE { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class DistributorLocationUpdate
    {
        public int ID { get; set; }
        public int DISTRIBUTOR_ID { get; set; }
        public string LOCATION { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE { get; set; }
        public string TELEPHONE { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class District
    {
        public string DISTRICT_NAME { get; set; }
        public int STATE_ID { get; set; }
        public int COUNTRY_ID { get; set; }
    }
    public class InsertResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class City
    {
        public string CITY_NAME { get; set; }
        public int DISTRICT_ID { get; set; }
        public int STATE_ID { get; set; }
        public int COUNTRY_ID { get; set; }
        public int STD_CODE { get; set; }
    }
   
}
