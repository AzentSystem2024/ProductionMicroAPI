namespace MicroApi.Models
{
    public class DocSettings
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public int USER_ID { get; set; }
        public List<DocSettingItem> DOC_SETTINGS { get; set; }
    }
    public class DocSettingItem
    {
        public int TRANS_TYPE { get; set; }
        public string GROUP_CODE { get; set; }
        public string PREFIX { get; set; }
        public int START { get; set; }
        public int WIDTH { get; set; }
        public bool VERIFY_REQUIRED { get; set; }
    }
    public class DocSettingsResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class DocSettingsListRequest
    {
        public int COMPANY_ID { get; set; }
       // public int FIN_ID { get; set; }
    }
    public class DocSettingsList
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string PREFIX { get; set; }
        public int? START { get; set; }
        public int? WIDTH { get; set; }
        public bool? VERIFY_REQUIRED { get; set; }
        public string LAST_NO { get; set; }
        public string NEXT_VOUCHER_NO { get; set; }
    }
    public class DocSettingsListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DocSettingsList> Data { get; set; }
    }
    //public class DocSettingsVoucherRequest
    //{
    //    public int COMPANY_ID { get; set; }
    //     public int TRANS_TYPE { get; set; }
    //}
    //public class DocSettingsVoucherResponse
    //{
    //    public int flag { get; set; }
    //    public string Message { get; set; }
    //    public string VOUCHER_NO { get; set; }

    //}
}
