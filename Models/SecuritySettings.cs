namespace MicroApi.Models
{
    public class SecuritySettings
    {
            public int UserID { get; set; }
            public int AccountLockAttempt { get; set; }
            public int AccountLockDuration { get; set; }
            public int AccountLockFailedLogin { get; set; }
            public bool AlertEmailOnPasswordChange { get; set; }
            public bool AlertSMSOnPasswordChange { get; set; }
            public bool DisableUserOnInactiveDays { get; set; }
            public bool LowercaseCharacters { get; set; }
            public int MinimumCategoriesRequired { get; set; }
            public int MinimumLength { get; set; }
            public bool Numbers { get; set; }
            //public bool OTPEmailOnPasswordChange { get; set; }
            //public bool OTPSMSOnPasswordChange { get; set; }
            public int PasswordAge { get; set; }
            public int PasswordRepeatCycle { get; set; }
            public bool PasswordValidationRequired { get; set; }
            public bool SpecialCharacters { get; set; }
            public string UnauthorizedBannerMessage { get; set; }
            public bool UppercaseCharacters { get; set; }
            public bool UserMustChangePasswordOnLogin { get; set; }
            public int SessionTimeoutMinutes { get; set; }
            //public bool OTPWhatsappOnPasswordChange { get; set; }
            public bool AlertWhatsappOnPasswordChange { get; set; }
            public bool EnableMFA { get; set; }
            public bool MFAGoogle { get; set; }
            public bool MFAMicrosoft { get; set; }
            public bool MFACustom { get; set; }
            public bool MFAOnLogin { get; set; }
            public bool MFAOnPasswordChange { get; set; }
            public bool MFASMS { get; set; }
            public bool MFAWhatsapp { get; set; }
            public bool MFAEmail { get; set; }
            public bool MFASingleToken { get; set; }
        }
        public class SecuritySettingResponse
        {
            public string flag { get; set; }
            public string message { get; set; }
            public List<SecuritySettings> data { get; set; }
            public List<SecuritySettingsMaster> Tooltip { get; set; }
        }
        public class SecuritySettingsMaster
        {
            public string AccountLockAttempt { get; set; }
            public string AccountLockDuration { get; set; }
            public string AccountLockFailedLogin { get; set; }
            public string AlertEmailOnPasswordChange { get; set; }
            public string AlertSMSOnPasswordChange { get; set; }
            public string DisableUserOnInactiveDays { get; set; }
            public string LowercaseCharacters { get; set; }
            public string MinimumCategoriesRequired { get; set; }
            public string MinimumLength { get; set; }
            public string Numbers { get; set; }
            public string OTPEmailOnPasswordChange { get; set; }
            public string OTPSMSOnPasswordChange { get; set; }
            public string PasswordAge { get; set; }
            public string PasswordRepeatCycle { get; set; }
            public string PasswordValidationRequired { get; set; }
            public string SpecialCharacters { get; set; }
            public string UnauthorizedBannerMessage { get; set; }
            public string UppercaseCharacters { get; set; }
            public string UserMustChangePasswordOnLogin { get; set; }
            public string OTPWhatsappOnPasswordChange { get; set; }
            public string AlertWhatsappOnPasswordChange { get; set; }
            public string SessionTimeoutMinutes { get; set; }
            //public bool EnableMFA { get; set; }
            //public bool MFAGoogle { get; set; }
            //public bool MFAMicrosoft { get; set; }
            //public bool MFACustom { get; set; }
            //public bool MFAOnLogin { get; set; }
            //public bool MFAOnPasswordChange { get; set; }
            //public bool MFASMS { get; set; }
            //public bool MFAWhatsapp { get; set; }
            //public bool MFAEmail { get; set; }
            //public bool MFASingleToken { get; set; }

        }
    
}
