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
                using SqlCommand cmd = new SqlCommand("SP_RPT_SALE_SUMMARY_NEW", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0; // important for large pivot reports

                // ✅ Proper parameter handling

                cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime)
                    .Value = filter.DATE_FROM ?? (object)DBNull.Value;

                cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime)
                    .Value = filter.DATE_TO ?? (object)DBNull.Value;

                cmd.Parameters.Add("@STORE_ID", SqlDbType.NVarChar)
                    .Value = string.IsNullOrEmpty(filter.STORE_ID) ? "" : filter.STORE_ID;

                cmd.Parameters.Add("@CUST_ID", SqlDbType.NVarChar)
                    .Value = string.IsNullOrEmpty(filter.CUST_ID) ? "" : filter.CUST_ID;

                cmd.Parameters.Add("@SALE_TYPE", SqlDbType.Int)
                    .Value = filter.SALE_TYPE;

                cmd.Parameters.Add("@INCLUDE_SUMMARY", SqlDbType.Int)
                    .Value = filter.INCLUDE_SUMMARY;

                //con.Open();

                using SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching Sales Summary: " + ex.Message);
            }

            return dt;
        }

        public SalesDetailResponse GetSalesDetails(SalesDetailFilter request)
        {
            var response = new SalesDetailResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_SalesDetails", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CompanyID", request.COMPANY_ID);
                cmd.Parameters.AddWithValue("@ItemID", request.ITEM_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CustID", request.CUSTOMER_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateFrom", request.DATE_FROM ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateTo", request.DATE_TO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StatusID", request.STATUS_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BrandID", request.BRAND_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StoreID", request.STORE_ID ?? (object)DBNull.Value);

                using SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    response.data.Add(new SalesDetailItem
                    {
                        SALE_ID = Convert.ToInt32(rdr["ID"]),
                        ITEM_NAME = rdr["DESCRIPTION"].ToString(),
                        NET_AMOUNT = Convert.ToDecimal(rdr["NET_AMOUNT"])
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

        public ConsignmentSummaryResponse GetConsignmentSummary(ConsignmentSummaryFilter request)
        {
            var response = new ConsignmentSummaryResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_ConsignmentSummary", con);

                cmd.CommandType = CommandType.StoredProcedure;

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
        public ItemWiseSalesSummaryResponse GetItemWiseSalesSummary(ItemWiseSalesSummaryFilter request)
        {
            ItemWiseSalesSummaryResponse response = new ItemWiseSalesSummaryResponse();

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_ITEM_WISE_SALES_SUMMARY", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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
                    cmd.Parameters.AddWithValue("@GROUP_BY_STORE", request.GROUP_BY_STORE);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.data.Add(new ItemWiseSalesSummaryItem
                            {
                                ITEM_ID = Convert.ToInt32(reader["ITEM_ID"]),
                                ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                STORE_ID = Convert.ToInt32(reader["STORE_ID"]),
                                STORE_NAME = reader["STORE_NAME"]?.ToString(),
                                QUANTITY = Convert.ToDecimal(reader["QUANTITY"]),
                                DISCOUNT = Convert.ToDecimal(reader["DISCOUNT"]),
                                GROSS_AMOUNT = Convert.ToDecimal(reader["GROSS_AMOUNT"]),
                                VAT_AMOUNT = Convert.ToDecimal(reader["VAT_AMOUNT"]),
                                NET_AMOUNT = Convert.ToDecimal(reader["NET_AMOUNT"])
                            });
                        }
                    }
                }
            }

            response.flag = response.data.Count > 0 ? 1 : 0;
            response.message = response.flag == 1 ? "Success" : "No records found";

            return response;
        }
        public DiscountWiseSalesResponse GetDiscountWiseSales(DiscountWiseSalesFilter request)
        {
            DiscountWiseSalesResponse response = new DiscountWiseSalesResponse();

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_DISCOUNT_WISE_SALES", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.data.Add(new DiscountWiseSalesItem
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
        public TenderReportResponse GetTenderReport(TenderReportFilter request)
        {
            var response = new TenderReportResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_RPT_TENDER_REPORT_PIVOT", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                cmd.Parameters.AddWithValue("@STORE_ID",
                    string.IsNullOrWhiteSpace(request.STORE_ID) ? "" : request.STORE_ID);
                cmd.Parameters.AddWithValue("@SALE_TYPE", request.SALE_TYPE);

                using SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var item = new TenderReportItem
                    {
                        TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                        INVOICE_NO = rdr["INVOICE NO"].ToString(),
                        DATE = Convert.ToDateTime(rdr["DATE"]),
                        STORE = rdr["STORE"].ToString(),
                        INVOICE_TYPE = rdr["INVOICE TYPE"].ToString()
                    };

                    // Handle dynamic pivot columns
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string columnName = rdr.GetName(i);

                        if (columnName != "TRANS_ID" &&
                            columnName != "INVOICE NO" &&
                            columnName != "DATE" &&
                            columnName != "STORE" &&
                            columnName != "INVOICE TYPE")
                        {
                            decimal value = rdr[i] == DBNull.Value ? 0 : Convert.ToDecimal(rdr[i]);
                            item.TenderAmounts[columnName] = value;
                        }
                    }

                    response.data.Add(item);
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
        public TenderSummaryResponse GetTenderSummary(TenderSummaryFilter request)
        {
            var response = new TenderSummaryResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_RPT_TENDER_REPORT_PIVOT_SUMMARY", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                cmd.Parameters.AddWithValue("@STORE_ID",
                    string.IsNullOrWhiteSpace(request.STORE_ID) ? "" : request.STORE_ID);
                cmd.Parameters.AddWithValue("@SALE_TYPE", request.SALE_TYPE);

                using SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var item = new TenderSummaryItem
                    {
                        TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                        INVOICE_NO = rdr["INVOICE NO"].ToString(),
                        DATE = Convert.ToDateTime(rdr["DATE"]),
                        STORE = rdr["STORE"].ToString(),
                        INVOICE_TYPE = rdr["INVOICE TYPE"].ToString()
                    };

                    // Handle dynamic pivot columns
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string columnName = rdr.GetName(i);

                        if (columnName != "TRANS_ID" &&
                            columnName != "INVOICE NO" &&
                            columnName != "DATE" &&
                            columnName != "STORE" &&
                            columnName != "INVOICE TYPE")
                        {
                            decimal value = rdr[i] == DBNull.Value ? 0 : Convert.ToDecimal(rdr[i]);
                            item.TenderAmounts[columnName] = value;
                        }
                    }

                    response.data.Add(item);
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
    }

}