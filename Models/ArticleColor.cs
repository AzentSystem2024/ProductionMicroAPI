namespace MicroApi.Models
{
    public class ArticleColor
    {
        public string CODE { get; set; }
        public string COLOR_ENGLISH { get; set; }
        public string COLOR_ARABIC { get; set; }
    }
    public class ArticleColorUpdate
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string COLOR_ENGLISH { get; set; }
        public string COLOR_ARABIC { get; set; }
    }
    public class ArticleColorResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public ArticleColorUpdate Data { get; set; }

    }
    public class ArticleColorListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ArticleColorUpdate> Data { get; set; }

    }

}
