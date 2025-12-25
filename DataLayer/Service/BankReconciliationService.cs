using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class BankReconciliationService:IBankReconciliationService
    {
        public BankReconciliationReportResponse GetBankReconciliationReport(BankReconciliation request)
        {
            BankReconciliationReportResponse response = new BankReconciliationReportResponse
            {
                Data = new List<BankReconciliationReport>()
            };

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_BANK_RECONCILIATION", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HEAD_ID", request.HEAD_ID);
                        cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BankReconciliationReport report = new BankReconciliationReport
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                    TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]) : (DateTime?)null,
                                    REF_NO = reader["REF_NO"]?.ToString(),
                                    PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                    DR_AMOUNT = reader["DR_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DR_AMOUNT"]) : 0,
                                    CR_AMOUNT = reader["CR_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["CR_AMOUNT"]) : 0,
                                    RUNNING_BALANCE = reader["OPENING_BALANCE"] != DBNull.Value ? Convert.ToDecimal(reader["OPENING_BALANCE"]) : 0
                                };
                                response.Data.Add(report);
                            }
                        }
                    }
                }

                response.flag = response.Data.Count > 0 ? 1 : 0;
                response.message = response.flag == 1 ? "Success" : "No records found";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = "Error: " + ex.Message;
            }

            return response;
        }
        public BankReconciliationSaveResponse SaveBankReconciliation(BankReconciliationInput request)
        {
            var response = new BankReconciliationSaveResponse();
            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    using (var cmd = new SqlCommand("SP_SAVE_BANK_RECONCILIATION", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // ✅ Pass RECON_DATE as NVARCHAR (no parsing)
                        cmd.Parameters.Add("@RECON_DATE", SqlDbType.NVarChar, 50).Value =
                            string.IsNullOrEmpty(request.RECON_DATE) ? (object)DBNull.Value : request.RECON_DATE;

                        // ✅ Table-valued parameter
                        var dt = new DataTable();
                        dt.Columns.Add("TRANS_ID", typeof(int));
                        foreach (var item in request.ReconciliationList)
                        {
                            dt.Rows.Add(item.TRANS_ID);
                        }

                        var tvpParam = cmd.Parameters.AddWithValue("@UDT_BANK_RECON", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_BANK_RECON";

                       // conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        response.flag = 1;
                        response.message = "Bank reconciliation saved successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = $"Error: {ex.Message}";
            }
            return response;
        }



    }
}

