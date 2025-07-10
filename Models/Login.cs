namespace MicroApi.Models
{
    public class Login
    {
        public string LOGIN_NAME { get; set; }
        public string? PASSWORD { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? FINANCIAL_YEAR_ID { get; set; }
    }

    public class LoginResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }

        public int? USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public int FINANCIAL_YEAR_ID { get; set; } = 1;

        public List<CompanyList> Companies { get; set; } = new List<CompanyList>();
        public List<MenuGroup> MenuGroups { get; set; } = new List<MenuGroup>();
    }

    public class CompanyList
    {
        public int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
    }
    public class MenuGroup
    {
        public int MenuGroupID { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public decimal MenuGroupOrder { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu>();
    }

    public class Menu
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public decimal MenuOrder { get; set; }
        public bool? Selected { get; set; }
        public bool CanAdd { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public string Path { get; set; }
    }

    public class InitLoginResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int? USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public List<CompanyList> Companies { get; set; } = new List<CompanyList>();
    }
    //public class UserMenusPermission
    //{
    //    public int MenuID { get; set; }
    //    public string MenuName { get; set; }
    //    public decimal MenuOrder { get; set; }
    //    public bool CanAdd { get; set; }
    //    public bool CanView { get; set; }
    //    public bool CanEdit { get; set; }
    //    public bool CanApprove { get; set; }
    //    public bool CanDelete { get; set; }
    //    public bool CanPrint { get; set; }
    //}

}
