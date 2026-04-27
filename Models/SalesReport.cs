using System;
using System.Collections.Generic;

namespace MicroApi.Models
{
    public class SalesSummaryFilter
    {
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public string STORE_ID { get; set; } = "";
        public string CUST_ID { get; set; } = "";
        public int SALE_TYPE { get; set; }
        public int INCLUDE_SUMMARY { get; set; }
    }

    public class SalesSummaryItem
    {

        public int TRANS_ID { get; set; }
        public int SALE_ID { get; set; }
        public DateTime SALE_DATE { get; set; }
        public string INVOICE_NO { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }

    public class SalesSummaryResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<SalesSummaryItem> data { get; set; } = new List<SalesSummaryItem>();
    }

    public class SalesDetailFilter
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }

        public string? ITEM_ID { get; set; }
        public string? CUSTOMER_ID { get; set; }
        public string? BRAND_ID { get; set; }
        public string? STATUS_ID { get; set; }
        public string? STORE_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }

        public int SALE_TYPE { get; set; }
        public string? SALESMAN_ID { get; set; }
        public string? DEPT_ID { get; set; }
        public string? CAT_ID { get; set; }
        public string? SUBCAT_ID { get; set; }
        public int DISCOUNTED_ITEMS_ONLY { get; set; }
        public string? CUSTOM1 { get; set; }
        public string? CUSTOM2 { get; set; }
        public int INCLUDE_SUMMARY { get; set; }
    }

    public class SalesDetailItem
    {
        public int SALE_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }

    public class SalesDetailResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        //public List<SalesDetailItem> data { get; set; } = new List<SalesDetailItem>();
        public List<SalesDetailModel> Data { get; set; } = new List<SalesDetailModel>();
    }

    public class ConsignmentSummaryFilter
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int? BRAND_ID { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public string? STATUS_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
    }
    public class SalesDetailModel
    {
        public string InvoiceNo { get; set; }
        public DateTime? Date { get; set; }
        public string Store { get; set; }
        public string InvoiceType { get; set; }
        public string Customer { get; set; }
        public string WalkInCustomer { get; set; }

        public string ItemCode { get; set; }
        public string Description { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public string Reason { get; set; }

        public decimal ExVatTotal { get; set; }
        public decimal VatPercent { get; set; }
        public decimal VatAmount { get; set; }
        public decimal IncVatTotal { get; set; }

        public string Salesman { get; set; }
        public decimal Commission { get; set; }
    }
    public class ConsignmentSummaryItem
    {
        public int CONSIGNMENT_ID { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
    }

    public class ConsignmentSummaryResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ConsignmentSummaryItem> data { get; set; } = new List<ConsignmentSummaryItem>();
    }

    public class ConsignmentReturnDetailFilter
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public int? BRAND_ID { get; set; }
        public string? STATUS_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
    }

    public class ConsignmentReturnDetailItem
    {
        public int RETURN_ID { get; set; }
        public decimal AMOUNT { get; set; }
    }

    public class ConsignmentReturnDetailResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ConsignmentReturnDetailItem> data { get; set; } = new List<ConsignmentReturnDetailItem>();
    }
    public class ItemWiseSalesFilter
    {
        public int COMPANY_ID { get; set; }   // ✅ NEW
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }

        public string STORE_ID { get; set; } = "";
        public int SALE_TYPE { get; set; } = 0;

        public string CUST_ID { get; set; } = "";
        public string SALESMAN_ID { get; set; } = "";
        public string DEPT_ID { get; set; } = "";
        public string CAT_ID { get; set; } = "";
        public string SUBCAT_ID { get; set; } = "";
        public string BRAND_ID { get; set; } = "";
        public string CUSTOM1 { get; set; } = "";
        public string CUSTOM2 { get; set; } = "";
        public string ITEM_ID { get; set; } = "";

        public int DISCOUNTED_ITEMS_ONLY { get; set; } = 0;
        public int INCLUDE_SUMMARY { get; set; } = 0;
    }
    public class ItemWiseSalesItem
    {
        public int ID { get; set; }
        public string SALE_NO { get; set; }
        public DateTime SALE_DATE { get; set; }
        public string STORE_NAME { get; set; }
        public string SALE_TYPE_NAME { get; set; }
        public string CUST_NAME { get; set; }
        public string COMMENT { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public decimal DISCOUNT { get; set; }
        public string DISC_REASON { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal VAT_PERCENT { get; set; }
        public decimal VAT_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public string SALESMAN { get; set; }
        public decimal COMMISSION { get; set; }
    }
    public class ItemWiseSalesResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ItemWiseSalesItem> data { get; set; } = new List<ItemWiseSalesItem>();
    }
    public class ItemWiseSalesSummaryFilter
    {
        public int COMPANY_ID { get; set; }   // ✅ NEW
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }

        public string STORE_ID { get; set; } = "";
        public int SALE_TYPE { get; set; } = 0;

        public string CUST_ID { get; set; } = "";
        public string SALESMAN_ID { get; set; } = "";
        public string DEPT_ID { get; set; } = "";
        public string CAT_ID { get; set; } = "";
        public string SUBCAT_ID { get; set; } = "";
        public string BRAND_ID { get; set; } = "";
        public string CUSTOM1 { get; set; } = "";
        public string CUSTOM2 { get; set; } = "";
        public string ITEM_ID { get; set; } = "";

        public int DISCOUNTED_ITEMS_ONLY { get; set; } = 0;
        public int INCLUDE_SUMMARY { get; set; } = 0;

        public int GROUP_BY_STORE { get; set; } = 0;   // 🔥 NEW PARAM
    }
    public class ItemWiseSalesSummaryItem
    {
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }

        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }

        public decimal QUANTITY { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal VAT_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
    }
    public class ItemWiseSalesSummaryResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ItemWiseSalesSummaryItem> data { get; set; } = new List<ItemWiseSalesSummaryItem>();
    }
    public class DiscountWiseSalesFilter
    {
        public int COMPANY_ID { get; set; }   // ✅ NEW
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }

        public string STORE_ID { get; set; } = "";
        public int SALE_TYPE { get; set; } = 0;

        public string CUST_ID { get; set; } = "";
        public string SALESMAN_ID { get; set; } = "";
        public string DEPT_ID { get; set; } = "";
        public string CAT_ID { get; set; } = "";
        public string SUBCAT_ID { get; set; } = "";
        public string BRAND_ID { get; set; } = "";
        public string CUSTOM1 { get; set; } = "";
        public string CUSTOM2 { get; set; } = "";
        public string ITEM_ID { get; set; } = "";

        public string REASON_ID { get; set; } = "";   // 🔥 NEW
        public int INCLUDE_SUMMARY { get; set; } = 0;
    }
    public class DiscountWiseSalesItem
    {
        public int ID { get; set; }
        public string SALE_NO { get; set; }
        public DateTime SALE_DATE { get; set; }
        public string STORE_NAME { get; set; }
        public string SALE_TYPE_NAME { get; set; }
        public string CUST_NAME { get; set; }
        public string COMMENT { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public decimal DISCOUNT { get; set; }
        public string DISC_REASON { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal VAT_PERCENT { get; set; }
        public decimal VAT_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public string SALESMAN { get; set; }
        public decimal COMMISSION { get; set; }
    }
    public class DiscountWiseSalesResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<DiscountWiseSalesItem> data { get; set; } = new List<DiscountWiseSalesItem>();
    }
    public class TenderReportFilter
    {
        public int COMPANY_ID { get; set; }   // ✅ NEW
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public string? STORE_ID { get; set; }
        public int SALE_TYPE { get; set; }
        public string? CUSTOMER_ID { get; set; }
    }

    public class TenderReportItem
    {
        public int TRANS_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public DateTime DATE { get; set; }
        public string STORE { get; set; }
        public string INVOICE_TYPE { get; set; }

        // Dynamic columns (Cash, Card, UPI, TOTAL etc.)
        public Dictionary<string, decimal> TenderAmounts { get; set; } = new();
    }

    public class TenderReportResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<TenderReportItem> data { get; set; } = new();
    }

    ///
    public class TenderSummaryFilter
    {
        public int COMPANY_ID { get; set; }   // ✅ NEW
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public string? STORE_ID { get; set; }
        public int SALE_TYPE { get; set; }
        public string? CUSTOMER_ID { get; set; }
    }

    public class TenderSummaryItem
    {
        public int TRANS_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public DateTime DATE { get; set; }
        public string STORE { get; set; }
        public string INVOICE_TYPE { get; set; }

        // Dynamic columns (Cash, Card, UPI, TOTAL etc.)
        public Dictionary<string, decimal> TenderAmounts { get; set; } = new();
    }

    public class TenderSummaryResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<TenderSummaryItem> data { get; set; } = new();
    }
}
