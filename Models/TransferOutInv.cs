namespace MicroApi.Models
{
    public class TransferOutInv
    {
        public int COMPANY_ID { get; set; }
        public int STORE_ID { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public int DEST_STORE_ID { get; set; }
        public double NET_AMOUNT { get; set; }
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }
        public string NARRATION { get; set; }
        public int? REASON_ID { get; set; }
        public bool? IS_APPROVED { get; set; }

        // Detail rows
        public List<TransferOutDetail> DETAILS { get; set; }
    }
    public class TransferOutDetail
    {
        public int ITEM_ID { get; set; }
        public string UOM { get; set; }
        public double? QUANTITY { get; set; }
        public double COST { get; set; }
        //public double AMOUNT { get; set; }
        public string? BATCH_NO { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
    public class TransferOutInvUpdate
    {
        public int TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public int? DEST_STORE_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public int? FIN_ID { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? REASON_ID { get; set; }
        public string? DOC_NO { get; set; }
        public string? COMPANY_NAME { get; set; }      //for select
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? COMPANY_CODE { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public string? STORE_CODE { get; set; }
        public string? STORE_ADDRESS1 { get; set; }
        public string? STORE_ADDRESS2 { get; set; }
        public string? STORE_ADDRESS3 { get; set; }
        public string? STORE_ZIP { get; set; }
        public string? STORE_CITY { get; set; }
        public string? STORE_STATE { get; set; }
        public string? STORE_PHONE { get; set; }
        public string? STORE_EMAIL { get; set; }
        public List<TransferOutDetailUpdate>? DETAILS { get; set; }
    }
    public class TransferOutDetailUpdate
    {
        public int ID { get; set; }
        public int? ITEM_ID { get; set; }
        public string? UOM { get; set; }
        public double? QUANTITY { get; set; }
        public double? COST { get; set; }
        public double? AMOUNT { get; set; }
        public string? BATCH_NO { get; set; }
        public double? QUANTITY_AVAILABLE { get; set; }
        public string? BARCODE { get; set; }
        public string? DESCRIPTION { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }

    }
    public class TransferOutDetailList
    {
        public int ID { get; set; }
        public int TRANS_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public int? DEST_STORE_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public string? NARRATION { get; set; }
        public int? REASON_ID { get; set; }
        public string? STORE_NAME { get; set; }
        public string? DOC_NO { get; set; }
        public string? STATUS { get; set; }


    }
    public class TransferSaveResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class ItemInfo
    {
        public int ID { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public double COST { get; set; }
        public double QUANTITY_AVAILABLE { get; set; }
    }
    public class ItemRequest
    {
        public int STORE_ID { get; set; }
        
    }
    public class ItemInfoResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ItemInfo> Data { get; set; }
    }
    public class TransferoutinvResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<TransferOutDetailList> Header { get; set; }

    }
    public class TransferDoc
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int TRANSFER_NO { get; set; }
    }
    public class TransferOutListRequest
    {
        public int COMPANY_ID { get; set; }

    }
}
