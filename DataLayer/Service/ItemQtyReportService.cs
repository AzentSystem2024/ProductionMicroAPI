using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ItemQtyReportService : IItemQtyReportService
    {
        public ItemQuantityReportResponse GetItemQuantityReport(ItemQuantityReportRequest request)
        {
            ItemQuantityReportResponse response = new ItemQuantityReportResponse
            {
                ItemQuantityDetails = new List<ItemQtyReport>()
            };
            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_ITEM_QUTY_REPORT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ASONDATE", request.ASONDATE);
                    cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.ItemQuantityDetails.Add(new ItemQtyReport
                            {
                                ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                MATRIXCODE = reader["MATRIXCODE"] != DBNull.Value ? reader["MATRIXCODE"].ToString() : string.Empty,
                                ITEMCODE = reader["ITEMCODE"] != DBNull.Value ? reader["ITEMCODE"].ToString() : string.Empty,
                                DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : string.Empty,
                                DEPARTMENT = reader["DEPARTMENT"] != DBNull.Value ? reader["DEPARTMENT"].ToString() : string.Empty,
                                CATEGORY = reader["CATEGORY"] != DBNull.Value ? reader["CATEGORY"].ToString() : string.Empty,
                                SUBCATEGORY = reader["SUBCATEGORY"] != DBNull.Value ? reader["SUBCATEGORY"].ToString() : string.Empty,
                                BRAND = reader["BRAND"] != DBNull.Value ? reader["BRAND"].ToString() : string.Empty,
                                QUANTITY_AVAILABLE = reader["QUANTITY_AVAILABLE"] != DBNull.Value ? Convert.ToDecimal(reader["QUANTITY_AVAILABLE"]) : 0
                            });
                        }
                    }
                }
            }

            response.Flag = (response.ItemQuantityDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";
            return response;
        }
    }
}
