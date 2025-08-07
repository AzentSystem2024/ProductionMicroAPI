using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class MiscPaymentService: IMiscPaymentService
    {
        public MiscpaymentResponse Save(MiscPayment model)
        {
            MiscpaymentResponse response = new MiscpaymentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);


                        // UDT setup
                        DataTable dt = new DataTable();

                        dt.Columns.Add("TRANS_ID", typeof(int));
                        dt.Columns.Add("STORE_ID", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("REMARKS", typeof(string));
                        dt.Columns.Add("AMOUNT", typeof(decimal));
                        dt.Columns.Add("VAT_AMOUNT", typeof(decimal));
                        dt.Columns.Add("VAT_REGN", typeof(string));
                        dt.Columns.Add("VAT_PERCENT", typeof(double));

                        int slno = 1;
                        // Add rows from your model
                        foreach (var item in model.MISC_DETAIL)
                        {
                            dt.Rows.Add( 0, slno++,
                                item.HEAD_ID,
                                item.REMARKS ?? string.Empty,
                                item.AMOUNT,
                                item.VAT_AMOUNT,
                                item.VAT_REGN ,
                                item.VAT_PERCENT
                            );
                        }
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_PAYMENT", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_TB_AC_PAYMENT";

                        // Execute
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Success.";
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
        public MiscPaymentListResponse GetMiscPaymentList()
        {
            MiscPaymentListResponse response = new MiscPaymentListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<MiscPaymentListItem>()
            };

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add required parameters
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 31); // or your transaction type
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID",0);
                        cmd.Parameters.AddWithValue("@FIN_ID",  0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", 0);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", 0);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", 0);
                        cmd.Parameters.AddWithValue("@BANK_NAME", 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", 0);
                        cmd.Parameters.AddWithValue("@NARRATION", 0);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", 0);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new MiscPaymentListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                    VOUCHER_NO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null,
                                    CHEQUE_NO = reader["CHEQUE_NO"] != DBNull.Value ? reader["CHEQUE_NO"].ToString() : null,
                                    CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? reader["CHEQUE_DATE"].ToString() : null,
                                    BANK_NAME = reader["BANK_NAME"] != DBNull.Value ? reader["BANK_NAME"].ToString() : null,
                                    PARTY_NAME = reader["PARTY_NAME"] != DBNull.Value ? reader["PARTY_NAME"].ToString() : null,
                                    NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                    VAT_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_AMOUNT"]) : 0,
                                    VAT_PERCENT = reader["VAT_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_PERCENT"]) : 0,
                                    VAT_REGN = reader["VAT_REGN"] != DBNull.Value ? Convert.ToSingle(reader["VAT_REGN"]) : 0,
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

    }
}
