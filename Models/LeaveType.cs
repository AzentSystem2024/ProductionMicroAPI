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

}
