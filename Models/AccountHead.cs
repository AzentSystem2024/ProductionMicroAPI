namespace MicroApi.Models
{
    public class AccountHead
    {
       // public int HEAD_ID { get; set; }
       // public string? HEAD_CODE { get; set; }
        public  string? HEAD_NAME { get; set; }
        public int GROUP_ID { get; set; }
        public bool IS_DIRECT { get; set; }
        public bool IS_SYS_HEAD { get; set; }
        public string ARABIC_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public int SERIAL_NO { get; set; }

    }
    public class AccountHeadListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<AccountHeadUpdate> Data { get; set; }

    }
    public class AccountHeadResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public AccountHeadUpdate Data { get; set; }

    }
    public class AccountHeadUpdate
    {
        public int HEAD_ID { get; set; }
        public string? HEAD_CODE { get; set; }

        public string HEAD_NAME { get; set; } = string.Empty;
        public int GROUP_ID { get; set; }
        public bool IS_DIRECT { get; set; }
        public bool IS_SYS_HEAD { get; set; }
        public string ARABIC_NAME { get; set; } = string.Empty;
        public bool IS_ACTIVE { get; set; }
        public int SERIAL_NO { get; set; }
        public int? MainGroupId { get; set; }
        public int? SubGroupId { get; set; }

    }
    
   
}
