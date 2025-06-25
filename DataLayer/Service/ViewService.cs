using MicroApi.Helper;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.DataLayer.Service
{
    public class ViewService :IViewService
    {
        public ViewResponse GetArticleStockView(int userId)
        {
            ViewResponse res = new ViewResponse
            {
                Data = new List<View>(),
                flag = 0,
                Message = "Failed"
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_ARTICLE_STOCK_VIEW", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                         cmd.Parameters.AddWithValue("@USER_ID", userId);
                        //cmd.Parameters.AddWithValue("@USER_ID", userId ?? (object)DBNull.Value);


                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var stockItem = new View
                                {
                                    ARTICLE_ID = Convert.ToInt32(reader["ARTICLE_ID"]),
                                    ART_NO = reader["ART_NO"]?.ToString(), 
                                    COLOR = reader["COLOR"]?.ToString(),
                                    CATEGORY = reader["CATEGORY"]?.ToString(),
                                    SIZE = Convert.ToInt32(reader["SIZE"] ?? 0),
                                    PRODUCTION_UNIT = reader["PRODUCTION_UNIT"]?.ToString(),
                                    QTY_AVAILABLE = reader["QTY_AVAILABLE"] != DBNull.Value ? Convert.ToInt32(reader["QTY_AVAILABLE"]) : 0,
                                    QTY_MULTIBOX = reader["QTY_MULTIBOX"] != DBNull.Value ? Convert.ToInt32(reader["QTY_MULTIBOX"]) : 0,
                                    QTY_TOTAL = reader["QTY_TOTAL"] != DBNull.Value ? Convert.ToInt32(reader["QTY_TOTAL"]) : 0
                                };

                                res.Data.Add(stockItem);
                            }
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                }
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
