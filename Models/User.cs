namespace MicroApi.Models
{
    public class User
    {
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string MOBILE { get; set; }     
        public string USER_LEVEL { get; set; }
        public DateTime? DOB { get; set; }
        public string EMAIL { get; set; }
        public bool IS_INACTIVE { get; set; }

        public List<UserCompanyMapping> Companies { get; set; }
    }

    public class UserCompanyMapping
    {
        public string COMPANY { get; set; }      
        public string COMPANY_NAME { get; set; }    
        public string COMPANY_REGION { get; set; }   
        public string COMPANY_GROUP { get; set; }    
        public string COMPANY_TYPE { get; set; }     
    }
    public class UserUpdate
    {
        public string ID { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string WHATSAPP_NO { get; set; }
        public string MOBILE { get; set; }
        public string USER_LEVEL { get; set; }
        public DateTime? DOB { get; set; }
        public string EMAIL { get; set; }
        public bool IS_INACTIVE { get; set; }

        public List<UserCompanyMapping> Companies { get; set; }
    }
    public class UserResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }  
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
        public string USER_LEVEL { get; set; }
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
