namespace MicroApi.Models
{
    public class MiscSales
    {
        public long? TRANS_ID { get; set; }   
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? FIN_ID { get; set; }

        public DateTime? TRANS_DATE { get; set; }

        public string? REF_NO { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public string? PARTY_NAME { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public int? VERIFY_USER_ID { get; set; }
        public int? APPROVE1_USER_ID { get; set; }
        public int? APPROVE2_USER_ID { get; set; }
        public int? APPROVE3_USER_ID { get; set; }
        public int? CREATED_STORE_ID { get; set; } = 1;
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }

        public bool? IS_APPROVED { get; set; }   

        public List<MiscSaleDetail> DETAILS { get; set; }
    }
    public class MiscSaleDetail
    {
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }

        public decimal? AMOUNT { get; set; }       
        public int? TAX_PERC { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? HEAD_ID { get; set; }
        public string? REMARKS { get; set; }
    }
    public class MiscSalesResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public MiscSales? Data { get; set; }
        public List<MiscSaleList>? List { get; set; }
    }
    public class MiscSaleList
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int TRANS_STATUS { get; set; }

        public string SALE_NO { get; set; }
        public DateTime TRANS_DATE { get; set; }

        public int CUSTOMER_ID { get; set; }
        public string CUST_NAME { get; set; }

        public decimal GROSS_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }

        public string REF_NO { get; set; }
    }
    public class MiscSalesListRequest
    {
        public int COMPANY_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
    }
    public class MiscSalesView
    {
        public int TRANS_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int TRANS_TYPE { get; set; }

        public string SALE_NO { get; set; }
        public string TRANS_DATE { get; set; }

        public int CUSTOMER_ID { get; set; }
        public string CUST_NAME { get; set; }
        public string NARRATION { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }

        public string REF_NO { get; set; }
        public string PARTY_NAME { get; set; }

        public List<MiscSalesItem> Details { get; set; }
    }

    public class MiscSalesItem
    {
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public int TAX_PERC { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public int HEAD_ID { get; set; }
        public string? REMARKS { get; set; }
    }

    public class MiscSalesViewResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public MiscSalesView Data { get; set; }
    }

}
