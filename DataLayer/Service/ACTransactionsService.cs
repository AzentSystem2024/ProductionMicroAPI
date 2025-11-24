using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Globalization;
using System.Linq;
using System.ComponentModel.Design;
using System.Reflection;

namespace MicroApi.DataLayer.Service
{
    public class ACTransactionsService:IACTransactionsService
    {
        public JournalResponse InsertJournal(JournalHeader model)
        {
            JournalResponse response = new JournalResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Header parameters
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        int status = (model.IS_APPROVED == true) ? 5 : (model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", status);
                        // cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                       //cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", ParseDate(model.ADD_TIME));
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DEPT_ID", model.DEPT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);

                        // === UDT Table ===
                        DataTable dt = new DataTable();
                        dt.Columns.Add("ID", typeof(int));
                        dt.Columns.Add("TRANS_ID", typeof(int));
                        dt.Columns.Add("COMPANY_ID", typeof(int));
                        dt.Columns.Add("STORE_ID", typeof(string));
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("DR_AMOUNT", typeof(decimal));
                        dt.Columns.Add("CR_AMOUNT", typeof(decimal));
                        dt.Columns.Add("REMARKS", typeof(string));
                        dt.Columns.Add("OPP_HEAD_ID", typeof(int));
                        dt.Columns.Add("OPP_HEAD_NAME", typeof(string));
                        dt.Columns.Add("BILL_NO", typeof(string));
                        dt.Columns.Add("JOB_ID", typeof(int));
                        dt.Columns.Add("CREATED_STORE_ID", typeof(string));
                        dt.Columns.Add("STORE_AUTO_ID", typeof(string));

                        int slno = 1;

                        foreach (var detail in model.DETAILS)
                        {
                            dt.Rows.Add(
                                DBNull.Value,                                
                                0,                                           
                                model.COMPANY_ID ?? 0,
                                model.STORE_ID?.ToString() ?? "",
                                slno++,
                                int.Parse(detail.LEDGER_CODE ?? "0"),
                                detail.DEBIT_AMOUNT ?? 0,
                                detail.CREDIT_AMOUNT ?? 0,
                                detail.PARTICULARS ?? "",
                                detail.OPP_HEAD_ID ?? 0,
                                detail.OPP_HEAD_NAME ?? "",
                                detail.BILL_NO ?? "",
                                detail.JOB_ID ?? 0,
                                detail.CREATED_STORE_ID ?? string.Empty,
                                detail.STORE_AUTO_ID?.ToString() ?? ""
                            );
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_TRANS_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_TB_AC_TRANS_DETAIL";

                        // Execute
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Journal saved successfully.";
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
                            var parsedDate = ParseDate(header.TRANS_DATE);
                            if (parsedDate == DBNull.Value)
                                throw new Exception("Invalid TRANS_DATE format. Expected formats: dd-MM-yyyy, yyyy-MM-dd, or MM/dd/yyyy.");


                            // 🔁 Prepare DataTable for UDT
                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID", typeof(int));
                            dt.Columns.Add("TRANS_ID", typeof(int));
                            dt.Columns.Add("COMPANY_ID", typeof(int));
                            dt.Columns.Add("STORE_ID", typeof(string));
                            dt.Columns.Add("SL_NO", typeof(int));
                            dt.Columns.Add("HEAD_ID", typeof(int));
                            dt.Columns.Add("DR_AMOUNT", typeof(decimal));
                            dt.Columns.Add("CR_AMOUNT", typeof(decimal));
                            dt.Columns.Add("REMARKS", typeof(string));
                            dt.Columns.Add("OPP_HEAD_ID", typeof(int));
                            dt.Columns.Add("OPP_HEAD_NAME", typeof(string));
                            dt.Columns.Add("BILL_NO", typeof(string));
                            dt.Columns.Add("JOB_ID", typeof(int));
                            dt.Columns.Add("CREATED_STORE_ID", typeof(string));
                            dt.Columns.Add("STORE_AUTO_ID", typeof(string));

                            int slno = 1;
                            foreach (var detail in header.DETAILS)
                            {
                                dt.Rows.Add(
                                    DBNull.Value,
                                     0,
                                    header.COMPANY_ID ?? 0,
                                    header.STORE_ID?.ToString() ?? "",
                                    slno++,
                                    int.Parse(detail.LEDGER_CODE ?? "0"), 
                                    detail.DEBIT_AMOUNT ?? 0,
                                    detail.CREDIT_AMOUNT ?? 0,
                                    detail.PARTICULARS ?? "",
                                    detail.OPP_HEAD_ID ?? 0,
                                    detail.OPP_HEAD_NAME ?? "",
                                    detail.BILL_NO ?? "",
                                    detail.JOB_ID ?? 0,
                                    detail.CREATED_STORE_ID ?? string.Empty,
                                    detail.STORE_AUTO_ID ?? string.Empty
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
                                cmd.Parameters.AddWithValue("@DEPT_ID", header.DEPT_ID ?? (object)DBNull.Value);

                                var tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_TRANS_DETAIL", dt);
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
        private static void AddParam(SqlCommand cmd, string name, object? value)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
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
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 4);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
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
                        cmd.Parameters.AddWithValue("@DEPT_ID", 0);

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
                                        DEPT_ID = reader["DEPT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPT_ID"]) : 0,
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
                                        DEPT_ID = reader["DEPT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPT_ID"]) : 0,
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

        public JournalResponse commit(JournalUpdateHeader header)
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
                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID", typeof(int));
                            dt.Columns.Add("TRANS_ID", typeof(int));
                            dt.Columns.Add("COMPANY_ID", typeof(int));
                            dt.Columns.Add("STORE_ID", typeof(string));
                            dt.Columns.Add("SL_NO", typeof(int));
                            dt.Columns.Add("HEAD_ID", typeof(int));
                            dt.Columns.Add("DR_AMOUNT", typeof(decimal));
                            dt.Columns.Add("CR_AMOUNT", typeof(decimal));
                            dt.Columns.Add("REMARKS", typeof(string));
                            dt.Columns.Add("OPP_HEAD_ID", typeof(int));
                            dt.Columns.Add("OPP_HEAD_NAME", typeof(string));
                            dt.Columns.Add("BILL_NO", typeof(string));
                            dt.Columns.Add("JOB_ID", typeof(int));
                            dt.Columns.Add("CREATED_STORE_ID", typeof(string));
                            dt.Columns.Add("STORE_AUTO_ID", typeof(string));

                            int slno = 1;
                            foreach (var detail in header.DETAILS)
                            {
                                dt.Rows.Add(
                                    DBNull.Value,
                                     0,
                                    header.COMPANY_ID ?? 0,
                                    header.STORE_ID?.ToString() ?? "",
                                    slno++,
                                    int.Parse(detail.LEDGER_CODE ?? "0"),
                                    detail.DEBIT_AMOUNT ?? 0,
                                    detail.CREDIT_AMOUNT ?? 0,
                                    detail.PARTICULARS ?? "",
                                    detail.OPP_HEAD_ID ?? 0,
                                    detail.OPP_HEAD_NAME ?? "",
                                    detail.BILL_NO ?? "",
                                    detail.JOB_ID ?? 0,
                                    detail.CREATED_STORE_ID ?? string.Empty,
                                    detail.STORE_AUTO_ID ?? string.Empty
                                );
                            }


                            // 🔁 Call SP
                            using (var cmd = new SqlCommand("SP_TB_AC_TRANS_HEADER_DETAIL", connection, tx))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ID", header.TRANS_ID);
                                cmd.Parameters.AddWithValue("@ACTION", 4);
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
                                cmd.Parameters.AddWithValue("@DEPT_ID", header.DEPT_ID ?? (object)DBNull.Value);

                                var tvp = cmd.Parameters.AddWithValue("@UDT_TB_AC_TRANS_DETAIL", dt);
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

        //----------------------------------END JOURNAL VOUCHER-----------------------------------------//

        public DebitNoteResponse SaveDebitNote(AC_DebitNote model)
        {
            DebitNoteResponse response = new DebitNoteResponse();

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
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE);
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
                        cmd.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);

                        // Table-valued parameter
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));
                        dt.Columns.Add("VAT_AMOUNT", typeof(double));
                        dt.Columns.Add("REMARKS", typeof(string));
                        dt.Columns.Add("GST_PERC", typeof(float));
                        dt.Columns.Add("HSN_CODE", typeof(string));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.GST_AMOUNT, item.REMARKS ?? string.Empty,item.GST_PERC,item.HSN_CODE);
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
        public DebitNoteResponse UpdateDebitNote(AC_DebitNoteUpdate model)
        {
            DebitNoteResponse response = new DebitNoteResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_DEBIT_NOTE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE);
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

                        // Table-valued parameter for NOTE_DETAIL
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));
                        dt.Columns.Add("VAT_AMOUNT", typeof(double));
                        dt.Columns.Add("REMARKS", typeof(string));
                        dt.Columns.Add("GST_PERC", typeof(float));
                        dt.Columns.Add("HSN_CODE", typeof(string));

                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.GST_AMOUNT, item.REMARKS ?? string.Empty,item.GST_PERC,item.HSN_CODE);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Debit note updated successfully.";
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

        public DebitNoteListResponse GetDebitNoteList()
        {
            DebitNoteListResponse response = new DebitNoteListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<DebitNoteListItem>()
            };

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_DEBIT_NOTE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 36); 
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
                        cmd.Parameters.AddWithValue("@SUPP_ID", 0); 

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new DebitNoteListItem
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
                                    SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0,
                                    SUPP_NAME = reader["SUPP_NAME"] != DBNull.Value ? reader["SUPP_NAME"].ToString() : null,
                                    DOC_NO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null
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
                response.Data = new List<DebitNoteListItem>();
            }

            return response;
        }
        public AC_DebitNoteSelect GetDebitNoteById(int id)
        {
            AC_DebitNoteSelect response = new AC_DebitNoteSelect
            {
                flag = 0,
                Message = "Failed",
                Data = new List<DebitNoteSelectedView>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_DEBIT_NOTE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 36); 

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DebitNoteSelectedView header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new DebitNoteSelectedView
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        TRANS_TYPE = Convert.ToInt32(reader["TRANS_TYPE"]),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]): null,
                                        TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value? Convert.ToInt32(reader["TRANS_STATUS"]): (int?)null,
                                        INVOICE_ID = reader["INVOICE_ID"] != DBNull.Value? Convert.ToInt32(reader["INVOICE_ID"]): (int?)null,
                                        INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value? Convert.ToSingle(reader["NET_AMOUNT"]): 0,
                                        DUE_AMOUNT = reader["DUE_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["DUE_AMOUNT"]) : 0,
                                        DOC_NO = reader["VOUCHER_NO"] != DBNull.Value? Convert.ToString(reader["VOUCHER_NO"]): null,
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        NOTE_DETAIL = new List<DebitNoteDetail>()
                                    };
                                }

                                header.NOTE_DETAIL.Add(new DebitNoteDetail
                                {
                                    SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : 0,
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    GST_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["VAT_AMOUNT"]) : 0,
                                    REMARKS = reader["REMARKS"]?.ToString(),
                                    GST_PERC = reader["GST_PERC"] != DBNull.Value ? Convert.ToSingle(reader["GST_PERC"]) : 0,
                                    HSN_CODE = reader["HSN_CODE"]?.ToString(),
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
                                response.Message = "No debit note found.";
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

        public DebitNoteResponse Commit(AC_DebitNoteUpdate model)
        {
            DebitNoteResponse response = new DebitNoteResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_DEBIT_NOTE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE);
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

                        // Table-valued parameter for NOTE_DETAIL
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SL_NO", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));
                        dt.Columns.Add("VAT_AMOUNT", typeof(double));
                        dt.Columns.Add("REMARKS", typeof(string));
                        dt.Columns.Add("GST_PERC", typeof(float));
                        dt.Columns.Add("HSN_CODE", typeof(string));


                        foreach (var item in model.NOTE_DETAIL)
                        {
                            dt.Rows.Add(item.SL_NO, item.HEAD_ID, item.AMOUNT, item.GST_AMOUNT, item.REMARKS ?? string.Empty,item.GST_PERC,item.HSN_CODE);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_AC_NOTE_DETAIL", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_TB_AC_NOTE_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Debit note committed successfully.";
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
        public DocNoResponse GetLastDocNo()
        {
            DocNoResponse res = new DocNoResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 36 
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
        public DebitNoteResponse Delete(int id)
        {
            DebitNoteResponse res = new DebitNoteResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_AC_DEBIT_NOTE";

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
        public DebitInvoiceResponse GetPendingInvoiceList(DebitInvoiceRequest request)
        {
            DebitInvoiceResponse response = new DebitInvoiceResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<DebitInvoicelist>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_AC_DEBIT_NOTE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 19);
                        cmd.Parameters.AddWithValue("@SUPP_ID", request.SUPP_ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DebitInvoicelist item = new DebitInvoicelist
                                {
                                    BILL_ID = reader["BILL_ID"] != DBNull.Value ? Convert.ToInt32(reader["BILL_ID"]) : 0,
                                    INVOICE_NO = reader["DOC_NO"] != DBNull.Value ? Convert.ToInt32(reader["DOC_NO"]) : 0,
                                    PURCH_DATE = reader["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCH_DATE"]).ToString("dd-MM-yyyy") : null,
                                    SUPP_INV_DATE = reader["SUPP_INV_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCH_DATE"]).ToString("dd-MM-yyyy") : null,
                                    SUPP_INV_NO = reader["SUPP_INV_NO"]?.ToString(),
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    PENDING_AMOUNT = reader["PENDING_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["PENDING_AMOUNT"]) : 0,
                                   

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
                response.Data = new List<DebitInvoicelist>();
            }

            return response;
        }
    }
}
