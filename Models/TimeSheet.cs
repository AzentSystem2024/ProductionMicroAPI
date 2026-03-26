using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    public class TimeSheetLogListData
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public object TS_MONTH { get; set; }
        public object EMP_NO { get; set; }
        public object EMP_NAME { get; set; }
        public object EMP_ID { get; set; }
        public object WORKED_DAYS { get; set; }
        public object OT_HOURS { get; set; }
        public object STATUS { get; set; }
    }

    public class TimeSheetLogListResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<TimeSheetLogListData> data { get; set; }
    }

    public class saveTimeSheetResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
    }

    public class saveTimeSheetData
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? EMP_CODE { get; set; }
        public string? EMP_NAME { get; set; }
        public string? STATUS { get; set; }
        public string TS_MONTH { get; set; }
        public int EMP_ID { get; set; }
        public float DAYS { get; set; }
        public float NORMAL_OT { get; set; }
        public float HOLIDAY_OT { get; set; }
        public string LEAVE_FROM { get; set; }
        public string LEAVE_TO { get; set; }
        public float WORKED_DAYS { get; set; }
        public float? DAYS_DEDUCTED { get; set; }
        public string REMARKS { get; set; }
        public List<saveTimeSheetDetailData>? TIMESHEET_DETAIL { get; set; }
        public List<saveTimeSheetSalaryData>? TIMESHEET_SALARY { get; set; }
    }

    public class saveTimeSheetDetailData
    {
        public int? ID { get; set; }
        public int? TS_ID { get; set; }
        public int? DEPT_ID { get; set; }
        public string? DEPT_NAME { get;set; }
        public float? DAYS { get; set; }
        public float? NORMAL_OT { get; set; }
        public float? HOLIDAY_OT { get; set; }
    }

    public class saveTimeSheetSalaryData
    {
        public int? ID { get; set; }
        public int? TS_ID { get; set; }
        public string? SALARY_HEAD_NAME { get; set; }
        public int? SALARY_HEAD_ID { get; set; }
        public float? AMOUNT { get; set; }
    }


    //public class saveRevisionDataResponse
    //{
    //    public string flag { get; set; }
    //    public string message { get; set; }
    //}
    public class TimeSheetHeader
    {
        public int ID { get; set; }
        public object EMP_NO { get; set; }
        public object EMP_NAME { get; set; }
        public object EMP_ID { get; set; }
        public object WORKED_DAYS { get; set; }
        public object LESS_HOURS { get; set; }
        public object OT_HOURS { get; set; }
        public object STATUS { get; set; }
        public object SALARY_ID { get; set; } 
       // public object AMOUNT { get; set; }

    }
    public class TimeSheetHeaderListResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<TimeSheetHeader> data { get; set; }
    }

    public class TimeSheetRequest
    {
        public int CompanyId { get; set; }
        public string Month { get; set; }
    }
    public class ApproveRequest
    {
        public List<int> IDs { get; set; }
    }
    public class TimeSheetRequestlist
    {
        public int COMPANY_ID { get; set; }
        public string MONTH { get; set; }
    }
    public class EmployeeVacationRequest
    {
        // public int EmployeeId { get; set; }
        // public int CompanyId { get; set; }
    }
    public class EmployeeVacationResponse
    {
        public DateTime? LEAVE_FROM { get; set; }
        public DateTime? LEAVE_TO { get; set; }
        public int TOTAL_DAYS { get; set; }
    }

    public class EmployeeVacationListResponseData
    {   
        public string flag { get; set; }
        public string message { get; set; }
        public List<EmployeeVacationResponse> data { get; set; }
    }

}
