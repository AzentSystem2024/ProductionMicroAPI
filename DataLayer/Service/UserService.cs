using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Helper;

namespace MicroApi.DataLayer.Service
{
    public class UserService
    {
        public UserResponse Insert(User user)
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

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@USER_NAME", user.USER_NAME ?? "");
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", user.LOGIN_NAME ?? "");
                        cmd.Parameters.AddWithValue("@PASSWORD", user.PASSWORD ?? "");
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", user.WHATSAPP_NO ?? "");
                        cmd.Parameters.AddWithValue("@MOBILE", user.MOBILE ??"");
                        cmd.Parameters.AddWithValue("@USER_LEVEL", user.USER_LEVEL ?? "");
                        cmd.Parameters.AddWithValue("@DOB", user.DOB ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL ?? "");
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", user.IS_INACTIVE);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int userId))
                        {
                            res.flag = 1;
                            res.Message = "Success";
                            res.UserId = userId;
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
                        cmd.Parameters.AddWithValue("@PASSWORD", user.PASSWORD ?? "");
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", user.WHATSAPP_NO ?? "");
                        cmd.Parameters.AddWithValue("@MOBILE", user.MOBILE ?? "");
                        cmd.Parameters.AddWithValue("@USER_LEVEL", user.USER_LEVEL ?? "");
                        cmd.Parameters.AddWithValue("@DOB", user.DOB ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL ?? "");
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", user.IS_INACTIVE);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int updatedId))
                        {
                            res.flag = 1;
                            res.Message = "Success";
                            res.UserId = updatedId;
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
                        cmd.Parameters.AddWithValue("@USER_LEVEL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOB", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
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
                                UserUpdate user = new UserUpdate
                                {
                                    USER_NAME = reader["USER_NAME"]?.ToString(),
                                    LOGIN_NAME = reader["LOGIN_NAME"]?.ToString(),
                                    PASSWORD = reader["PASSWORD"]?.ToString(),
                                    WHATSAPP_NO = reader["WHATSAPP_NO"]?.ToString(),
                                    MOBILE = reader["MOBILE"]?.ToString(),
                                    USER_LEVEL = reader["USER_LEVEL"]?.ToString(),
                                    DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : (DateTime?)null,
                                    EMAIL = reader["EMAIL"]?.ToString(),
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"]),
                                    Companies = new List<UserCompanyMapping>() 
                                };

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

        public UserListResponse GetLogList(int? id = null)
        {
            UserListResponse res = new UserListResponse();
            List<UserListData> users = new List<UserListData>();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@USER_NAME", DBNull.Value);
                    cmd.Parameters.AddWithValue("@LOGIN_NAME", DBNull.Value);
                    cmd.Parameters.AddWithValue("@PASSWORD", DBNull.Value);
                    cmd.Parameters.AddWithValue("@WHATSAPP_NO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOBILE", DBNull.Value);
                    cmd.Parameters.AddWithValue("@USER_LEVEL", DBNull.Value);
                    cmd.Parameters.AddWithValue("@DOB", DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserListData user = new UserListData
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                USER_NAME = reader["USER_NAME"]?.ToString() ?? "",
                                LOGIN_NAME = reader["LOGIN_NAME"]?.ToString() ?? "",
                                PASSWORD = reader["PASSWORD"]?.ToString() ?? "",
                                WHATSAPP_NO = reader["WHATSAPP_NO"]?.ToString() ?? "",
                                MOBILE = reader["MOBILE"]?.ToString() ?? "",
                                USER_LEVEL = reader["USER_LEVEL"]?.ToString() ?? "",
                                DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : (DateTime?)null,
                                EMAIL = reader["EMAIL"]?.ToString() ?? "",
                                IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"])
                            };

                            users.Add(user);
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = users;
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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
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

