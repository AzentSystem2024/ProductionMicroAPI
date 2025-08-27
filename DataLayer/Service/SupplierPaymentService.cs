using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SupplierPaymentService:ISupplierPaymentService
    {
        public SupplierPaymentResponse insert(SupplierPayment model)
        {
            SupplierPaymentResponse response = new SupplierPaymentResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.Add("@TRANS_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@PAY_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
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
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPPLIER_ID ?? 0);
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

                        foreach (var item in model.SUPP_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SUPP_PAY_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SUPP_PAY_DETAIL";

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
        public SupplierPaymentResponse Update(SupplierPaymentUpdate model)
        {
            SupplierPaymentResponse response = new SupplierPaymentResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2); 
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? 0);
                       // cmd.Parameters.AddWithValue("@PAY_ID", model.PAY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
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
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPPLIER_ID ?? 0);
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

                        foreach (var item in model.SUPP_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SUPP_PAY_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SUPP_PAY_DETAIL";

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
        public SupplierPaymentListResponse GetPaymentList()
        {
            SupplierPaymentListResponse response = new SupplierPaymentListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<SupplierPaymentListItem>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 21);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SupplierPaymentListItem item = new SupplierPaymentListItem
                                {
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    VOUCHER_NO = reader["VOUCHER_NO"]?.ToString(),
                                    TRANS_STATUS = reader["TRANS_STATUS"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_STATUS"]) : (int?)null,
                                    PAY_DATE = reader["PAY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PAY_DATE"]).ToString("dd-MM-yyyy") : null,
                                    // UNIT_ID = reader["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(reader["UNIT_ID"]) : 0,
                                    SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                    NARRATION = reader["NARRATION"]?.ToString(),
                                    PAY_TYPE_ID = reader["PAY_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_TYPE_ID"]) : 0,
                                    PAY_TYPE_NAME = reader["PAY_TYPE_NAME"]?.ToString(),
                                    PAY_HEAD_ID = reader["PAY_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["PAY_HEAD_ID"]) : 0,
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    RECEIVED_AMOUNT = reader["RECEIVED_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["RECEIVED_AMOUNT"]) : 0,
                                    CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                    CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    BANK_NAME = reader["BANK_NAME"]?.ToString(),
                                    SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                    PDC_ID = reader["PDC_ID"] != DBNull.Value ? Convert.ToInt32(reader["PDC_ID"]) : 0

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
                response.Data = new List<SupplierPaymentListItem>();
            }

            return response;
        }
        public SupplierSelectResponse GetSupplierById(int id)
        {
            var response = new SupplierSelectResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<SupplierPaymentSelect>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 21); 

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            SupplierPaymentSelect receipt = null;
                            var detailList = new List<SupplierDetail>();

                            while (reader.Read())
                            {
                                if (receipt == null)
                                {
                                    receipt = new SupplierPaymentSelect
                                    {
                                        TRANS_ID = Convert.ToInt32(reader["TRANS_ID"]),
                                        TRANS_TYPE = reader["TRANS_TYPE"] as int? ?? 0,
                                        SUPPLIER_NO = reader["VOUCHER_NO"] != DBNull.Value ? Convert.ToInt32(reader["VOUCHER_NO"]) : 0,
                                        PAY_DATE = reader["PAY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PAY_DATE"]).ToString("dd-MM-yyyy") : null,
                                        COMPANY_ID = reader["COMPANY_ID"] as int? ?? 0,
                                        FIN_ID = reader["FIN_ID"] as int? ?? 0,
                                        TRANS_STATUS = reader["TRANS_STATUS"] as int? ?? 0,
                                        REF_NO = reader["REF_NO"]?.ToString(),
                                        SUPP_ID = reader["SUPP_ID"] as int? ?? 0,
                                        NARRATION = reader["NARRATION"]?.ToString(),
                                        PAY_TYPE_ID = reader["PAY_TYPE_ID"] as int? ?? 0,
                                        PAY_HEAD_ID = reader["PAY_HEAD_ID"] as int? ?? 0,
                                        ADD_TIME = reader["ADD_TIME"] != DBNull.Value ? Convert.ToDateTime(reader["ADD_TIME"]).ToString("dd-MM-yyyy") : null,
                                        NET_AMOUNT = reader["NET_AMOUNT"] as decimal? ?? 0,
                                        CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                        CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                        BANK_NAME = reader["BANK_NAME"]?.ToString(),
                                        PDC_ID = reader["PDC_ID"] != DBNull.Value ? Convert.ToInt32(reader["PDC_ID"]) : 0,
                                        PAY_DETAIL = new List<SupplierDetail>()
                                    };
                                }

                                if (reader["BILL_ID"] != DBNull.Value)
                                {
                                    var detail = new SupplierDetail
                                    {
                                        BILL_ID = reader["BILL_ID"] as int? ?? 0,
                                        AMOUNT = reader["AMOUNT"] as double? ?? 0,
                                        DOC_NO = reader["DOC_NO"]?.ToString(),
                                        PURCH_DATE = reader["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCH_DATE"]).ToString("dd-MM-yyyy") : null,
                                        TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] as double? ?? 0,
                                        PENDING_AMOUNT = reader["PENDING_AMOUNT"] as double? ?? 0,
                                        SUPP_INV_DATE = reader["SUPP_INV_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SUPP_INV_DATE"]).ToString("dd-MM-yyyy") : null,
                                        SUPP_INV_NO = reader["SUPP_INV_NO"]?.ToString(),
                                        NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    };

                                    detailList.Add(detail);
                                }
                            }

                            if (receipt != null)
                            {
                                receipt.PAY_DETAIL = detailList;
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
        public PendingInvoiceResponse GetPendingInvoiceList(PendingInvoiceRequest request)
        {
            PendingInvoiceResponse response = new PendingInvoiceResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PendingInvoicelist>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", 19);
                        cmd.Parameters.AddWithValue("@SUPP_ID", request.SUPP_ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PendingInvoicelist item = new PendingInvoicelist
                                {
                                    BILL_ID = reader["BILL_ID"] != DBNull.Value ? Convert.ToInt32(reader["BILL_ID"]) : 0,
                                    DOC_NO = reader["DOC_NO"]?.ToString(),
                                    PURCH_DATE = reader["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCH_DATE"]).ToString("dd-MM-yyyy"): null,
                                    SUPP_INV_DATE = reader["SUPP_INV_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["SUPP_INV_DATE"]).ToString("dd-MM-yyyy") : null,
                                    SUPP_INV_NO = reader["SUPP_INV_NO"]?.ToString(),
                                    NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["NET_AMOUNT"]) : 0,
                                    PENDING_AMOUNT = reader["PENDING_AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["PENDING_AMOUNT"]) : 0,
                                    
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
                response.Data = new List<PendingInvoicelist>();
            }

            return response;
        }
        public SupplierPaymentResponse commit(SupplierPaymentUpdate model)
        {
            SupplierPaymentResponse response = new SupplierPaymentResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID);
                        // cmd.Parameters.AddWithValue("@PAY_ID", model.PAY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_TYPE", model.TRANS_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@TRANS_DATE", ParseDate(model.TRANS_DATE));
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
                        cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPPLIER_ID ?? 0);
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

                        foreach (var item in model.SUPP_DETAIL)
                        {
                            dt.Rows.Add(item.BILL_ID, item.AMOUNT);
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_SUPP_PAY_DETAIL", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_SUPP_PAY_DETAIL";

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
        public SupplierVoucherResponse GetSupplierNo()
        {
            SupplierVoucherResponse res = new SupplierVoucherResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 21 
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.SUPPLIER_PAYMENT = result != null ? Convert.ToInt32(result) : 0;
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

        public PDCListResponse GetPDCListBySupplierId(SupplierIdRequest request)
        {
            PDCListResponse response = new PDCListResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<PDCListItem>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SUPP_PAYMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6); //  PDC list
                        cmd.Parameters.AddWithValue("@SUPP_ID", request.SUPP_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PDCListItem item = new PDCListItem
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                    SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : 0,
                                    BENEFICIARY_NAME = reader["BENEFICIARY_NAME"]?.ToString(),
                                    ENTRY_NO = reader["ENTRY_NO"]?.ToString(),
                                    ENTRY_DATE = reader["ENTRY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["ENTRY_DATE"]).ToString("dd-MM-yyyy") : null,
                                    CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                    DUE_DATE = reader["DUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DUE_DATE"]).ToString("dd-MM-yyyy") : null,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["AMOUNT"]) : 0,
                                    REMARKS = reader["REMARKS"]?.ToString(),
                                    ENTRY_STATUS = reader["ENTRY_STATUS"]?.ToString(),
                                    BANK_NAME = reader["BANK_NAME"]?.ToString()
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
                response.Data = new List<PDCListItem>();
            }

            return response;
        }

    }
}
