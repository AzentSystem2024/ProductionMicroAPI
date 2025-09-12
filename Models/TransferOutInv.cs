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

        // Detail rows
        public List<TransferOutDetail> DETAILS { get; set; }
    }
    public class TransferOutDetail
    {
        public int ITEM_ID { get; set; }
        public string UOM { get; set; }
        public double QUANTITY { get; set; }
        public double COST { get; set; }
        public double AMOUNT { get; set; }
        public string? BATCH_NO { get; set; }
        public double REQ_QTY { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
    public class TransferOutInvUpdate
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public int? DEST_STORE_ID { get; set; }
        public double? NET_AMOUNT { get; set; }
        public int? FIN_ID { get; set; }
        public int? USER_ID { get; set; }
        public string? NARRATION { get; set; }
        public int? REASON_ID { get; set; }

        // Detail rows
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
        public double REQ_QTY { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
    public class TransferSaveResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

    }
    public class ItemInfo
    {
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public double UNIT_COST { get; set; }
        public double QTY_AVAILABLE { get; set; }
        public double QTY_ISSUED { get; set; }
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
        public List<TransferOutInvUpdate> Header { get; set; }

    }
}
