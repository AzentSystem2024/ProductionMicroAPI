using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ItemStockRptService:IItemStockRptService
    {
        public ItemStockViewResponse GetItemStockView(ItemStockRptRequest request)
        {
            ItemStockViewResponse response = new ItemStockViewResponse();
            List<ItemStockRpt> list = new List<ItemStockRpt>();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_ITEM_STOCK_VIEW", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID",request.COMPANY_ID );

                    //con.Open(); 

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ItemStockRpt item = new ItemStockRpt
                            {
                                ITEM_CODE = dr["ITEM_CODE"]?.ToString(),
                                ITEM_DESCRIPTION = dr["ITEM_DESCRIPTION"]?.ToString(),
                                DEPARTMENT = dr["DEPARTMENT"]?.ToString(),
                                CATEGORY = dr["CATEGORY"]?.ToString(),
                                SUB_CATEGORY = dr["SUB_CATEGORY"]?.ToString(),
                                BRAND = dr["BRAND"]?.ToString(),
                                CURRENT_STOCK = dr["CURRENT_STOCK"] == DBNull.Value ? 0 : Convert.ToDouble(dr["CURRENT_STOCK"])
                            };

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
                response.Message = ex.Message;
                response.Data = new List<ItemStockRpt>();
            }

            return response;
        }

    }
}
