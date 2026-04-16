namespace MicroApi.Models
{
    public class ItemBrand
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string BRAND_NAME { get; set; }

        public string COMPANY_ID { get; set; }
        //public int COMPANY_ID { get; set; }
        public String COMPANY_NAME { get; set; }
    }
    public class ItemBrandResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ItemBrand> data { get; set; }
    }
}
