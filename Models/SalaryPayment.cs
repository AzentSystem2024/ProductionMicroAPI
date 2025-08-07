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
        public DateTime? CHEQUE_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? SUPP_ID { get; set; }

        public List<SalaryPayDetail> SALARY_PAY_DETAIL { get; set; } = new List<SalaryPayDetail>();
    }
    public class SalaryPayDetail
    {
        public int PAYDETAIL_ID { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }
    public class SalaryPaymentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
}
