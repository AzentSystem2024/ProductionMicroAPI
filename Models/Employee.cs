namespace MicroApi.Models
{
    public class Employee
    {
        public long ID { get; set; }
            public string EMP_CODE { get; set; }
            public string EMP_NAME { get; set; }
            public DateTime? DOB { get; set; }
            public string ADDRESS1 { get; set; }
            public string ADDRESS2 { get; set; }
            public string ADDRESS3 { get; set; }
            public string CITY { get; set; }
            public int? STATE_ID { get; set; }
            public int? COUNTRY_ID { get; set; }
            public string MOBILE { get; set; }
            public string EMAIL { get; set; }
            public bool? IS_MALE { get; set; }
            public int? DEPT_ID { get; set; }
            public int? DESG_ID { get; set; }
            public DateTime? DOJ { get; set; }

            public string BANK_NAME { get; set; }
            public string BANK_CODE { get; set; }
            public string BANK_AC_NO { get; set; }

            public string PP_NO { get; set; }
            public DateTime? PP_EXPIRY { get; set; }

            public string VISA_NO { get; set; }
            public DateTime? VISA_EXPIRY { get; set; }

            public string LICENSE_NO { get; set; }
            public DateTime? LICENSE_EXPIRY { get; set; }

            public bool? IS_SALESMAN { get; set; }
            public string IMAGE_NAME { get; set; }
            public string WORK_PERMIT_NO { get; set; }
            public DateTime? WORK_PERMIT_EXPIRY { get; set; }

            public string IBAN_NO { get; set; }
            public string DAMAN_NO { get; set; }
            public string DAMAN_CATEGORY { get; set; }

            public string MOL_NUMBER { get; set; }
            public DateTime? LAST_REJOIN_DATE { get; set; }

            public string DEPT_NAME { get; set; }
            public string DESG_NAME { get; set; }
            public string STATE_NAME { get; set; }
            //public string? STORE_NAME { get; set; }

            public bool IS_INACTIVE { get; set; }
            public int? PAYMENT_TYPE { get; set; }

            public decimal LEAVE_DAY_BALANCE { get; set; }
            public decimal DAYS_DEDUCTED { get; set; }
            public bool IS_DELETED { get; set; }
            public decimal LEAVE_CREDIT { get; set; }


        //public List<EmployeeSalary> EmployeeSalary { get; set; }
        //public List<EmpAttachment> Attachment { get; set; }
    }
    public class EmpAttachment
    {
        public int ID { get; set; }
        public int DOC_TYPE { get; set; }
        public int DOC_ID { get; set; }
        public string FILE_NAME { get; set; }
        public byte[] FILE_DATA { get; set; }
        public string REMARKS { get; set; }
        public int USER_ID { get; set; }
        public DateTime CREATED_DATE_TIME { get; set; }
    }
    public class EmployeeSalary
    {
        public int ID { get; set; }
        public int EMP_ID { get; set; }
        public int HEAD_ID { get; set; }
        public string HEAD_NAME { get; set; }
        public float? AMOUNT { get; set; }
    }
    public class EmployeeResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Employee data { get; set; }
    }
    public class CompanyRequest
    {
        public int CompanyId { get; set; }
    }
}
