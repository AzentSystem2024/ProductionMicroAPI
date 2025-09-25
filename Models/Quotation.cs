namespace MicroApi.Models
{
    public class Quotation
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? QTN_NO { get; set; }
        public string? QTN_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? SUBJECT { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public int? VALID_DAYS { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public string? DISCOUNT_DESCRIPTION { get; set; }
        public float? DISCOUNT_AMOUNT { get; set; }
        public bool? ROUND_OFF { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_ID { get; set; }
        public int? USER_ID { get; set; }
        //public List<string> Terms { get; set; }
        public List<QuotationTerm> Terms { get; set; }
        public string? NARRATION { get; set; }
        public List<QuotationDetail> Details { get; set; }
    }
    public class QuotationDetail
    {
        public int? ID { get; set; }
        public int? QTN_ID { get; set; }
        public int? ITEM_ID { get; set; }
        //public string? ITEM_CODE { get; set; }
       // public string? ITEM_NAME { get; set; }
        public string? UOM { get; set; }
        public float? QUANTITY { get; set; }
        public float? PRICE { get; set; }
        public float? DISC_PERCENT { get; set; }
        public float? AMOUNT { get; set; }
        public float? TAX_PERCENT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? TOTAL_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
    }
    public class QuotationSelect
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? QTN_NO { get; set; }
        public string? QTN_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public string? CUST_NAME { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? SUBJECT { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public int? VALID_DAYS { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public string? DISCOUNT_DESCRIPTION { get; set; }
        public string? PAY_TERM_NAME { get; set; }
        public string? DELIVERY_TERM_NAME { get; set; }
        public float? DISCOUNT_AMOUNT { get; set; }
        public bool? ROUND_OFF { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_ID { get; set; }
        public int? USER_ID { get; set; }
        public List<QuotationTerm> TERMS { get; set; } = new List<QuotationTerm>();
        public string? NARRATION { get; set; }
        public List<QuotationDetailSelect> Details { get; set; }

    }
    public class QuotationDetailSelect
    {
        public int? ID { get; set; }
        public int? QTN_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? ITEM_NAME { get; set; }
        public string? UOM { get; set; }
        public float? QUANTITY { get; set; }
        public float? PRICE { get; set; }
        public float? DISC_PERCENT { get; set; }
        public float? AMOUNT { get; set; }
        public float? TAX_PERCENT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? TOTAL_AMOUNT { get; set; }
        public string? REMARKS { get; set; }
    }
    public class QuotationTerm
    {
        public int ID { get; set; }
        public int QTN_ID { get; set; }
        public string TERMS { get; set; }
    }
    public class QuotationDetailSelectResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public QuotationSelect Data { get; set; }
    }
    public class QuotationResponse
    {
        public string Flag { get; set; }
        public string Message { get; set; }
    }

    public class QuotationDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public Quotation Data { get; set; }
    }

    public class QuotationList
    {
        public int? ID { get; set; }
        public string? QTN_DATE { get; set; }
        public string? QTN_NO { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public int? CUST_ID { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? SUBJECT { get; set; }
        public string? REF_NO { get; set; }
        public float? NET_AMOUNT { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? NARRATION { get; set; }
    }

    public class QuotationListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<QuotationList> Data { get; set; }
    }

    public class QuotationUpdate
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? QTN_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? SUBJECT { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public int? VALID_DAYS { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public string? DISCOUNT_DESCRIPTION { get; set; }
        public float? DISCOUNT_AMOUNT { get; set; }
        public bool? ROUND_OFF { get; set; }
        public int? TRANS_ID { get; set; }
        public float? NET_AMOUNT { get; set; }
        //public List<string> Terms { get; set; }
        public List<QuotationTerm> Terms { get; set; }
        public string? NARRATION { get; set; }
        public List<QuotationDetail> Details { get; set; }
    }

    public class QuotationRequest
    {
        public int STORE_ID { get; set; }
    }

    public class QuotationApproval
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? QTN_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? SALESMAN_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? SUBJECT { get; set; }
        public string? REF_NO { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? DELIVERY_TERM_ID { get; set; }
        public int? VALID_DAYS { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public float? CHARGE_AMOUNT { get; set; }
        public string? DISCOUNT_DESCRIPTION { get; set; }
        public float? DISCOUNT_AMOUNT { get; set; }
        public bool? ROUND_OFF { get; set; }
        public float? NET_AMOUNT { get; set; }
        public List<string> Terms { get; set; }
        public List<QuotationDetail> Details { get; set; }
    }

    public class Item
    {
        public int? ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string? MATRIX_CODE { get; set; }
        public string? UOM {  get; set; }
        public float? COST { get; set; }
        public float? STOCK_QTY { get; set; }
        public decimal? VAT_PERC { get; set; }
    }

    public class ItemListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<Item> Data { get; set; }
    }
    public class TERMSLIST
    {
        public int? ID { get; set; }
        public int? QTN_ID { get; set; }
        public string? TERMS { get; set; }
    }
    public class TERMSLISTResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<TERMSLIST> Data { get; set; }
    }
    public class QuotationHistory
    {
        public int ITEM_ID { get; set; }
        public string QTN_NO { get; set; }
        public DateTime QTN_DATE { get; set; }
        public string CUST_NAME { get; set; }
        public string REF_NO { get; set; }
        public float QUANTITY { get; set; }
        public string UOM { get; set; }
        public float UNIT_PRICE { get; set; }
        public float DISC_PERCENT { get; set; }
        public float AMOUNT { get; set; }
    }

    public class QuotationHistoryResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<QuotationHistory> Data { get; set; }
    }
}
