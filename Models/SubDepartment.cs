namespace MicroApi.Models
{
    public class SubDepartment
    {
        public int? ID { get; set; }
        public string? CODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string? DEPARTMENT_NAME { get; set; }
    }
    public class SubDepartmentResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public SubDepartment data { get; set; }
        public List<SubDepartment> datas { get; set; }
    }
   
}
