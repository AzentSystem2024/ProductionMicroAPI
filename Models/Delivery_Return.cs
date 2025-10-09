namespace MicroApi.Models
{
    public class Delivery_Return
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime DR_DATE { get; set; }
        public string REF_NO { get; set; }
        public int CUST_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_PHONE { get; set; }
        public string CONTACT_FAX { get; set; }
        public string CONTACT_MOBILE { get; set; }
        public int SALESMAN_ID { get; set; }
        public int FIN_ID { get; set; }
        public double TOTAL_QTY { get; set; }
        public int USER_ID { get; set; }
        public string NARRATION { get; set; }
        public List<Delivery_Return_Detail> DETAILS { get; set; }
    }
    public class Delivery_Return_Detail
    {
        public int SO_DETAIL_ID { get; set; }
        public int DN_DETAIL_ID { get; set; }
        public int ITEM_ID { get; set; }
        public string REMARKS { get; set; }
        public string UOM { get; set; }
        public double QUANTITY { get; set; }
    }
    public class Delivery_ReturnUpdate
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? DR_DATE { get; set; }
        public string? REF_NO { get; set; }
        public int? CUST_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_FAX { get; set; }
        public string? CONTACT_MOBILE { get; set; }
        public int? SALESMAN_ID { get; set; }
        public int? FIN_ID { get; set; }
        public double? TOTAL_QTY { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public List<Delivery_Return_DetailUpdate>? DETAILS { get; set; }
    }
    public class Delivery_Return_DetailUpdate
    {
        public int? SO_DETAIL_ID { get; set; }
        public int DN_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? REMARKS { get; set; }
        public string? UOM { get; set; }
        public double? QUANTITY { get; set; }
    }
    public class DeliveryReturnsaveResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class DeliverynoteDetail
    {
        public int? SO_DETAIL_ID { get; set; }
        public int? DN_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? REMARKS { get; set; }
        public string? MATRIX_CODE { get; set; }
        public string? UOM { get; set; }
        public double? DELIVERED_QUANTITY { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? SIZE { get; set; }
        public string? COLOR { get; set; }
        public string? STYLE { get; set; }
        public double? SO_QTY { get; set; }
        public double? QTY_AVAILABLE { get; set; }
    }
    public class DeliverynoteDetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<DeliverynoteDetail> Data { get; set; }
    }
    public class DNRequest
    {
        public int CUST_ID { get; set; }
    }
    public class DeliveryRtnList
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? DR_DATE { get; set; }
        public string? DR_NO { get; set; }
        public string? REF_NO { get; set; }
        public int? CUST_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_FAX { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_MOBILE { get; set; }
        public int? SALESMAN_ID { get; set; }
        public double? TOTAL_QTY { get; set; }
        public string? STATUS { get; set; }
        public int? TRANS_ID { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public string? STORE_NAME { get; set; }
    }

    public class DeliveryRtnListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DeliveryRtnList> Data { get; set; }
    }
    public class DeliveryRtnSelect
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? DR_DATE { get; set; }
        public string? DR_NO { get; set; }
        public string? REF_NO { get; set; }
        public int? CUST_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_FAX { get; set; }
        public string? CONTACT_MOBILE { get; set; }
        public int? SALESMAN_ID { get; set; }
        public double? TOTAL_QTY { get; set; }
        public int? TRANS_ID { get; set; }
        public string? STATUS { get; set; }
        public string? NARRATION { get; set; }
        public List<DeliveryRtnDetailSelect>? DETAILS { get; set; }
    }
    public class DeliveryRtnDetailSelect
    {
        public int? DETAIL_ID { get; set; }
        public int? SO_DETAIL_ID { get; set; }
        public int? DN_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? REMARKS { get; set; }
        public string? UOM { get; set; }
        public double? QUANTITY { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public double? DELIVERED_QUANTITY { get; set; }
        public double? QTY_AVAILABLE { get; set; }
        public string? SIZE { get; set; }
        public string? COLOR { get; set; }
        public string? STYLE { get; set; }
        public string? MATRIX_CODE { get; set; }
        
    }

    // Response for select
    public class DeliveryRtnSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public DeliveryRtnSelect? Data { get; set; }
    }
    public class Delivery_Return_Approve
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime DR_DATE { get; set; }
        public string REF_NO { get; set; }
        public int CUST_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_PHONE { get; set; }
        public string CONTACT_FAX { get; set; }
        public string CONTACT_MOBILE { get; set; }
        public int SALESMAN_ID { get; set; }
        public int FIN_ID { get; set; }
        public double TOTAL_QTY { get; set; }
        public int USER_ID { get; set; }
        public string NARRATION { get; set; }
        public List<Delivery_Return_Detail> DETAILS { get; set; }
    }

}
