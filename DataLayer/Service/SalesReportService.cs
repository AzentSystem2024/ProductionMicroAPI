using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SalesReportService : ISalesReportService
    {
        public SalesSummaryResponse GetSalesSummary(SalesSummaryFilter request)
        {
            var response = new SalesSummaryResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();
                using SqlCommand cmd = new SqlCommand("SP_SalesSummary", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CompanyID", request.COMPANY_ID);
                cmd.Parameters.AddWithValue("@ItemID",
                string.IsNullOrWhiteSpace(request.ITEM_ID) ? (object)DBNull.Value : request.ITEM_ID);

                cmd.Parameters.AddWithValue("@BrandID",
                    string.IsNullOrWhiteSpace(request.BRAND_ID) ? (object)DBNull.Value : request.BRAND_ID);

                cmd.Parameters.AddWithValue("@CustID",
                    string.IsNullOrWhiteSpace(request.CUSTOMER_ID) ? (object)DBNull.Value : request.CUSTOMER_ID);

                cmd.Parameters.AddWithValue("@StatusID",
                    string.IsNullOrWhiteSpace(request.STATUS_ID) ? (object)DBNull.Value : request.STATUS_ID);

                cmd.Parameters.AddWithValue("@StoreID",
                    string.IsNullOrWhiteSpace(request.STORE_ID) ? (object)DBNull.Value : request.STORE_ID);


                using SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    response.data.Add(new SalesSummaryItem
                    {
                        SALE_ID = Convert.ToInt32(rdr["ID"]),
                        SALE_DATE = Convert.ToDateTime(rdr["SALE_DATE"]),
                        INVOICE_NO = rdr["SALE_NO"].ToString(),
                        CUSTOMER_NAME = rdr["CUST_NAME"].ToString(),
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
    }
}