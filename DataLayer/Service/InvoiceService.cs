using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class InvoiceService:IInvoiceService
    {
        public InvoiceResponse insert(Invoice model)
        {
            InvoiceResponse RESPONSE = new InvoiceResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_SALE_INVOICE", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 1);
                        CMD.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        CMD.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE);
                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        CMD.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        CMD.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        CMD.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        CMD.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        CMD.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? 0);
                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        CMD.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        CMD.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        CMD.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        CMD.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        CMD.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        CMD.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        CMD.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        CMD.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        CMD.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);
                        CMD.Parameters.AddWithValue("@ADD_TIME", ParseDate(model.ADD_TIME));
                        CMD.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@BILL_NO", model.BILL_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@STORE_AUTO_ID", model.STORE_AUTO_ID ?? 0);
                        CMD.Parameters.AddWithValue("@JOB_ID", model.JOB_ID ?? 0);

                        CMD.Parameters.AddWithValue("@SALE_DATE", ParseDate(model.SALE_DATE));
                        CMD.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        CMD.Parameters.AddWithValue("@DISTRIBUTOR_ID", model.DISTRIBUTOR_ID ?? 0);
                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TRANSFER_NO", model.TRANSFER_NO ?? string.Empty);

                        // SALE DETAIL UDT
                        DataTable DT = new DataTable();
                        DT.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        DT.Columns.Add("QUANTITY", typeof(double));
                        DT.Columns.Add("PRICE", typeof(double));
                        DT.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TAX_PERC", typeof(decimal));
                        DT.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        foreach (var ITEM in model.SALE_DETAILS)
                        {
                            DT.Rows.Add(
                                ITEM.TRANSFER_SUMMARY_ID,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.TAXABLE_AMOUNT,
                                ITEM.TAX_PERC,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT
                            );
                        }

                        SqlParameter TVP_PARAM = CMD.Parameters.AddWithValue("@UDT_SALE_DETAIL", DT);
                        TVP_PARAM.SqlDbType = SqlDbType.Structured;
                        TVP_PARAM.TypeName = "UDT_SALE_DETAIL";

                        CMD.ExecuteNonQuery();

                        RESPONSE.flag = 1;
                        RESPONSE.Message = "Success.";
                    }
                }
            }
            catch (Exception EX)
            {
                RESPONSE.flag = 0;
                RESPONSE.Message = "ERROR: " + EX.Message;
            }

            return RESPONSE;
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
        public TransferGridResponse GetTransferData()
        {
            var response = new TransferGridResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<TransferGridItem>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_SALE_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 5);

                        // Provide required parameters for SP even if not used (prevent SQL error)
                        cmd.Parameters.AddWithValue("@TRANSFER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", 0);
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
                        cmd.Parameters.AddWithValue("@UNIT_ID", 0);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", 0);
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
                        cmd.Parameters.AddWithValue("@BILL_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_AUTO_ID", 0);
                        cmd.Parameters.AddWithValue("@JOB_ID", 0);
                        cmd.Parameters.AddWithValue("@SALE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SALE_REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", 0);
                        // cmd.Parameters.AddWithValue("@UDT_SALE_DETAIL", DBNull.Value);
                        DataTable dt = new DataTable();
                        dt.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double));
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        // Leave dt empty — just pass the structure
                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SALE_DETAIL";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new TransferGridItem
                                {
                                    TRANSFER_SUMMARY_ID = reader["TRANSFER_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANSFER_ID"]) : 0,
                                    TRANSFER_NO = reader["TRANSFER_NO"]?.ToString(),
                                    TRANSFER_DATE = reader["TRANSFER_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANSFER_DATE"]).ToString("dd-MM-yyyy") : null,
                                    ARTICLE = reader["ARTICLE"]?.ToString(),
                                    TOTAL_PAIR_QTY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDouble(reader["QUANTITY"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToDouble(reader["PRICE"]) : 0,
                                    AMOUNT = reader["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAXABLE_AMOUNT"]) : 0,
                                    GST = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0
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
        public InvoiceHeaderResponse GetSaleInvoiceHeaderData()
        {
            var response = new InvoiceHeaderResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<InvoiceHeader>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_SALE_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0); 
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);

                        // Dummy/default values for remaining SP params
                        cmd.Parameters.AddWithValue("@TRANSFER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", 0);
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
                        cmd.Parameters.AddWithValue("@UNIT_ID", 0);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", 0);
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
                        cmd.Parameters.AddWithValue("@BILL_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_AUTO_ID", 0);
                        cmd.Parameters.AddWithValue("@JOB_ID", 0);
                        cmd.Parameters.AddWithValue("@SALE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SALE_REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", 0);

                        // UDT: empty structure
                        DataTable dt = new DataTable();
                        dt.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double));
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SALE_DETAIL";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var header = new InvoiceHeader
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    SALE_NO = reader["SALE_NO"]?.ToString(),
                                    SALE_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    DISTRIBUTOR_ID = reader["DISTRIBUTOR_ID"] != DBNull.Value ? Convert.ToInt32(reader["DISTRIBUTOR_ID"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                };

                                response.Data.Add(header);
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

        public InvoiceHeaderResponse GetSaleInvoiceById(int id)
        {
            InvoiceHeaderResponse response = new InvoiceHeaderResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<InvoiceHeader>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALE_INVOICE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25); 

                        // Pass remaining required parameters with defaults
                        cmd.Parameters.AddWithValue("@TRANSFER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", 0);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", 0);
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
                        cmd.Parameters.AddWithValue("@UNIT_ID", 0);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", 0);
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
                        cmd.Parameters.AddWithValue("@BILL_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@STORE_AUTO_ID", 0);
                        cmd.Parameters.AddWithValue("@JOB_ID", 0);
                        cmd.Parameters.AddWithValue("@SALE_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SALE_REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", 0);

                        // UDT Placeholder
                        DataTable dt = new DataTable();
                        dt.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double));
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SALE_DETAIL";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var invoice = new InvoiceHeader
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    SALE_NO = reader["SALE_NO"]?.ToString(),
                                    SALE_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    DISTRIBUTOR_ID = reader["DISTRIBUTOR_ID"] != DBNull.Value ? Convert.ToInt32(reader["DISTRIBUTOR_ID"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                };

                                response.Data.Add(invoice);
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
