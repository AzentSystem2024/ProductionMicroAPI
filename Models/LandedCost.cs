using Newtonsoft.Json;

namespace MicroApi.Models
{
    public class LandedCost
    {
        public int ID { get; set; }

        public string DESCRIPTION { get; set; }

        public bool IS_LOCAL_CURRENCY { get; set; }

        public bool IS_FIXED_AMOUNT { get; set; }

        public int COMPANY_ID { get; set; }

        public int AC_HEAD_ID { get; set; }

        public float VALUE { get; set; }

        public string COMPANY_NAME { get; set; }

        public bool IS_INACTIVE { get; set; }

        public bool IS_DELETED { get; set; }
    }
    public class LandedCostResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public LandedCost data { get; set; }
    }
}
