namespace MicroApi.Models
{
    public class Sales_Invoice
    {
        public int TRANS_TYPE { get; set; }
        public int COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }

        // SALE HEADER FIELDS
        //public string? SALE_REF_NO { get; set; }
        public int? CUST_ID { get; set; }
        public int? FIN_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }


        // FOR TROUT SUMMARY UPDATE
        public List<SaleDetails> SALE_DETAILS { get; set; }
    }
    public class SaleDetails
    {
        public int? DELIVERY_NOTE_ID { get; set; }
        public double? QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
    }
    public class SalesInvoiceResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class Sale_InvoiceUpdate
    {
        public int? TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public string? TRANS_DATE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string? REF_NO { get; set; }
        public string? NARRATION { get; set; }
        public int? CREATE_USER_ID { get; set; }

        // SALE HEADER FIELDS
        //public string? SALE_REF_NO { get; set; }
        public int? CUST_ID { get; set; }
        public int? FIN_ID { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public float? TAX_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }

        // public List<SalesDetailUpdate> SALE_DETAILS { get; set; }
        public List<SaleDetails> SALE_DETAILS { get; set; }
    }
    //public class SalesDetailUpdate
    //{
    //    public int? DELIVERY_NOTE_ID { get; set; }
    //    public string? TRANSFER_NO { get; set; }
    //    public string? TRANSFER_DATE { get; set; }
    //    public string? ARTICLE { get; set; }
    //    public double? TOTAL_PAIR_QTY { get; set; }
    //    //public double? QUANTITY { get; set; }
    //    public double? PRICE { get; set; }
    //    public decimal? AMOUNT { get; set; }
    //    public decimal? GST { get; set; }
    //    public decimal? TAX_AMOUNT { get; set; }
    //    public decimal? TOTAL_AMOUNT { get; set; }
    //}
    //public class TransferOutSummaryItem
    //{
    //    public int ID { get; set; }
    //    public string TRANSFER_NO { get; set; }
    //    public string TRANSFER_DATE { get; set; }
    //    public string ARTICLE { get; set; }
    //    public double TOTAL_PAIR_QTY { get; set; }
    //}

    //public class TransferOutSummaryResponse
    //{
    //    public int flag { get; set; }
    //    public string Message { get; set; }
    //    public List<TransferOutSummaryItem> Data { get; set; }
    //}
    public class DeliveryNoteItem
    {
        public int ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string DN_DATE { get; set; }
        public double TOTAL_QTY { get; set; }
    }

    public class DeliveryNoteResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DeliveryNoteItem> Data { get; set; }
    }


    public class DeliveryGridResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DeliveryGridItem> Data { get; set; }
    }
    public class DeliveryGridItem
    {
        public int DELIVERY_NOTE_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DELIVERY_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public double QUANTITY { get; set; }

    }
    public class DeliveryInvoiceRequest
    {
        public int CUST_ID { get; set; }
    }
    public class SalesInvoiceHeader
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string SALE_NO { get; set; }
        public string SALE_DATE { get; set; }
        public int CUST_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public string CUST_NAME { get; set; }

    }
    public class SalesInvoiceHeaderResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalesInvoiceHeader> Data { get; set; }
    }
    public class SalesInvoiceHeaderSelect
    {
        public int TRANS_ID { get; set; }
        public int TRANS_TYPE { get; set; }
        public int? TRANS_STATUS { get; set; }
        public string SALE_NO { get; set; }
        public string SALE_DATE { get; set; }
        public string? NARRATION { get; set; }
        public int CUST_ID { get; set; }
        public float GROSS_AMOUNT { get; set; }
        public float NET_AMOUNT { get; set; }
        public float TAX_AMOUNT { get; set; }
        public string? REF_NO { get; set; }
        public List<SalesInvoiceDetailUpdate> SALE_DETAILS { get; set; }

    }
    public class SalesInvoiceDetailUpdate
    {
        public int? DELIVERY_NOTE_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DELIVERY_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public double QUANTITY { get; set; }
        public double? PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GST { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }

    }
    public class SalesInvselectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<SalesInvoiceHeaderSelect> Data { get; set; }
    }
    public class SalesInvoicesaveResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class SalesInvoiceLatestVocherNO
    {
        public string? VOCHERNO { get; set; }
        public int? TRANS_ID { get; set; }
    }
    public class SalesInvoiceLatestVocherNOResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SalesInvoiceLatestVocherNO> Data { get; set; }

    }
}
