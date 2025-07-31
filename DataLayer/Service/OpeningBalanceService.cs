using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Reflection;

namespace MicroApi.DataLayer.Service
{
    public class OpeningBalanceService:IOpeningBalanceService
    {
        public OBResponse GetopeningBalance(OBRequest request)
        {
            OBResponse res = new OBResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_AC_OPENING_BALANCE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            var openingBalances = new List<OpeningBalanceSelect>();

                            while (reader.Read())
                            {
                                openingBalances.Add(new OpeningBalanceSelect
                                {
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : null,
                                    LEDGER_CODE = reader["LEDGER_CODE"]?.ToString(),
                                    LEDGER_NAME = reader["LEDGER_NAME"]?.ToString(),
                                    DEBIT_AMOUNT = reader["DEBIT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DEBIT_AMOUNT"]) : (decimal?)null,
                                    CREDIT_AMOUNT = reader["CREDIT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["CREDIT_AMOUNT"]) : (decimal?)null,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : null,
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0
                                });
                            }

                            if (openingBalances.Any())
                            {
                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = openingBalances;
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "No found";
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


        public OBResponse Insert(AcOpeningBalanceInsertRequest request)
        {
            OBResponse response = new OBResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_AC_OPENING_BALANCE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Main parameters
                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);

                    // Prepare UDT
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("TRANS_ID", typeof(int));
                    dt.Columns.Add("COMPANY_ID", typeof(int));
                    dt.Columns.Add("STORE_ID", typeof(int));
                    dt.Columns.Add("SL_NO", typeof(int));
                    dt.Columns.Add("HEAD_ID", typeof(int));
                    dt.Columns.Add("DR_AMOUNT", typeof(decimal));
                    dt.Columns.Add("CR_AMOUNT", typeof(decimal));
                    dt.Columns.Add("REMARKS", typeof(string));
                    dt.Columns.Add("OPP_HEAD_ID", typeof(int));
                    dt.Columns.Add("OPP_HEAD_NAME", typeof(string));
                    dt.Columns.Add("BILL_NO", typeof(string));
                    dt.Columns.Add("JOB_ID", typeof(int));
                    dt.Columns.Add("CREATED_STORE_ID", typeof(int));
                    dt.Columns.Add("STORE_AUTO_ID", typeof(int));

                    int slno = 1;

                    foreach (var item in request.Details)
                    {
                        dt.Rows.Add(DBNull.Value,
                            0,
                            0,
                            item.STORE_ID,
                            slno++,
                            item.HEAD_ID,
                            item.DR_AMOUNT,
                            item.CR_AMOUNT,
                            item.REMARKS ?? string.Empty,
                            item.OPP_HEAD_ID,
                            item.OPP_HEAD_NAME ?? string.Empty,
                            item.BILL_NO ?? string.Empty,
                            item.JOB_ID,
                            item.CREATED_STORE_ID,
                            item.STORE_AUTO_ID
                        );
                    }

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_TRANS_DETAIL", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    // Execute
                    cmd.ExecuteNonQuery();

                    // Assume success if no exception
                    response.flag = 1;
                    response.Message = "Success.";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error inserting opening balance: " + ex.Message;
            }

            return response;
        }

        public OBResponse Commit(OBCommitRequest request)
        {
            OBResponse response = new OBResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_AC_OPENING_BALANCE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Main parameters
                    cmd.Parameters.AddWithValue("@ACTION", 2); 
                    cmd.Parameters.AddWithValue("@TRANS_ID", request.TRANS_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);

                    // Prepare UDT
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("TRANS_ID", typeof(int));
                    dt.Columns.Add("COMPANY_ID", typeof(int));
                    dt.Columns.Add("STORE_ID", typeof(int));
                    dt.Columns.Add("SL_NO", typeof(int));
                    dt.Columns.Add("HEAD_ID", typeof(int));
                    dt.Columns.Add("DR_AMOUNT", typeof(decimal));
                    dt.Columns.Add("CR_AMOUNT", typeof(decimal));
                    dt.Columns.Add("REMARKS", typeof(string));
                    dt.Columns.Add("OPP_HEAD_ID", typeof(int));
                    dt.Columns.Add("OPP_HEAD_NAME", typeof(string));
                    dt.Columns.Add("BILL_NO", typeof(string));
                    dt.Columns.Add("JOB_ID", typeof(int));
                    dt.Columns.Add("CREATED_STORE_ID", typeof(int));
                    dt.Columns.Add("STORE_AUTO_ID", typeof(int));

                    int slno = 1;
                    foreach (var item in request.Details)
                    {
                        dt.Rows.Add(DBNull.Value,
                            request.TRANS_ID,
                            request.COMPANY_ID,
                            item.STORE_ID,
                            slno++,
                            item.HEAD_ID,
                            item.DR_AMOUNT,
                            item.CR_AMOUNT,
                            item.REMARKS ?? string.Empty,
                            item.OPP_HEAD_ID,
                            item.OPP_HEAD_NAME ?? string.Empty,
                            item.BILL_NO ?? string.Empty,
                            item.JOB_ID,
                            item.CREATED_STORE_ID,
                            item.STORE_AUTO_ID
                        );
                    }

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_TRANS_DETAIL", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    cmd.ExecuteNonQuery();

                    response.flag = 1;
                    response.Message = "Commit successful.";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error committing opening balance: " + ex.Message;
            }

            return response;
        }


    }
}
