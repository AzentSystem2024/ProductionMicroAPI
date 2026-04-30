namespace MicroApi.Models
{
    public class SalesPOS
    {
        public SalesPOSHeader Header { get; set; }
        public List<SalesPOSDetail> Details { get; set; }
        public List<SalesPOSTender> Tenders { get; set; }
    }

    public class SalesPOSHeader
    {
        public int SALE_ID { get; set; }
        public int TRANS_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public DateTime SALE_DATE { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string CUST_NAME { get; set; }
        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public int SALESMAN_ID { get; set; }
        public string EMP_NAME { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public decimal DISCOUNT_AMOUNT { get; set; }
    }

    public class SalesPOSDetail
    {
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal AMOUNT_INCL_VAT { get; set; }
        public decimal VAT_PERCENT { get; set; }
        public decimal VAT_AMOUNT { get; set; }
    }

    public class SalesPOSTender
    {
        public int TENDER_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
