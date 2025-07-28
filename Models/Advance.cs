using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{
    

    //public class GetSalaryHead
    //{
    //    public string CODE { get; set; }
    //    public string DESCRIPTION { get; set; }
    //    public int HEAD_ID { get; set; }
    //    public string PRESENT_AMOUNT { get; set; }
    //}

    //public class GetSalaryRevision
    //{
    //    public string EMP_CODE { get; set; }
    //    public string PREV_REVISION_DATE { get; set; }
    //    public List<GetSalaryHead> SALARY_HEAD { get; set; }
    //}

    //public class GetEmployeeDetailsResponse
    //{
    //    public string flag { get; set; }
    //    public string message { get; set; }
    //    public List<GetSalaryRevision> data { get; set; }
    //}

    //public class EmpDetail
    //{
    //    public int EMP_ID { get; set; }
    //    public int UserID { get; set; }
    //}

    public class AdvanceLogListData
    {
        public int ID { get; set; }
        public object ADV_NO { get; set; }
        public object DATE { get; set; }
        public object EMP_NO { get; set; }
        public object EMP_NAME { get; set; }
        public object ADV_TYPES { get; set; }
        public object AMOUNT { get; set; }
        public object REMARKS { get; set; }
        public object STATUS { get; set; }
    }

    public class AdvanceLogListResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<AdvanceLogListData> data { get; set; }
    }

    public class saveAdvanceResponseData
    {
        public string flag { get; set; }
        public string message { get; set; }
    }

    public class saveAdvanceData
    {
        public int ID { get; set; }

        public string? ADV_NO { get; set; }
        public int EMP_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public DateTime? DATE { get; set; }
        public int? ADV_TYPE_ID { get; set; }
        public string? ADV_TYPE_NAME { get; set; }
        public float? ADVANCE_AMOUNT { get; set; }
        public float? REC_AMOUNT { get; set; }
        public DateTime? REC_START_MONTH { get; set; }
        public int? REC_INSTALL_COUNT { get; set; }
        public float? REC_INSTALL_AMOUNT { get; set; }
        public decimal? RECOVERED_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
        public string? STATUS { get; set; }
        public int? PAY_TRANS_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
    }


    //public class saveRevisionDataResponse
    //{
    //    public string flag { get; set; }
    //    public string message { get; set; }
    //}

}
