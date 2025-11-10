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
                        cmd.Parameters.AddWithValue("@p_GROUP_NAME", account.GROUP_NAME);
                        cmd.Parameters.AddWithValue("@p_GROUP_SUPER_ID", account.GROUP_SUPER_ID);
                        cmd.Parameters.AddWithValue("@p_GROUP_TYPE", account.GROUP_TYPE);
                        cmd.Parameters.AddWithValue("@p_GROUP_LEVEL", account.GROUP_LEVEL);


                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                                res.Data = new AccountUpdate
                                {
                                    GROUP_ID = Convert.ToInt32(reader["GROUP_ID"]),
                                    GROUP_CODE = reader["GROUP_CODE"].ToString(),
                                    GROUP_NAME = account.GROUP_NAME,
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
                        cmd.Parameters.AddWithValue("@p_GROUP_ID", account.GROUP_ID);
                        cmd.Parameters.AddWithValue("@p_GROUP_NAME", account.GROUP_NAME);
                        cmd.Parameters.AddWithValue("@p_ARABIC_NAME", account.ARABIC_NAME);

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
                        cmd.Parameters.AddWithValue("@p_GROUP_ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new AccountUpdate
                                {
                                    GROUP_ID = Convert.ToInt32(reader["GROUP_ID"]),
                                    GROUP_CODE = reader["GROUP_CODE"].ToString(),
                                    GROUP_NAME = reader["GROUP_NAME"].ToString(),
                                    ARABIC_NAME = reader["ARABIC_NAME"].ToString(),
                                    IS_INACTIVE = false
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
                cmd.Parameters.AddWithValue("@p_GROUP_ID", (object)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@p_GROUP_NAME", DBNull.Value);
                cmd.Parameters.AddWithValue("@p_ARABIC_NAME", DBNull.Value);

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
                                GROUP_ID = Convert.ToInt32(dr["GROUP_ID"]),
                                GROUP_CODE = dr["GROUP_CODE"].ToString(),
                                GROUP_NAME = dr["GROUP_NAME"].ToString(),
                                ARABIC_NAME = dr["ARABIC_NAME"].ToString(),
                                IS_INACTIVE = false
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
                        cmd.Parameters.AddWithValue("@p_GROUP_ID", id);

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
