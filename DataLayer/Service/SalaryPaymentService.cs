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
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);

                        // Prepare table-valued parameter with only PAYDETAIL_ID
                        DataTable dtDetail = new DataTable();
                        dtDetail.Columns.Add("PAYDETAIL_ID", typeof(int));
                        dtDetail.Columns.Add("NET_AMOUNT", typeof(decimal));

                        foreach (var item in model.SALARY_PAY_DETAIL)
                        {
                            dtDetail.Rows.Add(item.PAYDETAIL_ID, item.NET_AMOUNT);
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

                        cmd.Parameters.AddWithValue("@ACTION", 4);
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

                        // Parse SAL_MONTH string into DateTime
                        if (DateTime.TryParse(request.SAL_MONTH, out DateTime salMonth))
                            cmd.Parameters.AddWithValue("@SAL_MONTH", salMonth);
                        else
                            throw new Exception("Invalid SAL_MONTH format. Expected yyyy-MM or yyyy-MM-dd.");

                        // Empty UDT
                        var emptyUDT = new DataTable();
                        emptyUDT.Columns.Add("PAYDETAIL_ID", typeof(int));
                        emptyUDT.Columns.Add("NET_AMOUNT", typeof(decimal));

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
        public SalaryPaymentListResponse GetsalaryPaymentList()
        {
            SalaryPaymentListResponse response = new SalaryPaymentListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<SalaryPaymentListItem>()
            };

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add required parameters
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 30);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new SalaryPaymentListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value  ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("yyyy-MM-dd") : null,
                                    VOUCHER_NO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null,
                                    CHEQUE_NO = reader["CHEQUE_NO"] != DBNull.Value ? reader["CHEQUE_NO"].ToString() : null,
                                    CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? reader["CHEQUE_DATE"].ToString() : null,
                                    BANK_NAME = reader["BANK_NAME"] != DBNull.Value ? reader["BANK_NAME"].ToString() : null,
                                    PARTY_NAME = reader["PARTY_NAME"] != DBNull.Value ? reader["PARTY_NAME"].ToString() : null,
                                    NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null,
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0,
                                    PAY_TYPE_ID = reader["PAY_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TYPE_ID"]) : 0,
                                    PAY_HEAD_ID = reader["PAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_HEAD_ID"]) : 0

                                });
                            }
                        }

                        response.flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public SalaryPaymentDetailResponse GetSalaryPaymentById(int id)
        {
            SalaryPaymentDetailResponse response = new SalaryPaymentDetailResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<SalaryPaymentDetail>()
            };

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 30);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);

                        // Fill unused params with NULL
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            SalaryPaymentDetail header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new SalaryPaymentDetail
                                    {
                                        TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                        VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                        SAL_MONTH = reader["SAL_MONTH"] != DBNull.Value ? Convert.ToDateTime(reader["SAL_MONTH"]).ToString("MM-yyyy") : null,
                                        CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                        CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        BANK_NAME = reader["BANK_NAME"]?.ToString(),
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        PAY_TYPE_ID = reader["PAY_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TYPE_ID"]) : 0,
                                        PAY_HEAD_ID = reader["PAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_HEAD_ID"]) : 0,
                                        TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0,
                                        DetailList = new List<SalaryPaymentDetailRow>()
                                    };
                                }

                                header.DetailList.Add(new SalaryPaymentDetailRow
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EMP_ID = reader["EMP_ID"] != DBNull.Value ? Convert.ToInt32(reader["EMP_ID"]) : 0,
                                    EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                    EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0
                                });
                            }

                            if (header != null)
                            {
                                response.Data.Add(header); 
                                response.flag = 1;
                                response.Message = "Success";
                            }
                            else
                            {
                                response.flag = 0;
                                response.Message = "No record found.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }


        public SalPayLastDocno GetLastDocNo()
        {
            SalPayLastDocno res = new SalPayLastDocno();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 30
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.VOUCHER_NO = result != null ? Convert.ToInt32(result) : 0;
                        res.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        public SalaryPaymentResponse update(SalaryPaymentUpdate model)
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
                        cmd.Parameters.AddWithValue("@ACTION", 2);

                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);

                        // Prepare table-valued parameter with only PAYDETAIL_ID
                        DataTable dtDetail = new DataTable();
                        dtDetail.Columns.Add("PAYDETAIL_ID", typeof(int));
                        dtDetail.Columns.Add("NET_AMOUNT", typeof(decimal));

                        foreach (var item in model.SALARY_PAY_DETAIL)
                        {
                            dtDetail.Rows.Add(item.PAYDETAIL_ID, item.NET_AMOUNT);
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
        public SalaryPaymentResponse commit(SalaryPaymentUpdate model)
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
                        cmd.Parameters.AddWithValue("@ACTION", 3);

                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);

                        // Prepare table-valued parameter with only PAYDETAIL_ID
                        DataTable dtDetail = new DataTable();
                        dtDetail.Columns.Add("PAYDETAIL_ID", typeof(int));
                        dtDetail.Columns.Add("NET_AMOUNT", typeof(decimal));

                        foreach (var item in model.SALARY_PAY_DETAIL)
                        {
                            dtDetail.Rows.Add(item.PAYDETAIL_ID, item.NET_AMOUNT);
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
