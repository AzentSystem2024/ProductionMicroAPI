namespace MicroApi.Models
{
    public class Account
    {
        public int GroupID {  get; set; }
       // public string? GROUP_CODE { get; set; }
        public string? GroupName { get; set;}
        public int GroupSuperID { get; set; }

        public int? GroupType { get; set; }
        public bool IsSystemGroup { get; set; }
        public string ArabicName { get; set; }
        public double GroupOrder {  get; set; }
        public int? GroupLevel { get; set; }
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
        public int GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;
        public int SerialNo { get; set; }
        public bool IsInactive { get; set; }

    }
}
