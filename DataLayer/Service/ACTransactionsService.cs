using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Globalization;
using System.Linq;

namespace MicroApi.DataLayer.Service
{
    public class ACTransactionsService:IACTransactionsService
    {
        public JournalResponse InsertJournal(JournalHeader input)
        {
            var res = new JournalResponse();

            try
            {
                using var connection = ADO.GetConnection();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using var tx = connection.BeginTransaction();
                try
                {
                    int headerId;
                    string journalNo;

                    // Step 1: Insert Header via Stored Procedure
                    using (var cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection, tx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        AddParam(cmd, "@ID", DBNull.Value);
                        AddParam(cmd, "@ACTION", 1);
                        AddParam(cmd, "@COMPANY_ID", input.COMPANY_ID);
                        AddParam(cmd, "@STORE_ID", input.STORE_ID);
                        AddParam(cmd, "@FIN_ID", input.FIN_ID);
                        AddParam(cmd, "@TRANS_TYPE", 4);
                        AddParam(cmd, "@TRANS_DATE", ParseDate(input.TRANS_DATE));
                        AddParam(cmd, "@TRANS_STATUS", input.TRANS_STATUS);
                        AddParam(cmd, "@VOUCHER_NO", DBNull.Value);
                        AddParam(cmd, "@RECEIPT_NO", DBNull.Value);
                        AddParam(cmd, "@IS_DIRECT", 1);
                        AddParam(cmd, "@REF_NO", input.REF_NO);
                        AddParam(cmd, "@CHEQUE_NO", input.CHEQUE_NO);
                        AddParam(cmd, "@CHEQUE_DATE", ParseDate(input.CHEQUE_DATE));
                        AddParam(cmd, "@BANK_NAME", input.BANK_NAME);
                        AddParam(cmd, "@RECON_DATE", DBNull.Value);
                        AddParam(cmd, "@PDC_ID", DBNull.Value);
                        AddParam(cmd, "@IS_CLOSED", false);
                        AddParam(cmd, "@PARTY_ID", input.PARTY_ID);
                        AddParam(cmd, "@PARTY_NAME", input.PARTY_NAME);
                        AddParam(cmd, "@PARTY_REF_NO", input.PARTY_REF_NO);
                        AddParam(cmd, "@IS_PASSED", false);
                        AddParam(cmd, "@SCHEDULE_NO", DBNull.Value);
                        AddParam(cmd, "@NARRATION", input.NARRATION);
                        AddParam(cmd, "@CREATE_USER_ID", input.USER_ID);
                        AddParam(cmd, "@VERIFY_USER_ID", DBNull.Value);
                        AddParam(cmd, "@APPROVE1_USER_ID", DBNull.Value);
                        AddParam(cmd, "@APPROVE2_USER_ID", DBNull.Value);
                        AddParam(cmd, "@APPROVE3_USER_ID", DBNull.Value);
                        AddParam(cmd, "@PAY_TYPE_ID", DBNull.Value);
                        AddParam(cmd, "@PAY_HEAD_ID", DBNull.Value);
                        AddParam(cmd, "@ADD_TIME", DateTime.Now);
                        AddParam(cmd, "@CREATED_STORE_ID", input.STORE_ID);

                        using var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            headerId = Convert.ToInt32(reader["TRANS_ID"]);
                            journalNo = reader["VOUCHER_NO"]?.ToString();
                        }
                        else throw new Exception("No data returned from journal header insert.");
                    }

                    if (input.DETAILS?.Count == 0)
                        throw new Exception("No journal details provided.");

                    // Step 2: Insert Details
                    foreach (var detail in input.DETAILS)
                    {
                        const string sql = @"
                    INSERT INTO TB_AC_TRANS_DETAIL 
                    (TRANS_ID, COMPANY_ID, STORE_ID, SL_NO, HEAD_ID, DR_AMOUNT, CR_AMOUNT, REMARKS,
                     OPP_HEAD_ID, OPP_HEAD_NAME, BILL_NO, JOB_ID, CREATED_STORE_ID, STORE_AUTO_ID)
                    VALUES 
                    (@TRANS_ID, @COMPANY_ID, @STORE_ID, @SL_NO, @HEAD_ID, @DR_AMOUNT, @CR_AMOUNT, @REMARKS,
                     NULL, NULL, @BILL_NO, NULL, @CREATED_STORE_ID, NULL)";

                        using var detailCmd = new SqlCommand(sql, connection, tx);
                        AddParam(detailCmd, "@TRANS_ID", headerId);
                        AddParam(detailCmd, "@COMPANY_ID", input.COMPANY_ID);
                        AddParam(detailCmd, "@STORE_ID", input.STORE_ID);
                        AddParam(detailCmd, "@SL_NO", detail.SL_NO);
                        AddParam(detailCmd, "@HEAD_ID", detail.LEDGER_CODE);
                        AddParam(detailCmd, "@DR_AMOUNT", detail.DEBIT_AMOUNT);
                        AddParam(detailCmd, "@CR_AMOUNT", detail.CREDIT_AMOUNT);
                        AddParam(detailCmd, "@REMARKS", detail.PARTICULARS);
                        AddParam(detailCmd, "@BILL_NO", detail.BILL_NO);
                        AddParam(detailCmd, "@CREATED_STORE_ID", input.STORE_ID);

                        detailCmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    res.flag = 1;
                    res.Message = $"Journal inserted successfully with Journal No: {journalNo}";
                }
                catch (Exception ex1)
                {
                    tx.Rollback();
                    res.flag = 0;
                    res.Message = $"Transaction Failed: {ex1.Message}";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Connection Failed: {ex.Message}";
            }

            return res;
        }

        private static void AddParam(SqlCommand cmd, string name, object? value)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        private static object ParseDate(string? dateStr)
        {
            return DateTime.TryParseExact(dateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)
                ? dt
                : DBNull.Value;
        }




        public JournalResponse UpdateJournal(JournalUpdateHeader header)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var tx = connection.BeginTransaction())
                    {
                        try
                        {
                            DateTime parsedDate;
                            if (!DateTime.TryParseExact(header.TRANS_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                                throw new Exception("Invalid TRANS_DATE format. Expected dd-MM-yyyy.");

                            // 🔁 Prepare DataTable for UDT
                            DataTable detailTable = new DataTable();
                            detailTable.Columns.Add("ID", typeof(int));
                            detailTable.Columns.Add("TRANS_ID", typeof(int));
                            detailTable.Columns.Add("COMPANY_ID", typeof(int));
                            detailTable.Columns.Add("STORE_ID", typeof(string));
                            detailTable.Columns.Add("SL_NO", typeof(int));
                            detailTable.Columns.Add("HEAD_ID", typeof(int));
                            detailTable.Columns.Add("DR_AMOUNT", typeof(decimal));
                            detailTable.Columns.Add("CR_AMOUNT", typeof(decimal));
                            detailTable.Columns.Add("REMARKS", typeof(string));
                            detailTable.Columns.Add("OPP_HEAD_ID", typeof(int));
                            detailTable.Columns.Add("OPP_HEAD_NAME", typeof(string));
                            detailTable.Columns.Add("BILL_NO", typeof(string));
                            detailTable.Columns.Add("JOB_ID", typeof(int));
                            detailTable.Columns.Add("CREATED_STORE_ID", typeof(string));
                            detailTable.Columns.Add("STORE_AUTO_ID", typeof(string));

                            foreach (var d in header.DETAILS)
                            {
                                detailTable.Rows.Add(
                                    d.ID,
                                    header.TRANS_ID,
                                    d.COMPANY_ID ?? 0,
                                    d.STORE_ID ?? 0,
                                    d.SL_NO ?? 0,
                                    d.HEAD_ID ?? 0,
                                    d.DEBIT_AMOUNT ?? 0,
                                    d.CREDIT_AMOUNT ?? 0,
                                    d.REMARKS ?? "",
                                    d.OPP_HEAD_ID ?? (object)DBNull.Value,
                                    d.OPP_HEAD_NAME ?? "",
                                    d.BILL_NO ?? "",
                                    d.JOB_ID ?? (object)DBNull.Value,
                                    d.CREATED_STORE_ID ?? 0,
                                    d.STORE_AUTO_ID ?? 0
                                );
                            }

                            // 🔁 Call SP
                            using (var cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection, tx))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ID", header.TRANS_ID);
                                cmd.Parameters.AddWithValue("@ACTION", 2);
                                cmd.Parameters.AddWithValue("@COMPANY_ID", header.COMPANY_ID);
                                cmd.Parameters.AddWithValue("@STORE_ID", header.STORE_ID);
                                cmd.Parameters.AddWithValue("@FIN_ID", header.FIN_ID);
                                cmd.Parameters.AddWithValue("@TRANS_TYPE", header.TRANS_TYPE);
                                cmd.Parameters.AddWithValue("@TRANS_DATE", parsedDate);
                                cmd.Parameters.AddWithValue("@TRANS_STATUS", header.TRANS_STATUS);
                                cmd.Parameters.AddWithValue("@VOUCHER_NO", header.JOURNAL_NO ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@RECEIPT_NO", header.RECEIPT_NO ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IS_DIRECT", header.IS_DIRECT ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@REF_NO", (object?)header.REF_NO ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CHEQUE_NO", (object?)header.CHEQUE_NO ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CHEQUE_DATE", (object?)header.CHEQUE_DATE ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@BANK_NAME", (object?)header.BANK_NAME ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@RECON_DATE", (object?)header.RECON_DATE ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@PDC_ID", header.PDC_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IS_CLOSED", header.IS_CLOSED ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@PARTY_ID", header.PARTY_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@PARTY_NAME", (object?)header.PARTY_NAME ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@PARTY_REF_NO", (object?)header.PARTY_REF_NO ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IS_PASSED", header.IS_PASSED ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@SCHEDULE_NO", header.SCHEDULE_NO ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@NARRATION", (object?)header.NARRATION ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CREATE_USER_ID", header.CREATE_USER_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@VERIFY_USER_ID", header.VERIFY_USER_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", header.APPROVE1_USER_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", header.APPROVE2_USER_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", header.APPROVE3_USER_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@PAY_TYPE_ID", header.PAY_TYPE_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@PAY_HEAD_ID", header.PAY_HEAD_ID ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ADD_TIME", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CREATED_STORE_ID", header.CREATED_STORE_ID ?? (object)DBNull.Value);

                                var tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_TRANS_DETAIL", detailTable);
                                tvp.SqlDbType = SqlDbType.Structured;

                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        //res.TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]);
                                        //res.JOURNAL_NO = reader["JOURNAL_NO"].ToString();
                                        res.flag = 1;
                                        res.Message = "Success";
                                    }
                                }
                            }

                            tx.Commit();
                        }
                        catch (Exception ex1)
                        {
                            tx.Rollback();
                            res.flag = 0;
                            res.Message = "Transaction Failed: " + ex1.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Database Error: " + ex.Message;
            }

            return res;
        }


        public JournalListResponse GetJournalVoucherList()
        {
            JournalListResponse res = new JournalListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<JournalListItem>()
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ACTION", 0);

                        // Pass required default values for mandatory SP parameters
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 4);
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
                        cmd.Parameters.AddWithValue("@IS_CLOSED", 0);
                        cmd.Parameters.AddWithValue("@PARTY_ID", 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PASSED", 0);
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            var headers = new Dictionary<int, JournalListItem>();

                            while (reader.Read())
                            {
                                int transId = Convert.ToInt32(reader["TRANS_ID"]);

                                if (!headers.ContainsKey(transId))
                                {
                                    headers[transId] = new JournalListItem
                                    {
                                        TRANS_ID = transId,
                                        JOURNAL_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"]?.ToString(),
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value
                                    ? Convert.ToInt32(reader["TRANS_TYPE"])
                                    : 0,
                                        TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value
                                    ? Convert.ToInt32(reader["TRANS_STATUS"])
                                    : 0,
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        DETAILS = new List<JournalListDetail>()
                                    };
                                }

                                headers[transId].DETAILS.Add(new JournalListDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : (int?)null,
                                    BILL_NO = reader["BILL_NO"]?.ToString(),
                                    LEDGER_CODE = reader["LEDGER_CODE"]?.ToString(),
                                    LEDGER_NAME = reader["LEDGER_NAME"]?.ToString(),
                                    PARTICULARS = reader["PARTICULARS"]?.ToString(),
                                    DEBIT_AMOUNT = reader["DEBIT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DEBIT_AMOUNT"]) : 0,
                                    CREDIT_AMOUNT = reader["CREDIT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["CREDIT_AMOUNT"]) : 0
                                });
                            }

                            res.Data = headers.Values.ToList();
                            res.flag = 1;
                            res.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Exception: " + ex.Message;
            }

            return res;
        }


        public JournalResponse GetJournalById(int id)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Only mandatory inputs
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 4);

                        using (var reader = cmd.ExecuteReader())
                        {
                            JournalViewHeader header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new JournalViewHeader
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        JOURNAL_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("yyyy-MM-dd"): null,
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        TRANS_TYPE = Convert.ToInt32(reader["TRANS_TYPE"]),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        IS_APPROVED = reader["TRANS_STATUS"] != DBNull.Value && Convert.ToInt32(reader["TRANS_STATUS"]) == 5,
                                        DETAILS = new List<JournalListDetail>()
                                    };
                                }

                                header.DETAILS.Add(new JournalListDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : (int?)null,
                                    BILL_NO = reader["BILL_NO"]?.ToString(),
                                    LEDGER_CODE = reader["LEDGER_CODE"]?.ToString(),
                                    LEDGER_NAME = reader["LEDGER_NAME"]?.ToString(),
                                    PARTICULARS = reader["PARTICULARS"]?.ToString(),
                                    DEBIT_AMOUNT = reader["DEBIT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DEBIT_AMOUNT"]) : 0,
                                    CREDIT_AMOUNT = reader["CREDIT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["CREDIT_AMOUNT"]) : 0
                                });
                            }

                            if (header != null)
                            {
                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = header;
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Journal not found";
                            }
                        }
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
        public VoucherResponse GetLastVoucherNo()
        {
            VoucherResponse res = new VoucherResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                SELECT TOP 1 VOUCHER_NO 
                FROM TB_AC_TRANS_HEADER 
                WHERE TRANS_TYPE = 4 
                ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.VoucherNo = result != null ? result.ToString() : "";
                        res.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.VoucherNo = "";
            }

            return res;
        }
        public JournalResponse DeleteJournal(int id)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_AC_TRANS_HEADER_DETAIL";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

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

        public JournalResponse JournalApproval(int transId, bool isApproved)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var tx = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection, tx))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@ID", transId);
                                cmd.Parameters.AddWithValue("@ACTION", 4);
                                cmd.Parameters.AddWithValue("@IS_APPROVED", isApproved ? 1 : 0);

                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        res.flag = 1;
                                        res.Message = "Updated";
                                        res.Data = new ApprovalRequest
                                        {
                                            TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                            IS_APPROVED = isApproved
                                        };
                                    }
                                    else
                                    {
                                        res.flag = 0;
                                        res.Message = "No record found to update.";
                                        res.Data = null;
                                    }
                                }
                            }

                            tx.Commit();
                        }
                        catch (Exception ex1)
                        {
                            tx.Rollback();
                            res.flag = 0;
                            res.Message = "Transaction Failed: " + ex1.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Database Error: " + ex.Message;
            }

            return res;
        }
        //----------------------------------END JOURNAL VOUCHER-----------------------------------------//

        public CreditNoteResponse SaveDebitNote(AC_DebitNote model)
        {
            CreditNoteResponse response = new CreditNoteResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_DEBIT_NOTE", connection))
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
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? 0);  
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
                        cmd.Parameters.AddWithValue("@BILL_NO", model.BILL_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@JOB_ID", model.JOB_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_AUTO_ID", model.STORE_AUTO_ID ?? 0);

                        // Table-valued parameter
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));
                        dt.Columns.Add("VAT_AMOUNT", typeof(double));
                        dt.Columns.Add("REMARKS", typeof(string));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.VAT_AMOUNT, item.REMARKS ?? string.Empty);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Debit note saved successfully.";
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
