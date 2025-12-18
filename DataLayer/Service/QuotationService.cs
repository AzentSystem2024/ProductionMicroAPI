using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class QuotationService : IQuotationService
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

        public int SaveData(Quotation quotation)
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

                        foreach (var detail in quotation.Details)
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
                                detail.REMARKS ?? ""
                            );
                        }
                        // Create DataTable for SQTN_TERMS
                        DataTable tvpTerms = new DataTable();
                        tvpTerms.Columns.Add("ID", typeof(int));
                        tvpTerms.Columns.Add("QTN_ID", typeof(int));
                        tvpTerms.Columns.Add("TERMS", typeof(string));

                        if (quotation.Terms != null && quotation.Terms.Any())
                        {
                            foreach (var term in quotation.Terms)
                            {
                                tvpTerms.Rows.Add(term.ID, term.QTN_ID, term.TERMS);
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 1);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", quotation.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", quotation.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", quotation.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@QTN_DATE", ParseDate(quotation.QTN_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", quotation.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SALESMAN_ID", quotation.SALESMAN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@CONTACT_NAME", quotation.CONTACT_NAME ?? "");
                            cmd.Parameters.AddWithValue("@SUBJECT", quotation.SUBJECT ?? "");
                            cmd.Parameters.AddWithValue("@REF_NO", quotation.REF_NO ?? "");
                            cmd.Parameters.AddWithValue("@PAY_TERM_ID", quotation.PAY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", quotation.DELIVERY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@VALID_DAYS", quotation.VALID_DAYS ?? 0);
                            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", quotation.GROSS_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@TAX_AMOUNT", quotation.TAX_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", quotation.CHARGE_DESCRIPTION ?? "");
                            cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", quotation.CHARGE_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@DISCOUNT_DESCRIPTION", quotation.DISCOUNT_DESCRIPTION ?? "");
                            cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", quotation.DISCOUNT_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@ROUND_OFF", quotation.ROUND_OFF ?? false);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", quotation.NET_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@USER_ID", quotation.USER_ID ?? 0);
                            //cmd.Parameters.AddWithValue("@TERMS", string.IsNullOrWhiteSpace(quotation.TERMS) ? (object)DBNull.Value : quotation.TERMS);
                            //if (!string.IsNullOrWhiteSpace(quotation.TERMS) && quotation.TERMS.ToLower() != "string")
                            //    cmd.Parameters.AddWithValue("@TERMS", quotation.TERMS);
                            //else
                            //    cmd.Parameters.AddWithValue("@TERMS", DBNull.Value);
                            cmd.Parameters.AddWithValue("@NARRATION", quotation.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SQTN_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_SQTN_DETAIL";

                            // Add SQTN_TERMS TVP
                            SqlParameter tvpTermsParam = cmd.Parameters.AddWithValue("@SQTN_TERMS", tvpTerms);
                            tvpTermsParam.SqlDbType = SqlDbType.Structured;
                            tvpTermsParam.TypeName = "dbo.UDT_TB_SQTN_TERM";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return quotation.ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }

        public QuotationResponse EditData(QuotationUpdate quotation)
        {
            QuotationResponse response = new QuotationResponse();
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

                        foreach (var detail in quotation.Details)
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
                                detail.REMARKS ?? ""
                            );
                        }
                        // Create DataTable for SQTN_TERMS
                        DataTable tvpTerms = new DataTable();
                        tvpTerms.Columns.Add("ID", typeof(int));
                        tvpTerms.Columns.Add("QTN_ID", typeof(int));
                        tvpTerms.Columns.Add("TERMS", typeof(string));

                        if (quotation.Terms != null && quotation.Terms.Any())
                        {
                            foreach (var term in quotation.Terms)
                            {
                                tvpTerms.Rows.Add(term.ID, term.QTN_ID, term.TERMS);
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2);
                            cmd.Parameters.AddWithValue("@QTN_ID", quotation.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", quotation.COMPANY_ID ?? 0);
                            cmd.Parameters.AddWithValue("@FIN_ID", quotation.FIN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@STORE_ID", quotation.STORE_ID ?? 0);
                            cmd.Parameters.AddWithValue("@QTN_DATE", ParseDate(quotation.QTN_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", quotation.CUST_ID ?? 0);
                            cmd.Parameters.AddWithValue("@SALESMAN_ID", quotation.SALESMAN_ID ?? 0);
                            cmd.Parameters.AddWithValue("@CONTACT_NAME", quotation.CONTACT_NAME ?? "");
                            cmd.Parameters.AddWithValue("@SUBJECT", quotation.SUBJECT ?? "");
                            cmd.Parameters.AddWithValue("@REF_NO", quotation.REF_NO ?? "");
                            cmd.Parameters.AddWithValue("@PAY_TERM_ID", quotation.PAY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", quotation.DELIVERY_TERM_ID ?? 0);
                            cmd.Parameters.AddWithValue("@VALID_DAYS", quotation.VALID_DAYS ?? 0);
                            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", quotation.GROSS_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@TAX_AMOUNT", quotation.TAX_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", quotation.CHARGE_DESCRIPTION ?? "");
                            cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", quotation.CHARGE_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@DISCOUNT_DESCRIPTION", quotation.DISCOUNT_DESCRIPTION ?? "");
                            cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", quotation.DISCOUNT_AMOUNT ?? 0);
                            cmd.Parameters.AddWithValue("@ROUND_OFF", quotation.ROUND_OFF ?? false);
                            cmd.Parameters.AddWithValue("@TRANS_ID", quotation.TRANS_ID ?? 0);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", quotation.NET_AMOUNT ?? 0);
                            //cmd.Parameters.AddWithValue("@TERMS", string.IsNullOrWhiteSpace(quotation.TERMS) ? (object)DBNull.Value : quotation.TERMS);
                            //if (!string.IsNullOrWhiteSpace(quotation.TERMS) && quotation.TERMS.ToLower() != "string")
                            //    cmd.Parameters.AddWithValue("@TERMS", quotation.TERMS);
                            //else
                            //    cmd.Parameters.AddWithValue("@TERMS", DBNull.Value);
                            cmd.Parameters.AddWithValue("@NARRATION", quotation.NARRATION ?? "");

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SQTN_DETAIL", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_TB_SQTN_DETAIL";

                            // Add SQTN_TERMS TVP
                            SqlParameter tvpTermsParam = cmd.Parameters.AddWithValue("@SQTN_TERMS", tvpTerms);
                            tvpTermsParam.SqlDbType = SqlDbType.Structured;
                            tvpTermsParam.TypeName = "dbo.UDT_TB_SQTN_TERM";

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

        public QuotationDetailSelectResponse GetQuotation(int qtnId)
        {
            QuotationDetailSelectResponse response = new QuotationDetailSelectResponse 
            { Data = new QuotationSelect { Details = new List<QuotationDetailSelect>(), TERMS = new List<QuotationTerm>() } };
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@QTN_ID", qtnId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.Data.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                            response.Data.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0;
                            response.Data.STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : 0;
                            response.Data.QTN_NO = reader["QTN_NO"] != DBNull.Value ? reader["QTN_NO"].ToString() : null;
                            response.Data.QTN_DATE = reader["QTN_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["QTN_DATE"]).ToString("yyyy-MM-dd") : null;
                            response.Data.CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : 0;
                            response.Data.CUST_NAME = reader["CUSTOMER_NAME"] != DBNull.Value ? reader["CUSTOMER_NAME"].ToString() : null;
                            response.Data.SALESMAN_ID = reader["SALESMAN_ID"] != DBNull.Value ? Convert.ToInt32(reader["SALESMAN_ID"]) : 0;
                            response.Data.EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? reader["EMP_NAME"].ToString() : null;
                            response.Data.CONTACT_NAME = reader["CONTACT_NAME"] != DBNull.Value ? reader["CONTACT_NAME"].ToString() : null;
                            response.Data.SUBJECT = reader["SUBJECT"] != DBNull.Value ? reader["SUBJECT"].ToString() : null;
                            response.Data.REF_NO = reader["REF_NO"] != DBNull.Value ? reader["REF_NO"].ToString() : null;
                            response.Data.PAY_TERM_ID = reader["PAY_TERM_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TERM_ID"]) : 0;
                            response.Data.DELIVERY_TERM_ID = reader["DELIVERY_TERM_ID"] != DBNull.Value ? Convert.ToInt32(reader["DELIVERY_TERM_ID"]) : 0;
                            response.Data.VALID_DAYS = reader["VALID_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["VALID_DAYS"]) : 0;
                            response.Data.GROSS_AMOUNT = reader["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_AMOUNT"]) : 0;
                            response.Data.TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["TAX_AMOUNT"]) : 0;
                            response.Data.CHARGE_DESCRIPTION = reader["CHARGE_DESCRIPTION"] != DBNull.Value ? reader["CHARGE_DESCRIPTION"].ToString() : null;
                            response.Data.CHARGE_AMOUNT = reader["CHARGE_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["CHARGE_AMOUNT"]) : 0;
                            response.Data.DISCOUNT_DESCRIPTION = reader["DISCOUNT_DESCRIPTION"] != DBNull.Value ? reader["DISCOUNT_DESCRIPTION"].ToString() : null;
                            response.Data.PAY_TERM_NAME = reader["PAY_TERM_NAME"] != DBNull.Value ? reader["PAY_TERM_NAME"].ToString() : null;
                            response.Data.DELIVERY_TERM_NAME = reader["DELIVERY_TERM_NAME"] != DBNull.Value ? reader["DELIVERY_TERM_NAME"].ToString() : null;
                            response.Data.DISCOUNT_AMOUNT = reader["DISCOUNT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["DISCOUNT_AMOUNT"]) : 0;
                            response.Data.ROUND_OFF = reader["ROUND_OFF"] != DBNull.Value ? Convert.ToBoolean(reader["ROUND_OFF"]) : false;
                            response.Data.NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : 0;
                            response.Data.TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;
                            response.Data.NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null;
                            response.Data.STATUS = reader["TRANS_STATUS"] != DBNull.Value ? reader["TRANS_STATUS"].ToString() : null;
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                QuotationDetailSelect detail = new QuotationDetailSelect
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    QTN_ID = reader["QTN_ID"] != DBNull.Value ? Convert.ToInt32(reader["QTN_ID"]) : 0,
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
                                    REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null
                                };
                                response.Data.Details.Add(detail);
                            }
                        }
                        // Read terms
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                var term = new QuotationTerm
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    QTN_ID = reader["QTN_ID"] != DBNull.Value ? Convert.ToInt32(reader["QTN_ID"]) : 0,
                                    TERMS = reader["TERMS"] != DBNull.Value ? reader["TERMS"].ToString() : null
                                };

                                response.Data.TERMS.Add(term);
                            }
                        }
                    }
                }
            }
            response.Flag = 1;
            response.Message = "Success";
            return response;
        }

        public QuotationListResponse GetAllQuotations(QuotationListRequest request)
        {
            QuotationListResponse response = new QuotationListResponse { Data = new List<QuotationList>() };
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuotationList quotation = new QuotationList
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                QTN_DATE = reader["QTN_DATE"] != DBNull.Value ? reader["QTN_DATE"].ToString() : null,
                                QTN_NO = reader["QTN_NO"] != DBNull.Value ? reader["QTN_NO"].ToString() : null,
                                COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                STORE_ID = reader["STORE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STORE_ID"]) : (int?)null,
                                STORE_NAME = reader["STORE_NAME"] != DBNull.Value ? reader["STORE_NAME"].ToString() : null,
                                CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : (int?)null,
                                CUSTOMER_NAME = reader["CUSTOMER_NAME"] != DBNull.Value ? reader["CUSTOMER_NAME"].ToString() : null,
                                SALESMAN_ID = reader["SALESMAN_ID"] != DBNull.Value ? Convert.ToInt32(reader["SALESMAN_ID"]) : (int?)null,
                                CONTACT_NAME = reader["CONTACT_NAME"] != DBNull.Value ? reader["CONTACT_NAME"].ToString() : null,
                                SUBJECT = reader["SUBJECT"] != DBNull.Value ? reader["SUBJECT"].ToString() : null,
                                REF_NO = reader["REF_NO"] != DBNull.Value ? reader["REF_NO"].ToString() : null,
                                NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["NET_AMOUNT"]) : (float?)null,
                                TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null
                            };
                            response.Data.Add(quotation);
                        }
                    }
                }
            }
            response.Flag = 1;
            response.Message = "Success";
            return response;
        }

        public ItemListResponse GetQuotationItems(QuotationRequest request)
        {
            ItemListResponse response = new ItemListResponse { Data = new List<Item>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
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

        public bool DeleteQuotation(int qtnId)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@QTN_ID", qtnId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        public QuotationResponse ApproveQuotation(QuotationUpdate quotationUpdate)
        {
            QuotationResponse response = new QuotationResponse();
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

                        // Only add details if they are provided
                        if (quotationUpdate.Details != null && quotationUpdate.Details.Any())
                        {
                            foreach (var detail in quotationUpdate.Details)
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
                                    detail.REMARKS ?? ""
                                );
                            }
                        }
                        // Create DataTable for SQTN_TERMS
                        DataTable tvpTerms = new DataTable();
                        tvpTerms.Columns.Add("ID", typeof(int));
                        tvpTerms.Columns.Add("QTN_ID", typeof(int));
                        tvpTerms.Columns.Add("TERMS", typeof(string));

                        if (quotationUpdate.Terms != null && quotationUpdate.Terms.Any())
                        {
                            foreach (var term in quotationUpdate.Terms)
                            {
                                tvpTerms.Rows.Add(term.ID, term.QTN_ID, term.TERMS);
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 7);
                            cmd.Parameters.AddWithValue("@QTN_ID", quotationUpdate.ID ?? 0);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", quotationUpdate.COMPANY_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@STORE_ID", quotationUpdate.STORE_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@QTN_DATE", string.IsNullOrEmpty(quotationUpdate.QTN_DATE) ? (object)DBNull.Value : ParseDate(quotationUpdate.QTN_DATE));
                            cmd.Parameters.AddWithValue("@CUST_ID", quotationUpdate.CUST_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@SALESMAN_ID", quotationUpdate.SALESMAN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CONTACT_NAME", string.IsNullOrEmpty(quotationUpdate.CONTACT_NAME) ? (object)DBNull.Value : quotationUpdate.CONTACT_NAME);
                            cmd.Parameters.AddWithValue("@SUBJECT", string.IsNullOrEmpty(quotationUpdate.SUBJECT) ? (object)DBNull.Value : quotationUpdate.SUBJECT);
                            cmd.Parameters.AddWithValue("@REF_NO", string.IsNullOrEmpty(quotationUpdate.REF_NO) ? (object)DBNull.Value : quotationUpdate.REF_NO);
                            cmd.Parameters.AddWithValue("@PAY_TERM_ID", quotationUpdate.PAY_TERM_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", quotationUpdate.DELIVERY_TERM_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@VALID_DAYS", quotationUpdate.VALID_DAYS ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", quotationUpdate.GROSS_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TAX_AMOUNT", quotationUpdate.TAX_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", string.IsNullOrEmpty(quotationUpdate.CHARGE_DESCRIPTION) ? (object)DBNull.Value : quotationUpdate.CHARGE_DESCRIPTION);
                            cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", quotationUpdate.CHARGE_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@DISCOUNT_DESCRIPTION", string.IsNullOrEmpty(quotationUpdate.DISCOUNT_DESCRIPTION) ? (object)DBNull.Value : quotationUpdate.DISCOUNT_DESCRIPTION);
                            cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", quotationUpdate.DISCOUNT_AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@ROUND_OFF", quotationUpdate.ROUND_OFF ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@NET_AMOUNT", quotationUpdate.NET_AMOUNT ?? (object)DBNull.Value);
                            // cmd.Parameters.AddWithValue("@TERMS", string.IsNullOrWhiteSpace(quotationUpdate.TERMS) ? (object)DBNull.Value : quotationUpdate.TERMS);
                            //if (!string.IsNullOrWhiteSpace(quotationUpdate.TERMS) && quotationUpdate.TERMS.ToLower() != "string")
                            //    cmd.Parameters.AddWithValue("@TERMS", quotationUpdate.TERMS);
                            //else
                            //    cmd.Parameters.AddWithValue("@TERMS", DBNull.Value);
                            cmd.Parameters.AddWithValue("@NARRATION", string.IsNullOrEmpty(quotationUpdate.NARRATION) ? (object)DBNull.Value : quotationUpdate.NARRATION);
                            //cmd.Parameters.AddWithValue("@USER_ID", quotationUpdate.USER_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TRANS_TYPE", 10);

                            // Only add the TVP parameter if there are details
                            if (tvp.Rows.Count > 0)
                            {
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SQTN_DETAIL", tvp);
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SQTN_DETAIL";
                            }
                            else
                            {
                                // If no details are provided, pass an empty TVP or handle accordingly
                                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@SQTN_DETAIL", new DataTable());
                                tvpParam.SqlDbType = SqlDbType.Structured;
                                tvpParam.TypeName = "dbo.UDT_TB_SQTN_DETAIL";
                            }
                            // Add SQTN_TERMS TVP
                            SqlParameter tvpTermsParam = cmd.Parameters.AddWithValue("@SQTN_TERMS", tvpTerms);
                            tvpTermsParam.SqlDbType = SqlDbType.Structured;
                            tvpTermsParam.TypeName = "dbo.UDT_TB_SQTN_TERM";

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        response.Flag = "1";
                        response.Message = "Quotation approved and updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Flag = "0";
                        response.Message = "Error approving quotation: " + ex.Message;
                    }
                }
            }
            return response;
        }
        public TERMSLISTResponse GetTERMSLIST()
        {
            TERMSLISTResponse response = new TERMSLISTResponse { Data = new List<TERMSLIST>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 8);
 
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TERMSLIST term = new TERMSLIST
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    QTN_ID = reader["QTN_ID"] != DBNull.Value ? Convert.ToInt32(reader["QTN_ID"]) : (int?)null,
                                    TERMS = reader["TERMS"] != DBNull.Value ? reader["TERMS"].ToString() : null,
                                };
                                response.Data.Add(term);
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
        public QuotationHistoryResponse GetQuotationHistoryByItemId(int itemId)
        {
            QuotationHistoryResponse response = new QuotationHistoryResponse { Data = new List<QuotationHistory>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ITEM_ID", itemId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QuotationHistory history = new QuotationHistory
                                {
                                    ITEM_ID = reader["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(reader["ITEM_ID"]) : 0,
                                    QTN_NO = reader["QTN_NO"] != DBNull.Value ? reader["QTN_NO"].ToString() : null,
                                    QTN_DATE = reader["QTN_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["QTN_DATE"]) : DateTime.MinValue,
                                    CUST_NAME = reader["CUST_NAME"] != DBNull.Value ? reader["CUST_NAME"].ToString() : null,
                                    REF_NO = reader["REF_NO"] != DBNull.Value ? reader["REF_NO"].ToString() : null,
                                    QUANTITY = reader["QUANTITY"] != DBNull.Value ? Convert.ToSingle(reader["QUANTITY"]) : 0,
                                    UOM = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : null,
                                    UNIT_PRICE = reader["UNIT_PRICE"] != DBNull.Value ? Convert.ToSingle(reader["UNIT_PRICE"]) : 0,
                                    DISC_PERCENT = reader["DISC_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DISC_PERCENT"]) : 0,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["AMOUNT"]) : 0
                                };
                                response.Data.Add(history);
                            }
                        }
                    }
                    response.Flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = null;
            }
            return response;
        }
        public LatestVocherResponse GetLatestVoucherNumber()
        {
            LatestVocherResponse response = new LatestVocherResponse { Data = new List<LatestVocher>() };
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_QUOTATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 9);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LatestVocher vocher = new LatestVocher
                                {
                                    VOCHERNO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                };
                                response.Data.Add(vocher);
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
