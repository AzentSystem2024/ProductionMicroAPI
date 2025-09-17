namespace MicroApi.Models
{
    public class TransferIn
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime REC_DATE { get; set; }    
        public int ORIGIN_STORE_ID { get; set; }
        public int ISSUE_ID { get; set; }
        public string NARRATION { get; set; }
        public int USER_ID { get; set; }
        public int FIN_ID { get; set; }           
        public int REASON_ID { get; set; }
        public double NET_AMOUNT { get; set; }

        public List<TransferInDetail> DETAILS { get; set; }
    }
    public class TransferInDetail
    {
        public int ISSUE_DETAIL_ID { get; set; }  
        public int ITEM_ID { get; set; }
        public string UOM { get; set; }
        public double COST { get; set; }
        public double QUANTITY_ISSUED { get; set; }
        public double QUANTITY_RECEIVED { get; set; }
        public string BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
    public class TransferInUpdate
    {
        public int ID { get; set; }  
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? REC_DATE { get; set; }
        public int? ORIGIN_STORE_ID { get; set; }
        public int? ISSUE_ID { get; set; }
        public string? NARRATION { get; set; }
        public int USER_ID { get; set; }
        public int? FIN_ID { get; set; }
        public int? REASON_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public List<TransferInDetail> DETAILS { get; set; }
    }

    public class TransferInDetailUpdate
    {
        public int ID { get; set; }
        public int? ISSUE_DETAIL_ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? UOM { get; set; }
        public double? COST { get; set; }
        public double? QUANTITY_RECEIVED { get; set; }
        public string? BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
        public double? QUANTITY_AVAILABLE { get; set; }
        public double? QUANTITY_ISSUED { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }

    }
    public class TransferList
    {
        public int TransferOut_ID { get; set; }
        public string TransferOut_NO { get; set; }
    }
    public class StoreInput
    {
        public int STORE_ID { get; set; }
    }
    public class TransferInListResponse
    {
        public int Flag { get; set; }

        public string Message { get; set; }
        public List<TransferInItemList> data { get; set; }
    }
    public class TransferOutInput
    {
        public int TransferOut_ID { get; set; }
    }
    public class TransferOutList
    {
        public int ITEM_ID { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM { get; set; }
        public float QUANTITY { get; set; }
        public int DETAIL_ID { get; set; }
        public float COST { get; set; }
    }

    public class TransferInResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<TransferIn> data { get; set; }
    }
    public class TransferInInput
    {
        public int STORE_ID { get; set; }
    }

    public class TransferInItemList
    {
        public int ITEM_ID { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public double COST { get; set; }
        public double QUANTITY_AVAILABLE { get; set; }
        public double QUANTITY_ISSUED { get; set; }
        public int ISSUE_ID { get; set; }
        public int ISSUE_DETAIL_ID { get; set; }
    }
    public class TransferInList
    {
        public int TRANSFER_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public int ORIGIN_STORE_ID { get; set; }
        public double NET_AMOUNT { get; set; }
        public string NARRATION { get; set; }
        public int REASON_ID { get; set; }
        public int ISSUE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public string STATUS { get; set; }
    }

    public class TransferInListsResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<TransferInList> data { get; set; }
    }
    public class TransferInInvUpdate
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public int? ORIGIN_STORE_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public int? FIN_ID { get; set; }
        public int? USER_ID { get; set; }
        public string NARRATION { get; set; }
        public int? REASON_ID { get; set; }
        public int TRANSFER_NO { get; set; }
        public int ISSUE_ID { get; set; }

        public List<TransferInDetailUpdate> DETAILS { get; set; }
    }
    public class TransferinDoc
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int TRANSFER_NO { get; set; }
    }

}
