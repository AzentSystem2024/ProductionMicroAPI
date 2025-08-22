namespace MicroApi.Models
{
    public class PDCModel
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? BANK_HEAD_ID { get; set; }
        public int? CUST_ID { get; set; }
        public int? SUPP_ID { get; set; }
        public string BENEFICIARY_NAME { get; set; }
        public int? BENEFICIARY_TYPE { get; set; }
        //public string ENTRY_NO { get; set; }
        public string? ENTRY_DATE { get; set; }
        public string? CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public decimal? AMOUNT { get; set; }
        public string? REMARKS { get; set; }
        public bool? IS_PAYMENT { get; set; }
        public int? ENTRY_STATUS { get; set; }
        public int? AC_TRANS_ID { get; set; }
    }
     public class PDCModelSelect
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int? BANK_HEAD_ID { get; set; }
        public int? CUST_ID { get; set; }
        public int? SUPP_ID { get; set; }
        public string BENEFICIARY_NAME { get; set; }
        public int? BENEFICIARY_TYPE { get; set; }
        public string ENTRY_NO { get; set; }
        public string? ENTRY_DATE { get; set; }
        public string CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public decimal? AMOUNT { get; set; }
        public string REMARKS { get; set; }
        public bool? IS_PAYMENT { get; set; }
        public string? ENTRY_STATUS { get; set; }
        public int? AC_TRANS_ID { get; set; }
    }

    public class PDCList
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string ENTRY_NO { get; set; }
        public string? ENTRY_DATE { get; set; }
        public string BENEFICIARY_NAME { get; set; }
        public int? BENEFICIARY_TYPE { get; set; }
        public string CHEQUE_NO { get; set; }
        public string? CHEQUE_DATE { get; set; }
        public decimal? AMOUNT { get; set; }
        public string REMARKS { get; set; }
        public bool? IS_PAYMENT { get; set; }
        public string ENTRY_STATUS { get; set; }
    }
    public class PDCResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PDCList> Data { get; set; }
    }
    public class PDCSaveResponse
    {
        public string Flag { get; set; }
        public string Message { get; set; }
    }
    public class PDCSelectResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PDCModelSelect> Data { get; set; }
    }
}

