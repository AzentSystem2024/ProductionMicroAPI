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
                    //cmd.Parameters.AddWithValue("@UNIT_ID", request.unitID);
                    //cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

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
                        insertCmd.Parameters.AddWithValue("@ORDER_NO", packing.ORDER_NO ?? (object)DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@ART_NO", packing.ART_NO);
                        insertCmd.Parameters.AddWithValue("@DESCRIPTION", packing.DESCRIPTION);
                        insertCmd.Parameters.AddWithValue("@ITEM_DESCRIPTION", packing.ITEM_DESCRIPTION);
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
                        insertCmd.Parameters.AddWithValue("@STD_PRICE", packing.STD_PRICE);
                        insertCmd.Parameters.AddWithValue("@STD_PRICE_EFFECT_FROM", packing.STD_PRICE_EFFECT_FROM);
                        insertCmd.Parameters.AddWithValue("@STD_PACKING", packing.STANDARD_PACKING);


                        // ---------- Packing Entry ----------
                        DataTable entryTable = new DataTable();
                        entryTable.Columns.Add("ARTICLE_ID", typeof(long));
                        entryTable.Columns.Add("QUANTITY", typeof(int));
                        entryTable.Columns.Add("SIZE", typeof(string));

                        foreach (var entry in packing.PackingEntries)
                        {
                            entryTable.Rows.Add(entry.ARTICLE_ID, entry.QUANTITY, entry.SIZE);
                        }

                        SqlParameter entryParam = insertCmd.Parameters.Add("@UDT_PACKING_ENTRY", SqlDbType.Structured);
                        entryParam.TypeName = "UDT_PACKING_ENTRY";
                        entryParam.Value = entryTable;

                        // ---------- PackingUnits ----------
                        DataTable unitTable = new DataTable();
                        unitTable.Columns.Add("UNIT_ID", typeof(int));

                        if (packing.Units != null && packing.Units.Count > 0)
                        {
                            foreach (var unit in packing.Units)
                            {
                                if (unit.UNIT_ID.HasValue)
                                    unitTable.Rows.Add(unit.UNIT_ID.Value);
                            }
                        }

                        SqlParameter unitParam = insertCmd.Parameters.Add(
                            "@UDT_TB_PACKING_UNITS",
                            SqlDbType.Structured
                        );
                        unitParam.TypeName = "dbo.UDT_TB_PACKING_UNITS";
                        unitParam.Value = unitTable;



                        // ---------- Packing BOM ----------
                        DataTable bomTable = new DataTable();
                        bomTable.Columns.Add("ITEM_ID", typeof(int));
                        bomTable.Columns.Add("QUANTITY", typeof(decimal));

                        foreach (var item in packing.BOM)
                        {
                            bomTable.Rows.Add(item.ITEM_ID, item.QUANTITY);
                        }

                        SqlParameter bomParam = insertCmd.Parameters.Add("@UDT_TB_PACKING_BOM", SqlDbType.Structured);
                        bomParam.TypeName = "dbo.UDT_TB_PACKING_BOM";
                        bomParam.Value = bomTable;

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
                        //cmd.Parameters.AddWithValue("@COMPANY_ID", packing.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@ART_NO", packing.ART_NO);
                        cmd.Parameters.AddWithValue("@ORDER_NO", packing.ORDER_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", packing.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@ITEM_DESCRIPTION", packing.ITEM_DESCRIPTION);
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
                        cmd.Parameters.AddWithValue("@STD_PACKING", packing.STANDARD_PACKING);

                        // ---------- Packing Entry TVP ----------
                        DataTable entryTable = new DataTable();
                        entryTable.Columns.Add("ARTICLE_ID", typeof(long));
                        entryTable.Columns.Add("QUANTITY", typeof(int));
                        entryTable.Columns.Add("SIZE", typeof(string));

                        foreach (var entry in packing.PackingEntries)
                        {
                            entryTable.Rows.Add(entry.ARTICLE_ID, entry.QUANTITY, entry.SIZE);
                        }

                        SqlParameter entryParam = cmd.Parameters.Add("@UDT_PACKING_ENTRY", SqlDbType.Structured);
                        entryParam.TypeName = "UDT_PACKING_ENTRY";
                        entryParam.Value = entryTable;



                        // ---------- Packing BOM TVP ----------
                        DataTable bomTable = new DataTable();
                        bomTable.Columns.Add("ITEM_ID", typeof(int));
                        bomTable.Columns.Add("QUANTITY", typeof(decimal));

                        foreach (var item in packing.BOM)
                        {
                            bomTable.Rows.Add(item.ITEM_ID, item.QUANTITY);
                        }

                        SqlParameter bomParam = cmd.Parameters.Add("@UDT_TB_PACKING_BOM", SqlDbType.Structured);
                        bomParam.TypeName = "dbo.UDT_TB_PACKING_BOM";
                        bomParam.Value = bomTable;

                        // ---------- Packing Units TVP ----------
                        DataTable unitTable = new DataTable();
                        unitTable.Columns.Add("UNIT_ID", typeof(int));

                        if (packing.Units != null && packing.Units.Count > 0)
                        {
                            foreach (var unit in packing.Units)
                            {
                                if (unit.UNIT_ID.HasValue)
                                    unitTable.Rows.Add(unit.UNIT_ID.Value);
                            }
                        }

                        SqlParameter unitParam = cmd.Parameters.Add(
                            "@UDT_TB_PACKING_UNITS",
                            SqlDbType.Structured
                        );
                        unitParam.TypeName = "dbo.UDT_TB_PACKING_UNITS";
                        unitParam.Value = unitTable;


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
                using (SqlConnection connection = ADO.GetConnection())
                {
                   // connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_ManagePackingData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ActionType", SqlDbType.NVarChar, 50).Value = "GetPackingById";
                        cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = id;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            PackingSelect packing = null;

                            // ---------- 1️⃣ MASTER ----------
                            if (reader.Read())
                            {
                                packing = new PackingSelect
                                {
                                    ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                   // COMPANY_ID = reader["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["COMPANY_ID"]),
                                    ART_NO = reader["ArtNo"]?.ToString(),
                                    ORDER_NO = reader["OrderNo"]?.ToString(),
                                    DESCRIPTION = reader["PackingName"]?.ToString(),
                                    COLOR = reader["Color"]?.ToString(),
                                    PACK_PRICE = reader["PackPrice"] == DBNull.Value ? 0 : Convert.ToSingle(reader["PackPrice"]),
                                    PAIR_QTY = reader["PairQty"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PairQty"]),
                                    PART_NO = reader["PartNo"]?.ToString(),
                                    ALIAS_NO = reader["AliasNo"]?.ToString(),
                                    UNIT_ID = reader["UnitId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UnitId"]),
                                    ARTICLE_TYPE = reader["ArticleType"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ArticleType"]),
                                    CATEGORY_ID = reader["CategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CategoryId"]),
                                    BRAND_ID = reader["BrandId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BrandId"]),
                                    ART_SERIAL = reader["ArtSerial"]?.ToString(),
                                    NEW_ARRIVAL_DAYS = reader["NewArrivalDays"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NewArrivalDays"]),
                                    IS_STOPPED = reader["IsStopped"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsStopped"]),
                                    SUPP_ID = reader["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SupplierId"]),
                                    COMBINATION = reader["Combination"]?.ToString(),
                                    IS_PURCHASABLE = reader["IsPurchasable"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPurchasable"]),
                                    IS_EXPORT = reader["IsExport"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsExport"]),
                                    IS_ANY_COMB = reader["IsAnyComb"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsAnyComb"]),
                                    IS_INACTIVE = reader["IsInactive"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsInactive"]),
                                    CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                                    COST = reader["Cost"] == DBNull.Value ? 0 : Convert.ToSingle(reader["Cost"]),
                                    STD_PRICE = reader["STD_PRICE"] != DBNull.Value ? Convert.ToDecimal(reader["STD_PRICE"]) : 0,
                                    STD_PRICE_EFFECT_FROM = reader["STD_PRICE_EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["STD_PRICE_EFFECT_FROM"]) : DateTime.MinValue,
                                    STANDARD_PACKING = reader["STD_PACKING"]?.ToString(),
                                    ITEM_DESCRIPTION = reader["ITEM_DESCRIPTION"]?.ToString(),
                                    PackingEntries = new List<Packing_Entry>(),
                                    BOM = new List<PackingBOM>(),
                                    Units = new List<PackingUnits>()
                                };
                            }

                            // ---------- 2️⃣ PACKING ENTRIES ----------
                            if (reader.NextResult() && packing != null)
                            {
                                while (reader.Read())
                                {
                                    packing.PackingEntries.Add(new Packing_Entry
                                    {
                                        ENTRY_ID = Convert.ToInt32(reader["ENTRY_ID"]),
                                        PACK_ID = Convert.ToInt32(reader["PACK_ID"]),
                                        ARTICLE_ID = Convert.ToInt32(reader["ARTICLE_ID"]),
                                        QUANTITY = Convert.ToSingle(reader["QUANTITY"]),
                                        SIZE = reader["SIZE"]?.ToString(),
                                        UNIT_ID = Convert.ToInt32(reader["UNIT_ID"])
                                    });
                                }
                            }

                            // ---------- 3️⃣ BOM ----------
                            if (reader.NextResult() && packing != null)
                            {
                                while (reader.Read())
                                {
                                    packing.BOM.Add(new PackingBOM
                                    {
                                        BOM_ID = Convert.ToInt32(reader["BOM_ID"]),
                                        PACKING_ID = Convert.ToInt32(reader["PACKING_ID"]),
                                        ITEM_ID = Convert.ToInt32(reader["ITEM_ID"]),
                                        QUANTITY = Convert.ToSingle(reader["QUANTITY"]),
                                        DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                        ITEM_CODE = reader["ITEM_NAME"]?.ToString(),
                                        UOM = reader["UOM"]?.ToString()
                                    });
                                }
                            }
                            // ---------- 4️⃣ PACKING UNITS ----------
                            if (reader.NextResult() && packing != null)
                            {
                                while (reader.Read())
                                {
                                    packing.Units.Add(new PackingUnits
                                    {
                                        UNIT_ID = reader["UNIT_ID"] == DBNull.Value
                                                    ? null
                                                    : Convert.ToInt32(reader["UNIT_ID"])
                                    });
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
                        //cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                packingList.Add(new PackingListItem
                                {
                                    ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(reader.GetOrdinal("ID")),
                                  // COMPANY_ID = reader.IsDBNull(reader.GetOrdinal("COMPANY_ID")) ? 0 : reader.GetInt32(reader.GetOrdinal("COMPANY_ID")),
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
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status")),
                                    STD_PRICE = reader["STD_PRICE"] != DBNull.Value ? Convert.ToDecimal(reader["STD_PRICE"]) : 0,
                                    STD_PRICE_EFFECT_FROM = reader["STD_PRICE_EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["STD_PRICE_EFFECT_FROM"]) : DateTime.MinValue,
                                    STANDARD_PACKING = reader.IsDBNull(reader.GetOrdinal("STD_PACKING")) ? null : reader.GetString(reader.GetOrdinal("STD_PACKING"))
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
        public string GetAliasNo()
        {
            string aliasno = "0";

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT ISNULL(MAX(TRY_CAST(ALIAS_NO AS INT)), 0) + 1 FROM TB_PACKING";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            aliasno = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return aliasno;
        }
        public PackingResponse ChangeStandardPrice(ChangeStandardPriceModel model)
        {
            PackingResponse response = new PackingResponse();

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_ManagePackingData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ActionType", "ChangeStandardPrice");
                    cmd.Parameters.AddWithValue("@ID", model.ID);
                    cmd.Parameters.AddWithValue("@STD_PRICE", model.STD_PRICE);
                    cmd.Parameters.AddWithValue("@STD_PRICE_EFFECT_FROM", model.STD_PRICE_EFFECT_FROM);
                    cmd.Parameters.AddWithValue("@CHANGE_USER_ID", model.CHANGE_USER_ID);

                    //con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            response.flag = Convert.ToInt32(dr["flag"]);
                            response.Message = dr["Message"].ToString();
                        }
                    }
                }
            }

            return response;
        }

        public List<PackingPriceLog> GetPackingPriceLog(PackingPriceLogrequest request)
        {
            List<PackingPriceLog> list = new List<PackingPriceLog>();

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_ManagePackingData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ActionType", "GetPackingPriceLog");
                    cmd.Parameters.AddWithValue("@ID", request.PACKING_ID);

                    //con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new PackingPriceLog
                            {
                                STD_PRICE = dr["STD_PRICE"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["STD_PRICE"])
                                    : 0,

                                STD_PRICE_EFFECT_FROM = dr["EFFECT_FROM"] != DBNull.Value
                                    ? Convert.ToDateTime(dr["EFFECT_FROM"])
                                    : DateTime.MinValue,

                                CREATED_TIME = dr["CREATED_TIME"] != DBNull.Value
                                    ? Convert.ToDateTime(dr["CREATED_TIME"])
                                    : DateTime.MinValue,

                                CREATED_USER_ID = dr["CREATED_USER_ID"] != DBNull.Value
                                    ? Convert.ToInt32(dr["CREATED_USER_ID"])
                                    : 0,
                                USER_NAME = dr["USER_NAME"] != DBNull.Value
                                    ? Convert.ToString(dr["USER_NAME"])
                                    : null
                            });
                        }
                    }
                }
            }

            return list;
        }

    }
}

