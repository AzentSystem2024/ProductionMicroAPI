namespace MicroApi.Models
{
    public class DepartmentGroup
    {
        public int? ID { get; set; }
        public string? CODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public int? COMPANY_ID { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public string? STATUS { get; set; }
    }
    public class DepartmentGroupResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public DepartmentGroup data { get; set; }
        public List<DepartmentGroup> datas { get; set; }
    }
    public class DepartmentGroupList
    {
        public int? COMPANY_ID { get; set; }
       
    }
}
