namespace MicroApi.Models
{
    public class Suppliers
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? HQID { get; set; }
        public int? AC_HEAD_ID { get; set; }
        public string? SUPP_CODE { get; set; }
        public string? SUPP_NAME { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? ZIP { get; set; }
        public int? STATE_ID { get; set; }
        public string? CITY { get; set; }
        public int? COUNTRY_ID { get; set; }
        public string? PHONE { get; set; }
        public string? EMAIL { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public string? MOBILE_NO { get; set; }
        public string? NOTES { get; set; }
        public string? FAX_NO { get; set; }
        public string? VAT_REGNO { get; set; }
        public int? CURRENCY_ID { get; set; }
        public int? PAY_TERM_ID { get; set; }
        public int? VAT_RULE_ID { get; set; }
        public string? COUNTRY_NAME { get; set; }
        public string? CURRENCY_CODE { get; set; }
        public string? PAYMENT_CODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? STATE_NAME { get; set; }
        public string? IS_DELETED { get; set; }
        public List<SupplierCost> Supplier_cost { get; set; }
    }
    public class SupplierCost
    {
        public int? SUPP_ID { get; set; }
        public int? COST_ID { get; set; }
    }

    public class SupplierResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Suppliers data { get; set; }
    }
}
