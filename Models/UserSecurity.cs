namespace MicroApi.Models
{
    public class UserSecurity
    {
        public bool Numbers { get; set; }
        public bool SpecialCharacters { get; set; }
        public bool LowercaseCharacters { get; set; }
        public bool UppercaseCharacters { get; set; }
        public int MinimumLength { get; set; }
        public bool PasswordValidationRequired { get; set; }
    }
    public class UserSecurityLogin
    {
        public string? LoginName { get; set; }
    }

    public class UserSecurityResponse
    {
        public int flag { get; set; }
        public string? message { get; set; }
        public List<UserSecurity> data { get; set; }
        public List<UserSecurityLogin> Login { get; set; }

        public UserSecurityResponse()
        {
            data = new List<UserSecurity>();
            Login = new List<UserSecurityLogin>();
        }
    }
    public class ChangePassword
    {
        public int UserID { get; set; }
        public string NewPassword { get; set; }
    }

    public class PasswordResponse
    {
        public bool Success { get; set; }
    }

    public class ChangePasswordResponse
    {
        public string flag { get; set; }
        public string Message { get; set; }
       // public PasswordResponse Data { get; set; }
    }

}
