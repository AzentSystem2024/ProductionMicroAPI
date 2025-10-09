namespace MicroApi.Models
{
    public class ItemStoreProperties
    {
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string DEPT_NAME { get; set; }
        public string CAT_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public int ITEM_ID { get; set; }
        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public bool IS_INACTIVE { get; set; }
        public bool IS_NOT_SALE_ITEM { get; set; }
        public bool IS_NOT_SALE_RETURN { get; set; }
        public bool IS_PRICE_REQUIRED { get; set; }
        public bool IS_NOT_DISCOUNTABLE { get; set; }
        public bool Selected { get; set; }
    }

    public class ItemStorePropertiesResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ItemStoreProperties> data { get; set; }
    }
}
