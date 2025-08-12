namespace MicroApi.Models
{
    public class SalaryPayment
    {
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? TRANS_ID { get; set; } 
        public int? TRANS_TYPE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public int? CREATE_USER_ID { get; set; }

        public List<SalaryPayDetail> SALARY_PAY_DETAIL { get; set; } = new List<SalaryPayDetail>();
    }
    public class SalaryPayDetail
    {
        public int PAYDETAIL_ID { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }
    public class SalaryPaymentUpdate
    {
        public int? TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? TRANS_TYPE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public int? CREATE_USER_ID { get; set; }

        public List<SalaryPayDetail> SALARY_PAY_DETAIL { get; set; } = new List<SalaryPayDetail>();
    }
    public class SalaryPaymentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class SalaryPaymentPending
    {
        public int ID { get; set; }
        public int EMP_ID { get; set; }
        public string EMP_NAME { get; set; }
        public string EMP_CODE { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }
    public class SalaryPendingRequest
    {
        public string SAL_MONTH { get; set; }
    }
    public class SalaryPendingResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<SalaryPaymentPending> data { get; set; }
    }
    public class SalaryPaymentListItem
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string TRANS_DATE { get; set; }
        public string VOUCHER_NO { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? PARTY_NAME { get; set; }
        public string NARRATION { get; set; }
        public int? PAY_TYPE_ID { get; set; }
        public int? PAY_HEAD_ID { get; set; }
        public int TRANS_STATUS { get; set; }

    }
    public class SalaryPaymentListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalaryPaymentListItem> Data { get; set; }
    }
    public class SalPayLastDocno
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int VOUCHER_NO { get; set; }
    }
    public class SalaryPaymentDetail
    {
        public int EMP_ID { get; set; }
        public string EMP_NAME { get; set; }
        public string EMP_CODE { get; set; }
        public decimal NET_AMOUNT { get; set; }

        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public string TRANS_DATE { get; set; }
        public string VOUCHER_NO { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public string PARTY_NAME { get; set; }
        public string NARRATION { get; set; }
        public int PAY_TYPE_ID { get; set; }
        public int PAY_HEAD_ID { get; set; }
        public int TRANS_STATUS { get; set; }
    }
    public class SalaryPaymentDetailResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalaryPaymentDetail> Data { get; set; } = new List<SalaryPaymentDetail>();
    }
}
