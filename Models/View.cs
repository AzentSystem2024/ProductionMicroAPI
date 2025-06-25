namespace MicroApi.Models
{
    public class View
    {
        public int ARTICLE_ID { get; set; }
        public string ART_NO { get; set; }
        public string COLOR { get; set; }
        public string CATEGORY { get; set; }
        public int SIZE { get; set; }
        public string PRODUCTION_UNIT { get; set; }
        public int QTY_AVAILABLE { get; set; }
        public int QTY_MULTIBOX { get; set; }
        public int QTY_TOTAL { get; set; }
    }
    public class ViewResponse
    {
        public List<View> Data { get; set; }
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class ArticleStockViewRequest
    {
        public int USER_ID { get; set; }
    }
}
