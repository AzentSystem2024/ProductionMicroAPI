using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    public class TimeSheetLogListData
    {
        public int ID { get; set; }
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
        public object ID { get; set; }
        public string TS_MONTH { get; set; }
        public object EMP_ID { get; set; }
        public object EMP_CODE { get; set; }
        public object EMP_NAME { get; set; }
        public object STATUS { get; set; }
        public object DAYS { get; set; }
        public object NORMAL_OT { get; set; }
        public object HOLIDAY_OT { get; set; }
        public string LEAVE_FROM { get; set; }
        public string LEAVE_TO { get; set; }
        public object WORKED_DAYS { get; set; }
        public object DAYS_DEDUCTED { get; set; }
        public object REMARKS { get; set; }
        public List<saveTimeSheetDetailData> TIMESHEET_DETAIL { get; set; }
        public List<saveTimeSheetSalaryData> TIMESHEET_SALARY { get; set; }
    }

    public class saveTimeSheetDetailData
    {
        public object ID { get; set; }
        public object TS_ID { get; set; }
        public object STORE_ID { get; set; }
        public object STORE_NAME { get;set; }
        public object DAYS { get; set; }
        public object NORMAL_OT { get; set; }
        public object HOLIDAY_OT { get; set; }
    }

    public class saveTimeSheetSalaryData
    {
        public object ID { get; set; }
        public object TS_ID { get; set; }
        public object SALARY_HEAD_NAME { get; set; }
        public object SALARY_HEAD_ID { get; set; }
        public object AMOUNT { get; set; }
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
        public object AMOUNT { get; set; }

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


}
