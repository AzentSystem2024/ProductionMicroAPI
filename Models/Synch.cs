namespace MicroApi.Models
{
    public class Synch
    {
        public string TABLE_NAME { get; set; }
        public int STORE_ID { get; set; }
        public object? DATA { get; set; }
    }
    public class SynchResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class SynchDownload
    {
        public string TABLE_NAME { get; set; }

        public int STORE_ID { get; set; }

        public string TIMESTAMP { get; set; }
    }
    public class SynchDownloadResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public string DATA { get; set; }
    }
    public class PendingStoreResponse
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string STORE_NAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string LAST_SYNCH_TIME { get; set; }
        public string TIME_DIFFERENCE { get; set; }
    }
    public class UpdateLastSynchTimeRequest
    {
        public int STORE_ID { get; set; }
    }
}
