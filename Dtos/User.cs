namespace MicroApi.Dtos
{
    public class User
    {
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public int USER_LEVEL { get; set; }
        public String LEVEL_NAME { get; set; }
        public bool IS_INACTIVE { get; set; }
        public string COMPANY_ID { get; set; }
        public bool CAN_VIEW_COST { get; set; }
        public int STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
    }
    public class Settings
    {
        public string ID { get; set; }
        public bool CUST_CODE_AUTO { get; set; }

        public bool SUPP_CODE_AUTO { get; set; }
        public bool EMP_CODE_AUTO { get; set; }
        public bool ITEM_CODE_AUTO { get; set; }
        public string DEFAULT_COUNTRY_CODE { get; set; }
        public string ITEM_PROPERTY1 { get; set; }
        public string ITEM_PROPERTY2 { get; set; }
        public string ITEM_PROPERTY3 { get; set; }
        public string ITEM_PROPERTY4 { get; set; }
        public string ITEM_PROPERTY5 { get; set; }
        public string REFERENCE_LABEL { get; set; }
        public string COMMENT_LABEL { get; set; }
        public string STATE_LABEL { get; set; }
        public bool COMMIT_WITH_SAVE { get; set; }
        public string DATE_FORMAT { get; set; }
        public int CURRENCY_ID { get; set; }
        public string CURRENCY_NAME { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_SYMBOL { get; set; }
        public bool NEGATIVE_ALLOWED { get; set; }

    }
    public class UserResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public User data { get; set; }
        public Settings settings { get; set; }
        // public List<UserRights> Modules { get; set; }
    }
    public class UserVerificationInput
    {
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public int COMPANY_ID { get; set; }


        public string LOCAL_IP { get; set; }
        public string COMPUTER_NAME { get; set; }
        public string DOMAIN_NAME { get; set; }
        public string COMPUTER_USER { get; set; }
        public string INTERNET_IP { get; set; }
        public string SYSTEM_TIME_UTC { get; set; }
        public bool? FORCE_LOGIN { get; set; }
        public string USER_ID { get; set; }
    }
    public class UserLoginResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public User data { get; set; }
        public string token { get; set; }
        public Settings settings { get; set; }
        public List<UserMenu> menus { get; set; }
        public List<MenuPrivileges> privileges { get; set; }
        public string Taxname { get; set; }
    }
    public class UserMenu
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string path { get; set; }
        public List<UserMenuItem> items { get; set; }
    }
    public class UserMenuItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string path { get; set; }
    }
    public class MenuPrivileges
    {
        public string id { get; set; }
        public string text { get; set; }

        public string CanAdd { get; set; }
        public string CanView { get; set; }
        public string CanModify { get; set; }
        public string CanDelete { get; set; }
        public string CanVerify { get; set; }
        public string CanApprove { get; set; }
        public string CanPrint { get; set; }
        public string CanModifyDate { get; set; }
    }
    public class UserRights
    {
        //public string ID { get; set; }
        //public string MODULE_NAME { get; set; }
        //public string MAIN_MODULE_ID { get; set; }
        //public string MAIN_MODULE_NAME { get; set; }
        //public string COMPONENT_NAME { get; set; }
        //public string MENU_ICON { get; set; }
        public string CAN_ADD { get; set; }
        public string CAN_VIEW { get; set; }
        public string CAN_MODIFY { get; set; }
        public string CAN_DELETE { get; set; }
        public string CAN_VERIFY { get; set; }
        public string CAN_APPROVE { get; set; }
        public string CAN_PRINT { get; set; }
        public string CAN_EXPORT { get; set; }
        public string CAN_MODIFY_DATE { get; set; }
        public string CAN_VIEW_COST { get; set; }
        public int DOC_TYPE { get; set; }
    }
    public class UserRightsResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<UserRights> UserRight { get; set; }

    }
    public class UserRightsInput
    {
        public int UserID { get; set; }
        public string Path { get; set; }

    }
    public class UserRightAccessInput
    {
        public int LEVEL_ID { get; set; }
        public string COMPONENT_NAME { get; set; }
    }
}
