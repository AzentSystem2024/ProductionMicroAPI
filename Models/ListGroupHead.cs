namespace MicroApi.Models
{
    public class ListGroupHead
    {
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string Category { get; set; }
        public string HeadCode { get; set; }
        public string HeadName { get; set; }
        public int ID { get; set; }
    }
    public class ListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ListGroupHead> Data { get; set; }

    }

}
