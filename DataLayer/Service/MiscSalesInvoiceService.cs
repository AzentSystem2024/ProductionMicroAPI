using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class MiscSalesInvoiceService:IMiscSalesInvoiceService
    {
        public MiscSalesInvoiceResponse insert(MiscSalesInvoiceSave model)
        {
            MiscSalesInvoiceResponse RESPONSE = new MiscSalesInvoiceResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_MISC_SALE_INVOICE", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 1);

                        CMD.Parameters.AddWithValue("@ID", model.ID);

                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);

                        CMD.Parameters.AddWithValue("@INVOICE_NO", model.INVOICE_NO);
                        CMD.Parameters.AddWithValue("@INVOICE_DATE", model.INVOICE_DATE);

                        CMD.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        CMD.Parameters.AddWithValue("@REF_DATE", model.REF_DATE);


                        CMD.Parameters.AddWithValue("@CUSTOMER_ID", model.CUSTOMER_ID ?? 0);
                        CMD.Parameters.AddWithValue("@TPA_ID", model.TPA_ID ?? 0);
                        CMD.Parameters.AddWithValue("@ENCOUNTER_TYPE", model.ENCOUNTER_TYPE);
                        CMD.Parameters.AddWithValue("@PATIENT_ID", model.PATIENT_ID);
                        CMD.Parameters.AddWithValue("@PATIENT_NAME", model.PATIENT_NAME);

                        CMD.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                        CMD.Parameters.AddWithValue("@USER_ID", model.USER_ID);


                        // ⭐ Required new parameter
                        CMD.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);


                        // UDT for details
                        DataTable DT = new DataTable();
                        DT.Columns.Add("ITEM_ID", typeof(int));
                        DT.Columns.Add("CLINICIAN", typeof(string));
                        DT.Columns.Add("ORDERING_CLINICIAN", typeof(string));
                        DT.Columns.Add("DEPARTMENT_ID", typeof(int));
                        DT.Columns.Add("SUB_DEPARTMENT_ID", typeof(int));
                        DT.Columns.Add("QUANTITY", typeof(decimal));
                        DT.Columns.Add("DURATION", typeof(decimal));
                        DT.Columns.Add("GROSS_AMOUNT", typeof(decimal));

                        DT.Columns.Add("PATIENT_SHARE", typeof(decimal));
                        DT.Columns.Add("VAT_CODE", typeof(string));
                        DT.Columns.Add("VAT_CLASS_ID", typeof(int));
                        DT.Columns.Add("VAT_PERC", typeof(decimal));
                        DT.Columns.Add("VAT_AMOUNT", typeof(decimal));
                        DT.Columns.Add("NET_AMOUNT", typeof(decimal));

                        foreach (var ITEM in model.DETAILS)
                        {
                            DT.Rows.Add(
                                ITEM.ITEM_ID,
                                ITEM.CLINICIAN,
                                ITEM.ORDERING_CLINICIAN,
                                ITEM.DEPARTMENT_ID,
                                ITEM.SUB_DEPARTMENT_ID,
                                ITEM.QUANTITY,
                                ITEM.DURATION, 
                                ITEM.GROSS_AMOUNT, 
                                ITEM.PATIENT_SHARE,
                                ITEM.VAT_CODE,
                                ITEM.VAT_CLASS_ID,
                                ITEM.VAT_PERC,
                                ITEM.VAT_AMOUNT,
                                ITEM.NET_AMOUNT
                            );
                        }

                        SqlParameter TVP_PARAM = CMD.Parameters.AddWithValue("@UDT_MISC_SALE_INVOICE_DETAIL", DT);
                        TVP_PARAM.SqlDbType = SqlDbType.Structured;
                        TVP_PARAM.TypeName = "UDT_MISC_SALE_INVOICE_DETAIL";

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
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.SALE_DATE));
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
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

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
                                    ARTICLE = reader["DESCRIPTION"]?.ToString(),
                                    TRANSFER_DATE = reader["DN_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DN_DATE"]).ToString("dd-MM-yyyy") : null,
                                    TOTAL_PAIR_QTY = reader["TOTAL_PAIR_QTY"] != DBNull.Value ? Convert.ToDouble(reader["TOTAL_PAIR_QTY"]) : 0,
                                    PRICE = reader["PACK_PRICE"] != DBNull.Value ? Convert.ToDecimal(reader["PACK_PRICE"]) :0,
                                    GST_PERC = reader["GST_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["GST_PERC"]) : 0,
                                    HSN_CODE = reader["HSN_CODE"] == DBNull.Value ? null : reader["HSN_CODE"].ToString(),
                                    REF_NO = reader["REF_NO"] == DBNull.Value ? null : reader["REF_NO"].ToString(),
                                    DN_NO = reader["DN_NO"]?.ToString(),

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

        public MiscSalesInvoiceResponse getMisSalesInvoiceData(InvoiceListRequest request)
        {
            var response = new MiscSalesInvoiceResponse
            {
                flag = 0,
                Message = "Failed",
                data = new List<MiscSalesInvoiceLookupData>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_MISC_SALE_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@DATE_FROM",
                            request.DATE_FROM == null ? (object)DBNull.Value : Convert.ToDateTime(request.DATE_FROM));
                        cmd.Parameters.AddWithValue("@DATE_TO",
                            request.DATE_TO == null ? (object)DBNull.Value : Convert.ToDateTime(request.DATE_TO));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.data.Add(new MiscSalesInvoiceLookupData
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,

                                    // SALE_NO is string in DB → keep as string (IMPORTANT)
                                    DOC_NO = reader["SALE_NO"]?.ToString(),

                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0,

                                    SALE_DATE = reader["SALE_DATE"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["SALE_DATE"])
                                        : DateTime.MinValue,

                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["GROSS_AMOUNT"])
                                        : 0,

                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["TAX_AMOUNT"])
                                        : 0,

                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["NET_AMOUNT"])
                                        : 0,

                                    CUST_NAME = reader["CUST_NAME"]?.ToString(),

                                    PATIENT_ID = reader["PATIENT_ID"]?.ToString(),

                                    ENCOUNTER_TYPE = reader["ENCOUNTER_TYPE"]?.ToString()
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




        public MiscSalesInvoiceSave GetMiscSaleInvoiceById(int id)
        {
            var response = new MiscSalesInvoiceSave();
            response.DETAILS = new List<MiscSalesInvoiceDetailData>();

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    //--------------------------------------------------
                    // 🔹 HEADER QUERY
                    //--------------------------------------------------
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    SELECT 
                         TB_SALE_HEADER.ID,
                         TB_SALE_HEADER.SALE_NO,
                         TB_SALE_HEADER.SALE_DATE,
                         TB_SALE_HEADER.REF_NO,
                         TB_SALE_HEADER.REF_DATE,
                         TB_SALE_HEADER.CUSTOMER_ID,
                         TB_SALE_HEADER.TPA_ID,
                         TB_SALE_HEADER.ENCOUNTER_TYPE,
                         TB_SALE_HEADER.PATIENT_ID,
                         TB_SALE_HEADER.PATIENT_NAME,
                         TB_SALE_HEADER.COMPANY_ID,
	                     TB_AC_TRANS_HEADER.STORE_ID AS STORE_ID,
                         TB_AC_TRANS_HEADER.TRANS_STATUS
                    FROM TB_SALE_HEADER
                    INNER JOIN TB_AC_TRANS_HEADER ON 
                    TB_AC_TRANS_HEADER.TRANS_ID = TB_SALE_HEADER.TRANS_ID
                    WHERE TB_SALE_HEADER.ID = @ID";

                        var param = cmd.CreateParameter();
                        param.ParameterName = "@ID";
                        param.Value = id;
                        cmd.Parameters.Add(param);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response.ID = reader["ID"] as int?;
                                response.INVOICE_NO = reader["SALE_NO"]?.ToString();
                                response.INVOICE_DATE = reader["SALE_DATE"] as DateTime?;
                                response.REF_NO = reader["REF_NO"]?.ToString();
                                response.REF_DATE = reader["REF_DATE"] as DateTime?;
                                response.CUSTOMER_ID = reader["CUSTOMER_ID"] as int?;
                                response.TPA_ID = reader["TPA_ID"] as int?;
                                response.ENCOUNTER_TYPE = reader["ENCOUNTER_TYPE"]?.ToString();
                                response.PATIENT_ID = reader["PATIENT_ID"]?.ToString();
                                response.PATIENT_NAME = reader["PATIENT_NAME"]?.ToString();
                                response.COMPANY_ID = reader["COMPANY_ID"] as int?;
                                response.STORE_ID = reader["STORE_ID"] as int?;
                                response.TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value
                                                        ? Convert.ToInt32(reader["TRANS_STATUS"])
                                                        : (int?)null;

                                response.IS_APPROVED = response.TRANS_STATUS == 5 ? true : false ;
                            }
                        }
                    }

                    //--------------------------------------------------
                    //  DETAIL QUERY
                    //--------------------------------------------------
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    SELECT 
                        TB_SALE_DETAIL.ITEM_ID,
                        TB_SALE_DETAIL.ORDERING_CLINICIAN,
                        TB_SALE_DETAIL.CLINICIAN,
                        TB_SALE_DETAIL.DEPARTMENT_ID,
                        TB_SALE_DETAIL.SUB_DEPARTMENT_ID,
                        TB_SALE_DETAIL.QUANTITY,
                        TB_SALE_DETAIL.DURATION,
                        TB_SALE_DETAIL.GROSS_AMOUNT,
                        TB_SALE_DETAIL.PATIENT_SHARE,
                        TB_SALE_DETAIL.TAX_CODE,
                        TB_SALE_DETAIL.TAX_PERC,
                        TB_SALE_DETAIL.TAX_AMOUNT,
                        TB_SALE_DETAIL.TOTAL_AMOUNT,
                        TB_SALE_DETAIL.VAT_CLASS_ID,
	                    TB_ITEMS.DESCRIPTION AS ITEM_DESRIPTION
                    FROM TB_SALE_DETAIL
                    INNER JOIN TB_ITEMS ON 
                    TB_ITEMS.ID = TB_SALE_DETAIL.ITEM_ID
                    WHERE SALE_ID = @SALE_ID";

                        var param = cmd.CreateParameter();
                        param.ParameterName = "@SALE_ID";
                        param.Value = id;
                        cmd.Parameters.Add(param);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.DETAILS.Add(new MiscSalesInvoiceDetailData
                                {
                                    ITEM_ID = reader["ITEM_ID"] as int?,
                                    ITEM_DESCRIPTION = reader["ITEM_DESRIPTION"]?.ToString(),
                                    ORDERING_CLINICIAN = reader["ORDERING_CLINICIAN"]?.ToString(),
                                    CLINICIAN = reader["CLINICIAN"]?.ToString(),
                                    DEPARTMENT_ID = reader["DEPARTMENT_ID"] as int?,
                                    SUB_DEPARTMENT_ID = reader["SUB_DEPARTMENT_ID"] as int?,
                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDecimal(reader["QUANTITY"]) : 0,
                                    DURATION = reader["DURATION"] != DBNull.Value ? Convert.ToDecimal(reader["DURATION"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["GROSS_AMOUNT"]) : 0,
                                    PATIENT_SHARE = reader["PATIENT_SHARE"] != DBNull.Value ? Convert.ToDecimal(reader["PATIENT_SHARE"]) : 0,
                                    VAT_CODE = reader["TAX_CODE"]?.ToString(),
                                    VAT_PERC = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    VAT_CLASS_ID = reader["VAT_CLASS_ID"] as int?,
                                    VAT_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
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
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.SALE_DATE));
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
                     SELECT TOP 1 VOUCHER_NO + 1 FROM TB_AC_TRANS_HEADER 
                     INNER JOIN TB_SALE_HEADER ON TB_AC_TRANS_HEADER.TRANS_ID=TB_SALE_HEADER.TRANS_ID
                     LEFT JOIN  TB_CUSTOMER ON TB_CUSTOMER.ID=TB_SALE_HEADER.CUSTOMER_ID
                     WHERE TB_AC_TRANS_HEADER.TRANS_TYPE = 25 AND TB_CUSTOMER.CUST_TYPE in (1,2)
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

                    string procedureName = "SP_MISC_SALE_INVOICE";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
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
        public List<InvoiceCust_stateName> Getcustlist(InvoiceListRequest request)
        {
            var InvoiceCust_stateName = new List<InvoiceCust_stateName>();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SALE_INVOICE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new InvoiceCust_stateName
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    DESCRIPTION = reader["CUST_NAME"] != DBNull.Value ? reader["CUST_NAME"].ToString() : string.Empty,
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    STATE_NAME = reader["STATE_NAME"] != DBNull.Value ? reader["STATE_NAME"].ToString() : string.Empty
                                };
                                InvoiceCust_stateName.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return InvoiceCust_stateName;
        }


        public getItemsResponse getItems(getItemsInput input)
        {
            var response = new getItemsResponse
            {
                flag = 0,
                message = "Failed",
                data = new List<getItems>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    SELECT 
                        TB_ITEMS.DESCRIPTION, 
                        TB_ITEMS.GST_PERC, 
                        TB_VAT_CLASS.CODE,
	                    TB_VAT_CLASS.ID AS VAT_CLASS_ID
                    FROM TB_ITEMS
                    INNER JOIN TB_VAT_CLASS 
                        ON TB_VAT_CLASS.ID = TB_ITEMS.VAT_CLASS_ID
                    WHERE TB_ITEMS.ID = @ITEM_ID";

                        // 🔥 Parameter
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@ITEM_ID";
                        param.Value = input.ITEM_ID;
                        cmd.Parameters.Add(param);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.data.Add(new getItems
                                {
                                    ITEM_DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    VAT_PERC = reader["GST_PERC"] != DBNull.Value
                                                ? Convert.ToDecimal(reader["GST_PERC"])
                                                : 0,
                                    VAT_CODE = reader["CODE"]?.ToString(),
                                    VAT_CLASS_ID = reader["VAT_CLASS_ID"] != DBNull.Value
                                                    ? Convert.ToInt32(reader["VAT_CLASS_ID"])
                                                    : 0
                                });
                            }
                        }
                    }

                    response.flag = 1;
                    response.message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = "Error: " + ex.Message;
            }

            return response;
        }
    }
}
