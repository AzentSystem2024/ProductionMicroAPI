using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class PackingService : IPackingService
    {

        public List<ProductionUnit> GetProductionUnits()
        {
            List<ProductionUnit> units = new List<ProductionUnit>();
            using (var connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "GetProductionUnits");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            units.Add(new ProductionUnit
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                UnitName = reader.GetString(reader.GetOrdinal("UNIT_NAME"))
                            });
                        }
                    }
                }
            }
            return units;
        }

        public List<ArticleSize> GetArticleSizesForCombination(string artNo, string color, int categoryID, int unitID)
        {
            List<ArticleSize> articleSizes = new List<ArticleSize>();
            using (var connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "GetArticlesForCombination");
                    cmd.Parameters.AddWithValue("@ART_NO", artNo);
                    cmd.Parameters.AddWithValue("@COLOR", color);
                    cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryID);
                    cmd.Parameters.AddWithValue("@UNIT_ID", unitID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articleSizes.Add(new ArticleSize
                            {
                                ArticleID = reader.GetInt64(reader.GetOrdinal("ArticleID")),
                                Size = reader.GetString(reader.GetOrdinal("Size"))
                            });
                        }
                    }
                }
            }
            return articleSizes;
        }



        //public List<ArticleWithSize> GetArticlesWithSizes()
        //{
        //    List<ArticleWithSize> articlesWithSizes = new List<ArticleWithSize>();
        //    using (var connection = ADO.GetConnection())
        //    {
        //        if (connection.State == ConnectionState.Closed)
        //            connection.Open();

        //        using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ActionType", "GetArticlesForCombination");

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    articlesWithSizes.Add(new ArticleWithSize
        //                    {
        //                        ArticleID = reader.GetInt64(reader.GetOrdinal("ArticleID")),
        //                        ArtNo = reader.GetString(reader.GetOrdinal("ArtNo")),
        //                        Color = reader.GetString(reader.GetOrdinal("Color")),
        //                        CategoryID = Convert.ToInt32(reader.GetOrdinal("CategoryID")),
        //                        UnitID = Convert.ToInt32(reader.GetOrdinal("UnitID")),
        //                        Size = reader.GetString(reader.GetOrdinal("Size"))
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return articlesWithSizes;
        //}
        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (var connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "GetSuppliers");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                UnitName = reader.GetString(reader.GetOrdinal("UNIT_NAME"))
                            });
                        }
                    }
                }
            }
            return suppliers;
        }
        public PackingResponse Insert(PackingMasters packing)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    //// Fetch the last ORDER_NO for the given UNIT_ID and increment it by 1
                    //using (var cmd = new SqlCommand("SELECT ISNULL(MAX(CAST(ORDER_NO AS INT)), 0) AS LastOrderNo FROM TB_PACKING WHERE UNIT_ID = @UNIT_ID", connection))
                    //{
                    //    cmd.Parameters.AddWithValue("@UNIT_ID", packing.UNIT_ID);
                    //    var lastOrderNo = cmd.ExecuteScalar();

                    //    // Ensure lastOrderNo is not null and convert it to an integer
                    //    int nextOrderNo = lastOrderNo != null && lastOrderNo != DBNull.Value ? Convert.ToInt32(lastOrderNo) + 1 : 1;
                    //    packing.ORDER_NO = nextOrderNo.ToString();
                    //}

                    //// Generate the combination string based on the selected sizes and quantities
                    //string combination = GenerateCombinationString(packing.ART_NO, packing.COLOR, packing.CATEGORY_ID, packing.UNIT_ID, packing.PAIR_QTY);
                    //packing.COMBINATION = combination;

                    // Insert the record with the generated ORDER_NO
                    using (var insertCmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        insertCmd.CommandType = CommandType.StoredProcedure;
                        insertCmd.Parameters.AddWithValue("@ActionType", "InsertPacking");
                        insertCmd.Parameters.AddWithValue("@ART_NO", packing.ART_NO);
                        //insertCmd.Parameters.AddWithValue("@ORDER_NO", packing.ORDER_NO);
                        insertCmd.Parameters.AddWithValue("@DESCRIPTION", packing.DESCRIPTION);
                        insertCmd.Parameters.AddWithValue("@CATEGORY_ID", packing.CATEGORY_ID);
                        insertCmd.Parameters.AddWithValue("@COLOR", packing.COLOR);
                        insertCmd.Parameters.AddWithValue("@PAIR_QTY", packing.PAIR_QTY);
                        insertCmd.Parameters.AddWithValue("@IS_INACTIVE", packing.IS_INACTIVE);
                        insertCmd.Parameters.AddWithValue("@ART_SERIAL", packing.ART_SERIAL);
                        insertCmd.Parameters.AddWithValue("@IS_ANY_QTY", packing.IS_ANY_QTY);
                        insertCmd.Parameters.AddWithValue("@PART_NO", packing.PART_NO);
                        insertCmd.Parameters.AddWithValue("@ALIAS_NO", packing.ALIAS_NO);
                        insertCmd.Parameters.AddWithValue("@IS_ANY_COMB", packing.IS_ANY_COMB);
                        insertCmd.Parameters.AddWithValue("@PACK_PRICE", packing.PACK_PRICE);
                        insertCmd.Parameters.AddWithValue("@UNIT_ID", packing.UNIT_ID);
                        insertCmd.Parameters.AddWithValue("@IS_PURCHASABLE", packing.IS_PURCHASABLE);
                        insertCmd.Parameters.AddWithValue("@SUPP_ID", packing.SUPP_ID);
                        insertCmd.Parameters.AddWithValue("@IS_EXPORT", packing.IS_EXPORT);
                        insertCmd.Parameters.AddWithValue("@ARTICLE_TYPE", packing.ARTICLE_TYPE);
                        insertCmd.Parameters.AddWithValue("@BRAND_ID", packing.BRAND_ID);
                        insertCmd.Parameters.AddWithValue("@COMBINATION", packing.COMBINATION);
                        insertCmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", packing.NEW_ARRIVAL_DAYS);

                        insertCmd.ExecuteNonQuery();
                        res.flag = 1;
                        res.Message = "Insert successful";
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


        public PackingResponse Update(PackingUpdate packing)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "UpdatePacking");
                        cmd.Parameters.AddWithValue("@ID", packing.ID);
                        cmd.Parameters.AddWithValue("@ART_NO", packing.ART_NO);
                        cmd.Parameters.AddWithValue("@ORDER_NO", packing.ORDER_NO);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", packing.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", packing.CATEGORY_ID);
                        cmd.Parameters.AddWithValue("@COLOR", packing.COLOR);
                        cmd.Parameters.AddWithValue("@PAIR_QTY", packing.PAIR_QTY);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", packing.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@ART_SERIAL", packing.ART_SERIAL);
                        cmd.Parameters.AddWithValue("@PART_NO", packing.PART_NO);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", packing.ALIAS_NO);
                        cmd.Parameters.AddWithValue("@IS_ANY_COMB", packing.IS_ANY_COMB);
                        cmd.Parameters.AddWithValue("@PACK_PRICE", packing.PRICE);
                        cmd.Parameters.AddWithValue("@UNIT_ID", packing.UNIT_ID);
                        cmd.Parameters.AddWithValue("@IS_PURCHASABLE", packing.IS_PURCHASABLE);
                        cmd.Parameters.AddWithValue("@SUPP_ID", packing.SUPP_ID);
                        cmd.Parameters.AddWithValue("@IS_EXPORT", packing.IS_EXPORT);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", packing.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@BRAND_ID", packing.BRAND_ID);
                        cmd.Parameters.AddWithValue("@COMBINATION", packing.COMBINATION);
                       // cmd.Parameters.AddWithValue("@IMAGE_NAME", packing.IMAGE_NAME);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", packing.NEW_ARRIVAL_DAYS);

                        cmd.ExecuteNonQuery();
                        res.flag = 1;
                        res.Message = "Update successful";
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


        public PackingResponse GetPackingById(int id)
        {
            PackingResponse response = new PackingResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "GetPackingById");
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response.Data = new PackingUpdate
                                {
                                    ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                    ART_NO = reader.IsDBNull(reader.GetOrdinal("ArtNo")) ? null : reader.GetString(reader.GetOrdinal("ArtNo")),
                                    ORDER_NO = reader.IsDBNull(reader.GetOrdinal("OrderNo")) ? null : reader.GetString(reader.GetOrdinal("OrderNo")),
                                    DESCRIPTION = reader.IsDBNull(reader.GetOrdinal("PackingName")) ? null : reader.GetString(reader.GetOrdinal("PackingName")),
                                    COLOR = reader.IsDBNull(reader.GetOrdinal("Color")) ? null : reader.GetString(reader.GetOrdinal("Color")),
                                    PRICE = reader.IsDBNull(reader.GetOrdinal("PackPrice")) ? 0 : (float)reader.GetDouble(reader.GetOrdinal("PackPrice")),
                                    PAIR_QTY = reader.IsDBNull(reader.GetOrdinal("PairQty")) ? 0 : reader.GetInt32(reader.GetOrdinal("PairQty")),
                                    PART_NO = reader.IsDBNull(reader.GetOrdinal("PartNo")) ? null : reader.GetString(reader.GetOrdinal("PartNo")),
                                    ALIAS_NO = reader.IsDBNull(reader.GetOrdinal("AliasNo")) ? null : reader.GetString(reader.GetOrdinal("AliasNo")),
                                    UNIT_ID = reader.IsDBNull(reader.GetOrdinal("UnitId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UnitId")),
                                   // UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : reader.GetString(reader.GetOrdinal("UnitCode")),
                                    ARTICLE_TYPE = reader.IsDBNull(reader.GetOrdinal("ArticleType")) ? 0 : reader.GetInt32(reader.GetOrdinal("ArticleType")),
                                   // ARTICLE_TYPE_NAME = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(reader.GetOrdinal("Type")),
                                    CATEGORY_ID = reader.IsDBNull(reader.GetOrdinal("CategoryId")) ? 0 : reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                    //CATEGORY_NAME = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                                    BRAND_ID = reader.IsDBNull(reader.GetOrdinal("BrandId")) ? 0 : reader.GetInt32(reader.GetOrdinal("BrandId")),
                                   // BRAND_NAME = reader.IsDBNull(reader.GetOrdinal("Brand")) ? null : reader.GetString(reader.GetOrdinal("Brand")),
                                    ART_SERIAL = reader.IsDBNull(reader.GetOrdinal("ArtSerial")) ? null : reader.GetString(reader.GetOrdinal("ArtSerial")),
                                   // IMAGE_NAME = reader.IsDBNull(reader.GetOrdinal("ImageName")) ? null : reader.GetString(reader.GetOrdinal("ImageName")),
                                    NEW_ARRIVAL_DAYS = reader.IsDBNull(reader.GetOrdinal("NewArrivalDays")) ? 0 : reader.GetInt32(reader.GetOrdinal("NewArrivalDays")),
                                    IS_STOPPED = reader.IsDBNull(reader.GetOrdinal("IsStopped")) ? false : reader.GetBoolean(reader.GetOrdinal("IsStopped")),
                                    SUPP_ID = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                    //SupplierName = reader.IsDBNull(reader.GetOrdinal("Supplier")) ? null : reader.GetString(reader.GetOrdinal("Supplier")),
                                    COMBINATION = reader.IsDBNull(reader.GetOrdinal("Combination")) ? null : reader.GetString(reader.GetOrdinal("Combination")),
                                    IS_PURCHASABLE = reader.IsDBNull(reader.GetOrdinal("IsPurchasable")) ? false : reader.GetBoolean(reader.GetOrdinal("IsPurchasable")),
                                    IS_EXPORT = reader.IsDBNull(reader.GetOrdinal("IsExport")) ? false : reader.GetBoolean(reader.GetOrdinal("IsExport")),
                                    IS_ANY_COMB = reader.IsDBNull(reader.GetOrdinal("IsAnyComb")) ? false : reader.GetBoolean(reader.GetOrdinal("IsAnyComb")),
                                    IS_INACTIVE = reader.IsDBNull(reader.GetOrdinal("IsInactive")) ? false : reader.GetBoolean(reader.GetOrdinal("IsInactive")),
                                    CreatedDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                                };
                            }
                        }
                    }
                    response.flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

        public PackingListResponses GetPackingList()
        {
            PackingListResponses response = new PackingListResponses();
            List<PackingListItem> packingList = new List<PackingListItem>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "GetPackingList");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                packingList.Add(new PackingListItem
                                {
                                    ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(reader.GetOrdinal("ID")),
                                    ArtNo = reader.IsDBNull(reader.GetOrdinal("ArtNo")) ? null : reader.GetString(reader.GetOrdinal("ArtNo")),
                                    Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? null : reader.GetString(reader.GetOrdinal("Color")),
                                    OrderNo = reader.IsDBNull(reader.GetOrdinal("OrderNo")) ? null : reader.GetString(reader.GetOrdinal("OrderNo")),
                                    Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                                    PackingName = reader.IsDBNull(reader.GetOrdinal("PackingName")) ? null : reader.GetString(reader.GetOrdinal("PackingName")),
                                    Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(reader.GetOrdinal("Type")),
                                    Brand = reader.IsDBNull(reader.GetOrdinal("Brand")) ? null : reader.GetString(reader.GetOrdinal("Brand")),
                                    Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? null : reader.GetString(reader.GetOrdinal("Unit")),
                                    PartNo = reader.IsDBNull(reader.GetOrdinal("PartNo")) ? null : reader.GetString(reader.GetOrdinal("PartNo")),
                                    AliasNo = reader.IsDBNull(reader.GetOrdinal("AliasNo")) ? null : reader.GetString(reader.GetOrdinal("AliasNo")),
                                    PairQty = reader.IsDBNull(reader.GetOrdinal("PairQty")) ? 0 : reader.GetInt32(reader.GetOrdinal("PairQty")),
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status"))
                                });
                            }
                        }
                    }
                }

                response.flag = 1;
                response.Message = "Success";
                response.Data = packingList;
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }


        public PackingResponse DeletePackingData(int id)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "DeletePacking");
                        cmd.Parameters.AddWithValue("@ID", id);

                        cmd.ExecuteNonQuery();
                        res.flag = 1;
                        res.Message = "Delete successful";
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
    }
}

