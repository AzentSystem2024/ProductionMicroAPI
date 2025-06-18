namespace MicroApi.Models
{
    public class UserLab
    {
        public int USER_ID { get; set; }
        public int DEPT_ID { get; set; }
        public string DEPT_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public bool IS_ADMIN { get; set; }
        public bool IS_INACTIVE { get; set; }
        public bool IS_LAB_USER { get; set; }
        public bool IS_COLLECTION_USER { get; set; }
        public bool IS_VERIFY_REPORT { get; set; }
    }
    public class UserLabLoginResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public UserLab data { get; set; }
        public string token { get; set; }
        //public List<UserLab> data { get; set; }

    }
    public class UserLabVerificationInput
    {
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }

    }
}
