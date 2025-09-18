using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                            cmd.Parameters.AddWithValue("@TRANS_ID", stockAdjustment.TRANS_ID ?? 0);
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
            StockAdjustmentDetailResponse response = new StockAdjustmentDetailResponse 
            {
                Data = new StockAdjustment
                {
                    Details = new List<StockAdjustmentDetail>()
                }
               }
            ;

            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_STOCK_ADJUSTMENT", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);  // Action for select by ID
                    cmd.Parameters.AddWithValue("@ADJ_ID", adjId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read header information (assuming the first row contains header info)
                        if (reader.Read())
                        {
                            response.Data.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                            response.Data.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0;
                            response.Data.STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0;
                            response.Data.ADJ_NO = reader["ADJ_NO"] != DBNull.Value ? reader["ADJ_NO"].ToString() : null;
                            response.Data.ADJ_DATE = reader["ADJ_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["ADJ_DATE"]).ToString("yyyy-MM-dd") : null;
                            response.Data.REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : 0;
                            response.Data.FIN_ID = reader["FIN_ID"] != DBNull.Value ? Convert.ToInt32(reader["FIN_ID"]) : 0;
                            response.Data.TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;
                           // response.Data.CREDIT_HEAD_ID = reader["CREDIT_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["CREDIT_HEAD_ID"]) : 0;
                            response.Data.NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0;
                            response.Data.NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null;
                            response.Data.STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0;
                        }

                        // Read details
                        if (reader.NextResult())  // Assuming the stored procedure returns a second result set for details
                        {
                            while (reader.Read())
                            {
                                StockAdjustmentDetail detail = new StockAdjustmentDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                    STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0,
                                    ADJ_ID = reader["ADJ_ID"] != DBNull.Value ? Convert.ToInt32(reader["ADJ_ID"]) : 0,
                                    REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : 0,
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                    ITEM_CODE =  reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    ITEM_NAME= reader["ITEM_NAME"] != DBNull.Value ? reader["ITEM_NAME"].ToString() : null,
                                    COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : 0,
                                    STOCK_QTY = reader["STOCK_QTY"] != DBNull.Value ? Convert.ToSingle(reader["STOCK_QTY"]) : 0,
                                    NEW_QTY = reader["NEW_QTY"] != DBNull.Value ? Convert.ToSingle(reader["NEW_QTY"]) : 0,
                                    ADJ_QTY = reader["ADJ_QTY"] != DBNull.Value ? Convert.ToSingle(reader["ADJ_QTY"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    BATCH_NO = reader["BATCH_NO"] != DBNull.Value ? reader["BATCH_NO"].ToString() : null,
                                    EXPIRY_DATE = reader["EXPIRY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["EXPIRY_DATE"]) : (DateTime?)null
                                };
                                response.Data.Details.Add(detail);
                            }
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
                                ADJ_DATE = reader["ADJ_DATE"] != DBNull.Value ? reader["ADJ_DATE"].ToString() : null,
                                ADJ_NO = reader["ADJ_NO"] != DBNull.Value ? reader["ADJ_NO"].ToString() : null,
                                COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                STORE_NAME = reader["STORE_NAME"] != DBNull.Value ? reader["STORE_NAME"].ToString() : null,
                                REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : (int?)null,
                                REASON_DESCRIPTION = reader["REASON_DESCRIPTION"] != DBNull.Value ? Convert.ToString(reader["REASON_DESCRIPTION"]) : null,
                                TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                NARRATION = reader["NARRATION"] != DBNull.Value ? Convert.ToString(reader["NARRATION"]) : null,
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
                                    STOCK_QTY = reader["STOCK_QTY"] != DBNull.Value ? Convert.ToSingle(reader["STOCK_QTY"]) : (float?)0
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
        public StockAdjustmentResponse ApproveStockAdjustment(StockAdjustmentApproval stockAdjustment)
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
                            cmd.Parameters.AddWithValue("@ACTION", 7);  // Action for approval
                            cmd.Parameters.AddWithValue("@ADJ_ID", stockAdjustment.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", stockAdjustment.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", stockAdjustment.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@ADJ_DATE", stockAdjustment.ADJ_DATE);
                            cmd.Parameters.AddWithValue("@REASON_ID", stockAdjustment.REASON_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", stockAdjustment.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", stockAdjustment.NET_AMOUNT ?? 0);
                           // cmd.Parameters.AddWithValue("@STATUS", stockAdjustment.STATUS ?? 0);

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ITEM_ADJ_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_ITEM_ADJ_DETAIL";

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        response.Flag = "1";
                        response.Message = "Approval successful";
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

    }
}
