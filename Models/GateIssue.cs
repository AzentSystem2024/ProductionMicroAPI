namespace MicroApi.Models
{
    public class GateIssue
    {
        public int COMPANY_ID { get; set; }
        public string ISSUE_TO { get; set; }
        public string ISSUED_BY { get; set; }
        public DateTime ISSUE_TIME { get; set; }
        public string REMARKS { get; set; }

        public List<GateIssueArticle> Articles { get; set; }
    }
    public class GateIssueArticle
    {
        public long ARTICLE_PRODUCTION_ID { get; set; }
    }
    public class GateIssueResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }

}
