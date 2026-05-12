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
}
