using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SalaryHeadService : ISalaryHeadService
    {
        public SalaryHeadListResponse GetAllSalaryHead()
        {
            SalaryHeadListResponse response = new SalaryHeadListResponse();
            response.Data = new List<SalaryHeadUpdate>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_SALARY_HEAD", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    foreach (DataRow dr in tbl.Rows)
                    {
                        SalaryHeadUpdate salaryHead = new SalaryHeadUpdate
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            HEAD_NAME = dr["HEAD_NAME"]?.ToString(),
                            PAYSLIP_TITLE = dr["HEAD_TITLE"]?.ToString(),
                            HEAD_ACTIVE = dr["HEAD_ACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr["HEAD_ACTIVE"]) : (bool?)null,
                            HEAD_TYPE = dr["HEAD_TYPE"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_TYPE"]) : (int?)null,
                            AFFECT_LEAVE = dr["AFFECT_LEAVE"] != DBNull.Value ? Convert.ToBoolean(dr["AFFECT_LEAVE"]) : (bool?)null,
                            HEAD_NATURE = dr["HEAD_NATURE"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_NATURE"]) : (int?)null,
                            FIXED_AMOUNT = dr["HEAD_AMOUNT"] != DBNull.Value ? Convert.ToDouble(dr["HEAD_AMOUNT"]) : (double?)null,
                            HEAD_PERCENT = dr["HEAD_PERCENT"] != DBNull.Value ? Convert.ToDouble(dr["HEAD_PERCENT"]) : (double?)null,
                            RANGE_EXISTS = dr["RANGE_EXISTS"] != DBNull.Value ? Convert.ToBoolean(dr["RANGE_EXISTS"]) : (bool?)null,
                            RANGE_FROM = dr["RANGE_FROM"] != DBNull.Value ? Convert.ToDouble(dr["RANGE_FROM"]) : (double?)null,
                            RANGE_TO = dr["RANGE_TO"] != DBNull.Value ? Convert.ToDouble(dr["RANGE_TO"]) : (double?)null,
                            HEAD_PERCENT_INCLUDE_OT = dr["HEAD_PERCENT_INCLUDE_OT"] != DBNull.Value ? Convert.ToBoolean(dr["HEAD_PERCENT_INCLUDE_OT"]) : (bool?)null,
                            INSTALLMENT_RECOVERY = dr["INSTALLMENT_RECOVERY"] != DBNull.Value ? Convert.ToBoolean(dr["INSTALLMENT_RECOVERY"]) : (bool?)null,
                            IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr["IS_INACTIVE"]) : false,
                            AC_HEAD_ID = dr.Table.Columns.Contains("AC_HEAD_ID") && dr["AC_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(dr["AC_HEAD_ID"]) : (int?)null,
                            HEAD_ORDER = dr["HEAD_ORDER"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_ORDER"]) : (int?)null
                        };

                        response.Data.Add(salaryHead);
                    }

                    response.flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }

        public Int32 SaveData(SalaryHead salaryHead)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_SALARY_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        object DbValue(object? val)
                        {
                            return val == null ? (object)string.Empty : val;
                        }

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@ID", string.Empty);  // Pass empty instead of 0 or null
                        cmd.Parameters.AddWithValue("@HEAD_NAME", salaryHead.HEAD_NAME ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_TITLE", salaryHead.PAYSLIP_TITLE ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_ACTIVE", salaryHead.HEAD_ACTIVE?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_TYPE", salaryHead.HEAD_TYPE?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@AFFECT_LEAVE", salaryHead.AFFECT_LEAVE?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_ORDER", salaryHead.HEAD_ORDER?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_NATURE", salaryHead.HEAD_NATURE?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_AMOUNT", salaryHead.FIXED_AMOUNT?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_PERCENT", salaryHead.HEAD_PERCENT?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@RANGE_EXISTS", salaryHead.RANGE_EXISTS?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@RANGE_FROM", salaryHead.RANGE_FROM?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@RANGE_TO", salaryHead.RANGE_TO?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HEAD_PERCENT_INCLUDE_OT", salaryHead.HEAD_PERCENT_INCLUDE_OT?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@INSTALLMENT_RECOVERY", salaryHead.INSTALLMENT_RECOVERY?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", salaryHead.IS_INACTIVE.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@AC_HEAD_ID", salaryHead.AC_HEAD_ID?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@PERCENT_HEAD_ID", salaryHead.PERCENT_HEAD_ID?.ToString() ?? "");

                        Int32 result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Int32 EditData(SalaryHeadUpdate salaryHead)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_SALARY_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        object GetDbValue(object? val)
                        {
                            return val == null || val.Equals(0) || (val is string s && string.IsNullOrWhiteSpace(s))
                                ? DBNull.Value
                                : val;
                        }

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", salaryHead.ID);
                        cmd.Parameters.AddWithValue("@HEAD_NAME", GetDbValue(salaryHead.HEAD_NAME));
                        cmd.Parameters.AddWithValue("@HEAD_TITLE", GetDbValue(salaryHead.PAYSLIP_TITLE));
                        cmd.Parameters.AddWithValue("@HEAD_ACTIVE", GetDbValue(salaryHead.HEAD_ACTIVE));
                        cmd.Parameters.AddWithValue("@HEAD_TYPE", GetDbValue(salaryHead.HEAD_TYPE));
                        cmd.Parameters.AddWithValue("@AFFECT_LEAVE", GetDbValue(salaryHead.AFFECT_LEAVE));
                        cmd.Parameters.AddWithValue("@HEAD_NATURE", GetDbValue(salaryHead.HEAD_NATURE));
                        cmd.Parameters.AddWithValue("@HEAD_AMOUNT", GetDbValue(salaryHead.FIXED_AMOUNT));
                        cmd.Parameters.AddWithValue("@HEAD_PERCENT", GetDbValue(salaryHead.HEAD_PERCENT));
                        cmd.Parameters.AddWithValue("@RANGE_EXISTS", GetDbValue(salaryHead.RANGE_EXISTS));
                        cmd.Parameters.AddWithValue("@RANGE_FROM", GetDbValue(salaryHead.RANGE_FROM));
                        cmd.Parameters.AddWithValue("@RANGE_TO", GetDbValue(salaryHead.RANGE_TO));
                        cmd.Parameters.AddWithValue("@HEAD_PERCENT_INCLUDE_OT", GetDbValue(salaryHead.HEAD_PERCENT_INCLUDE_OT));
                        cmd.Parameters.AddWithValue("@INSTALLMENT_RECOVERY", GetDbValue(salaryHead.INSTALLMENT_RECOVERY));
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", GetDbValue(salaryHead.IS_INACTIVE));
                        cmd.Parameters.AddWithValue("@AC_HEAD_ID", GetDbValue(salaryHead.AC_HEAD_ID));
                        cmd.Parameters.AddWithValue("@HEAD_ORDER", GetDbValue(salaryHead.HEAD_ORDER));
                        cmd.Parameters.AddWithValue("@PERCENT_HEAD_ID", GetDbValue(salaryHead.PERCENT_HEAD_ID));

                        Int32 result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SalaryHeadUpdate GetItem(int id)
        {
            SalaryHeadUpdate salaryHead = new SalaryHeadUpdate();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_SALARY_HEAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];

                                salaryHead.ID = Convert.ToInt32(dr["ID"]);
                                salaryHead.HEAD_NAME = Convert.ToString(dr["HEAD_NAME"]);
                                salaryHead.PAYSLIP_TITLE = Convert.ToString(dr["HEAD_TITLE"]);
                                salaryHead.HEAD_ACTIVE = dr["HEAD_ACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr["HEAD_ACTIVE"]) : (bool?)null;
                                salaryHead.HEAD_TYPE = dr["HEAD_TYPE"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_TYPE"]) : (int?)null;
                                salaryHead.AFFECT_LEAVE = dr["AFFECT_LEAVE"] != DBNull.Value ? Convert.ToBoolean(dr["AFFECT_LEAVE"]) : (bool?)null;
                                salaryHead.HEAD_NATURE = dr["HEAD_NATURE"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_NATURE"]) : (int?)null;
                                salaryHead.FIXED_AMOUNT = dr["HEAD_AMOUNT"] != DBNull.Value ? Convert.ToDouble(dr["HEAD_AMOUNT"]) : (double?)null;
                                salaryHead.HEAD_PERCENT = dr["HEAD_PERCENT"] != DBNull.Value ? Convert.ToDouble(dr["HEAD_PERCENT"]) : (double?)null;
                                salaryHead.RANGE_EXISTS = dr["RANGE_EXISTS"] != DBNull.Value ? Convert.ToBoolean(dr["RANGE_EXISTS"]) : (bool?)null;
                                salaryHead.RANGE_FROM = dr["RANGE_FROM"] != DBNull.Value ? Convert.ToDouble(dr["RANGE_FROM"]) : (double?)null;
                                salaryHead.RANGE_TO = dr["RANGE_TO"] != DBNull.Value ? Convert.ToDouble(dr["RANGE_TO"]) : (double?)null;
                                salaryHead.HEAD_PERCENT_INCLUDE_OT = dr["HEAD_PERCENT_INCLUDE_OT"] != DBNull.Value ? Convert.ToBoolean(dr["HEAD_PERCENT_INCLUDE_OT"]) : (bool?)null;
                                salaryHead.INSTALLMENT_RECOVERY = dr["INSTALLMENT_RECOVERY"] != DBNull.Value ? Convert.ToBoolean(dr["INSTALLMENT_RECOVERY"]) : (bool?)null;
                                salaryHead.IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr["IS_INACTIVE"]) : false;
                                salaryHead.AC_HEAD_ID = dr["AC_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(dr["AC_HEAD_ID"]) : (int?)null;
                                salaryHead.HEAD_ORDER = dr["HEAD_ORDER"] != DBNull.Value ? Convert.ToInt32(dr["HEAD_ORDER"]) : (int?)null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return salaryHead;
        }


        public bool DeleteSalaryHead(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_SALARY_HEAD";
                    cmd.Parameters.AddWithValue("ACTION", 3); // 3 for Delete
                    cmd.Parameters.AddWithValue("ID", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}