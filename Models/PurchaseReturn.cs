namespace MicroApi.Models
{
    public class PurchaseReturn
    {
        public int? TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? COMPANY_NAME { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public string? RET_NO { get; set; }
        public DateTime? RET_DATE { get; set; }
        public int? SUPP_ID { get; set; }
        public string? SUPPLIER_NAME { get; set; }
        public int? GRN_ID { get; set; }
        public string? GRN_NO { get; set; }
        public bool? IS_CREDIT { get; set; }
        public decimal? GROSS_AMOUNT { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? NET_AMOUNT { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public string? STATUS { get; set; }
        public string? CURRENCY_SYMBOL { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? ADDRESS1 { get; set; }//FOR SELECT
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public string? COMPANY_CODE { get; set; }
        public string? SUPP_ADDRESS1 { get; set; }
        public string? SUPP_ADDRESS2 { get; set; }
        public string? SUPP_ADDRESS3 { get; set; }
        public string? SUPP_EMAIL { get; set; }
        public string? SUPP_PHONE { get; set; }
        public string? SUPP_CODE { get; set; }
        public string? SUPP_STATE_NAME { get; set; }
        public string? SUPP_ZIP { get; set; }
        public string? SUPP_CITY { get; set; }
        public string? USER_NAME { get; set; }
        public List<PurchaseReturnDetail> PurchDetail { get; set; }
    }
    public class PurchaseReturnDetail
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? COMPANY_NAME { get; set; }
        public int? STORE_ID { get; set; }
        public string? STORE_NAME { get; set; }

        public string? BAR_CODE { get; set; }
        public int? RET_ID { get; set; }
        public int? GRN_DET_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? ITEM_NAME { get; set; }
        public string? BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
        public float? PENDING_QTY { get; set; }
        public float? QUANTITY { get; set; }

        public float? RETURN_QTY { get; set; }

        public float? QTY_STOCK { get; set; }
        public decimal? RATE { get; set; }

        public decimal? SUPP_PRICE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? VAT_PERC { get; set; }
        public decimal? VAT_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public string? UOM { get; set; }
        public string? UOM_PURCH { get; set; }
        public int? UOM_MULTIPLE { get; set; }
        public float? DISC_PERCENT { get; set; }
        public int? PURCH_DET_ID { get; set; }
        public DateTime? PURCH_DATE { get; set; }
        public string? DOC_NO { get; set; }

    }
    public class Input
    {
        public int SUPP_ID { get; set; }
    }
    public class GRN_List
    {

        public int GRN_ID { get; set; }
        public string GRN_NO { get; set; }
        public DateTime GRN_DATE { get; set; }
        public int SUPP_ID { get; set; }
        public string SUPP_NAME { get; set; }
        public string CURRENCY_SYMBOL { get; set; }
        public string CURRENCY { get; set; }

    }
    public class GRN_DATA
    {
        public string BAR_CODE { get; set; }
        public int ITEM_ID { get; set; }

        public int STORE_ID { get; set; }
        public int GRN_ID { get; set; }
        public string ITEM_DES { get; set; }
        public int DETAIL_ID { get; set; }
        public float QUANTITY { get; set; }
        public float RETURN_QUANTITY { get; set; }
        public float RATE { get; set; }
        public float DISC_PERCENT { get; set; }
        public float VAT_PERCENT { get; set; }
        public float SUPP_PRICE { get; set; }
        public float QTY_STOCK { get; set; }
        public string UOM { get; set; }
        public int UOM_MULTPLE { get; set; }
        public string UOM_PURCH { get; set; }
    }
    public class GrnInput
    {
        public int GRN_ID { get; set; }
    }
    public class GRN_Response
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<GRN_List> data { get; set; }
        public List<GRN_DATA> Grndata { get; set; }

    }
    public class PurchaseReturnResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PurchaseReturn> data { get; set; }
    }
    public class PurchaseInvoiceDetail
    {
        public int ID { get; set; }             
        public string DOC_NO { get; set; }      
        public DateTime PURCH_DATE { get; set; }
        public int DETAIL_ID { get; set; }      
        public int GRN_DET_ID { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public float PENDING_QTY { get; set; }
        public decimal RATE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal VAT_PERC { get; set; }
        public float? QTY_STOCK { get; set; }
        public string BARCODE { get; set; }
        public string UOM { get; set; }
        public string UOM_PURCH { get; set; }
        public int UOM_MULTIPLE { get; set; }
       


    }
    public class PurchaseInvoiceDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PurchaseInvoiceDetail> Data { get; set; }
    }
    public class InvoiceInput
    {
        public int SUPP_ID { get; set; }
    }
    public class PurchaseReturnList
    {
        public int ID { get; set; }
        public string RET_NO { get; set; }
        public DateTime RET_DATE { get; set; }
        public int SUPP_ID { get; set; }
        public string SUPP_NAME { get; set; }
        public decimal GROSS_AMOUNT { get; set; }
        public decimal VAT_AMOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public int TRANS_ID { get; set; }
        public int TRANS_STATUS { get; set; }
    }
    public class PurchaseReturnListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PurchaseReturnList> Data { get; set; }
    }
    public class PurchaseReturnDoc
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int PURCHASE_NO { get; set; }
    }
    public class PurchReturnhis
    {
        public int ACTION { get; set; }
        public int DOC_ID { get; set; }
        public DateTime TIME { get; set; }
        public string DESCRIPTION { get; set; }
        public int DOC_TYPE_ID { get; set; }
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
    }
    public class PurchReturnHisRequest
    {
        public int TRANS_ID { get; set; }
    }
}
