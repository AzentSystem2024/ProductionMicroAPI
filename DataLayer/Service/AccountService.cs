using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.DataLayer.Service
{
    public class AccountService : IAccountService
    {
        public AccountResponse Insert(Account account)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_GROUP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 1);
                        cmd.Parameters.AddWithValue("@p_GroupName", account.GroupName);
                        cmd.Parameters.AddWithValue("@p_GroupSuperID", account.GroupSuperID);
                        cmd.Parameters.AddWithValue("@p_GroupType", account.GroupType);
                        cmd.Parameters.AddWithValue("@p_GroupLevel", account.GroupLevel);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                                res.Data = new AccountUpdate
                                {
                                    GroupID = Convert.ToInt32(reader["GroupID"]),
                                    GroupCode = reader["GroupCode"].ToString(),
                                    GroupName = account.GroupName,
                                    SerialNo = Convert.ToInt32(reader["SerialNo"])
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

        public AccountResponse Update(AccountUpdate account)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_GROUP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 2);
                        cmd.Parameters.AddWithValue("@p_GroupID", account.GroupID);
                        cmd.Parameters.AddWithValue("@p_GroupName", account.GroupName);
                        cmd.Parameters.AddWithValue("@p_ArabicName", account.ArabicName);

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

        public AccountResponse GetAccountById(int id)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_GROUP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 0);
                        cmd.Parameters.AddWithValue("@p_GroupID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new AccountUpdate
                                {
                                    GroupID = Convert.ToInt32(reader["GroupID"]),
                                    GroupName = reader["GroupName"].ToString(),
                                    ArabicName = reader["ArabicName"].ToString(),
                                    IsInactive = false
                                };
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = $"This Group is not found for ID = {id}";
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

        public AccountListResponse GetLogList(int? id = null)
        {
            AccountListResponse res = new AccountListResponse();
            List<AccountUpdate> result = new List<AccountUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_AC_GROUP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_ACTION", 0);
                cmd.Parameters.AddWithValue("@p_GroupID", (object)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@p_GroupName", DBNull.Value);
                cmd.Parameters.AddWithValue("@p_ArabicName", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            result.Add(new AccountUpdate
                            {
                                GroupID = Convert.ToInt32(dr["GroupID"]),
                                GroupCode = dr["GroupCode"].ToString(),
                                GroupName = dr["GroupName"].ToString(),
                                ArabicName = dr["ArabicName"].ToString(),
                                IsInactive = false
                            });
                        }
                        res.flag = 1;
                        res.Message = "Success";
                        res.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }
            return res;
        }

        public AccountResponse DeleteAccountData(int id)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_GROUP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ACTION", 3);
                        cmd.Parameters.AddWithValue("@p_GroupID", id);

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
