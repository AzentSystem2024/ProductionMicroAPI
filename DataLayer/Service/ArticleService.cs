using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Principal;

namespace MicroApi.Service
{
    public class ArticleService : IArticleService
    {
        public ArticleResponse Insert(Article article)
        {
            var res = new ArticleResponse();

            try
            {
                // Business validation
                if (article.IS_COMPONENT && article.COMPONENT_ARTICLE_ID.HasValue && article.COMPONENT_ARTICLE_ID.Value != 0)
                {
                    res.flag = 0;
                    res.Message = "IS_COMPONENT cannot be true while COMPONENT_ARTICLE_ID is set.";
                    return res;
                }

                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // ==========================
                        // Basic Parameters
                        // ==========================
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@ART_NO", article.ART_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", article.DESCRIPTION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@COLOR", article.COLOR ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PRICE", article.PRICE);
                        cmd.Parameters.AddWithValue("@PACK_QTY", article.PACK_QTY);
                        cmd.Parameters.AddWithValue("@PART_NO", article.PART_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", article.ALIAS_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NEXT_SERIAL", article.NEXT_SERIAL);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", article.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", article.CATEGORY_ID);
                        cmd.Parameters.AddWithValue("@BRAND_ID", article.BRAND_ID);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", article.NEW_ARRIVAL_DAYS);
                        cmd.Parameters.AddWithValue("@IS_STOPPED", article.IS_STOPPED);
                        cmd.Parameters.AddWithValue("@SUPPLIER_ID", article.SUPPLIER_ID);
                        cmd.Parameters.AddWithValue("@IS_COMPONENT", article.IS_COMPONENT);
                        cmd.Parameters.AddWithValue("@COMPONENT_ARTICLE_ID", (object)article.COMPONENT_ARTICLE_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IMAGE_NAME", article.IMAGE_NAME ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATED_DATE", article.CREATED_DATE);
                        cmd.Parameters.AddWithValue("@STANDARD_PACKING", article.STANDARD_PACKING ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", article.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@HSN_CODE", article.HSN_CODE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GST_PERC", article.GST_PERC ?? 0);

                        // ==========================
                        // SIZES (UDT)
                        // ==========================
                        DataTable sizeTable = new DataTable();
                        sizeTable.Columns.Add("SIZE", typeof(string));

                        if (article.Sizes != null)
                        {
                            foreach (var s in article.Sizes)
                                sizeTable.Rows.Add(s.SizeValue);
                        }

                        var sizeParam = cmd.Parameters.AddWithValue("@SIZES", sizeTable);
                        sizeParam.SqlDbType = SqlDbType.Structured;
                        sizeParam.TypeName = "dbo.UDT_TB_ARTICLE_SIZE";

                        // ==========================
                        // BOM (UDT)
                        // ==========================
                        DataTable bomTable = new DataTable();
                        bomTable.Columns.Add("ITEM_ID", typeof(int));
                        bomTable.Columns.Add("QUANTITY", typeof(decimal));

                        if (article.BOM != null)
                        {
                            foreach (var b in article.BOM)
                                bomTable.Rows.Add(b.ITEM_CODE, b.QUANTITY);
                        }

                        var bomParam = cmd.Parameters.AddWithValue("@UDT_TB_ARTICLE_BOM", bomTable);
                        bomParam.SqlDbType = SqlDbType.Structured;
                        bomParam.TypeName = "dbo.UDT_TB_ARTICLE_BOM";

                        // ==========================
                        // UNITS (UDT)
                        // ==========================
                        DataTable unitTable = new DataTable();
                        unitTable.Columns.Add("UNIT_ID", typeof(int));

                        if (article.Units != null)
                        {
                            foreach (var u in article.Units)
                                unitTable.Rows.Add(u.UNIT_ID);
                        }

                        var unitParam = cmd.Parameters.AddWithValue("@UDT_TB_ARTICLE_UNITS", unitTable);
                        unitParam.SqlDbType = SqlDbType.Structured;
                        unitParam.TypeName = "dbo.UDT_TB_ARTICLE_UNITS";

                        // ==========================
                        // Execute & Read SP Result
                        // ==========================
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["Flag"]);
                                res.Message = reader["Message"].ToString();

                                if (res.flag == 1 && reader.FieldCount > 2)
                                {
                                    res.Data = new ArticleUpdate
                                    {
                                        ID = Convert.ToInt64(reader["ARTICLE_ID"])
                                    };
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


        public ArticleResponse Update(ArticleUpdate article)
        {
            var res = new ArticleResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 🔹 Main parameters
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", article.ID ?? 0);
                        cmd.Parameters.AddWithValue("@ART_NO", article.ART_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", article.DESCRIPTION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@COLOR", article.COLOR ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PRICE", article.PRICE ?? 0);
                        cmd.Parameters.AddWithValue("@PACK_QTY", article.PACK_QTY);
                        cmd.Parameters.AddWithValue("@PART_NO", article.PART_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", article.ALIAS_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@NEXT_SERIAL", article.NEXT_SERIAL);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", article.ARTICLE_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", article.CATEGORY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@BRAND_ID", article.BRAND_ID ?? 0);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", article.NEW_ARRIVAL_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@IS_STOPPED", article.IS_STOPPED);
                        cmd.Parameters.AddWithValue("@SUPPLIER_ID", article.SUPPLIER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_COMPONENT", article.IS_COMPONENT);
                        cmd.Parameters.AddWithValue("@COMPONENT_ARTICLE_ID", (object)article.COMPONENT_ARTICLE_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IMAGE_NAME", article.IMAGE_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATED_DATE", article.CREATED_DATE);
                        cmd.Parameters.AddWithValue("@STANDARD_PACKING", article.STANDARD_PACKING ?? string.Empty);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", article.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@HSN_CODE", article.HSN_CODE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GST_PERC", article.GST_PERC ?? 0);

                        // 🔹 Sizes table
                        var sizeTable = new DataTable();
                        sizeTable.Columns.Add("SIZE", typeof(string));

                        if (article.SIZES != null)
                            foreach (var s in article.SIZES)
                                sizeTable.Rows.Add(s.SizeValue);

                        var sizeParam = cmd.Parameters.AddWithValue("@SIZES", sizeTable);
                        sizeParam.SqlDbType = SqlDbType.Structured;
                        sizeParam.TypeName = "dbo.UDT_TB_ARTICLE_SIZE";

                        // 🔹 BOM table
                        var bomTable = new DataTable();
                        bomTable.Columns.Add("ITEM_ID", typeof(int));
                        bomTable.Columns.Add("QUANTITY", typeof(decimal));

                        if (article.BOM != null)
                            foreach (var b in article.BOM)
                                bomTable.Rows.Add(b.ITEM_CODE, b.QUANTITY);

                        var bomParam = cmd.Parameters.AddWithValue("@UDT_TB_ARTICLE_BOM", bomTable);
                        bomParam.SqlDbType = SqlDbType.Structured;
                        bomParam.TypeName = "dbo.UDT_TB_ARTICLE_BOM";

                        // 🔹 Units table
                        var unitTable = new DataTable();
                        unitTable.Columns.Add("UNIT_ID", typeof(int));

                        if (article.Units != null)
                            foreach (var u in article.Units)
                                unitTable.Rows.Add(u.UNIT_ID);

                        var unitParam = cmd.Parameters.AddWithValue("@UDT_TB_ARTICLE_UNITS", unitTable);
                        unitParam.SqlDbType = SqlDbType.Structured;
                        unitParam.TypeName = "dbo.UDT_TB_ARTICLE_UNITS";

                        // 🔹 Execute & READ RESPONSE
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["Flag"]);
                                res.Message = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public ArticleResponse GetArticleById(int id)
        {
            var res = new ArticleResponse();
            ArticleUpdate articleDetails = null;

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            // ───────────────────────────────────────────────────────────────
                            // 1️⃣ MAIN ARTICLE DATA
                            // ───────────────────────────────────────────────────────────────
                            if (reader.Read())
                            {
                                articleDetails = new ArticleUpdate
                                {
                                    ID = Convert.ToInt64(reader["ARTICLE_ID"]),
                                    ART_NO = reader["ART_NO"]?.ToString(),
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    COLOR = reader["COLOR"]?.ToString(),
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    PACK_QTY = reader["PACK_QTY"] != DBNull.Value ? Convert.ToInt32(reader["PACK_QTY"]) : 0,
                                    PART_NO = reader["PART_NO"]?.ToString(),
                                    ALIAS_NO = reader["ALIAS_NO"]?.ToString(),
                                    ARTICLE_TYPE = reader["ARTICLE_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_TYPE"]) : 0,
                                    CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0,
                                    BRAND_ID = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : 0,
                                    NEW_ARRIVAL_DAYS = reader["NEW_ARRIVAL_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["NEW_ARRIVAL_DAYS"]) : 0,
                                    IMAGE_NAME = reader["IMAGE_NAME"]?.ToString(),
                                    SUPPLIER_ID = reader["SUPPLIER_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPPLIER_ID"]) : 0,
                                    SupplierName = reader["SUPPLIER_NAME"]?.ToString(),
                                    IS_COMPONENT = reader["IS_COMPONENT"] != DBNull.Value ? Convert.ToBoolean(reader["IS_COMPONENT"]) : false,
                                    COMPONENT_ARTICLE_ID = reader["COMPONENT_ARTICLE_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPONENT_ARTICLE_ID"]) : (int?)null,
                                    ComponentArticleNo = reader["COMPONENT_ARTICLE_NO"]?.ToString(),
                                    ComponentArticleName = reader["COMPONENT_ARTICLE_NAME"]?.ToString(),
                                    CREATED_DATE = reader["CREATED_DATE"] != DBNull.Value ?
                                        ((DateTimeOffset)reader["CREATED_DATE"]).DateTime : (DateTime?)null,
                                    COMPANY_ID = Convert.ToInt32(reader["COMPANY_ID"]),
                                    STANDARD_PACKING = reader["STD_PACKING"]?.ToString(),
                                    HSN_CODE = reader["HSN_CODE"]?.ToString(),
                                    GST_PERC = reader["GST_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["GST_PERC"]) : 0,
                                    NEXT_SERIAL = reader["NEXT_SERIAL"] != DBNull.Value ? Convert.ToInt32(reader["NEXT_SERIAL"]) : 0,
                                    LAST_ORDER_NO = reader["ORDER_NO"]?.ToString(),
                                    SIZES = new List<Sizes>(),
                                    Units = new List<ArticleUnits>()
                                };

                                // Parse sizes JSON
                                if (reader["SIZES"] != DBNull.Value)
                                {
                                    articleDetails.SIZES = JsonConvert.DeserializeObject<List<Sizes>>(reader["SIZES"].ToString());
                                }
                            }

                            // ───────────────────────────────────────────────────────────────
                            // 2️⃣ UNITS RESULT SET
                            // ───────────────────────────────────────────────────────────────
                            if (reader.NextResult())
                            {
                                var unitList = new List<ArticleUnits>();

                                while (reader.Read())
                                {
                                    unitList.Add(new ArticleUnits
                                    {
                                        UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                       
                                    });
                                }

                                articleDetails.Units = unitList;
                            }

                            // ───────────────────────────────────────────────────────────────
                            // 3️⃣ BOM RESULT SET
                            // ───────────────────────────────────────────────────────────────
                            if (reader.NextResult())
                            {
                                var bomList = new List<ArticleBOM>();

                                while (reader.Read())
                                {
                                    bomList.Add(new ArticleBOM
                                    {
                                        BOM_ID = reader["BOM_ID"] != DBNull.Value ? Convert.ToInt32(reader["BOM_ID"]) : 0,
                                        ARTICLE_ID = reader["ARTICLE_ID"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_ID"]) : 0,
                                        ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                        QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0,
                                        ITEM_CODE = reader["ITEM_NAME"]?.ToString(),
                                        DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                        UOM = reader["UOM"]?.ToString()
                                    });
                                }

                                articleDetails.BOM = bomList;
                            }

                            // ───────────────────────────────────────────────────────────────
                            // 4️⃣ STATUS RESULT
                            // ───────────────────────────────────────────────────────────────
                            if (reader.NextResult() && reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["flag"]);
                                res.Message = reader["Message"].ToString();
                            }

                            res.Data = articleDetails;
                        }
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




        public ArticleListResponse GetArticleList(ArticleListReq request)
        {
            var res = new ArticleListResponse();
            res.Data = new List<ArticleUpdate>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var article = new ArticleUpdate
                                {
                                    ID = reader["ARTICLE_ID"] != DBNull.Value ? Convert.ToInt64(reader["ARTICLE_ID"]) : 0,
                                    ART_NO = reader["ART_NO"]?.ToString(),
                                    ALIAS_NO = reader["ALIAS_NO"]?.ToString(),
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    COLOR = reader["COLOR"]?.ToString(),
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    CATEGORY_NAME = reader["CATEGORY_NAME"]?.ToString(),
                                    BRAND_NAME = reader["BRAND_NAME"]?.ToString(),
                                    ARTICLE_TYPE_NAME = reader["ARTICLE_TYPE_NAME"]?.ToString(),
                                    IS_COMPONENT = reader["IS_COMPONENT"] != DBNull.Value && Convert.ToBoolean(reader["IS_COMPONENT"]),
                                    ComponentArticleNo = reader["COMPONENT_ARTICLE_NO"]?.ToString(),
                                    ComponentArticleName = reader["COMPONENT_ARTICLE_NAME"]?.ToString(),
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                    STANDARD_PACKING = reader["STD_PACKING"]?.ToString() ?? "",
                                    HSN_CODE = reader["HSN_CODE"]?.ToString(),
                                    GST_PERC = reader["GST_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["GST_PERC"]) : 0,
                                    SIZES = new List<Sizes>()
                                };

                                // 🟦 Parse SIZES JSON column
                                if (reader["SIZES"] != DBNull.Value)
                                {
                                    var json = reader["SIZES"].ToString();
                                    article.SIZES = JsonConvert.DeserializeObject<List<Sizes>>(json);
                                }

                                res.Data.Add(article);
                            }
                        }
                    }
                }

                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }



        public ListItemsResponse GetItems(ArticleListReq request)
        {
            ListItemsResponse res = new ListItemsResponse();
            res.DataList = new List<ItemData>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                SELECT 
                    ID, 
                    ITEM_CODE + '-' + DESCRIPTION AS ITEM_NAME 
                FROM TB_ITEMS 
                WHERE IS_DELETED = 0 
                  AND COMPANY_ID = @COMPANY_ID
                ORDER BY ID ASC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // ✅ PASS COMPANY_ID FROM REQUEST BODY
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.DataList.Add(new ItemData
                                {
                                    ID = reader["ID"] != DBNull.Value
                                            ? Convert.ToInt64(reader["ID"])
                                            : 0,

                                    DESCRIPTION = reader["ITEM_NAME"]?.ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }

                res.flag = res.DataList.Count > 0 ? 1 : 0;
                res.Message = res.DataList.Count > 0 ? "Success" : "No record found";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.DataList = new List<ItemData>();
            }

            return res;
        }



        public ItemdataResponse GetItemByCode(ItemcodeRequest request)
        {
            ItemdataResponse res = new ItemdataResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT ID, DESCRIPTION, UOM 
                             FROM TB_ITEMS 
                             WHERE IS_DELETED = 0 AND ITEM_CODE = @ITEM_CODE";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ITEM_CODE", request.ITEM_CODE);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new ItemData
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt64(reader["ID"]) : 0,
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString() ?? string.Empty,
                                    UOM = reader["UOM"]?.ToString() ?? string.Empty
                                };

                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "No record found";
                            }
                        }
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

        public ArticleResponse DeleteArticleData(DeleteArticleRequest request)
        {
            var res = new ArticleResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ART_NO", request.ART_NO); // Pass the ART_NO parameter
                        cmd.ExecuteNonQuery();

                        res.flag = 1;
                        res.Message = "Deleted successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        public string GetLastOrderNoByUnitId(ArticleListReq request)
        {
            string lastOrderNo = "0";

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                SELECT ISNULL(MAX(ORDER_NO), 0) 
                FROM TB_ARTICLE
                WHERE COMPANY_ID = @COMPANY_ID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            lastOrderNo = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lastOrderNo;
        }


    }
}
