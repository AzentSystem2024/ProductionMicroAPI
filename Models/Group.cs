namespace MicroApi.Models
{
    public class Group
    {
        public int? GroupID { get; set; }
        public string GroupName { get; set; }
        public int? GroupSuperID { get; set; }
        public int? GroupLevel { get; set; }

    }
    public class GroupResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Group> Data { get; set; } = new List<Group>();



    }
}
