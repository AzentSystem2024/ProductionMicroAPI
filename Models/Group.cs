namespace MicroApi.Models
{
    public class Group
    {
        public int? GROUP_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public int? GROUP_SUPER_ID { get; set; }
        public int? GROUP_LEVEL { get; set; }

    }
    public class GroupResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Group> Data { get; set; } = new List<Group>();



    }
}
