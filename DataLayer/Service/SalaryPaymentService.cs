using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SalaryPaymentService:ISalaryPaymentService
    {
        public SalaryPaymentResponse Insert(SalaryPayment model)
        {
            SalaryPaymentResponse response = new SalaryPaymentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_PAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Always tell SP this is ACTION 1
                        cmd.Parameters.AddWithValue("@ACTION", 1);

                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@TRANS_ID", 0); // Not required for ACTION 1
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? (object)DBNull.Value);

                        // Prepare table-valued parameter with only PAYDETAIL_ID
                        DataTable dtDetail = new DataTable();
                        dtDetail.Columns.Add("PAYDETAIL_ID", typeof(int));

                        foreach (var item in model.SALARY_PAY_DETAIL)
                        {
                            dtDetail.Rows.Add(item.PAYDETAIL_ID);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_SALARY_PAY_DETAIL", dtDetail);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_SALARY_PAY_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }
        public SalaryPendingResponse GetPendingSalaryList(SalaryPendingRequest request)
        {
            var res = new SalaryPendingResponse
            {
                data = new List<SalaryPaymentPending>()
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_SALARY_PAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", DBNull.Value);

                        // Parse SAL_MONTH string into DateTime
                        if (DateTime.TryParse(request.SAL_MONTH, out DateTime salMonth))
                            cmd.Parameters.AddWithValue("@SAL_MONTH", salMonth);
                        else
                            throw new Exception("Invalid SAL_MONTH format. Expected yyyy-MM or yyyy-MM-dd.");

                        // Empty UDT
                        var emptyUDT = new DataTable();
                        emptyUDT.Columns.Add("PAYDETAIL_ID", typeof(int));
                        var tvpParam = cmd.Parameters.AddWithValue("@UDT_SALARY_PAY_DETAIL", emptyUDT);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_SALARY_PAY_DETAIL";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new SalaryPaymentPending
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EMP_ID = Convert.ToInt32(reader["EMP_ID"]),
                                    EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                    EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                    NET_AMOUNT = Convert.ToDecimal(reader["NET_AMOUNT"])
                                });
                            }
                        }
                    }

                    res.flag = res.data.Any() ? 1 : 0;
                    res.message = res.data.Any() ? "Success" : "No data found";
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
