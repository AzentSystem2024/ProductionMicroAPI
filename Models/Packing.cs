namespace MicroApi.Models
{
    public class PackingMasters
    {
        public int? COMPANY_ID { get; set; }
        public string? ART_NO { get; set; }
        public string? ORDER_NO { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? COLOR { get; set; }
        public int? PAIR_QTY { get; set; }
        public string? ART_SERIAL { get; set; }
        public float? PACK_PRICE { get; set; }
        public float? COST {  get; set; }
       // public int PACK_QTY { get; set; }
        public string? PART_NO { get; set; }
        public string? ALIAS_NO { get; set; }
        public int? UNIT_ID { get; set; }
        public int? ARTICLE_TYPE { get; set; }
        public int? CATEGORY_ID { get; set; }
       // public string IMAGE_NAME { get; set; }
        public int? BRAND_ID { get; set; }
        public int? SUPP_ID { get; set; }
        public string? COMBINATION {  get; set; }
        public int? NEW_ARRIVAL_DAYS { get; set; }
        public bool? IS_ANY_QTY { get; set; }
        public bool? IS_PURCHASABLE { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public bool? IS_EXPORT {  get; set; }
        public bool? IS_STOPPED { get; set; }
        public bool? IS_ANY_ARTICLES {  get; set; }
       
        public bool? IS_ANY_COMB { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public string? SIZE { get; set; }
        public List<PackingBOM> BOM { get; set; }
        public List<PackingEntry> PackingEntries { get; set; }
    }
    public class PackingEntry
    {
        public long? ARTICLE_ID { get; set; }
        public float? QUANTITY { get; set; }
        public string? SIZE { get; set; }
    }

    public class PackingBOM
    {

        public int? ITEM_ID { get; set; }
        public float? QUANTITY { get; set; }
        public int? PACKING_ID { get; set; }
        public int? BOM_ID { get; set; }// FOR SELECT
        public string? DESCRIPTION { get; set; }
        public string? UOM { get; set; }
        public string? ITEM_CODE { get; set; }

    }
    public class PackingResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public PackingUpdate Data { get; set; }
    }
    public class PackingUpdate
    {
        public long ID { get; set; }
        public int? COMPANY_ID { get; set; }

        public string? ART_NO { get; set; }
        public string? ORDER_NO { get; set; }
        //public string LAST_ORDER_NO { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? COLOR { get; set; }
        public int? PAIR_QTY { get; set; }
        public string? PART_NO { get; set; }
        public string? ALIAS_NO { get; set; }
        public float? PACK_PRICE { get; set; }
        public int? UNIT_ID { get; set; }
        //public string UnitCode { get; set; }
        public int? ARTICLE_TYPE { get; set; }
        //public string ARTICLE_TYPE_NAME { get; set; }
        public int? CATEGORY_ID { get; set; }
       // public string CATEGORY_NAME { get; set; }
        public int? BRAND_ID { get; set; }
       // public string BRAND_NAME { get; set; }
        public string? ART_SERIAL { get; set; }
       // public string IMAGE_NAME { get; set; }
        public int? NEW_ARRIVAL_DAYS { get; set; }
        public bool? IS_STOPPED { get; set; }
        public int? SUPP_ID { get; set; }
        public bool? IS_ANY_QTY { get; set; }
        // public string SupplierName { get; set; }
        public string? COMBINATION { get; set; }
        public bool? IS_PURCHASABLE { get; set; }
        public bool? IS_EXPORT { get; set; }
        public bool? IS_ANY_COMB { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public DateTime?  CreatedDate { get; set; }
        public float? COST { get; set; }
        public List<PackingBOM> BOM { get; set; }
        public List<PackingEntry> PackingEntries { get; set; }

    }
    public class PackingListItem
    {
        public long ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string ArtNo { get; set; }
        public string Color { get; set; }
        public string OrderNo { get; set; }
        public string Category { get; set; }
        public string PackingName { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public string PartNo { get; set; }
        public string AliasNo { get; set; }
        public int PairQty { get; set; }
        public string Status { get; set; }
    }

    public class PackingListResponses
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<PackingListItem> Data { get; set; }
    }
    public class ProductionUnit
    {
        public int ID { get; set; }
        public string UnitName { get; set; }
    }

    public class ArticleSize
    {
        public long ArticleID { get; set; }
        public string Size { get; set; }
    }

    public class Supplier
    {
        public int ID { get; set; }
        public string UnitName { get; set; }
    }
    public class PackingSelect
    {
        public long ID { get; set; }
        public int COMPANY_ID { get; set; }

        public string ART_NO { get; set; }
        public string ORDER_NO { get; set; }
        //public string LAST_ORDER_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string COLOR { get; set; }
        public float? PACK_PRICE { get; set; }
        public int PAIR_QTY { get; set; }
        public string PART_NO { get; set; }
        public string ALIAS_NO { get; set; }
        public int UNIT_ID { get; set; }
        //public string UnitCode { get; set; }
        public int ARTICLE_TYPE { get; set; }
        //public string ARTICLE_TYPE_NAME { get; set; }
        public int CATEGORY_ID { get; set; }
        // public string CATEGORY_NAME { get; set; }
        public int BRAND_ID { get; set; }
        // public string BRAND_NAME { get; set; }
        public string ART_SERIAL { get; set; }
        // public string IMAGE_NAME { get; set; }
        public int NEW_ARRIVAL_DAYS { get; set; }
        public bool IS_STOPPED { get; set; }
        public int SUPP_ID { get; set; }
        public bool IS_ANY_QTY { get; set; }
        // public string SupplierName { get; set; }
        public string COMBINATION { get; set; }
        public bool IS_PURCHASABLE { get; set; }
        public bool IS_EXPORT { get; set; }
        public bool IS_ANY_COMB { get; set; }
        public bool IS_INACTIVE { get; set; }
        public DateTime CreatedDate { get; set; }
        public float? COST { get; set; }
        public List<PackingBOM> BOM { get; set; }
        public List<Packing_Entry> PackingEntries { get; set; }

    }
    public class Packing_Entry
    {
        public int ENTRY_ID { get; set; }
        public int PACK_ID { get; set; }
        public int ARTICLE_ID { get; set; }
        public float QUANTITY { get; set; }
        public string SIZE { get; set; }
        public int UNIT_ID { get; set; }
    }
    public class PackingSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } 
        public PackingSelect Data { get; set; }   
    }
    public class PackingListReq
    {
        public int COMPANY_ID { get; set; }
       
    }
    public class ArticleSizeCombinationRequest
    {
        public string artNo { get; set; }
        public string color { get; set; }
        public int categoryID { get; set; }
        public int unitID { get; set; }
        public int COMPANY_ID { get; set; }
    }


}
