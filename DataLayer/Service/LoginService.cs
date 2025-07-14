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
                            // Result 1: FLAG + MESSAGE
                            if (reader.Read())
                            {
                                response.flag = reader["FLAG"] != DBNull.Value ? Convert.ToInt32(reader["FLAG"]) : 0;
                                response.Message = reader["MESSAGE"]?.ToString();
                            }

                            if (response.flag == 1)
                            {
                                // Result 2: USER INFO
                                if (reader.NextResult() && reader.Read())
                                {
                                    response.USER_ID = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : (int?)null;
                                    response.USER_NAME = reader["USER_NAME"]?.ToString();
                                }

                                // Result 3: Assigned Companies
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

                                // Result 4: Selected Company
                                if (reader.NextResult() && reader.Read())
                                {
                                    response.SELECTED_COMPANY = new CompanyList
                                    {
                                        COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                        COMPANY_NAME = reader["COMPANY_NAME"]?.ToString()
                                    };
                                }

                                // Result 5: Menu Permissions
                                if (reader.NextResult())
                                {
                                    Dictionary<int, MenuGroup> menuGroups = new Dictionary<int, MenuGroup>();

                                    while (reader.Read())
                                    {
                                        int menuGroupId = reader["MenuGroupID"] != DBNull.Value ? Convert.ToInt32(reader["MenuGroupID"]) : 0;

                                        if (!menuGroups.ContainsKey(menuGroupId))
                                        {
                                            menuGroups[menuGroupId] = new MenuGroup
                                            {
                                                MenuGroupID = menuGroupId,
                                                Text = reader["Text"]?.ToString(),
                                                Icon = reader["Icon"]?.ToString(),
                                                MenuGroupOrder = reader["MenuGroupOrder"] != DBNull.Value ? Convert.ToDecimal(reader["MenuGroupOrder"]) : 0,
                                                Menus = new List<Menu>()
                                            };
                                        }

                                        MenuGroup group = menuGroups[menuGroupId];

                                        group.Menus.Add(new Menu
                                        {
                                            MenuID = reader["MenuID"] != DBNull.Value ? Convert.ToInt32(reader["MenuID"]) : 0,
                                            MenuName = reader["MenuName"]?.ToString(),
                                            MenuOrder = reader["MenuOrder"] != DBNull.Value ? Convert.ToDecimal(reader["MenuOrder"]) : 0,
                                            Selected = reader["Selected"] != DBNull.Value && Convert.ToBoolean(reader["Selected"]),
                                            CanAdd = reader["CanAdd"] != DBNull.Value && Convert.ToBoolean(reader["CanAdd"]),
                                            CanView = reader["CanView"] != DBNull.Value && Convert.ToBoolean(reader["CanView"]),
                                            CanEdit = reader["CanEdit"] != DBNull.Value && Convert.ToBoolean(reader["CanEdit"]),
                                            CanApprove = reader["CanApprove"] != DBNull.Value && Convert.ToBoolean(reader["CanApprove"]),
                                            CanDelete = reader["CanDelete"] != DBNull.Value && Convert.ToBoolean(reader["CanDelete"]),
                                            CanPrint = reader["CanPrint"] != DBNull.Value && Convert.ToBoolean(reader["CanPrint"]),
                                            Path = reader["Path"]?.ToString()
                                        });
                                    }

                                    response.MenuGroups = menuGroups.Values.ToList();
                                }

                                // Result 6: Financial Years
                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        response.FINANCIAL_YEARS.Add(new FinancialYear
                                        {
                                            FIN_ID = reader["FIN_ID"] != DBNull.Value ? Convert.ToInt32(reader["FIN_ID"]) : 0,
                                            FIN_CODE = reader["FIN_CODE"]?.ToString(),
                                            DATE_FROM = reader["DATE_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["DATE_FROM"]) : DateTime.MinValue,
                                            DATE_TO = reader["DATE_TO"] != DBNull.Value ? Convert.ToDateTime(reader["DATE_TO"]) : DateTime.MinValue,
                                            IS_CLOSED = reader["IS_CLOSED"] != DBNull.Value && Convert.ToBoolean(reader["IS_CLOSED"])
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
                            // First result: FLAG and MESSAGE
                            if (reader.Read())
                            {
                                response.flag = reader["FLAG"] != DBNull.Value ? Convert.ToInt32(reader["FLAG"]) : 0;
                                response.Message = reader["MESSAGE"]?.ToString();
                            }

                            // Skip second result (USER INFO)
                            if (reader.NextResult()) { }

                            // Third result: company list
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    // ✅ Correct check for nullable int (int?)
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