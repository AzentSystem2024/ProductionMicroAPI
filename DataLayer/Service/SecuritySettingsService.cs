using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SecuritySettingsService:ISecuritySettingsService
    {
        public SecuritySettingResponse GetAllSecuritySetting(Int32 intUserID)
        {
            SecuritySettingResponse response = new SecuritySettingResponse();
            response.data = new List<SecuritySettings>();
            response.Tooltip = new List<SecuritySettingsMaster>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_SECURITY_SETTINGS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                // cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                // Assuming the first table contains the SecuritySettings data
                DataTable tbl = ds.Tables[0];
                foreach (DataRow dr in tbl.Rows)
                {
                    response.data.Add(new SecuritySettings
                    {
                        AccountLockAttempt = dr["AccountLockAttempt"] != DBNull.Value ? Convert.ToInt32(dr["AccountLockAttempt"]) : 0,
                        AccountLockDuration = dr["AccountLockDuration"] != DBNull.Value ? Convert.ToInt32(dr["AccountLockDuration"]) : 0,
                        AccountLockFailedLogin = dr["AccountLockFailedLogin"] != DBNull.Value ? Convert.ToInt32(dr["AccountLockFailedLogin"]) : 0,
                        AlertEmailOnPasswordChange = dr["AlertEmailOnPasswordChange"] != DBNull.Value ? Convert.ToBoolean(dr["AlertEmailOnPasswordChange"]) : false,
                        AlertSMSOnPasswordChange = dr["AlertSMSOnPasswordChange"] != DBNull.Value ? Convert.ToBoolean(dr["AlertSMSOnPasswordChange"]) : false,
                        DisableUserOnInactiveDays = dr["DisableUserOnInactiveDays"] != DBNull.Value ? Convert.ToBoolean(dr["DisableUserOnInactiveDays"]) : false,
                        LowercaseCharacters = dr["LowercaseCharacters"] != DBNull.Value ? Convert.ToBoolean(dr["LowercaseCharacters"]) : false,
                        MinimumCategoriesRequired = dr["MinimumCategoriesRequired"] != DBNull.Value ? Convert.ToInt32(dr["MinimumCategoriesRequired"]) : 0,
                        MinimumLength = dr["MinimumLength"] != DBNull.Value ? Convert.ToInt32(dr["MinimumLength"]) : 0,
                        Numbers = dr["Numbers"] != DBNull.Value && Convert.ToInt32(dr["Numbers"]) == 1,
                        //OTPEmailOnPasswordChange = dr["OTPEmailOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr["OTPEmailOnPasswordChange"]) == 1,
                        //OTPSMSOnPasswordChange = dr["OTPSMSOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr["OTPSMSOnPasswordChange"]) == 1,
                        PasswordAge = dr["PasswordAge"] != DBNull.Value ? Convert.ToInt32(dr["PasswordAge"]) : 0,
                        PasswordRepeatCycle = dr["PasswordRepeatCycle"] != DBNull.Value ? Convert.ToInt32(dr["PasswordRepeatCycle"]) : 0,
                        PasswordValidationRequired = dr["PasswordValidationRequired"] != DBNull.Value && Convert.ToInt32(dr["PasswordValidationRequired"]) == 1,

                        SpecialCharacters = dr["SpecialCharacters"] != DBNull.Value && Convert.ToInt32(dr["SpecialCharacters"]) == 1,

                        UnauthorizedBannerMessage = dr["UnauthorizedBannerMessage"] != DBNull.Value ? Convert.ToString(dr["UnauthorizedBannerMessage"]) : string.Empty,
                        UppercaseCharacters = dr["UppercaseCharacters"] != DBNull.Value && Convert.ToInt32(dr["UppercaseCharacters"]) == 1,

                        UserMustChangePasswordOnLogin = dr["UserMustChangePasswordOnLogin"] != DBNull.Value && Convert.ToInt32(dr["UserMustChangePasswordOnLogin"]) == 1,

                        //OTPWhatsappOnPasswordChange = dr["OTPWhatsappOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr["OTPWhatsappOnPasswordChange"]) == 1,

                        AlertWhatsappOnPasswordChange = dr["AlertWhatsappOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr["AlertWhatsappOnPasswordChange"]) == 1,
                        SessionTimeoutMinutes = ADO.ToInt32(dr["SessionTimeoutMinutes"]),

                        //OTPWhatsappOnPasswordChange = dr["OTPWhatsappOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr["OTPWhatsappOnPasswordChange"]) == 1,
                        EnableMFA = dr["EnableMFA"] != DBNull.Value && Convert.ToInt32(dr["EnableMFA"]) == 1,
                        MFAGoogle = dr["MFAGoogle"] != DBNull.Value && Convert.ToInt32(dr["MFAGoogle"]) == 1,
                        MFAMicrosoft = dr["MFAMicrosoft"] != DBNull.Value && Convert.ToInt32(dr["MFAMicrosoft"]) == 1,
                        MFACustom = dr["MFACustom"] != DBNull.Value && Convert.ToInt32(dr["MFACustom"]) == 1,
                        MFAOnLogin = dr["MFAOnLogin"] != DBNull.Value && Convert.ToInt32(dr["MFAOnLogin"]) == 1,
                        MFAOnPasswordChange = dr["MFAOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr["MFAOnPasswordChange"]) == 1,
                        MFASMS = dr["MFASMS"] != DBNull.Value && Convert.ToInt32(dr["MFASMS"]) == 1,
                        MFAWhatsapp = dr["MFAWhatsapp"] != DBNull.Value && Convert.ToInt32(dr["MFAWhatsapp"]) == 1,
                        MFAEmail = dr["MFAEmail"] != DBNull.Value && Convert.ToInt32(dr["MFAEmail"]) == 1,
                        MFASingleToken = dr["MFASingleToken"] != DBNull.Value && Convert.ToInt32(dr["MFASingleToken"]) == 1,

                    });
                }

                // Assuming the second table contains the SecuritySettingsMaster data
                if (ds.Tables.Count > 1)
                {
                    DataTable tbl1 = ds.Tables[1];
                    foreach (DataRow dr1 in tbl1.Rows)
                    {
                        response.Tooltip.Add(new SecuritySettingsMaster
                        {
                            AccountLockAttempt = dr1["AccountLockAttempt"] != DBNull.Value ? Convert.ToString(dr1["AccountLockAttempt"]) : null,
                            AccountLockDuration = dr1["AccountLockDuration"] != DBNull.Value ? Convert.ToString(dr1["AccountLockDuration"]) : null,
                            AccountLockFailedLogin = dr1["AccountLockFailedLogin"] != DBNull.Value ? Convert.ToString(dr1["AccountLockFailedLogin"]) : null,
                            AlertEmailOnPasswordChange = dr1["AlertEmailOnPasswordChange"] != DBNull.Value ? Convert.ToString(dr1["AlertEmailOnPasswordChange"]) : null,
                            AlertSMSOnPasswordChange = dr1["AlertSMSOnPasswordChange"] != DBNull.Value ? Convert.ToString(dr1["AlertSMSOnPasswordChange"]) : null,
                            DisableUserOnInactiveDays = dr1["DisableUserOnInactiveDays"] != DBNull.Value ? Convert.ToString(dr1["DisableUserOnInactiveDays"]) : null,
                            LowercaseCharacters = dr1["LowercaseCharacters"] != DBNull.Value ? Convert.ToString(dr1["LowercaseCharacters"]) : null,
                            MinimumCategoriesRequired = dr1["MinimumCategoriesRequired"] != DBNull.Value ? Convert.ToString(dr1["MinimumCategoriesRequired"]) : null,
                            MinimumLength = dr1["MinimumLength"] != DBNull.Value ? Convert.ToString(dr1["MinimumLength"]) : null,
                            Numbers = dr1["Numbers"] != DBNull.Value ? Convert.ToString(dr1["Numbers"]) : null,
                            //OTPEmailOnPasswordChange = dr1["OTPEmailOnPasswordChange"] != DBNull.Value ? Convert.ToString(dr1["OTPEmailOnPasswordChange"]) : null,
                            //OTPSMSOnPasswordChange = dr1["OTPSMSOnPasswordChange"] != DBNull.Value ? Convert.ToString(dr1["OTPSMSOnPasswordChange"]) : null,
                            PasswordAge = dr1["PasswordAge"] != DBNull.Value ? Convert.ToString(dr1["PasswordAge"]) : null,
                            PasswordRepeatCycle = dr1["PasswordRepeatCycle"] != DBNull.Value ? Convert.ToString(dr1["PasswordRepeatCycle"]) : null,
                            PasswordValidationRequired = dr1["PasswordValidationRequired"] != DBNull.Value ? Convert.ToString(dr1["PasswordValidationRequired"]) : null,
                            SpecialCharacters = dr1["SpecialCharacters"] != DBNull.Value ? Convert.ToString(dr1["SpecialCharacters"]) : null,
                            UnauthorizedBannerMessage = dr1["UnauthorizedBannerMessage"] != DBNull.Value ? Convert.ToString(dr1["UnauthorizedBannerMessage"]) : null,
                            UppercaseCharacters = dr1["UppercaseCharacters"] != DBNull.Value ? Convert.ToString(dr1["UppercaseCharacters"]) : null,
                            UserMustChangePasswordOnLogin = dr1["UserMustChangePasswordOnLogin"] != DBNull.Value ? Convert.ToString(dr1["UserMustChangePasswordOnLogin"]) : null,
                            //OTPWhatsappOnPasswordChange = dr1["OTPWhatsappOnPasswordChange"] != DBNull.Value ? Convert.ToString(dr1["OTPWhatsappOnPasswordChange"]) : null,
                            AlertWhatsappOnPasswordChange = dr1["AlertWhatsappOnPasswordChange"] != DBNull.Value ? Convert.ToString(dr1["AlertWhatsappOnPasswordChange"]) : null,
                            SessionTimeoutMinutes = ADO.ToString(dr1["SessionTimeoutMinutes"]),
                            //EnableMFA = dr1["EnableMFA"] != DBNull.Value && Convert.ToInt32(dr1["EnableMFA"]) == 1,
                            //MFAGoogle = dr1["MFAGoogle"] != DBNull.Value && Convert.ToInt32(dr1["MFAGoogle"]) == 1,
                            //MFAMicrosoft = dr1["MFAMicrosoft"] != DBNull.Value && Convert.ToInt32(dr1["MFAMicrosoft"]) == 1,
                            //MFACustom = dr1["MFACustom"] != DBNull.Value && Convert.ToInt32(dr1["MFACustom"]) == 1,
                            //MFAOnLogin = dr1["MFAOnLogin"] != DBNull.Value && Convert.ToInt32(dr1["MFAOnLogin"]) == 1,
                            //MFAOnPasswordChange = dr1["MFAOnPasswordChange"] != DBNull.Value && Convert.ToInt32(dr1["MFAOnPasswordChange"]) == 1,
                            //MFASMS = dr1["MFASMS"] != DBNull.Value && Convert.ToInt32(dr1["MFASMS"]) == 1,
                            //MFAWhatsapp = dr1["MFAWhatsapp"] != DBNull.Value && Convert.ToInt32(dr1["MFAWhatsapp"]) == 1,
                            //MFAEmail = dr1["MFAEmail"] != DBNull.Value && Convert.ToInt32(dr1["MFAEmail"]) == 1,
                            //MFASingleToken = dr1["MFASingleToken"] != DBNull.Value && Convert.ToInt32(dr1["MFASingleToken"]) == 1,

                        });
                    }
                }

                // Set the flag and message
                response.flag = "Success";
                response.message = "Data retrieved successfully.";
            }

            return response;
        }

        public Int32 Save(SecuritySettings securitySettings, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_SECURITY_SETTINGS";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("AccountLockAttempt", securitySettings.AccountLockAttempt);
                    cmd.Parameters.AddWithValue("AccountLockDuration", securitySettings.AccountLockDuration);
                    cmd.Parameters.AddWithValue("AccountLockFailedLogin", securitySettings.AccountLockFailedLogin);

                    cmd.Parameters.AddWithValue("AlertEmailOnPasswordChange", securitySettings.AlertEmailOnPasswordChange);
                    cmd.Parameters.AddWithValue("AlertSMSOnPasswordChange", securitySettings.AlertSMSOnPasswordChange);

                    cmd.Parameters.AddWithValue("DisableUserOnInactiveDays", securitySettings.DisableUserOnInactiveDays);
                    cmd.Parameters.AddWithValue("LowercaseCharacters", securitySettings.LowercaseCharacters);
                    cmd.Parameters.AddWithValue("MinimumCategoriesRequired", securitySettings.MinimumCategoriesRequired);
                    cmd.Parameters.AddWithValue("MinimumLength", securitySettings.MinimumLength);
                    cmd.Parameters.AddWithValue("Numbers", securitySettings.Numbers);

                    //cmd.Parameters.AddWithValue("OTPEmailOnPasswordChange", securitySettings.OTPEmailOnPasswordChange);
                    //cmd.Parameters.AddWithValue("OTPSMSOnPasswordChange", securitySettings.OTPSMSOnPasswordChange);

                    cmd.Parameters.AddWithValue("PasswordAge", securitySettings.PasswordAge);
                    cmd.Parameters.AddWithValue("PasswordRepeatCycle", securitySettings.PasswordRepeatCycle);
                    cmd.Parameters.AddWithValue("PasswordValidationRequired", securitySettings.PasswordValidationRequired);
                    cmd.Parameters.AddWithValue("SpecialCharacters", securitySettings.SpecialCharacters);
                    cmd.Parameters.AddWithValue("UnauthorizedBannerMessage", securitySettings.UnauthorizedBannerMessage);
                    cmd.Parameters.AddWithValue("UppercaseCharacters", securitySettings.UppercaseCharacters);
                    cmd.Parameters.AddWithValue("UserMustChangePasswordOnLogin", securitySettings.UserMustChangePasswordOnLogin);

                    //cmd.Parameters.AddWithValue("OTPWhatsappOnPasswordChange", securitySettings.OTPWhatsappOnPasswordChange);
                    cmd.Parameters.AddWithValue("AlertWhatsappOnPasswordChange", securitySettings.AlertWhatsappOnPasswordChange);
                    cmd.Parameters.AddWithValue("SessionTimeoutMinutes", securitySettings.SessionTimeoutMinutes);
                    cmd.Parameters.AddWithValue("EnableMFA", securitySettings.EnableMFA);
                    cmd.Parameters.AddWithValue("MFAGoogle", securitySettings.MFAGoogle);
                    cmd.Parameters.AddWithValue("MFAMicrosoft", securitySettings.MFAMicrosoft);
                    cmd.Parameters.AddWithValue("MFACustom", securitySettings.MFACustom);
                    cmd.Parameters.AddWithValue("MFAOnLogin", securitySettings.MFAOnLogin);
                    cmd.Parameters.AddWithValue("MFAOnPasswordChange", securitySettings.MFAOnPasswordChange);
                    cmd.Parameters.AddWithValue("MFAWhatsapp", securitySettings.MFAWhatsapp);
                    cmd.Parameters.AddWithValue("MFAEmail", securitySettings.MFAEmail);
                    cmd.Parameters.AddWithValue("MFASMS", securitySettings.MFASMS);
                    cmd.Parameters.AddWithValue("MFASingleToken", securitySettings.MFASingleToken);
                    cmd.Parameters.AddWithValue("UserID", securitySettings.UserID);



                    // cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
