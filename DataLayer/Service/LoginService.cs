using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class LoginService:ILoginService
    {
        public LoginResponse VerifyLogin(Login loginInput)
        {
            var response = new LoginResponse();

            if (string.IsNullOrWhiteSpace(loginInput.LOGIN_NAME) || string.IsNullOrWhiteSpace(loginInput.PASSWORD))
            {
                response.flag = 0;
                response.Message = "Username and password are required.";
                return response;
            }

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_VERIFY_LOGIN", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", loginInput.LOGIN_NAME);
                        cmd.Parameters.AddWithValue("@PASSWORD", AzentLibrary.Library.EncryptString(loginInput.PASSWORD ?? ""));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", loginInput.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FINANCIAL_YEAR_ID", loginInput.FINANCIAL_YEAR_ID ?? (object)DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            // First result: flag + message
                            if (reader.Read())
                            {
                                response.flag = reader["FLAG"] != DBNull.Value ? Convert.ToInt32(reader["FLAG"]) : 0;
                                response.Message = reader["MESSAGE"]?.ToString();
                            }

                            if (response.flag == 1)
                            {
                                // Second result: user info
                                if (reader.NextResult() && reader.Read())
                                {
                                    response.USER_ID = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : 0;
                                    response.USER_NAME = reader["USER_NAME"]?.ToString();
                                    response.FINANCIAL_YEAR_ID = reader["FINANCIAL_YEAR_ID"] != DBNull.Value ? Convert.ToInt32(reader["FINANCIAL_YEAR_ID"]) : 0;
                                    
                                }

                                // Third result: companies
                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        response.Companies.Add(new CompanyList
                                        {
                                            COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                            COMPANY_NAME = reader["COMPANY_NAME"]?.ToString()
                                        });
                                    }
                                }
                                // Fourth result: menu permissions
                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        response.MenuPermissions.Add(new UserMenusPermission
                                        {
                                            MenuID = reader["MenuID"] != DBNull.Value ? Convert.ToInt32(reader["MenuID"]) : 0,
                                            CanAdd = reader["CanAdd"] != DBNull.Value && Convert.ToBoolean(reader["CanAdd"]),
                                            CanView = reader["CanView"] != DBNull.Value && Convert.ToBoolean(reader["CanView"]),
                                            CanEdit = reader["CanEdit"] != DBNull.Value && Convert.ToBoolean(reader["CanEdit"]),
                                            CanApprove = reader["CanApprove"] != DBNull.Value && Convert.ToBoolean(reader["CanApprove"]),
                                            CanDelete = reader["CanDelete"] != DBNull.Value && Convert.ToBoolean(reader["CanDelete"]),
                                            CanPrint = reader["CanPrint"] != DBNull.Value && Convert.ToBoolean(reader["CanPrint"])
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }


    }
}
