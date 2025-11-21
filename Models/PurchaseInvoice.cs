namespace MicroApi.Models
{
    public class PurchaseInvoice
    {
        public int ID { get; set; }
        public string PURCH_NO { get; set; }
        public DateTime? PURCH_DATE { get; set; }
        public int SUPP_ID { get; set; }
        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public string SUPPPLIER_NAME { get; set; }
        public float NET_AMOUNT { get; set; }
        public string NARRATION { get; set; }
        public string STATUS { get; set; }
        public string PO_NO { get; set; }
        public long? TRANS_ID { get; set; }
        
    }
    public class PIDropdownInput
    {
        public int? SUPP_ID { get; set; }
    }
    public class PIDropdownResponce
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PIDropdownData> data { get; set; }
    }
    public class PIDropdownData
    {
        public string PO_NO { get; set; }
        public DateTime PO_DATE { get; set; }
        public string SUPP_NAME { get; set; }
        public int PO_ID { get; set; }
        public int SUPP_ID { get; set; }
    }
    public class GRNDetailInput
    {
        public int PO_ID { get; set; }
    }
    public class GRNDetailResponce
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<GRNDetails> GRNDetails { get; set; }
    }
    public class GRNDetails
    {
        public int ID { get; set; }
        public int GRN_NO { get; set; }
        public int ITEM_ID { get; set; }
        public float PENDING_QTY { get; set; }
        public string UOM { get; set; }
        public float DISC_PERCENT { get; set; }
        public decimal TAX_PERCENT { get; set; }
        public float PRICE { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }

        // ------------ Additional Properties from JSON ---------------------
        public int COMPANY_ID { get; set; }
        public int SUPP_ID { get; set; }
        public int STORE_ID { get; set; }
        public int PURCH_ID { get; set; }
        public int GRN_DET_ID { get; set; }
        public string PACKING { get; set; }
        public float QUANTITY { get; set; }
        public float RATE { get; set; }
        public float AMOUNT { get; set; }
        public float RETURN_QTY { get; set; }
        public string ITEM_DESC { get; set; }
        public long PO_DET_ID { get; set; }
        public float COST { get; set; }
        public float SUPP_PRICE { get; set; }
        public float SUPP_AMOUNT { get; set; }
        public decimal VAT_PERC { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public int GRN_STORE_ID { get; set; }
        public float RETURN_AMOUNT { get; set; }
    }

    public class PurchHeader
    {
        public int? ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int USER_ID { get; set; }
        public string? PURCH_NO { get; set; }
        public string? SUPPPLIER_NAME { get; set; }
        public string? NARRATION { get; set; }
        public DateTime? PURCH_DATE { get; set; }
        public int? SUPP_ID { get; set; }
        public string? SUPP_INV_NO { get; set; }
        public DateTime? SUPP_INV_DATE { get; set; }
        public int? FIN_ID { get; set; }
        public long? TRANS_ID { get; set; }
        public short? PURCH_TYPE { get; set; }
        public float? DISCOUNT_AMOUNT { get; set; }
        public float? SUPP_GROSS_AMOUNT { get; set; }
        public float? SUPP_NET_AMOUNT { get; set; }
        public float? GROSS_AMOUNT { get; set; }
        public string? CHARGE_DESCRIPTION { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public float? NET_AMOUNT { get; set; }
        public decimal? RETURN_AMOUNT { get; set; }
        public float? ADJ_AMOUNT { get; set; }
        public float PAID_AMOUNT { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? COMPANY_NAME { get; set; } //for select
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public string? COMPANY_CODE { get; set; }
        public string? SUPP_NAME { get; set; }
        public string? SUPP_ADDRESS1 { get; set; }
        public string? SUPP_ADDRESS2 { get; set; }
        public string? SUPP_ADDRESS3 { get; set; }
        public string? SUPP_EMAIL { get; set; }
        public string? SUPP_PHONE { get; set; }
        public string? SUPP_CODE { get; set; }
        public string? SUPP_STATE_NAME { get; set; }
        public string? SUPP_ZIP { get; set; }
        public string? SUPP_CITY { get; set; }
        public List<PurchDetails> PurchDetails { get; set; }
    }
    public class PurchDetails
    {
        public long? ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int? GRN_DET_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public int PURCH_ID { get; set; }
        public string? PACKING { get; set; }
        public float? QUANTITY { get; set; }
        public float? RATE { get; set; }
        public float? AMOUNT { get; set; }
        public float? RETURN_QTY { get; set; }
        public string? ITEM_DESC { get; set; }
        public long? PO_DET_ID { get; set; }
        public string? UOM { get; set; }
        public float? DISC_PERCENT { get; set; }
        public float? COST { get; set; }
        public float? SUPP_PRICE { get; set; }
        public float? SUPP_AMOUNT { get; set; }
        public decimal? VAT_PERC { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public int? GRN_STORE_ID { get; set; }
        public float RETURN_AMOUNT { get; set; }
        public string STORE_NAME { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public decimal PO_QUANTITY { get; set; }
        public decimal GRN_QUANTITY { get; set; }
        public string? GRN_NO { get; set; }
        public DateTime? GRN_DATE { get; set; }
        public decimal? PENDING_QTY { get; set; }
        public float? TOTAL_AMOUNT { get; set; }
    }
    public class PurchResponce
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public PurchHeader? Data { get; set; }
        //public List<PurchHeader>? PurchHeaders { get; set; }
        //public int? UserId { get; set; }
        public List<PurchaseInvoice> PurchHeaders { get; set; }

    }
    public class PurchSelectResponce
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public PurchHeader? Data { get; set; }
        

    }
    public class GrnPendingQty
    {
        public int GRN_ID { get; set; }
        public int GRN_NO { get; set; }
        public DateTime GRN_DATE { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal RATE { get; set; }
        public decimal RETURN_QTY { get; set; }
        public int PO_DET_ID { get; set; }
        public decimal COST { get; set; }
        public decimal SUPP_PRICE { get; set; }
        public decimal SUPP_AMOUNT { get; set; }
        public int GRN_DET_ID { get; set; }
        public string UOM { get; set; }
        public decimal INVOICE_QTY { get; set; }
        public decimal PENDING_QTY { get; set; }
    }
    public class GrnPendingQtyResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<GrnPendingQty> Data { get; set; }
    }
    public class PurchaseVoucherResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int PURCHASE_NO { get; set; }
    }
    public class PendingGRNRequest
    {
        public int SUPP_ID { get; set; }
       
    }

}
