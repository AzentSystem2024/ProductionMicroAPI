using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class PayTimeEntryService : IPayTimeEntryService
    {
        public PayTimeResponse Save(PayTimeEntryInsert request)
        {
            PayTimeResponse res = new PayTimeResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_PAYTIME_ENTRY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add action & params
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@SAL_MONTH", request.SAL_MONTH);
                        cmd.Parameters.AddWithValue("@HEAD_ID", request.HEAD_ID);

                        // Table-valued parameter
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("EMP_ID", typeof(int));
                        tvp.Columns.Add("AMOUNT", typeof(float));
                        tvp.Columns.Add("DAYS", typeof(int));

                        foreach (var item in request.PAY_ENTRIES)
                        {
                            tvp.Rows.Add(item.EMP_ID, item.AMOUNT, item.DAYS ?? 0);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_PAYTIME_ENTRY", tvp);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_PAYTIME_ENTRY";

                        cmd.ExecuteNonQuery();

                        res.flag = 1;
                        res.message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 1;
                res.message = "Error: " + ex.Message;
            }

            return res;
        }

        public PayTimeSelectResponse GetPayTimeEntry(PayTimeEntryRequest request)
        {
            PayTimeSelectResponse res = new PayTimeSelectResponse();
            res.data = new List<PayTimeEntryResult>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_PAYTIME_ENTRY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0); // SELECT case
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@HEAD_ID", request.HEAD_ID);
                        cmd.Parameters.AddWithValue("@SAL_MONTH", request.SAL_MONTH);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new PayTimeEntryResult
                                {
                                    EMP_ID = Convert.ToInt32(reader["ID"]),
                                    EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                    EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                    DAYS = reader["DAYS"] != DBNull.Value ? Convert.ToInt32(reader["DAYS"]) : (int?)null,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : (float?)null
                                });
                            }
                        }
                    }

                    if (res.data.Any())
                    {
                        res.flag = 1;
                        res.message = "Success";
                    }
                    else
                    {
                        res.flag = 0;
                        res.message = "No data found";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
                res.data = null;
            }

            return res;
        }


    }
}
