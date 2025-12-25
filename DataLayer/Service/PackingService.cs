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

        public List<ArticleSize> GetArticleSizesForCombination(ArticleSizeCombinationRequest request)
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
                    cmd.Parameters.AddWithValue("@ART_NO", request.artNo);
                    cmd.Parameters.AddWithValue("@COLOR", request.color);
                    cmd.Parameters.AddWithValue("@CATEGORY_ID", request.categoryID);
                    cmd.Parameters.AddWithValue("@UNIT_ID", request.unitID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articleSizes.Add(new ArticleSize
                            {
                                ArticleID = reader["ArticleID"] != DBNull.Value
                                    ? Convert.ToInt64(reader["ArticleID"])
                                    : 0,

                                Size = reader["Size"]?.ToString()
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

                    using (var insertCmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        insertCmd.CommandType = CommandType.StoredProcedure;

                        insertCmd.Parameters.AddWithValue("@ActionType", "InsertPacking");
                        insertCmd.Parameters.AddWithValue("@COMPANY_ID", packing.COMPANY_ID);
                        insertCmd.Parameters.AddWithValue("@ORDER_NO", packing.ORDER_NO ?? (object)DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@ART_NO", packing.ART_NO);
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

                        DataTable packingEntryTable = new DataTable();
                        packingEntryTable.Columns.Add("ARTICLE_ID", typeof(long));
                        packingEntryTable.Columns.Add("QUANTITY", typeof(float));
                        packingEntryTable.Columns.Add("SIZE", typeof(string));

                        if (packing.PackingEntries != null)
                        {
                            foreach (var entry in packing.PackingEntries)
                            {
                                packingEntryTable.Rows.Add(entry.ARTICLE_ID, entry.QUANTITY, entry.SIZE);
                            }
                        }

                        // ✅ Add the structured parameter
                        SqlParameter udtParam = new SqlParameter("@UDT_PACKING_ENTRY", SqlDbType.Structured);
                        udtParam.TypeName = "UDT_PACKING_ENTRY";
                        udtParam.Value = packingEntryTable;
                        insertCmd.Parameters.Add(udtParam);

                        using (SqlDataReader dr = insertCmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                res.flag = Convert.ToInt32(dr["flag"]);
                                res.Message = dr["Message"].ToString();
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
                        cmd.Parameters.AddWithValue("@COMPANY_ID", packing.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@ART_NO", packing.ART_NO);
                        cmd.Parameters.AddWithValue("@ORDER_NO", packing.ORDER_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", packing.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", packing.CATEGORY_ID);
                        cmd.Parameters.AddWithValue("@COLOR", packing.COLOR);
                        cmd.Parameters.AddWithValue("@PAIR_QTY", packing.PAIR_QTY);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", packing.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@ART_SERIAL", packing.ART_SERIAL ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_ANY_QTY", packing.IS_ANY_QTY);
                        cmd.Parameters.AddWithValue("@PART_NO", packing.PART_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ALIAS_NO", packing.ALIAS_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_ANY_COMB", packing.IS_ANY_COMB);
                        cmd.Parameters.AddWithValue("@PACK_PRICE", packing.PACK_PRICE);
                        cmd.Parameters.AddWithValue("@UNIT_ID", packing.UNIT_ID);
                        cmd.Parameters.AddWithValue("@IS_PURCHASABLE", packing.IS_PURCHASABLE);
                        cmd.Parameters.AddWithValue("@SUPP_ID", packing.SUPP_ID);
                        cmd.Parameters.AddWithValue("@IS_EXPORT", packing.IS_EXPORT);
                        cmd.Parameters.AddWithValue("@ARTICLE_TYPE", packing.ARTICLE_TYPE);
                        cmd.Parameters.AddWithValue("@BRAND_ID", packing.BRAND_ID);
                        cmd.Parameters.AddWithValue("@COMBINATION", packing.COMBINATION);
                        cmd.Parameters.AddWithValue("@NEW_ARRIVAL_DAYS", packing.NEW_ARRIVAL_DAYS);

                        // ✅ Add UDT parameter for packing entries
                        DataTable packingEntryTable = new DataTable();
                        packingEntryTable.Columns.Add("ARTICLE_ID", typeof(long));
                        packingEntryTable.Columns.Add("QUANTITY", typeof(float));
                        packingEntryTable.Columns.Add("SIZE", typeof(string));

                        if (packing.PackingEntries != null)
                        {
                            foreach (var entry in packing.PackingEntries)
                            {
                                packingEntryTable.Rows.Add(entry.ARTICLE_ID, entry.QUANTITY, entry.SIZE);
                            }
                        }

                        SqlParameter udtParam = new SqlParameter("@UDT_PACKING_ENTRY", SqlDbType.Structured)
                        {
                            TypeName = "UDT_PACKING_ENTRY",
                            Value = packingEntryTable
                        };
                        cmd.Parameters.Add(udtParam);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                res.flag = Convert.ToInt32(dr["flag"]);
                                res.Message = dr["Message"].ToString();
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



        public PackingSelectResponse GetPackingById(int id)
        {
            PackingSelectResponse response = new PackingSelectResponse();

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
                            PackingSelect packing = null;

                            // ✅ Read master data (first result set)
                            if (reader.Read())
                            {
                                packing = new PackingSelect
                                {
                                    ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                    COMPANY_ID = reader.IsDBNull(reader.GetOrdinal("COMPANY_ID")) ? 0 : reader.GetInt32(reader.GetOrdinal("COMPANY_ID")),
                                    ART_NO = reader.IsDBNull(reader.GetOrdinal("ArtNo")) ? null : reader.GetString(reader.GetOrdinal("ArtNo")),
                                    ORDER_NO = reader.IsDBNull(reader.GetOrdinal("OrderNo")) ? null : reader.GetString(reader.GetOrdinal("OrderNo")),
                                    DESCRIPTION = reader.IsDBNull(reader.GetOrdinal("PackingName")) ? null : reader.GetString(reader.GetOrdinal("PackingName")),
                                    COLOR = reader.IsDBNull(reader.GetOrdinal("Color")) ? null : reader.GetString(reader.GetOrdinal("Color")),
                                    PACK_PRICE = reader.IsDBNull(reader.GetOrdinal("PackPrice")) ? 0 : (float)reader.GetDouble(reader.GetOrdinal("PackPrice")),
                                    PAIR_QTY = reader.IsDBNull(reader.GetOrdinal("PairQty")) ? 0 : reader.GetInt32(reader.GetOrdinal("PairQty")),
                                    PART_NO = reader.IsDBNull(reader.GetOrdinal("PartNo")) ? null : reader.GetString(reader.GetOrdinal("PartNo")),
                                    ALIAS_NO = reader.IsDBNull(reader.GetOrdinal("AliasNo")) ? null : reader.GetString(reader.GetOrdinal("AliasNo")),
                                    UNIT_ID = reader.IsDBNull(reader.GetOrdinal("UnitId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UnitId")),
                                    ARTICLE_TYPE = reader.IsDBNull(reader.GetOrdinal("ArticleType")) ? 0 : reader.GetInt32(reader.GetOrdinal("ArticleType")),
                                    CATEGORY_ID = reader.IsDBNull(reader.GetOrdinal("CategoryId")) ? 0 : reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                    BRAND_ID = reader.IsDBNull(reader.GetOrdinal("BrandId")) ? 0 : reader.GetInt32(reader.GetOrdinal("BrandId")),
                                    ART_SERIAL = reader.IsDBNull(reader.GetOrdinal("ArtSerial")) ? null : reader.GetString(reader.GetOrdinal("ArtSerial")),
                                    NEW_ARRIVAL_DAYS = reader.IsDBNull(reader.GetOrdinal("NewArrivalDays")) ? 0 : reader.GetInt32(reader.GetOrdinal("NewArrivalDays")),
                                    IS_STOPPED = reader.IsDBNull(reader.GetOrdinal("IsStopped")) ? false : reader.GetBoolean(reader.GetOrdinal("IsStopped")),
                                    SUPP_ID = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                    COMBINATION = reader.IsDBNull(reader.GetOrdinal("Combination")) ? null : reader.GetString(reader.GetOrdinal("Combination")),
                                    IS_PURCHASABLE = reader.IsDBNull(reader.GetOrdinal("IsPurchasable")) ? false : reader.GetBoolean(reader.GetOrdinal("IsPurchasable")),
                                    IS_EXPORT = reader.IsDBNull(reader.GetOrdinal("IsExport")) ? false : reader.GetBoolean(reader.GetOrdinal("IsExport")),
                                    IS_ANY_COMB = reader.IsDBNull(reader.GetOrdinal("IsAnyComb")) ? false : reader.GetBoolean(reader.GetOrdinal("IsAnyComb")),
                                    IS_INACTIVE = reader.IsDBNull(reader.GetOrdinal("IsInactive")) ? false : reader.GetBoolean(reader.GetOrdinal("IsInactive")),
                                    CreatedDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    COST = reader["Cost"] != DBNull.Value ? Convert.ToSingle(reader["Cost"]) : 0,
                                    PackingEntries = new List<Packing_Entry>() 
                                };
                            }

                            if (reader.NextResult() && packing != null)
                            {
                                while (reader.Read())
                                {
                                    var entry = new Packing_Entry
                                    {
                                        ENTRY_ID = reader["ENTRY_ID"] != DBNull.Value ? Convert.ToInt32(reader["ENTRY_ID"]) : 0,
                                        PACK_ID = reader["PACK_ID"] != DBNull.Value ? Convert.ToInt32(reader["PACK_ID"]) : 0,
                                        ARTICLE_ID = reader["ARTICLE_ID"] != DBNull.Value ? Convert.ToInt32(reader["ARTICLE_ID"]) : 0,
                                        QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0,
                                        SIZE = reader["SIZE"] != DBNull.Value ? Convert.ToString(reader["SIZE"]) : null,
                                        UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0
                                    };
                                    packing.PackingEntries.Add(entry);
                                }
                            }

                            response.Data = packing;
                        }

                        response.flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }



        public PackingListResponses GetPackingList(PackingListReq request)
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
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                packingList.Add(new PackingListItem
                                {
                                    ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(reader.GetOrdinal("ID")),
                                   COMPANY_ID = reader.IsDBNull(reader.GetOrdinal("COMPANY_ID")) ? 0 : reader.GetInt32(reader.GetOrdinal("COMPANY_ID")),
                                    ArtNo = reader.IsDBNull(reader.GetOrdinal("ArtNo")) ? null : reader.GetString(reader.GetOrdinal("ArtNo")),
                                    Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? null : reader.GetString(reader.GetOrdinal("Color")),
                                    OrderNo = reader.IsDBNull(reader.GetOrdinal("OrderNo")) ? null : reader.GetString(reader.GetOrdinal("OrderNo")),
                                    Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                                    PackingName = reader.IsDBNull(reader.GetOrdinal("PackingName")) ? null : reader.GetString(reader.GetOrdinal("PackingName")),
                                    Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(reader.GetOrdinal("Type")),
                                    Brand = reader.IsDBNull(reader.GetOrdinal("Brand")) ? null : reader.GetString(reader.GetOrdinal("Brand")),
                                    UnitId = reader.IsDBNull(reader.GetOrdinal("UnitId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UnitId")),
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

