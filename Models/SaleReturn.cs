namespace MicroApi.Models
{
    public class SaleReturn
    {
        public int? COMPANY_ID { get; set; }
        public int? CUST_ID { get; set; }
        public DateTime? RET_DATE { get; set; }
        public string? REF_NO { get; set; }
        public List<SaleReturnDetail>? DETAILS { get; set; }
    }
    public class SaleReturnDetail
    {
        public int? BOX_ID { get; set; }
    }
    public class SaleReturnResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
    public class SaleReturnList
    {
        public int ID { get; set; }
        public DateTime? RET_DATE { get; set; }
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public string? REF_NO { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? COMPANY_NAME { get; set; }
        public string? CUST_NAME { get; set; }
        public string? RET_NO { get; set; }
    }
    public class SaleReturnListRequest
    {
        public int COMPANY_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
    }
    public class SaleReturnListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SaleReturnList> Data { get; set; }
    }
    public class SaleInvoiceDetail
    {
        public int ID { get; set; }
        public string DOC_NO { get; set; }
        public DateTime SALE_DATE { get; set; }
        public int SALE_DET_ID { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public float PENDING_QTY { get; set; }
        public decimal PRICE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal TAXABLE_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TAX_PERC { get; set; }
        public float? QTY_STOCK { get; set; }
        public string BARCODE { get; set; }
        public string UOM { get; set; }
        public string UOM_PURCH { get; set; }
        public int UOM_MULTIPLE { get; set; }
        public string HSN_CODE { get; set; }
        public decimal GST_PERC { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }

    }
    public class SaleInvoiceDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SaleInvoiceDetail> Data { get; set; }
    }
    public class InvoieRequest
    {
        public int CUST_ID { get; set; }
        public int COMPANY_ID { get; set; }
    }
    public class SaleReturnDetailInsert
    {
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? SALE_DET_ID { get; set; }
        public int? ITEM_ID { get; set; }

        public float? PENDING_QTY { get; set; }
        public float? QUANTITY { get; set; }

        public decimal? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }

        public decimal? VAT_PERC { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }

        public string? UOM { get; set; }
        public string? UOM_PURCH { get; set; }
        public int? UOM_MULTIPLE { get; set; }

        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
    }
    public class SaleReturnInsertRequest
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? CUST_ID { get; set; }
        public DateTime? RET_DATE { get; set; }

        public string? REF_NO { get; set; }
        public string SALE_NO { get; set; }

        public int? SALE_ID { get; set; }
        public bool? IS_CREDIT { get; set; }

        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }

        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }

        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }
        public bool? IS_APPROVED { get; set; }
        public List<SaleReturnDetailInsert>? Details { get; set; }
    }
    public class SaleReturnViewResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public SaleReturnViewHeader Header { get; set; }
        public List<SaleReturnViewDetail> Details { get; set; }
    }

    public class SaleReturnViewHeader
    {
        public int? RET_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? RET_NO { get; set; }
        public DateTime? RET_DATE { get; set; }
        public int? CUST_ID { get; set; }
        public int? SALE_ID { get; set; }
        public string? SALE_NO { get; set; }
        public bool? IS_CREDIT { get; set; }
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public int? FIN_ID { get; set; }
        public long? TRANS_ID { get; set; }
        public string? VEHICLE_NO { get; set; }
        public bool? ROUND_OFF { get; set; }

        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public int? TRANS_STATUS { get; set; }

        public string? CUST_NAME { get; set; }
        public string? COMPANY_NAME { get; set; }
    }

    public class SaleReturnViewDetail
    {
        public int? DETAIL_ID { get; set; }
        public int? SALE_DET_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public decimal? DN_QTY { get; set; }
        public decimal? QUANTITY { get; set; }
        public decimal? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? VAT_PERC { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public string? UOM { get; set; }
        public string? UOM_PURCH { get; set; }
        public int? UOM_MULTIPLE { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }

        public string? DESCRIPTION { get; set; }
        public string? BARCODE { get; set; }
        public string? HSN_CODE { get; set; }
    }

}
