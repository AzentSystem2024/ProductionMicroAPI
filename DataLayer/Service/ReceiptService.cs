﻿using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class ReceiptService:IReceiptService
    {
        public ReceiptResponse insert(Receipt model)
        {
            ReceiptResponse response = new ReceiptResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.Add("@TRANS_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@REC_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@REC_NO", model.REC_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@REC_DATE", ParseDate(model.REC_DATE));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? false);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", model.DISTRIBUTOR_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", ParseDate(model.ADD_TIME));
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // UDT: UDT_CUST_REC_DETAIL
                        DataTable dt = new DataTable();
                        dt.Columns.Add("BILL_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));

                        foreach (var item in model.REC_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_CUST_REC_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_CUST_REC_DETAIL";

                        // Execute
                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }
        public ReceiptResponse Update(ReceiptUpdate model)
        {
            ReceiptResponse response = new ReceiptResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2); // Action for update
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? 0);
                        cmd.Parameters.AddWithValue("@REC_ID", model.REC_ID ?? 0);

                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@REC_NO", model.REC_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@REC_DATE", ParseDate(model.REC_DATE));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_STATUS", model.TRANS_STATUS ?? 0);
                        cmd.Parameters.AddWithValue("@RECEIPT_NO", model.RECEIPT_NO ?? 0);
                        cmd.Parameters.AddWithValue("@IS_DIRECT", model.IS_DIRECT ?? false);
                        cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", model.CHEQUE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(model.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@BANK_NAME", model.BANK_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@RECON_DATE", ParseDate(model.RECON_DATE));
                        cmd.Parameters.AddWithValue("@PDC_ID", model.PDC_ID ?? 0);
                        cmd.Parameters.AddWithValue("@IS_CLOSED", model.IS_CLOSED ?? false);
                        cmd.Parameters.AddWithValue("@UNIT_ID", model.UNIT_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", model.DISTRIBUTOR_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_ID", model.PARTY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PARTY_NAME", model.PARTY_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PARTY_REF_NO", model.PARTY_REF_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_PASSED", model.IS_PASSED ?? false);
                        cmd.Parameters.AddWithValue("@SCHEDULE_NO", model.SCHEDULE_NO ?? 0);
                        cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREATE_USER_ID", model.CREATE_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VERIFY_USER_ID", model.VERIFY_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE1_USER_ID", model.APPROVE1_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE2_USER_ID", model.APPROVE2_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@APPROVE3_USER_ID", model.APPROVE3_USER_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TYPE_ID", model.PAY_TYPE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_HEAD_ID", model.PAY_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@ADD_TIME", ParseDate(model.ADD_TIME));
                        cmd.Parameters.AddWithValue("@CREATED_STORE_ID", model.CREATED_STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? 0);

                        // UDT: UDT_CUST_REC_DETAIL
                        DataTable dt = new DataTable();
                        dt.Columns.Add("BILL_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(double));

                        foreach (var item in model.REC_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_CUST_REC_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_CUST_REC_DETAIL";

                        cmd.ExecuteNonQuery();

                        response.flag = 1;
                        response.Message = "Receipt updated successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }

        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[]
            {
                "dd-MM-yyyy HH:mm:ss",
                "dd-MM-yyyy",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-dd",
                "MM/dd/yyyy HH:mm:ss",
                "MM/dd/yyyy"
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public PendingInvoiceListResponse GetPendingInvoiceList()
        {
            PendingInvoiceListResponse response = new PendingInvoiceListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PendingInvoiceItem>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 25);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PendingInvoiceItem item = new PendingInvoiceItem
                                {
                                    INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                    INVOICE_DATE = reader["INVOICE_DATE"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["INVOICE_DATE"]).ToString("dd-MM-yyyy")
                                        : null,
                                    REF_NO = reader["REF_NO"]?.ToString(),
                                    NARRATION = reader["NARRATION"]?.ToString(),
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    SETTLED_TILL_DATE = reader["SETTLED_TILL_DATE"] != DBNull.Value ? Convert.ToDouble(reader["SETTLED_TILL_DATE"]) : 0,
                                    PENDING_AMOUNT = reader["PENDING_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["PENDING_AMOUNT"]) : 0,
                                    BILL_ID = reader["BILL_ID"] != DBNull.Value ? Convert.ToInt32(reader["BILL_ID"]) : 0
                                };

                                response.Data.Add(item);
                            }

                            response.flag = 1;
                            response.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = new List<PendingInvoiceItem>();
            }

            return response;
        }
        public ReceiptListResponse GetReceiptList()
        {
            ReceiptListResponse response = new ReceiptListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<ReceiptListItem>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0); 
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 27);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReceiptListItem item = new ReceiptListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                    REC_DATE = reader["REC_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["REC_DATE"]).ToString("dd-MM-yyyy") : null,
                                   // UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    DISTRIBUTOR_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                    NARRATION = reader["NARRATION"]?.ToString(),
                                    PAY_TYPE_ID = reader["PAY_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TYPE_ID"]) : 0,
                                    PAY_TYPE_NAME = reader["PAY_TYPE_NAME"]?.ToString(),
                                    PAY_HEAD_ID = reader["PAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_HEAD_ID"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    RECEIVED_AMOUNT = reader["RECEIVED_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["RECEIVED_AMOUNT"]) : 0,
                                    CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                    CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    BANK_NAME = reader["BANK_NAME"]?.ToString(),
                                };

                                response.Data.Add(item);
                            }

                            response.flag = 1;
                            response.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = new List<ReceiptListItem>();
            }

            return response;
        }
        public ReceiptSelectResponse GetReceiptById(int id)
        {
            var response = new ReceiptSelectResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<ReceiptSelect>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 27);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            ReceiptSelect receipt = null;
                            var recDetails = new List<ReceiptDetail>();
                            var pendingDetails = new List<ReceiptDetail>();

                            while (reader.Read())
                            {
                                if (receipt == null)
                                {
                                    receipt = new ReceiptSelect
                                    {
                                        TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                        TRANS_TYPE = reader["TRANS_TYPE"] as int? ?? 0,
                                        REC_DATE = reader["REC_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["REC_DATE"]).ToString("dd-MM-yyyy") : null,
                                        COMPANY_ID = reader["COMPANY_ID"] as int? ?? 0,
                                        STORE_ID = reader["STORE_ID"] as int? ?? 0,
                                        FIN_ID = reader["FIN_ID"] as int? ?? 0,
                                        TRANS_STATUS = reader["TRANS_STATUS"] as int? ?? 0,
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        DISTRIBUTOR_ID = reader["CUSTOMER_ID"] as int? ?? 0,
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        PAY_TYPE_ID = reader["PAY_TYPE_ID"] as int? ?? 0,
                                        PAY_HEAD_ID = reader["PAY_HEAD_ID"] as int? ?? 0,
                                        ADD_TIME = reader["ADD_TIME"] != DBNull.Value ? Convert.ToDateTime(reader["ADD_TIME"]).ToString("dd-MM-yyyy") : null,
                                        NET_AMOUNT = reader["NET_AMOUNT"] as decimal? ?? 0,
                                        CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                        CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        BANK_NAME = reader["BANK_NAME"]?.ToString(),
                                        REC_DETAIL = new List<ReceiptDetail>(),
                                    };
                                }

                                int billId = reader["BILL_ID"] as int? ?? 0;

                                if (billId > 0)
                                {
                                    ReceiptDetail detail = new ReceiptDetail
                                    {
                                        BILL_ID = billId,
                                        AMOUNT = reader["AMOUNT"] as double? ?? 0.0,
                                        //SL_NO = reader["SL_NO"] != DBNull.Value ? Convert.ToInt32(reader["SL_NO"]) : 0,
                                        INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                        INVOICE_DATE = reader["INVOICE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["INVOICE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        NET_AMOUNT = reader["NET_AMOUNT"] as double? ?? 0,
                                        SETTLED_TILL_DATE = reader["SETTLED_TILL_DATE"] as double? ?? 0,
                                        PENDING_AMOUNT = reader["PENDING_AMOUNT"] as double? ?? 0
                                    };

                                    if ((detail.PENDING_AMOUNT > 0 || detail.NET_AMOUNT > 0) && detail.AMOUNT == 0)
                                        pendingDetails.Add(detail);
                                    else
                                        recDetails.Add(detail);
                                }
                            }

                            if (receipt != null)
                            {
                                receipt.REC_DETAIL = recDetails;
                                response.Data.Add(receipt);
                                response.flag = 1;
                                response.Message = "Success";
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




        public ReceiptResponse CommitReceipt(CommitReceiptRequest request)
        {
            ReceiptResponse response = new ReceiptResponse();

            try
            {
                if (request.IS_APPROVED && request.TRANS_ID > 0)
                {
                    using (SqlConnection con = ADO.GetConnection())
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ACTION", 3);
                            cmd.Parameters.AddWithValue("@TRANS_ID", request.TRANS_ID);

                            cmd.ExecuteNonQuery();

                            response.flag = 1;
                            response.Message = "Receipt committed successfully.";
                        }
                    }
                }
                else
                {
                    response.flag = 0;
                    response.Message = "Invalid TRANS_ID or IS_APPROVED is false.";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public RecResponse GetReceiptNo()
        {
            RecResponse res = new RecResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 27 
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.RECEIPT_NO = result != null ? Convert.ToInt32(result) : 0;
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
        public ReceiptResponse Delete(int id)
        {
            ReceiptResponse res = new ReceiptResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_CUST_RECEIPT";

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

        public ReceiptLedgerListResponse GetLedgerList()
        {
            ReceiptLedgerListResponse response = new ReceiptLedgerListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<ReceiptLedgerList>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_CUST_RECEIPT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 27);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReceiptLedgerList item = new ReceiptLedgerList
                                {
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                    HEAD_NAME = reader["HEAD_NAME"]?.ToString(),
                                    GROUP_ID = reader["GROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["GROUP_ID"]) : 0
                                   
                                };

                                response.Data.Add(item);
                            }

                            response.flag = 1;
                            response.Message = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = new List<ReceiptLedgerList>();
            }

            return response;
        }
    }
}
