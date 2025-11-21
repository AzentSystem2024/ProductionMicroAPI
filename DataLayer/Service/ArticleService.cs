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
                // Validation: prevent conflicting component settings
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

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Basic parameters
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@ART_NO", article.ART_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", article.DESCRIPTION ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@COLOR", article.COLOR ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PRICE", article.PRICE);
                        cmd.Parameters.AddWithValue("@PACK_QTY", article.PACK_QTY);
                        cmd.Parameters.AddWithValue("@PART_NO", article.PART_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", article.ALIAS_NO ?? (object)DBNull.Value);
                        //cmd.Parameters.AddWithValue("@UNIT_ID", article.UNIT_ID);
                        cmd.Parameters.AddWithValue("@UNIT_ID", article.UNIT_ID ?? (object)DBNull.Value);
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

                        // ✅ Structured parameter for SIZES (UDT_TB_ARTICLE_SIZE)
                        var sizeTable = new DataTable();
                        sizeTable.Columns.Add("SIZE", typeof(string));
                        if (article.Sizes != null)
                        {
                            foreach (var size in article.Sizes)
                            {
                                sizeTable.Rows.Add(size.SizeValue);
                            }
                        }
                        var sizeParam = cmd.Parameters.AddWithValue("@SIZES", sizeTable);
                        sizeParam.SqlDbType = SqlDbType.Structured;
                        sizeParam.TypeName = "dbo.UDT_TB_ARTICLE_SIZE";

                        // ✅ Structured parameter for BOM (UDT_TB_ARTICLE_BOM)
                        var bomTable = new DataTable();
                        bomTable.Columns.Add("ITEM_ID", typeof(int));
                        bomTable.Columns.Add("QUANTITY", typeof(decimal));
                        if (article.BOM != null)
                        {
                            foreach (var item in article.BOM)
                            {
                                bomTable.Rows.Add(item.ITEM_CODE, item.QUANTITY);
                            }
                        }
                        var bomParam = cmd.Parameters.AddWithValue("@UDT_TB_ARTICLE_BOM", bomTable);
                        bomParam.SqlDbType = SqlDbType.Structured;
                        bomParam.TypeName = "dbo.UDT_TB_ARTICLE_BOM";

                        // Execute
                        cmd.ExecuteNonQuery();

                        res.flag = 1;
                        res.Message = "Success";
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
                        //cmd.Parameters.AddWithValue("@UNIT_ID", article.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@UNIT_ID", article.UNIT_ID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", article.ARTICLE_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", article.CATEGORY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@BRAND_ID", article.BRAND_ID ?? 0);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", article.NEW_ARRIVAL_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@IS_STOPPED", article.IS_STOPPED);
                        //cmd.Parameters.AddWithValue("@IS_INACTIVE", article.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@SUPPLIER_ID", article.SUPPLIER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_COMPONENT", article.IS_COMPONENT);
                        cmd.Parameters.AddWithValue("@COMPONENT_ARTICLE_ID", (object)article.COMPONENT_ARTICLE_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IMAGE_NAME", article.IMAGE_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATED_DATE", article.CREATED_DATE);
                        cmd.Parameters.AddWithValue("@STANDARD_PACKING", article.STANDARD_PACKING ?? string.Empty);

                        // 🔹 Structured parameter for SIZES
                        var sizeTable = new DataTable();
                        sizeTable.Columns.Add("SIZE", typeof(string));

                        if (article.SIZES != null)
                        {
                            foreach (var size in article.SIZES)
                                sizeTable.Rows.Add(size.SizeValue);
                        }

                        var sizeParam = cmd.Parameters.AddWithValue("@SIZES", sizeTable);
                        sizeParam.SqlDbType = SqlDbType.Structured;
                        sizeParam.TypeName = "dbo.UDT_TB_ARTICLE_SIZE";

                        // 🔹 Structured parameter for BOMItems
                        var bomTable = new DataTable();
                        bomTable.Columns.Add("ITEM_ID", typeof(int));
                        bomTable.Columns.Add("QUANTITY", typeof(decimal));

                        if (article.BOM != null)
                        {
                            foreach (var bom in article.BOM)
                                bomTable.Rows.Add(bom.ITEM_CODE, bom.QUANTITY);
                        }

                        var bomParam = cmd.Parameters.AddWithValue("@UDT_TB_ARTICLE_BOM", bomTable);
                        bomParam.SqlDbType = SqlDbType.Structured;
                        bomParam.TypeName = "dbo.UDT_TB_ARTICLE_BOM";

                        // 🔹 Execute
                        cmd.ExecuteNonQuery();

                        res.flag = 1;
                        res.Message = "Updated Successfully";
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

        public ArticleResponse GetArticleById(ArticleSelectRequest request)
        {
            ArticleResponse res = new ArticleResponse();
            ArticleUpdate articleDetails = null;

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
                        cmd.Parameters.AddWithValue("@UNIT_ID", request.UnitID);
                        cmd.Parameters.AddWithValue("@ART_NO", request.ArtNo);
                        cmd.Parameters.AddWithValue("@COLOR", request.Color);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", request.CategoryID);
                        cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = request.Price;

                        using (var reader = cmd.ExecuteReader())
                        {
                            // ✅ 1. Read Main Article Details
                            if (reader.Read())
                            {
                                articleDetails = new ArticleUpdate
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt64(reader["ID"]) : 0,
                                    ART_NO = reader["ART_NO"]?.ToString() ?? string.Empty,
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString() ?? string.Empty,
                                    COLOR = reader["COLOR"]?.ToString() ?? string.Empty,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    PACK_QTY = reader["PACK_QTY"] != DBNull.Value ? Convert.ToInt32(reader["PACK_QTY"]) : 0,
                                    PART_NO = reader["PART_NO"]?.ToString() ?? string.Empty,
                                    NEXT_SERIAL = reader["NEXT_SERIAL"] != DBNull.Value ? Convert.ToInt32(reader["NEXT_SERIAL"]) : (int?)null,
                                    ALIAS_NO = reader["ALIAS_NO"]?.ToString() ?? string.Empty,
                                    UNIT_ID = reader["UNIT_ID"]?.ToString() ?? string.Empty,
                                    ARTICLE_TYPE = reader["ARTICLE_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_TYPE"]) : 0,
                                    CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0,
                                    BRAND_ID = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : 0,
                                    NEW_ARRIVAL_DAYS = reader["NEW_ARRIVAL_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["NEW_ARRIVAL_DAYS"]) : 0,
                                    IMAGE_NAME = reader["IMAGE_NAME"]?.ToString() ?? string.Empty,
                                    SUPPLIER_ID = reader["SUPPLIER_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPPLIER_ID"]) : 0,
                                    SupplierName = reader["SUPP_NAME"]?.ToString() ?? string.Empty,
                                    IS_COMPONENT = reader["IS_COMPONENT"] != DBNull.Value && Convert.ToBoolean(reader["IS_COMPONENT"]),
                                    COMPONENT_ARTICLE_ID = reader["COMPONENT_ARTICLE_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPONENT_ARTICLE_ID"]) : (int?)null,
                                    ComponentArticleNo = reader["COMPONENT_ARTICLE_NO"]?.ToString() ?? string.Empty,
                                    ComponentArticleName = reader["COMPONENT_ARTICLE_NAME"]?.ToString() ?? string.Empty,
                                    LAST_ORDER_NO = reader["LASTORDERNO"]?.ToString() ?? string.Empty,
                                    CREATED_DATE = reader["CREATED_DATE"] != DBNull.Value
                                        ? (reader["CREATED_DATE"] is DateTimeOffset dto ? dto.DateTime : Convert.ToDateTime(reader["CREATED_DATE"]))
                                        : (DateTime?)null,
                                   STANDARD_PACKING = reader["STD_PACKING"]?.ToString() ?? string.Empty
                                };

                                // Parse JSON Sizes
                                if (reader["SIZES"] != DBNull.Value)
                                {
                                    var sizesJson = reader["SIZES"].ToString();
                                    articleDetails.SIZES = JsonConvert.DeserializeObject<List<Sizes>>(sizesJson);
                                }
                            }

                            // ✅ 2. Move to BOM result set
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
                                        ITEM_CODE = reader["ITEM_CODE"]?.ToString() ?? string.Empty,
                                        DESCRIPTION = reader["DESCRIPTION"]?.ToString() ?? string.Empty,
                                        UOM = reader["UOM"]?.ToString() ?? string.Empty,
                                    });
                                }

                                if (articleDetails != null)
                                    articleDetails.BOM = bomList;
                            }

                            // ✅ 3. Read final success flag (optional)
                            if (reader.NextResult() && reader.Read())
                            {
                                res.flag = reader["Flag"] != DBNull.Value ? Convert.ToInt32(reader["Flag"]) : 0;
                                res.Message = reader["Message"]?.ToString() ?? string.Empty;
                            }
                            else
                            {
                                res.flag = 1;
                                res.Message = "Success";
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

        public ArticleListResponse GetArticleList()
        {
            ArticleListResponse res = new ArticleListResponse();
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            var articles = new List<ArticleUpdate>();

                            #region STEP 1 ➤ Read article master
                            while (reader.Read())
                            {
                                var article = new ArticleUpdate
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt64(reader["ID"]) : 0,
                                    ART_NO = reader["ART_NO"]?.ToString(),
                                    UNIT_ID = reader["UNIT_ID"]?.ToString(), // ⚠️ Keep as STRING
                                    COLOR = reader["COLOR"]?.ToString(),
                                    CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    ARTICLE_TYPE_NAME = reader["ARTICLE_TYPE_NAME"]?.ToString(),
                                    CATEGORY_NAME = reader["CATEGORY_NAME"]?.ToString(),
                                    BRAND_NAME = reader["BRAND_NAME"]?.ToString(),
                                    IS_STOPPED = reader["IS_STOPPED"] != DBNull.Value && Convert.ToBoolean(reader["IS_STOPPED"]),
                                    IS_COMPONENT = reader["IS_COMPONENT"] != DBNull.Value && Convert.ToBoolean(reader["IS_COMPONENT"]),
                                    ComponentArticleNo = reader["COMPONENT_ARTICLE_NO"]?.ToString(),
                                    ComponentArticleName = reader["COMPONENT_ARTICLE_NAME"]?.ToString(),
                                    CREATED_DATE = reader["CREATED_DATE"] != DBNull.Value
                                    ? (reader["CREATED_DATE"] is DateTimeOffset dto
                                            ? dto.UtcDateTime
                                            : Convert.ToDateTime(reader["CREATED_DATE"]))
                                    : (DateTime?)null,
                                    STANDARD_PACKING = reader["STD_PACKING"]?.ToString() ?? "",
                                    SIZES = new List<Sizes>()
                                };
                                articles.Add(article);
                            }
                            #endregion

                            #region STEP 2 ➤ Read Sizes
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var size = new Sizes
                                    {
                                        SizeValue = reader["SizeValue"] != DBNull.Value ? Convert.ToInt32(reader["SizeValue"]) : 0,
                                        OrderNo = reader["ORDER_NO"]?.ToString()
                                    };

                                    string artNo = reader["ART_NO"]?.ToString();
                                    string color = reader["COLOR"]?.ToString();
                                    string unitIdStr = reader["UNIT_ID"]?.ToString() ?? "";  // ⚠️ Keep string
                                    int catId = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0;
                                    float price = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0;

                                    // ✅ Match Sizes correctly for multi-unit
                                    var match = articles.FirstOrDefault(a =>
                                    {
                                        if (a.ART_NO != artNo) return false;
                                        if (a.COLOR != color) return false;
                                        if (a.CATEGORY_ID != catId) return false;
                                        if (a.PRICE != price) return false;
                                        if (string.IsNullOrEmpty(a.UNIT_ID)) return false;

                                        // Article Unit IDs as string list (no conversion)
                                        var articleUnits = a.UNIT_ID.Split(',')
                                                                    .Select(x => x.Trim())
                                                                    .ToList();

                                        // Size Row Unit ID as string list (no conversion)
                                        var sizeUnits = unitIdStr.Split(',')
                                                                 .Select(x => x.Trim())
                                                                 .ToList();

                                        // Check if any unit matches (string comparison)
                                        return sizeUnits.Any(u => articleUnits.Contains(u));
                                    });


                                    if (match != null)
                                        match.SIZES.Add(size);
                                }
                            }
                            #endregion

                            res.Data = articles;
                            res.flag = 1;
                            res.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        public ListItemsResponse GetItems()
        {
            ListItemsResponse res = new ListItemsResponse();
            res.DataList = new List<ItemData>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT ID, ITEM_CODE + '-' + DESCRIPTION AS ITEM_NAME 
                             FROM TB_ITEMS 
                             WHERE IS_DELETED = 0 
                             ORDER BY ID ASC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())  
                            {
                                res.DataList.Add(new ItemData
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt64(reader["ID"]) : 0,
                                    DESCRIPTION = reader["ITEM_NAME"]?.ToString() ?? string.Empty,
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

        public string GetLastOrderNoByUnitId()
        {
            string lastOrderNo = "0";

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT ISNULL(MAX(ORDER_NO), 0) AS LastOrderNo 
                             FROM TB_ARTICLE";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
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
