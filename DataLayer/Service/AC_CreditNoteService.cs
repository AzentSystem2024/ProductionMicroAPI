using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

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
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECON_DATE", model.RECON_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADD_TIME", model.ADD_TIME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@INVOICE_ID", model.INVOICE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@INVOICE_NO", model.INVOICE_NO ?? (object)DBNull.Value);

                        // TVP
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));
                        dt.Columns.Add("VAT_AMOUNT", typeof(double));
                        dt.Columns.Add("REMARKS", typeof(string));
                        //dt.Columns.Add("TRANS_ID", typeof(long));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.VAT_AMOUNT, item.REMARKS);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        // Final insert
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
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID == 0 ? DBNull.Value : (object)model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", model.CHEQUE_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RECON_DATE", model.RECON_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADD_TIME", model.ADD_TIME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@INVOICE_ID", model.INVOICE_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@INVOICE_NO", model.INVOICE_NO ?? (object)DBNull.Value);

                        // Table-valued parameter
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(float));
                        dt.Columns.Add("VAT_AMOUNT", typeof(float));
                        dt.Columns.Add("REMARKS", typeof(string));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.VAT_AMOUNT, item.REMARKS);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        cmd.ExecuteNonQuery();

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

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new CreditNoteListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    TRANS_DATE = Convert.ToDateTime(reader["TRANS_DATE"]).ToString("yyyy-MM-dd"),
                                    CUSTOMER_NAME = reader["PARTY_NAME"]?.ToString(),
                                    INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    VAT_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                    NARRATION = reader["NARRATION"]?.ToString(),
                                    REMARKS = reader["REMARKS"]?.ToString()
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
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        NOTE_DETAIL = new List<CreditNoteDetailUpdate>()
                                    };
                                }

                                header.NOTE_DETAIL.Add(new CreditNoteDetailUpdate
                                {
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : 0,
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    VAT_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_AMOUNT"]) : 0,
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


    }
}
