using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    

    public class EmployeeVacationLogListData
    {
        public int ID { get; set; }
        public object DOC_NO { get; set; }
        public object DATE { get; set; }
        public object DEPT_DATE { get; set; }
        public object REJOIN_DATE { get; set; }
        public object EXPECT_RETURN { get; set; }   
        public object EMP_ID { get; set; }
        public object EMP_NO { get; set; }
        public object EMP_NAME { get; set; }
        public object LEAVE_TYPE { get; set; }
        public object REMARKS { get; set; }
        public object STATUS { get; set; }
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
        public object ID { get; set; }
        public object USER_ID { get; set; }
        public object STORE_ID { get; set; }
        public object DOC_NO { get; set; }
        public object DOC_DATE { get; set; }
        public object EMP_ID { get; set; }
        public object EMP_NAME { get; set; }
        public object LEAVE_TYPE_ID { get; set; }
        public object LEAVE_TYPE_NAME { get; set; }
        public object LEAVE_CREDIT { get; set; }
        public object IS_TICKET { get; set; }
        public object LAST_REJOIN_DATE { get; set; }
        public object VAC_DAYS { get; set; }
        public object LS_PAYABLE { get; set; }
        public object DEPT_DATE { get; set; }
        public object EXPECT_RETURN { get; set; }
        public object TRAVELLED_DATE { get; set; }
        public object ACTUAL_DAYS { get; set; }
        public object DEDUCT_DAYS { get; set; }
        public object LEFT_REASON { get; set; }
        public object REJOIN_DATE { get; set; }
        public object REMARKS { get; set; }
        public object STATUS { get; set; }
    }


    //public class saveRevisionDataResponse
    //{
    //    public string flag { get; set; }
    //    public string message { get; set; }
    //}

}
