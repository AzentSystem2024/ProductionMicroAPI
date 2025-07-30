using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SupplierPaymentService:ISupplierPaymentService
    {
        public SupplierPaymentResponse insert(SupplierPayment model)
        {
            SupplierPaymentResponse response = new SupplierPaymentResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.Add("@TRANS_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@PAY_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? false);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPPLIER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", ParseDate(model.ADD_TIME));
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // UDT: UDT_CUST_REC_DETAIL
                        DataTable dt = new DataTable();
                        dt.Columns.Add("BILL_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));

                        foreach (var item in model.SUPP_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SUPP_PAY_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SUPP_PAY_DETAIL";

                        // Execute
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
        public SupplierPaymentResponse Update(SupplierPaymentUpdate model)
        {
            SupplierPaymentResponse response = new SupplierPaymentResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2); 
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? 0);
                       // cmd.Parameters.AddWithValue("@PAY_ID", model.PAY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? false);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPPLIER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", ParseDate(model.ADD_TIME));
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // UDT: UDT_CUST_REC_DETAIL
                        DataTable dt = new DataTable();
                        dt.Columns.Add("BILL_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));

                        foreach (var item in model.SUPP_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SUPP_PAY_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SUPP_PAY_DETAIL";

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
        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[]
            {
                "dd-MM-yyyy HH:mm:ss",
                "dd-MM-yyyy",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-dd",
                "MM/dd/yyyy HH:mm:ss",
                "MM/dd/yyyy"
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public SupplierPaymentListResponse GetPaymentList()
        {
            SupplierPaymentListResponse response = new SupplierPaymentListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<SupplierPaymentListItem>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 21);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SupplierPaymentListItem item = new SupplierPaymentListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                    PAY_DATE = reader["PAY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PAY_DATE"]).ToString("dd-MM-yyyy") : null,
                                    // UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                    NARRATION = reader["NARRATION"]?.ToString(),
                                    PAY_TYPE_ID = reader["PAY_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TYPE_ID"]) : 0,
                                    PAY_TYPE_NAME = reader["PAY_TYPE_NAME"]?.ToString(),
                                    PAY_HEAD_ID = reader["PAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_HEAD_ID"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    RECEIVED_AMOUNT = reader["RECEIVED_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["RECEIVED_AMOUNT"]) : 0,
                                    CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                    CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    BANK_NAME = reader["BANK_NAME"]?.ToString(),
                                };

                                response.Data.Add(item);
                            }

                            response.flag = 1;
                            response.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = new List<SupplierPaymentListItem>();
            }

            return response;
        }
    }
}
