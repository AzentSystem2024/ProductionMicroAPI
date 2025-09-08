namespace MicroApi.Models
{
    public class Attachments
    {
        public int ID { get; set; }
        public int DOC_TYPE { get; set; }
        public int DOC_ID { get; set; }
        public string FILE_NAME { get; set; }
        public byte[] FILE_DATA { get; set; }
        public string REMARKS { get; set; }
        public int USER_ID { get; set; }
        public string? USER_NAME { get; set; }
        public DateTime CREATED_DATE_TIME { get; set; }
    }

    public class AttachmentInput
    {
        public int DOC_TYPE { get; set; }
        public int DOC_ID { get; set; }
    }

    public class AttachmentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<Attachments> data { get; set; }
    }
}
