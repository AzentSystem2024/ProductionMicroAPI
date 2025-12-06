using Newtonsoft.Json;

namespace MicroApi.Models
{
    public class PurchaseOrderHeader
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? PO_NO { get; set; }
        public DateTime? PO_DATE { get; set; }
        public int? SUPP_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? SUPP_CONTACT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? SUPP_ADDRESS { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? SUPP_MOBILE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? REF_NO { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PAY_TERM_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? DELIVERY_TERM_ID { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PO_STATUS { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? NOTES { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? GROSS_AMOUNT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? TAX_AMOUNT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? NET_AMOUNT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TRANS_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? FIN_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? SHIP_TO { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? PURPOSE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? LOCATION { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? CONTACT_NAME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? CONTACT_MOBILE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? DELIVERY_DESC { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ISSUED_EMP_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PO_TYPE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? SUPP_GROSS_AMOUNT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? SUPP_NET_AMOUNT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? EXCHANGE_RATE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CREATED_STORE_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CURRENCY_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? STATUS_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? SUPP_NAME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? CURRENCY { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? STORE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? NARRATION { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? STATUS { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public DateTime? DELIVERY_DATE { get; set; }
        public int? USER_ID { get; set; }
        public string? PAY_TERM { get; set; }
        public string? DELIVERY_TERM { get; set; }
        public string? CURRENCY_NAME { get; set; }
        public string? VAT_RULE_NAME { get; set; }
        public string? SUPPLIER_EMAIL { get; set; }
        public bool? IS_APPROVED { get; set; }
        public string? ADDRESS1 { get; set; }//FOR SELECT
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public string? COMPANY_NAME { get; set; }
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

        public List<PurchaseOrderDetail>? PoDetails { get; set; }
    }
    public class PurchaseOrderDetail
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public int? PO_ID { get; set; }
        public int? JOB_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public float? QUANTITY { get; set; }
        public string? PACKING { get; set; }
        public float? PRICE { get; set; }
        public float? AMOUNT { get; set; }
        public float? DISC_PERCENT { get; set; }
        public decimal? TAX_PERCENT { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public string? ITEM_DESC { get; set; }
        public string? UOM { get; set; }
        public float? GRN_QTY { get; set; }
        public float? INVOICE_QTY { get; set; }
        public float? SUPP_PRICE { get; set; }
        public float? SUPP_AMOUNT { get; set; }
        public int? CREATE_STORE_ID { get; set; }
        public string? ITEM_CODE { get; set; }
    }

    public class PurchaseOrderResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<PurchaseOrderHeader> data { get; set; }
    }
    public class SupplierInput
    {
        public int SUPP_ID { get; set; }
    }
    public class SupplierList
    {
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public string PURCH_PRICE { get; set; }
        public string LAST_PO_DATE { get; set; }

        public string COST { get; set; }
        public int VAT_CLASS_ID { get; set; }
        public string VAT_NAME { get; set; }
        public string VAT_PERC { get; set; }
        public int CURRENCY_ID { get; set; }
        public string CURRENCY_NAME { get; set; }
        public string EXCHANGE { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_SYMBOL { get; set; }
        public int VAT_RULE_ID { get; set; }
        public string VAT_RULE_NAME { get; set; }
        public int ITEM_ID { get; set; }
        public int PACKING_ID { get; set; }
        public string PACKING_NAME { get; set; }
        public string SUPPLIER_MAIL { get; set; }
        

    }
    public class ItemInput
    {
        public int ITEM_ID { get; set; }
    }
    public class ItemList
    {
        public DateTime PO_DATE { get; set; }
        public string PO_NO { get; set; }
        public string QUANTITY { get; set; }
        public string SUPP_PRICE { get; set; }
        public string PRICE { get; set; }
        public int CURRENCY_ID { get; set; }
        public string CURRENCY_NAME { get; set; }

    }
    public class PurchaseDoc
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int PURCHASE_NO { get; set; }
    }
    public class POhis
    {
        public int ACTION { get; set; }
        public int DOC_ID { get; set; }
        public DateTime TIME { get; set; }
        public string DESCRIPTION { get; set; }
        public int DOC_TYPE_ID { get; set; }
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
    }
    public class POHIS
    {
        public int TRANS_ID { get; set; }
    }
}
