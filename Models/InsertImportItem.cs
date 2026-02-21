namespace MicroApi.Models
{
    public class InsertImportItem
    {
        public int ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string POS_DESCRIPTION { get; set; }
        public string ARABIC_DESCRIPTION { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_NAME { get; set; }
        public string CAT_CODE { get; set; }
        public string CAT_NAME { get; set; }
        public string SUBCAT_CODE { get; set; }
        public string SUBCAT_NAME { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string SUPP_CODE { get; set; }
        public string SUPP_NAME { get; set; }
        public string SUPP_PRICE { get; set; }
        public string COST { get; set; }
        public string VAT_PERCENT { get; set; }
        public string PRICE { get; set; }
        public string ITEM_TYPE { get; set; }
        public string IS_DISCOUNTABLE { get; set; }
        public string COSTING_METHOD { get; set; }
        public string IS_NOT_SALE_ITEM { get; set; }

        // public int USER_ID { get; set; }
        public int flag { get; set; }
        public string message { get; set; }

    }
}
