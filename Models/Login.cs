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
        public int? COMPANY_ID { get; set; }
        public string DEFAULT_COUNTRY_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public int? USER_ROLE_ID { get; set; }
        public string? USER_ROLE_NAME { get; set; }
        public List<FinancialYear> FINANCIAL_YEARS { get; set; } = new();
        public CompanyList SELECTED_COMPANY { get; set; } = new CompanyList();
        public List<CompanyList> Companies { get; set; } = new List<CompanyList>();
        public List<MenuGroup> MenuGroups { get; set; } = new List<MenuGroup>();
        public int? VAT_ID { get; set; }
        public string VAT_NAME { get; set; }
        public GeneralSettings GeneralSettings { get; set; }
        public List<StoreInfo> Configuration { get; set; } = new List<StoreInfo>();

    }

    public class CompanyList
    {
        public int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string? STATE_NAME { get; set; }
    }
    public class MenuGroup
    {
        public int MenuGroupID { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public decimal MenuGroupOrder { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<MenuGroup> SubGroups { get; set; } = new List<MenuGroup>();

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
        public List<Menu> SubMenus { get; set; } = new List<Menu>();
    }

    public class InitLoginResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int? USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public List<CompanyList> Companies { get; set; } = new List<CompanyList>();
        public int? VAT_ID { get; set; }
        public string VAT_NAME { get; set; }
    }
    public class FinancialYear
    {
        public int FIN_ID { get; set; }
        public string FIN_CODE { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        public bool IS_CLOSED { get; set; }
    }

    public class GeneralSettings
    {
        public string ID_PREFIX { get; set; } = "";
        public string DateFormat { get; set; } = "dd-mm-yyyy";
        public string CURRENCY_NAME { get; set; } = "";
        public string SYMBOL { get; set; } = "";
        public string CODE { get; set; } = "";
        public bool CUST_CODE_AUTO { get; set; }
        public bool SUPP_CODE_AUTO { get; set; }
        public bool EMP_CODE_AUTO { get; set; }
        public bool ITEM_CODE_AUTO { get; set; }
        public string DEFAULT_COUNTRY_CODE { get; set; } = "";
        public string ITEM_PROPERTY1 { get; set; } = "";
        public string ITEM_PROPERTY2 { get; set; } = "";
        public string ITEM_PROPERTY3 { get; set; } = "";
        public string ITEM_PROPERTY4 { get; set; } = "";
        public string ITEM_PROPERTY5 { get; set; } = "";
        public string REFERENCE_LABEL { get; set; } = "";
        public string COMMENT_LABEL { get; set; } = "";
        public string STATE_LABEL { get; set; } = "";
        public string VAT_TITLE { get; set; } = "";
        public string STORE_TITLE { get; set; } = "";
        public bool ENABLE_MATRIX_CODE { get; set; }
        public string? QTN_SUBJECT { get; set; }
        public bool? SELLING_PRICE_INCL_VAT { get; set; }
        public string? HSN_CODE { get; set; }
        public float? GST_PERC { get; set; }
    }
    public class StoreInfo
    {
        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
    }
}
