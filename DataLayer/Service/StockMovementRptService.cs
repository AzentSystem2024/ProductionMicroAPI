using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class StockMovementRptService:IStockMovementRptService
    {
        public StockMovementRptResponse GetStockMovementReport(StockMovementRequest request)
        {
            var response = new StockMovementRptResponse
            {
                data = new List<StockMovementRpt>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    //connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_RPT_STOCK_MOVEMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                        //cmd.Parameters.Add("@STORE_ID", SqlDbType.Int).Value = request.STORE_ID;
                        cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = request.DATE_FROM;
                        cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = request.DATE_TO;
                        cmd.Parameters.Add("@ITEM_ID", SqlDbType.Int).Value = request.ITEM_ID ?? 0;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var rpt = new StockMovementRpt
                                {
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                    ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                    ITEM_NAME = reader["ITEM_NAME"]?.ToString(),
                                    MATRIX_CODE = reader["MATRIX_CODE"]?.ToString(),
                                    COLOR = reader["COLOR"]?.ToString(),
                                    SIZE = reader["SIZE"]?.ToString(),
                                    STYLE = reader["STYLE"]?.ToString(),
                                    OPENING_QTY = reader["OPENING_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["OPENING_QTY"]) : 0,
                                    GRN_QTY = reader["GRN_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["GRN_QTY"]) : 0,
                                    PURCHASE_RETURN_QTY = reader["PURCHASE_RETURN_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["PURCHASE_RETURN_QTY"]) : 0,
                                    TRANSFEROUT_QTY = reader["TRANSFER_OUT_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["TRANSFER_OUT_QTY"]) : 0,
                                    TRANSFERIN_QTY = reader["TRANSFER_IN_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["TRANSFER_IN_QTY"]) : 0,
                                    DELIVERY_QTY = reader["DELIVERY_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["DELIVERY_QTY"]) : 0,
                                    DELIVERY_RETURN_QTY = reader["DELIVERY_RETURN_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["DELIVERY_RETURN_QTY"]) : 0,
                                    SALE_QTY = reader["SALE_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["SALE_QTY"]) : 0,
                                    SALE_RETURN_QTY = reader["SALE_RETURN_QTY"] != DBNull.Value ? Convert.ToDecimal(reader["SALE_RETURN_QTY"]) : 0,
                                    ADJUSTED = reader["ADJUSTED"] != DBNull.Value ? Convert.ToDecimal(reader["ADJUSTED"]) : 0,
                                    BALANCE_STOCK = reader["BALANCE_STOCK"] != DBNull.Value ? Convert.ToDecimal(reader["BALANCE_STOCK"]) : 0
                                };
                                response.data.Add(rpt);
                            }
                        }
                    }
                }

                response.flag = 1;
                response.message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = "Error fetching Stock Movement Report: " + ex.Message;
            }

            return response;
        }
    }
}
