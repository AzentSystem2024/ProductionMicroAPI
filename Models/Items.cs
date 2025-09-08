namespace MicroApi.Models
{
    public class Items
    {
        public int? ID { get; set; }
        public int? HQID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? BARCODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? POS_DESCRIPTION { get; set; }
        public string? ARABIC_DESCRIPTION { get; set; }
        public int? TYPE_ID { get; set; }
        public string? TYPE_NAME { get; set; }
        public int? DEPT_ID { get; set; }
        public string? DEPT_NAME { get; set; }
        public int? CAT_ID { get; set; }
        public string? CAT_NAME { get; set; }
        public int? SUBCAT_ID { get; set; }
        public string? SUBCAT_NAME { get; set; }
        public int? BRAND_ID { get; set; }
        public string? BRAND_NAME { get; set; }
        // public string PACKING { get; set; }

        public string? LONG_DESCRIPTION { get; set; }
        public float? SALE_PRICE { get; set; }
        public float? COST { get; set; }
        public float? PROFIT_MARGIN { get; set; }
        public float? QTY_STOCK { get; set; }
        public float? QTY_COMMITTED { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? LAST_PO_DATE { get; set; }
        public DateTime? LAST_GRN_DATE { get; set; }
        public float? RESTOCK_LEVEL { get; set; }
        public float? REORDER_POINT { get; set; }
        public DateTime? LAST_SALE_DATE { get; set; }
        public int? PARENT_ITEM_ID { get; set; }
        public float? CHILD_QTY { get; set; }
        public int? ORIGIN_COUNTRY { get; set; }
        public string? COUNTRY_NAME { get; set; }
        public int? SHELF_LIFE { get; set; }
        public string? NOTES { get; set; }
        public string? BIN_LOCATION { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public bool? IS_PRICE_REQUIRED { get; set; }
        public bool? IS_NOT_DISCOUNTABLE { get; set; }
        public bool? IS_NOT_PURCH_ITEM { get; set; }
        public bool? IS_NOT_SALE_ITEM { get; set; }
        public bool? IS_NOT_SALE_RETURN { get; set; }
        public bool? IS_BLOCKED { get; set; }
        public string? IMAGE_NAME { get; set; }
        public int? ITEM_SL { get; set; }
        public float? SALE_PRICE1 { get; set; }
        public float? SALE_PRICE2 { get; set; }
        public float? SALE_PRICE3 { get; set; }
        public float? SALE_PRICE4 { get; set; }
        public float? SALE_PRICE5 { get; set; }
        public float? PURCH_PRICE { get; set; }
        public int? PURCH_CURRENCY { get; set; }
        public bool? IS_CONSIGNMENT { get; set; }
        public int? VAT_CLASS_ID { get; set; }
        public string? VAT_NAME { get; set; }
        public int? ITEM_PROPERTY1 { get; set; }
        public int? ITEM_PROPERTY2 { get; set; }
        public int? ITEM_PROPERTY3 { get; set; }
        public int? ITEM_PROPERTY4 { get; set; }
        public int? ITEM_PROPERTY5 { get; set; }
        public int? COSTING_METHOD { get; set; }
        public string? COSTINGMETHOD { get; set; }
        public int? PACKING_ID { get; set; }
        public string? PACKING { get; set; }
        public int? UNIT_ID { get; set; }
        public string? UOM { get; set; }
        public bool? IS_DIFFERENT_UOM_PURCH { get; set; }
        public int? UOM_PURCH { get; set; }
        public int? UOM_MULTPLE { get; set; }

        public List<ITEM_STORES> item_stores { get; set; }
        public List<ITEM_ALIAS> item_alias { get; set; }
        public List<ITEM_SUPPLIERS> item_suppliers { get; set; }
        public List<ITEM_COMPONENTS> item_components { get; set; }


     

    }
    public class ITEM_ALIAS
    {
        public int? ID { get; set; }
        public string? ALIAS { get; set; }
        public bool? IS_DEFAULT { get; set; }

    }
    public class ITEM_STORES
    {
        public int? ID { get; set; }
        public int? STORE_ID { get; set; }
        public float SALE_PRICE { get; set; }
        public float? SALE_PRICE1 { get; set; }
        public float? SALE_PRICE2 { get; set; }
        public float? SALE_PRICE3 { get; set; }
        public float? SALE_PRICE4 { get; set; }
        public float? SALE_PRICE5 { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public bool? IS_NOT_SALE_ITEM { get; set; }
        public bool? IS_NOT_SALE_RETURN { get; set; }
        public bool? IS_PRICE_REQUIRED { get; set; }
        public bool? IS_NOT_DISCOUNTABLE { get; set; }
        public string? STORE_CODE { get; set; }
        public string? STORE_NAME { get; set; }
        public float? COST { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string? QTY_AVAILABLE { get; set; }
        public bool? IS_SELECTED { get; set; }
    }
    public class ITEM_SUPPLIERS
    {
        public int? ID { get; set; }
        public string? SUPP_ID { get; set; }
        public string? SUPP_NAME { get; set; }
        public string? REORDER_NO { get; set; }
        public float? COST { get; set; }
        public bool? IS_PRIMARY { get; set; }
        public bool? IS_CONSIGNMENT { get; set; }

    }
    public class ITEM_COMPONENTS
    {
        public int? ID { get; set; }
        public int? ITEM_ID { get; set; }
        public int? COMPONENT_ITEM_ID { get; set; }
        public float? QUANTITY { get; set; }
        public string? UOM { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? DESCRIPTION { get; set; }

    }

    public class ALIAS_DUPLICATE
    {
        public string? ALIAS { get; set; }

    }

    public class ItemsResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<Items> data { get; set; }

        //public string ITEM_CODE { get; set; }
        // public string DESCRIPTION { get; set; }

    }
    public class MasterFilter
    {
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }

    }
    public class ItemResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Items data { get; set; }
    }
    public class DateRequest
    {
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
    }

}
