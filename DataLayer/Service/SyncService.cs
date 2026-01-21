using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SyncService: ISyncService
    {
        public SyncResponse Insert(Sync model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ARTICLE_PRODUCTION_UPLOAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // ----------- HEADER PARAMETERS -------------
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                        

                        // ----------- BUILD UDT TABLE ----------------
                        DataTable dtUDT = new DataTable();

                        dtUDT.Columns.Add("ARTICLE_ID", typeof(long));
                        dtUDT.Columns.Add("ARTICLE_PRODUCTION_ID", typeof(long));
                        dtUDT.Columns.Add("BOX_ID", typeof(long));
                        dtUDT.Columns.Add("BARCODE", typeof(string));
                        dtUDT.Columns.Add("PRICE", typeof(double));
                        dtUDT.Columns.Add("PRODUCTION_DATE", typeof(DateTime));

                        if (model.Articles != null && model.Articles.Count > 0)
                        {
                            foreach (var item in model.Articles)
                            {
                                dtUDT.Rows.Add(
                                    item.ARTICLE_ID,
                                    item.ARTICLE_PRODUCTION_ID,
                                    item.BOX_ID,
                                    item.BARCODE ?? "",
                                    Convert.ToDouble(item.PRICE),
                                    item.PRODUCTION_DATE
                                );
                            }
                        }

                        // ---------- TVP PARAMETER -----------------
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_ARTICLE_PRODUCTION", dtUDT);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_ARTICLE_PRODUCTION";

                        //connection.Open();
                        cmd.ExecuteNonQuery();

                        response.Flag = 1;
                        response.Message = "Article production uploaded successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
            }

            return response;
        }

        public SyncResponse UploadPackProduction(PackProductionSync model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_BOX_PRODUCTION_UPLOAD", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);

                    // UDT_BOX_PRODUCTION DataTable
                    DataTable dtUDT = new DataTable();
                    dtUDT.Columns.Add("PACK_PRODUCTION_ID", typeof(long));
                    dtUDT.Columns.Add("PACKING_ID", typeof(long));
                    dtUDT.Columns.Add("BOX_SERIAL", typeof(int));
                    dtUDT.Columns.Add("BARCODE", typeof(string));
                    dtUDT.Columns.Add("BOX_PRICE", typeof(float));
                    dtUDT.Columns.Add("PRODUCTION_DATE", typeof(DateTime));

                    foreach (var item in model.PackItems)
                    {
                        dtUDT.Rows.Add(
                            item.PACK_PRODUCTION_ID,
                            item.PACKING_ID,
                            item.BOX_SERIAL,
                            item.BARCODE ?? string.Empty,
                            item.BOX_PRICE,
                            item.PRODUCTION_DATE
                        );
                    }

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_BOX_PRODUCTION", SqlDbType.Structured);
                    tvp.TypeName = "UDT_BOX_PRODUCTION";
                    tvp.Value = dtUDT;

                    // UDT_ARTICLE_PRODUCTION (OPTIONAL)
                   
                    if (model.Articles != null && model.Articles.Count > 0)
                    {
                        DataTable dtArticle = new DataTable();
                        dtArticle.Columns.Add("ARTICLE_PRODUCTION_ID", typeof(long));
                        dtArticle.Columns.Add("ARTICLE_ID", typeof(long));
                        dtArticle.Columns.Add("BOX_ID", typeof(long));
                        dtArticle.Columns.Add("BARCODE", typeof(string));
                        dtArticle.Columns.Add("PRICE", typeof(float));
                        dtArticle.Columns.Add("PRODUCTION_DATE", typeof(DateTime));

                        foreach (var a in model.Articles)
                        {
                            dtArticle.Rows.Add(
                                a.ARTICLE_PRODUCTION_ID,
                                a.ARTICLE_ID,
                                a.BOX_ID,  
                                a.BARCODE ?? string.Empty,
                                a.PRICE,
                                a.PRODUCTION_DATE
                            );
                        }

                        SqlParameter tvpArticle = cmd.Parameters.Add("@UDT_ARTICLE_PRODUCTION", SqlDbType.Structured);
                        tvpArticle.TypeName = "UDT_ARTICLE_PRODUCTION";
                        tvpArticle.Value = dtArticle;
                    }

                   // connection.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Pack production uploaded successfully";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
            }

            return response;
        }

        public SyncResponse UploadProductionDN(ProductionDN model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_OUT(DN)", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 1);
                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                cmd.Parameters.AddWithValue("@CUST_ID", model.CUST_ID);
                cmd.Parameters.AddWithValue("@DN_DATE", model.DN_DATE);
                cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);

                // TVP
                DataTable dt = new DataTable();
                
                dt.Columns.Add("PACKING_ID", typeof(int));
                dt.Columns.Add("PO_NO", typeof(string));
                dt.Columns.Add("ORDER_ENTRY_ID", typeof(int));
                foreach (var item in model.Items)
                {
                    dt.Rows.Add(item.PACKING_ID,item.PO_NO,item.ORDER_ENTRY_ID); 
                }

                SqlParameter tvp = cmd.Parameters.AddWithValue(
                    "@UDT_TRANSFER_OUT_DN", dt);
                tvp.SqlDbType = SqlDbType.Structured;
                tvp.TypeName = "UDT_TRANSFER_OUT_DN";

                //con.Open();
                cmd.ExecuteNonQuery();

                response.Flag = 1;
                response.Message = "Production DN created successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }


        //public SyncResponse UploadProductionTransferIn(ProductionTransferIn model)
        //{
        //    SyncResponse response = new SyncResponse();

        //    try
        //    {
        //        using (SqlConnection con = ADO.GetConnection())
        //        using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_IN", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@ID", 0);
        //            cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
        //            cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
        //            cmd.Parameters.AddWithValue("@REC_DATE", model.REC_DATE);
        //            cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", model.ORIGIN_STORE_ID);
        //            cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
        //            cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
        //            cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
        //            cmd.Parameters.AddWithValue("@ISSUE_ID", (object?)model.ISSUE_ID ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("@REASON_ID", (object?)model.REASON_ID ?? DBNull.Value);

        //            // 🔹 Calculate NET_AMOUNT properly
        //            decimal netAmount = model.Items.Sum(x => (x.QUANTITY ?? 0) * (x.COST ?? 0));
        //            cmd.Parameters.AddWithValue("@NET_AMOUNT", netAmount);
        //            cmd.Parameters.AddWithValue("@TRANS_ID", 0);

        //            // 🔹 Build TVP DataTable
        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("ISSUE_DETAIL_ID", typeof(int));
        //            dt.Columns.Add("ITEM_ID", typeof(int));
        //            dt.Columns.Add("UOM", typeof(string));
        //            dt.Columns.Add("COST", typeof(double));
        //            dt.Columns.Add("ISSUE_QTY", typeof(decimal));
        //            dt.Columns.Add("QUANTITY", typeof(decimal));
        //            dt.Columns.Add("BATCH_NO", typeof(string));
        //            dt.Columns.Add("EXPIRY_DATE", typeof(DateTime));
        //            dt.Columns.Add("PACKING_ID", typeof(int));

        //            foreach (var item in model.Items)
        //            {
        //                dt.Rows.Add(
        //                    item.ISSUE_DETAIL_ID ?? 0,
        //                    item.ITEM_ID,
        //                    item.UOM ?? "",
        //                    item.COST ?? 0,
        //                    item.QUANTITY_ISSUED ?? 0,
        //                    item.QUANTITY ?? 0,
        //                    item.BATCH_NO ?? "",
        //                    (object?)item.EXPIRY_DATE ?? DBNull.Value,
        //                    item.PACKING_ID
        //                );
        //            }

        //            SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_TRANSFERINV_IN", SqlDbType.Structured);
        //            tvp.TypeName = "UDT_TB_TRANSFERINV_IN";
        //            tvp.Value = dt;

        //           // con.Open();
        //            cmd.ExecuteNonQuery();

        //            response.Flag = 1;
        //            response.Message = "Warehouse Transfer In completed successfully.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Flag = 0;
        //        response.Message = "ERROR: " + ex.Message;
        //    }

        //    return response;
        //}
      
        public SyncResponse UploadProductionDR(ProductionDR model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_DR", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                    cmd.Parameters.AddWithValue("@DR_DATE", model.DR_DATE);
                    cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@CUST_ID", model.CUST_ID);

                    cmd.Parameters.AddWithValue("@CONTACT_NAME", model.CONTACT_NAME ?? "");
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", model.CONTACT_PHONE ?? "");
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", model.CONTACT_FAX ?? "");
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", model.CONTACT_MOBILE ?? "");

                    cmd.Parameters.AddWithValue("@SALESMAN_ID", model.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");

                    // 🔹 Build TVP
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SO_DETAIL_ID", typeof(int));
                    dt.Columns.Add("DN_DETAIL_ID", typeof(int));
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("REMARKS", typeof(string));
                    dt.Columns.Add("UOM", typeof(string));
                    dt.Columns.Add("QUANTITY", typeof(decimal));
                    dt.Columns.Add("PACKING_ID", typeof(int));

                    decimal totalQty = 0;

                    foreach (var item in model.Items)
                    {
                        totalQty += item.QUANTITY;

                        dt.Rows.Add(
                            item.SO_DETAIL_ID,
                            item.DN_DETAIL_ID,
                            item.ITEM_ID,
                            item.REMARKS ?? "",
                            item.UOM ?? "",
                            item.QUANTITY,
                            item.PACKING_ID
                        );
                    }

                    cmd.Parameters.AddWithValue("@TOTAL_QTY", totalQty);

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_DR_DETAIL", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_DR_DETAIL";
                    tvp.Value = dt;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Delivery Receipt created successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }

        public ProductionViewResponse GetProductionById(int id)
        {
            var response = new ProductionViewResponse
            {
                RawMaterials = new List<ProductionRawMaterial>(),
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_ARTICLE_PRODUCTION_UPLOAD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", id);

                    // con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // ===== Header =====
                        if (dr.Read())
                        {
                            response.Header = new ProductionHeader
                            {
                                PRODUCTION_ID = Convert.ToInt64(dr["PRODUCTION_ID"]),
                                PROD_NO = dr["PROD_NO"]?.ToString(),
                                PROD_DATE = dr["PROD_DATE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["PROD_DATE"]),
                                PRODUCT_ID = Convert.ToInt32(dr["PRODUCT_ID"]),
                                PRODUCED_QTY = Convert.ToDecimal(dr["PRODUCED_QTY"]),
                                TOTAL_COST = Convert.ToDecimal(dr["TOTAL_COST"]),
                                UNIT_COST = Convert.ToDecimal(dr["UNIT_COST"]),
                                ADDL_COST = Convert.ToDecimal(dr["ADDL_COST"]),
                                ADDL_DESCRIPTION = dr["ADDL_DESCRIPTION"]?.ToString(),
                                TRANS_ID = Convert.ToInt64(dr["TRANS_ID"]),
                                VOUCHER_NO = dr["VOUCHER_NO"]?.ToString(),
                                REF_NO = dr["REF_NO"]?.ToString(),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                COST_PRODUCTION = Convert.ToDecimal(dr["COST_PRODUCTION"]),
                            };
                        }

                        // ===== Raw Materials =====
                        dr.NextResult();
                        while (dr.Read())
                        {
                            response.RawMaterials.Add(new ProductionRawMaterial
                            {
                                ID = Convert.ToInt64(dr["ID"]),
                                ITEM_ID = Convert.ToInt32(dr["ITEM_ID"]),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                UOM = dr["UOM"]?.ToString(),
                                BOM_QTY = Convert.ToDecimal(dr["BOM_QTY"]),
                                REQUIRED_QTY = Convert.ToDecimal(dr["REQUIRED_QTY"]),
                                USED_QTY = Convert.ToDecimal(dr["USED_QTY"]),
                                UNIT_COST = Convert.ToDecimal(dr["UNIT_COST"]),
                                TOTAL_COST = Convert.ToDecimal(dr["TOTAL_COST"]),
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                            });
                        }


                    }
                }

                response.Flag = 1;
                response.Message = "Production data fetched successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public ProductionListResponse GetProductionList(ProductionListRequest model)
        {
            ProductionListResponse response = new ProductionListResponse();
            List<ProductionListItem> list = new List<ProductionListItem>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_ARTICLE_PRODUCTION_UPLOAD", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductionListItem item = new ProductionListItem();

                            item.PRODUCTION_ID = dr["PRODUCTION_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["PRODUCTION_ID"]);
                            item.PROD_NO = dr["PROD_NO"] == DBNull.Value ? "" : dr["PROD_NO"].ToString();
                            item.PROD_DATE = dr["PROD_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["PROD_DATE"]);
                            item.PRODUCED_QTY = dr["PRODUCED_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PRODUCED_QTY"]);
                            item.TOTAL_COST = dr["TOTAL_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TOTAL_COST"]);
                            item.TRANS_ID = dr["TRANS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["TRANS_ID"]);
                            item.VOUCHER_NO = dr["VOUCHER_NO"] == DBNull.Value ? "" : dr["VOUCHER_NO"].ToString();
                            item.ITEM_CODE = dr["ITEM_CODE"] == DBNull.Value ? "" : dr["ITEM_CODE"].ToString();
                            item.DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? "" : dr["DESCRIPTION"].ToString();


                            list.Add(item);
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
                response.Data = new List<ProductionListItem>();
            }

            return response;
        }
        public ProductionViewResponse GetBoxProductionById(int id)
        {
            var response = new ProductionViewResponse
            {
                RawMaterials = new List<ProductionRawMaterial>(),
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_BOX_PRODUCTION_UPLOAD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", id);

                    // con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // ===== Header =====
                        if (dr.Read())
                        {
                            response.Header = new ProductionHeader
                            {
                                PRODUCTION_ID = Convert.ToInt64(dr["PRODUCTION_ID"]),
                                PROD_NO = dr["PROD_NO"]?.ToString(),
                                PROD_DATE = dr["PROD_DATE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["PROD_DATE"]),
                                PRODUCT_ID = Convert.ToInt32(dr["PRODUCT_ID"]),
                                PRODUCED_QTY = Convert.ToDecimal(dr["PRODUCED_QTY"]),
                                TOTAL_COST = Convert.ToDecimal(dr["TOTAL_COST"]),
                                UNIT_COST = Convert.ToDecimal(dr["UNIT_COST"]),
                                ADDL_COST = Convert.ToDecimal(dr["ADDL_COST"]),
                                ADDL_DESCRIPTION = dr["ADDL_DESCRIPTION"]?.ToString(),
                                TRANS_ID = Convert.ToInt64(dr["TRANS_ID"]),
                                VOUCHER_NO = dr["VOUCHER_NO"]?.ToString(),
                                REF_NO = dr["REF_NO"]?.ToString(),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                COST_PRODUCTION = Convert.ToDecimal(dr["COST_PRODUCTION"]),
                            };
                        }

                        // ===== Raw Materials =====
                        dr.NextResult();
                        while (dr.Read())
                        {
                            response.RawMaterials.Add(new ProductionRawMaterial
                            {
                                ID = Convert.ToInt64(dr["ID"]),
                                ITEM_ID = Convert.ToInt32(dr["ITEM_ID"]),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                UOM = dr["UOM"]?.ToString(),
                                BOM_QTY = Convert.ToDecimal(dr["BOM_QTY"]),
                                REQUIRED_QTY = Convert.ToDecimal(dr["REQUIRED_QTY"]),
                                USED_QTY = Convert.ToDecimal(dr["USED_QTY"]),
                                UNIT_COST = Convert.ToDecimal(dr["UNIT_COST"]),
                                TOTAL_COST = Convert.ToDecimal(dr["TOTAL_COST"]),
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                            });
                        }


                    }
                }

                response.Flag = 1;
                response.Message = "Production data fetched successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public ProductionListResponse GetBoxProductionList(ProductionListRequest model)
        {
            ProductionListResponse response = new ProductionListResponse();
            List<ProductionListItem> list = new List<ProductionListItem>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_BOX_PRODUCTION_UPLOAD", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductionListItem item = new ProductionListItem();

                            item.PRODUCTION_ID = dr["PRODUCTION_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["PRODUCTION_ID"]);
                            item.PROD_NO = dr["PROD_NO"] == DBNull.Value ? "" : dr["PROD_NO"].ToString();
                            item.PROD_DATE = dr["PROD_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["PROD_DATE"]);
                            item.PRODUCED_QTY = dr["PRODUCED_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PRODUCED_QTY"]);
                            item.TOTAL_COST = dr["TOTAL_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TOTAL_COST"]);
                            item.TRANS_ID = dr["TRANS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["TRANS_ID"]);
                            item.VOUCHER_NO = dr["VOUCHER_NO"] == DBNull.Value ? "" : dr["VOUCHER_NO"].ToString();
                            item.ITEM_CODE = dr["ITEM_CODE"] == DBNull.Value ? "" : dr["ITEM_CODE"].ToString();
                            item.DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? "" : dr["DESCRIPTION"].ToString();


                            list.Add(item);
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
                response.Data = new List<ProductionListItem>();
            }

            return response;
        }

        public DNListResponse GetDNList(ProductionListRequest model)
        {
            DNListResponse response = new DNListResponse();
            List<DNList> list = new List<DNList>();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_OUT(DN)", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);

                        if (con.State != ConnectionState.Open)
                            con.Open(); // Ensure connection is open

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DNList item = new DNList
                                {
                                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : (int?)null,
                                    DN_DATE = dr["DN_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["DN_DATE"]) : (DateTime?)null,
                                    DN_NO = dr["DN_NO"] != DBNull.Value ? dr["DN_NO"].ToString() : null,
                                    TOTAL_QTY = dr["TOTAL_QTY"] != DBNull.Value ? Convert.ToDouble(dr["TOTAL_QTY"]) : 0,
                                    STATUS = dr["STATUS"] != DBNull.Value ? dr["STATUS"].ToString() : "UNKNOWN",
                                    CUSTOMER_NAME = dr["CUSTOMER_NAME"] != DBNull.Value ? dr["CUSTOMER_NAME"].ToString() : string.Empty,
                                    COMPANY_NAME = dr["COMPANY_NAME"] != DBNull.Value ? dr["COMPANY_NAME"].ToString() : string.Empty
                                };

                                list.Add(item);
                            }
                        }
                    }
                }

                response.Flag = 1;
                response.Message = list.Count > 0 ? "Success" : "No records found";
                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = new List<DNList>();
            }

            return response;
        }



        public DNViewResponse GetProductionDNById(int id)
        {
            var response = new DNViewResponse
            {
                Data = new DNViewHeader
                {
                    Details = new List<DNViewDetail>()
                }
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_OUT(DN)", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // ================= HEADER =================
                        if (dr.Read())
                        {
                            response.Data.ID = Convert.ToInt32(dr["ID"]);
                            response.Data.DN_NO = dr["DN_NO"]?.ToString();
                            response.Data.DN_DATE = Convert.ToDateTime(dr["DN_DATE"]);
                            response.Data.TOTAL_QTY = Convert.ToDouble(dr["TOTAL_QTY"]);

                            response.Data.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                            response.Data.CUST_ID = Convert.ToInt32(dr["CUST_ID"]);
                            response.Data.COMPANY_NAME = dr["CUST_NAME"]?.ToString();
                            response.Data.CONTACT_NAME = dr["CONTACT_NAME"]?.ToString();
                            response.Data.CONTACT_PHONE = dr["CONTACT_PHONE"]?.ToString();
                            response.Data.CONTACT_MOBILE = dr["CONTACT_MOBILE"]?.ToString();
                            response.Data.CONTACT_FAX = dr["CONTACT_FAX"]?.ToString();

                            // ===== first detail row =====
                            response.Data.Details.Add(new DNViewDetail
                            {
                                DETAIL_ID = Convert.ToInt32(dr["DETAIL_ID"]),
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                QUANTITY = Convert.ToDecimal(dr["QUANTITY"]),
                                UOM = dr["UOM"]?.ToString()
                            });
                        }

                        // ================= DETAILS =================
                        while (dr.Read())
                        {
                            response.Data.Details.Add(new DNViewDetail
                            {
                                DETAIL_ID = Convert.ToInt32(dr["DETAIL_ID"]),
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                QUANTITY = Convert.ToDecimal(dr["QUANTITY"]),
                                UOM = dr["UOM"]?.ToString()
                            });
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "DN data fetched successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public GRNUploadResponse SaveProductionTransferInGRN(ProductionTransferInGRN model)
        {
            var response = new GRNUploadResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_IN(GRN)", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // SP Parameters
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@GRN_DATE", model.GRN_DATE);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("PACKING_ID", typeof(int));

                    foreach (var item in model.Items)
                    {
                        dt.Rows.Add(item.PACKING_ID); 
                    }

                    SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_TRANSFER_IN_GRN", dt);
                    tvp.SqlDbType = SqlDbType.Structured;
                    tvp.TypeName = "UDT_TRANSFER_IN_GRN";

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            response.Flag = Convert.ToInt32(dr["Flag"]);
                            response.Message = dr["Message"].ToString();
                            response.GRN_ID = Convert.ToInt32(dr["GRN_ID"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

        public SyncResponse SaveDispatch(DispatchSave model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_DISPATCH", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // ---------- HEADER PARAMS ----------
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID);

                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", (object?)model.DISTRIBUTOR_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUBDEALER_ID", (object?)model.SUBDEALER_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LOCATION_ID", (object?)model.LOCATION_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BRAND_ID", (object?)model.BRAND_ID ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@IS_SHIPPED", model.IS_SHIPPED);
                        cmd.Parameters.AddWithValue("@SO_NO", model.SO_NO ?? "");
                        cmd.Parameters.AddWithValue("@TRANSPORTATION", model.TRANSPORTATION ?? "");

                        cmd.Parameters.AddWithValue("@DISPATCH_TIME", (object?)model.DISPATCH_TIME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SHIPPED_TIME", (object?)model.SHIPPED_TIME ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);

                        cmd.Parameters.AddWithValue("@IS_RETURN", model.IS_RETURN);
                        cmd.Parameters.AddWithValue("@RETURN_TIME", (object?)model.RETURN_TIME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@RETURN_USER_ID", model.RETURN_USER_ID);
                        cmd.Parameters.AddWithValue("@RETURN_REASON", model.RETURN_REASON ?? "");

                        cmd.Parameters.AddWithValue("@IS_COMMITTED", model.IS_COMMITTED);
                        cmd.Parameters.AddWithValue("@UPDATED_DATE", (object?)model.UPDATED_DATE ?? DBNull.Value);

                        // ---------- BUILD UDT ----------
                        DataTable dtDispatch = new DataTable();
                        dtDispatch.Columns.Add("BOX_ID", typeof(int));
                        dtDispatch.Columns.Add("ORDER_DETAIL_ID", typeof(int));

                        if (model.Boxes != null && model.Boxes.Count > 0)
                        {
                            foreach (var box in model.Boxes)
                            {
                                dtDispatch.Rows.Add(
                                    box.BOX_ID,
                                    box.ORDER_DETAIL_ID ?? 0
                                );
                            }
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_DISPATCH_DETAIL", dtDispatch);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "dbo.UDT_DISPATCH_DETAIL";

                        // ---------- EXEC ----------
                       // connection.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                response.Flag = Convert.ToInt32(dr["Flag"]);
                                response.Message = dr["Message"].ToString();
                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }

    }
}
