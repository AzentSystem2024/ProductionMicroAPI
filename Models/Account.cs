namespace MicroApi.Models
{
    public class Account
    {
        public int GROUP_ID {  get; set; }
       // public string? GROUP_CODE { get; set; }
        public string? GROUP_NAME { get; set;}
        public int GROUP_SUPER_ID { get; set; }

        public int? GROUP_TYPE { get; set; }
        public bool IS_SYSTEM_GROUP { get; set; }
        public string ARABIC_NAME { get; set; }
        public double GROUP_ORDER {  get; set; }
        public int? GROUP_LEVEL { get; set; }
       // public int SERIAL_NO { get; set; }
      //  public bool IS_INACTIVE { get; set; }

    }
    public class AccountListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<AccountUpdate> Data { get; set; }

    }
    public class AccountResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public AccountUpdate Data { get; set; }

    }
    public class AccountUpdate
    {
        public int GROUP_ID { get; set; }
        public string GROUP_CODE { get; set; }
        public string GROUP_NAME { get; set; } = string.Empty;
        public string ARABIC_NAME { get; set; } = string.Empty;
        public int SERIAL_NO { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
}
