using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    


    public class EmployeeEOSLogListData
    {
        public int ID { get; set; }
        public object DOC_NO { get; set; }
        public object DATE { get; set; }
        public object EMP_NO { get; set; }
        public object EMP_ID { get; set; }
        public object EMP_NAME { get; set; }
        public object REASON { get; set; }
        public object REMARKS { get; set; }
        public object STATUS { get; set; }
    }

    public class EmployeeEOSLogListResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<EmployeeEOSLogListData> data { get; set; }
    }

    public class saveEmployeeEOSResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
    }

    public class saveEmployeeEOSData
    {
        public int? ID { get; set; }
        public int? USER_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? DOC_NO { get; set; }
        public int? EMP_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public DateTime? EOS_DATE { get; set; }
        public int? REASON_ID { get; set; }
        public string? REASON_NAME { get; set; }
        public string? DAYS { get; set; }
        public string? EOS_AMOUNT { get; set; }
        public string? LEAVE_AMOUNT { get; set; }
        public string? PENDING_SALARY { get; set; }
        public string? ADD_AMOUNT { get; set; }
        public string? DED_AMOUNT { get; set; }
        public string? ADD_REMARKS { get; set; }
        public string? DED_REMARKS { get; set; }

        public string? REMARKS { get; set; }
        public string? STATUS { get; set; }
        public int? TRANS_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? PAY_TRANS_ID { get; set; }
        public DateTime? RELIEVING_DATE { get; set; }

    }

    public class EOSEmployeeData
    {
        public object JOIN_DATE { get; set; }
        public object LESS_SERVICE_DAYS { get; set; }
    }
    public class EOSEmployeeInput
    {
        public object EMP_ID { get; set; }
    }


    //public class saveRevisionDataResponse
    //{
    //    public string flag { get; set; }
    //    public string message { get; set; }
    //}

}
