namespace MicroApi.Models
{
    public class Transfer
    {
        public string TRANSFER_NO { get; set; }
        public DateTime TRANSFER_DATE { get; set; }
       // public int TRANSFER_FROM { get; set; }
        public string TRANSFER_TO { get; set; }
        public int ART_NO { get; set; }
        public string COLOR { get; set; }
        public string CATEGORY { get; set; }
        public string PACKING { get; set; }
        public string RECEIVED_TIME { get; set; }
        public bool IS_RECEIVED { get; set; }
        public int RECEIVED_QUANTITY { get; set; }
        public int TRANSFER_QTY { get; set; }
        public int PAIR_QTY { get; set; }
        public int TOTAL_PAIR_QTY { get; set; }

    }
    public class TransferListResponse
    {
        public int flag { get; set; } = 1;
        public string Message { get; set; }
        public List<Transfer> Data { get; set; }
    }
}
