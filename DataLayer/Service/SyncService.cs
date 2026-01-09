using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
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

                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                        

                        DataTable dtUDT = new DataTable();
                        dtUDT.Columns.Add("ARTICLE_ID", typeof(long));
                        dtUDT.Columns.Add("ARTICLE_PRODUCTION_ID", typeof(long));
                        //dtUDT.Columns.Add("PAIRS", typeof(int));
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
                                    //item.PAIRS,
                                    item.BOX_ID,
                                    item.BARCODE ?? "",
                                    Convert.ToDouble(item.PRICE),item.PRODUCTION_DATE
                                );
                            }

                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_ARTICLE_PRODUCTION", dtUDT);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_ARTICLE_PRODUCTION";

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
                using (SqlCommand cmd = new SqlCommand("SP_PACK_PRODUCTION_UPLOAD", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);

                    DataTable dtUDT = new DataTable();
                    dtUDT.Columns.Add("PACK_PRODUCTION_ID", typeof(long));
                    dtUDT.Columns.Add("PACKING_ID", typeof(long));
                    dtUDT.Columns.Add("SMALL_BOX_ID", typeof(int));
                    dtUDT.Columns.Add("MASTER_CARTON_ID", typeof(int));
                    dtUDT.Columns.Add("BOX_SERIAL", typeof(int));
                    dtUDT.Columns.Add("QTY", typeof(float));
                    dtUDT.Columns.Add("BARCODE", typeof(string));
                    dtUDT.Columns.Add("BOX_PRICE", typeof(float));
                    dtUDT.Columns.Add("PRODUCTION_DATE", typeof(DateTime));

                    foreach (var item in model.PackItems)
                    {
                        dtUDT.Rows.Add(
                            item.PACK_PRODUCTION_ID,
                            item.PACKING_ID, item.SMALL_BOX_ID, item.MASTER_CARTON_ID,
                            item.BOX_SERIAL ?? "",item.QTY,
                            item.BARCODE ?? "",
                            item.BOX_PRICE,item.PRODUCTION_DATE
                        );
                    }

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_PACK_PRODUCTION", SqlDbType.Structured);
                    tvp.TypeName = "UDT_PACK_PRODUCTION";
                    tvp.Value = dtUDT;

                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Pack production uploaded successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
            }

            return response;
        }
        public SyncResponse UploadProductionTransferOut(ProductionTransferOut model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_OUT", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                    cmd.Parameters.AddWithValue("@TRANSFER_DATE", model.TRANSFER_DATE);
                    cmd.Parameters.AddWithValue("@DEST_STORE_ID", model.DEST_STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@REASON_ID", (object?)model.REASON_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRANS_ID", 0);

                    // 🔹 Build TVP
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("UOM", typeof(string));
                    dt.Columns.Add("QUANTITY", typeof(decimal));
                    dt.Columns.Add("COST", typeof(decimal));
                    dt.Columns.Add("AMOUNT", typeof(decimal));
                    dt.Columns.Add("BATCH_NO", typeof(string));
                    dt.Columns.Add("EXPIRY_DATE", typeof(DateTime));
                    dt.Columns.Add("PACKING_ID", typeof(int));

                    decimal netAmount = 0;

                    foreach (var item in model.TransferItems)
                    {
                        decimal qty = item.QUANTITY ?? 0m;
                        decimal cost = (decimal)(item.COST ?? 0);
                        decimal amount = qty * cost;

                        netAmount += amount;

                        dt.Rows.Add(
                            item.ITEM_ID,
                            item.UOM ?? "",
                            qty,
                            cost,
                            amount,
                            item.BATCH_NO ?? "",
                            item.EXPIRY_DATE ?? (object)DBNull.Value,
                            item.PACKING_ID
                        );
                    }

                    // 🔹 Send computed NET_AMOUNT
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", netAmount);

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_TRANSFER_DETAIL", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_TRANSFER_DETAIL";
                    tvp.Value = dt;

                   // connection.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Production transfer completed successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }

        public SyncResponse UploadProductionTransferIn(ProductionTransferIn model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_TRANSFER_IN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                    cmd.Parameters.AddWithValue("@REC_DATE", model.REC_DATE);
                    cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", model.ORIGIN_STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@ISSUE_ID", (object?)model.ISSUE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object?)model.REASON_ID ?? DBNull.Value);

                    // 🔹 Calculate NET_AMOUNT properly
                    decimal netAmount = model.Items.Sum(x => (x.QUANTITY ?? 0) * (x.COST ?? 0));
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", netAmount);
                    cmd.Parameters.AddWithValue("@TRANS_ID", 0);

                    // 🔹 Build TVP DataTable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ISSUE_DETAIL_ID", typeof(int));
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("UOM", typeof(string));
                    dt.Columns.Add("COST", typeof(double));
                    dt.Columns.Add("ISSUE_QTY", typeof(decimal));
                    dt.Columns.Add("QUANTITY", typeof(decimal));
                    dt.Columns.Add("BATCH_NO", typeof(string));
                    dt.Columns.Add("EXPIRY_DATE", typeof(DateTime));
                    dt.Columns.Add("PACKING_ID", typeof(int));

                    foreach (var item in model.Items)
                    {
                        dt.Rows.Add(
                            item.ISSUE_DETAIL_ID ?? 0,
                            item.ITEM_ID,
                            item.UOM ?? "",
                            item.COST ?? 0,
                            item.QUANTITY_ISSUED ?? 0,
                            item.QUANTITY ?? 0,
                            item.BATCH_NO ?? "",
                            (object?)item.EXPIRY_DATE ?? DBNull.Value,
                            item.PACKING_ID
                        );
                    }

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_TRANSFERINV_IN", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_TRANSFERINV_IN";
                    tvp.Value = dt;

                   // con.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Warehouse Transfer In completed successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }
        public SyncResponse UploadProductionDN(ProductionDN model)
        {
            SyncResponse response = new SyncResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_PRODUCTION_DN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                    cmd.Parameters.AddWithValue("@DN_DATE", model.DN_DATE);
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
                    cmd.Parameters.AddWithValue("@DN_TYPE", model.DN_TYPE);

                    // 🔹 Build TVP
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SO_DETAIL_ID", typeof(int));
                    dt.Columns.Add("REMARKS", typeof(string));
                    dt.Columns.Add("QUANTITY", typeof(decimal));
                    dt.Columns.Add("PACKING_ID", typeof(int));

                    decimal totalQty = 0;

                    foreach (var item in model.Items)
                    {
                        totalQty += item.QUANTITY;
                        dt.Rows.Add(item.SO_DETAIL_ID, item.REMARKS ?? "", item.QUANTITY, item.PACKING_ID);
                    }

                    cmd.Parameters.AddWithValue("@TOTAL_QTY", totalQty);

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_DN_DETAIL", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_DN_DETAIL";
                    tvp.Value = dt;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Delivery Note created successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }
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


    }
}
