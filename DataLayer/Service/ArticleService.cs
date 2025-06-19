using MicroApi.Helper;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace MicroApi.Service
{
    public class ArticleService : IArticleService
    {
        public ArticleResponse Insert(Article article)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                // Ensure IsComponent and ComponentArticleID are not both set
                if (article.IsComponent && article.ComponentArticleID.HasValue && article.ComponentArticleID.Value != 0)
                {
                    res.flag = 0;
                    res.Message = "IsComponent cannot be true while ComponentArticleID is set.";
                    return res;
                }
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@ART_NO", article.ART_NO);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", (object)article.DESCRIPTION ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@COLOR", (object)article.COLOR ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SIZE", (object)article.SIZE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PRICE", article.PRICE);
                        cmd.Parameters.AddWithValue("@PACK_QTY", article.PACK_QTY);
                        cmd.Parameters.AddWithValue("@PART_NO", (object)article.PART_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", (object)article.ALIAS_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UNIT_ID", article.UNIT_ID);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", article.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", article.CATEGORY_ID);
                        cmd.Parameters.AddWithValue("@IMAGE_NAME", (object)article.IMAGE_NAME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BRAND_ID", article.BRAND_ID);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", article.NEW_ARRIVAL_DAYS);
                        cmd.Parameters.AddWithValue("@IS_STOPPED", article.IS_STOPPED);
                        cmd.Parameters.AddWithValue("@SUPPLIER_ID", article.SUPPLIER_ID);
                        cmd.Parameters.AddWithValue("@IsComponent", article.IsComponent);
                        cmd.Parameters.AddWithValue("@ComponentArticleID", (object)article.ComponentArticleID ?? DBNull.Value);


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
        //public string GetSupplierNameByUnitId(int unitId)
        //{
        //    string supplierName = string.Empty;
        //    try
        //    {
        //        using (var connection = ADO.GetConnection())
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();

        //            using (var cmd = new SqlCommand("SELECT UNIT_NAME FROM TB_MATERIAL_UNITS WHERE ID = @UNIT_ID", connection))
        //            {
        //                cmd.Parameters.AddWithValue("@UNIT_ID", unitId);

        //                var result = cmd.ExecuteScalar();
        //                if (result != null && result != DBNull.Value)
        //                {
        //                    supplierName = result.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return supplierName;
        //}


        public ArticleResponse Update(ArticleUpdate article)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", article.ID);
                        cmd.Parameters.AddWithValue("@ART_NO", (object)article.ART_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", (object)article.DESCRIPTION ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@COLOR", (object)article.COLOR ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SIZE", (object)article.SIZE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PRICE", (object)article.PRICE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PACK_QTY", (object)article.PACK_QTY ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PART_NO", (object)article.PART_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", (object)article.ALIAS_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UNIT_ID", (object)article.UNIT_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", (object)article.ARTICLE_TYPE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", (object)article.CATEGORY_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BRAND_ID", (object)article.BRAND_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", (object)article.NEW_ARRIVAL_DAYS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_STOPPED", article.IS_STOPPED);
                        cmd.Parameters.AddWithValue("@SUPPLIER_ID", (object)article.SUPPLIER_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsComponent", article.IsComponent);
                        cmd.Parameters.AddWithValue("@ComponentArticleID", (object)article.ComponentArticleID ?? DBNull.Value);

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


        public ArticleResponse GetArticleById(int id)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new ArticleUpdate
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    ART_NO = reader["ART_NO"] != DBNull.Value ? reader["ART_NO"].ToString() : string.Empty,
                                    ORDER_NO = reader["ORDER_NO"] != DBNull.Value ? reader["ORDER_NO"].ToString() : string.Empty,
                                    LAST_ORDER_NO = reader["LAST_ORDER_NO"] != DBNull.Value ? reader["LAST_ORDER_NO"].ToString() : string.Empty,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : string.Empty,
                                    COLOR = reader["COLOR"] != DBNull.Value ? reader["COLOR"].ToString() : string.Empty,
                                    SIZE = reader["SIZE"] != DBNull.Value ? reader["SIZE"].ToString() : string.Empty,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    PACK_QTY = reader["PACK_QTY"] != DBNull.Value ? Convert.ToInt32(reader["PACK_QTY"]) : 0,
                                    PART_NO = reader["PART_NO"] != DBNull.Value ? reader["PART_NO"].ToString() : string.Empty,
                                    ALIAS_NO = reader["ALIAS_NO"] != DBNull.Value ? reader["ALIAS_NO"].ToString() : string.Empty,
                                    UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    ARTICLE_TYPE = reader["ARTICLE_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_TYPE"]) : 0,
                                    CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0,
                                    BRAND_ID = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : 0,
                                    NEW_ARRIVAL_DAYS = reader["NEW_ARRIVAL_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["NEW_ARRIVAL_DAYS"]) : 0,
                                    NEXT_SERIAL = reader["NEXT_SERIAL"] != DBNull.Value ? Convert.ToInt32(reader["NEXT_SERIAL"]) : 0,
                                    IMAGE_NAME = reader["IMAGE_NAME"] != DBNull.Value ? reader["IMAGE_NAME"].ToString() : string.Empty,
                                    IsComponent = reader["IsComponent"] != DBNull.Value && Convert.ToBoolean(reader["IsComponent"])

                                };
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = $"This Article is not found for ID={id}";
                                res.Data = null;
                            }
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


        public ArticleListResponse GetLogList()
        {
            ArticleListResponse res = new ArticleListResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);

                        using (var reader = cmd.ExecuteReader())
                        {
                            res.Data = MapArticles(reader).ToList();
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

        private IEnumerable<ArticleUpdate> MapArticles(SqlDataReader reader)
        {
            while (reader.Read())
            {
                yield return new ArticleUpdate
                {
                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt64(reader["ID"]) : 0,
                    ART_NO = reader["ART_NO"] != DBNull.Value ? reader["ART_NO"].ToString() : string.Empty,
                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : string.Empty,
                    COLOR = reader["COLOR"] != DBNull.Value ? reader["COLOR"].ToString() : string.Empty,
                    SIZE = reader["SIZE"] != DBNull.Value ? reader["SIZE"].ToString() : string.Empty,
                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                    PACK_QTY = reader["PACK_QTY"] != DBNull.Value ? Convert.ToInt32(reader["PACK_QTY"]) : 0,
                    PART_NO = reader["PART_NO"] != DBNull.Value ? reader["PART_NO"].ToString() : string.Empty,
                    ALIAS_NO = reader["ALIAS_NO"] != DBNull.Value ? reader["ALIAS_NO"].ToString() : string.Empty,
                    UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                    ARTICLE_TYPE = reader["ARTICLE_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_TYPE"]) : 0,
                    ARTICLE_TYPE_NAME = reader["ARTICLE_TYPE_NAME"] != DBNull.Value ? reader["ARTICLE_TYPE_NAME"].ToString() : string.Empty,
                    CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0,
                    CATEGORY_NAME = reader["CATEGORY_NAME"] != DBNull.Value ? reader["CATEGORY_NAME"].ToString() : string.Empty,
                    BRAND_ID = reader["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(reader["BRAND_ID"]) : 0,
                    BRAND_NAME = reader["BRAND_NAME"] != DBNull.Value ? reader["BRAND_NAME"].ToString() : string.Empty,
                    NEW_ARRIVAL_DAYS = reader["NEW_ARRIVAL_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["NEW_ARRIVAL_DAYS"]) : 0,
                    IS_STOPPED = reader["IS_STOPPED"] != DBNull.Value && Convert.ToBoolean(reader["IS_STOPPED"]),
                    //STATUS = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : string.Empty,
                    IsComponent = reader["IsComponent"] != DBNull.Value && Convert.ToBoolean(reader["IsComponent"])

                };
            }
        }

        public ArticleResponse DeleteArticleData(int id)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var cmd = new SqlCommand("SP_TB_ARTICLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

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

        //getting Lasrorderno from the unit
        public string GetLastOrderNoByUnitId(int unitId)
        {
            string lastOrderNo = "0"; // Default value if no order number is found
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_GetLastOrderNoByUnitId", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UNIT_ID", unitId);

                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            lastOrderNo = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex.Message);
            }
            return lastOrderNo;
        }
    }
}
