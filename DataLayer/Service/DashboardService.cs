using MicroApi.Models;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Globalization;
using System.Linq;
using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace MicroApi.DataLayer.Service
{
    public class DashboardService:IDashboardService
    {
        public DashboardResponse GetDashboardSummary(DashboardRequest request)
        {
            var response = new DashboardResponse
            {
                flag = 0,
                Message = "Failed",
                data = new DashboardData
                {
                    GrossSale = new List<GrossSaleData>(),
                    TopMovingItems = new List<TopMovingItemData>(),
                    TenderSummary = new List<TenderSummaryStoreData>()
                }
            };

            try
            {
                using (var conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = new SqlCommand("SP_DASHBOARD_SUMMARY", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);

                        cmd.Parameters.AddWithValue("@DATE_FROM",
                            request.DATE_FROM == null
                            ? (object)DBNull.Value
                            : Convert.ToDateTime(request.DATE_FROM));

                        cmd.Parameters.AddWithValue("@DATE_TO",
                            request.DATE_TO == null
                            ? (object)DBNull.Value
                            : Convert.ToDateTime(request.DATE_TO));

                       

                        using (var reader = cmd.ExecuteReader())
                        {
                            /* ============================================
                               1. GROSS SALE
                               ============================================ */

                            while (reader.Read())
                            {
                                response.data.GrossSale.Add(new GrossSaleData
                                {
                                    STORE_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]): 0,
                                    STORE_NAME = reader["STORE"]?.ToString(),
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["AMOUNT"]): 0
                                });
                            }

                            /* ============================================
                               2. TOP MOVING ITEMS
                               ============================================ */

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    response.data.TopMovingItems.Add(new TopMovingItemData
                                    {
                                        ITEM_ID = reader["ITEM"] != DBNull.Value
                                            ? Convert.ToInt32(reader["ITEM"])
                                            : 0,
                                        ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value
                                            ? Convert.ToString(reader["ITEM_CODE"])
                                            : null,
                                        DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value
                                            ? Convert.ToString(reader["DESCRIPTION"])
                                            : null,

                                        QTY_SOLD = reader["QTY_SOLD"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["QTY_SOLD"])
                                            : 0,

                                        TIMES_SOLD = reader["TIMES_SOLD"] != DBNull.Value
                                            ? Convert.ToInt32(reader["TIMES_SOLD"])
                                            : 0,

                                        TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["TOTAL_AMOUNT"])
                                            : 0
                                    });
                                }
                            }

                            /* ============================================
                               3. TENDER SUMMARY
                               ============================================ */

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    int storeId = reader["STORE_ID"] != DBNull.Value
                                        ? Convert.ToInt32(reader["STORE_ID"])
                                        : 0;

                                    string storeName = reader["STORE"]?.ToString();

                                    var existingStore = response.data.TenderSummary
                                        .FirstOrDefault(x => x.STORE_ID == storeId);

                                    if (existingStore == null)
                                    {
                                        existingStore = new TenderSummaryStoreData
                                        {
                                            STORE_ID = storeId,
                                            STORE_NAME = storeName,
                                            TenderTypes = new List<TenderTypeData>()
                                        };

                                        response.data.TenderSummary.Add(existingStore);
                                    }

                                    existingStore.TenderTypes.Add(new TenderTypeData
                                    {
                                        TENDER = reader["TENDER"]?.ToString(),

                                        AMOUNT = reader["AMOUNT"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["AMOUNT"])
                                            : 0
                                    });
                                }
                            }
                        }

                        response.flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
    }
}
