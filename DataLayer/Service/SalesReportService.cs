using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SalesReportService : ISalesReportService
    {
        public DataTable GetSalesSummary(SalesSummaryFilter filter)
        {
            DataTable dt = new DataTable();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_SALE_SUMMARY_NEW", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = filter.COMPANY_ID;
                        cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = filter.FIN_ID;

                        cmd.Parameters.AddWithValue("@DATE_FROM", filter.DATE_FROM);
                        cmd.Parameters.AddWithValue("@DATE_TO", filter.DATE_TO);
                        cmd.Parameters.AddWithValue("@STORE_ID", filter.STORE_ID ?? "");
                        cmd.Parameters.AddWithValue("@CUST_ID", filter.CUST_ID ?? "");
                        cmd.Parameters.AddWithValue("@SALE_TYPE", filter.SALE_TYPE);
                        cmd.Parameters.AddWithValue("@INCLUDE_SUMMARY", filter.INCLUDE_SUMMARY);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching Sales Summary: " + ex.Message);
            }

            return dt;
        }

        public DataTable GetSalesDetails(SalesDetailFilter filter)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_RPT_SALE_DETAIL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // 🔥 REQUIRED PARAMS
                cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = filter.COMPANY_ID;
                cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = filter.FIN_ID;

                cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime)
                    .Value = Convert.ToDateTime(filter.DATE_FROM);

                cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime)
                    .Value = Convert.ToDateTime(filter.DATE_TO);

                // 🔥 OPTIONAL PARAMS (VERY IMPORTANT FIX)
                cmd.Parameters.Add("@STORE_ID", SqlDbType.VarChar)
                    .Value = string.IsNullOrEmpty(filter.STORE_ID) ? "" : filter.STORE_ID;

                cmd.Parameters.Add("@SALE_TYPE", SqlDbType.Int)
                    .Value = filter.SALE_TYPE;

                cmd.Parameters.Add("@CUST_ID", SqlDbType.NVarChar)
                    .Value = filter.CUSTOMER_ID ?? "";

                cmd.Parameters.Add("@SALESMAN_ID", SqlDbType.NVarChar)
                    .Value = filter.SALESMAN_ID ?? "";

                cmd.Parameters.Add("@DEPT_ID", SqlDbType.NVarChar)
                    .Value = filter.DEPT_ID ?? "";

                cmd.Parameters.Add("@CAT_ID", SqlDbType.NVarChar)
                    .Value = filter.CAT_ID ?? "";

                cmd.Parameters.Add("@SUBCAT_ID", SqlDbType.NVarChar)
                    .Value = filter.SUBCAT_ID ?? "";

                cmd.Parameters.Add("@BRAND_ID", SqlDbType.NVarChar)
                    .Value = filter.BRAND_ID ?? "";

                cmd.Parameters.Add("@CUSTOM1", SqlDbType.NVarChar)
                    .Value = filter.CUSTOM1 ?? "";

                cmd.Parameters.Add("@CUSTOM2", SqlDbType.NVarChar)
                    .Value = filter.CUSTOM2 ?? "";

                cmd.Parameters.Add("@ITEM_ID", SqlDbType.NVarChar)
                    .Value = filter.ITEM_ID ?? "";

                cmd.Parameters.Add("@DISCOUNTED_ITEMS_ONLY", SqlDbType.Int)
                    .Value = filter.DISCOUNTED_ITEMS_ONLY;

                cmd.Parameters.Add("@INCLUDE_SUMMARY", SqlDbType.Int)
                    .Value = filter.INCLUDE_SUMMARY;

                // con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        public ConsignmentSummaryResponse GetConsignmentSummary(ConsignmentSummaryFilter request)
        {
            var response = new ConsignmentSummaryResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_ConsignmentSummary", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@CompanyID", request.COMPANY_ID);
                cmd.Parameters.AddWithValue("@BrandID", request.BRAND_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CustID", request.CUSTOMER_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateFrom", request.DATE_FROM ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateTo", request.DATE_TO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StatusID", request.STATUS_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StoreID", request.STORE_ID);

                using SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    response.data.Add(new ConsignmentSummaryItem
                    {
                        CONSIGNMENT_ID = Convert.ToInt32(rdr["ID"]),
                        TOTAL_AMOUNT = Convert.ToDecimal(rdr["NET_AMOUNT"])
                    });
                }

                response.flag = 1;
                response.message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }

        public ConsignmentReturnDetailResponse GetConsignmentReturnDetail(ConsignmentReturnDetailFilter request)
        {
            var response = new ConsignmentReturnDetailResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_ConsignmentReturnDetails", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@CompanyID", request.COMPANY_ID);
                cmd.Parameters.AddWithValue("@StoreID", request.STORE_ID);
                cmd.Parameters.AddWithValue("@CustID", request.CUSTOMER_ID ?? 0);
                cmd.Parameters.AddWithValue("@ItemID", request.ITEM_ID ?? 0);
                cmd.Parameters.AddWithValue("@BrandID", request.BRAND_ID ?? 0);
                cmd.Parameters.AddWithValue("@StatusID", request.STATUS_ID ?? "");
                cmd.Parameters.AddWithValue("@FromDate", request.DATE_FROM?.ToString("yyyy-MM-dd") ?? "");
                cmd.Parameters.AddWithValue("@ToDate", request.DATE_TO?.ToString("yyyy-MM-dd") ?? "");

                using SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    response.data.Add(new ConsignmentReturnDetailItem
                    {
                        RETURN_ID = Convert.ToInt32(rdr["ID"]),
                        AMOUNT = Convert.ToDecimal(rdr["AMOUNT"])
                    });
                }

                response.flag = 1;
                response.message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }
        public ItemWiseSalesResponse GetItemWiseSales(ItemWiseSalesFilter request)
        {
            ItemWiseSalesResponse response = new ItemWiseSalesResponse();

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_ITEM_WISE_SALES", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID ?? "");
                    cmd.Parameters.AddWithValue("@SALE_TYPE", request.SALE_TYPE);
                    cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID ?? "");
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", request.SALESMAN_ID ?? "");
                    cmd.Parameters.AddWithValue("@DEPT_ID", request.DEPT_ID ?? "");
                    cmd.Parameters.AddWithValue("@CAT_ID", request.CAT_ID ?? "");
                    cmd.Parameters.AddWithValue("@SUBCAT_ID", request.SUBCAT_ID ?? "");
                    cmd.Parameters.AddWithValue("@BRAND_ID", request.BRAND_ID ?? "");
                    cmd.Parameters.AddWithValue("@CUSTOM1", request.CUSTOM1 ?? "");
                    cmd.Parameters.AddWithValue("@CUSTOM2", request.CUSTOM2 ?? "");
                    cmd.Parameters.AddWithValue("@ITEM_ID", request.ITEM_ID ?? "");
                    cmd.Parameters.AddWithValue("@DISCOUNTED_ITEMS_ONLY", request.DISCOUNTED_ITEMS_ONLY);
                    cmd.Parameters.AddWithValue("@INCLUDE_SUMMARY", request.INCLUDE_SUMMARY);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.data.Add(new ItemWiseSalesItem
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                SALE_NO = reader["SALE_NO"]?.ToString(),
                                SALE_DATE = Convert.ToDateTime(reader["SALE_DATE"]),
                                STORE_NAME = reader["STORE_NAME"]?.ToString(),
                                SALE_TYPE_NAME = reader["SALE_TYPE_NAME"]?.ToString(),
                                CUST_NAME = reader["CUST_NAME"]?.ToString(),
                                COMMENT = reader["COMMENT"]?.ToString(),
                                ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                QUANTITY = Convert.ToDecimal(reader["QUANTITY"]),
                                PRICE = Convert.ToDecimal(reader["PRICE"]),
                                DISCOUNT = Convert.ToDecimal(reader["DISCOUNT"]),
                                DISC_REASON = reader["DISC_REASON"]?.ToString(),
                                GROSS_AMOUNT = Convert.ToDecimal(reader["GROSS_AMOUNT"]),
                                VAT_PERCENT = Convert.ToDecimal(reader["VAT_PERCENT"]),
                                VAT_AMOUNT = Convert.ToDecimal(reader["VAT_AMOUNT"]),
                                NET_AMOUNT = Convert.ToDecimal(reader["NET_AMOUNT"]),
                                SALESMAN = reader["SALESMAN"]?.ToString(),
                                COMMISSION = Convert.ToDecimal(reader["COMMISSION"])
                            });
                        }
                    }
                }
            }

            response.flag = response.data.Count > 0 ? 1 : 0;
            response.message = response.flag == 1 ? "Success" : "No records found";

            return response;
        }
        public DataTable GetItemWiseSalesSummary(ItemWiseSalesSummaryFilter request)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_RPT_ITEM_WISE_SALES_SUMMARY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // REQUIRED
                cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = request.FIN_ID;

                cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = request.DATE_FROM;
                cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = request.DATE_TO;

                // OPTIONAL (SAFE NULL HANDLING)
                cmd.Parameters.Add("@STORE_ID", SqlDbType.VarChar).Value = request.STORE_ID ?? "";
                cmd.Parameters.Add("@SALE_TYPE", SqlDbType.Int).Value = request.SALE_TYPE;
                cmd.Parameters.Add("@CUST_ID", SqlDbType.NVarChar).Value = request.CUST_ID ?? "";
                cmd.Parameters.Add("@SALESMAN_ID", SqlDbType.NVarChar).Value = request.SALESMAN_ID ?? "";
                cmd.Parameters.Add("@DEPT_ID", SqlDbType.NVarChar).Value = request.DEPT_ID ?? "";
                cmd.Parameters.Add("@CAT_ID", SqlDbType.NVarChar).Value = request.CAT_ID ?? "";
                cmd.Parameters.Add("@SUBCAT_ID", SqlDbType.NVarChar).Value = request.SUBCAT_ID ?? "";
                cmd.Parameters.Add("@BRAND_ID", SqlDbType.NVarChar).Value = request.BRAND_ID ?? "";
                cmd.Parameters.Add("@CUSTOM1", SqlDbType.NVarChar).Value = request.CUSTOM1 ?? "";
                cmd.Parameters.Add("@CUSTOM2", SqlDbType.NVarChar).Value = request.CUSTOM2 ?? "";
                cmd.Parameters.Add("@ITEM_ID", SqlDbType.NVarChar).Value = request.ITEM_ID ?? "";
                cmd.Parameters.Add("@DISCOUNTED_ITEMS_ONLY", SqlDbType.Int).Value = request.DISCOUNTED_ITEMS_ONLY;
                cmd.Parameters.Add("@INCLUDE_SUMMARY", SqlDbType.Int).Value = request.INCLUDE_SUMMARY;
                cmd.Parameters.Add("@GROUP_BY_STORE", SqlDbType.Int).Value = request.GROUP_BY_STORE;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);   // ✅ safer than ExecuteReader for summary
                }
            }

            return dt;
        }
        public DataTable GetDiscountWiseSales(DiscountWiseSalesFilter request)
        {
            DiscountWiseSalesResponse response = new DiscountWiseSalesResponse();
            DataTable dt = new DataTable();
            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_DISCOUNT_WISE_SALES", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                    cmd.Parameters.AddWithValue("@FIN_ID", SqlDbType.Int).Value = request.FIN_ID;
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID ?? "");
                    cmd.Parameters.AddWithValue("@SALE_TYPE", request.SALE_TYPE);
                    cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID ?? "");
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", request.SALESMAN_ID ?? "");
                    cmd.Parameters.AddWithValue("@DEPT_ID", request.DEPT_ID ?? "");
                    cmd.Parameters.AddWithValue("@CAT_ID", request.CAT_ID ?? "");
                    cmd.Parameters.AddWithValue("@SUBCAT_ID", request.SUBCAT_ID ?? "");
                    cmd.Parameters.AddWithValue("@BRAND_ID", request.BRAND_ID ?? "");
                    cmd.Parameters.AddWithValue("@CUSTOM1", request.CUSTOM1 ?? "");
                    cmd.Parameters.AddWithValue("@CUSTOM2", request.CUSTOM2 ?? "");
                    cmd.Parameters.AddWithValue("@ITEM_ID", request.ITEM_ID ?? "");
                    cmd.Parameters.AddWithValue("@REASON_ID", request.REASON_ID ?? "");
                    cmd.Parameters.AddWithValue("@INCLUDE_SUMMARY", request.INCLUDE_SUMMARY);


                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);   // ✅ safer than ExecuteReader for summary
                    }

                }
                return dt;
            }

            //  response.flag = response.data.Count > 0 ? 1 : 0;
            // response.message = response.flag == 1 ? "Success" : "No records found";


        }
        public DataTable GetTenderReport(TenderReportFilter request)
        {
            DataTable dt = new DataTable();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_RPT_TENDER_REPORT_PIVOT", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // ✅ Correct parameter usage
                cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = request.FIN_ID;
                cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = request.DATE_FROM;
                cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = request.DATE_TO;

                cmd.Parameters.Add("@STORE_ID", SqlDbType.NVarChar).Value =
                    string.IsNullOrWhiteSpace(request.STORE_ID) ? "" : request.STORE_ID;

                cmd.Parameters.Add("@SALE_TYPE", SqlDbType.Int).Value = request.SALE_TYPE;
                cmd.Parameters.AddWithValue("@CUSTOMER_ID",
                    string.IsNullOrWhiteSpace(request.CUSTOMER_ID) ? "" : request.CUSTOMER_ID);
                // Optional (if SP expects it)
                // cmd.Parameters.Add("@INCLUDE_SUMMARY", SqlDbType.Int).Value = request.INCLUDE_SUMMARY;



                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // 👉 Log this properly in your system
                throw;
            }

            return dt;
        }
        public DataTable GetTenderSummary(TenderSummaryFilter request)
        {
            //TenderSummaryResponse
            var response = new TenderSummaryResponse();
            DataTable dt = new DataTable();
            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_RPT_TENDER_REPORT_PIVOT_SUMMARY", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                cmd.Parameters.AddWithValue("@FIN_ID", SqlDbType.Int).Value = request.FIN_ID;
                cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                cmd.Parameters.AddWithValue("@STORE_ID",
                    string.IsNullOrWhiteSpace(request.STORE_ID) ? "" : request.STORE_ID);
                cmd.Parameters.AddWithValue("@SALE_TYPE", request.SALE_TYPE);
                cmd.Parameters.AddWithValue("@CUSTOMER_ID",
                    string.IsNullOrWhiteSpace(request.CUSTOMER_ID) ? "" : request.CUSTOMER_ID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // 👉 Log this properly in your system
                throw;
            }

            return dt;
        }
        public DataTable GetZReport(ZReportFilter request)
        {
            DataTable dt = new DataTable();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_GENERATE_XREPORT", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // ✅ Correct parameter usage
                cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = request.FIN_ID;
                cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = request.DATE_FROM;
                cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = request.DATE_TO;

                //cmd.Parameters.Add("@SHIFT_ID", SqlDbType.Int).Value = request.SHIFT_ID;
                //cmd.Parameters.Add("@STORE_ID", SqlDbType.Int).Value = request.STORE_ID;
                cmd.Parameters.Add("@BATCH_ID", SqlDbType.Int).Value = request.BATCH_ID;

                // Optional (if SP expects it)
                // cmd.Parameters.Add("@INCLUDE_SUMMARY", SqlDbType.Int).Value = request.INCLUDE_SUMMARY;



                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // 👉 Log this properly in your system
                throw;
            }

            return dt;
        }
    }

}