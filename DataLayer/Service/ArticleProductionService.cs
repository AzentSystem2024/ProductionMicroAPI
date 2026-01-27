using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ArticleProductionService:IArticleProductionService
    {
        public ArticleProdResponse Insert(ArticleProduction model)
        {
            ArticleProdResponse response = new ArticleProdResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_PRODUCTION_ENTRY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ===== Header Parameters =====
                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@ADDL_COST", model.ADDL_COST);
                    cmd.Parameters.AddWithValue("@ADDL_DESCRIPTION", model.REMARKS ?? "");
                    cmd.Parameters.AddWithValue("@PRODUCT_ID", model.PRODUCT_ID);
                    cmd.Parameters.AddWithValue("@PROD_QTY", model.PROD_QTY);
                    cmd.Parameters.AddWithValue("@PRODUCTION_DATE", model.PRODUCTION_DATE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ITEM_COST", model.TOTAL_ITEM_COST);
                    cmd.Parameters.AddWithValue("@UNIT_COST", model.UNIT_PRODUCT_COST);
                    cmd.Parameters.AddWithValue("@PRODUCTION_TYPE", model.PRODUCTION_TYPE);

                    // ===== RAW MATERIALS UDT =====
                    DataTable dtRaw = new DataTable();
                    dtRaw.Columns.Add("ITEM_ID", typeof(int));
                    dtRaw.Columns.Add("UOM", typeof(string));
                    dtRaw.Columns.Add("BOM_QTY", typeof(float));
                    dtRaw.Columns.Add("REQUIRED_QTY", typeof(float));
                    dtRaw.Columns.Add("USED_QTY", typeof(float));
                    dtRaw.Columns.Add("UNIT_COST", typeof(float));
                    dtRaw.Columns.Add("TOTAL_COST", typeof(float));
                    //dtRaw.Columns.Add("QTY_AVAILABLE", typeof(float));


                    if (model.RawMaterials != null && model.RawMaterials.Count > 0)
                    {
                        foreach (var r in model.RawMaterials)
                        {
                            dtRaw.Rows.Add(
                                r.ID,
                                r.UOM ?? "",
                                r.QUANTITY,
                                r.REQUIRED_QTY,
                                r.USED_QTY,
                                r.COST,
                                r.AMOUNT
                            );
                        }
                    }

                    SqlParameter pRaw = cmd.Parameters.AddWithValue("@UDT_PRODUCTION_DETAIL", dtRaw);
                    pRaw.SqlDbType = SqlDbType.Structured;
                    pRaw.TypeName = "UDT_PRODUCTION_DETAIL";

                   // con.Open();
                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Article Production saved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error : " + ex.Message;
            }

            return response;
        }
        public ArticleProdResponse Update(ArticleProductionUpdate model)
        {
            ArticleProdResponse response = new ArticleProdResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_PRODUCTION_ENTRY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ===== Header Parameters =====
                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@ID", model.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@ADDL_COST", model.ADDL_COST);
                    cmd.Parameters.AddWithValue("@ADDL_DESCRIPTION", model.REMARKS ?? "");
                    cmd.Parameters.AddWithValue("@PRODUCT_ID", model.PRODUCT_ID);
                    cmd.Parameters.AddWithValue("@PROD_QTY", model.PROD_QTY);
                    cmd.Parameters.AddWithValue("@PRODUCTION_DATE", model.PRODUCTION_DATE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ITEM_COST", model.TOTAL_ITEM_COST);
                    cmd.Parameters.AddWithValue("@UNIT_COST", model.UNIT_PRODUCT_COST);
                    cmd.Parameters.AddWithValue("@PRODUCTION_TYPE", model.PRODUCTION_TYPE);

                    // ===== RAW MATERIALS UDT =====
                    DataTable dtRaw = new DataTable();
                    dtRaw.Columns.Add("ITEM_ID", typeof(int));
                    dtRaw.Columns.Add("UOM", typeof(string));
                    dtRaw.Columns.Add("BOM_QTY", typeof(float));
                    dtRaw.Columns.Add("REQUIRED_QTY", typeof(float));
                    dtRaw.Columns.Add("USED_QTY", typeof(float));
                    dtRaw.Columns.Add("UNIT_COST", typeof(float));
                    dtRaw.Columns.Add("TOTAL_COST", typeof(float));

                    if (model.RawMaterials != null && model.RawMaterials.Count > 0)
                    {
                        foreach (var r in model.RawMaterials)
                        {
                            dtRaw.Rows.Add(
                                r.ID,
                                r.UOM ?? "",
                                r.QUANTITY,
                                r.REQUIRED_QTY,
                                r.USED_QTY,
                                r.COST,
                                r.AMOUNT
                            );
                        }
                    }

                    SqlParameter pRaw = cmd.Parameters.AddWithValue("@UDT_PRODUCTION_DETAIL", dtRaw);
                    pRaw.SqlDbType = SqlDbType.Structured;
                    pRaw.TypeName = "UDT_PRODUCTION_DETAIL";

                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Article Production updated successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public ArticleProdResponse commit(ArticleProductionUpdate model)
        {
            ArticleProdResponse response = new ArticleProdResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_PRODUCTION_ENTRY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ===== Header Parameters =====
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@ID", model.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                    cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? "");
                    cmd.Parameters.AddWithValue("@ADDL_COST", model.ADDL_COST);
                    cmd.Parameters.AddWithValue("@ADDL_DESCRIPTION", model.REMARKS ?? "");
                    cmd.Parameters.AddWithValue("@PRODUCT_ID", model.PRODUCT_ID);
                    cmd.Parameters.AddWithValue("@PROD_QTY", model.PROD_QTY);
                    cmd.Parameters.AddWithValue("@PRODUCTION_DATE", model.PRODUCTION_DATE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ITEM_COST", model.TOTAL_ITEM_COST);
                    cmd.Parameters.AddWithValue("@UNIT_COST", model.UNIT_PRODUCT_COST);
                    cmd.Parameters.AddWithValue("@PRODUCTION_TYPE", model.PRODUCTION_TYPE);

                    // ===== RAW MATERIALS UDT =====
                    DataTable dtRaw = new DataTable();
                    dtRaw.Columns.Add("ITEM_ID", typeof(int));
                    dtRaw.Columns.Add("UOM", typeof(string));
                    dtRaw.Columns.Add("BOM_QTY", typeof(float));
                    dtRaw.Columns.Add("REQUIRED_QTY", typeof(float));
                    dtRaw.Columns.Add("USED_QTY", typeof(float));
                    dtRaw.Columns.Add("UNIT_COST", typeof(float));
                    dtRaw.Columns.Add("TOTAL_COST", typeof(float));

                    if (model.RawMaterials != null && model.RawMaterials.Count > 0)
                    {
                        foreach (var r in model.RawMaterials)
                        {
                            dtRaw.Rows.Add(
                                r.ID,
                                r.UOM ?? "",
                                r.QUANTITY,
                                r.REQUIRED_QTY,
                                r.USED_QTY,
                                r.COST,
                                r.AMOUNT
                            );
                        }
                    }

                    SqlParameter pRaw = cmd.Parameters.AddWithValue("@UDT_PRODUCTION_DETAIL", dtRaw);
                    pRaw.SqlDbType = SqlDbType.Structured;
                    pRaw.TypeName = "UDT_PRODUCTION_DETAIL";

                    cmd.ExecuteNonQuery();

                    response.Flag = 1;
                    response.Message = "Article Production committed successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public ArticleProdResponse Delete(int id)
        {
            ArticleProdResponse res = new ArticleProdResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_PRODUCTION_ENTRY";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

        public ArticleBomResponse GetArticleBomList(ArticleBomRequest model)
        {
            ArticleBomResponse res = new ArticleBomResponse
            {
                Data = new List<ArticleBomItem>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_PRODUCTION_ENTRY", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@ARTICLEID", model.ARTICLE_ID);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleBomItem item = new ArticleBomItem
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                UOM = dr["UOM"]?.ToString(),
                                QUANTITY = dr["QUANTITY"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["QUANTITY"]),
                                COST = dr["COST"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["COST"]),
                                QTY_AVAILABLE = dr["QTY_STOCK"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["QTY_STOCK"])
                            };

                            res.Data.Add(item);
                        }
                    }

                    res.Flag = 1;
                    res.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "ERROR: " + ex.Message;
            }

            return res;
        }

        public ProductionViewResponse GetProductionById(int id)
        {
            var response = new ProductionViewResponse
            {
                RawMaterials = new List<ProductionRawMaterial>(),
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_PRODUCTION_ENTRY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    cmd.Parameters.AddWithValue("@ID", id);

                    // con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // ===== Header =====
                        if (dr.Read())
                        {
                            response.Header = new ProductionHeader
                            {
                                PRODUCTION_ID = Convert.ToInt64(dr["PRODUCTION_ID"]),
                                PROD_NO = dr["PROD_NO"]?.ToString(),
                                PROD_DATE = dr["PROD_DATE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["PROD_DATE"]),
                                PRODUCT_ID = Convert.ToInt32(dr["PRODUCT_ID"]),
                                PRODUCED_QTY = Convert.ToDecimal(dr["PRODUCED_QTY"]),
                                TOTAL_COST = Convert.ToDecimal(dr["TOTAL_COST"]),
                                UNIT_COST = Convert.ToDecimal(dr["UNIT_COST"]),
                                ADDL_COST = Convert.ToDecimal(dr["ADDL_COST"]),
                                REMARKS = dr["ADDL_DESCRIPTION"]?.ToString(),
                                TRANS_ID = Convert.ToInt64(dr["TRANS_ID"]),
                                VOUCHER_NO = dr["VOUCHER_NO"]?.ToString(),
                                REF_NO = dr["REF_NO"]?.ToString(),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                COST_PRODUCTION = Convert.ToDecimal(dr["COST_PRODUCTION"]),
                            };
                        }

                        // ===== Raw Materials =====
                        dr.NextResult();
                        while (dr.Read())
                        {
                            response.RawMaterials.Add(new ProductionRawMaterial
                            {
                                ID = Convert.ToInt64(dr["ID"]),
                                ITEM_ID = Convert.ToInt32(dr["ITEM_ID"]),
                                DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                                UOM = dr["UOM"]?.ToString(),
                                BOM_QTY = Convert.ToDecimal(dr["BOM_QTY"]),
                                REQUIRED_QTY = Convert.ToDecimal(dr["REQUIRED_QTY"]),
                                USED_QTY = Convert.ToDecimal(dr["USED_QTY"]),
                                UNIT_COST = Convert.ToDecimal(dr["UNIT_COST"]),
                                TOTAL_COST = Convert.ToDecimal(dr["TOTAL_COST"]),
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                            });
                        }

                       
                    }
                }

                response.Flag = 1;
                response.Message = "Production data fetched successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public ProductionListResponse articleprodlist(ProductionListRequest model)
        {
            ProductionListResponse response = new ProductionListResponse();
            List<ProductionListItem> list = new List<ProductionListItem>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_GET_PRODUCTION_LIST", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductionListItem item = new ProductionListItem();

                            item.PRODUCTION_ID = dr["PRODUCTION_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["PRODUCTION_ID"]);
                            item.PROD_NO = dr["PROD_NO"] == DBNull.Value ? "" : dr["PROD_NO"].ToString();
                            item.PROD_DATE = dr["PROD_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["PROD_DATE"]);
                            item.PRODUCED_QTY = dr["PRODUCED_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PRODUCED_QTY"]);
                            item.TOTAL_COST = dr["TOTAL_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TOTAL_COST"]);
                            item.TRANS_ID = dr["TRANS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["TRANS_ID"]);
                            item.VOUCHER_NO = dr["VOUCHER_NO"] == DBNull.Value ? "" : dr["VOUCHER_NO"].ToString();
                            item.ITEM_CODE = dr["ITEM_CODE"] == DBNull.Value ? "" : dr["ITEM_CODE"].ToString();
                            item.DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? "" : dr["DESCRIPTION"].ToString();


                            list.Add(item);
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR : " + ex.Message;
                response.Data = new List<ProductionListItem>();
            }

            return response;
        }

    }
}
