using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Helper;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class UserService:IUserService
    {
        public UserResponse Insert(User user)
        {
            UserResponse res = new UserResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_USERS";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@USER_NAME", user.USER_NAME ?? "");
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", user.LOGIN_NAME ?? "");

                        string encryptedPassword = AzentLibrary.Library.EncryptString(user.PASSWORD ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PASSWORD", encryptedPassword);

                        cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL ?? "");
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", user.WHATSAPP_NO ?? "");
                        cmd.Parameters.AddWithValue("@MOBILE", user.MOBILE ?? "");
                        cmd.Parameters.AddWithValue("@USER_ROLE", user.USER_ROLE);
                        //cmd.Parameters.AddWithValue("@DOB", user.DOB ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", user.IS_INACTIVE);

                        string companyIdsCsv = user.COMPANY_ID != null ? string.Join(",", user.COMPANY_ID) : "";
                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyIdsCsv);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int userId))
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

        public UserResponse Update(UserUpdate user)
        {
            UserResponse res = new UserResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_USERS";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", user.ID);
                        cmd.Parameters.AddWithValue("@USER_NAME", user.USER_NAME ?? "");
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", user.LOGIN_NAME ?? "");

                        string encryptedPassword = AzentLibrary.Library.EncryptString(user.PASSWORD ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PASSWORD", encryptedPassword);
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", user.WHATSAPP_NO ?? "");
                        cmd.Parameters.AddWithValue("@MOBILE", user.MOBILE ?? "");
                        cmd.Parameters.AddWithValue("@USER_ROLE", user.USER_ROLE);
                       // cmd.Parameters.AddWithValue("@DOB", user.DOB ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL ?? "");

                        string companyIds = user.COMPANY_ID != null ? string.Join(",", user.COMPANY_ID) : "";
                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyIds);

                        cmd.Parameters.AddWithValue("@IS_INACTIVE", user.IS_INACTIVE);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int updatedId))
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }


        public UserSelectResponse GetUserById(int id)
        {
            var res = new UserSelectResponse
            {
                Data = new List<UserUpdate>()
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    UserUpdate user = null;
                    using (var cmd = new SqlCommand("SP_TB_USERS", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@USER_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PASSWORD", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MOBILE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@USER_ROLE", DBNull.Value);
                        //cmd.Parameters.AddWithValue("@DOB", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                res.flag = 0;
                                res.Message = "User not found";
                                return res;
                            }

                            while (reader.Read())

                            {
                                if (user == null)
                                {
                                    string encryptedPwd = reader["PASSWORD"]?.ToString() ?? string.Empty;
                                    string decryptedPwd;
                                    try
                                    {
                                        decryptedPwd = AzentLibrary.Library.DecryptString(encryptedPwd);
                                    }
                                    catch
                                    {
                                        decryptedPwd = "[Unable to decrypt]";
                                    }
                                     user = new UserUpdate
                                    {
                                         ID = Convert.ToInt32(reader["ID"]),
                                         USER_NAME = reader["USER_NAME"]?.ToString(),
                                        LOGIN_NAME = reader["LOGIN_NAME"]?.ToString(),
                                        PASSWORD = decryptedPwd,
                                        WHATSAPP_NO = reader["WHATSAPP_NO"]?.ToString(),
                                        MOBILE = reader["MOBILE"]?.ToString(),
                                         USER_ROLE = reader["USER_ROLE"] != DBNull.Value ? Convert.ToInt32(reader["USER_ROLE"]) : 0,
                                        // DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : (DateTime?)null,
                                        EMAIL = reader["EMAIL"]?.ToString(),
                                        IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"]),
                                        COMPANY_ID = reader["COMPANY_ID"]?.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries) .Select(s => int.TryParse(s, out var val) ? val : 0) .Where(id => id > 0) .ToList(),
                                     };
                                }

                                res.Data.Add(user);
                            }

                            res.flag = 1;
                            res.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

        public UserListResponse GetUserList()
        {
            UserListResponse res = new UserListResponse
            {
                Data = new List<UserListData>(),
                flag = 0,
                Message = "No users found"
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_USERS", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@USER_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PASSWORD", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MOBILE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@USER_ROLE", DBNull.Value);
                        //cmd.Parameters.AddWithValue("@DOB", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new UserListData
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    USER_NAME = reader["USER_NAME"]?.ToString() ?? "",
                                    LOGIN_NAME = reader["LOGIN_NAME"]?.ToString() ?? "",
                                    PASSWORD = reader["PASSWORD"]?.ToString() ?? "",
                                    WHATSAPP_NO = reader["WHATSAPP_NO"]?.ToString() ?? "",
                                    MOBILE = reader["MOBILE"]?.ToString() ?? "",
                                    USER_ROLE = reader["USER_ROLE"]?.ToString() ?? "",
                                   // DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : (DateTime?)null,
                                    EMAIL = reader["EMAIL"]?.ToString() ?? "",
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"]),
                                    COMPANY_ID = reader["COMPANY_ID"]?.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.TryParse(s, out var val) ? val : 0).Where(id => id > 0).ToList(),

                                };

                                res.Data.Add(user);
                            }
                        }
                    }

                    if (res.Data.Any())
                    {
                        res.flag = 1;
                        res.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }


        public UserResponse DeleteUserData(int id)
        {
            UserResponse res = new UserResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_USERS";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int deletedId))
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }


    }
}

