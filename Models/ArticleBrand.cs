namespace MicroApi.Models
{
    public class ArticleBrand
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_INACTIVE { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class ArticleBrandUpdate
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_INACTIVE { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class ArticleBrandResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public ArticleBrandUpdate Data { get; set; }

    }
    public class ArticleBrandListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ArticleBrandUpdate> Data { get; set; }

    }
    public class ArticleBrandListRequest
    {
        public int COMPANY_ID { get; set; }
      
    }
}
