namespace MicroApi.Models
{
    public class Stores
    {
        public int? ID { get; set; }

        public int? COMPANY_ID { get; set; }

        public string? CODE { get; set; }

        public string? STORE_NAME { get; set; }

        public int? AC_HEAD_ID { get; set; }

        public bool? IS_ACTIVE { get; set; }

        public bool? IS_PRODUCTION { get; set; }

        public string? ADDRESS1 { get; set; }

        public string? ADDRESS2 { get; set; }

        public string? ADDRESS3 { get; set; }

        public string? CITY { get; set; }

        public int? COUNTRY_ID { get; set; }
        public int? STATE_ID { get; set; }
        public bool? IS_DEFAULT_STORE { get; set; }

        public string? PHONE { get; set; }

        public string EMAIL { get; set; }

        public string? STORE_NO { get; set; }

        public string? VAT_REGNO { get; set; }

        public int? GROUP_ID { get; set; }

        public string? COMPANY_NAME { get; set; }

        public string? COUNTRY_NAME { get; set; }

        public string? GROUP_NAME { get; set; }
        public string? STATE_NAME { get; set; }

        public string? IS_DELETED { get; set; }

        public String? ZIP_CODE { get; set; }
    }
    public class StoresResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Stores data { get; set; }
    }
    public class StoresList
    {
        public int COMPANY_ID { get; set; }
    }
}
