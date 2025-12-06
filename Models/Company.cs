namespace MicroApi.Models
{
    public class Company
    {
        public string COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public string CONTACT_NAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string WHATSAPP { get; set; }
        public int COMPANY_TYPE { get; set; }
        public int? STATE_ID { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public string? GST_NO { get; set; }
        public string? PAN_NO { get; set; }
        public string? CIN { get; set; }
    }
    public class CompanyUpdate
    {
        public int ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public string CONTACT_NAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string WHATSAPP { get; set; }
        public int COMPANY_TYPE { get; set; }
        public string? COMPANY_TYPE_NAME { get; set; }
        public bool IS_INACTIVE { get; set; }
        public int? STATE_ID { get; set; }
        public string? GST_NO { get; set; }
        public string? PAN_NO { get; set; }
        public string? CIN { get; set; }
    }
    public class CompanyResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public CompanyUpdate Data { get; set; } 

    }
    public class CompanyListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<CompanyUpdate> Data { get; set; }
    }
}
