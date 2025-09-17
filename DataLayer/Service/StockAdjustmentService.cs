using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy" };

            if (DateTime.TryParseExact(dateStr, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public int SaveData(StockAdjustment stockAdjustment)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("REASON_ID", typeof(int));
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("COST", typeof(float));
                        tvp.Columns.Add("STOCK_QTY", typeof(float));
                        tvp.Columns.Add("NEW_QTY", typeof(float));
                        tvp.Columns.Add("ADJ_QTY", typeof(float));
                        tvp.Columns.Add("AMOUNT", typeof(float));
                        tvp.Columns.Add("BATCH_NO", typeof(string));
                        tvp.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                        foreach (var detail in stockAdjustment.Details)
                        {
                            tvp.Rows.Add(
                                detail.REASON_ID ?? 0,
                                detail.ITEM_ID ?? 0,
                                detail.COST ?? 0,
                                detail.STOCK_QTY ?? 0,
                                detail.NEW_QTY ?? 0,
                                detail.ADJ_QTY ?? 0,
                                detail.AMOUNT ?? 0,
                                detail.BATCH_NO ?? "0",
                                detail.EXPIRY_DATE ?? DateTime.Now
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 1);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", stockAdjustment.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", stockAdjustment.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@ADJ_NO", stockAdjustment.ADJ_NO ?? "");
                            cmd.Parameters.AddWithValue("@ADJ_DATE", ParseDate(stockAdjustment.ADJ_DATE));
                            cmd.Parameters.AddWithValue("@REASON_ID", stockAdjustment.REASON_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", stockAdjustment.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", stockAdjustment.NET_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", stockAdjustment.USER_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", stockAdjustment.NARRATION ?? "");
                            //cmd.Parameters.AddWithValue("@STATUS", stockAdjustment.STATUS ?? false);

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ITEM_ADJ_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_ITEM_ADJ_DETAIL";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return stockAdjustment.ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }
        public StockAdjustmentResponse EditData(StockAdjustmentUpdate stockAdjustment)
        {
            StockAdjustmentResponse response = new StockAdjustmentResponse();

            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("REASON_ID", typeof(int));
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("COST", typeof(float));
                        tvp.Columns.Add("STOCK_QTY", typeof(float));
                        tvp.Columns.Add("NEW_QTY", typeof(float));
                        tvp.Columns.Add("ADJ_QTY", typeof(float));
                        tvp.Columns.Add("AMOUNT", typeof(float));
                        tvp.Columns.Add("BATCH_NO", typeof(string));
                        tvp.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                        foreach (var detail in stockAdjustment.Details)
                        {
                            tvp.Rows.Add(
                                detail.REASON_ID ?? 0,
                                detail.ITEM_ID ?? 0,
                                detail.COST ?? 0,
                                detail.STOCK_QTY ?? 0,
                                detail.NEW_QTY ?? 0,
                                detail.ADJ_QTY ?? 0,
                                detail.AMOUNT ?? 0,
                                detail.BATCH_NO ?? "0",
                                detail.EXPIRY_DATE ?? DateTime.Now
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2);
                            cmd.Parameters.AddWithValue("@ADJ_ID", stockAdjustment.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", stockAdjustment.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", stockAdjustment.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@ADJ_DATE", stockAdjustment.ADJ_DATE);
                            cmd.Parameters.AddWithValue("@REASON_ID", stockAdjustment.REASON_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", stockAdjustment.FIN_ID ?? 0);
                            //cmd.Parameters.AddWithValue("@TRANS_ID", stockAdjustment.TRANS_ID ?? 0);
                           // cmd.Parameters.AddWithValue("@CREDIT_HEAD_ID", stockAdjustment.CREDIT_HEAD_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", stockAdjustment.NET_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", stockAdjustment.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ITEM_ADJ_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_ITEM_ADJ_DETAIL";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        response.Flag = "1";
                        response.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Flag = "0";
                        response.Message = ex.Message;
                    }
                }
            }
            return response;
        }
        public StockAdjustmentDetailResponse GetStockAdjustment(int adjId)
        {
            StockAdjustmentDetailResponse response = new StockAdjustmentDetailResponse { Data = new List<StockAdjustmentDetail>() };

            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@ADJ_ID", adjId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StockAdjustmentDetail detail = new StockAdjustmentDetail
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                //ADJ_ID = reader["ADJ_ID"] != DBNull.Value ? Convert.ToInt32(reader["ADJ_ID"]) : (int?)null,
                                REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : (int?)null,
                                ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : (float?)null,
                                COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : (float?)null,
                                STOCK_QTY = reader["STOCK_QTY"] != DBNull.Value ? Convert.ToSingle(reader["STOCK_QTY"]) : (float?)null,
                                NEW_QTY = reader["NEW_QTY"] != DBNull.Value ? Convert.ToSingle(reader["NEW_QTY"]) : (float?)null,
                                ADJ_QTY = reader["ADJ_QTY"] != DBNull.Value ? Convert.ToSingle(reader["ADJ_QTY"]) : (float?)null,
                                AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : (float?)null,
                                BATCH_NO = reader["BATCH_NO"] != DBNull.Value ? reader["BATCH_NO"].ToString() : null,
                                EXPIRY_DATE = reader["EXPIRY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["EXPIRY_DATE"]) : (DateTime?)null
                            };
                            response.Data.Add(detail);
                        }
                    }
                }
            }

            response.Flag = 1;
            response.Message = "Success";
            return response;
        }
 

        public StockAdjustmentListResponse GetAllStockAdjustments()
        {
            StockAdjustmentListResponse response = new StockAdjustmentListResponse { Data = new List<StockAdjustmentList>() };

            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    //cmd.Parameters.AddWithValue("@STORE_ID");
                    //cmd.Parameters.AddWithValue("@COMPANY_ID");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StockAdjustmentList adjustment = new StockAdjustmentList
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                ADJ_NO = reader["ADJ_NO"] != DBNull.Value ? reader["ADJ_NO"].ToString() : null,
                                COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                ADJ_DATE = reader["ADJ_DATE"] != DBNull.Value ? Convert.ToString(reader["ADJ_DATE"]) : null,
                                REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : (int?)null,
                                DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? Convert.ToString(reader["DESCRIPTION"]) : null,
                                NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : (float?)null,
                                ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? Convert.ToString(reader["ITEM_CODE"]) : null,
                                ITEM_NAME = reader["ITEM_NAME"] != DBNull.Value ? Convert.ToString(reader["ITEM_NAME"]) : null,
                                COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : (float?)null,
                                STOCK_QTY = reader["STOCK_QTY"] != DBNull.Value ? Convert.ToSingle(reader["STOCK_QTY"]) : (float?)null,
                                AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : (float?)null,
                            };

                            response.Data.Add(adjustment);
                        }
                    }
                }
            }

            response.Flag = 1;
            response.Message = "Success";
            return response;
        }
        public bool DeleteStockAdjustment(int adjId)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@ADJ_ID", adjId);

                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        public StockItemListResponse GetStockAdjustmentItems(StockAdjustmentRequest request)
        {
            StockItemListResponse response = new StockItemListResponse { Data = new List<StockItem>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6); // Action for listing items
                        cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);
                        //cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StockItem item = new StockItem
                                {
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                    ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    MATRIX_CODE = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : null,
                                    COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : (float?)0,
                                    CURRENT_STOCK = reader["CURRENT_STOCK"] != DBNull.Value ? Convert.ToSingle(reader["CURRENT_STOCK"]) : (float?)0
                                };
                                response.Data.Add(item);
                            }
                        }
                    }
                }
                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = null;
            }

            return response;
        }

    }
}
