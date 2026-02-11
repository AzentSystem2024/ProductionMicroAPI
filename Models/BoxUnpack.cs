namespace MicroApi.Models
{
    public class BoxUnpack
    {
        public int COMPANY_ID { get; set; }
        public long BOX_ID { get; set; }
        public DateTime? UNPACK_DATE { get; set; }
    }
    public class BoxUnpackResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
}
