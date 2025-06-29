using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class AC_CreditNoteService:IAC_CreditNoteService
    {
        public CreditNoteResponse SaveCreditNote(AC_CreditNote model)
        {
            CreditNoteResponse response = new CreditNoteResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_CREDIT_NOTE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? 0);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", model.RECON_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
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
                        cmd.Parameters.AddWithValue("@INVOICE_ID", model.INVOICE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@INVOICE_NO", model.INVOICE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", model.DISTRIBUTOR_ID ?? 0);

                        // UDT setup
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));
                        dt.Columns.Add("VAT_AMOUNT", typeof(double));
                        dt.Columns.Add("REMARKS", typeof(string));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.GST_AMOUNT, item.REMARKS ?? string.Empty);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        // Execute
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Credit note saved successfully.";
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
        public CreditNoteResponse UpdateCreditNote(AC_CreditNoteUpdate model)
        {
            CreditNoteResponse response = new CreditNoteResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_CREDIT_NOTE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID == 0 ? 0 : model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? 0);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", model.RECON_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
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
                        cmd.Parameters.AddWithValue("@INVOICE_ID", model.INVOICE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@INVOICE_NO", model.INVOICE_NO ?? string.Empty);

                        // Distributor/Unit logic
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", model.DISTRIBUTOR_ID ?? 0);

                        // Table-valued parameter
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(float));
                        dt.Columns.Add("VAT_AMOUNT", typeof(float));
                        dt.Columns.Add("REMARKS", typeof(string));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.GST_AMOUNT, item.REMARKS ?? string.Empty);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Credit note updated successfully.";
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

        public CreditNoteListResponse GetCreditNoteList()
        {
            CreditNoteListResponse response = new CreditNoteListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<CreditNoteListItem>()
            };

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_CREDIT_NOTE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 37);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", 0);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", 0);
                        cmd.Parameters.AddWithValue("@REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECON_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PDC_ID", 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", false);
                        cmd.Parameters.AddWithValue("@PARTY_ID", 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PASSED", false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", 0);
                        cmd.Parameters.AddWithValue("@NARRATION", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@INVOICE_ID", 0);
                        cmd.Parameters.AddWithValue("@INVOICE_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@REMARKS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@UNIT_ID", 0);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", 0); // Ensure SP supports this

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new CreditNoteListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy")
                                        : null,
                                    INVOICE_NO = reader["INVOICE_NO"] != DBNull.Value ? reader["INVOICE_NO"].ToString() : null,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    GST_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                    NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null,
                                   // REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null,
                                    UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    DISTRIBUTOR_ID = reader["DISTRIBUTOR_ID"] != DBNull.Value ? Convert.ToInt32(reader["DISTRIBUTOR_ID"]) : 0
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
                response.Data = new List<CreditNoteListItem>();
            }

            return response;
        }


        public AC_CreditNoteSelect GetCreditNoteById(int id)
        {
            AC_CreditNoteSelect response = new AC_CreditNoteSelect
            {
                flag = 0,
                Message = "Failed",
                Data = new List<CreditNoteSelectedView>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_CREDIT_NOTE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 37);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            CreditNoteSelectedView header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new CreditNoteSelectedView
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        TRANS_TYPE = 37,
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]) : null,
                                        TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                        INVOICE_ID = reader["INVOICE_ID"] != DBNull.Value ? Convert.ToInt32(reader["INVOICE_ID"]) : (int?)null,
                                        INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,             
                                        DISTRIBUTOR_ID = reader["DISTRIBUTOR_ID"] != DBNull.Value ? Convert.ToInt32(reader["DISTRIBUTOR_ID"]) : 0, 
                                        NOTE_DETAIL = new List<CreditNoteDetailUpdate>()
                                    };
                                }

                                header.NOTE_DETAIL.Add(new CreditNoteDetailUpdate
                                {
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : 0,
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    GST_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_AMOUNT"]) : 0,
                                    REMARKS = reader["REMARKS"]?.ToString()
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
                                response.Message = "No credit note found.";
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

        public CreditNoteResponse CommitCreditNote(CreditNoteCommitRequest request)
        {
            CreditNoteResponse response = new CreditNoteResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_CREDIT_NOTE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", request.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", request.TRANS_TYPE);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);
                        cmd.Parameters.AddWithValue("@INVOICE_ID", request.INVOICE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@INVOICE_NO", request.INVOICE_NO ?? "");
                        cmd.Parameters.AddWithValue("@UNIT_ID", request.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", request.CREATED_STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@BILL_NO", request.BILL_NO ?? "");
                        cmd.Parameters.AddWithValue("@JOB_ID", request.JOB_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_AUTO_ID", request.STORE_AUTO_ID ?? 0);

                        cmd.Parameters.AddWithValue("@TRANS_STATUS", 5);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", 0);
                        cmd.Parameters.AddWithValue("@REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECON_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PDC_ID", 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", false);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PASSED", false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", DateTime.Now);

                        // Table-Valued Parameter
                        DataTable dtDetail = new DataTable();
                        dtDetail.Columns.Add("SL_NO", typeof(int));
                        dtDetail.Columns.Add("HEAD_ID", typeof(int));
                        dtDetail.Columns.Add("AMOUNT", typeof(float));
                        dtDetail.Columns.Add("VAT_AMOUNT", typeof(float));
                        dtDetail.Columns.Add("REMARKS", typeof(string));

                        foreach (var item in request.NOTE_DETAIL)
                        {
                            dtDetail.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.GST_AMOUNT, item.REMARKS ?? "");
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dtDetail);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Credit note committed successfully.";
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

        public DocResponse GetLastDocNo()
        {
            DocResponse res = new DocResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 37 
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.DOC_NO = result != null ? Convert.ToInt32(result) : 0;
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
        public CreditNoteResponse Delete(int id)
        {
            CreditNoteResponse res = new CreditNoteResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_AC_CREDIT_NOTE";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

    }
}
