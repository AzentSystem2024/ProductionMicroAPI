namespace MicroApi.Models
{
    public class ArticleProduction
    {
        public int UNIT_ID { get; set; }
        public int USER_ID { get; set; }
        public List<ArticleProduction_Item> Articles { get; set; }
    }
    public class ArticleProduction_Item
    {
        public long ARTICLE_PRODUCTION_ID { get; set; }  
        public long ARTICLE_ID { get; set; }            
        public int PAIRS { get; set; }
        public int BOX_ID { get; set; }
        public string BARCODE { get; set; }
        public float PRICE { get; set; }

    }
    public class ArticleProdResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }

}
