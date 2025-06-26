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
                            journalNo = reader["JOURNAL_NO"]?.ToString();
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
                        AddParam(detailCmd, "@HEAD_ID", detail.LEDGE_CODE);
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

        // 🔧 Helper to handle nulls gracefully
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
                            // ✅ Parse TRANS_DATE (assumes dd-MM-yyyy format)
                            DateTime parsedDate;
                            if (!DateTime.TryParseExact(header.TRANS_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                            {
                                throw new Exception("Invalid TRANS_DATE format. Expected dd-MM-yyyy.");
                            }

                            // ✅ Update Header
                            string updateHeaderSql = @"
                        UPDATE TB_AC_TRANS_HEADER
                        SET TRANS_DATE = @DATE,
                            REF_NO = @REF_NO,
                            PARTY_NAME = @PARTY_NAME,
                            NARRATION = @NARRATION,
                            TRANS_TYPE = @TRANS_TYPE,
                            TRANS_STATUS = @TRANS_STATUS,
                            CHEQUE_NO = @CHEQUE_NO,
                            CHEQUE_DATE = @CHEQUE_DATE,
                            BANK_NAME = @BANK_NAME,
                            RECON_DATE = @RECON_DATE,
                            IS_PASSED = @IS_PASSED,
                            PARTY_ID = @PARTY_ID,
                            PARTY_REF_NO = @PARTY_REF_NO,
                            SCHEDULE_NO = @SCHEDULE_NO,
                            IS_CLOSED = @IS_CLOSED,
                            VERIFY_USER_ID = @VERIFY_USER_ID,
                            APPROVE1_USER_ID = @APPROVE1_USER_ID,
                            APPROVE2_USER_ID = @APPROVE2_USER_ID,
                            APPROVE3_USER_ID = @APPROVE3_USER_ID,
                            PAY_TYPE_ID = @PAY_TYPE_ID,
                            PAY_HEAD_ID = @PAY_HEAD_ID
                        WHERE TRANS_ID = @TRANS_ID";
                            int transStatus = (header.IS_APPROVED == true) ? 5 : (header.TRANS_STATUS ?? 1);


                            using (var headerCmd = new SqlCommand(updateHeaderSql, connection, tx))
                            {
                                headerCmd.Parameters.AddWithValue("@TRANS_ID", header.TRANS_ID);
                                headerCmd.Parameters.AddWithValue("@DATE", parsedDate);
                                headerCmd.Parameters.AddWithValue("@REF_NO", (object?)header.REF_NO ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@PARTY_NAME", (object?)header.PARTY_NAME ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@NARRATION", (object?)header.NARRATION ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@TRANS_TYPE", header.TRANS_TYPE ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@TRANS_STATUS", transStatus);
                                headerCmd.Parameters.AddWithValue("@CHEQUE_NO", (object?)header.CHEQUE_NO ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@CHEQUE_DATE", (object?)header.CHEQUE_DATE ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@BANK_NAME", (object?)header.BANK_NAME ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@RECON_DATE", (object?)header.RECON_DATE ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@IS_PASSED", (object?)header.IS_PASSED ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@PARTY_ID", (object?)header.PARTY_ID ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@PARTY_REF_NO", (object?)header.PARTY_REF_NO ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@SCHEDULE_NO", header.SCHEDULE_NO ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@IS_CLOSED", (object?)header.IS_CLOSED ?? DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@VERIFY_USER_ID", header.VERIFY_USER_ID ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@APPROVE1_USER_ID", header.APPROVE1_USER_ID ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@APPROVE2_USER_ID", header.APPROVE2_USER_ID ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@APPROVE3_USER_ID", header.APPROVE3_USER_ID ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@PAY_TYPE_ID", header.PAY_TYPE_ID ?? (object)DBNull.Value);
                                headerCmd.Parameters.AddWithValue("@PAY_HEAD_ID", header.PAY_HEAD_ID ?? (object)DBNull.Value);

                                headerCmd.ExecuteNonQuery();
                            }

                            // 🔁 Update or Insert Details
                            foreach (var d in header.DETAILS)
                            {
                                if (d.ID > 0)
                                {
                                    // 🔄 Update
                                    string updateSql = @"
                                UPDATE TB_AC_TRANS_DETAIL
                                SET BILL_NO = @BILL_NO,
                                    SL_NO = @SL_NO,
                                    HEAD_ID = @LEDGE_CODE,
                                    REMARKS = @PARTICULARS,
                                    DR_AMOUNT = @DEBIT_AMOUNT,
                                    CR_AMOUNT = @CREDIT_AMOUNT
                                WHERE ID = @ID";

                                    using (var cmd = new SqlCommand(updateSql, connection, tx))
                                    {
                                        cmd.Parameters.AddWithValue("@ID", d.ID);
                                        cmd.Parameters.AddWithValue("@SL_NO", d.SL_NO ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@BILL_NO", (object?)d.BILL_NO ?? DBNull.Value);
                                        cmd.Parameters.AddWithValue("@LEDGE_CODE", (object?)d.LEDGE_CODE ?? DBNull.Value);
                                        cmd.Parameters.AddWithValue("@PARTICULARS", (object?)d.PARTICULARS ?? DBNull.Value);
                                        cmd.Parameters.AddWithValue("@DEBIT_AMOUNT", d.DEBIT_AMOUNT);
                                        cmd.Parameters.AddWithValue("@CREDIT_AMOUNT", d.CREDIT_AMOUNT);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // ➕ Insert
                                    string insertSql = @"
                                INSERT INTO TB_AC_TRANS_DETAIL
                                (TRANS_ID, SL_NO, BILL_NO, HEAD_ID, REMARKS, DR_AMOUNT, CR_AMOUNT)
                                VALUES
                                (@TRANS_ID, @SL_NO, @BILL_NO, @LEDGE_CODE, @PARTICULARS, @DEBIT_AMOUNT, @CREDIT_AMOUNT)";

                                    using (var cmd = new SqlCommand(insertSql, connection, tx))
                                    {
                                        cmd.Parameters.AddWithValue("@TRANS_ID", header.TRANS_ID);
                                        cmd.Parameters.AddWithValue("@SL_NO", d.SL_NO ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@BILL_NO", (object?)d.BILL_NO ?? DBNull.Value);
                                        cmd.Parameters.AddWithValue("@LEDGE_CODE", (object?)d.LEDGE_CODE ?? DBNull.Value);
                                        cmd.Parameters.AddWithValue("@PARTICULARS", (object?)d.PARTICULARS ?? DBNull.Value);
                                        cmd.Parameters.AddWithValue("@DEBIT_AMOUNT", d.DEBIT_AMOUNT);
                                        cmd.Parameters.AddWithValue("@CREDIT_AMOUNT", d.CREDIT_AMOUNT);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            tx.Commit();
                            res.flag = 1;
                            res.Message = "Success";
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
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 0);
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
                                        TRANS_TYPE = Convert.ToInt32(reader["TRANS_TYPE"]),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        DETAILS = new List<JournalListDetail>()
                                    };
                                }

                                headers[transId].DETAILS.Add(new JournalListDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : (int?)null,
                                    BILL_NO = reader["BILL_NO"]?.ToString(),
                                    LEDGE_CODE = reader["LEDGE_CODE"]?.ToString(),
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
                                        DETAILS = new List<JournalListDetail>()
                                    };
                                }

                                header.DETAILS.Add(new JournalListDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : (int?)null,
                                    BILL_NO = reader["BILL_NO"]?.ToString(),
                                    LEDGE_CODE = reader["LEDGE_CODE"]?.ToString(),
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



    }
}
