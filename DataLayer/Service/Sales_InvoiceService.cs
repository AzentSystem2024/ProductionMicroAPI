using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class Sales_InvoiceService: ISales_InvoiceService
    {
        public SalesInvoiceResponse insert(Sales_Invoice model)
        {
            SalesInvoiceResponse RESPONSE = new SalesInvoiceResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_TB_DEL_SALE_INVOICE", CONNECTION))
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
                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.CUST_ID ?? 0);
                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // SALE DETAIL UDT
                        DataTable DT = new DataTable();
                        DT.Columns.Add("DELIVERY_NOTE_ID", typeof(int));
                        DT.Columns.Add("QUANTITY", typeof(double));
                        DT.Columns.Add("PRICE", typeof(double));
                        DT.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TAX_PERC", typeof(decimal));
                        DT.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        foreach (var ITEM in model.SALE_DETAILS)
                        {
                            DT.Rows.Add(
                                ITEM.DELIVERY_NOTE_ID,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.AMOUNT,
                                ITEM.GST,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT
                            );
                        }

                        SqlParameter TVP_PARAM = CMD.Parameters.AddWithValue("@UDT_DEL_SALE_DETAIL", DT);
                        TVP_PARAM.SqlDbType = SqlDbType.Structured;
                        TVP_PARAM.TypeName = "UDT_DEL_SALE_DETAIL";

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
        public SalesInvoiceResponse Update(Sale_InvoiceUpdate model)
        {
            SalesInvoiceResponse response = new SalesInvoiceResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DEL_SALE_INVOICE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", model.CUST_ID ?? 0);
                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // Prepare UDT (User Defined Table) for Sale Detail
                        DataTable dt = new DataTable();
                        dt.Columns.Add("DELIVERY_NOTE_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double)); 
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        foreach (var item in model.SALE_DETAILS)
                        {
                            dt.Rows.Add(
                                item.DELIVERY_NOTE_ID ?? 0,
                                item.TOTAL_PAIR_QTY,
                                item.PRICE ?? 0,
                                item.AMOUNT ?? 0,
                                item.GST ?? 0,
                                item.TAX_AMOUNT ?? 0,
                                item.TOTAL_AMOUNT ?? 0
                            );
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_DEL_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_DEL_SALE_DETAIL";

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
        public DeliveryGridResponse GetTransferData(DeliveryInvoiceRequest request)
        {
            var response = new DeliveryGridResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<DeliveryGridItem>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_TB_DEL_SALE_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 5);
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
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", request.CUST_ID);
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
                        DataTable dt = new DataTable();
                        dt.Columns.Add("DELIVERY_NOTE_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double));
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_DEL_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_DEL_SALE_DETAIL";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new DeliveryGridItem
                                {
                                    DELIVERY_NOTE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                    DELIVERY_DATE = reader["DN_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DN_DATE"]).ToString("dd-MM-yyyy") : null,
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    QUANTITY = reader["TOTAL_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_QTY"]) : 0
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
        public SalesInvoiceHeaderResponse GetSaleInvoiceHeaderData()
        {
            SalesInvoiceHeaderResponse res = new SalesInvoiceHeaderResponse();
            List<SalesInvoiceHeader> list = new List<SalesInvoiceHeader>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_DEL_SALE_INVOICE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 🔹 Header Parameters
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
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
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", 0);
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

                    // 🔹 Empty UDT Table
                    DataTable dt = new DataTable();
                    dt.Columns.Add("DELIVERY_NOTE_ID", typeof(int));
                    dt.Columns.Add("QUANTITY", typeof(double));
                    dt.Columns.Add("PRICE", typeof(double));
                    dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TAX_PERC", typeof(decimal));
                    dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                    SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_DEL_SALE_DETAIL", dt);
                    tvp.SqlDbType = SqlDbType.Structured;
                    tvp.TypeName = "UDT_DEL_SALE_DETAIL";

                    // 🔹 Fill DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable resultTable = new DataTable();
                    da.Fill(resultTable);

                    // 🔹 Map to Model
                    foreach (DataRow dr in resultTable.Rows)
                    {
                        SalesInvoiceHeader obj = new SalesInvoiceHeader
                        {
                            TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]),
                            TRANS_TYPE = ADO.ToInt32(dr["TRANS_TYPE"]),
                            TRANS_STATUS = dr["TRANS_STATUS"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["TRANS_STATUS"]),
                            SALE_NO = ADO.ToString(dr["SALE_NO"]),
                            SALE_DATE = dr["SALE_DATE"] == DBNull.Value ? null : Convert.ToDateTime(dr["SALE_DATE"]).ToString("dd-MM-yyyy"),
                            CUST_ID = ADO.ToInt32(dr["CUSTOMER_ID"]),
                            GROSS_AMOUNT = dr["GROSS_AMOUNT"] == DBNull.Value ? 0 : Convert.ToSingle(dr["GROSS_AMOUNT"]),
                            TAX_AMOUNT = dr["TAX_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TAX_AMOUNT"]),
                            NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToSingle(dr["NET_AMOUNT"]),
                            CUST_NAME = ADO.ToString(dr["CUST_NAME"])
                        };

                        list.Add(obj);
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = list;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = "Error: " + ex.Message;
                    res.Data = new List<SalesInvoiceHeader>();
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return res;
        }
        public SalesInvselectResponse GetSaleInvoiceById(int id)
        {
            SalesInvselectResponse response = new SalesInvselectResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<SalesInvoiceHeaderSelect>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DEL_SALE_INVOICE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);

                        // Add all required default parameters
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
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", 0);
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
                        dt.Columns.Add("DELIVERY_NOTE_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double));
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_DEL_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_DEL_SALE_DETAIL";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            SalesInvoiceHeaderSelect invoice = null;
                            List<SalesInvoiceDetailUpdate> saleDetails = new List<SalesInvoiceDetailUpdate>();

                            while (reader.Read())
                            {
                                if (invoice == null)
                                {
                                    invoice = new SalesInvoiceHeaderSelect
                                    {
                                        TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        SALE_NO = reader["SALE_NO"]?.ToString(),
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        SALE_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        CUST_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                        GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                        TAX_AMOUNT = reader["GST_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GST_AMOUNT"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                        SALE_DETAILS = new List<SalesInvoiceDetailUpdate>()
                                    };
                                }

                                // Add to SALE_DETAILS
                                saleDetails.Add(new SalesInvoiceDetailUpdate
                                {
                                    DELIVERY_NOTE_ID = reader["TRANSFER_SUMMARY_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANSFER_SUMMARY_ID"]) : (int?)null,
                                    ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                    DELIVERY_DATE = reader["DN_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DN_DATE"]).ToString("dd-MM-yyyy") : null,
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    QUANTITY = reader["TOTAL_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_QTY"]) : 0,
                                    //QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDouble(reader["QUANTITY"]) : (double?)null,

                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToDouble(reader["PRICE"]) : 0,
                                    AMOUNT = reader["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAXABLE_AMOUNT"]) : 0,
                                    GST = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0
                                });
                            }

                            if (invoice != null)
                            {
                                invoice.SALE_DETAILS = saleDetails;
                                response.Data.Add(invoice);
                                response.flag = 1;
                                response.Message = "Success";
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
        public int Delete(int id)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_DEL_SALE_INVOICE", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@TRANS_ID", id);
                    object result = cmd.ExecuteScalar();
                    Int32 deletedId = ADO.ToInt32(result);
                    objtrans.Commit();
                    return deletedId;
                }
                catch (Exception ex)
                {
                    objtrans.Rollback();
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        public SalesInvoiceResponse Approve(Sale_InvoiceUpdate model)
        {
            SalesInvoiceResponse RESPONSE = new SalesInvoiceResponse();
            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();
                    using (SqlCommand CMD = new SqlCommand("SP_TB_DEL_SALE_INVOICE", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.Parameters.AddWithValue("@ACTION", 3);
                        CMD.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? 0);
                        CMD.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE);
                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        CMD.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        CMD.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        CMD.Parameters.AddWithValue("@TRANS_STATUS", 5); // Approved status
                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.CUST_ID ?? 0);
                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // Prepare UDT (User Defined Table) for Sale Detail
                        DataTable dt = new DataTable();
                        dt.Columns.Add("DELIVERY_NOTE_ID", typeof(int));
                        dt.Columns.Add("QUANTITY", typeof(double));
                        dt.Columns.Add("PRICE", typeof(double));
                        dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TAX_PERC", typeof(decimal));
                        dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));

                        foreach (var item in model.SALE_DETAILS)
                        {
                            dt.Rows.Add(
                                item.DELIVERY_NOTE_ID ?? 0,
                                item.TOTAL_PAIR_QTY ?? 0,
                                item.PRICE ?? 0,
                                item.AMOUNT ?? 0,
                                item.GST ?? 0,
                                item.TAX_AMOUNT ?? 0,
                                item.TOTAL_AMOUNT ?? 0
                            );
                        }

                        SqlParameter tvp = CMD.Parameters.AddWithValue("@UDT_DEL_SALE_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_DEL_SALE_DETAIL";

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

    }
}
