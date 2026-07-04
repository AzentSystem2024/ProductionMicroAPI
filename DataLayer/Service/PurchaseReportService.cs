using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
namespace MicroApi.DataLayer.Service
{
    public class PurchaseReportService:IPurchaseReportService
    {
        public PurchaseSummaryResponse GetPurchaseSummary(PurchaseReport filter)
        {
            var response = new PurchaseSummaryResponse
            {
                data = new List<PurchaseSummaryRpt>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_PURCHASE_SUMMARY", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = filter.COMPANY_ID;
                        cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = filter.FIN_ID;
                        cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = filter.DATE_FROM;
                        cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = filter.DATE_TO;
                        cmd.Parameters.Add("@STORE_ID", SqlDbType.VarChar).Value = filter.STORE_ID ?? "";
                        cmd.Parameters.Add("@SUPP_ID", SqlDbType.VarChar).Value = filter.SUPP_ID ?? "";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var rpt = new PurchaseSummaryRpt
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt64(reader["TRANS_ID"]) : 0,
                                    STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0,
                                    PURCHASE_NO = reader["Purchase No"]?.ToString(),
                                    DATE = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"]) : DateTime.MinValue,
                                    STORE = reader["Store"]?.ToString(),
                                    SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                    SUPPLIER = reader["Supplier"]?.ToString(),
                                    INVOICE_NO = reader["Invoice No"]?.ToString(),
                                    DISCOUNT = reader["Discount"] != DBNull.Value ? Convert.ToDecimal(reader["Discount"]) : 0,
                                    EX_VAT_TOTAL = reader["Ex. VAT Total"] != DBNull.Value ? Convert.ToDecimal(reader["Ex. VAT Total"]) : 0,
                                    VAT_AMOUNT = reader["VAT Amount"] != DBNull.Value ? Convert.ToDecimal(reader["VAT Amount"]) : 0,
                                    INC_VAT_TOTAL = reader["Inc. VAT Total"] != DBNull.Value ? Convert.ToDecimal(reader["Inc. VAT Total"]) : 0
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
                response.message = "Error fetching Purchase Summary: " + ex.Message;
            }

            return response;
        }
        public ItemWisePurchaseResponse GetItemWisePurchaseReport(ItemWisePurchaseReportRequest filter)
        {
            var response = new ItemWisePurchaseResponse
            {
                data = new List<ItemWisePurchaseRpt>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_ITEM_WISE_PURCHASE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = filter.COMPANY_ID;
                        cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = filter.FIN_ID;
                        cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = filter.DATE_FROM;
                        cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = filter.DATE_TO;

                        cmd.Parameters.Add("@STORE_ID", SqlDbType.VarChar).Value = filter.STORE_ID ?? "";
                        cmd.Parameters.Add("@SUPP_ID", SqlDbType.VarChar).Value = filter.SUPP_ID ?? "";
                        cmd.Parameters.Add("@DEPT_ID", SqlDbType.VarChar).Value = filter.DEPT_ID ?? "";
                        cmd.Parameters.Add("@CAT_ID", SqlDbType.VarChar).Value = filter.CAT_ID ?? "";
                        cmd.Parameters.Add("@SUBCAT_ID", SqlDbType.VarChar).Value = filter.SUBCAT_ID ?? "";
                        cmd.Parameters.Add("@BRAND_ID", SqlDbType.VarChar).Value = filter.BRAND_ID ?? "";
                        cmd.Parameters.Add("@CUSTOM1", SqlDbType.VarChar).Value = filter.CUSTOM1 ?? "";
                        cmd.Parameters.Add("@CUSTOM2", SqlDbType.VarChar).Value = filter.CUSTOM2 ?? "";
                        cmd.Parameters.Add("@ITEM_ID", SqlDbType.VarChar).Value = filter.ITEM_ID ?? "";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemWisePurchaseRpt rpt = new ItemWisePurchaseRpt
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    PURCH_NO = reader["PURCH_NO"]?.ToString(),
                                    PURCH_DATE = reader["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCH_DATE"]) : DateTime.MinValue,

                                    STORE_NAME = reader["STORE_NAME"]?.ToString(),

                                    SUPP_NAME = reader["SUPP_NAME"]?.ToString(),

                                    ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),

                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToDecimal(reader["QUANTITY"]) : 0,
                                    RATE = reader["RATE"] != DBNull.Value ? Convert.ToDecimal(reader["RATE"]) : 0,

                                    DISCOUNT = reader["DISCOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DISCOUNT"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["GROSS_AMOUNT"]) : 0,

                                    VAT_PERCENT = reader["VAT_PERCENT"] != DBNull.Value ? Convert.ToDecimal(reader["VAT_PERCENT"]) : 0,
                                    VAT_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["VAT_AMOUNT"]) : 0,

                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0,

                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt64(reader["TRANS_ID"]) : 0,

                                    DEPT_NAME = reader["DEPT_NAME"]?.ToString(),
                                    CAT_NAME = reader["CAT_NAME"]?.ToString(),
                                    SUBCAT_NAME = reader["SUBCAT_NAME"]?.ToString(),
                                    BRAND_NAME = reader["BRAND_NAME"]?.ToString(),
                                    CUSTOM1_NAME = reader["CUSTOM1_NAME"]?.ToString(),
                                    CUSTOM2_NAME = reader["CUSTOM2_NAME"]?.ToString()
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
                response.message = ex.Message;
            }

            return response;
        }
    }
}
