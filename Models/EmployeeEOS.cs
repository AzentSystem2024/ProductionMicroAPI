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
        public object? ID { get; set; }
        public object? USER_ID { get; set; }
        public object? STORE_ID { get; set; }
        public object DOC_NO { get; set; }
        public object EMP_ID { get; set; }
        public object EMP_NAME { get; set; }
        public object EOS_DATE { get; set; }
        public object REASON_ID { get; set; }
        public object REASON_NAME { get; set; }
        public object DAYS { get; set; }
        public object EOS_AMOUNT { get; set; }
        public object LEAVE_AMOUNT { get; set; }
        public object PENDING_SALARY { get; set; }
        public object ADD_AMOUNT { get; set; }
        public object DED_AMOUNT { get; set; }
        public object ADD_REMARKS { get; set; }
        public object DED_REMARKS { get; set; }

        public object REMARKS { get; set; }
        public object STATUS { get; set; }
        public object TRANS_ID { get; set; }
        public object PAY_TRANS_ID { get; set; }

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
