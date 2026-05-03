using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SalesInvoiceService:ISalesInvoiceService
    {
        public SalesResponse GetSalesInvoiceItem(SalesInvoiceRequest input)
        {
            SalesResponse response = new SalesResponse();
            List<SalesInvoice> items = new List<SalesInvoice>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_SALES_INVOICE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    cmd.Parameters.AddWithValue("@ITEM_ID", input.ITEM_ID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    items = tbl.AsEnumerable().Select(dr => new SalesInvoice
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        ITEM_CODE = ADO.ToString(dr["ITEM_CODE"]),
                        DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                        BARCODE = ADO.ToString(dr["BARCODE"]),
                        UOM = ADO.ToString(dr["UOM"]),
                        TAX_PERC = dr["VAT_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VAT_PERC"]),
                        PRICE = dr["COST"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["COST"]),
                        HSN_CODE = dr["HSN_CODE"] == DBNull.Value ? null : dr["HSN_CODE"].ToString()
                    }).ToList();



                    response.Data = items;
                    response.Message = "Success";
                    response.Flag = 1;
                }
            }
            catch (Exception ex)
            {
                response.Data = new List<SalesInvoice>();
                response.Message = ex.Message;
                response.Flag = 0;
            }

            return response;
        }
        //public SalesResponse GetSalesInvoiceItem(SalesInvoiceRequest input)
        //{
        //    SalesResponse response = new SalesResponse();
        //    List<SalesInvoice> items = new List<SalesInvoice>();

        //    try
        //    {
        //        using (SqlConnection connection = ADO.GetConnection())
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();

        //            // ✅ Step 1: Get PRICE_CLASS_ID
        //            int priceClassId = 0;
        //            using (SqlCommand cmdPrice = new SqlCommand("SELECT ISNULL(PRICE_CLASS_ID,0) FROM TB_CUSTOMER WHERE ID=@CUST_ID", connection))
        //            {
        //                cmdPrice.Parameters.AddWithValue("@CUST_ID", input.CUSTOMER_ID);
        //                priceClassId = Convert.ToInt32(cmdPrice.ExecuteScalar());
        //            }

        //            // ✅ Step 2: Fetch Item Data
        //            SqlCommand cmd = new SqlCommand("SP_TB_SALES_INVOICE", connection);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@ACTION", 5);
        //            cmd.Parameters.AddWithValue("@ITEM_ID", input.ITEM_ID);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable tbl = new DataTable();
        //            da.Fill(tbl);

        //            // ✅ Step 3: Apply Price Logic
        //            items = tbl.AsEnumerable().Select(dr => new SalesInvoice
        //            {
        //                ID = ADO.ToInt32(dr["ID"]),
        //                ITEM_CODE = ADO.ToString(dr["ITEM_CODE"]),
        //                DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
        //                BARCODE = ADO.ToString(dr["BARCODE"]),
        //                UOM = ADO.ToString(dr["UOM"]),

        //                TAX_PERC = dr["VAT_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VAT_PERC"]),

        //                PRICE =
        //                (
        //                    priceClassId == 1
        //                        ? (dr["SALE_PRICE1"] == DBNull.Value || Convert.ToDecimal(dr["SALE_PRICE1"]) == 0
        //                            ? (dr["SALE_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALE_PRICE"]))
        //                            : Convert.ToDecimal(dr["SALE_PRICE1"]))

        //                    : priceClassId == 2
        //                        ? (dr["SALE_PRICE2"] == DBNull.Value || Convert.ToDecimal(dr["SALE_PRICE2"]) == 0
        //                            ? (dr["SALE_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALE_PRICE"]))
        //                            : Convert.ToDecimal(dr["SALE_PRICE2"]))

        //                    : priceClassId == 3
        //                        ? (dr["SALE_PRICE3"] == DBNull.Value || Convert.ToDecimal(dr["SALE_PRICE3"]) == 0
        //                            ? (dr["SALE_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALE_PRICE"]))
        //                            : Convert.ToDecimal(dr["SALE_PRICE3"]))

        //                    : priceClassId == 4
        //                        ? (dr["SALE_PRICE4"] == DBNull.Value || Convert.ToDecimal(dr["SALE_PRICE4"]) == 0
        //                            ? (dr["SALE_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALE_PRICE"]))
        //                            : Convert.ToDecimal(dr["SALE_PRICE4"]))

        //                    : priceClassId == 5
        //                        ? (dr["SALE_PRICE5"] == DBNull.Value || Convert.ToDecimal(dr["SALE_PRICE5"]) == 0
        //                            ? (dr["SALE_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALE_PRICE"]))
        //                            : Convert.ToDecimal(dr["SALE_PRICE5"]))

        //                    : (dr["SALE_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALE_PRICE"]))
        //                ),

        //                HSN_CODE = dr["HSN_CODE"] == DBNull.Value ? null : dr["HSN_CODE"].ToString()

        //            }).ToList();

        //            response.Data = items;
        //            response.Message = "Success";
        //            response.Flag = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Data = new List<SalesInvoice>();
        //        response.Message = ex.Message;
        //        response.Flag = 0;
        //    }

        //    return response;
        //}

        public SalesInvoiceInsertResponse Insert(SalesInvoiceInsertRequest input)
        {
            SalesInvoiceInsertResponse response = new SalesInvoiceInsertResponse();

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_SALES_INVOICE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", input.FIN_ID);
                    cmd.Parameters.AddWithValue("@TRANS_DATE", input.TRANS_DATE);
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", input.CUSTOMER_ID);
                    cmd.Parameters.AddWithValue("@PARTY_NAME", input.PARTY_NAME ?? "");
                    cmd.Parameters.AddWithValue("@NARRATION", input.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@CREATE_USER_ID", input.USER_ID);
                    cmd.Parameters.AddWithValue("@REF_NO", input.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@GROSS_AMOUNT", input.GROSS_AMOUNT);
                    cmd.Parameters.AddWithValue("@TAX_AMOUNT", input.TAX_AMOUNT);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", input.NET_AMOUNT);
                    cmd.Parameters.AddWithValue("@IS_APPROVED", input.IS_APPROVED);
                    cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", input.DISCOUNT_AMOUNT);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("QUANTITY", typeof(float));
                    dt.Columns.Add("PRICE", typeof(float));
                    dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TAX_PERC", typeof(decimal));
                    dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("DISC_PERC", typeof(decimal));
                    dt.Columns.Add("DISC_AMT", typeof(decimal));


                    foreach (var item in input.Details)
                    {
                        dt.Rows.Add(item.QUANTITY, item.PRICE,
                            item.AMOUNT,
                            item.TAX_PERC,
                            item.TAX_AMOUNT,
                             item.TOTAL_AMOUNT,
                            item.ITEM_ID,
                            item.DISC_PERC,
                            item.DISC_AMT
                        );
                    }

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_SALE_DETAIL", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    //con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            response.Flag = 1;
                            response.Message = "Sales Invoice Created Successfully";
                        }
                        else
                        {
                            response.Flag = 0;
                            response.Message = "Failed to create Sales Invoice";
                        }
                    }
                }
            }

            return response;
        }
        public SalesInvoiceInsertResponse Update(SalesInvoiceInsertRequest input)
        {
            SalesInvoiceInsertResponse response = new SalesInvoiceInsertResponse();

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_SALES_INVOICE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@TRANS_ID", input.TRANS_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FIN_ID", input.FIN_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRANS_DATE", input.TRANS_DATE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", input.CUSTOMER_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PARTY_NAME", input.PARTY_NAME ?? "");
                    cmd.Parameters.AddWithValue("@NARRATION", input.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@CREATE_USER_ID", input.USER_ID);
                    cmd.Parameters.AddWithValue("@REF_NO", input.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@GROSS_AMOUNT", input.GROSS_AMOUNT ?? 0);
                    cmd.Parameters.AddWithValue("@TAX_AMOUNT", input.TAX_AMOUNT ?? 0);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", input.NET_AMOUNT ?? 0);
                    cmd.Parameters.AddWithValue("@IS_APPROVED", input.IS_APPROVED);
                    cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", input.DISCOUNT_AMOUNT);



                    // ✅ UDT
                    DataTable dt = new DataTable();
                    dt.Columns.Add("QUANTITY", typeof(float));
                    dt.Columns.Add("PRICE", typeof(float));
                    dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TAX_PERC", typeof(decimal));
                    dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("DISC_PERC", typeof(decimal));
                    dt.Columns.Add("DISC_AMT", typeof(decimal));

                    if (input.Details != null)
                    {
                        foreach (var item in input.Details)
                        {
                            dt.Rows.Add(
                                item.QUANTITY, item.PRICE,
                            item.AMOUNT,
                            item.TAX_PERC,
                            item.TAX_AMOUNT,
                             item.TOTAL_AMOUNT,
                            item.ITEM_ID,
                            item.DISC_PERC,
                            item.DISC_AMT
                            );
                        }
                    }

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_SALE_DETAIL", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                   // con.Open();

                    int rows = cmd.ExecuteNonQuery(); 

                    if (rows > 0)
                    {
                        response.Flag = 1;
                        response.Message = "Sales Invoice Updated Successfully";
                    }
                    else
                    {
                        response.Flag = 1; 
                        response.Message = "Sales Invoice Updated";
                    }
                }
            }

            return response;
        }

        public SalesInvoiceListResponse GetSalesInvoiceHeaderList(InvoiceListRequest request)
        {
            var response = new SalesInvoiceListResponse
            {
                Flag = 0,
                Message = "Failed",
                Data = new List<SalesInvoiceListHeader>()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_TB_SALES_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);

                        cmd.Parameters.AddWithValue("@DATE_FROM",
                            request.DATE_FROM == null ? (object)DBNull.Value : Convert.ToDateTime(request.DATE_FROM));

                        cmd.Parameters.AddWithValue("@DATE_TO",
                            request.DATE_TO == null ? (object)DBNull.Value : Convert.ToDateTime(request.DATE_TO));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new SalesInvoiceListHeader
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                    VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                    TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                    CUSTOMER_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    GST_AMOUNT = reader["GST_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GST_AMOUNT"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,
                                    CUST_NAME = reader["CUST_NAME"]?.ToString(),
                                    DISCOUNT_AMOUNT = reader["DISCOUNT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DISCOUNT_AMOUNT"]) : 0,
                                    STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0,
                                    STORE_NAME = reader["STORE_NAME"] != DBNull.Value ? Convert.ToString(reader["STORE_NAME"]) : null,
                                });
                            }
                        }

                        response.Flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public SalesInvoiceViewResponse GetSalesInvoiceById(int Id)
        {
            var response = new SalesInvoiceViewResponse
            {
                Flag = 0,
                Message = "Failed",
                Data = new SalesInvoiceView()
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_TB_SALES_INVOICE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", Id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            SalesInvoiceView header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new SalesInvoiceView
                                    {
                                        TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        SALE_NO = reader["SALE_NO"]?.ToString(),

                                        TRANS_DATE = reader["SALE_DATE"] != DBNull.Value
                                            ? Convert.ToDateTime(reader["SALE_DATE"]).ToString("dd-MM-yyyy")
                                            : null,

                                        CUSTOMER_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,

                                        GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                        TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0,

                                        DISCOUNT_AMOUNT = reader["DISCOUNT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DISCOUNT_AMOUNT"]) : 0,

                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        PARTY_NAME = reader["CUST_NAME"]?.ToString(),

                                        // Customer
                                        CUST_NAME = reader["CUST_NAME"]?.ToString(),
                                        CUST_CODE = reader["CUST_CODE"]?.ToString(),
                                        CUST_ADDRESS1 = reader["CUST_ADDRESS1"]?.ToString(),
                                        CUST_ADDRESS2 = reader["CUST_ADDRESS2"]?.ToString(),
                                        CUST_ADDRESS3 = reader["CUST_ADDRESS3"]?.ToString(),
                                        CITY = reader["CITY"]?.ToString(),
                                        ZIP = reader["ZIP"]?.ToString(),
                                        CUST_PHONE = reader["CUST_PHONE"]?.ToString(),
                                        CUST_EMAIL = reader["CUST_EMAIL"]?.ToString(),
                                        STATE_NAME = reader["STATE_NAME"]?.ToString(),

                                        // Company
                                        COMPANY_NAME = reader["COMPANY_NAME"]?.ToString(),
                                        COMPANY_CODE = reader["COMPANY_CODE"]?.ToString(),
                                        ADDRESS1 = reader["ADDRESS1"]?.ToString(),
                                        ADDRESS2 = reader["ADDRESS2"]?.ToString(),
                                        ADDRESS3 = reader["ADDRESS3"]?.ToString(),
                                        GST_NO = reader["GST_NO"]?.ToString(),
                                        PAN_NO = reader["PAN_NO"]?.ToString(),
                                        CIN = reader["CIN"]?.ToString(),
                                        EMAIL = reader["EMAIL"]?.ToString(),
                                        PHONE = reader["PHONE"]?.ToString(),

                                        Details = new List<SalesInvoiceItem>()
                                    };
                                }

                                // Detail rows
                                header.Details.Add(new SalesInvoiceItem
                                {
                                    ITEM_ID = Convert.ToInt32(reader["ITEM_ID"]),
                                    ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    HSN_CODE = reader["HSN_CODE"]?.ToString(),
                                    UOM = reader["UOM"]?.ToString(),
                                    COST = reader["COST"] != DBNull.Value ? Convert.ToDecimal(reader["COST"]) : 0,

                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDouble(reader["QUANTITY"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToDouble(reader["PRICE"]) : 0,
                                    AMOUNT = reader["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAXABLE_AMOUNT"]) : 0,
                                    TAX_PERC = reader["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_PERC"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0,
                                    DISC_PERC = reader["DISC_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["DISC_PERC"]) : 0,
                                    DISC_AMT = reader["DISC_AMT"] != DBNull.Value ? Convert.ToDecimal(reader["DISC_AMT"]) : 0

                                });
                            }

                            response.Data = header;
                        }

                        response.Flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public SalesInvoiceInsertResponse Commit(SalesInvoiceInsertRequest input)
        {
            SalesInvoiceInsertResponse response = new SalesInvoiceInsertResponse();

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_SALES_INVOICE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@TRANS_ID", input.TRANS_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", input.COMPANY_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FIN_ID", input.FIN_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRANS_DATE", input.TRANS_DATE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", input.CUSTOMER_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PARTY_NAME", input.PARTY_NAME ?? "");
                    cmd.Parameters.AddWithValue("@NARRATION", input.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@CREATE_USER_ID", input.USER_ID);
                    cmd.Parameters.AddWithValue("@REF_NO", input.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@GROSS_AMOUNT", input.GROSS_AMOUNT ?? 0);
                    cmd.Parameters.AddWithValue("@TAX_AMOUNT", input.TAX_AMOUNT ?? 0);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", input.NET_AMOUNT ?? 0);
                    cmd.Parameters.AddWithValue("@IS_APPROVED", input.IS_APPROVED);
                    cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", input.DISCOUNT_AMOUNT);



                    // ✅ UDT
                    DataTable dt = new DataTable();
                    dt.Columns.Add("QUANTITY", typeof(float));
                    dt.Columns.Add("PRICE", typeof(float));
                    dt.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TAX_PERC", typeof(decimal));
                    dt.Columns.Add("TAX_AMOUNT", typeof(decimal));
                    dt.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("DISC_PERC", typeof(decimal));
                    dt.Columns.Add("DISC_AMT", typeof(decimal));

                    if (input.Details != null)
                    {
                        foreach (var item in input.Details)
                        {
                            dt.Rows.Add(
                                item.QUANTITY, item.PRICE,
                            item.AMOUNT,
                            item.TAX_PERC,
                            item.TAX_AMOUNT,
                             item.TOTAL_AMOUNT,
                            item.ITEM_ID,
                            item.DISC_PERC,
                            item.DISC_AMT
                            );
                        }
                    }

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_SALE_DETAIL", dt);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    // con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        response.Flag = 1;
                        response.Message = "Sales Invoice Commit Successfully";
                    }
                    else
                    {
                        response.Flag = 1;
                        response.Message = "Sales Invoice Commit Successfully";
                    }
                }
            }

            return response;
        }
        public SalesInvoiceInsertResponse Delete(int id)
        {
            SalesInvoiceInsertResponse res = new SalesInvoiceInsertResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_SALES_INVOICE";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
