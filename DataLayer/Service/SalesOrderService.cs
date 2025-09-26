using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SalesOrderService : ISalesOrderService
    {
        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;
            string[] formats = new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy" };
            if (DateTime.TryParseExact(dateStr, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dt))
                return dt;
            return DBNull.Value;
        }

        public int SaveData(SalesOrder salesOrder)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("UOM", typeof(string));
                        tvp.Columns.Add("QUANTITY", typeof(float));
                        tvp.Columns.Add("PRICE", typeof(float));
                        tvp.Columns.Add("DISC_PERCENT", typeof(float));
                        tvp.Columns.Add("AMOUNT", typeof(float));
                        tvp.Columns.Add("TAX_PERCENT", typeof(float));
                        tvp.Columns.Add("TAX_AMOUNT", typeof(float));
                        tvp.Columns.Add("TOTAL_AMOUNT", typeof(float));
                        tvp.Columns.Add("REMARKS", typeof(string));
                        tvp.Columns.Add("DN_QTY", typeof(float));

                        foreach (var detail in salesOrder.Details)
                        {
                            tvp.Rows.Add(
                                detail.ITEM_ID ?? 0,
                                detail.UOM ?? "",
                                detail.QUANTITY ?? 0,
                                detail.PRICE ?? 0,
                                detail.DISC_PERCENT ?? 0,
                                detail.AMOUNT ?? 0,
                                detail.TAX_PERCENT ?? 0,
                                detail.TAX_AMOUNT ?? 0,
                                detail.TOTAL_AMOUNT ?? 0,
                                detail.REMARKS ?? "",
                                detail.DN_QTY ?? 0
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 1);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salesOrder.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", salesOrder.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", salesOrder.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SO_DATE", ParseDate(salesOrder.SO_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", salesOrder.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SALESMAN_ID", salesOrder.SALESMAN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@CONTACT_NAME", salesOrder.CONTACT_NAME ?? "");
                            cmd.Parameters.AddWithValue("@CONTACT_PHONE", salesOrder.CONTACT_PHONE ?? "");
                            cmd.Parameters.AddWithValue("@CONTACT_EMAIL", salesOrder.CONTACT_EMAIL ?? "");
                            cmd.Parameters.AddWithValue("@QTN_ID", salesOrder.QTN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@REF_NO", salesOrder.REF_NO ?? "");
                            cmd.Parameters.AddWithValue("@PAY_TERM_ID", salesOrder.PAY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", salesOrder.DELIVERY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", salesOrder.GROSS_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", salesOrder.CHARGE_DESCRIPTION ?? "");
                            cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", salesOrder.CHARGE_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", salesOrder.NET_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", salesOrder.USER_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", salesOrder.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return salesOrder.ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }

        public SalesOrderResponse EditData(SalesOrderUpdate salesOrder)
        {
            SalesOrderResponse response = new SalesOrderResponse();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("UOM", typeof(string));
                        tvp.Columns.Add("QUANTITY", typeof(float));
                        tvp.Columns.Add("PRICE", typeof(float));
                        tvp.Columns.Add("DISC_PERCENT", typeof(float));
                        tvp.Columns.Add("AMOUNT", typeof(float));
                        tvp.Columns.Add("TAX_PERCENT", typeof(float));
                        tvp.Columns.Add("TAX_AMOUNT", typeof(float));
                        tvp.Columns.Add("TOTAL_AMOUNT", typeof(float));
                        tvp.Columns.Add("REMARKS", typeof(string));
                        tvp.Columns.Add("DN_QTY", typeof(float));

                        foreach (var detail in salesOrder.Details)
                        {
                            tvp.Rows.Add(
                                detail.ITEM_ID ?? 0,
                                detail.UOM ?? "",
                                detail.QUANTITY ?? 0,
                                detail.PRICE ?? 0,
                                detail.DISC_PERCENT ?? 0,
                                detail.AMOUNT ?? 0,
                                detail.TAX_PERCENT ?? 0,
                                detail.TAX_AMOUNT ?? 0,
                                detail.TOTAL_AMOUNT ?? 0,
                                detail.REMARKS ?? "",
                                detail.DN_QTY ?? 0
                            );
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2);
                            cmd.Parameters.AddWithValue("@ID", salesOrder.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salesOrder.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", salesOrder.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", salesOrder.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SO_DATE", ParseDate(salesOrder.SO_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", salesOrder.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SALESMAN_ID", salesOrder.SALESMAN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@CONTACT_NAME", salesOrder.CONTACT_NAME ?? "");
                            cmd.Parameters.AddWithValue("@CONTACT_PHONE", salesOrder.CONTACT_PHONE ?? "");
                            cmd.Parameters.AddWithValue("@CONTACT_EMAIL", salesOrder.CONTACT_EMAIL ?? "");
                            cmd.Parameters.AddWithValue("@QTN_ID", salesOrder.QTN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@REF_NO", salesOrder.REF_NO ?? "");
                            cmd.Parameters.AddWithValue("@PAY_TERM_ID", salesOrder.PAY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", salesOrder.DELIVERY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", salesOrder.GROSS_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", salesOrder.CHARGE_DESCRIPTION ?? "");
                            cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", salesOrder.CHARGE_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", salesOrder.NET_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@TRANS_ID", salesOrder.TRANS_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NARRATION", salesOrder.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        response.Flag = "1";
                        response.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Flag = "0";
                        response.Message = ex.Message;
                    }
                }
            }
            return response;
        }

        public SalesOrderDetailSelectResponse GetSalesOrder(int soId)
        {
            SalesOrderDetailSelectResponse response = new SalesOrderDetailSelectResponse
            {
                Data = new SalesOrderSelect { Details = new List<SalesOrderDetailSelect>() }
            };
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@SO_ID", soId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.Data.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                            response.Data.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0;
                            response.Data.STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0;
                            response.Data.SO_NO = reader["SO_NO"] != DBNull.Value ? reader["SO_NO"].ToString() : null;
                            response.Data.SO_DATE = reader["SO_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SO_DATE"]).ToString("yyyy-MM-dd") : null;
                            response.Data.CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : 0;
                            response.Data.CUST_NAME = reader["CUSTOMER_NAME"] != DBNull.Value ? reader["CUSTOMER_NAME"].ToString() : null;
                            response.Data.SALESMAN_ID = reader["SALESMAN_ID"] != DBNull.Value ? Convert.ToInt32(reader["SALESMAN_ID"]) : 0;
                            response.Data.EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? reader["EMP_NAME"].ToString() : null;
                            response.Data.CONTACT_NAME = reader["CONTACT_NAME"] != DBNull.Value ? reader["CONTACT_NAME"].ToString() : null;
                            response.Data.CONTACT_PHONE = reader["CONTACT_PHONE"] != DBNull.Value ? reader["CONTACT_PHONE"].ToString() : null;
                            response.Data.CONTACT_EMAIL = reader["CONTACT_EMAIL"] != DBNull.Value ? reader["CONTACT_EMAIL"].ToString() : null;
                            response.Data.REF_NO = reader["REF_NO"] != DBNull.Value ? reader["REF_NO"].ToString() : null;
                            response.Data.PAY_TERM_ID = reader["PAY_TERM_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TERM_ID"]) : 0;
                            response.Data.DELIVERY_TERM_ID = reader["DELIVERY_TERM_ID"] != DBNull.Value ? Convert.ToInt32(reader["DELIVERY_TERM_ID"]) : 0;
                            response.Data.PAY_TERM_NAME = reader["PAY_TERM_NAME"] != DBNull.Value ? reader["PAY_TERM_NAME"].ToString() : null;
                            response.Data.DELIVERY_TERM_NAME = reader["DELIVERY_TERM_NAME"] != DBNull.Value ? reader["DELIVERY_TERM_NAME"].ToString() : null;
                            response.Data.GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0;
                            response.Data.CHARGE_DESCRIPTION = reader["CHARGE_DESCRIPTION"] != DBNull.Value ? reader["CHARGE_DESCRIPTION"].ToString() : null;
                            response.Data.CHARGE_AMOUNT = reader["CHARGE_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["CHARGE_AMOUNT"]) : 0;
                            response.Data.NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0;
                            response.Data.TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;
                            response.Data.NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null;
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                SalesOrderDetailSelect detail = new SalesOrderDetailSelect
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    SO_ID = reader["SO_ID"] != DBNull.Value ? Convert.ToInt32(reader["SO_ID"]) : 0,
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                    ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    ITEM_NAME = reader["ITEM_NAME"] != DBNull.Value ? reader["ITEM_NAME"].ToString() : null,
                                    UOM = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : null,
                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    DISC_PERCENT = reader["DISC_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DISC_PERCENT"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    TAX_PERCENT = reader["TAX_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_PERCENT"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_AMOUNT"]) : 0,
                                    REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null,
                                    DN_QTY = reader["DN_QTY"] != DBNull.Value ? Convert.ToSingle(reader["DN_QTY"]) : 0,
                                };
                                response.Data.Details.Add(detail);
                            }
                        }
                    }
                }
            }
            response.Flag = 1;
            response.Message = "Success";
            return response;
        }

        public SalesOrderListResponse GetAllSalesOrders()
        {
            SalesOrderListResponse response = new SalesOrderListResponse { Data = new List<SalesOrderList>() };
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalesOrderList salesOrder = new SalesOrderList
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                SO_DATE = reader["SO_DATE"] != DBNull.Value ? reader["SO_DATE"].ToString() : null,
                                SO_NO = reader["SO_NO"] != DBNull.Value ? reader["SO_NO"].ToString() : null,
                                COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                STORE_NAME = reader["STORE_NAME"] != DBNull.Value ? reader["STORE_NAME"].ToString() : null,
                                CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : (int?)null,
                                CUSTOMER_NAME = reader["CUSTOMER_NAME"] != DBNull.Value ? reader["CUSTOMER_NAME"].ToString() : null,
                                SALESMAN_ID = reader["SALESMAN_ID"] != DBNull.Value ? Convert.ToInt32(reader["SALESMAN_ID"]) : (int?)null,
                                CONTACT_NAME = reader["CONTACT_NAME"] != DBNull.Value ? reader["CONTACT_NAME"].ToString() : null,
                                REF_NO = reader["REF_NO"] != DBNull.Value ? reader["REF_NO"].ToString() : null,
                                NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : (float?)null,
                                TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null
                            };
                            response.Data.Add(salesOrder);
                        }
                    }
                }
            }
            response.Flag = 1;
            response.Message = "Success";
            return response;
        }

        public ItemListResponse GetSalesOrderItems(SalesOrderRequest request)
        {
            ItemListResponse response = new ItemListResponse { Data = new List<Item>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Item item = new Item
                                {
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                    ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    MATRIX_CODE = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : null,
                                    UOM = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : null,
                                    COST = reader["COST"] != DBNull.Value ? Convert.ToSingle(reader["COST"]) : (float?)0,
                                    STOCK_QTY = reader["STOCK_QTY"] != DBNull.Value ? Convert.ToSingle(reader["STOCK_QTY"]) : (float?)0,
                                    VAT_PERC = reader["VAT_PERC"] != DBNull.Value ? Convert.ToDecimal(reader["VAT_PERC"]) : 0,
                                };
                                response.Data.Add(item);
                            }
                        }
                    }
                }
                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = null;
            }
            return response;
        }

        public bool DeleteSalesOrder(int soId)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@SO_ID", soId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public SalesOrderResponse ApproveSalesOrder(SalesOrderUpdate request)
        {
            SalesOrderResponse response = new SalesOrderResponse();
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("ITEM_ID", typeof(int));
                        tvp.Columns.Add("UOM", typeof(string));
                        tvp.Columns.Add("QUANTITY", typeof(float));
                        tvp.Columns.Add("PRICE", typeof(float));
                        tvp.Columns.Add("DISC_PERCENT", typeof(float));
                        tvp.Columns.Add("AMOUNT", typeof(float));
                        tvp.Columns.Add("TAX_PERCENT", typeof(float));
                        tvp.Columns.Add("TAX_AMOUNT", typeof(float));
                        tvp.Columns.Add("TOTAL_AMOUNT", typeof(float));
                        tvp.Columns.Add("REMARKS", typeof(string));
                        tvp.Columns.Add("DN_QTY", typeof(float));

                        if (request.Details != null && request.Details.Any())
                        {
                            foreach (var detail in request.Details)
                            {
                                tvp.Rows.Add(
                                    detail.ITEM_ID ?? 0,
                                    detail.UOM ?? "",
                                    detail.QUANTITY ?? 0,
                                    detail.PRICE ?? 0,
                                    detail.DISC_PERCENT ?? 0,
                                    detail.AMOUNT ?? 0,
                                    detail.TAX_PERCENT ?? 0,
                                    detail.TAX_AMOUNT ?? 0,
                                    detail.TOTAL_AMOUNT ?? 0,
                                    detail.REMARKS ?? "",
                                    detail.DN_QTY ?? 0
                                );
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 7);
                            cmd.Parameters.AddWithValue("@SO_ID", request.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@SO_DATE", string.IsNullOrEmpty(request.SO_DATE) ? (object)DBNull.Value : ParseDate(request.SO_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@SALESMAN_ID", request.SALESMAN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CONTACT_NAME", string.IsNullOrEmpty(request.CONTACT_NAME) ? (object)DBNull.Value : request.CONTACT_NAME);
                            cmd.Parameters.AddWithValue("@CONTACT_PHONE", string.IsNullOrEmpty(request.CONTACT_PHONE) ? (object)DBNull.Value : request.CONTACT_PHONE);
                            cmd.Parameters.AddWithValue("@CONTACT_EMAIL", string.IsNullOrEmpty(request.CONTACT_EMAIL) ? (object)DBNull.Value : request.CONTACT_EMAIL);
                            cmd.Parameters.AddWithValue("@QTN_ID", request.QTN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@REF_NO", string.IsNullOrEmpty(request.REF_NO) ? (object)DBNull.Value : request.REF_NO);
                            cmd.Parameters.AddWithValue("@PAY_TERM_ID", request.PAY_TERM_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", request.DELIVERY_TERM_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", request.GROSS_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", string.IsNullOrEmpty(request.CHARGE_DESCRIPTION) ? (object)DBNull.Value : request.CHARGE_DESCRIPTION);
                            cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", request.CHARGE_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", request.NET_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@NARRATION", string.IsNullOrEmpty(request.NARRATION) ? (object)DBNull.Value : request.NARRATION);

                            if (tvp.Rows.Count > 0)
                            {
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", tvp);
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";
                            }
                            else
                            {
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SO_DETAIL", new DataTable());
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SO_DETAIL";
                            }

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        response.Flag = "1";
                        response.Message = "Sales Order approved and updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Flag = "0";
                        response.Message = "Error approving Sales Order: " + ex.Message;
                    }
                }
            }
            return response;
        }
        public SOQUOTATIONLISTResponse GetSOQUOTATIONLIST()
        {
            SOQUOTATIONLISTResponse response = new SOQUOTATIONLISTResponse { Data = new List<SOQUOTATIONLIST>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_SO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 8);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SOQUOTATIONLIST item = new SOQUOTATIONLIST
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    QTN_ID = reader["QTN_ID"] != DBNull.Value ? Convert.ToInt32(reader["QTN_ID"]) : (int?)null,
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : (int?)null,
                                    ITEM_CODE = reader["ITEM_CODE"] != DBNull.Value ? reader["ITEM_CODE"].ToString() : null,
                                    ITEM_NAME = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    MATRIX_CODE = reader["MATRIX_CODE"] != DBNull.Value ? reader["MATRIX_CODE"].ToString() : null,
                                    UOM = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : null,
                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0,
                                    GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0,
                                    PRICE = reader["PRICE"] != DBNull.Value ? Convert.ToSingle(reader["PRICE"]) : 0,
                                    DISC_PERCENT = reader["DISC_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DISC_PERCENT"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0,
                                    TAX_PERCENT = reader["TAX_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_PERCENT"]) : 0,
                                    TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0,
                                    TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_AMOUNT"]) : 0,
                                    REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null
                                };
                                response.Data.Add(item);
                            }
                        }
                    }
                }
                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = null;
            }
            return response;
        }
    }
}
