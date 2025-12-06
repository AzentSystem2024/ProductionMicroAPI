namespace MicroApi.Models
{
    public class GRN
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int PO_ID { get; set; }
        public int PO_NO { get; set; }
        public int GRN_NO { get; set; }
        public DateTime GRN_DATE { get; set; }
        public int SUPP_ID { get; set; }
        public float NET_AMOUNT { get; set; }
        public float? TOTAL_COST { get; set; }
        public float SUPP_GROSS_AMOUNT { get; set; }
        public float SUPP_NET_AMOUNT { get; set; }
        public float EXCHANGE_RATE { get; set; }
        public string? NARRATION { get; set; }
        public string? STORE_NAME { get; set; }
        public string? SUPPPLIER_NAME { get; set; }
        public string? STATUS { get; set; }
        public int CURRENCY_ID { get; set; }
        public int USER_ID { get; set; }
        public string? CURRENCY_SYMBOL { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? ADDRESS1 { get; set; }//FOR SELECT
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? COMPANY_NAME { get; set; }
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
        public List<GRNDetail> GRNDetails { get; set; }
        public List<GRN_ITEM_COST>? GRN_Item_Cost { get; set; }
        public List<GRN_COST>? GRN_Cost { get; set; }

    }
    public class GRN_COST
    {
        public int ID { get; set; }
        public int STORE_ID { get; set; }
        public int GRN_ID { get; set; }
        public int COST_ID { get; set; }
        public float PERCENT { get; set; }
        public float AMOUNT_FC { get; set; }
        public float AMOUNT { get; set; }
        public float VALUE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_LOCAL_CURRENCY { get; set; }
        public bool IS_FIXED_AMOUNT { get; set; }
    }
    public class GRN_ITEM_COST
    {
        public int ID { get; set; }
        public int GRN_ID { get; set; }
        public int STORE_ID { get; set; }
        public int ITEM_ID { get; set; }
        public int COST_ID { get; set; }
        public float AMOUNT { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_LOCAL_CURRENCY { get; set; }
        public bool IS_FIXED_AMOUNT { get; set; }
    }
    public class GRNDetail
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public int PO_DETAIL_ID { get; set; }
        public int GRN_ID { get; set; }
        public int ITEM_ID { get; set; }
        public float QUANTITY { get; set; }
        public float RATE { get; set; }
        public float AMOUNT { get; set; }
        public float INVOICE_QTY { get; set; }
        public float DISC_PERCENT { get; set; }
        public float COST { get; set; }
        public float SUPP_PRICE { get; set; }
        public float SUPP_AMOUNT { get; set; }
        public float RECEIVED_QTY { get; set; }
        public string UOM_PURCH { get; set; }
        public string UOM { get; set; }
        public int UOM_MULTIPLE { get; set; }
        public string STORE_NAME { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public float PO_QUANTITY { get; set; }
        public float GRN_QUANTITY { get; set; }
        public float? PRICE { get; set; }


    }
    public class PO
    {
        public string PO_NO { get; set; }
        public DateTime PO_DATE { get; set; }
        public string SUPP_NAME { get; set; }
        public int PO_ID { get; set; }
        public int SUPP_ID { get; set; }
    }
    public class poinput
    {
        public int STORE_ID { get; set; }
        public int SUPP_ID { get; set; }
    }
    public class GRNResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PO> data { get; set; }
        public List<PODetails> Podetails { get; set; }
        public List<LandedCosts> LandedCosts { get; set; }
        public List<GRN> grnheader { get; set; }
    }
    public class GRNSaveResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }

    }
    public class PODetails
    {
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal PRICE { get; set; }
        public decimal QUANTITY { get; set; }
        public string UOM { get; set; }
        public int UOM_MULTIPLE { get; set; }
        public string UOM_PURCH { get; set; }
        public float GRN_QTY { get; set; }
        public int CURRENCY_ID { get; set; }
        public string? CURRENCY_NAME { get; set; }
        public float DISC_PERCENT { get; set; }
        public string? CURRENCY_SYMBOL { get; set; }
        public float SUPP_AMOUNT { get; set; }
        public float SUPP_PRICE { get; set; }
        public int ITEM_ID { get; set; }
        public int PO_DETAIL_ID { get; set; }

    }
    public class LandedCosts
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; } = string.Empty;
        public bool IS_LOCAL_CURRENCY { get; set; }
        public bool IS_FIXED_AMOUNT { get; set; }
        public float VALUE { get; set; }

    }
    public class PODetailsInput
    {
        public int PO_ID { get; set; }
    }
}
