namespace MicroApi.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DEPT_NAME { get; set; }
        public string? COMPANY_NAME { get; set; }
        public int COMPANY_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETED { get; set; }
        public int? COST_BUCKET_ID { get; set; }
        public string? COST_BUCKET_NAME { get; set; }
    }
    public class DepartmentResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Department data { get; set; }
        public List<Department> datas { get; set; }
    }
    public class Departmentlist
    {
        public int COMPANY_ID { get; set; }
       
    }
}
