namespace MicroApi.Models
{
    public class User
    {
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string MOBILE { get; set; }     
        public string USER_ROLE { get; set; }
        public DateTime? DOB { get; set; }
        public string EMAIL { get; set; }
        public bool IS_INACTIVE { get; set; }
        public List<int> COMPANY_ID { get; set; }

    }

   
    public class UserUpdate
    {
        public int ID { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string MOBILE { get; set; }
        public string USER_ROLE { get; set; }
        public DateTime? DOB { get; set; }
        public string EMAIL { get; set; }
        public List<int> COMPANY_ID { get; set; }
        public bool IS_INACTIVE { get; set; }

    }
    public class UserResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class UserSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<UserUpdate> Data { get; set; }
    }
    public class UserListData
    {
        public int ID { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string MOBILE { get; set; }
        public string USER_ROLE { get; set; }
        public DateTime? DOB { get; set; }
        public string EMAIL { get; set; }
        public bool IS_INACTIVE { get; set; }
    }
    public class UserListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<UserListData> Data { get; set; }
    }
}
