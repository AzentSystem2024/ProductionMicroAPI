using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SalesPOSService: ISalesPOSService
    {
        public SalesPOS GetSalesPOSById(int Id)
        {
            SalesPOS result = new SalesPOS();

            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_POS_SALES", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TRANS_ID", Id);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // HEADER
                        if (reader.Read())
                        {
                            result.Header = new SalesPOSHeader
                            {
                                SALE_ID = reader["SALE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SALE_ID"]),
                                INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                SALE_DATE = reader["SALE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["SALE_DATE"]),
                                CUSTOMER_ID = reader["CUSTOMER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CUSTOMER_ID"]),
                                CUST_NAME = reader["CUST_NAME"]?.ToString(),
                                STORE_ID = reader["STORE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["STORE_ID"]),
                                STORE_NAME = reader["STORE_NAME"]?.ToString(),
                                SALESMAN_ID = reader["SALESMAN_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SALESMAN_ID"]),
                                EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                GROSS_AMOUNT = reader["GROSS_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["GROSS_AMOUNT"]),
                                TAX_AMOUNT = reader["TAX_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TAX_AMOUNT"]),
                                NET_AMOUNT = reader["NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["NET_AMOUNT"]),
                                DISCOUNT_AMOUNT = reader["DISCOUNT_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DISCOUNT_AMOUNT"])
                            };
                        }

                        // DETAIL
                        if (reader.NextResult())
                        {
                            result.Details = new List<SalesPOSDetail>();

                            while (reader.Read())
                            {
                                result.Details.Add(new SalesPOSDetail
                                {
                                    ITEM_ID = reader["ITEM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ITEM_ID"]),
                                    ITEM_CODE = reader["ITEM_CODE"]?.ToString(),
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                    QUANTITY = reader["QUANTITY"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["QUANTITY"]),
                                    PRICE = reader["PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PRICE"]),
                                    DISCOUNT = reader["DISCOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DISCOUNT"]),
                                    AMOUNT_INCL_VAT = reader["AMOUNT_INCL_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AMOUNT_INCL_VAT"]),
                                    VAT_PERCENT = reader["VAT_PERCENT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["VAT_PERCENT"]),
                                    VAT_AMOUNT = reader["VAT_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["VAT_AMOUNT"])
                                });
                            }
                        }

                        // TENDER
                        if (reader.NextResult())
                        {
                            result.Tenders = new List<SalesPOSTender>();

                            while (reader.Read())
                            {
                                result.Tenders.Add(new SalesPOSTender
                                {
                                    TENDER_ID = reader["TENDER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TENDER_ID"]),
                                    AMOUNT = reader["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AMOUNT"]),
                                    DESCRIPTION = reader["DESCRIPTION"]?.ToString()
                                });
                            }
                        }
                    }
                }
            }

            return result;
        }


    }
}
