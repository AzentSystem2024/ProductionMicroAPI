namespace MicroApi.Models
{
    public class ArticleType
    {
        public string DESCRIPTION { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class ArticleTypeUpdate
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class ArticleTypeResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public ArticleTypeUpdate Data { get; set; }

    }
    public class ArticleTypeListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ArticleTypeUpdate> Data { get; set; }

    }
    public class ArticleTypeListReq
    {
        public int COMPANY_ID { get; set; }
      
    }
}
