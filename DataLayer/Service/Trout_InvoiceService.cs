using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class Trout_InvoiceService:ITrout_InvoiceService
    {
        public Trout_InvoiceResponse insert(Trout_Invoice model)
        {
            Trout_InvoiceResponse RESPONSE = new Trout_InvoiceResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_TROUT_INVOICE", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 1);
                        CMD.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);   
                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        CMD.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);

                        CMD.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        CMD.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.CUST_ID ?? 0);
                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);
                        CMD.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);


                        // UDT for details
                        DataTable DT = new DataTable();
                        DT.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        DT.Columns.Add("QUANTITY", typeof(double));
                        DT.Columns.Add("PRICE", typeof(double));
                        DT.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TAX_PERC", typeof(decimal));
                        DT.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                        DT.Columns.Add("DN_DETAIL_ID", typeof(int));
                        DT.Columns.Add("CGST", typeof(decimal));
                        DT.Columns.Add("SGST", typeof(decimal));

                        foreach (var ITEM in model.SALE_DETAILS)
                        {
                            DT.Rows.Add(
                                0,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.TAXABLE_AMOUNT,
                                ITEM.TAX_PERC,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT,
                                ITEM.DN_DETAIL_ID,
                                ITEM.CGST,
                                ITEM.SGST
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
            catch (Exception ex)
            {
                RESPONSE.flag = 0;
                RESPONSE.Message = "ERROR: " + ex.Message;
            }

            return RESPONSE;
        }
        public Trout_InvoiceResponse update(Trout_InvoiceUpdate model)
        {
            Trout_InvoiceResponse RESPONSE = new Trout_InvoiceResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_TROUT_INVOICE", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 2);
                        CMD.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        CMD.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);

                        CMD.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        CMD.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.CUST_ID ?? 0);
                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);
                        CMD.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);


                        // UDT for details
                        DataTable DT = new DataTable();
                        DT.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        DT.Columns.Add("QUANTITY", typeof(double));
                        DT.Columns.Add("PRICE", typeof(double));
                        DT.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TAX_PERC", typeof(decimal));
                        DT.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                        DT.Columns.Add("DN_DETAIL_ID", typeof(int));
                        DT.Columns.Add("CGST", typeof(decimal));
                        DT.Columns.Add("SGST", typeof(decimal));

                        foreach (var ITEM in model.SALE_DETAILS)
                        {
                            DT.Rows.Add(
                                0,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.TAXABLE_AMOUNT,
                                ITEM.TAX_PERC,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT,
                                ITEM.DN_DETAIL_ID,
                                ITEM.CGST,
                                ITEM.SGST
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
            catch (Exception ex)
            {
                RESPONSE.flag = 0;
                RESPONSE.Message = "ERROR: " + ex.Message;
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
        public PendingDeliverydataResponse GetTransferData(PendingDeliverydataRequest request)
        {
            var response = new PendingDeliverydataResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PendingDeliverydata>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_TROUT_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", request.CUST_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return new PendingDeliverydataResponse
                                {
                                    flag = 0,
                                    Message = "No records found.",
                                    Data = new List<PendingDeliverydata>()
                                };
                            }

                            var transferList = new List<PendingDeliverydata>();

                            while (reader.Read())
                            {
                                var item = new PendingDeliverydata
                                {
                                    DN_DETAIL_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    ART_NO = reader["ART_NO"]?.ToString(),
                                    ARTICLE = reader["ARTICLE"]?.ToString(),
                                    DN_DATE = reader["DN_DATE"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["DN_DATE"]).ToString("dd-MM-yyyy")
                                        : null,
                                    TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value
                                        ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"])
                                        : 0
                                };

                                transferList.Add(item);
                            }

                            response.flag = 1;
                            response.Message = "Success";
                            response.Data = transferList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = new List<PendingDeliverydata>();
            }

            return response;
        }
        public Trout_InvoiceListResponse GetSaleInvoiceHeaderData()
        {
            var response = new Trout_InvoiceListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<Trout_InvoiceList>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_TROUT_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);

                        using (var reader = cmd.ExecuteReader())
                        {
                            List<Trout_InvoiceList> list = new List<Trout_InvoiceList>();

                            while (reader.Read())
                            {
                                list.Add(new Trout_InvoiceList
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                    INVOICE_NO = reader["SALE_NO"]?.ToString(),
                                    INVOICE_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    CUST_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                    CUST_NAME = reader["CUST_NAME"]?.ToString()
                                });
                            }

                            response.Data = list;
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

        public Trout_InvoiceSelectResponse GetSaleInvoiceById(int id)
        {
            Trout_InvoiceSelectResponse response = new Trout_InvoiceSelectResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<Trout_InvoiceSelect>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TROUT_INVOICE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Trout_InvoiceSelect invoice = null;
                            List<TroutSaleDetailSelect> saleDetails = new List<TroutSaleDetailSelect>();

                            while (reader.Read())
                            {
                                if (invoice == null)
                                {
                                    invoice = new Trout_InvoiceSelect
                                    {
                                        TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        INVOICE_NO = reader["SALE_NO"]?.ToString(),
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        TRANS_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        //UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                        CUST_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                        GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                        TAX_AMOUNT = reader["GST_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GST_AMOUNT"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        CUST_NAME = reader["CUST_NAME"]?.ToString(),
                                        COMPANY_NAME = reader["COMPANY_NAME"]?.ToString(),
                                        ADDRESS1 = reader["ADDRESS1"]?.ToString(),
                                        ADDRESS2 = reader["ADDRESS2"]?.ToString(),
                                        ADDRESS3 = reader["ADDRESS3"]?.ToString(),
                                        COMPANY_CODE = reader["COMPANY_CODE"]?.ToString(),
                                        EMAIL = reader["EMAIL"]?.ToString(),
                                        PHONE = reader["PHONE"]?.ToString(),
                                        CUST_CODE = reader["CUST_CODE"]?.ToString(),
                                        CUST_ADDRESS1 = reader["CUST_ADDRESS1"]?.ToString(),
                                        CUST_ADDRESS2 = reader["CUST_ADDRESS2"]?.ToString(),
                                        CUST_ADDRESS3 = reader["CUST_ADDRESS3"]?.ToString(),
                                        CUST_ZIP = reader["ZIP"]?.ToString(),
                                        CUST_CITY = reader["CITY"]?.ToString(),
                                        CUST_STATE = reader["STATE_NAME"]?.ToString(),
                                        CUST_PHONE = reader["CUST_PHONE"]?.ToString(),
                                        CUST_EMAIL = reader["CUST_EMAIL"]?.ToString(),
                                        VEHICLE_NO = reader["VEHICLE_NO"]?.ToString(),
                                        ROUND_OFF = ADO.Toboolean(reader["ROUND_OFF"]),
                                        GST_NO = reader["GST_NO"]?.ToString(),
                                        PAN_NO = reader["PAN_NO"]?.ToString(),
                                        CIN = reader["CIN"]?.ToString(),
                                        DELIVERY_ADD1 = reader["CUST_DELIVERY_ADD1"]?.ToString(),
                                        DELIVERY_ADD2 = reader["CUST_DELIVERY_ADD2"]?.ToString(),
                                        DELIVERY_ADD3 = reader["CUST_DELIVERY_ADD3"]?.ToString(),
                                        MOBILE = reader["MOBILE"]?.ToString(),
                                        SALE_DETAILS = new List<TroutSaleDetailSelect>()
                                    };
                                }

                                // Add to SALE_DETAILS
                                saleDetails.Add(new TroutSaleDetailSelect
                                {
                                    DN_DETAIL_ID = reader["DN_DETAIL_ID"] != DBNull.Value ? Convert.ToInt32(reader["DN_DETAIL_ID"]) : (int?)null,
                                    ART_NO = reader["TRANSFER_NO"]?.ToString(),
                                    DN_DATE = reader["TRANSFER_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANSFER_DATE"]).ToString("dd-MM-yyyy") : null,
                                    ARTICLE = reader["ARTICLE"]?.ToString(),
                                    TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"]) : 0,

                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDouble(reader["QUANTITY"]) : (double?)null,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToDouble(reader["PRICE"]) : 0,
                                    TAXABLE_AMOUNT = reader["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAXABLE_AMOUNT"]) : 0,
                                    TAX_PERC = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0,
                                    CGST = reader["CGST"] != DBNull.Value ? Convert.ToDecimal(reader["CGST"]) : 0,
                                    SGST = reader["SGST"] != DBNull.Value ? Convert.ToDecimal(reader["SGST"]) : 0,
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
        public Trout_InvoiceResponse commit(Trout_InvoiceUpdate model)
        {
            Trout_InvoiceResponse RESPONSE = new Trout_InvoiceResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_TROUT_INVOICE", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 3);
                        CMD.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        CMD.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);

                        CMD.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        CMD.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        CMD.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.CUST_ID ?? 0);
                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);
                        CMD.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);


                        // UDT for details
                        DataTable DT = new DataTable();
                        DT.Columns.Add("TRANSFER_SUMMARY_ID", typeof(int));
                        DT.Columns.Add("QUANTITY", typeof(double));
                        DT.Columns.Add("PRICE", typeof(double));
                        DT.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TAX_PERC", typeof(decimal));
                        DT.Columns.Add("TAX_AMOUNT", typeof(decimal));
                        DT.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                        DT.Columns.Add("DN_DETAIL_ID", typeof(int));
                        DT.Columns.Add("CGST", typeof(decimal));
                        DT.Columns.Add("SGST", typeof(decimal));

                        foreach (var ITEM in model.SALE_DETAILS)
                        {
                            DT.Rows.Add(
                                0,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.TAXABLE_AMOUNT,
                                ITEM.TAX_PERC,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT,
                                ITEM.DN_DETAIL_ID,
                                ITEM.CGST,
                                ITEM.SGST
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
            catch (Exception ex)
            {
                RESPONSE.flag = 0;
                RESPONSE.Message = "ERROR: " + ex.Message;
            }

            return RESPONSE;
        }
        public TroutInvResponse GetInvoiceNo()
        {
            TroutInvResponse res = new TroutInvResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO + 1 FROM TB_AC_TRANS_HEADER 
                     INNER JOIN TB_SALE_HEADER ON TB_AC_TRANS_HEADER.TRANS_ID=TB_SALE_HEADER.TRANS_ID
                     LEFT JOIN  TB_CUSTOMER ON TB_CUSTOMER.ID=TB_SALE_HEADER.CUSTOMER_ID
                     WHERE TB_AC_TRANS_HEADER.TRANS_TYPE = 25 AND TB_CUSTOMER.CUST_TYPE=3
                     ORDER BY TB_AC_TRANS_HEADER.TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.INVOICE_NO = result != null ? Convert.ToInt32(result) : 0;
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
        public Trout_InvoiceResponse Delete(int id)
        {
            Trout_InvoiceResponse res = new Trout_InvoiceResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TROUT_INVOICE";

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
        public List<TroutCust_stateName> Getcustlist()
        {
            var TroutCust_stateName = new List<TroutCust_stateName>();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TROUT_INVOICE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new TroutCust_stateName
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    DESCRIPTION = reader["CUST_NAME"] != DBNull.Value ? reader["CUST_NAME"].ToString() : string.Empty,
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    STATE_NAME = reader["STATE_NAME"] != DBNull.Value ? reader["STATE_NAME"].ToString() : string.Empty
                                };
                                TroutCust_stateName.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return TroutCust_stateName;
        }
    }
}
