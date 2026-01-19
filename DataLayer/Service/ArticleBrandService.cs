using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class ArticleBrandService:IArticleBrandService
    {
        public ArticleBrandResponse Insert(ArticleBrand articleBrand)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_BRAND";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@CODE", articleBrand.CODE);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", articleBrand.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", articleBrand.IS_INACTIVE);
                        //cmd.Parameters.AddWithValue("@COMPANY_ID", articleBrand.COMPANY_ID);

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
                        return res;
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
        public ArticleBrandResponse Update(ArticleBrandUpdate articleBrand)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE_BRAND", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", articleBrand.ID);
                        cmd.Parameters.AddWithValue("@CODE", articleBrand.CODE);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", articleBrand.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", articleBrand.IS_INACTIVE);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                            return res;
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
                res.Message = ex.Message;
            }

            return res;
        }
        public ArticleBrandResponse GetArticleBrandById(int id)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE_BRAND", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new ArticleBrandUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CODE = reader["CODE"].ToString(),
                                    DESCRIPTION = reader["DESCRIPTION"].ToString(),
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"]),
                                };
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Failed";
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
        public ArticleBrandListResponse GetLogList()
        {
            ArticleBrandListResponse res = new ArticleBrandListResponse();
            List<ArticleBrandUpdate> Lstarticlebrand = new List<ArticleBrandUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_BRAND", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@CODE", DBNull.Value);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstarticlebrand.Add(new ArticleBrandUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                CODE = dr["CODE"].ToString(),
                                DESCRIPTION = dr["DESCRIPTION"].ToString(),
                                IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])

                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstarticlebrand;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }
        public ArticleBrandResponse DeleteArticleBrandData(int id)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_BRAND";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
