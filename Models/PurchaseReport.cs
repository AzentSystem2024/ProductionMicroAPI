using System;
using System.Collections.Generic;
namespace MicroApi.Models
{
    public class PurchaseReport
    {
            public int COMPANY_ID { get; set; }
            public int FIN_ID { get; set; }
            public DateTime DATE_FROM { get; set; }
            public DateTime DATE_TO { get; set; }
            public string STORE_ID { get; set; }
            public string SUPP_ID { get; set; }
        
    }
    public class PurchaseSummaryRpt
    {
        public int ID { get; set; }
        public long TRANS_ID { get; set; }
        public int STORE_ID { get; set; }
        public string PURCHASE_NO { get; set; }
        public DateTime DATE { get; set; }
        public string STORE { get; set; }
        public int SUPP_ID { get; set; }
        public string SUPPLIER { get; set; }
        public string INVOICE_NO { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal EX_VAT_TOTAL { get; set; }
        public decimal VAT_AMOUNT { get; set; }
        public decimal INC_VAT_TOTAL { get; set; }
    }

    public class PurchaseSummaryResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<PurchaseSummaryRpt> data { get; set; }
    }
    public class ItemWisePurchaseReportRequest
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }

        public string STORE_ID { get; set; }
        public string SUPP_ID { get; set; }
        public string DEPT_ID { get; set; }
        public string CAT_ID { get; set; }
        public string SUBCAT_ID { get; set; }
        public string BRAND_ID { get; set; }
        public string CUSTOM1 { get; set; }
        public string CUSTOM2 { get; set; }
        public string ITEM_ID { get; set; }
    }
    public class ItemWisePurchaseRpt
    {
        public int ID { get; set; }
        public string PURCH_NO { get; set; }
        public DateTime PURCH_DATE { get; set; }

        public string STORE_NAME { get; set; }

        public string SUPP_NAME { get; set; }

        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }

        public decimal QUANTITY { get; set; }
        public decimal RATE { get; set; }

        public decimal DISCOUNT { get; set; }
        public decimal GROSS_AMOUNT { get; set; }

        public decimal VAT_PERCENT { get; set; }
        public decimal VAT_AMOUNT { get; set; }

        public decimal NET_AMOUNT { get; set; }

        public long TRANS_ID { get; set; }

        public string DEPT_NAME { get; set; }
        public string CAT_NAME { get; set; }
        public string SUBCAT_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public string CUSTOM1_NAME { get; set; }
        public string CUSTOM2_NAME { get; set; }
    }

    public class ItemWisePurchaseResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ItemWisePurchaseRpt> data { get; set; }
    }
}
