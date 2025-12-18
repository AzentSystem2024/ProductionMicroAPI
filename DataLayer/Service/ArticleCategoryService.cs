using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class ArticleCategoryService:IArticleCategoryService
    {
        public ArticleCategoryListResponse GetArticleCategory(int Id)
        {
            ArticleCategoryListResponse res = new ArticleCategoryListResponse();
            List<ArticleCategory> listSizes = new List<ArticleCategory>();

            string query = @"
                SELECT ID, CATEGORY_ID, SIZE 
                FROM TB_ARTICLE_CATEGORY_SIZES 
                WHERE CATEGORY_ID = @CATEGORY_ID
                ORDER BY SIZE";

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CATEGORY_ID", Id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            listSizes.Add(new ArticleCategory
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                CATEGORY_ID = Convert.ToInt32(dr["CATEGORY_ID"]),
                                SIZE = Convert.ToInt32(dr["SIZE"])
                            });
                        }
                    }
                }

                res.flag = 1;
                res.Message = "Success";
                res.Data = listSizes;
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }



        public CategoryResponse GetCategoryPackingDetails(int Id)
        {
            var res = new CategoryResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    // Load category basic details
                    var catCmd = new SqlCommand("SELECT ID, CODE, DESCRIPTION,IS_INACTIVE,COMPANY_ID FROM TB_ARTICLE_CATEGORY WHERE IS_DELETED=0 AND ID = @catId", con);
                    catCmd.Parameters.AddWithValue("@catId", Id);

                    using var reader = catCmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        res.flag = 0;
                        res.Message = "Category not found";
                        return res;
                    }

                    var category = new Category
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        COMPANY_ID = Convert.ToInt32(reader["COMPANY_ID"]),
                        CODE = Convert.ToString(reader["CODE"]),
                        NAME = Convert.ToString(reader["DESCRIPTION"]),
                        IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : false,
                        SIZES = new List<int>(),
                        PACKING = new List<PackingMaster>()
                    };
                    reader.Close();

                    // Load sizes
                    var sizeCmd = new SqlCommand("SELECT SIZE FROM TB_ARTICLE_CATEGORY_SIZES WHERE CATEGORY_ID = @catId", con);
                    sizeCmd.Parameters.AddWithValue("@catId", Id);

                    var sizeTbl = new DataTable();
                    sizeTbl.Load(sizeCmd.ExecuteReader());
                    foreach (DataRow row in sizeTbl.Rows)
                    {
                        category.SIZES.Add(Convert.ToInt32(row["SIZE"])); 
                    }

                    // Load packing + combination
                    var packCmd = new SqlCommand(@"
                SELECT 
                    P.PACKING AS PACK_NAME,
                    P.IS_EXPORT,
                    P.IS_ANY_COMBINATION,
                    P.PAIR_QTY,
                    S.SIZE AS SIZE_ID,       
                    C.PACK_QTY AS QUANTITY
                FROM TB_ARTICLE_CATEGORY_PACKING P
                INNER JOIN TB_ARTICLE_CATEGORY_COMBINATION C ON P.ID = C.PACK_ID
                INNER JOIN TB_ARTICLE_CATEGORY_SIZES S ON C.SIZE_ID = S.ID
                WHERE P.CATEGORY_ID = @catId
                ORDER BY P.PACKING, S.SIZE", con);

                    packCmd.Parameters.AddWithValue("@catId", Id);

                    var packTbl = new DataTable();
                    packTbl.Load(packCmd.ExecuteReader());

                    var packings = new Dictionary<string, PackingMaster>();

                    foreach (DataRow row in packTbl.Rows)
                    {
                        var packName = Convert.ToString(row["PACK_NAME"]);

                        if (!packings.ContainsKey(packName))
                        {
                            packings[packName] = new PackingMaster
                            {
                                NAME = packName,
                                ISEXPORT = Convert.ToBoolean(row["IS_EXPORT"]),
                                ISANYCOMBINATION = Convert.ToBoolean(row["IS_ANY_COMBINATION"]),
                                PAIR_QTY = Convert.ToInt32(row["PAIR_QTY"]),
                                PACKCOMBINATIONS = new List<PackCombination>()
                            };
                        }

                        packings[packName].PACKCOMBINATIONS.Add(new PackCombination
                        {
                            size = Convert.ToInt32(row["SIZE_ID"]),    
                            pairQty = Convert.ToInt32(row["QUANTITY"])
                        });
                    }

                    category.PACKING = packings.Values.ToList();

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = category;
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public ArticleCategoryResponse InsertCategoryDetails(ArticleCategoryInsert request)
        {
            var res = new ArticleCategoryResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    // 1️⃣ Insert into TB_ARTICLE_CATEGORY
                    string catSql = @"INSERT INTO TB_ARTICLE_CATEGORY (CODE, DESCRIPTION,IS_DELETED,IS_INACTIVE,COMPANY_ID) 
                              VALUES (@CODE, @DESC,0,@IS_INACTIVE,@COMPANY_ID); 
                              SELECT SCOPE_IDENTITY();";

                    int categoryId;
                    using (SqlCommand cmd = new SqlCommand(catSql, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@CODE", request.CODE);
                        cmd.Parameters.AddWithValue("@DESC", request.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", request.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        categoryId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2️⃣ Insert sizes
                    foreach (var size in request.SIZES)
                    {
                        string sizeSql = @"INSERT INTO TB_ARTICLE_CATEGORY_SIZES (CATEGORY_ID, SIZE) 
                                   VALUES (@CID, @SIZE);";
                        using (SqlCommand cmd = new SqlCommand(sizeSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@CID", categoryId);
                            cmd.Parameters.AddWithValue("@SIZE", size);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 3️⃣ Insert packing + combinations
                    foreach (var pack in request.PACKING)
                    {
                        string packSql = @"INSERT INTO TB_ARTICLE_CATEGORY_PACKING 
                                   (CATEGORY_ID, PACKING, IS_EXPORT, IS_ANY_COMBINATION,PAIR_QTY)
                                   VALUES (@CID, @PACK, @EXPORT, @COMBO,@PAIR_QTY);
                                   SELECT SCOPE_IDENTITY();";

                        int packId;
                        using (SqlCommand cmd = new SqlCommand(packSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@CID", categoryId);
                            cmd.Parameters.AddWithValue("@PACK", pack.NAME);
                            cmd.Parameters.AddWithValue("@EXPORT", pack.ISEXPORT);
                            cmd.Parameters.AddWithValue("@COMBO", pack.ISANYCOMBINATION);
                            cmd.Parameters.AddWithValue("@PAIR_QTY", pack.PAIR_QTY);
                            packId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        foreach (var combo in pack.PACKCOMBINATIONS)
                        {
                            // Get size ID
                            string sizeIdSql = @"SELECT ID FROM TB_ARTICLE_CATEGORY_SIZES 
                                         WHERE CATEGORY_ID = @CID AND SIZE = @SIZE";
                            int sizeId;
                            using (SqlCommand cmd = new SqlCommand(sizeIdSql, con, tx))
                            {
                                cmd.Parameters.AddWithValue("@CID", categoryId);
                                cmd.Parameters.AddWithValue("@SIZE", combo.size);
                                var result = cmd.ExecuteScalar();
                                if (result == null) throw new Exception($"Size {combo.size} not found for category.");
                                sizeId = Convert.ToInt32(result);
                            }

                            // Insert combination
                            string comboSql = @"INSERT INTO TB_ARTICLE_CATEGORY_COMBINATION 
                                        (PACK_ID, SIZE_ID, PACK_QTY)
                                        VALUES (@PACKID, @SIZEID, @QTY)";
                            using (SqlCommand cmd = new SqlCommand(comboSql, con, tx))
                            {
                                cmd.Parameters.AddWithValue("@PACKID", packId);
                                cmd.Parameters.AddWithValue("@SIZEID", sizeId);
                                cmd.Parameters.AddWithValue("@QTY", combo.pairQty);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tx.Commit();
                    res.flag = 1;
                    res.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        public ArticleCategoryResponse UpdateCategoryDetails(ArticleCategoryUpdate request)
        {
            var res = new ArticleCategoryResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    //con.Open();
                    using (SqlTransaction tran = con.BeginTransaction())
                    {
                        try
                        {
                            // Update main category
                            var updateCmd = new SqlCommand(@"
                        UPDATE TB_ARTICLE_CATEGORY 
                        SET CODE = @CODE, DESCRIPTION = @DESC ,IS_INACTIVE = @IS_INACTIVE,COMPANY_ID=@COMPANY_ID
                        WHERE IS_DELETED=0 AND ID = @ID", con, tran);
                            updateCmd.Parameters.AddWithValue("@CODE", request.CODE);
                            updateCmd.Parameters.AddWithValue("@DESC", request.DESCRIPTION);
                            updateCmd.Parameters.AddWithValue("@IS_INACTIVE", request.IS_INACTIVE);
                            updateCmd.Parameters.AddWithValue("@ID", request.ID);
                            updateCmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                            updateCmd.ExecuteNonQuery();

                            // Delete old data
                            var delCombCmd = new SqlCommand(@"
                        DELETE FROM TB_ARTICLE_CATEGORY_COMBINATION 
                        WHERE PACK_ID IN (SELECT ID FROM TB_ARTICLE_CATEGORY_PACKING WHERE CATEGORY_ID = @CID)", con, tran);
                            delCombCmd.Parameters.AddWithValue("@CID", request.ID);
                            delCombCmd.ExecuteNonQuery();

                            var delPackCmd = new SqlCommand("DELETE FROM TB_ARTICLE_CATEGORY_PACKING WHERE CATEGORY_ID = @CID", con, tran);
                            delPackCmd.Parameters.AddWithValue("@CID", request.ID);
                            delPackCmd.ExecuteNonQuery();

                            var delSizeCmd = new SqlCommand("DELETE FROM TB_ARTICLE_CATEGORY_SIZES WHERE CATEGORY_ID = @CID", con, tran);
                            delSizeCmd.Parameters.AddWithValue("@CID", request.ID);
                            delSizeCmd.ExecuteNonQuery();

                            // Insert new sizes
                            var sizeIdMap = new Dictionary<int, int>();
                            foreach (var size in request.SIZES)
                            {
                                var sizeCmd = new SqlCommand(@"
                            INSERT INTO TB_ARTICLE_CATEGORY_SIZES (CATEGORY_ID, SIZE)
                            VALUES (@CID, @SIZE);
                            SELECT SCOPE_IDENTITY();", con, tran);
                                sizeCmd.Parameters.AddWithValue("@CID", request.ID);
                                sizeCmd.Parameters.AddWithValue("@SIZE", size);

                                var sizeId = Convert.ToInt32(sizeCmd.ExecuteScalar());
                                sizeIdMap[size] = sizeId;
                            }

                            // Insert packing + combinations
                            foreach (var pack in request.PACKING)
                            {
                                var packCmd = new SqlCommand(@"
                            INSERT INTO TB_ARTICLE_CATEGORY_PACKING 
                            (CATEGORY_ID, PACKING, IS_EXPORT, IS_ANY_COMBINATION, PAIR_QTY)
                            VALUES (@CID, @PACKING, @ISEXPORT, @ISANYCOMBINATION, @PAIR_QTY);
                            SELECT SCOPE_IDENTITY();", con, tran);
                                packCmd.Parameters.AddWithValue("@CID", request.ID);
                                packCmd.Parameters.AddWithValue("@PACKING", pack.NAME);
                                packCmd.Parameters.AddWithValue("@ISEXPORT", pack.ISEXPORT);
                                packCmd.Parameters.AddWithValue("@ISANYCOMBINATION", pack.ISANYCOMBINATION);
                                packCmd.Parameters.AddWithValue("@PAIR_QTY", pack.PAIR_QTY);

                                var packId = Convert.ToInt32(packCmd.ExecuteScalar());

                                foreach (var comb in pack.PACKCOMBINATIONS)
                                {
                                    if (!sizeIdMap.TryGetValue(comb.size, out int sizeId))
                                    {
                                        throw new Exception($"Size '{comb.size}' not found in inserted sizes.");
                                    }

                                    var combCmd = new SqlCommand(@"
                                INSERT INTO TB_ARTICLE_CATEGORY_COMBINATION 
                                (PACK_ID, SIZE_ID, PACK_QTY)
                                VALUES (@PACKID, @SIZEID, @QTY);", con, tran);
                                    combCmd.Parameters.AddWithValue("@PACKID", packId);
                                    combCmd.Parameters.AddWithValue("@SIZEID", sizeId);
                                    combCmd.Parameters.AddWithValue("@QTY", comb.pairQty);

                                    combCmd.ExecuteNonQuery();
                                }
                            }

                            tran.Commit();
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        catch (Exception exInner)
                        {
                            tran.Rollback();
                            res.flag = 0;
                            res.Message = $"Transaction failed: {exInner.Message}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Connection failed: {ex.Message}";
            }

            return res;
        }


        public PackingListResponse GetPackingListByCategoryName(string categoryName)
        {
            var res = new PackingListResponse
            {
                flag = 1,
                Message = "Success",
                PackingList = new List<PackingSummary>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    var cmd = new SqlCommand(@"
                SELECT P.PACKING, P.PAIR_QTY
                FROM TB_ARTICLE_CATEGORY_PACKING P
                INNER JOIN TB_ARTICLE_CATEGORY C ON P.CATEGORY_ID = C.ID
                WHERE C.DESCRIPTION = @CATEGORY_NAME
                ORDER BY P.PACKING", con);

                    cmd.Parameters.AddWithValue("@CATEGORY_NAME", categoryName);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.PackingList.Add(new PackingSummary
                            {
                                PACKING = dr["PACKING"].ToString(),
                                PAIR = Convert.ToInt32(dr["PAIR_QTY"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Error: {ex.Message}";
            }

            return res;
        }

        public ArticleCategoryResponse SavePackingDetails(PackingSave request)
        {
            var res = new ArticleCategoryResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlTransaction tran = con.BeginTransaction())
                    {
                        try
                        {
                            // 🔹 Get category ID
                            var catCmd = new SqlCommand("SELECT ID FROM TB_ARTICLE_CATEGORY WHERE IS_DELETED=0 AND DESCRIPTION = @DESC", con, tran);
                            catCmd.Parameters.AddWithValue("@DESC", request.CATEGORY);
                            var categoryIdObj = catCmd.ExecuteScalar();

                            if (categoryIdObj == null)
                                throw new Exception("Category not found.");

                            int categoryId = Convert.ToInt32(categoryIdObj);

                            // 🔹 Insert into TB_ARTICLE_CATEGORY_PACKING
                            var packCmd = new SqlCommand(@"
                     INSERT INTO TB_ARTICLE_CATEGORY_PACKING 
                     (CATEGORY_ID, PACKING, IS_EXPORT, IS_ANY_COMBINATION, PAIR_QTY) 
                     VALUES (@CID, @PACK, @ISEXPORT, @ISANYCOMBINATION, @PAIR_QTY);
                     SELECT SCOPE_IDENTITY();", con, tran);

                            packCmd.Parameters.AddWithValue("@CID", categoryId);
                            packCmd.Parameters.AddWithValue("@PACK", request.PACK_NAME);
                            packCmd.Parameters.AddWithValue("@ISEXPORT", request.ISEXPORTPACKING);
                            packCmd.Parameters.AddWithValue("@ISANYCOMBINATION", request.ISANYCOMBINATION);
                            packCmd.Parameters.AddWithValue("@PAIR_QTY", request.PAIR_QTY);

                            int packId = Convert.ToInt32(packCmd.ExecuteScalar());

                            // 🔹 Insert size combinations
                            foreach (var sizeDetail in request.SIZEDETAILS)
                            {
                                // Ensure size exists or insert if needed
                                int sizeId;
                                var sizeIdCmd = new SqlCommand(@"
                          SELECT ID FROM TB_ARTICLE_CATEGORY_SIZES 
                          WHERE CATEGORY_ID = @CID AND SIZE = @SIZE", con, tran);
                                sizeIdCmd.Parameters.AddWithValue("@CID", categoryId);
                                sizeIdCmd.Parameters.AddWithValue("@SIZE", sizeDetail.SIZE);

                                var sizeIdObj = sizeIdCmd.ExecuteScalar();

                                if (sizeIdObj == null)
                                {
                                    // Insert size
                                    var insertSizeCmd = new SqlCommand(@"
                         INSERT INTO TB_ARTICLE_CATEGORY_SIZES (CATEGORY_ID, SIZE) 
                         VALUES (@CID, @SIZE);
                         SELECT SCOPE_IDENTITY();", con, tran);
                                    insertSizeCmd.Parameters.AddWithValue("@CID", categoryId);
                                    insertSizeCmd.Parameters.AddWithValue("@SIZE", sizeDetail.SIZE);

                                    sizeId = Convert.ToInt32(insertSizeCmd.ExecuteScalar());
                                }
                                else
                                {
                                    sizeId = Convert.ToInt32(sizeIdObj);
                                }

                                // Insert combination
                                var combCmd = new SqlCommand(@"
                          INSERT INTO TB_ARTICLE_CATEGORY_COMBINATION (PACK_ID, SIZE_ID, PACK_QTY) 
                          VALUES (@PACKID, @SIZEID, @QTY)", con, tran);
                                combCmd.Parameters.AddWithValue("@PACKID", packId);
                                combCmd.Parameters.AddWithValue("@SIZEID", sizeId);
                                combCmd.Parameters.AddWithValue("@QTY", sizeDetail.PAIR_QTY);

                                combCmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        catch (Exception exInner)
                        {
                            tran.Rollback();
                            res.flag = 0;
                            res.Message = $"Transaction failed: {exInner.Message}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Connection failed: {ex.Message}";
            }

            return res;
        }

        public ArticleCategoryResponse DeleteArticleCategoryData(int id)
        {
            ArticleCategoryResponse res = new ArticleCategoryResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string sql = @"UPDATE TB_ARTICLE_CATEGORY
                                    SET IS_DELETED = 1
                                    WHERE ID = @ID";

                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed";
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
        public CategoryListResponse GetAllArticleCategories(ArticleCategoryListReq request)
        {
            var res = new CategoryListResponse
            {
                flag = 1,
                Message = "Success",
                CATEGORIES = new List<ArticleCategoryItem>()
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string sql = @"
                SELECT ID, CODE, DESCRIPTION, IS_INACTIVE
                FROM TB_ARTICLE_CATEGORY
                WHERE IS_DELETED = 0 
                  AND COMPANY_ID = @COMPANY_ID
                ORDER BY TRY_CAST(CODE AS INT) ASC";

                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        // ✅ PASS COMPANY_ID FROM REQUEST BODY
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.CATEGORIES.Add(new ArticleCategoryItem
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    CODE = reader["CODE"]?.ToString() ?? string.Empty,
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString() ?? string.Empty,
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value
                                                    ? Convert.ToBoolean(reader["IS_INACTIVE"])
                                                    : false
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.CATEGORIES = new List<ArticleCategoryItem>();
            }

            return res;
        }



    }
}
