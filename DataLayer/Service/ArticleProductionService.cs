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
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ARTICLE_PRODUCTION_UPLOAD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID);
                        cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);

                        // Prepare table-valued parameter
                        DataTable dtUDT = new DataTable();
                        
                        dtUDT.Columns.Add("ARTICLE_ID", typeof(long));
                        dtUDT.Columns.Add("ARTICLE_PRODUCTION_ID", typeof(long));
                        dtUDT.Columns.Add("PAIRS", typeof(int));
                        dtUDT.Columns.Add("BOX_ID", typeof(int));
                        dtUDT.Columns.Add("BARCODE", typeof(string));
                        dtUDT.Columns.Add("PRICE", typeof(float));

                        if (model.Articles != null && model.Articles.Count > 0)
                        {
                            foreach (var item in model.Articles)
                            {
                                dtUDT.Rows.Add(item.ARTICLE_ID, item.ARTICLE_PRODUCTION_ID,item.PAIRS,item.BOX_ID,item.BARCODE,item.PRICE);
                            }
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_ARTICLE_PRODUCTION", dtUDT);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_ARTICLE_PRODUCTION";

                        cmd.ExecuteNonQuery();

                        response.Flag = 1;
                        response.Message = "Article production uploaded successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }

    }
}
