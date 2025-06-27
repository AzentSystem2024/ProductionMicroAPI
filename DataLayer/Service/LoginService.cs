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

                        using (var reader = cmd.ExecuteReader())
                        {
                            // Result 1: FLAG and MESSAGE
                            if (reader.Read())
                            {
                                response.flag = reader["FLAG"] != DBNull.Value ? Convert.ToInt32(reader["FLAG"]) : 0;
                                response.Message = reader["MESSAGE"]?.ToString();
                            }

                            if (response.flag == 1)
                            {
                                // Skip 2nd result set (user info)
                                if (reader.NextResult()) { } // skip user info

                                // Result 3: Company list + USER_ID/USER_NAME
                                if (reader.NextResult())
                                {
                                    bool isFirstRow = true;

                                    while (reader.Read())
                                    {
                                        if (isFirstRow)
                                        {
                                            response.USER_ID = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : 0;
                                            response.USER_NAME = reader["USER_NAME"]?.ToString();
                                            isFirstRow = false;
                                        }

                                        response.Companies.Add(new CompanyList
                                        {
                                            COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                            COMPANY_NAME = reader["COMPANY_NAME"]?.ToString()
                                        });
                                    }

                                    response.FINANCIAL_YEAR_ID = 1;
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
