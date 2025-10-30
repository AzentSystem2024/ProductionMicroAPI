namespace MicroApi.Models
{
    public class CustomerAddress
    {
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? LOCATION { get; set; }
        public string? MOBILE { get; set; }
        public string? PHONE { get; set; }
        public bool? IS_INACTIVE { get; set; }
    }
    public class CustomerAddressUpdate
    {
        public int ID { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? LOCATION { get; set; }
        public string? MOBILE { get; set; }
        public string? PHONE { get; set; }
        public bool? IS_INACTIVE { get; set; }
    }
    public class CustomerAddressResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        //public CustomerUpdate data { get; set; }
    }
}
