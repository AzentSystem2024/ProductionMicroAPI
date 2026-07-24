using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{

    public class EOSPaymentRequest
    {
        public int EmployeeId { get; set; }
    }
    public class EOSPaymentResponse
    {
        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public string Reason { get; set; }

        public string DocNo { get; set; }

        public DateTime? JoinDate { get; set; }

        public DateTime? LastWorkingDay { get; set; }

        public int Days { get; set; }

        public int TotalServiceDays { get; set; }

        public decimal UnPaidLeave { get; set; }

        public decimal PendingSalary { get; set; }

        public decimal EOSAmount { get; set; }

        public decimal UnPaidLeaveSalary { get; set; }

        public decimal Additions { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetAmount { get; set; }

        public string Remarks { get; set; }
    }
 
        
        public class EmployeeEOSPaymentLogListData
        {
            public int ID { get; set; }
            public object DOC_NO { get; set; }
            public object DOC_DATE { get; set; }
            public object EMP_NO { get; set; }
            public object EMP_ID { get; set; }
            public object EMP_NAME { get; set; }
            public object EOS_DOC_NO { get; set; }
            public object EOS_AMOUNT { get; set; }
            public object NET_AMOUNT { get; set; }
            public object STATUS { get; set; }
        }

    public class EmployeeEOSPaymentListResponseData
    {
            public string flag { get; set; }
            public string message { get; set; }
        //public List<EmployeeEOSPaymentLogListData> data { get; set; }
        
            public List<EmployeeEOSPaymentData> data { get; set; }
    }

    public class EmployeeEOSPaymentData
    {
        public int ID { get; set; }
        public string DOC_NO { get; set; }
        public DateTime? DOC_DATE { get; set; }
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public decimal EOS_AMOUNT { get; set; }
        public decimal PENDING_SALARY { get; set; }
        public decimal LEAVE_AMOUNT { get; set; }
        public decimal ADD_AMOUNT { get; set; }
        public decimal DED_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public string REMARKS { get; set; }
        public string STATUS { get; set; }
    }

    public class SaveEmployeeEOSPaymentData
        {
            public int? ID { get; set; }
            public int? USER_ID { get; set; }
            public int? STORE_ID { get; set; }
        public int? DOC_STATUS { get; set; }
        public string? DOC_NO { get; set; }
            public DateTime? DOC_DATE { get; set; }

            public int? EOS_ID { get; set; }

            public int? EMP_ID { get; set; }
       
        public int REASON_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string? EMP_NO { get; set; }
            public string? EMP_NAME { get; set; }

            public string? EOS_DOC_NO { get; set; }

            public decimal? EOS_AMOUNT { get; set; }
            public decimal? PENDING_SALARY { get; set; }
            public decimal? LEAVE_AMOUNT { get; set; }

            public decimal? ADD_AMOUNT { get; set; }
            public decimal? DED_AMOUNT { get; set; }

            public string? ADD_REMARKS { get; set; }
            public string? DED_REMARKS { get; set; }

            public decimal? NET_AMOUNT { get; set; }

            public string? REMARKS { get; set; }

            public string? STATUS { get; set; }

            public int? TRANS_ID { get; set; }
            public int? FIN_ID { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? PAYMENT_ACCOUNT_ID { get; set; }
        
             public string? VOUCHER_NO { get; set; }
        public string? PAYMENT_MODE { get; set; }
        
            public int? PAY_HEAD_ID { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        
            public string? CHEQUE_NO { get; set; }
            public DateTime? CHEQUE_DATE { get; set; }
        }

        public class SaveEmployeeEOSPaymentResponseData
        {
            public string flag { get; set; }
            public string message { get; set; }
        }

    


       

        //public class EOSPaymentRequest
        //{
        //    public int EmployeeId { get; set; }
        //}

        //public class EOSPaymentResponse
        //{
        //    public string EmployeeCode { get; set; }
        //    public string EmployeeName { get; set; }
        //    public string Reason { get; set; }
        //    public string DocNo { get; set; }

        //    public DateTime? JoinDate { get; set; }
        //    public DateTime? LastWorkingDay { get; set; }

        //    public int Days { get; set; }
        //    public int TotalServiceDays { get; set; }

        //    public decimal UnPaidLeave { get; set; }
        //    public decimal PendingSalary { get; set; }
        //    public decimal EOSAmount { get; set; }
        //    public decimal UnPaidLeaveSalary { get; set; }

        //    public decimal Additions { get; set; }
        //    public decimal Deductions { get; set; }

        //    public decimal NetAmount { get; set; }

        //    public string Remarks { get; set; }
        //}

    

}
 