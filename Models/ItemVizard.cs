namespace MicroApi.Models
{
    public class ItemVizard
    {
        public int ID { get; set; }
        public int HQID { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string POS_DESCRIPTION { get; set; }
        public string ARABIC_DESCRIPTION { get; set; }
        public int TYPE_ID { get; set; }
        public string TYPE_NAME { get; set; }
        public int DEPT_ID { get; set; }
        public string DEPT_NAME { get; set; }
        public int CAT_ID { get; set; }
        public string CAT_NAME { get; set; }
        public int SUBCAT_ID { get; set; }
        public string SUBCAT_NAME { get; set; }
        public int BRAND_ID { get; set; }
        public string BRAND_NAME { get; set; }
        public string LONG_DESCRIPTION { get; set; }
        public float? ITEM_SALE_PRICE { get; set; }
        public float? ITEM_COST { get; set; }
        public float? PROFIT_MARGIN { get; set; }
        public float? QTY_STOCK { get; set; }
        public float? QTY_COMMITTED { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? LAST_PO_DATE { get; set; }
        public DateTime? LAST_GRN_DATE { get; set; }
        public float? RESTOCK_LEVEL { get; set; }
        public float? REORDER_POINT { get; set; }
        public DateTime? LAST_SALE_DATE { get; set; }
        public int PARENT_ITEM_ID { get; set; }  // Keep this as int, but ensure it has a default value if needed
        public float? CHILD_QTY { get; set; }

        public int ORIGIN_COUNTRY { get; set; }
        public string COUNTRY_NAME { get; set; }
        public int SHELF_LIFE { get; set; }
        public string NOTES { get; set; }
        public string BIN_LOCATION { get; set; }
        public bool? ITEM_IS_INACTIVE { get; set; }
        public bool? ITEM_IS_PRICE_REQUIRED { get; set; }
        public bool? ITEM_IS_NOT_DISCOUNTABLE { get; set; }
        public bool? ITEM_IS_NOT_PURCH_ITEM { get; set; }
        public bool? ITEM_IS_NOT_SALE_ITEM { get; set; }
        public bool? ITEM_IS_NOT_SALE_RETURN { get; set; }
        public bool? IS_BLOCKED { get; set; }


        public string IMAGE_NAME { get; set; }
        public int ITEM_SL { get; set; }
        public float? ITEM_SALE_PRICE1 { get; set; }
        public float? ITEM_SALE_PRICE2 { get; set; }
        public float? ITEM_SALE_PRICE3 { get; set; }
        public float? ITEM_SALE_PRICE4 { get; set; }
        public float? ITEM_SALE_PRICE5 { get; set; }
        public float? PURCH_PRICE { get; set; }
        public int PURCH_CURRENCY { get; set; }
        public bool? IS_CONSIGNMENT { get; set; }
        public int VAT_CLASS_ID { get; set; }
        public string VAT_NAME { get; set; }
        public int ITEM_PROPERTY1 { get; set; }
        public int ITEM_PROPERTY2 { get; set; }
        public int ITEM_PROPERTY3 { get; set; }
        public int ITEM_PROPERTY4 { get; set; }
        public int ITEM_PROPERTY5 { get; set; }
        public int COSTING_METHOD { get; set; }
        public string COSTINGMETHOD { get; set; }
        public int PACKING_ID { get; set; }
        public string PACKING { get; set; }
        public int UNIT_ID { get; set; }
        public string UOM { get; set; }

        //public int ID { get; set; }
        public int STORE_ID { get; set; }
        public float? STORE_SALE_PRICE { get; set; }
        public float? STORE_SALE_PRICE1 { get; set; }
        public float? STORE_SALE_PRICE2 { get; set; }
        public float? STORE_SALE_PRICE3 { get; set; }
        public float? STORE_SALE_PRICE4 { get; set; }
        public float? STORE_SALE_PRICE5 { get; set; }
        public bool? STORE_IS_INACTIVE { get; set; }
        public bool? STORE_IS_NOT_SALE_ITEM { get; set; }
        public bool? STORE_IS_NOT_SALE_RETURN { get; set; }
        public bool? STORE_IS_PRICE_REQUIRED { get; set; }
        public bool? STORE_IS_NOT_DISCOUNTABLE { get; set; }
        public string STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public float COST { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public List<ITEM_STORES> item_stores { get; set; }

    }
    public class ItemVizardInput
    {
        public int STORE_ID { get; set; }
    }
    public class ItemVizardResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ItemVizard> data { get; set; }
        public List<ItemPriceWizard> PriceWizardData { get; set; }

    }

    public class ItemVizardStore
    {
        public int ID { get; set; }
        public int ITEM_ID { get; set; }
        public int STORE_ID { get; set; }
        public float STORE_SALE_PRICE { get; set; }
        public float STORE_SALE_PRICE1 { get; set; }
        public float STORE_SALE_PRICE2 { get; set; }
        public float STORE_SALE_PRICE3 { get; set; }
        public float STORE_SALE_PRICE4 { get; set; }
        public float STORE_SALE_PRICE5 { get; set; }
        public float COST { get; set; }
        public bool STORE_IS_INACTIVE { get; set; }
        public bool STORE_IS_NOT_SALE_ITEM { get; set; }
        public bool STORE_IS_NOT_SALE_RETURN { get; set; }
        public bool STORE_IS_PRICE_REQUIRED { get; set; }
        public bool STORE_IS_NOT_DISCOUNTABLE { get; set; }
    }

    public class ItemPriceWizardInput
    {
        public string STORE_ID { get; set; }
    }
    public class ItemPriceWizard
    {
        public int ID { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string CAT_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public string DEPT_NAME { get; set; }
        public string STORE_ID { get; set; }
        public float SALE_PRICE { get; set; }
        public float SALE_PRICE1 { get; set; }
        public float SALE_PRICE2 { get; set; }
        public float SALE_PRICE3 { get; set; }
        public float SALE_PRICE4 { get; set; }
        public float SALE_PRICE5 { get; set; }
        public float COST { get; set; }

    }
}
