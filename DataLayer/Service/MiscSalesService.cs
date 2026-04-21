using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class MiscSalesService:IMiscSalesService
    {
        public MiscSalesResponse Insert(MiscSales input)
        {
            MiscSalesResponse response = new MiscSalesResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_MISC_SALE_INVOICE_RETAIL", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 🔹 Header Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", input.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", input.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", input.REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", input.CUSTOMER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", input.PARTY_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", input.NARRATION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", input.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", input.VERIFY_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", input.APPROVE1_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", input.APPROVE2_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", input.APPROVE3_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", input.CREATED_STORE_ID ?? 1);

                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", input.GROSS_AMOUNT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", input.TAX_AMOUNT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", input.NET_AMOUNT ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@IS_APPROVED", input.IS_APPROVED == true ? 1 : 0);

                        // 🔹 UDT (Detail Table)
                        DataTable dt = new DataTable();
                        dt.Columns.Add("COMPANY_ID", typeof(int));
                        dt.Columns.Add("STORE_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(int));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("REMARKS", typeof(string));

                        if (input.DETAILS != null && input.DETAILS.Count > 0)
                        {
                            foreach (var item in input.DETAILS)
                            {
                                dt.Rows.Add(
                                    item.COMPANY_ID ?? (object)DBNull.Value,
                                    item.STORE_ID ?? (object)DBNull.Value,
                                    item.AMOUNT ?? 0,
                                    item.TAX_PERC ?? 0,
                                    item.TAX_AMOUNT ?? 0,
                                    item.TOTAL_AMOUNT ?? 0,
                                    item.HEAD_ID ?? (object)DBNull.Value,
                                    item.REMARKS ?? (object)DBNull.Value
                                );
                            }
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_MISC_SALE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;

                        // 🔹 Execute
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        da.Fill(result);

                        if (result.Rows.Count > 0)
                        {
                           
                            response.Message = "Inserted Successfully";
                            response.Flag = 1;
                        }
                        else
                        {
                            response.Flag = 0;
                            response.Message = "Insert Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public MiscSalesResponse GetMiscSalesList(MiscSalesListRequest input)
        {
            MiscSalesResponse response = new MiscSalesResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_MISC_SALE_INVOICE_RETAIL", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", input.DATE_FROM ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DATE_TO", input.DATE_TO ?? (object)DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    // DEBUG
                    if (tbl.Rows.Count == 0)
                    {
                        response.List = new List<MiscSaleList>();
                        response.Flag = 1;
                        response.Message = "No data found";
                        return response;
                    }

                    response.List = tbl.AsEnumerable().Select(dr => new MiscSaleList
                    {
                        TRANS_ID = dr["TRANS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRANS_ID"]),
                        TRANS_TYPE = dr["TRANS_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRANS_TYPE"]),
                        TRANS_STATUS = dr["TRANS_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRANS_STATUS"]),

                        SALE_NO = dr["SALE_NO"]?.ToString(),
                        TRANS_DATE = dr["SALE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["SALE_DATE"]),

                        CUSTOMER_ID = dr["CUSTOMER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CUSTOMER_ID"]),
                        CUST_NAME = dr["CUST_NAME"]?.ToString(),

                        GROSS_AMOUNT = dr["GROSS_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GROSS_AMOUNT"]),
                        TAX_AMOUNT = dr["GST_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GST_AMOUNT"]),
                        NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["NET_AMOUNT"]),

                        REF_NO = dr["REF_NO"]?.ToString()
                    }).ToList();

                    response.Flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.List = new List<MiscSaleList>();
            }

            return response;
        }

        public MiscSalesResponse Update(MiscSales input)
        {
            MiscSalesResponse response = new MiscSalesResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_MISC_SALE_INVOICE_RETAIL", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 🔹 Header Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", input.TRANS_ID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", input.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", input.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", input.REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", input.CUSTOMER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", input.PARTY_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", input.NARRATION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", input.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", input.VERIFY_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", input.APPROVE1_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", input.APPROVE2_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", input.APPROVE3_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", input.CREATED_STORE_ID ?? 1);

                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", input.GROSS_AMOUNT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", input.TAX_AMOUNT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", input.NET_AMOUNT ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@IS_APPROVED", input.IS_APPROVED == true ? 1 : 0);

                        // 🔹 UDT (Detail Table)
                        DataTable dt = new DataTable();
                        dt.Columns.Add("COMPANY_ID", typeof(int));
                        dt.Columns.Add("STORE_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(int));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("REMARKS", typeof(string));

                        if (input.DETAILS != null && input.DETAILS.Count > 0)
                        {
                            foreach (var item in input.DETAILS)
                            {
                                dt.Rows.Add(
                                    item.COMPANY_ID ?? (object)DBNull.Value,
                                    item.STORE_ID ?? (object)DBNull.Value,
                                    item.AMOUNT ?? 0,
                                    item.TAX_PERC ?? 0,
                                    item.TAX_AMOUNT ?? 0,
                                    item.TOTAL_AMOUNT ?? 0,
                                    item.HEAD_ID ?? (object)DBNull.Value,
                                    item.REMARKS ?? (object)DBNull.Value
                                );
                            }
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_MISC_SALE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;

                        // 🔹 Execute
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        da.Fill(result);

                        da.Fill(result);

                        if (result.Rows.Count > 0)
                        {
                            response.Flag = Convert.ToInt32(result.Rows[0]["FLAG"]);
                            response.Message = result.Rows[0]["MESSAGE"].ToString();
                        }
                        else
                        {
                            response.Flag = 0;
                            response.Message = "Update Failed";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public MiscSalesResponse Commit(MiscSales input)
        {
            MiscSalesResponse response = new MiscSalesResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_MISC_SALE_INVOICE_RETAIL", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 🔹 Header Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", input.TRANS_ID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", input.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", input.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", input.REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", input.CUSTOMER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", input.PARTY_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", input.NARRATION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", input.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", input.VERIFY_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", input.APPROVE1_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", input.APPROVE2_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", input.APPROVE3_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", input.CREATED_STORE_ID ?? 1);

                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", input.GROSS_AMOUNT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", input.TAX_AMOUNT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", input.NET_AMOUNT ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@IS_APPROVED", input.IS_APPROVED == true ? 1 : 0);

                        // 🔹 UDT (Detail Table)
                        DataTable dt = new DataTable();
                        dt.Columns.Add("COMPANY_ID", typeof(int));
                        dt.Columns.Add("STORE_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(int));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("REMARKS", typeof(string));

                        if (input.DETAILS != null && input.DETAILS.Count > 0)
                        {
                            foreach (var item in input.DETAILS)
                            {
                                dt.Rows.Add(
                                    item.COMPANY_ID ?? (object)DBNull.Value,
                                    item.STORE_ID ?? (object)DBNull.Value,
                                    item.AMOUNT ?? 0,
                                    item.TAX_PERC ?? 0,
                                    item.TAX_AMOUNT ?? 0,
                                    item.TOTAL_AMOUNT ?? 0,
                                    item.HEAD_ID ?? (object)DBNull.Value,
                                    item.REMARKS ?? (object)DBNull.Value
                                );
                            }
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_MISC_SALE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;

                        // 🔹 Execute
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        da.Fill(result);

                        if (result.Rows.Count > 0)
                        {

                            response.Message = "Committed Successfully";
                            response.Flag = 1;
                        }
                        else
                        {
                            response.Flag = 0;
                            response.Message = "Commit Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public MiscSalesViewResponse GetMiscSalesById(int Id)
        {
            var response = new MiscSalesViewResponse
            {
                Flag = 0,
                Message = "Failed",
                Data = null
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_MISC_SALE_INVOICE_RETAIL", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", Id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            MiscSalesView header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new MiscSalesView
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        TRANS_TYPE = Convert.ToInt32(reader["TRANS_TYPE"]),
                                        STORE_ID = ADO.ToInt32(reader["STORE_ID"]),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        SALE_NO = reader["SALE_NO"]?.ToString(),
                                        TRANS_DATE = reader["SALE_DATE"] != DBNull.Value
                                                        ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy")
                                                        : null,

                                        CUSTOMER_ID = reader["CUSTOMER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CUSTOMER_ID"]),
                                        CUST_NAME = reader["CUST_NAME"]?.ToString(),

                                        GROSS_AMOUNT = reader["GROSS_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["GROSS_AMOUNT"]),
                                        TAX_AMOUNT = reader["GST_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["GST_AMOUNT"]),
                                        NET_AMOUNT = reader["NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["NET_AMOUNT"]),

                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),

                                        Details = new List<MiscSalesItem>()
                                    };
                                }

                                header.Details.Add(new MiscSalesItem
                                {
                                    AMOUNT = reader["TAXABLE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TAXABLE_AMOUNT"]),
                                    TAX_PERC = reader["TAX_PERC"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TAX_PERC"]),
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TAX_AMOUNT"]),
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TOTAL_AMOUNT"]),
                                    HEAD_ID = reader["HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["HEAD_ID"]),
                                    REMARKS = reader["REMARKS"] == DBNull.Value ? null : Convert.ToString(reader["REMARKS"]),
                                    COMPANY_ID = ADO.ToInt32(reader["COMPANY_ID"]),
                                    STORE_ID = ADO.ToInt32(reader["STORE_ID"]),
                                });
                            }

                            response.Data = header;
                        }

                        response.Flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }

        public MiscSalesResponse Delete(int id)
        {
            MiscSalesResponse res = new MiscSalesResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_MISC_SALE_INVOICE_RETAIL";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
