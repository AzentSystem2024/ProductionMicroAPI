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
                    TenderSummary = new List<TenderSummaryStoreData>(),

                    ProfitLoss = new ProfitLossSummary
                    {
                        Revenue = new List<RevenueData>(),
                        Expense = new List<ExpenseData>()
                    }
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
                            /* ============================================
                               4. REVENUE
                               ============================================ */

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    int storeId = reader["STORE_ID"] != DBNull.Value
                                        ? Convert.ToInt32(reader["STORE_ID"])
                                        : 0;

                                    string store = reader["STORE"]?.ToString();

                                    decimal revenue = reader["REVENUE"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["REVENUE"])
                                        : 0;

                                    decimal expense = reader["EXPENSE"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["EXPENSE"])
                                        : 0;

                                    if (revenue != 0)
                                    {
                                        response.data.ProfitLoss.Revenue.Add(new RevenueData
                                        {
                                            STORE_ID = storeId,
                                            STORE = store,
                                            REVENUE = revenue
                                        });
                                    }

                                    if (expense != 0)
                                    {
                                        response.data.ProfitLoss.Expense.Add(new ExpenseData
                                        {
                                            STORE_ID = storeId,
                                            STORE = store,
                                            EXPENSE = expense
                                        });
                                    }
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
        public DashboarddataResponse GetDashboard(DashboardRequest request)
        {
            DashboarddataResponse response = new DashboarddataResponse();

            response.flag = 1;
            response.Message = "Success";

            response.data = new GetDashboardData
            {
                TopMovingArticles = new List<TopMovingArticle>(),
                LastMonthSales = new List<LastMonthSale>(),
                RegionWiseSales = new List<RegionWiseSale>(),
                SalesmanWiseSales = new List<SalesmanWiseSale>()
            };

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_GET_DASHBOARD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DATE_FROM",
                        (object)request.DATE_FROM ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@DATE_TO",
                        (object)request.DATE_TO ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@COMPANY_ID",
                        (object)request.COMPANY_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@FIN_ID",
                        (object)request.FIN_ID ?? DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Top Moving Articles
                        while (dr.Read())
                        {
                            response.data.TopMovingArticles.Add(new TopMovingArticle
                            {
                                ART_NO = dr["ART_NO"].ToString(),
                                ARTICLE_NAME = dr["ARTICLE_NAME"].ToString(),
                                TOTAL_QTY = Convert.ToDecimal(dr["TOTAL_QTY"]),
                                TOTAL_TRANSACTIONS = Convert.ToInt32(dr["TOTAL_TRANSACTIONS"])
                            });
                        }

                        // Last Month Sales
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                response.data.LastMonthSales.Add(new LastMonthSale
                                {
                                    SALE_NO = dr["SALE_NO"].ToString(),
                                    SALE_DATE = Convert.ToDateTime(dr["SALE_DATE"]),
                                    ITEM_CODE = dr["ITEM_CODE"].ToString(),
                                    ITEM_NAME = dr["ITEM_NAME"].ToString(),
                                    QUANTITY = Convert.ToDecimal(dr["QUANTITY"]),
                                    PRICE = Convert.ToDecimal(dr["PRICE"]),
                                    NET_AMOUNT = Convert.ToDecimal(dr["NET_AMOUNT"])
                                });
                            }
                        }

                        // Region Wise Sales
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                response.data.RegionWiseSales.Add(new RegionWiseSale
                                {
                                    STATE_NAME = dr["STATE_NAME"].ToString(),
                                    SALE_NO = dr["SALE_NO"].ToString(),
                                    SALE_DATE = Convert.ToDateTime(dr["SALE_DATE"]),
                                    TOTAL_INVOICES = Convert.ToInt32(dr["TOTAL_INVOICES"]),
                                    TOTAL_SALES = Convert.ToDecimal(dr["TOTAL_SALES"]),
                                    GROSS_AMOUNT = Convert.ToDecimal(dr["GROSS_AMOUNT"]),
                                    TAX_AMOUNT = Convert.ToDecimal(dr["TAX_AMOUNT"])
                                });
                            }
                        }

                        // Salesman Wise Sales
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                response.data.SalesmanWiseSales.Add(new SalesmanWiseSale
                                {
                                    SALESMAN_ID = Convert.ToInt32(dr["SALESMAN_ID"]),
                                    SALESMAN_NAME = dr["SALESMAN_NAME"].ToString(),
                                    TOTAL_INVOICES = Convert.ToInt32(dr["TOTAL_INVOICES"]),
                                    TOTAL_SALES = Convert.ToDecimal(dr["TOTAL_SALES"])
                                });
                            }
                        }
                    }
                }
            }

            return response;
        }
    }
}
