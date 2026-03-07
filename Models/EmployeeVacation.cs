using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    

    public class EmployeeVacationLogListData
    {
        public int? ID { get; set; }
        public string? DOC_NO { get; set; }
        public DateTime? DATE { get; set; }
        public DateTime? DEPT_DATE { get; set; }
        public DateTime? REJOIN_DATE { get; set; }
        public DateTime? EXPECT_RETURN { get; set; }   
        public int? EMP_ID { get; set; }
        public string? EMP_NO { get; set; }
        public string? EMP_NAME { get; set; }
        public string? LEAVE_TYPE { get; set; }
        public string? REMARKS { get; set; }
        public string? STATUS { get; set; }
    }

    public class EmployeeVacationLogListResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<EmployeeVacationLogListData> data { get; set; }
    }

    public class saveEmployeeVacationResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
    }

    public class saveEmployeeVacationData
    {
        public int? ID { get; set; }
        public int? USER_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? DOC_NO { get; set; }
        public DateTime? DOC_DATE { get; set; }
        public int? EMP_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public int? LEAVE_TYPE_ID { get; set; }
        public string? LEAVE_TYPE_NAME { get; set; }
        public int? LEAVE_CREDIT { get; set; }
        public bool? IS_TICKET { get; set; }
        public DateTime? LAST_REJOIN_DATE { get; set; }
        public int? VAC_DAYS { get; set; }
        public bool? LS_PAYABLE { get; set; }
        public DateTime? DEPT_DATE { get; set; }
        public DateTime? EXPECT_RETURN { get; set; }
        public DateTime? TRAVELLED_DATE { get; set; }
        public int? ACTUAL_DAYS { get; set; }
        public int? DEDUCT_DAYS { get; set; }
        public int? LEFT_REASON { get; set; }
        public DateTime? REJOIN_DATE { get; set; }
        public string? REMARKS { get; set; }
        public string? STATUS { get; set; }
    }


    //public class saveRevisionDataResponse
    //{
    //    public string flag { get; set; }
    //    public string message { get; set; }
    //}
    public class EmployeeLeaveCreditData
    {
        public int? EMP_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public string? EMP_CODE { get; set; }
        public int? LEAVE_CREDIT { get; set; }
        public int? LEAVE_DAY_BALANCE { get; set; }
        public DateTime? LAST_REJOIN_DATE { get; set; }
    }

    public class EmployeeLeaveCreditResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<EmployeeLeaveCreditData> data { get; set; }
    }

    public class EmployeeLeaveCreditRequest
    {
        public int? EMP_ID { get; set; }
    }

}
