using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    

   
    public class LeaveTypeLogListData
    {
        public int ID { get; set; }
        public object CODE { get; set; }
        public object DESCRIPTION { get; set; }
        public object LEAVE_SALARY_PAYABLE { get; set; }
        public object STATUS { get; set; }
    }

    public class LeaveTypeLogListResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<LeaveTypeLogListData> data { get; set; }
    }

    public class saveLeaveTypeResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
    }

    public class saveLeaveTypeData
    {
        public int? ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool LEAVE_SALARY_PAYABLE { get; set; }
        public bool? IS_INACTIVE { get; set; }
    }
    public class LeaveListRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DepartmentId { get; set; }   //0 = All
        public int EmployeeId { get; set; }     //0 = All
    }

    public class LeaveListResponse
    {
        public string LeaveCode { get; set; }
        public string ApplicationNo { get; set; }
        public string EmployeeCode { get; set; }
        public string DepartmentCode { get; set; }
        public string EmployeeName { get; set; }
        public decimal LeaveBalance { get; set; }
        public DateTime? LeaveStartDate { get; set; }
        public DateTime? LeaveEndDate { get; set; }
        public decimal ApprovedDays { get; set; }
        public string Approved { get; set; }
        public DateTime? ResumedDate { get; set; }
    }

}
