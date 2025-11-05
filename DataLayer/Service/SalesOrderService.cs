using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SalesOrderService : ISalesOrderService
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

        public int SaveData(SalesOrder salesOrder)
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
                        tvp.Columns.Add("BRAND_ID", typeof(int));
                        tvp.Columns.Add("ARTICLE_TYPE", typeof(int));
                        tvp.Columns.Add("CATEGORY_ID", typeof(string));
                        tvp.Columns.Add("ART_NO", typeof(string));
                        tvp.Columns.Add("COLOR_ID", typeof(string));
                        tvp.Columns.Add("CONTENT", typeof(string));
                        tvp.Columns.Add("PACKING_ID", typeof(string));
                        tvp.Columns.Add("QUANTITY", typeof(float));
                        



                        foreach (var detail in salesOrder.Details)
                        {
                            tvp.Rows.Add(
                                detail.BRAND_ID ?? 0,
                                detail.ARTICLE_TYPE ?? 0,
                                detail.CATEGORY_ID ?? string.Empty,
                                detail.ART_NO ?? string.Empty,
                                detail.COLOR_ID ?? string.Empty,
                                detail.CONTENT ?? string.Empty,
                                detail.PACKING_ID ?? string.Empty,
                                detail.QUANTITY ?? 0
                                
                            );
                        }
                       

                        using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 1);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salesOrder.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", salesOrder.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", salesOrder.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SO_DATE", ParseDate(salesOrder.SO_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", salesOrder.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", salesOrder.USER_ID ?? 0);
                            cmd.Parameters.AddWithValue("@REMARKS", salesOrder.REMARKS ?? "");
                            cmd.Parameters.AddWithValue("@DELIVERY_ADDRESS", salesOrder.DELIVERY_ADDRESS ?? 0);
                            cmd.Parameters.AddWithValue("@WAREHOUSE", salesOrder.WAREHOUSE ?? 0);
                            cmd.Parameters.AddWithValue("@TOTAL_QTY", salesOrder.TOTAL_QTY ?? 0);
                            cmd.Parameters.AddWithValue("@SUBDEALER_ID", salesOrder.SUBDEALER_ID ?? 0);

                            // Add QTN_ID_LIST as a structured parameter
                            //SqlParameter qtnParam = cmd.Parameters.AddWithValue("@QTN_ID_LIST", tvpQTN);
                            //qtnParam.SqlDbType = SqlDbType.Structured;
                            //qtnParam.TypeName = "dbo.UDT_QTN_ID_LIST";

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return salesOrder.ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }

        public SalesOrderResponse EditData(SalesOrderUpdate salesOrder)
        {
            SalesOrderResponse response = new SalesOrderResponse();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("BRAND_ID", typeof(int));
                        tvp.Columns.Add("ARTICLE_TYPE", typeof(int));
                        tvp.Columns.Add("CATEGORY_ID", typeof(string));
                        tvp.Columns.Add("ART_NO", typeof(string));
                        tvp.Columns.Add("COLOR_ID", typeof(string));
                        tvp.Columns.Add("CONTENT", typeof(string));
                        tvp.Columns.Add("PACKING_ID", typeof(string));
                        tvp.Columns.Add("QUANTITY", typeof(float));

                        if (salesOrder.Details != null && salesOrder.Details.Any())
                        {
                            foreach (var detail in salesOrder.Details)
                            {
                                tvp.Rows.Add(
                                 detail.BRAND_ID ?? 0,
                                detail.ARTICLE_TYPE ?? 0,
                                detail.CATEGORY_ID ?? string.Empty,
                                detail.ART_NO ?? string.Empty,
                                detail.COLOR_ID ?? string.Empty,
                                detail.CONTENT ?? string.Empty,
                                detail.PACKING_ID ?? string.Empty,
                                detail.QUANTITY ?? 0
                                );
                            }
                        }
                        // Create DataTable for QTN_ID_LIST
                        //DataTable tvpQTN = new DataTable();
                        //tvpQTN.Columns.Add("QTN_ID", typeof(int));
                        //foreach (var qtnId in salesOrder.QTN_ID_LIST)
                        //{
                        //    tvpQTN.Rows.Add(qtnId);
                        //}

                        using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2);
                            cmd.Parameters.AddWithValue("@ID", salesOrder.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salesOrder.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", salesOrder.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", salesOrder.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SO_DATE", ParseDate(salesOrder.SO_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", salesOrder.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", salesOrder.USER_ID ?? 0);
                            cmd.Parameters.AddWithValue("@REMARKS", salesOrder.REMARKS ?? "");
                            cmd.Parameters.AddWithValue("@DELIVERY_ADDRESS", salesOrder.DELIVERY_ADDRESS ?? 0);
                            cmd.Parameters.AddWithValue("@WAREHOUSE", salesOrder.WAREHOUSE ?? 0);
                            cmd.Parameters.AddWithValue("@TOTAL_QTY", salesOrder.TOTAL_QTY ?? 0);
                            cmd.Parameters.AddWithValue("@SUBDEALER_ID", salesOrder.SUBDEALER_ID ?? 0);

                            // Add QTN_ID_LIST as a structured parameter
                            //SqlParameter qtnParam = cmd.Parameters.AddWithValue("@QTN_ID_LIST", tvpQTN);
                            //qtnParam.SqlDbType = SqlDbType.Structured;
                            //qtnParam.TypeName = "dbo.UDT_QTN_ID_LIST";

                            if (tvp.Rows.Count > 0)
                            {
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", tvp);
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";
                            }
                            else
                            {
                                // Pass an empty table if no details are provided
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", new DataTable());
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";
                            }

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

        public SalesOrderDetailSelectResponse GetSalesOrder(int id)
        {
            SalesOrderDetailSelectResponse response = new SalesOrderDetailSelectResponse
            {
                Data = new SalesOrderSelect
                {
                    Details = new List<SalesOrderDetailSelect>()
                }
            };

            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id); 

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool isHeaderSet = false;

                        while (reader.Read())
                        {
                            if (!isHeaderSet)
                            {
                                response.Data.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                                response.Data.STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0;
                                response.Data.SO_NO = reader["SO_NO"]?.ToString();
                                response.Data.SO_DATE = reader["SO_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SO_DATE"]).ToString("yyyy-MM-dd") : null;
                                response.Data.CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : 0;
                                response.Data.CUST_NAME = reader["CUST_NAME"]?.ToString();
                                response.Data.TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;
                                response.Data.DELIVERY_ADDRESS = reader["DELIVERY_ADDRESS"] != DBNull.Value ? Convert.ToInt32(reader["DELIVERY_ADDRESS"]) : 0;
                                response.Data.WAREHOUSE = reader["WAREHOUSE"] != DBNull.Value ? Convert.ToInt32(reader["WAREHOUSE"]) : 0;
                                response.Data.REMARKS = reader["REMARKS"]?.ToString();
                                response.Data.TOTAL_QTY = reader["TOTAL_QTY"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_QTY"]) : 0;
                                response.Data.TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : 0;
                               // response.Data.DELIVERY_ADDRESS = reader["ADDRESS1"]?.ToString();
                                response.Data.ADDRESS = reader["DELIVERY_ADDRESS_FULL"]?.ToString();
                                response.Data.SUBDEALER_ID = reader["SUBDEALER_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUBDEALER_ID"]) : 0;
                                isHeaderSet = true;
                            }

                            SalesOrderDetailSelect detail = new SalesOrderDetailSelect
                            {
                               
                                CONTENT = reader["CONTENT"] != DBNull.Value ? Convert.ToString(reader["CONTENT"]) : null,
                                PACKING_ID = reader["DESCRIPTION"] != DBNull.Value ? Convert.ToString(reader["DESCRIPTION"]) : null,
                                BRAND_ID = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : 0,
                                ARTICLE_TYPE = reader["ARTICLE_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_TYPE"]) : 0,
                                CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToString(reader["CATEGORY_ID"]) : null,
                                ART_NO = reader["ART_NO"] != DBNull.Value ? Convert.ToString(reader["ART_NO"]) : null,
                                COLOR_ID = reader["COLOR"] != DBNull.Value ? Convert.ToString(reader["COLOR"]) : null,
                                QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0
                            };

                            response.Data.Details.Add(detail);
                        }
                    }
                }
            }

            response.Flag = 1;
            response.Message = "Success";
            return response;
        }


        public SalesOrderListResponse GetAllSalesOrders()
        {
            SalesOrderListResponse response = new SalesOrderListResponse
            {
                Data = new List<SalesOrderList>()
            };

            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalesOrderList salesOrder = new SalesOrderList
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                SO_NO = reader["SO_NO"] != DBNull.Value ? reader["SO_NO"].ToString() : null,
                                SO_DATE = reader["SO_DATE"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["SO_DATE"]).ToString("yyyy-MM-dd") : null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : (int?)null,
                                CUSTOMER_NAME = reader["CUST_NAME"] != DBNull.Value ? reader["CUST_NAME"].ToString() : null,
                                DELIVERY_ADDRESS_ID = reader["DELIVERY_ADDRESS"] != DBNull.Value ? Convert.ToInt32(reader["DELIVERY_ADDRESS"]) : (int?)null, 
                                WAREHOUSE = reader["WAREHOUSE"] != DBNull.Value ? Convert.ToInt32(reader["WAREHOUSE"]) : (int?)null,
                                REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null,
                                TOTAL_QTY = reader["TOTAL_QTY"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_QTY"]) : (float?)null,
                                TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : (int?)null,
                                TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                DELIVERY_ADDRESS = reader["ADDRESS"] != DBNull.Value ? reader["ADDRESS"].ToString() : null,
                                ADDRESS = reader["DELIVERY_ADDRESS_FULL"] != DBNull.Value ? reader["DELIVERY_ADDRESS_FULL"].ToString() : null,
                                SUBDEALER_ID = reader["SUBDEALER_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUBDEALER_ID"]) : (int?)null,
                            };

                            response.Data.Add(salesOrder);
                        }
                    }
                }
            }

            response.Flag = 1;
            response.Message = "Success";
            return response;
        }


        public ItemListsResponse GetSalesOrderItems()
        {
            ItemListsResponse response = new ItemListsResponse { Data = new List<ITEMS>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ITEMS item = new ITEMS
                                {
                                    ARTICLE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,

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
        public ItemListsResponse GetarticleType()
        {
            ItemListsResponse response = new ItemListsResponse { Data = new List<ITEMS>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT DISTINCT TB_ARTICLE_TYPE.ID,TB_ARTICLE_TYPE.DESCRIPTION
                                    FROM TB_ARTICLE_TYPE INNER JOIN TB_PACKING ON TB_ARTICLE_TYPE.ID=TB_PACKING.ARTICLE_TYPE
                                    WHERE TB_ARTICLE_TYPE.IS_DELETED=0";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ITEMS item = new ITEMS
                                {
                                    ARTICLE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
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
        public ItemListsResponse Getcategory(SalesOrderRequest request)
        {
            ItemListsResponse response = new ItemListsResponse { Data = new List<ITEMS>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT DISTINCT TB_ARTICLE_CATEGORY.ID,TB_ARTICLE_CATEGORY.DESCRIPTION FROM TB_ARTICLE_CATEGORY 
                                    INNER JOIN TB_PACKING ON TB_ARTICLE_CATEGORY.ID=TB_PACKING.CATEGORY_ID 
                                    WHERE TB_PACKING.BRAND_ID=@BRAND_ID AND TB_PACKING.ARTICLE_TYPE=@ARTICLE_TYPE AND TB_ARTICLE_CATEGORY.IS_DELETED=0";
         

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;

                        // Add parameters from request payload
                        cmd.Parameters.AddWithValue("@BRAND_ID", request.BRAND_ID);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", request.ARTICLE_TYPE);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ITEMS item = new ITEMS
                                {
                                    ARTICLE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
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
        public ItemListsResponse GetArtNo(SalesOrderRequest request)
        {
            ItemListsResponse response = new ItemListsResponse { Data = new List<ITEMS>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT DISTINCT  MIN(TB_PACKING.ID) AS ID,TB_PACKING.ART_NO FROM TB_ARTICLE_CATEGORY 
                                    INNER JOIN TB_PACKING ON TB_ARTICLE_CATEGORY.ID=TB_PACKING.CATEGORY_ID 
                                    WHERE TB_PACKING.BRAND_ID=@BRAND_ID AND TB_PACKING.ARTICLE_TYPE=@ARTICLE_TYPE AND 
                                    TB_ARTICLE_CATEGORY.DESCRIPTION=@CATEGORY_ID AND TB_ARTICLE_CATEGORY.IS_DELETED=0 
                                    GROUP BY TB_PACKING.ART_NO";
                    

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;

                        // Parameters from payload
                        cmd.Parameters.AddWithValue("@BRAND_ID", request.BRAND_ID);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", request.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", request.CATEGORY_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ITEMS item = new ITEMS
                                {
                                    ARTICLE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    DESCRIPTION = reader["ART_NO"] != DBNull.Value ? reader["ART_NO"].ToString() : null
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
        public ItemListsResponse GetColor(SalesOrderRequest request)
        {
            ItemListsResponse response = new ItemListsResponse { Data = new List<ITEMS>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                            SELECT DISTINCT MIN(TB_PACKING.ID) AS ID, TB_PACKING.COLOR FROM TB_PACKING
                            INNER JOIN TB_ARTICLE_CATEGORY ON TB_ARTICLE_CATEGORY.ID=TB_PACKING.CATEGORY_ID
                            WHERE TB_PACKING.BRAND_ID = @BRAND_ID
                            AND TB_PACKING.ARTICLE_TYPE = @ARTICLE_TYPE
                            AND TB_ARTICLE_CATEGORY.DESCRIPTION= @CATEGORY_ID                            
                            AND TB_PACKING.ART_NO = @ART_NO
                            GROUP BY TB_PACKING.COLOR";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@BRAND_ID", request.BRAND_ID);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", request.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", request.CATEGORY_ID);
                        cmd.Parameters.AddWithValue("@ART_NO", request.ARTNO_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ITEMS item = new ITEMS
                                {
                                    ARTICLE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    DESCRIPTION = reader["COLOR"] != DBNull.Value ? reader["COLOR"].ToString() : null
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
        public ItemListsResponse GetPacking(SalesOrderRequest request)
        {
            ItemListsResponse response = new ItemListsResponse { Data = new List<ITEMS>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
               	 SELECT DISTINCT TB_PACKING.ID, TB_PACKING.DESCRIPTION FROM TB_PACKING
                 INNER JOIN TB_ARTICLE_CATEGORY ON TB_ARTICLE_CATEGORY.ID=TB_PACKING.CATEGORY_ID
                 WHERE TB_PACKING.BRAND_ID = @BRAND_ID
                 AND TB_PACKING.ARTICLE_TYPE = @ARTICLE_TYPE
                 AND TB_ARTICLE_CATEGORY.DESCRIPTION = @CATEGORY_ID
                 AND TB_PACKING.ART_NO = @ART_NO
                 AND TB_PACKING.COLOR = @COLOR";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@BRAND_ID", request.BRAND_ID);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", request.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", request.CATEGORY_ID);
                        cmd.Parameters.AddWithValue("@ART_NO", request.ARTNO_ID);
                        cmd.Parameters.AddWithValue("@COLOR", request.COLOR);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ITEMS item = new ITEMS
                                {
                                    ARTICLE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null
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
        public PairResponse GetPairQtyByPackingId(PackingPairRequest request)
        {
            PairResponse response = new PairResponse { Data = new List<PairDtl>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT ID, PAIR_QTY, COMBINATION 
                             FROM TB_PACKING 
                             WHERE ID = @PACKING_ID AND IS_INACTIVE = 0";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PACKING_ID", request.PACKING_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PairDtl pair = new PairDtl
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    PAIR_QTY = reader["PAIR_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["PAIR_QTY"]) : (decimal?)null,
                                    // ✅ Clean escaped double quotes from DB value
                                    COMBINATION = reader["COMBINATION"] != DBNull.Value
                                        ? Convert.ToString(reader["COMBINATION"]).Replace("\\\"", "\"")
                                        : null
                                };

                                response.Data.Add(pair);
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

        public WarehouseResponse GetWarehouseByCustId(SOQUOTATIONRequest request)
        {
            WarehouseResponse response = new WarehouseResponse { Data = new List<WarehouseDtl>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT DISTINCT TB_COMPANY_MASTER.ID,TB_COMPANY_MASTER.COMPANY_NAME FROM TB_COMPANY_MASTER
                                    INNER JOIN TB_CUSTOMER ON TB_COMPANY_MASTER.ID= TB_CUSTOMER.WAREHOUSE_ID
                                    WHERE TB_CUSTOMER.CUST_TYPE=1 AND TB_CUSTOMER.IS_DELETED=0 AND TB_CUSTOMER.ID=@CUST_ID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WarehouseDtl warehouse = new WarehouseDtl
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    WAREHOUSE_NAME = reader["COMPANY_NAME"] != DBNull.Value ? reader["COMPANY_NAME"].ToString() : null,
                                };
                                response.Data.Add(warehouse);
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

        public bool DeleteSalesOrder(int soId)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@SO_ID", soId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public SalesOrderResponse ApproveSalesOrder(SalesOrderUpdate request)
        {
            SalesOrderResponse response = new SalesOrderResponse();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("BRAND_ID", typeof(int));
                        tvp.Columns.Add("ARTICLE_TYPE", typeof(int));
                        tvp.Columns.Add("CATEGORY_ID", typeof(string));
                        tvp.Columns.Add("ART_NO", typeof(string));
                        tvp.Columns.Add("COLOR_ID", typeof(string));
                        tvp.Columns.Add("CONTENT", typeof(string));
                        tvp.Columns.Add("PACKING_ID", typeof(string));
                        tvp.Columns.Add("QUANTITY", typeof(float));

                        if (request.Details != null && request.Details.Any())
                        {
                            foreach (var detail in request.Details)
                            {
                                tvp.Rows.Add(
                                 detail.BRAND_ID ?? 0,
                                 detail.ARTICLE_TYPE ?? 0,
                                 detail.CATEGORY_ID ?? string.Empty,
                                 detail.ART_NO ?? string.Empty,
                                 detail.COLOR_ID ?? string.Empty,
                                 detail.CONTENT ?? string.Empty,
                                 detail.PACKING_ID ?? string.Empty,
                                 detail.QUANTITY ?? 0
                                );
                            }
                        }

                        //// Create DataTable for QTN_ID_LIST
                        //DataTable tvpQTN = new DataTable();
                        //tvpQTN.Columns.Add("QTN_ID", typeof(int));
                        //foreach (var qtnId in request.QTN_ID_LIST)
                        //{
                        //    tvpQTN.Rows.Add(qtnId);
                        //}

                        using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 7);
                            cmd.Parameters.AddWithValue("@ID", request.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SO_DATE", ParseDate(request.SO_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", request.USER_ID ?? 0);
                            cmd.Parameters.AddWithValue("@REMARKS", request.REMARKS ?? "");
                            cmd.Parameters.AddWithValue("@DELIVERY_ADDRESS", request.DELIVERY_ADDRESS ?? 0);
                            cmd.Parameters.AddWithValue("@WAREHOUSE", request.WAREHOUSE ?? 0);
                            cmd.Parameters.AddWithValue("@TOTAL_QTY", request.TOTAL_QTY ?? 0);
                            cmd.Parameters.AddWithValue("@SUBDEALER_ID", request.SUBDEALER_ID ?? 0);

                            // Add QTN_ID_LIST as a structured parameter
                            //SqlParameter qtnParam = cmd.Parameters.AddWithValue("@QTN_ID_LIST", tvpQTN);
                            //qtnParam.SqlDbType = SqlDbType.Structured;
                            //qtnParam.TypeName = "dbo.UDT_QTN_ID_LIST";


                            if (tvp.Rows.Count > 0)
                            {
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", tvp);
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";
                            }
                            else
                            {
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", new DataTable());
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";
                            }

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        response.Flag = "1";
                        response.Message = "Sales Order approved and updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Flag = "0";
                        response.Message = "Error approving Sales Order: " + ex.Message;
                    }
                }
            }
            return response;
        }
        public SOQUOTATIONLISTResponse GetSOQUOTATIONLIST(SOQUOTATIONRequest request)
        {
            SOQUOTATIONLISTResponse response = new SOQUOTATIONLISTResponse { Data = new List<SOQUOTATIONLIST>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 8);
                        cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SOQUOTATIONLIST item = new SOQUOTATIONLIST
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    QTN_ID = reader["QTN_ID"] != DBNull.Value ? Convert.ToInt32(reader["QTN_ID"]) : (int?)null,
                                    CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : (int?)null,
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                    ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    ITEM_NAME = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    MATRIX_CODE = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : null,
                                    UOM = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : null,
                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    DISC_PERCENT = reader["DISC_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DISC_PERCENT"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    TAX_PERCENT = reader["TAX_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_PERCENT"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_AMOUNT"]) : 0,
                                    REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null
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
        public LatestVocherNOResponse GetLatestVoucherNumber()
        {
            LatestVocherNOResponse response = new LatestVocherNOResponse { Data = new List<LatestVocherNO>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 9);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LatestVocherNO vocher = new LatestVocherNO
                                {
                                    VOCHERNO = reader["NEXT_SO_NO"] != DBNull.Value ? reader["NEXT_SO_NO"].ToString() : null,
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
        public List<Subdealers> GetSubdealer(SubdealerRequest id)
        {
            var subdealers = new List<Subdealers>();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    string query = @"
                SELECT ID, CUST_NAME 
                FROM TB_CUSTOMER 
                WHERE CUST_TYPE = 2 
                  AND DEALER_ID = @DEALER_ID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@DEALER_ID", id.DEALER_ID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new Subdealers
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    SUBDEALER = reader["CUST_NAME"] != DBNull.Value ? reader["CUST_NAME"].ToString() : string.Empty
                                };
                                subdealers.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching sub dealer data: " + ex.Message, ex);
            }
            return subdealers;
        }

    }
}
