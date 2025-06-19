namespace MicroApi.Models
{
    public class Articlelist
    {
        public string ART_NO { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal PRICE { get; set; }
        public string ArticleType { get; set; }
        public string Brand { get; set; }
        public string ALIAS_NO { get; set; }
        public string PART_NO { get; set; }
        public string Status { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class ArticleLookUpResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
       public List<Articlelist> Data { get; set; }
    }
}
