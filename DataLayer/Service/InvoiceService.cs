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
                        CMD.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);   // ok
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

                        CMD.Parameters.AddWithValue("@STORE_AUTO_ID", model.STORE_AUTO_ID ?? 0);
                        CMD.Parameters.AddWithValue("@JOB_ID", model.JOB_ID ?? 0);

                        CMD.Parameters.AddWithValue("@SALE_DATE", ParseDate(model.SALE_DATE));
                        CMD.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);

                        CMD.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.DISTRIBUTOR_ID ?? 0);

                        CMD.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@TAX_AMOUNT", model.GST_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        CMD.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);

                        // ⭐ Required new parameter
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
                                (object?)ITEM.TRANSFER_SUMMARY_ID ?? DBNull.Value,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.AMOUNT,
                                ITEM.GST,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT,
                                ITEM.DN_DETAIL_ID, ITEM.CGST, ITEM.SGST
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

        public InvoiceResponse Update(InvoiceUpdate model)
        {
            InvoiceResponse response = new InvoiceResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALE_INVOICE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? 0);
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
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);

                        cmd.Parameters.AddWithValue("@SALE_DATE", ParseDate(model.SALE_DATE));
                        cmd.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", model.DISTRIBUTOR_ID ?? 0);
                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);

                        // Prepare UDT (User Defined Table) for Sale Detail
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
                                (object?)ITEM.TRANSFER_SUMMARY_ID ?? DBNull.Value,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.AMOUNT,
                                ITEM.GST,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT,
                                ITEM.DN_DETAIL_ID, ITEM.CGST, ITEM.SGST
                            );
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SALE_DETAIL", DT);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SALE_DETAIL";

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
        //public TransferGridResponse GetTransferData(TransferInvoiceRequest request)
        //{
        //    var response = new TransferGridResponse
        //    {
        //        flag = 0,
        //        Message = "Failed",
        //        Data = new List<TransferGridItem>()
        //    };

        //    try
        //    {
        //        using (var conn = ADO.GetConnection())
        //        {
        //            if (conn.State == ConnectionState.Closed)
        //                conn.Open();

        //            using (var cmd = new SqlCommand("SP_SALE_INVOICE", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@ACTION", 5);
        //                cmd.Parameters.AddWithValue("@CUSTOMER_ID", request.CUST_ID);

        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    if (!reader.HasRows)
        //                    {
        //                        return new TransferGridResponse
        //                        {
        //                            flag = 0,
        //                            Message = "No records found.",
        //                            Data = new List<TransferGridItem>()
        //                        };
        //                    }

        //                    // Detect result type based on columns returned
        //                    var columnNames = Enumerable.Range(0, reader.FieldCount)
        //                        .Select(reader.GetName)
        //                        .ToList();

        //                    // 🔹 CASE 1: Transfer Out Summary (CUST_TYPE=1)
        //                    if (columnNames.Contains("TRANSFER_NO"))
        //                    {
        //                        var transferResponse = new TransferGridResponse
        //                        {
        //                            flag = 1,
        //                            Message = "Success",
        //                            Data = new List<TransferGridItem>()
        //                        };

        //                        while (reader.Read())
        //                        {
        //                            transferResponse.Data.Add(new TransferGridItem
        //                            {
        //                                TRANSFER_SUMMARY_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
        //                                TRANSFER_NO = reader["TRANSFER_NO"]?.ToString(),
        //                                TRANSFER_DATE = reader["TRANSFER_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANSFER_DATE"]).ToString("dd-MM-yyyy") : null,
        //                                ARTICLE = reader["ARTICLE"]?.ToString(),
        //                                CUST_TYPE = reader["CUST_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["CUST_TYPE"]) : (int?)null,
        //                                TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"]) : 0
        //                            });
        //                        }
        //                        return transferResponse;
        //                    }
        //                    // 🔹 CASE 2: Delivery Note List (CUST_TYPE=2)
        //                    else if (columnNames.Contains("ITEM_CODE"))
        //                    {
        //                        var deliveryResponse = new TransferGridResponse
        //                        {
        //                            flag = 1,
        //                            Message = "Success",
        //                            Data = new List<TransferGridItem>()
        //                        };

        //                        while (reader.Read())
        //                        {
        //                            deliveryResponse.Data.Add(new TransferGridItem
        //                            {
        //                                TRANSFER_SUMMARY_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
        //                                TRANSFER_NO = reader["ITEM_CODE"]?.ToString(),
        //                                ARTICLE = reader["DESCRIPTION"]?.ToString(),
        //                                CUST_TYPE = reader["CUST_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["CUST_TYPE"]) : (int?)null,
        //                                TRANSFER_DATE = reader["DN_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DN_DATE"]).ToString("dd-MM-yyyy") : null,
        //                                TOTAL_PAIR_QTY = reader["TOTAL_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_QTY"]) : 0
        //                            });
        //                        }
        //                        return deliveryResponse;
        //                    }
        //                    // If neither matched
        //                    return new TransferGridResponse
        //                    {
        //                        flag = 0,
        //                        Message = "Unknown result structure from SP.",
        //                        Data = new List<TransferGridItem>()
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new TransferGridResponse
        //        {
        //            flag = 0,
        //            Message = "Error: " + ex.Message,
        //            Data = new List<TransferGridItem>()
        //        };
        //    }
        //}
        public TransferGridResponse GetTransferData(TransferInvoiceRequest request)
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
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", request.CUST_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return new TransferGridResponse
                                {
                                    flag = 0,
                                    Message = "No records found.",
                                    Data = new List<TransferGridItem>()
                                };
                            }

                            var transferList = new List<TransferGridItem>();

                            while (reader.Read())
                            {
                                var item = new TransferGridItem
                                {
                                    DN_DETAIL_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    TRANSFER_NO = reader["ART_NO"]?.ToString(),
                                    ARTICLE = reader["ARTICLE"]?.ToString(),
                                    TRANSFER_DATE = reader["DN_DATE"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["DN_DATE"]).ToString("dd-MM-yyyy")
                                        : null,
                                    TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value
                                        ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"])
                                        : 0,
                                    PRICE = reader["PACK_PRICE"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["PACK_PRICE"])
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
                response.Data = new List<TransferGridItem>();
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
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);
                       

                        using (var reader = cmd.ExecuteReader())
                        {
                            Dictionary<int, InvoiceHeader> invoiceMap = new Dictionary<int, InvoiceHeader>();

                            while (reader.Read())
                            {
                                int transId = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;

                                // Create header if not exists
                                if (!invoiceMap.ContainsKey(transId))
                                {
                                    var header = new InvoiceHeader
                                    {
                                        TRANS_ID = transId,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                        SALE_NO = reader["SALE_NO"]?.ToString(),
                                        SALE_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        DISTRIBUTOR_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                        GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                        GST_AMOUNT = reader["GST_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GST_AMOUNT"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                        CUST_NAME = reader["CUST_NAME"]?.ToString(),
                                        VEHICLE_NO = reader["VEHICLE_NO"]?.ToString(),
                                        SALE_DETAILS = new List<SaleDetailUpdate>()
                                    };

                                    invoiceMap[transId] = header;
                                }

                                // Add detail row
                                invoiceMap[transId].SALE_DETAILS.Add(new SaleDetailUpdate
                                {
                                    DN_DETAIL_ID = reader["DN_DETAIL_ID"] != DBNull.Value ? Convert.ToInt32(reader["DN_DETAIL_ID"]) : (int?)null,
                                    TRANSFER_NO = reader["TRANSFER_NO"]?.ToString(),
                                    TRANSFER_DATE = reader["TRANSFER_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANSFER_DATE"]).ToString("dd-MM-yyyy") : null,
                                    ARTICLE = reader["ARTICLE"]?.ToString(),
                                    TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToDouble(reader["PRICE"]) : 0,
                                    AMOUNT = reader["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAXABLE_AMOUNT"]) : 0,
                                    GST = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0
                                });
                            }

                            response.Data = invoiceMap.Values.ToList();
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


        public InvoiceHeaderSelectResponse GetSaleInvoiceById(int id)
        {
            InvoiceHeaderSelectResponse response = new InvoiceHeaderSelectResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<InvoiceHeaderSelect>()
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

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            InvoiceHeaderSelect invoice = null;
                            List<SaleDetailUpdate> saleDetails = new List<SaleDetailUpdate>();

                            while (reader.Read())
                            {
                                if (invoice == null)
                                {
                                    invoice = new InvoiceHeaderSelect
                                    {
                                        TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        SALE_NO = reader["SALE_NO"]?.ToString(),
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        SALE_DATE = reader["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        //UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                        DISTRIBUTOR_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
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
                                        ROUND_OFF = reader["ROUND_OFF"] != DBNull.Value ? Convert.ToBoolean(reader["ROUND_OFF"]) : false,
                                        SALE_DETAILS = new List<SaleDetailUpdate>()
                                    };
                                }

                                // Add to SALE_DETAILS
                                saleDetails.Add(new SaleDetailUpdate
                                {
                                    TRANSFER_SUMMARY_ID = reader["TRANSFER_SUMMARY_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANSFER_SUMMARY_ID"]) : (int?)null,
                                    TRANSFER_NO = reader["TRANSFER_NO"]?.ToString(),
                                    TRANSFER_DATE = reader["TRANSFER_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANSFER_DATE"]).ToString("dd-MM-yyyy") : null,
                                    ARTICLE = reader["ARTICLE"]?.ToString(),
                                    TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"]) : 0,

                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDouble(reader["QUANTITY"]) : (double?)null,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToDouble(reader["PRICE"]) : 0,
                                    AMOUNT = reader["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAXABLE_AMOUNT"]) : 0,
                                    GST = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0,
                                    DN_DETAIL_ID = reader["DN_DETAIL_ID"] != DBNull.Value ? Convert.ToInt32(reader["DN_DETAIL_ID"]) : (int?)null,
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



        public InvoiceResponse commit(InvoiceUpdate model)
        {
            InvoiceResponse response = new InvoiceResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALE_INVOICE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
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
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);

                        cmd.Parameters.AddWithValue("@SALE_DATE", ParseDate(model.SALE_DATE));
                        cmd.Parameters.AddWithValue("@SALE_REF_NO", model.SALE_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", model.DISTRIBUTOR_ID ?? 0);
                        cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);

                        // Prepare UDT (User Defined Table) for Sale Detail

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
                                (object?)ITEM.TRANSFER_SUMMARY_ID ?? DBNull.Value,
                                ITEM.QUANTITY,
                                ITEM.PRICE,
                                ITEM.AMOUNT,
                                ITEM.GST,
                                ITEM.TAX_AMOUNT,
                                ITEM.TOTAL_AMOUNT,
                                ITEM.DN_DETAIL_ID, ITEM.CGST, ITEM.SGST
                            );
                        }
                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SALE_DETAIL", DT);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SALE_DETAIL";

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

        public InvResponse GetInvoiceNo()
        {
            InvResponse res = new InvResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO FROM TB_AC_TRANS_HEADER 
                    INNER JOIN TB_SALE_HEADER ON TB_AC_TRANS_HEADER.TRANS_ID=TB_SALE_HEADER.TRANS_ID
                    INNER JOIN TB_SALE_DETAIL ON TB_SALE_HEADER.ID=TB_SALE_DETAIL.SALE_ID
                    LEFT JOIN TB_DN_DETAIL ON TB_SALE_DETAIL.DN_DETAIL_ID=TB_DN_DETAIL.ID
                    LEFT JOIN  TB_DN_HEADER ON TB_DN_DETAIL.DN_ID=TB_DN_HEADER.ID
                    WHERE TB_AC_TRANS_HEADER.TRANS_TYPE = 25 AND TB_DN_HEADER.DN_TYPE=2
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
        public InvoiceResponse Delete(int id)
        {
            InvoiceResponse res = new InvoiceResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_SALE_INVOICE";

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
