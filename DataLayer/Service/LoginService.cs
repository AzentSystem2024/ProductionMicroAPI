using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class LoginService : ILoginService
    {
        public LoginResponse VerifyLogin(Login loginInput)
        {
            var response = new LoginResponse();

            if (string.IsNullOrWhiteSpace(loginInput.LOGIN_NAME) ||
                string.IsNullOrWhiteSpace(loginInput.PASSWORD))
            {
                response.flag = 0;
                response.Message = "Username and password are required.";
                return response;
            }

            try
            {
                using var connection = ADO.GetConnection();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using var cmd = new SqlCommand("SP_VERIFY_LOGIN", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                // Required params
                cmd.Parameters.AddWithValue("@LOGIN_NAME", loginInput.LOGIN_NAME);
                cmd.Parameters.AddWithValue("@PASSWORD", AzentLibrary.Library.EncryptString(loginInput.PASSWORD));
                cmd.Parameters.AddWithValue("@COMPANY_ID", loginInput.COMPANY_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FINANCIAL_YEAR_ID", loginInput.FINANCIAL_YEAR_ID ?? (object)DBNull.Value);

                // Audit params (very important)
                cmd.Parameters.AddWithValue("@LOCAL_IP", loginInput.LOCAL_IP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@COMPUTER_NAME", loginInput.COMPUTER_NAME ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DOMAIN_NAME", loginInput.DOMAIN_NAME ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@COMPUTER_USER", loginInput.COMPUTER_USER ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@INTERNET_IP", loginInput.INTERNET_IP ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SYSTEM_TIME_UTC", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@FORCE_LOGIN", false);
                cmd.Parameters.AddWithValue("@TOKEN", Guid.NewGuid().ToString("N"));

                using var reader = cmd.ExecuteReader();

                // ---------- RESULT 1 : FLAG + MESSAGE ----------
                if (reader.Read())
                {
                    response.flag = Convert.ToInt32(reader["FLAG"]);
                    response.Message = reader["MESSAGE"].ToString();
                }

                if (response.flag != 1)
                    return response;

                // ---------- RESULT 2 : USER INFO ----------
                if (reader.NextResult() && reader.Read())
                {
                    response.USER_ID = reader["USER_ID"] as int?;
                    response.USER_NAME = reader["USER_NAME"]?.ToString();
                    response.DEFAULT_COUNTRY_CODE = reader["DEFAULT_COUNTRY_CODE"]?.ToString();
                    response.COUNTRY_NAME = reader["COUNTRY_NAME"]?.ToString();

                    response.USER_ROLE_ID = Convert.ToInt32(reader["USER_ROLE"]);
                    response.USER_ROLE_NAME = reader["UserRole"]?.ToString();
                }

                // ---------- RESULT 3 : ASSIGNED COMPANIES ----------
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        response.Companies.Add(new CompanyList
                        {
                            COMPANY_ID = Convert.ToInt32(reader["COMPANY_ID"]),
                            COMPANY_NAME = reader["COMPANY_NAME"]?.ToString()
                        });
                    }
                }

                // ---------- RESULT 4 : SELECTED COMPANY ----------
                if (reader.NextResult() && reader.Read())
                {
                    response.SELECTED_COMPANY = new CompanyList
                    {
                        COMPANY_ID = Convert.ToInt32(reader["COMPANY_ID"]),
                        COMPANY_NAME = reader["COMPANY_NAME"]?.ToString(),
                        STATE_ID = Convert.ToInt32(reader["ID"]),
                        STATE_NAME = reader["STATE_NAME"]?.ToString()
                    };
                }

                // ---------- RESULT 5 : MENU ----------
                if (reader.NextResult())
                {
                    var menuGroups = new Dictionary<int, MenuGroup>();

                    while (reader.Read())
                    {
                        int groupId = Convert.ToInt32(reader["MenuGroupID"]);

                        if (!menuGroups.TryGetValue(groupId, out var group))
                        {
                            group = new MenuGroup
                            {
                                MenuGroupID = groupId,
                                Text = reader["Text"]?.ToString(),
                                Icon = reader["Icon"]?.ToString(),
                                MenuGroupOrder = Convert.ToDecimal(reader["MenuGroupOrder"])
                            };
                            menuGroups[groupId] = group;
                        }

                        group.Menus.Add(new Menu
                        {
                            MenuID = Convert.ToInt32(reader["MenuID"]),
                            MenuName = reader["MenuName"]?.ToString(),
                            MenuOrder = Convert.ToDecimal(reader["MenuOrder"]),
                            Selected = Convert.ToBoolean(reader["Selected"]),
                            CanAdd = Convert.ToBoolean(reader["CanAdd"]),
                            CanView = Convert.ToBoolean(reader["CanView"]),
                            CanEdit = Convert.ToBoolean(reader["CanEdit"]),
                            CanApprove = Convert.ToBoolean(reader["CanApprove"]),
                            CanDelete = Convert.ToBoolean(reader["CanDelete"]),
                            CanPrint = Convert.ToBoolean(reader["CanPrint"]),
                            Path = reader["Path"]?.ToString()
                        });
                    }

                    response.MenuGroups = menuGroups.Values.ToList();
                }

                // ---------- RESULT 6 : FINANCIAL YEAR ----------
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        response.FINANCIAL_YEARS.Add(new FinancialYear
                        {
                            FIN_ID = Convert.ToInt32(reader["FIN_ID"]),
                            FIN_CODE = reader["FIN_CODE"]?.ToString(),
                            DATE_FROM = Convert.ToDateTime(reader["DATE_FROM"]),
                            DATE_TO = Convert.ToDateTime(reader["DATE_TO"]),
                            IS_CLOSED = Convert.ToBoolean(reader["IS_CLOSED"])
                        });
                    }
                }

                // ---------- RESULT 7 : PRIVILEGE (SKIP FULLY) ----------
                if (reader.NextResult())
                {
                    while (reader.Read()) { }
                }

                // ---------- RESULT 8 : GENERAL SETTINGS ----------
                if (reader.NextResult() && reader.Read())
                {
                    response.GeneralSettings = new GeneralSettings
                    {
                        ID_PREFIX = reader["ID_PREFIX"]?.ToString(),
                        DateFormat = reader["DateFormat"]?.ToString(),
                        CURRENCY_NAME = reader["CURRENCY_NAME"]?.ToString(),
                        SYMBOL = reader["SYMBOL"]?.ToString(),
                        CODE = reader["CODE"]?.ToString(),
                        DEFAULT_COUNTRY_CODE = reader["DEFAULT_COUNTRY_CODE"]?.ToString(),
                        VAT_TITLE = reader["VAT_TITLE"]?.ToString(),
                        STORE_TITLE = reader["STORE_TITLE"]?.ToString(),
                        ENABLE_MATRIX_CODE = Convert.ToBoolean(reader["ENABLE_MATRIX_CODE"]),
                        QTN_SUBJECT = reader["QTN_SUBJECT"]?.ToString(),
                        SELLING_PRICE_INCL_VAT = Convert.ToBoolean(reader["SELLING_PRICE_INCL_VAT"]),
                        HSN_CODE = reader["FT_HSN_CODE"]?.ToString()
                    };
                }

                // ---------- RESULT 9 : VAT INFO ----------
                if (reader.NextResult() && reader.Read())
                {
                    response.VAT_ID = Convert.ToInt32(reader["VAT_ID"]);
                    response.VAT_NAME = reader["VAT_NAME"]?.ToString();
                }

                // ---------- RESULT 10 : STORE CONFIG ----------
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        response.Configuration.Add(new StoreInfo
                        {
                            STORE_ID = Convert.ToInt32(reader["STORE_ID"]),
                            STORE_NAME = reader["STORE_NAME"]?.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

        public InitLoginResponse InitLoginData(string loginName)
        {
            var response = new InitLoginResponse();

            if (string.IsNullOrWhiteSpace(loginName))
            {
                response.flag = 0;
                response.Message = "Login name is required.";
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
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", loginName);
                        cmd.Parameters.AddWithValue("@PASSWORD", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FINANCIAL_YEAR_ID", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response.flag = reader["FLAG"] != DBNull.Value ? Convert.ToInt32(reader["FLAG"]) : 0;
                                response.Message = reader["MESSAGE"]?.ToString();
                            }

                            if (reader.NextResult()) { }

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    if (response.USER_ID == null && reader["USER_ID"] != DBNull.Value)
                                    {
                                        response.USER_ID = Convert.ToInt32(reader["USER_ID"]);
                                        response.USER_NAME = reader["USER_NAME"]?.ToString();
                                    }

                                    response.Companies.Add(new CompanyList
                                    {
                                        COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                        COMPANY_NAME = reader["COMPANY_NAME"]?.ToString()
                                    });
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