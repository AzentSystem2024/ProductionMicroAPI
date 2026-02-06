using Newtonsoft.Json;

namespace MicroApi.Models
{
    public class WorksheetItem
    {
        public int? ID { get; set; }

        public string? WS_NO { get; set; }
        public DateTime? WS_DATE { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? STORE_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? flag { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? USER_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? COMPANY_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? NARRATION { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<WorksheetItemProperties>? worksheet_item_property { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ItemStoreProperty>? worksheet_item_store { get; set; }
        public List<WorksheetItemPrice>? worksheet_item_price { get; set; }
        public List<WorksheetPromotionSchema>? worksheet_promotion_schema { get; set; }
    }
    public class WorksheetItemProperties
    {
        public int? ID { get; set; }
        public int? ITEM_ID { get; set; }
        public bool? IS_PRICE_REQUIRED { get; set; }
        public bool? IS_PRICE_REQUIRED_NEW { get; set; }
        public bool? IS_NOT_DISCOUNTABLE { get; set; }
        public bool? IS_NOT_DISCOUNTABLE_NEW { get; set; }
        public bool? IS_NOT_SALE_ITEM { get; set; }
        public bool? IS_NOT_SALE_ITEM_NEW { get; set; }
        public bool? IS_NOT_SALE_RETURN { get; set; }
        public bool? IS_NOT_SALE_RETURN_NEW { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public bool? IS_INACTIVE_NEW { get; set; }
        public string? BARCODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? CAT_NAME { get; set; }
        public string? BRAND_NAME { get; set; }
        public bool? Selected { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }

    }
    public class ItemStoreProperty
    {
        public int? ID { get; set; }
        public int? WS_ID { get; set; }
        public int? STORE_ID { get; set; }

    }
    public class WorksheetItemPropertiesResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<WorksheetItem> dataworksheet { get; set; }
        public WorksheetItem data { get; set; }
    }
    public class WorksheetItemPrice
    {
        public int? ID { get; set; }
        public int? ITEM_ID { get; set; }

        public float? SALE_PRICE { get; set; }

        public float? SALE_PRICE1 { get; set; }

        public float? SALE_PRICE2 { get; set; }

        public float? SALE_PRICE3 { get; set; }

        public float? SALE_PRICE4 { get; set; }

        public float? SALE_PRICE5 { get; set; }
        public float? PRICE_NEW { get; set; }
        public float? PRICE_LEVEL1_NEW { get; set; }
        public float? PRICE_LEVEL2_NEW { get; set; }
        public float? PRICE_LEVEL3_NEW { get; set; }
        public float? PRICE_LEVEL4_NEW { get; set; }
        public float? PRICE_LEVEL5_NEW { get; set; }
        public string? BARCODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? CAT_NAME { get; set; }
        public string? BRAND_NAME { get; set; }
        public bool? Selected { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? ITEM_TYPE { get; set; }
    }
    public class ItemPriceProperties
    {
        public string? BARCODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? CAT_NAME { get; set; }
        public string? BRAND_NAME { get; set; }
        public int? ITEM_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public float? SALE_PRICE { get; set; }
        public float? SALE_PRICE1 { get; set; }
        public float? SALE_PRICE2 { get; set; }
        public float? SALE_PRICE3 { get; set; }
        public float? SALE_PRICE4 { get; set; }
        public float? SALE_PRICE5 { get; set; }
        public bool? Selected { get; set; }
    }
    public class ItemPricePropertiesResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ItemPriceProperties> data { get; set; }
    }

    public class WorksheetPromotionSchema
    {
        public int? ID { get; set; }
        public int? ITEM_ID { get; set; }
        public float? PRICE { get; set; }
        public float? COST { get; set; }
        public float? PROMOTION_PRICE { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
        public DateTime? TIME_FROM { get; set; }
        public DateTime? TIME_TO { get; set; }
        public int? PROMOTION_SCHEMA_ID { get; set; }
        public string? PROMOTION_SCHEMA { get; set; }
        public string? PROMOTION_WEEKDAYS { get; set; }
        public int? PROMOTION_LEVEL { get; set; }
        public string? PROMOTION_LEVEL_NAME { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public string? PROMOTION_NAME { get; set; }
        public int? PROMOTION_GROUP_ID { get; set; }
        public bool? IS_BUY { get; set; }
        public bool? IS_GET { get; set; }
        public bool? IS_HAPPY_HOUR { get; set; }

        public int? CAT_ID { get; set; }
        public int? DEPT_ID { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? CAT_NAME { get; set; }
        public string? BARCODE { get; set; }
        public string? ITEM_DESCRIPTION { get; set; }
        public string? NARRATION { get; set; }

    }
}
