namespace MicroApi.Models
{
    public class PromotionSchema
    {
        public int? ID { get; set; }
        public string? DESCRIPTION { get; set; }
        public float? QTY_BUY { get; set; }
        public float? QTY_GET { get; set; }
        public float? DISC_PERCENT { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public int? SCHEMA_TYPE_ID { get; set; }
        public string? SCHEMA_TYPE { get; set; }
        public bool? SCHEMA_ON_REGULAR_PRICE { get; set; }
        public bool? SCHEMA_ON_QUANTITY_MULTIPLE { get; set; }
        public List<PromotionSchemaEntry> promotionschema_entry { get; set; }
    }
    public class PromotionSchemaEntry
    {
        public int? ID { get; set; }
        public int? PROMOTION_ID { get; set; }
        public float? QTY_BUY { get; set; }
        public float? DISC_PERCENT { get; set; }
        public string? DESCRIPTION { get; set; }
    }
    public class PromotionSchemaResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<PromotionSchema> promotion_data { get; set; }
        public PromotionSchema data { get; set; }
    }
}
