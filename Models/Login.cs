namespace MicroApi.Models
{
    public class Login
    {
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FINANCIAL_YEAR_ID { get; set; }
    }

    public class LoginResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

        public int? USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public List<CompanyList> Companies { get; set; } = new List<CompanyList>();
        public int FINANCIAL_YEAR_ID { get; set; } = 1;
        public List<UserMenusPermission> MenuPermissions { get; set; } = new List<UserMenusPermission>();
    }

    public class CompanyList
    {
        public int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
    }
    public class UserMenusPermission
    {
        public int MenuID { get; set; }
        public bool CanAdd { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
    }

}
