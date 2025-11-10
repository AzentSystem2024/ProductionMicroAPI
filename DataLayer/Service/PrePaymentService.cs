using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class PrePaymentService:IPrePaymentService
    {
        public PrePaymentResponse Save(PrePayment model)
        {
            PrePaymentResponse response = new PrePaymentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PREPAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_PERCENT", model.TAX_PERCENT ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@PREPAY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? 0);
                        cmd.Parameters.AddWithValue("@EXP_HEAD_ID", model.EXP_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PREPAY_HEAD_ID", model.PREPAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DATE_FROM", model.DATE_FROM ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_TO", model.DATE_TO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_DAYS", model.NO_OF_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@EXPENSE_AMOUNT", model.EXPENSE_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NO_OF_MONTHS", model.NO_OF_MONTHS ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);

                        // UDT for TB_PREPAY_DETAIL
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("DUE_DATE", typeof(DateTime));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));

                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.DUE_DATE ?? DateTime.MinValue,
                                    item.DUE_AMOUNT ?? 0
                                );
                            }
                        }

                        SqlParameter tvpPrepayDetail = cmd.Parameters.AddWithValue("@UDT_TB_PREPAY_DETAIL", dtPrepayDetail);
                        tvpPrepayDetail.SqlDbType = SqlDbType.Structured;
                        tvpPrepayDetail.TypeName = "UDT_TB_PREPAY_DETAIL";                   

                        // Execute
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Success.";
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
        public PrePaymentResponse Update(PrePaymentUpdate model)
        {
            PrePaymentResponse response = new PrePaymentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PREPAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Main parameters for ACTION = 2
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_PERCENT", model.TAX_PERCENT ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? 0);
                        cmd.Parameters.AddWithValue("@EXP_HEAD_ID", model.EXP_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PREPAY_HEAD_ID", model.PREPAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DATE_FROM", model.DATE_FROM ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_TO", model.DATE_TO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_DAYS", model.NO_OF_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@EXPENSE_AMOUNT", model.EXPENSE_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NO_OF_MONTHS", model.NO_OF_MONTHS ?? 0);
                        cmd.Parameters.AddWithValue("@PREPAY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);


                        // UDT for TB_PREPAY_DETAIL
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("DUE_DATE", typeof(DateTime));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));

                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.DUE_DATE ?? DateTime.MinValue,
                                    item.DUE_AMOUNT ?? 0                                );
                            }
                        }

                        SqlParameter tvpPrepayDetail = cmd.Parameters.AddWithValue("@UDT_TB_PREPAY_DETAIL", dtPrepayDetail);
                        tvpPrepayDetail.SqlDbType = SqlDbType.Structured;
                        tvpPrepayDetail.TypeName = "UDT_TB_PREPAY_DETAIL";

                        // Execute update
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Update successful.";
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
        public PrePaymentListResponse GetPrePaymentList()
        {
            var response = new PrePaymentListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PrePaymentListHeader>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PREPAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 38);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_PERCENT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PREPAY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EXP_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PREPAY_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_FROM", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_TO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_DAYS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EXPENSE_AMOUNT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_MONTHS", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Dictionary<int, PrePaymentListHeader> headerMap = new Dictionary<int, PrePaymentListHeader>();

                            while (reader.Read())
                            {
                                int transId = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;

                                if (!headerMap.ContainsKey(transId))
                                {
                                    headerMap[transId] = new PrePaymentListHeader
                                    {
                                        TRANS_ID = transId,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                        TRANS_STATUS = reader["TRANS_STATUS"]?.ToString(),
                                        ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                        SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                        SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                        EXP_HEAD_ID = reader["EXP_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["EXP_HEAD_ID"]) : 0,
                                        PREPAY_HEAD_ID = reader["PREPAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PREPAY_HEAD_ID"]) : 0,
                                        DATE_FROM = reader["DATE_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["DATE_FROM"]).ToString("dd-MM-yyyy") : null,
                                        NO_OF_MONTHS = reader["NO_OF_MONTHS"] != DBNull.Value ? Convert.ToInt32(reader["NO_OF_MONTHS"]) : 0,
                                        NO_OF_DAYS = reader["NO_OF_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["NO_OF_DAYS"]) : 0,
                                        DATE_TO = reader["DATE_TO"] != DBNull.Value ? Convert.ToDateTime(reader["DATE_TO"]).ToString("dd-MM-yyyy") : null,
                                        EXPENSE_AMOUNT = reader["EXPENSE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["EXPENSE_AMOUNT"]) : 0,
                                        TAX_PERCENT = reader["TAX_PERCENT"] != DBNull.Value ? Convert.ToDouble(reader["TAX_PERCENT"]) : 0,
                                        TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0,
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        Details = new List<PrePaymentListDetail>()
                                    };
                                }

                                // Add detail to header
                                headerMap[transId].Details.Add(new PrePaymentListDetail
                                {
                                    DUE_DATE = reader["DUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    DUE_AMOUNT = reader["DUE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DUE_AMOUNT"]) : 0
                                });
                            }

                            response.Data = headerMap.Values.ToList();
                            response.flag = 1;
                            response.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = -1;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }

        public PrePaymentListHeaderResponse GetPrePaymentById(int id)
        {
            var response = new PrePaymentListHeaderResponse
            {
                flag = 0,
                Message = "Failed",
                Data = null
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PREPAYMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); 
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FIN_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 38);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_PERCENT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PREPAY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EXP_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PREPAY_HEAD_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_FROM", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_TO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_DAYS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EXPENSE_AMOUNT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_MONTHS", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            PrePaymentListHeader header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new PrePaymentListHeader
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        TRANS_TYPE = Convert.ToInt32(reader["TRANS_TYPE"]),
                                        VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ?
                                                     Convert.ToDateTime(reader["TRANS_DATE"]).ToString("yyyy-MM-dd") : null,
                                        TRANS_STATUS = reader["TRANS_STATUS"]?.ToString(),
                                        ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                        SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                        EXP_HEAD_ID = reader["EXP_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["EXP_HEAD_ID"]) : 0,
                                        PREPAY_HEAD_ID = reader["PREPAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PREPAY_HEAD_ID"]) : 0,
                                        DATE_FROM = reader["DATE_FROM"] != DBNull.Value ?
                                                    Convert.ToDateTime(reader["DATE_FROM"]).ToString("yyyy-MM-dd") : null,
                                        NO_OF_MONTHS = reader["NO_OF_MONTHS"] != DBNull.Value ? Convert.ToInt32(reader["NO_OF_MONTHS"]) : 0,
                                        NO_OF_DAYS = reader["NO_OF_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["NO_OF_DAYS"]) : 0,
                                        DATE_TO = reader["DATE_TO"] != DBNull.Value ?
                                                  Convert.ToDateTime(reader["DATE_TO"]).ToString("yyyy-MM-dd") : null,
                                        EXPENSE_AMOUNT = reader["EXPENSE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["EXPENSE_AMOUNT"]) : 0,
                                        TAX_PERCENT = reader["TAX_PERCENT"] != DBNull.Value ? Convert.ToDouble(reader["TAX_PERCENT"]) : 0,
                                        TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0,
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        PARTY_NAME = reader["PARTY_NAME"]?.ToString(),
                                        Details = new List<PrePaymentListDetail>()
                                    };
                                }

                                // Add detail row
                                header.Details.Add(new PrePaymentListDetail
                                {
                                    DUE_DATE = reader["DUE_DATE"] != DBNull.Value ?
                                               Convert.ToDateTime(reader["DUE_DATE"]).ToString("yyyy-MM-dd") : null,
                                    DUE_AMOUNT = reader["DUE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DUE_AMOUNT"]) : 0
                                });
                            }

                            if (header != null)
                            {
                                response.Data = header;
                                response.flag = 1;
                                response.Message = "Success";
                            }
                            else
                            {
                                response.flag = 0;
                                response.Message = "No record found.";
                            }
                        }
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
        public PrePaymentResponse commit(PrePaymentUpdate model)
        {
            PrePaymentResponse response = new PrePaymentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PREPAYMENT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Main parameters for ACTION = 2
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_PERCENT", model.TAX_PERCENT ?? 0);
                        cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? 0);
                        cmd.Parameters.AddWithValue("@EXP_HEAD_ID", model.EXP_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PREPAY_HEAD_ID", model.PREPAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DATE_FROM", model.DATE_FROM ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DATE_TO", model.DATE_TO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_OF_DAYS", model.NO_OF_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@EXPENSE_AMOUNT", model.EXPENSE_AMOUNT ?? 0);
                        cmd.Parameters.AddWithValue("@NO_OF_MONTHS", model.NO_OF_MONTHS ?? 0);
                        cmd.Parameters.AddWithValue("@PREPAY_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);


                        // UDT for TB_PREPAY_DETAIL
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("DUE_DATE", typeof(DateTime));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));

                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.DUE_DATE ?? DateTime.MinValue,
                                    item.DUE_AMOUNT ?? 0);
                            }
                        }

                        SqlParameter tvpPrepayDetail = cmd.Parameters.AddWithValue("@UDT_TB_PREPAY_DETAIL", dtPrepayDetail);
                        tvpPrepayDetail.SqlDbType = SqlDbType.Structured;
                        tvpPrepayDetail.TypeName = "UDT_TB_PREPAY_DETAIL";

                        // Execute update
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Commit successfully.";
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
        public PrePaymentResponse Delete(int id)
        {
            PrePaymentResponse res = new PrePaymentResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_PREPAYMENT";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        public PrePaymentLastDocno GetLastDocNo()
        {
            PrePaymentLastDocno res = new PrePaymentLastDocno();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 38
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.INVOICE_NO= result != null ? Convert.ToInt32(result) : 0;
                        res.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
