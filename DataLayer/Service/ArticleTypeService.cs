using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class ArticleTypeService:IArticleTypeService
    {
        public ArticleTypeResponse Insert(ArticleType articleType)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_TYPE";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", articleType.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", articleType.COMPANY_ID);


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
        public ArticleTypeResponse Update(ArticleTypeUpdate articleType)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE_TYPE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", articleType.ID);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", articleType.DESCRIPTION);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", articleType.COMPANY_ID);


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
        public ArticleTypeResponse GetArticleTypeById(int id)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ARTICLE_TYPE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new ArticleTypeUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    COMPANY_ID = Convert.ToInt32(reader["COMPANY_ID"]),
                                    DESCRIPTION = reader["DESCRIPTION"].ToString()
                                  
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
        public ArticleTypeListResponse GetLogList(ArticleTypeListReq request)
        {
            ArticleTypeListResponse res = new ArticleTypeListResponse();
            List<ArticleTypeUpdate> Lstarticletype = new List<ArticleTypeUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_ARTICLE_TYPE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DBNull.Value);
                cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);


                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstarticletype.Add(new ArticleTypeUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                DESCRIPTION = dr["DESCRIPTION"].ToString()                               

                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstarticletype;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }
        public ArticleTypeResponse DeleteArticleTypeData(int id)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ARTICLE_TYPE";

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
