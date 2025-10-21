    using MicroApi.DataLayer.Interface;
    using MicroApi.Helper;
    using MicroApi.Models;
    using System.Data;
    using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class PhysicalStockService : IPhysicalStockService
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

        public int SaveData(PhysicalStock physicalStock)
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
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("QTY_OH", typeof(float));
                        tvp.Columns.Add("COST", typeof(float));
                        tvp.Columns.Add("QTY_COUNT", typeof(float));
                        tvp.Columns.Add("ADJUSTED_QTY", typeof(decimal));
                        tvp.Columns.Add("BATCH_NO", typeof(string));
                        tvp.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                        foreach (var detail in physicalStock.Details)
                        {
                            tvp.Rows.Add(
                                detail.ITEM_ID ?? 0,
                                detail.QTY_OH ?? 0,
                                detail.COST ?? 0,
                                detail.QTY_COUNT ?? 0,
                                detail.ADJUSTED_QTY ?? 0,
                                detail.BATCH_NO ?? "0",
                                detail.EXPIRY_DATE ?? DateTime.Now
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 1);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", physicalStock.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", physicalStock.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@PHYSICAL_NO", physicalStock.PHYSICAL_NO ?? "");
                            cmd.Parameters.AddWithValue("@PHYSICAL_DATE", ParseDate(physicalStock.PHYSICAL_DATE));
                            cmd.Parameters.AddWithValue("@REASON_ID", physicalStock.REASON_ID ?? 0);
                            cmd.Parameters.AddWithValue("@REFERENCE_NO", physicalStock.REFERENCE_NO ?? "");
                            cmd.Parameters.AddWithValue("@FIN_ID", physicalStock.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", physicalStock.USER_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", physicalStock.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ITEM_PHYSICAL_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_ITEM_PHYSICAL_DETAIL";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return physicalStock.ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }

        public PhysicalStockResponse EditData(PhysicalStockUpdate physicalStock)
        {
            PhysicalStockResponse response = new PhysicalStockResponse();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("QTY_OH", typeof(float));
                        tvp.Columns.Add("COST", typeof(float));
                        tvp.Columns.Add("QTY_COUNT", typeof(float));
                        tvp.Columns.Add("ADJUSTED_QTY", typeof(decimal));
                        tvp.Columns.Add("BATCH_NO", typeof(string));
                        tvp.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                        foreach (var detail in physicalStock.Details)
                        {
                            tvp.Rows.Add(
                                detail.ITEM_ID ?? 0,
                                detail.QTY_OH ?? 0,
                                detail.COST ?? 0,
                                detail.QTY_COUNT ?? 0,
                                detail.ADJUSTED_QTY ?? 0,
                                detail.BATCH_NO ?? "0",
                                detail.EXPIRY_DATE ?? DateTime.Now
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2);
                            cmd.Parameters.AddWithValue("@PHYSICAL_ID", physicalStock.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", physicalStock.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", physicalStock.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@PHYSICAL_DATE", physicalStock.PHYSICAL_DATE);
                            cmd.Parameters.AddWithValue("@REASON_ID", physicalStock.REASON_ID ?? 0);
                            cmd.Parameters.AddWithValue("@REFERENCE_NO", physicalStock.REFERENCE_NO ?? "");
                            cmd.Parameters.AddWithValue("@FIN_ID", physicalStock.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", physicalStock.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ITEM_PHYSICAL_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_ITEM_PHYSICAL_DETAIL";

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

        public PhysicalStockSelectDetailResponse GetPhysicalStock(int physicalId)
        {
            PhysicalStockSelectDetailResponse response = new PhysicalStockSelectDetailResponse { Data = new PhysicalStockSelect { Details = new List<PhysicalStockSelectDetail>() } };
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@PHYSICAL_ID", physicalId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.Data.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                            response.Data.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0;
                            response.Data.STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0;
                            response.Data.PHYSICAL_NO = reader["PHYSICAL_NO"] != DBNull.Value ? reader["PHYSICAL_NO"].ToString() : null;
                            response.Data.PHYSICAL_DATE = reader["PHYSICAL_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PHYSICAL_DATE"]).ToString("yyyy-MM-dd") : null;
                            response.Data.REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : 0;
                            response.Data.REASON_NAME = reader["REASON_NAME"] != DBNull.Value ? reader["REASON_NAME"].ToString() : null;
                            response.Data.REFERENCE_NO = reader["REFERENCE_NO"] != DBNull.Value ? reader["REFERENCE_NO"].ToString() : null;
                            response.Data.FIN_ID = reader["FIN_ID"] != DBNull.Value ? Convert.ToInt32(reader["FIN_ID"]) : 0;
                            response.Data.TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;
                            response.Data.NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null;
                            response.Data.STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0;
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                PhysicalStockSelectDetail detail = new PhysicalStockSelectDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                    STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0,
                                    PHYSICAL_ID = reader["PHYSICAL_ID"] != DBNull.Value ? Convert.ToInt32(reader["PHYSICAL_ID"]) : 0,
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                    ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    ITEM_NAME = reader["ITEM_NAME"] != DBNull.Value ? reader["ITEM_NAME"].ToString() : null,
                                    QTY_OH = reader["QTY_OH"] != DBNull.Value ? Convert.ToSingle(reader["QTY_OH"]) : 0,
                                    COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : 0,
                                    QTY_COUNT = reader["QTY_COUNT"] != DBNull.Value ? Convert.ToSingle(reader["QTY_COUNT"]) : 0,
                                    COUNT_TIME = reader["COUNT_TIME"] != DBNull.Value ? Convert.ToDateTime(reader["COUNT_TIME"]) : (DateTime?)null,
                                    ADJUSTED_QTY = reader["ADJUSTED_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["ADJUSTED_QTY"]) : 0,
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

        public PhysicalStockListResponse GetAllPhysicalStocks()
        {
            PhysicalStockListResponse response = new PhysicalStockListResponse { Data = new List<PhysicalStockList>() };
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhysicalStockList physicalStock = new PhysicalStockList
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                PHYSICAL_DATE = reader["PHYSICAL_DATE"] != DBNull.Value ? reader["PHYSICAL_DATE"].ToString() : null,
                                PHYSICAL_NO = reader["PHYSICAL_NO"] != DBNull.Value ? reader["PHYSICAL_NO"].ToString() : null,
                                COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                STORE_NAME = reader["STORE_NAME"] != DBNull.Value ? reader["STORE_NAME"].ToString() : null,
                                MATRIX_CODE = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : null,
                                REASON_ID = reader["REASON_ID"] != DBNull.Value ? Convert.ToInt32(reader["REASON_ID"]) : (int?)null,
                                REASON_DESCRIPTION = reader["REASON_DESCRIPTION"] != DBNull.Value ? Convert.ToString(reader["REASON_DESCRIPTION"]) : null,
                                TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                NARRATION = reader["NARRATION"] != DBNull.Value ? Convert.ToString(reader["NARRATION"]) : null,
                            };
                            response.Data.Add(physicalStock);
                        }
                    }
                }
            }
            response.Flag = 1;
            response.Message = "Success";
            return response;
        }

        public bool DeletePhysicalStock(int physicalId)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@PHYSICAL_ID", physicalId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public StockItemListResponse GetPhysicalStockItems(PhysicalStockRequest request)
        {
            StockItemListResponse response = new StockItemListResponse { Data = new List<StockItem>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);

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

        public PhysicalStockResponse ApprovePhysicalStock(PhysicalStockApproval physicalStock)
        {
            PhysicalStockResponse response = new PhysicalStockResponse();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("QTY_OH", typeof(float));
                        tvp.Columns.Add("COST", typeof(float));
                        tvp.Columns.Add("QTY_COUNT", typeof(float));
                        tvp.Columns.Add("ADJUSTED_QTY", typeof(decimal));
                        tvp.Columns.Add("BATCH_NO", typeof(string));
                        tvp.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                        foreach (var detail in physicalStock.Details)
                        {
                            tvp.Rows.Add(
                                detail.ITEM_ID ?? 0,
                                detail.QTY_OH ?? 0,
                                detail.COST ?? 0,
                                detail.QTY_COUNT ?? 0,
                                detail.ADJUSTED_QTY ?? 0,
                                detail.BATCH_NO ?? "0",
                                detail.EXPIRY_DATE ?? DateTime.Now
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 7);
                            cmd.Parameters.AddWithValue("@PHYSICAL_ID", physicalStock.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", physicalStock.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", physicalStock.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@PHYSICAL_DATE", physicalStock.PHYSICAL_DATE);
                            cmd.Parameters.AddWithValue("@REASON_ID", physicalStock.REASON_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", physicalStock.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", physicalStock.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ITEM_PHYSICAL_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_ITEM_PHYSICAL_DETAIL";

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
        public List<StockItems> GetFilteredItems(FilteredItemsRequest request)
        {
            List<StockItems> items = new List<StockItems>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 8);

                    // Create a DataTable for the UDT parameter
                    DataTable filterIdsTable = new DataTable();
                    filterIdsTable.Columns.Add("FilterType", typeof(string));
                    filterIdsTable.Columns.Add("Id", typeof(int));

                    // Add rows for each filter
                    if (request.FilterIds != null && request.FilterIds.Count > 0)
                    {
                        foreach (var filter in request.FilterIds)
                        {
                            filterIdsTable.Rows.Add(filter.FilterType, filter.Id);
                        }
                    }

                    // Add the UDT parameter
                    SqlParameter filterIdsParam = cmd.Parameters.AddWithValue("@FilterIds", filterIdsTable);
                    filterIdsParam.SqlDbType = SqlDbType.Structured;
                    filterIdsParam.TypeName = "dbo.UDT_FilterIds";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StockItems item = new StockItems
                            {
                                ItemId = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                ItemCode = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : string.Empty,
                                Description = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : string.Empty,
                                MatrixCode = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : string.Empty,
                                Cost = reader["COST"] != DBNull.Value ? Convert.ToDecimal(reader["COST"]) : 0,
                               // StoreId = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0,
                                StockQty = reader["STOCK_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["STOCK_QTY"]) : 0,
                                DeptId = reader["DEPT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPT_ID"]) : 0,
                                DeptName = reader["DEPT_NAME"] != DBNull.Value ? reader["DEPT_NAME"].ToString() : string.Empty,
                                CatId = reader["CAT_ID"] != DBNull.Value ? Convert.ToInt32(reader["CAT_ID"]) : 0,
                                CatName = reader["CAT_NAME"] != DBNull.Value ? reader["CAT_NAME"].ToString() : string.Empty,
                                Brand_Id = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : 0,
                                BrandName = reader["BRAND_NAME"] != DBNull.Value ? reader["BRAND_NAME"].ToString() : string.Empty,
                                Supp_Name = reader["SUPP_NAME"] != DBNull.Value ? reader["SUPP_NAME"].ToString() : string.Empty,
                                SuppId = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0
                            };
                            items.Add(item);
                        }
                    }
                }
            }
            return items;
        }
        public ItemCodeListResponse GetPhysicalStockItemsByItemCodes(ItemCodeRequest request)
        {
            ItemCodeListResponse response = new ItemCodeListResponse { Data = new List<ItemDetailsModel>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 9);

                        // Create a DataTable for the UDT parameter
                        DataTable itemCodesTable = new DataTable();
                        itemCodesTable.Columns.Add("BARCODE", typeof(string));

                        // Add rows for each item code
                        foreach (var itemCode in request.BarCodes)
                        {
                            itemCodesTable.Rows.Add(itemCode);
                        }

                        // Add the UDT parameter
                        SqlParameter itemCodesParam = cmd.Parameters.AddWithValue("@BARCODES", itemCodesTable);
                        itemCodesParam.SqlDbType = SqlDbType.Structured;
                        itemCodesParam.TypeName = "dbo.UDT_BarCodes";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemDetailsModel item = new ItemDetailsModel
                                {
                                    ItemId = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                    ItemCode = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    ItemName = reader["ITEM_NAME"] != DBNull.Value ? reader["ITEM_NAME"].ToString() : null,
                                    MatrixCode = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : null,
                                    Barcode = reader["BARCODE"] != DBNull.Value ? reader["BARCODE"].ToString() : null,
                                    CatId = reader["CAT_ID"] != DBNull.Value ? Convert.ToInt32(reader["CAT_ID"]) : (int?)null,
                                    CatName = reader["CAT_NAME"] != DBNull.Value ? reader["CAT_NAME"].ToString() : null,
                                    BrandId = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : (int?)null,
                                    BrandName = reader["BRAND_NAME"] != DBNull.Value ? reader["BRAND_NAME"].ToString() : null,
                                    DeptId = reader["DEPT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPT_ID"]) : (int?)null,
                                    DeptName = reader["DEPT_NAME"] != DBNull.Value ? reader["DEPT_NAME"].ToString() : null,
                                    //COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : (float?)0,
                                    QtyStock = reader["QTY_STOCK"] != DBNull.Value ? Convert.ToDecimal(reader["QTY_STOCK"]) : 0,
                                    // Add other fields as needed
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

        public List<HistoryModel> GetPhysicalStockHistory()
        {
            List<HistoryModel> history = new List<HistoryModel>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 10);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HistoryModel model = new HistoryModel
                            {
                                Action = reader["ACTION"] != DBNull.Value ? Convert.ToInt32(reader["ACTION"]) : 0,
                                Time = reader["TIME"] != DBNull.Value ? Convert.ToDateTime(reader["TIME"]) : DateTime.MinValue,
                                Description = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : string.Empty,
                                DocTypeId = reader["DOC_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["DOC_TYPE_ID"]) : 0,
                                UserId = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : 0,
                                UserName = reader["USER_NAME"] != DBNull.Value ? reader["USER_NAME"].ToString() : string.Empty,
                            };
                            history.Add(model);
                        }
                    }
                }
            }
            return history;
        }
        public PhysicalStockLatestVocherNOResponse GetLatestVoucherNumber()
        {
            PhysicalStockLatestVocherNOResponse response = new PhysicalStockLatestVocherNOResponse { Data = new List<PhysicalStockLatestVocherNO>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PHYSICAL_STOCK", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PhysicalStockLatestVocherNO vocher = new PhysicalStockLatestVocherNO
                                {
                                    VOCHERNO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                };
                                response.Data.Add(vocher);
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




