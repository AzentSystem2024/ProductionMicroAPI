namespace MicroApi.Models
{
    public class Department
    {
        //public int ID { get; set; }
        public string DEPARTMENT { get; set; } 
        public int HOSPITAL_ID { get; set; }
        public string HOSPITAL_NAME { get; set; }
        public string BILL_PREFIX { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class DepartmentUpdate
    {
        public int ID { get; set; }
        public string DEPARTMENT { get; set; }
        public int HOSPITAL_ID { get; set; }
        public string HOSPITAL_NAME { get; set; }
        public string BILL_PREFIX { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class DepartmentListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<DepartmentUpdate> Data { get; set; }
    }
    public class DepartmentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public DepartmentUpdate Data { get; set; }

    }
}
