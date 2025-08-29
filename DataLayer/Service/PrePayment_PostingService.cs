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
                        cmd.Parameters.AddWithValue("@TRANS_DATE", model.TRANS_DATE ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        // Create DataTable for UDT_TB_PREPAY_POSTING
                        DataTable dtPrepayDetail = new DataTable();
                        dtPrepayDetail.Columns.Add("ID", typeof(int));
                        dtPrepayDetail.Columns.Add("DUE_AMOUNT", typeof(decimal));
                        dtPrepayDetail.Columns.Add("SL_NO", typeof(int)); 

                        int slNo = 1;
                        if (model.PREPAY_DETAIL != null)
                        {
                            foreach (var item in model.PREPAY_DETAIL)
                            {
                                dtPrepayDetail.Rows.Add(
                                    item.ID,
                                    item.DUE_AMOUNT,
                                    slNo++
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
         public PrePayment_PostingListResponse GetPrePaymentList()
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
                                        VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                        TRANS_DATE = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).ToString("dd-MM-yyyy") : null,
                                        TRANS_STATUS = reader["TRANS_STATUS"]?.ToString(),
                                        ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                        SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                        SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                        EXP_HEAD_ID = reader["EXP_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["EXP_HEAD_ID"]) : 0,
                                        PREPAY_HEAD_ID = reader["PREPAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PREPAY_HEAD_ID"]) : 0,
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
    }
}
