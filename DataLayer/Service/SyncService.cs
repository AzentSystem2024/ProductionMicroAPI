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

                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID);
                        cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);

                        DataTable dtUDT = new DataTable();
                        dtUDT.Columns.Add("ARTICLE_ID", typeof(long));
                        dtUDT.Columns.Add("ARTICLE_PRODUCTION_ID", typeof(long));
                        dtUDT.Columns.Add("PAIRS", typeof(int));
                        dtUDT.Columns.Add("BOX_ID", typeof(long));
                        dtUDT.Columns.Add("BARCODE", typeof(string));
                        dtUDT.Columns.Add("PRICE", typeof(double));

                        if (model.Articles != null && model.Articles.Count > 0)
                        {
                            foreach (var item in model.Articles)
                            {
                                dtUDT.Rows.Add(
                                    item.ARTICLE_ID,
                                    item.ARTICLE_PRODUCTION_ID,
                                    item.PAIRS,
                                    item.BOX_ID,
                                    item.BARCODE ?? "",
                                    Convert.ToDouble(item.PRICE)
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

                    cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID);
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

                    foreach (var item in model.PackItems)
                    {
                        dtUDT.Rows.Add(
                            item.PACK_PRODUCTION_ID,
                            item.PACKING_ID, item.SMALL_BOX_ID, item.MASTER_CARTON_ID,
                            item.BOX_SERIAL ?? "",item.QTY,
                            item.BARCODE ?? "",
                            item.BOX_PRICE
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

                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                    cmd.Parameters.AddWithValue("@TRANSFER_DATE", model.TRANSFER_DATE);
                    cmd.Parameters.AddWithValue("@DEST_STORE_ID", model.DEST_STORE_ID);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)model.REASON_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRANS_ID", 0);

                    // 🔹 Build TVP
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("UOM", typeof(string));
                    dt.Columns.Add("QUANTITY", typeof(decimal));
                    dt.Columns.Add("COST", typeof(double));
                    dt.Columns.Add("AMOUNT", typeof(double));
                    dt.Columns.Add("BATCH_NO", typeof(string));
                    dt.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                    foreach (var item in model.TransferItems)
                    {
                        decimal quantity = item.QUANTITY ?? 0;
                        double cost = item.COST ?? 0;
                        double amount = (double)quantity * cost;

                        dt.Rows.Add(
                            item.ITEM_ID,
                            item.UOM ?? "",
                            quantity,cost, amount,
                            item.BATCH_NO ?? "",
                            item.EXPIRY_DATE ?? (object)DBNull.Value
                        );
                    }

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_TRANSFER_DETAIL", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_TRANSFER_DETAIL";
                    tvp.Value = dt;

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
                    cmd.Parameters.AddWithValue("@TROUT_ID", 0);
                    cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", model.ORIGIN_STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? "");
                    cmd.Parameters.AddWithValue("@ISSUE_ID", (object?)model.ISSUE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object?)model.REASON_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT);
                    cmd.Parameters.AddWithValue("@TRANS_ID", 0);

                    // 🔹 Build TVP
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ISSUE_DETAIL_ID", typeof(int));
                    dt.Columns.Add("ITEM_ID", typeof(int));
                    dt.Columns.Add("UOM", typeof(string));
                    dt.Columns.Add("COST", typeof(double));
                    dt.Columns.Add("ISSUE_QTY", typeof(decimal));
                    dt.Columns.Add("QUANTITY", typeof(decimal));
                    dt.Columns.Add("BATCH_NO", typeof(string));
                    dt.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                    foreach (var item in model.Items)
                    {
                        dt.Rows.Add(
                            0,
                            item.ITEM_ID,
                            item.UOM ?? "",
                            item.COST,
                            item.QUANTITY,
                            item.QUANTITY,
                            item.BATCH_NO ?? "",
                            (object?)item.EXPIRY_DATE ?? DBNull.Value
                        );
                    }

                    SqlParameter tvp = cmd.Parameters.Add("@UDT_TB_TRANSFERINV_IN", SqlDbType.Structured);
                    tvp.TypeName = "UDT_TB_TRANSFERINV_IN";
                    tvp.Value = dt;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Warehouse Transfer In completed successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
            }

            return response;
        }

    }
}
