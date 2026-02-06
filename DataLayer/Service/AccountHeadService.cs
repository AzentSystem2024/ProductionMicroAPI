using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

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
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 1);
                        cmd.Parameters.AddWithValue("@p_HEAD_NAME", accountHead.HEAD_NAME);
                        cmd.Parameters.AddWithValue("@p_GROUP_ID", accountHead.GROUP_ID);
                        cmd.Parameters.AddWithValue("@p_IS_DIRECT", accountHead.IS_DIRECT);
                        cmd.Parameters.AddWithValue("@p_ARABIC_NAME", (object)accountHead.ARABIC_NAME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_IS_INACTIVE", accountHead.IS_INACTIVE);
                        // cmd.Parameters.AddWithValue("@p_CostTypeID", (object)accountHead.CostTypeID ?? DBNull.Value);

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
                                    IS_INACTIVE = true,
                                    SERIAL_NO = Convert.ToInt32(reader["SERIAL_NO"]),
                                   // CostTypeID = accountHead.CostTypeID
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
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 2);
                        cmd.Parameters.AddWithValue("@p_HEAD_ID", accountHead.HEAD_ID);
                        cmd.Parameters.AddWithValue("@p_HEAD_NAME", accountHead.HEAD_NAME);
                        cmd.Parameters.AddWithValue("@p_GROUP_ID", accountHead.GROUP_ID);
                        cmd.Parameters.AddWithValue("@p_IS_DIRECT", accountHead.IS_DIRECT);
                        cmd.Parameters.AddWithValue("@p_ARABIC_NAME", (object)accountHead.ARABIC_NAME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_IS_INACTIVE", accountHead.IS_INACTIVE);
                       // cmd.Parameters.AddWithValue("@p_CostTypeID", (object)accountHead.CostTypeID ?? DBNull.Value);

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
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 0);
                        cmd.Parameters.AddWithValue("@p_HEAD_ID", id);

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
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"]),
                                    SERIAL_NO = Convert.ToInt32(reader["SERIAL_NO"]),
                                    //CostTypeID = reader["CostTypeID"] != DBNull.Value ? Convert.ToInt32(reader["CostTypeID"]) : (int?)null,
                                    MAIN_GROUP_ID = reader["MainGROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["MainGROUP_ID"]) : (int?)null,
                                    SUB_GROUP_ID = reader["SubGROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SubGROUP_ID"]) : (int?)null
                                };
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = $"This Head is not found for ID = {id}";
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
            List<AccountHeadUpdate> lstHead = new List<AccountHeadUpdate>();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 0);
                        cmd.Parameters.AddWithValue("@p_HEAD_ID", (object)id ?? DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable tbl = new DataTable();
                            tbl.Load(reader);

                            foreach (DataRow dr in tbl.Rows)
                            {
                                lstHead.Add(new AccountHeadUpdate
                                {
                                    HEAD_ID = dr["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_ID"]) : 0,
                                    HEAD_CODE = dr["HEAD_CODE"]?.ToString(),
                                    HEAD_NAME = dr["HEAD_NAME"]?.ToString(),

                                    GROUP_ID = dr["GROUP_ID"] != DBNull.Value ? Convert.ToInt32(dr["GROUP_ID"]) : (int?)null,
                                    IS_DIRECT = dr["IS_DIRECT"] != DBNull.Value ? Convert.ToBoolean(dr["IS_DIRECT"]) : (bool?)null,
                                    ARABIC_NAME = dr["ARABIC_NAME"]?.ToString(),

                                    IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr["IS_INACTIVE"]) : (bool?)null,
                                    SERIAL_NO = dr["SERIAL_NO"] != DBNull.Value ? Convert.ToInt32(dr["SERIAL_NO"]) : (int?)null,

                                    MAIN_GROUP_ID = dr["MainGROUP_ID"] != DBNull.Value ? Convert.ToInt32(dr["MainGROUP_ID"]) : (int?)null,
                                    SUB_GROUP_ID = dr["SubGROUP_ID"] != DBNull.Value ? Convert.ToInt32(dr["SubGROUP_ID"]) : (int?)null
                                });

                            }
                            res.flag = 1;
                            res.Message = "Success";
                            res.Data = lstHead;
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
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 3);
                        cmd.Parameters.AddWithValue("@p_HEAD_ID", id);

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
        public AccountHeadListResponse GetList()
        {
            AccountHeadListResponse res = new AccountHeadListResponse();
            List<AccountHeadUpdate> lstHead = new List<AccountHeadUpdate>();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 4);

                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable tbl = new DataTable();
                            tbl.Load(reader);

                            foreach (DataRow dr in tbl.Rows)
                            {
                                lstHead.Add(new AccountHeadUpdate
                                {
                                    HEAD_ID = dr["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_ID"]) : 0,
                                    HEAD_CODE = dr["HEAD_CODE"]?.ToString(),
                                    HEAD_NAME = dr["HEAD_NAME"]?.ToString(),
                                    GROUP_ID = dr["GROUP_ID"] != DBNull.Value ? Convert.ToInt32(dr["GROUP_ID"]) : 0,
                                    IS_DIRECT = dr["IS_DIRECT"] != DBNull.Value && Convert.ToBoolean(dr["IS_DIRECT"]),
                                    ARABIC_NAME = dr["ARABIC_NAME"]?.ToString(),
                                    IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(dr["IS_INACTIVE"]),
                                    SERIAL_NO = dr["SERIAL_NO"] != DBNull.Value ? Convert.ToInt32(dr["SERIAL_NO"]) : 0,

                                    MAIN_GROUP_ID = dr["MainGROUP_ID"] != DBNull.Value
                                     ? Convert.ToInt32(dr["MainGROUP_ID"])
                                     : (int?)null,

                                    SUB_GROUP_ID = dr["SubGROUP_ID"] != DBNull.Value
                                     ? Convert.ToInt32(dr["SubGROUP_ID"])
                                     : (int?)null
                                });

                            }
                            res.flag = 1;
                            res.Message = "Success";
                            res.Data = lstHead;
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
