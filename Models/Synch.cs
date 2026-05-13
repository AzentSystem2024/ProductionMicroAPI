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
}
