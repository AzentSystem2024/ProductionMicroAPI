using MicroApi.Models;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Globalization;
using System.Linq;
using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace MicroApi.DataLayer.Service
{
    public class MiscReceiptService:IMiscReceiptService
    {
        public MiscReceiptResponse Insert(MiscReceipt model)
        {
            MiscReceiptResponse response = new MiscReceiptResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_MISC_RECEIPT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Header parameters
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
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
                                detail.HEAD_ID ?? 0,
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

        public MiscReceiptResponse Update(MiscReceiptUpdate model)
        {
            MiscReceiptResponse response = new MiscReceiptResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_MISC_RECEIPT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Header parameters
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
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
                                detail.HEAD_ID ?? 0,
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
        public MiscReceiptListResponse GetReceiptList()
        {
            MiscReceiptListResponse res = new MiscReceiptListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<MiscReceiptListItem>()
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_MISC_RECEIPT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ACTION", 0);

                        // Pass required default values for mandatory SP parameters
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 2);
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            var headers = new Dictionary<int, MiscReceiptListItem>();

                            while (reader.Read())
                            {
                                int transId = Convert.ToInt32(reader["TRANS_ID"]);

                                if (!headers.ContainsKey(transId))
                                {
                                    headers[transId] = new MiscReceiptListItem
                                    {
                                        TRANS_ID = transId,
                                        VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"]?.ToString(),
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value
                                    ? Convert.ToInt32(reader["TRANS_TYPE"])
                                    : 0,
                                        TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value
                                    ? Convert.ToInt32(reader["TRANS_STATUS"])
                                    : 0,
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        DETAILS = new List<MiscListDetail>()
                                    };
                                }

                                headers[transId].DETAILS.Add(new MiscListDetail
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
        public MiscReceiptResponse GetMiscReceiptById(int id)
        {
            MiscReceiptResponse res = new MiscReceiptResponse();

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
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 2);

                        using (var reader = cmd.ExecuteReader())
                        {
                            MiscReceiptViewHeader header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new MiscReceiptViewHeader
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("yyyy-MM-dd") : null,
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        TRANS_TYPE = Convert.ToInt32(reader["TRANS_TYPE"]),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        IS_APPROVED = reader["TRANS_STATUS"] != DBNull.Value && Convert.ToInt32(reader["TRANS_STATUS"]) == 5,
                                        DETAILS = new List<MiscListDetail>()
                                    };
                                }

                                header.DETAILS.Add(new MiscListDetail
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
    }
}
