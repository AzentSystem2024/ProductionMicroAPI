using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ArticleColorService:IArticleColorService
    {
        public ArticleColorResponse Insert(ArticleColor articleColor)
        {
            ArticleColorResponse res = new ArticleColorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_COLOR";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@CODE", articleColor.CODE);
                        cmd.Parameters.AddWithValue("@COLOR_ENGLISH", articleColor.COLOR_ENGLISH);
                        cmd.Parameters.AddWithValue("@COLOR_ARABIC", articleColor.COLOR_ARABIC);

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
        public ArticleColorResponse Update(ArticleColorUpdate articleColor)
        {
            ArticleColorResponse res = new ArticleColorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE_COLOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", articleColor.ID);
                        cmd.Parameters.AddWithValue("@CODE", articleColor.CODE);
                        cmd.Parameters.AddWithValue("@COLOR_ENGLISH", articleColor.COLOR_ENGLISH);
                        cmd.Parameters.AddWithValue("@COLOR_ARABIC", articleColor.COLOR_ARABIC);
                        
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
        public ArticleColorResponse GetArticleColorById(int id)
        {
            ArticleColorResponse res = new ArticleColorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE_COLOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); 
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new ArticleColorUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CODE = reader["CODE"].ToString(),
                                    COLOR_ENGLISH = reader["COLOR_ENGLISH"].ToString(),
                                    COLOR_ARABIC = reader["COLOR_ARABIC"].ToString()
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
        public ArticleColorListResponse GetLogList(int? id = null)
        {
            ArticleColorListResponse res = new ArticleColorListResponse();
            List<ArticleColorUpdate> Lstarticlecolor = new List<ArticleColorUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_COLOR", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", (object?)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CODE", DBNull.Value);
                cmd.Parameters.AddWithValue("@COLOR_ENGLISH", DBNull.Value);
                cmd.Parameters.AddWithValue("@COLOR_ARABIC", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstarticlecolor.Add(new ArticleColorUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                CODE = dr["CODE"].ToString(),
                                COLOR_ENGLISH = dr["COLOR_ENGLISH"].ToString(),
                                COLOR_ARABIC = dr["COLOR_ARABIC"].ToString()

                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstarticlecolor;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }
        public ArticleColorResponse DeleteArticleColorData(int id)
        {
            ArticleColorResponse res = new ArticleColorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_COLOR";

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
