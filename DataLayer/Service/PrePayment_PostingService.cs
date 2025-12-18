using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class PrePayment_PostingService : IPrePayment_PostingService
    {
        public PrePayment_RequestResponse GetPrePaymentPendingList(PrePayment_PostingRequest request)
        {
            PrePayment_RequestResponse response = new PrePayment_RequestResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PrePayment_RequestList>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_PREPAYMENT_POSTING", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DUE_DATE", request.DUE_DATE);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PrePayment_RequestList item = new PrePayment_RequestList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    INVOICE_NO = reader["VOUCHER_NO"]?.ToString(),
                                    INVOICE_DATE = reader["DUE_DATE"] != DBNull.Value
                                                    ? Convert.ToDateTime(reader["DUE_DATE"])
                                                    : DateTime.MinValue,
                                    DR_LEDGER = reader["DR_LEDGER"]?.ToString(),
                                    CR_LEDGER = reader["CR_LEDGER"]?.ToString(),
                                    DUE_AMOUNT = reader["DUE_AMOUNT"] != DBNull.Value
                                                    ? Convert.ToDouble(reader["DUE_AMOUNT"])
                                                    : 0,
                                    NARRATION = reader["NARRATION"]?.ToString()
                                };
                                response.Data.Add(item);
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
                response.Data = new List<PrePayment_RequestList>();
            }

            return response;
        }
        public PrepaymentPostingResponse Save(PrePayment_Posting model)
        {
            var response = new PrepaymentPostingResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_PREPAYMENT_POSTING", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // SP Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 39); 
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        // Create DataTable for UDT_TB_PREPAY_POSTING
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("ID", typeof(int));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));
                        dtPrepayDetail.Columns.Add("DUE_DATE", typeof(DateTime));
                        dtPrepayDetail.Columns.Add("SL_NO", typeof(int));
                        dtPrepayDetail.Columns.Add("DR_AMOUNT", typeof(decimal));
                        dtPrepayDetail.Columns.Add("CR_AMOUNT", typeof(decimal));

                        int slNo = 1;
                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.ID,
                                    item.DUE_AMOUNT,
                                    item.DUE_DATE,
                                    slNo++,
                                    0M,
                                    0M
                                );
                            }
                        }

                        SqlParameter tvpPrepayDetail = cmd.Parameters.AddWithValue("@UDT_TB_PREPAY_POSTING", dtPrepayDetail);
                        tvpPrepayDetail.SqlDbType = SqlDbType.Structured;
                        tvpPrepayDetail.TypeName = "UDT_TB_PREPAY_POSTING";

                        // Execute SP
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
        public PrepaymentPostingResponse Edit(PrePayment_PostingEdit model)
        {
            var response = new PrepaymentPostingResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_PREPAYMENT_POSTING", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // SP Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 39);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        // Create DataTable for UDT_TB_PREPAY_POSTING
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("ID", typeof(int));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));
                        dtPrepayDetail.Columns.Add("DUE_DATE", typeof(DateTime));
                        dtPrepayDetail.Columns.Add("SL_NO", typeof(int));
                        dtPrepayDetail.Columns.Add("DR_AMOUNT", typeof(decimal)); 
                        dtPrepayDetail.Columns.Add("CR_AMOUNT", typeof(decimal)); 

                        int slNo = 1;
                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.ID,
                                    item.DUE_AMOUNT,
                                    item.DUE_DATE,
                                    slNo++,
                                    item.DR_AMOUNT,
                                    item.CR_AMOUNT
                                );
                            }
                        }

                        SqlParameter tvpPrepayDetail = cmd.Parameters.AddWithValue("@UDT_TB_PREPAY_POSTING", dtPrepayDetail);
                        tvpPrepayDetail.SqlDbType = SqlDbType.Structured;
                        tvpPrepayDetail.TypeName = "UDT_TB_PREPAY_POSTING";

                        // Execute SP
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
        public PrePayment_PostingListResponse GetPrePaymentList(PrepaytListRequest request)
        {
            var response = new PrePayment_PostingListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PrePayment_PostingListHeader>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_PREPAYMENT_POSTING", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 39);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Dictionary<int, PrePayment_PostingListHeader> headerMap = new Dictionary<int, PrePayment_PostingListHeader>();

                            while (reader.Read())
                            {
                                int transId = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0;

                                if (!headerMap.ContainsKey(transId))
                                {
                                    headerMap[transId] = new PrePayment_PostingListHeader
                                    {
                                        TRANS_ID = transId,
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        DOC_NO = reader["DOC_NO"]?.ToString(),
                                        INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                        TRANS_STATUS = reader["TRANS_STATUS"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0,
                                        Details = new List<PrePayment_PostingListDetail>()
                                    };
                                }

                                // Add detail to header
                                headerMap[transId].Details.Add(new PrePayment_PostingListDetail
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
        public PostingSelectResponse GetPrePaymentById(int id)
        {
            var response = new PostingSelectResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PostingSelect>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_PREPAYMENT_POSTING", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 39);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            PostingSelect header = null;

                            while (reader.Read())
                            {
                                if (header == null)
                                {
                                    header = new PostingSelect
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        TRANS_TYPE = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0,
                                        DOC_NO = reader["DOC_NO"]?.ToString(),
                                        INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                        TRANS_STATUS = reader["TRANS_STATUS"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0,
                                        PREPAY_DETAIL = new List<PostingSelectDetail>()
                                    };
                                }

                                // Add each detail row
                                header.PREPAY_DETAIL.Add(new PostingSelectDetail
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    DUE_DATE = reader["DUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DUE_DATE"]).ToString("yyyy-MM-dd") : null,
                                    DUE_AMOUNT = reader["DUE_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DUE_AMOUNT"]) : 0,
                                    DR_AMOUNT = reader["DR_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["DR_AMOUNT"]) : 0,
                                    CR_AMOUNT = reader["CR_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["CR_AMOUNT"]) : 0
                                });
                            }

                            if (header != null)
                            {
                                response.Data.Add(header);
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

        public PrepaymentPostingResponse commit(PrePayment_PostingEdit model)
        {
            var response = new PrepaymentPostingResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_PREPAYMENT_POSTING", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // SP Parameters
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 39);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        // Create DataTable for UDT_TB_PREPAY_POSTING
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("ID", typeof(int));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));
                        dtPrepayDetail.Columns.Add("DUE_DATE", typeof(DateTime));
                        dtPrepayDetail.Columns.Add("SL_NO", typeof(int));
                        dtPrepayDetail.Columns.Add("DR_AMOUNT", typeof(decimal));
                        dtPrepayDetail.Columns.Add("CR_AMOUNT", typeof(decimal));

                        int slNo = 1;
                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.ID,
                                    item.DUE_AMOUNT,
                                    item.DUE_DATE,
                                    slNo++,
                                    item.DR_AMOUNT,
                                    item.CR_AMOUNT
                                );
                            }
                        }

                        SqlParameter tvpPrepayDetail = cmd.Parameters.AddWithValue("@UDT_TB_PREPAY_POSTING", dtPrepayDetail);
                        tvpPrepayDetail.SqlDbType = SqlDbType.Structured;
                        tvpPrepayDetail.TypeName = "UDT_TB_PREPAY_POSTING";

                        // Execute SP
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
        public PrepaymentPostingResponse Delete(int id)
        {
            PrepaymentPostingResponse res = new PrepaymentPostingResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_PREPAYMENT_POSTING";

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
    }
}
