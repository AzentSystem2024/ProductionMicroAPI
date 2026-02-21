namespace MicroApi.Models
{
    public class ImportItemLog
    {
        public int ID { get; set; }
        public int? BATCH_NO { get; set; }
        public DateTime? IMPORT_DATE { get; set; }
        public int? TEMPLATE_ID { get; set; }
        public string? STORE_ID { get; set; }
        public string? REMARKS { get; set; }
        public int? USER_ID { get; set; }
        public string? USER_NAME { get; set; }
        public string? STORE_NAME { get; set; }
        public string? TEMPLATE_NAME { get; set; }

        public List<InsertImportItemLogEntry>? importitem_logentry { get; set; }
    }
    public class InsertImportItemLogEntry
    {
        public int? ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? BARCODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? POS_DESCRIPTION { get; set; }
        public string? ARABIC_DESCRIPTION { get; set; }
        public string? DEPT_CODE { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? CAT_CODE { get; set; }
        public string? CAT_NAME { get; set; }
        public string? SUBCAT_CODE { get; set; }
        public string? SUBCAT_NAME { get; set; }
        public string? BRAND_CODE { get; set; }
        public string? BRAND_NAME { get; set; }
        public string? SUPP_CODE { get; set; }
        public string? SUPP_NAME { get; set; }
        public float? SUPP_PRICE { get; set; }
        public float? COST { get; set; }
        public float? VAT_PERCENT { get; set; }
        public float? PRICE { get; set; }
        public string? ITEM_TYPE { get; set; }
        public string? IS_DISCOUNTABLE { get; set; }
        public string? COSTING_METHOD { get; set; }
        public string? IS_SELLABLE { get; set; }
        public Int32? SHELF_LIFE { get; set; }

    }
    public class ImportItemResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public ImportItemLog data { get; set; }
    }
}
