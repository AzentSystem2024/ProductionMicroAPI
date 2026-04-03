using System.Reflection.Metadata;

namespace MicroApi.Models
{
    public class getItems
    {
        public string ITEM_DESCRIPTION { get; set; }
        public string VAT_CODE { get; set; }
        public decimal VAT_PERC { get; set; }
    }

    public class getItemsResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<getItems> data { get; set; }
    }
    public class getItemsInput
    {
        public int? ITEM_ID { get; set; }
    }


    public class MiscSalesInvoiceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<MiscSalesInvoiceLookupData> data { get; set; }
    }

    public class MiscSalesInvoiceSave
    {
        public int? ID { get; set; }
        public string? INVOICE_NO { get; set; }
        public DateTime? INVOICE_DATE { get; set; }
        public string? REF_NO { get; set; }
        public DateTime? REF_DATE { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public int? TPA_ID { get; set; }
        public string? ENCOUNTER_TYPE { get; set; }
        public string? PATIENT_ID { get; set; }
        public string? PATIENT_NAME { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }  
        public bool? IS_APPROVED { get; set; }
        public int? TRANS_STATUS { get; set; }
        public List<MiscSalesInvoiceDetailData> DETAILS { get; set; }
    }

    public class MiscSalesInvoiceDetailData
    {
        public int? ITEM_ID { get; set; }
        public string? CLINICIAN { get; set; }
        public string? ITEM_DESCRIPTION { get; set; }
        public string? ORDERING_CLINICIAN { get; set; }
        public int? DEPARTMENT_ID { get; set; }
        public int? SUB_DEPARTMENT_ID { get; set; }
        public decimal? QUANTITY { get; set; }
        public decimal? DURATION { get; set; }
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? PATIENT_SHARE { get; set; }
        public string? VAT_CODE { get; set; }
        public decimal? VAT_PERC { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }
    }

    public class MiscSalesInvoiceLookupData
    {
        public int ID { get; set; }
        public string DOC_NO { get; set; }
        public int TRANS_STATUS { get; set; }
        public DateTime SALE_DATE { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public string CUST_NAME { get; set; }
        public string PATIENT_ID { get; set; }
        public string ENCOUNTER_TYPE { get; set; }
    }

}
