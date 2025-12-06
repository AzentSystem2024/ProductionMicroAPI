namespace MicroApi.Models
{
    public class Delivery_Note
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime DN_DATE { get; set; }
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
        public int? DN_TYPE { get; set; }
        public bool? IS_APPROVED { get; set; }
        public List<DELIVERY_NOTE_DETAIL> DETAILS { get; set; }
    }
    public class DELIVERY_NOTE_DETAIL
    {
        public int? SO_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string REMARKS { get; set; }
        public string? UOM { get; set; }
        public double? DELIVERED_QUANTITY { get; set; }
        public int? PACKING_ID { get; set; }
    }
    public class Delivery_NoteUpdate
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? DN_DATE { get; set; }
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
        public int? DN_TYPE { get; set; }
        public List<DELIVERY_NOTE_DETAILUPDATE>? DETAILS { get; set; }
    }
    public class DELIVERY_NOTE_DETAILUPDATE
    {
        public int? SO_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? REMARKS { get; set; }
        public string? UOM { get; set; }
        public double? DELIVERED_QUANTITY { get; set; }
        public int? PACKING_ID { get; set; }
    }
    public class DeliverynotesaveResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class SODetail
    {
        public int? SO_DETAIL_ID { get; set; }
        public int? PACKING_ID { get; set; }
        public string? PACKING { get; set; }
        public string? ART_NO { get; set; }
        public string? BRAND { get; set; }
        public double? QUANTITY { get; set; }
        public string? ARTICLE_TYPE { get; set; }
        public string? COLOR { get; set; }
        public string? CATEGORY { get; set; }
        public string? REMARKS { get; set; }
    }
    public class SODetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<SODetail> Data { get; set; }
    }
    public class Delivery_Note_List
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? DN_DATE { get; set; }
        public string? DN_NO { get; set; }
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
        public string NARRATION { get; set; }
    }

    public class Delivery_Note_List_Response
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<Delivery_Note_List> Data { get; set; }
    }
    public class Delivery_Note_Select
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? DN_DATE { get; set; }
        public string? DN_NO { get; set; }
        public string? REF_NO { get; set; }
        public int? CUST_ID { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_FAX { get; set; }
        public string? CONTACT_MOBILE { get; set; }
        public int? SALESMAN_ID { get; set; }
        public double? TOTAL_QTY { get; set; }
        public int? TRANS_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? DN_TYPE { get; set; }
        public List<Delivery_Note_Detail_Select>? DETAILS { get; set; }
    }
    public class Delivery_Note_Detail_Select
    {
        public int? DETAIL_ID { get; set; }
        public int? SO_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? REMARKS { get; set; }
        public string? UOM { get; set; }
        public double? QUANTITY { get; set; }
        public string? ITEM_CODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public double? DELIVERED_QUANTITY { get; set; }
        public int? PACKING_ID { get; set; }
        public string? PACKING { get; set; }
        public string? ART_NO { get; set; }
        public string? BRAND { get; set; }
        public string? ARTICLE_TYPE { get; set; }
        public string? COLOR { get; set; }
        public string? CATEGORY { get; set; }
    }

    // Response for select
    public class Delivery_Note_Select_Response
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public Delivery_Note_Select? Data { get; set; }
    }
    public class DeliveryDoc
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int DELIVERY_NO { get; set; }
    }
    public class DeliveryRequest
    {
        public int CUST_ID { get; set; }
    }
    public class Custdetail
    {
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_FAX { get; set; }
        public string? CONTACT_PHONE { get; set; }
        public string? CONTACT_MOBILE { get; set; }
        public string? CONTACT_EMAIL { get; set; }
    }
    public class CustdetailResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<Custdetail> Data { get; set; }
    }
}

