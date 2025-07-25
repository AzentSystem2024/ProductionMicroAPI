namespace MicroApi.Models
{
    public class Article
    {
        public string ART_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string COLOR { get; set; }
        public string SIZE { get; set; }
        public float PRICE { get; set; }
        public int PACK_QTY { get; set; }
        public string PART_NO { get; set; }
        public string ALIAS_NO { get; set; }
        public int UNIT_ID { get; set; }
        public int ARTICLE_TYPE { get; set; }
        public int CATEGORY_ID { get; set; }
        public string IMAGE_NAME { get; set; }
        public int BRAND_ID { get; set; }
        public int NEW_ARRIVAL_DAYS { get; set; }
        public bool IS_STOPPED { get; set; }
        public int SUPPLIER_ID { get; set; }
        public bool IS_COMPONENT { get; set; }
        public int? COMPONENT_ARTICLE_ID { get; set; }
    }

    public class ArticleResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public ArticleUpdate Data { get; set; }
    }

    public class ArticleUpdate
    {
        public long ID { get; set; }
        public string ART_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string LAST_ORDER_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string COLOR { get; set; }
        public string SIZE { get; set; }
        public float PRICE { get; set; }
        public int PACK_QTY { get; set; }
        public string PART_NO { get; set; }
        public string ALIAS_NO { get; set; }
        public int UNIT_ID { get; set; }
        //public string? UnitCode { get; set; }
        public int ARTICLE_TYPE { get; set; }
        public string? ARTICLE_TYPE_NAME { get; set; }
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int BRAND_ID { get; set; }
        public string? BRAND_NAME { get; set; }
        public int NEXT_SERIAL { get; set; }
        public string IMAGE_NAME { get; set; }
        public int NEW_ARRIVAL_DAYS { get; set; }
        public bool IS_STOPPED { get; set; }
        public int SUPPLIER_ID { get; set; }
        public string? SupplierName { get; set; }
        public bool IS_COMPONENT { get; set; }
        public int? COMPONENT_ARTICLE_ID { get; set; }
        public string ComponentArticleNo { get; set; }
        public string ComponentArticleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Sizes> Sizes { get; set; }
    }


    public class ArticleListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ArticleUpdate> Data { get; set; }
    }
    public class Sizes
    {
        public int ID { get; set; }
        public string Size { get; set; }
        public string OrderNo { get; set; }
    }
    public class ArticleSelectRequest
    {
        public int UnitID { get; set; }
        public string ArtNo { get; set; }
        public string Color { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
    }


}
