using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace MicroApi.DataLayer.Service
{
    public class AccountHeadService : IAccountHeadService
    {
        public AccountHeadResponse Insert(AccountHead accountHead)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@HEAD_NAME", accountHead.HEAD_NAME);
                        cmd.Parameters.AddWithValue("@GROUP_ID", accountHead.GROUP_ID);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", accountHead.IS_DIRECT);
                        cmd.Parameters.AddWithValue("@ARABIC_NAME", (object)accountHead.ARABIC_NAME ?? DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                                res.Data = new AccountHeadUpdate
                                {
                                    HEAD_ID = Convert.ToInt32(reader["HEAD_ID"]),
                                    HEAD_NAME = accountHead.HEAD_NAME,
                                    GROUP_ID = accountHead.GROUP_ID,
                                    IS_DIRECT = accountHead.IS_DIRECT,
                                    ARABIC_NAME = accountHead.ARABIC_NAME,
                                    IS_ACTIVE = true,
                                    SERIAL_NO = Convert.ToInt32(reader["SERIAL_NO"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }


        public AccountHeadResponse Update(AccountHeadUpdate accountHead)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@HEAD_ID", accountHead.HEAD_ID);
                        cmd.Parameters.AddWithValue("@HEAD_CODE", accountHead.HEAD_CODE);
                        cmd.Parameters.AddWithValue("@HEAD_NAME", accountHead.HEAD_NAME);
                        cmd.Parameters.AddWithValue("@GROUP_ID", accountHead.GROUP_ID);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", accountHead.IS_DIRECT);
                        cmd.Parameters.AddWithValue("@ARABIC_NAME", accountHead.ARABIC_NAME);
                        cmd.Parameters.AddWithValue("@IS_ACTIVE", accountHead.IS_ACTIVE);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        public AccountHeadResponse GetAccountHeadById(int id)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@HEAD_ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = new AccountHeadUpdate
                                {
                                    HEAD_ID = Convert.ToInt32(reader["HEAD_ID"]),
                                    HEAD_CODE = reader["HEAD_CODE"].ToString(),
                                    HEAD_NAME = reader["HEAD_NAME"].ToString(),
                                    GROUP_ID = Convert.ToInt32(reader["GROUP_ID"]),
                                    IS_DIRECT = Convert.ToBoolean(reader["IS_DIRECT"]),
                                    ARABIC_NAME = reader["ARABIC_NAME"].ToString(),
                                    IS_ACTIVE = Convert.ToBoolean(reader["IS_ACTIVE"]),
                                    SERIAL_NO = Convert.ToInt32(reader["SERIAL_NO"]),

                                    MainGroupId = reader["MAIN_GROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["MAIN_GROUP_ID"]) : (int?)null,
                                    SubGroupId = reader["SUB_GROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUB_GROUP_ID"]) : (int?)null
                                };
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = $"This Head is not found for ID={id}";
                                res.Data = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }
            return res;
        }

        public AccountHeadListResponse GetLogList(int? id = null)
        {
            AccountHeadListResponse res = new AccountHeadListResponse();
            List<AccountHeadUpdate> LstHead = new List<AccountHeadUpdate>();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@HEAD_ID", (object)id ?? DBNull.Value);
                       

                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable tbl = new DataTable();
                            tbl.Load(reader);
                            foreach (DataRow dr in tbl.Rows)
                            {
                                LstHead.Add(new AccountHeadUpdate
                                {
                                    HEAD_ID = Convert.ToInt32(dr["HEAD_ID"]),
                                    HEAD_CODE = dr["HEAD_CODE"].ToString(),
                                    HEAD_NAME = dr["HEAD_NAME"].ToString(),
                                    GROUP_ID = Convert.ToInt32(dr["GROUP_ID"]),
                                    IS_DIRECT = Convert.ToBoolean(dr["IS_DIRECT"]),
                                    ARABIC_NAME = dr["ARABIC_NAME"].ToString(),
                                    IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]),
                                    SERIAL_NO = Convert.ToInt32(dr["SERIAL_NO"]),
                                    MainGroupId = dr["MAIN_GROUP_ID"] != DBNull.Value ? Convert.ToInt32(dr["MAIN_GROUP_ID"]) : (int?)null,
                                    SubGroupId = dr["SUB_GROUP_ID"] != DBNull.Value ? Convert.ToInt32(dr["SUB_GROUP_ID"]) : (int?)null
                                });
                            }
                            res.flag = 1;
                            res.Message = "Success";
                            res.Data = LstHead;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        public AccountHeadResponse DeleteAccountHeadData(int id)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@HEAD_ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
