namespace MicroApi.Models
{
    public class Reasons
    {
        public int ID { get; set; }

        public int COMPANY_ID { get; set; }

        public string CODE { get; set; }

        public string DESCRIPTION { get; set; }

        public int REASON_TYPE { get; set; }

        public DateTime START_DATE { get; set; }

        public DateTime END_DATE { get; set; }

        public string ARABIC_DESCRIPTION { get; set; }

        public int DISCOUNT_TYPE { get; set; }
        public int? AC_HEAD_ID { get; set; }
        public float DISCOUNT_PERCENT { get; set; }

        public bool IS_DELETED { get; set; }

        public List<REASON_STORES> reason_stores { get; set; }
    }
    public class REASON_STORES
    {
        public int ID { get; set; }

        public string STORE_ID { get; set; }

    }
    public class Response
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Reasons data { get; set; }
    }
}
