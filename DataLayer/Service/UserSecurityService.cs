using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class UserSecurityService: IUserSecurityService
    {
        public UserSecurityResponse GetAllUserSecurity(Int32 intUserID)
        {
            UserSecurityResponse response = new UserSecurityResponse();
            response.data = new List<UserSecurity>();
            response.Login = new List<UserSecurityLogin>();

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CHANGE_PASSWORD";
            cmd.Parameters.AddWithValue("ACTION", 0);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // SecuritySettings data
            DataTable tbl = ds.Tables[0];
            foreach (DataRow dr in tbl.Rows)
            {
                response.data.Add(new UserSecurity
                {
                    Numbers = dr["Numbers"] != DBNull.Value && Convert.ToInt32(dr["Numbers"]) == 1,
                    SpecialCharacters = dr["SpecialCharacters"] != DBNull.Value && Convert.ToInt32(dr["SpecialCharacters"]) == 1,
                    LowercaseCharacters = dr["LowercaseCharacters"] != DBNull.Value ? Convert.ToBoolean(dr["LowercaseCharacters"]) : false,
                    UppercaseCharacters = dr["UppercaseCharacters"] != DBNull.Value && Convert.ToInt32(dr["UppercaseCharacters"]) == 1,
                    MinimumLength = dr["MinimumLength"] != DBNull.Value ? Convert.ToInt32(dr["MinimumLength"]) : 0,
                    PasswordValidationRequired = ADO.Toboolean(dr["PasswordValidationRequired"])
                });
            }

            // Login name
            if (ds.Tables.Count > 1)
            {
                DataTable tbl1 = ds.Tables[1];
                foreach (DataRow dr1 in tbl1.Rows)
                {
                    response.Login.Add(new UserSecurityLogin
                    {
                        LoginName = dr1["LOGIN_NAME"] != DBNull.Value ? Convert.ToString(dr1["LOGIN_NAME"]) : null
                    });
                }
            }
            response.flag = 1;
            response.message = "Success";

            return response;
        }
        public ChangePasswordResponse UpdateUserPassword(ChangePassword changePassword, int userID)
        {
            ChangePasswordResponse res = new ChangePasswordResponse();
           // res.Data = new PasswordResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CHANGE_PASSWORD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@USER_ID", changePassword.UserID);
                        cmd.Parameters.AddWithValue("@NewPassword", AzentLibrary.Library.EncryptString(changePassword.NewPassword));

                        cmd.ExecuteNonQuery();
                    }

                    // ✅ Send notification email (optional, similar to your ChangePassword logic)
                    //string queryEmail = "SELECT Email, LOGIN_NAME FROM TB_USERS WHERE USER_ID = " + ADO.SQLString(changePassword.UserID);
                    //DataTable dtUser = ADO.GetDataTable(queryEmail);

                    //if (dtUser.Rows.Count > 0)
                    //{
                    //    string emailID = dtUser.Rows[0]["Email"].ToString();
                    //    string userName = dtUser.Rows[0]["LOGIN_NAME"].ToString();

                    //    string subject = "Your password has been changed successfully";
                    //    string message = $"Dear {userName},<br/><br/>Your password has been updated successfully.<br/><br/>New Password: {changePassword.NewPassword}<br/><br/>Regards,<br/>Security Team";

                    //    // Optional: Send email notification
                    //    _notificationService.SendEmail(changePassword.UserID, emailID, subject, message);
                    //}

                    res.flag = "1";
                    res.Message = "Password updated successfully.";
                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.Message = ex.Message;
            }

            return res;
        }

    }
}
