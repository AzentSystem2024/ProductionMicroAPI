using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SalaryPaymentService:ISalaryPaymentService
    {
        public SalaryPaymentResponse insert(SalaryPayment model)
        {
            SalaryPaymentResponse response = new SalaryPaymentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_PAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? (object)DBNull.Value);

                        DataTable dtDetail = new DataTable();
                        dtDetail.Columns.Add("PAYDETAIL_ID", typeof(int));
                        dtDetail.Columns.Add("NET_AMOUNT", typeof(decimal));

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

    }
}
