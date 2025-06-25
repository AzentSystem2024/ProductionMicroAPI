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
                        cmd.Parameters.AddWithValue("@p_HeadName", accountHead.HeadName);
                        cmd.Parameters.AddWithValue("@p_GroupID", accountHead.GroupID);
                        cmd.Parameters.AddWithValue("@p_IsDirect", accountHead.IsDirect);
                        cmd.Parameters.AddWithValue("@p_ArabicName", (object)accountHead.ArabicName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_CostTypeID", (object)accountHead.CostTypeID ?? DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                                res.Data = new AccountHeadUpdate
                                {
                                    HeadID = Convert.ToInt32(reader["HeadID"]),
                                    HeadName = accountHead.HeadName,
                                    GroupID = accountHead.GroupID,
                                    IsDirect = accountHead.IsDirect,
                                    ArabicName = accountHead.ArabicName,
                                    IsActive = true,
                                    SerialNo = Convert.ToInt32(reader["SerialNo"]),
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
                        cmd.Parameters.AddWithValue("@p_HeadID", accountHead.HeadID);
                        cmd.Parameters.AddWithValue("@p_HeadName", accountHead.HeadName);
                        cmd.Parameters.AddWithValue("@p_GroupID", accountHead.GroupID);
                        cmd.Parameters.AddWithValue("@p_IsDirect", accountHead.IsDirect);
                        cmd.Parameters.AddWithValue("@p_ArabicName", (object)accountHead.ArabicName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_IsActive", accountHead.IsActive);
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
                        cmd.Parameters.AddWithValue("@p_HeadID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = new AccountHeadUpdate
                                {
                                    HeadID = Convert.ToInt32(reader["HeadID"]),
                                    HeadCode = reader["HeadCode"].ToString(),
                                    HeadName = reader["HeadName"].ToString(),
                                    GroupID = Convert.ToInt32(reader["GroupID"]),
                                    IsDirect = Convert.ToBoolean(reader["IsDirect"]),
                                    ArabicName = reader["ArabicName"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    SerialNo = Convert.ToInt32(reader["SerialNo"]),
                                    //CostTypeID = reader["CostTypeID"] != DBNull.Value ? Convert.ToInt32(reader["CostTypeID"]) : (int?)null,
                                    MainGroupId = reader["MainGroupID"] != DBNull.Value ? Convert.ToInt32(reader["MainGroupID"]) : (int?)null,
                                    SubGroupId = reader["SubGroupID"] != DBNull.Value ? Convert.ToInt32(reader["SubGroupID"]) : (int?)null
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
                        cmd.Parameters.AddWithValue("@p_HeadID", (object)id ?? DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable tbl = new DataTable();
                            tbl.Load(reader);

                            foreach (DataRow dr in tbl.Rows)
                            {
                                lstHead.Add(new AccountHeadUpdate
                                {
                                    HeadID = Convert.ToInt32(dr["HeadID"]),
                                    HeadCode = dr["HeadCode"].ToString(),
                                    HeadName = dr["HeadName"].ToString(),
                                    GroupID = Convert.ToInt32(dr["GroupID"]),
                                    IsDirect = Convert.ToBoolean(dr["IsDirect"]),
                                    ArabicName = dr["ArabicName"].ToString(),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    SerialNo = Convert.ToInt32(dr["SerialNo"]),
                                   // CostTypeID = dr["CostTypeID"] != DBNull.Value ? Convert.ToInt32(dr["CostTypeID"]) : (int?)null,
                                    MainGroupId = dr["MainGroupID"] != DBNull.Value ? Convert.ToInt32(dr["MainGroupID"]) : (int?)null,
                                    SubGroupId = dr["SubGroupID"] != DBNull.Value ? Convert.ToInt32(dr["SubGroupID"]) : (int?)null
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
                        cmd.Parameters.AddWithValue("@p_HeadID", id);

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
