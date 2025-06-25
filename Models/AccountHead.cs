namespace MicroApi.Models
{
    public class AccountHead
    {
       // public int HEAD_ID { get; set; }
       // public string? HEAD_CODE { get; set; }
        public  string? HeadName{ get; set; }
        public int GroupID { get; set; }
        public bool IsDirect { get; set; }
        public bool IsSysHead { get; set; }
        public string ArabicName { get; set; }
        public bool IsActive { get; set; }
        public int SerialNo { get; set; }
        public int CostTypeID {  get; set; }

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
        public int HeadID { get; set; }
        public string? HeadCode { get; set; } = null;

        public string HeadName { get; set; } = string.Empty;
        public int GroupID { get; set; }
        public bool IsDirect { get; set; }
        public bool IsSysHead { get; set; }
        public string ArabicName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SerialNo { get; set; }
        public int? MainGroupId { get; set; }
        public int? SubGroupId { get; set; }

    }
    
   
}
