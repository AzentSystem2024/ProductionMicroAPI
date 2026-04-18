using System;
using System.Collections.Generic;

namespace MicroApi.Models
{
    public class SalesSummaryFilter
    {
        public int COMPANY_ID { get; set; }
        public string? ITEM_ID { get; set; }
        public string? BRAND_ID { get; set; }
        public string? CUSTOMER_ID { get; set; }
        public string? STATUS_ID { get; set; }
        public string? STORE_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
    }

    public class SalesSummaryItem
    {
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
        public string? ITEM_ID { get; set; }
        public string? CUSTOMER_ID { get; set; }
        public string? BRAND_ID { get; set; }
        public string? STATUS_ID { get; set; }
        public string? STORE_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
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
        public List<SalesDetailItem> data { get; set; } = new List<SalesDetailItem>();
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
}
