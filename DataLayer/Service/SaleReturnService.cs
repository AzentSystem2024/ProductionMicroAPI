using Azure.Core;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SaleReturnService:ISaleReturnService
    {
        public SaleReturnResponse Insert(SaleReturn model)
        {
            SaleReturnResponse response = new SaleReturnResponse
            {
                Flag = 0,
                Message = "Unknown error"
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // 🔹 Header params
                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                    cmd.Parameters.AddWithValue("@CUST_ID", model.CUST_ID ?? 0);
                    cmd.Parameters.AddWithValue("@RET_DATE", model.RET_DATE ?? DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? (object)DBNull.Value);

                    // 🔹 Prepare UDT DataTable
                    DataTable dtUDT = new DataTable();
                    dtUDT.Columns.Add("BOX_ID", typeof(int));

                    if (model.DETAILS != null)
                    {
                        foreach (var d in model.DETAILS)
                        {
                            dtUDT.Rows.Add(d.BOX_ID ?? 0);
                        }
                    }

                    // 🔹 TVP parameter
                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_SALE_RET_DETAIL", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_SALE_RET_DETAIL";
                    tvp.Value = dtUDT;

                    // 🔹 Execute
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Sale return inserted successfully";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public SaleReturnListResponse GetSaleReturnList(SaleReturnListRequest request)
        {
            SaleReturnListResponse res = new SaleReturnListResponse();
            res.Data = new List<SaleReturnList>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", (object?)request.DATE_FROM ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DATE_TO", (object?)request.DATE_TO ?? DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.Data.Add(new SaleReturnList
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                TRANS_ID = dr["TRANS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRANS_ID"]),
                                RET_DATE = dr["RET_DATE"] == DBNull.Value ? null : Convert.ToDateTime(dr["RET_DATE"]),
                                GROSS_AMOUNT = dr["GROSS_AMOUNT"] == DBNull.Value ? null : Convert.ToDecimal(dr["GROSS_AMOUNT"]),
                                VAT_AMOUNT = dr["VAT_AMOUNT"] == DBNull.Value ? null : Convert.ToDecimal(dr["VAT_AMOUNT"]),
                                NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? null : Convert.ToDecimal(dr["NET_AMOUNT"]),
                                REF_NO = dr["REF_NO"]?.ToString(),
                                TRANS_STATUS = dr["TRANS_STATUS"] == DBNull.Value ? null : Convert.ToInt32(dr["TRANS_STATUS"]),
                                COMPANY_NAME = dr["COMPANY_NAME"]?.ToString(),
                                CUST_NAME = dr["CUST_NAME"]?.ToString(),
                                RET_NO = dr["RET_NO"]?.ToString()
                            });
                        }
                    }
                }

                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public SaleInvoiceDetailResponse GetSalesInvoiceDetail(InvoieRequest request)
        {
            SaleInvoiceDetailResponse res = new SaleInvoiceDetailResponse();
            res.Data = new List<SaleInvoiceDetail>();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        res.Data.Add(new SaleInvoiceDetail
                        {
                            ID = row["ID"] != DBNull.Value ? Convert.ToInt32(row["ID"]) : 0,
                            DOC_NO = row["SALE_NO"] != DBNull.Value ? row["SALE_NO"].ToString() : "",
                            SALE_DATE = row["SALE_DATE"] != DBNull.Value ? Convert.ToDateTime(row["SALE_DATE"]) : DateTime.MinValue,
                            SALE_DET_ID = row["DETAIL_ID"] != DBNull.Value ? Convert.ToInt32(row["DETAIL_ID"]) : 0,
                            ITEM_ID = row["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                            ITEM_NAME = row["DESCRIPTION"] != DBNull.Value ? row["DESCRIPTION"].ToString() : "",
                            PENDING_QTY = row["QUANTITY"] != DBNull.Value ? Convert.ToSingle(row["QUANTITY"]) : 0,
                            PRICE = row["STD_PRICE"] != DBNull.Value ? Convert.ToDecimal(row["STD_PRICE"]) : 0,
                            AMOUNT = row["PRICE"] != DBNull.Value ? Convert.ToDecimal(row["PRICE"]) : 0,
                            TAXABLE_AMOUNT = row["TAXABLE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(row["TAXABLE_AMOUNT"]) : 0,
                            TAX_AMOUNT = row["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(row["TAX_AMOUNT"]) : 0,
                            TAX_PERC = row["TAX_PERC"] != DBNull.Value ? Convert.ToDecimal(row["TAX_PERC"]) : 0,
                            QTY_STOCK = row["QTY_STOCK"] != DBNull.Value ? Convert.ToSingle(row["QTY_STOCK"]) : 0,
                            BARCODE = row["BARCODE"] != DBNull.Value ? row["BARCODE"].ToString() : "",
                            UOM = row["UOM"] != DBNull.Value ? row["UOM"].ToString() : "",
                            UOM_PURCH = row["UOM_PURCH"] != DBNull.Value ? row["UOM_PURCH"].ToString() : "",
                            UOM_MULTIPLE = row["UOM_MULTPLE"] != DBNull.Value ? Convert.ToInt32(row["UOM_MULTPLE"]) : 0,
                            HSN_CODE = row["HSN_CODE"] != DBNull.Value ? row["HSN_CODE"].ToString() : "",
                            GST_PERC = row["GST_PERC"] != DBNull.Value ? Convert.ToDecimal(row["GST_PERC"]) : 0,
                            CGST = row["CGST"] != DBNull.Value ? Convert.ToDecimal(row["CGST"]) : 0,
                            SGST = row["SGST"] != DBNull.Value ? Convert.ToDecimal(row["SGST"]) : 0,

                        });
                    }

                    res.Flag = 1;
                    res.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SaleInvoiceDetail>();
            }

            return res;
        }
        public Int32 InsertSaleReturn(SaleReturnInsertRequest model)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction trans = connection.BeginTransaction();

            try
            {
                // ---------- Build UDT DataTable ----------
                DataTable tbl = new DataTable();

                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("SALE_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("DN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("CGST", typeof(decimal));
                tbl.Columns.Add("SGST", typeof(decimal));

                if (model.Details != null && model.Details.Any())
                {
                    foreach (var d in model.Details)
                    {
                        var row = tbl.NewRow();

                        row["COMPANY_ID"] = d.COMPANY_ID;
                        row["STORE_ID"] = d.STORE_ID;
                        row["SALE_DET_ID"] = d.SALE_DET_ID;
                        row["ITEM_ID"] = d.ITEM_ID;
                        row["DN_QTY"] = d.PENDING_QTY;
                        row["QUANTITY"] = d.QUANTITY;
                        row["RATE"] = d.PRICE;
                        row["AMOUNT"] = d.AMOUNT;
                        row["VAT_PERC"] = d.VAT_PERC;
                        row["VAT_AMOUNT"] = d.VAT_AMOUNT;
                        row["TOTAL_AMOUNT"] = d.TOTAL_AMOUNT;
                        row["UOM"] = d.UOM;
                        row["UOM_PURCH"] = d.UOM_PURCH;
                        row["UOM_MULTIPLE"] = d.UOM_MULTIPLE;
                        row["CGST"] = d.CGST;
                        row["SGST"] = d.SGST;

                        tbl.Rows.Add(row);
                    }
                }

                // ---------- Call SP ----------
                SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                cmd.Parameters.AddWithValue("@CUST_ID", model.CUST_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", model.RET_DATE);
                cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                cmd.Parameters.AddWithValue("@SALE_ID", model.SALE_ID);
                cmd.Parameters.AddWithValue("@SALE_NO", model.SALE_NO ?? "");
                cmd.Parameters.AddWithValue("@IS_CREDIT", model.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", model.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                cmd.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? "");
                cmd.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);
                cmd.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);

                var tvp = cmd.Parameters.AddWithValue("@UDT_SALE_RETURN_DETAIL", tbl);
                tvp.SqlDbType = SqlDbType.Structured;
                tvp.TypeName = "UDT_SALE_RETURN_DETAIL";

                cmd.ExecuteNonQuery();

                trans.Commit();
                return 0;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public Int32 UpdateSaleReturn(SaleReturnInsertRequest model)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction trans = connection.BeginTransaction();

            try
            {
                // ---------- Build UDT DataTable ----------
                DataTable tbl = new DataTable();

                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("SALE_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("DN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("CGST", typeof(decimal));
                tbl.Columns.Add("SGST", typeof(decimal));

                if (model.Details != null && model.Details.Any())
                {
                    foreach (var d in model.Details)
                    {
                        var row = tbl.NewRow();

                        row["COMPANY_ID"] = d.COMPANY_ID;
                        row["STORE_ID"] = d.STORE_ID;
                        row["SALE_DET_ID"] = d.SALE_DET_ID;
                        row["ITEM_ID"] = d.ITEM_ID;
                        row["DN_QTY"] = d.PENDING_QTY;
                        row["QUANTITY"] = d.QUANTITY;
                        row["RATE"] = d.PRICE;
                        row["AMOUNT"] = d.AMOUNT;
                        row["VAT_PERC"] = d.VAT_PERC;
                        row["VAT_AMOUNT"] = d.VAT_AMOUNT;
                        row["TOTAL_AMOUNT"] = d.TOTAL_AMOUNT;
                        row["UOM"] = d.UOM;
                        row["UOM_PURCH"] = d.UOM_PURCH;
                        row["UOM_MULTIPLE"] = d.UOM_MULTIPLE;
                        row["CGST"] = d.CGST;
                        row["SGST"] = d.SGST;

                        tbl.Rows.Add(row);
                    }
                }

                // ---------- Call SP ----------
                SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 6);
                cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                cmd.Parameters.AddWithValue("@CUST_ID", model.CUST_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", model.RET_DATE);
                cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                cmd.Parameters.AddWithValue("@SALE_ID", model.SALE_ID);
                cmd.Parameters.AddWithValue("@SALE_NO", model.SALE_NO ?? "");
                cmd.Parameters.AddWithValue("@IS_CREDIT", model.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", model.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                cmd.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? "");
                cmd.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);

                var tvp = cmd.Parameters.AddWithValue("@UDT_SALE_RETURN_DETAIL", tbl);
                tvp.SqlDbType = SqlDbType.Structured;
                tvp.TypeName = "UDT_SALE_RETURN_DETAIL";

                cmd.ExecuteNonQuery();

                trans.Commit();
                return 0;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public Int32 CommitSaleReturn(SaleReturnInsertRequest model)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction trans = connection.BeginTransaction();

            try
            {
                // ---------- Build UDT DataTable ----------
                DataTable tbl = new DataTable();

                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("SALE_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("DN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("CGST", typeof(decimal));
                tbl.Columns.Add("SGST", typeof(decimal));

                if (model.Details != null && model.Details.Any())
                {
                    foreach (var d in model.Details)
                    {
                        var row = tbl.NewRow();

                        row["COMPANY_ID"] = d.COMPANY_ID;
                        row["STORE_ID"] = d.STORE_ID;
                        row["SALE_DET_ID"] = d.SALE_DET_ID;
                        row["ITEM_ID"] = d.ITEM_ID;
                        row["DN_QTY"] = d.PENDING_QTY;
                        row["QUANTITY"] = d.QUANTITY;
                        row["RATE"] = d.PRICE;
                        row["AMOUNT"] = d.AMOUNT;
                        row["VAT_PERC"] = d.VAT_PERC;
                        row["VAT_AMOUNT"] = d.VAT_AMOUNT;
                        row["TOTAL_AMOUNT"] = d.TOTAL_AMOUNT;
                        row["UOM"] = d.UOM;
                        row["UOM_PURCH"] = d.UOM_PURCH;
                        row["UOM_MULTIPLE"] = d.UOM_MULTIPLE;
                        row["CGST"] = d.CGST;
                        row["SGST"] = d.SGST;

                        tbl.Rows.Add(row);
                    }
                }

                // ---------- Call SP ----------
                SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 7);
                cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                cmd.Parameters.AddWithValue("@CUST_ID", model.CUST_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", model.RET_DATE);
                cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                cmd.Parameters.AddWithValue("@SALE_ID", model.SALE_ID);
                cmd.Parameters.AddWithValue("@SALE_NO", model.SALE_NO ?? "");
                cmd.Parameters.AddWithValue("@IS_CREDIT", model.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", model.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                cmd.Parameters.AddWithValue("@VEHICLE_NO", model.VEHICLE_NO ?? "");
                cmd.Parameters.AddWithValue("@ROUND_OFF", model.ROUND_OFF);

                var tvp = cmd.Parameters.AddWithValue("@UDT_SALE_RETURN_DETAIL", tbl);
                tvp.SqlDbType = SqlDbType.Structured;
                tvp.TypeName = "UDT_SALE_RETURN_DETAIL";

                cmd.ExecuteNonQuery();

                trans.Commit();
                return 0;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public SaleReturnViewResponse GetSaleReturnById(int id)
        {
            var response = new SaleReturnViewResponse
            {
                Header = new SaleReturnViewHeader(),
                Details = new List<SaleReturnViewDetail>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_SALE_RETURN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    cmd.Parameters.AddWithValue("@TRANS_ID", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        bool headerLoaded = false;

                        while (dr.Read())
                        {
                            // HEADER — load once
                            if (!headerLoaded)
                            {
                                response.Header = new SaleReturnViewHeader
                                {
                                    RET_ID = dr["RET_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["RET_ID"]),
                                    COMPANY_ID = dr["COMPANY_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["COMPANY_ID"]),
                                    STORE_ID = dr["STORE_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["STORE_ID"]),
                                    RET_NO = dr["RET_NO"]?.ToString(),
                                    RET_DATE = dr["RET_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RET_DATE"]),
                                    CUST_ID = dr["CUST_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CUST_ID"]),
                                    SALE_ID = dr["SALE_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SALE_ID"]),
                                    SALE_NO = dr["SALE_NO"]?.ToString(),
                                    IS_CREDIT = dr["IS_CREDIT"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["IS_CREDIT"]),
                                    GROSS_AMOUNT = dr["GROSS_AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["GROSS_AMOUNT"]),
                                    VAT_AMOUNT = dr["VAT_AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["VAT_AMOUNT"]),
                                    NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["NET_AMOUNT"]),
                                    FIN_ID = dr["FIN_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["FIN_ID"]),
                                    TRANS_ID = dr["TRANS_ID"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["TRANS_ID"]),
                                    VEHICLE_NO = dr["VEHICLE_NO"]?.ToString(),
                                    ROUND_OFF = dr["ROUND_OFF"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["ROUND_OFF"]),
                                    REF_NO = dr["REF_NO"]?.ToString(),
                                    NARRATION = dr["NARRATION"]?.ToString(),
                                    TRANS_STATUS = dr["TRANS_STATUS"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["TRANS_STATUS"]),
                                    CUST_NAME = dr["CUST_NAME"]?.ToString(),
                                    COMPANY_NAME = dr["COMPANY_NAME"]?.ToString(),
                                    ADDRESS1 = dr["ADDRESS1"]?.ToString(),
                                    ADDRESS2 = dr["ADDRESS2"]?.ToString(),
                                    ADDRESS3 = dr["ADDRESS3"]?.ToString(),
                                    COMPANY_CODE = dr["COMPANY_CODE"]?.ToString(),
                                    EMAIL = dr["EMAIL"]?.ToString(),
                                    PHONE = dr["PHONE"]?.ToString(),
                                    CUST_CODE = dr["CUST_CODE"]?.ToString(),
                                    CUST_ADDRESS1 = dr["CUST_ADDRESS1"]?.ToString(),
                                    CUST_ADDRESS2 = dr["CUST_ADDRESS2"]?.ToString(),
                                    CUST_ADDRESS3 = dr["CUST_ADDRESS3"]?.ToString(),
                                    CUST_ZIP = dr["ZIP"]?.ToString(),
                                    CUST_CITY = dr["CITY"]?.ToString(),
                                    CUST_STATE = dr["STATE_NAME"]?.ToString(),
                                    CUST_PHONE = dr["CUST_PHONE"]?.ToString(),
                                    CUST_EMAIL = dr["CUST_EMAIL"]?.ToString(),
                                    GST_NO = dr["GST_NO"]?.ToString(),
                                    PAN_NO = dr["PAN_NO"]?.ToString(),
                                    CIN = dr["CIN"]?.ToString(),
                                };


                                headerLoaded = true;
                            }

                            // DETAILS — every row
                            response.Details.Add(new SaleReturnViewDetail
                            {
                                DETAIL_ID = dr["DETAIL_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DETAIL_ID"]),
                                SALE_DET_ID = dr["SALE_DET_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SALE_DET_ID"]),
                                ITEM_ID = dr["ITEM_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ITEM_ID"]),
                                PENDING_QTY = dr["PENDING_QTY"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["PENDING_QTY"]),
                                QUANTITY = dr["QUANTITY"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["QUANTITY"]),
                                PRICE = dr["RATE"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["RATE"]),
                                AMOUNT = dr["AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["AMOUNT"]),
                                VAT_PERC = dr["VAT_PERC"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["VAT_PERC"]),
                                VAT_AMOUNT = dr["VAT_AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["VAT_AMOUNT"]),
                                TOTAL_AMOUNT = dr["TOTAL_AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["TOTAL_AMOUNT"]),
                                UOM = dr["UOM"]?.ToString(),
                                UOM_PURCH = dr["UOM_PURCH"]?.ToString(),
                                UOM_MULTIPLE = dr["UOM_MULTIPLE"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UOM_MULTIPLE"]),
                                CGST = dr["CGST"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["CGST"]),
                                SGST = dr["SGST"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["SGST"]),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                BARCODE = dr["BARCODE"]?.ToString(),
                                HSN_CODE = dr["HSN_CODE"]?.ToString(),
                                SALE_NO = dr["SALE_NO"]?.ToString()
                            });

                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Sale return fetched successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public bool Delete(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_SALE_RETURN";
                cmd.Parameters.AddWithValue("ACTION", 8);
                cmd.Parameters.AddWithValue("@TRANS_ID", id);
                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
