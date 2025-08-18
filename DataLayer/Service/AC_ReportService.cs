using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using MicroApi.DataLayer.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class AC_ReportService : IAC_ReportService
    {
        public LedgerReportInitData GetInitData(int id)
        {
            var reportData = new LedgerReportInitData
            {
                LEDGER_HEADS = new List<DropDownItem>()
            };

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT H.HEAD_ID, H.HEAD_NAME
            FROM TB_AC_HEAD H
            INNER JOIN TB_AC_TRANS_DETAIL TD ON TD.HEAD_ID = H.HEAD_ID
            INNER JOIN TB_AC_TRANS_HEADER TH ON TH.TRANS_ID = TD.TRANS_ID
            WHERE TH.COMPANY_ID = @COMPANY_ID
            ORDER BY H.HEAD_NAME", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@COMPANY_ID", id);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        reportData.LEDGER_HEADS.Add(new DropDownItem
                        {
                            ID = Convert.ToInt32(rdr["HEAD_ID"]),
                            NAME = rdr["HEAD_NAME"].ToString()
                        });
                    }
                }
            }

            reportData.flag = 1;
            reportData.message = "Success";
            return reportData;
        }


        public LedgerStatementResponse GetLedgerStatement(AC_Report request)
        {
            var response = new LedgerStatementResponse
            {
                data = new List<LedgerStatementItem>()
            };

            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_LEDGER_STATEMENT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@HEAD_ID", request.HEAD_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.data.Add(new LedgerStatementItem
                            {
                                TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                                TRANS_DATE = rdr["TRANS_DATE"] == DBNull.Value ? null : Convert.ToDateTime(rdr["TRANS_DATE"]),
                                TRANS_TYPE_ID = rdr["TRANS_TYPE_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["TRANS_TYPE_ID"]),
                                TRANS_TYPE_NAME = rdr["TRANS_TYPE_NAME"].ToString(),
                                VOUCHER_NO = rdr["VOUCHER_NO"].ToString(),
                                PARTICULARS = rdr["PARTICULARS"].ToString(),
                                DR_AMOUNT = Convert.ToDecimal(rdr["DR_AMOUNT"]),
                                CR_AMOUNT = Convert.ToDecimal(rdr["CR_AMOUNT"]),
                                BALANCE = rdr["BALANCE"].ToString()
                            });
                        }
                    }

                    response.flag = 1;
                    response.message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }
        public ArticleProductionResponse GetArticleProductionReport(ArticleProductionFilter request)
        {
            var response = new ArticleProductionResponse
            {
                data = new List<ArticleProductionItem>()
            };

            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_ARTICLE_PRODUCTION", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.data.Add(new ArticleProductionItem
                            {
                                ART_NO = rdr["ART_NO"].ToString(),
                                COLOR = rdr["COLOR"].ToString(),
                                CATEGORY = rdr["CATEGORY"].ToString(),
                                BRAND = rdr["BRAND"].ToString(),
                                SIZE = rdr["SIZE"].ToString(),
                                PRODUCTION_UNIT = rdr["UNIT"].ToString(),
                                QUANTITY = Convert.ToInt32(rdr["QUANTITY"])
                            });
                        }
                    }

                    response.flag = 1;
                    response.message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }

        public BoxProductionResponse GetBoxProductionReport(BoxProductionFilter request)
        {
            var response = new BoxProductionResponse
            {
                data = new List<BoxProductionItem>()
            };

            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_BOX_PRODUCTION", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.data.Add(new BoxProductionItem
                            {
                                ART_NO = rdr["ART_NO"].ToString(),
                                COLOR = rdr["COLOR"].ToString(),
                                CATEGORY = rdr["CATEGORY"].ToString(),
                                BRAND = rdr["BRAND"].ToString(),
                                SIZE = rdr["SIZE"].ToString(),
                                PRODUCTION_UNIT = rdr["UNIT"].ToString(),
                                QUANTITY = Convert.ToInt32(rdr["QUANTITY"])
                            });
                        }
                    }

                    response.flag = 1;
                    response.message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }
        public CashBookResponse GetCashBookReport(CashBookFilter request)
        {
            var response = new CashBookResponse { data = new List<CashBookItem>() };
            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_CASH_BOOK", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.data.Add(new CashBookItem
                            {
                                TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                                TRANS_TYPE = Convert.ToInt32(rdr["TRANS_TYPE"]),
                                TRANS_DATE = rdr["TRANS_DATE"] as DateTime?,
                                VOUCHER_NO = rdr["VOUCHER_NO"].ToString(),
                                PARTICULARS = rdr["OPP_HEAD_NAME"].ToString(),
                                REMARKS = rdr["NARRATION"].ToString(),
                                DR_AMOUNT = Convert.ToDecimal(rdr["DR_AMOUNT"]),
                                CR_AMOUNT = Convert.ToDecimal(rdr["CR_AMOUNT"]),
                                TRANS_DESCRIPTION = Convert.ToString(rdr["TRANS_DESCRIPTION"])
                            });
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
        public BalanceSheetResponse GetBalanceSheetReport(BalanceSheetFilter request)
        {
            var response = new BalanceSheetResponse { data = new List<BalanceSheetItem>() };
            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_BALANCE_SHEET", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.data.Add(new BalanceSheetItem
                            {
                                TYPE_ID = Convert.ToInt32(rdr["TYPE_ID"]),
                                TYPE_NAME = rdr["TYPE_NAME"].ToString(),
                                MAIN_GROUP_ID = Convert.ToInt32(rdr["MAIN_GROUP_ID"]),
                                MAIN_GROUP_NAME = rdr["MAIN_GROUP_NAME"].ToString(),
                                HEAD_ID = Convert.ToInt32(rdr["HEAD_ID"]),
                                PARTICULARS = rdr["HEAD_NAME"].ToString(),
                                AMOUNT = Convert.ToDecimal(rdr["AMOUNT"])
                            });
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
        public ProfitLossReportResponse GetProfitLossReport(ProfitLossReportRequest request)
        {
            ProfitLossReportResponse response = new ProfitLossReportResponse
            {
                data = new List<ProfitLossReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PROFIT_LOSS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProfitLossReport report = new ProfitLossReport
                            {
                                TYPE_ID = reader["TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["TYPE_ID"]) : 0,
                                TYPE_NAME = reader["TYPE_NAME"]?.ToString(),
                                MAIN_GROUP_ID = reader["MAIN_GROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["MAIN_GROUP_ID"]) : 0,
                                MAIN_GROUP_NAME = reader["MAIN_GROUP_NAME"]?.ToString(),
                                HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                HEAD_NAME = reader["HEAD_NAME"]?.ToString(),
                                AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDouble(reader["AMOUNT"]) : 0,
                                BL_ORDER = reader["BL_ORDER"] != DBNull.Value ? Convert.ToDouble(reader["BL_ORDER"]) : 0
                            };

                            response.data.Add(report);
                        }
                    }
                }
            }

            response.flag = response.data.Count > 0 ? 1 : 0;
            response.message = response.data.Count > 0 ? "Success" : "No records found";

            return response;
        }

        public CustomerStatementResponse GetCustomerStatement(Customer_Statement_Request request)
        {
            CustomerStatementResponse response = new CustomerStatementResponse
            {
                Data = new List<Customer_Statement_Rpt>()
            }
            ;

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_CUSTOMER_STATEMENT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", request.CUSTOMER_ID ?? 0);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer_Statement_Rpt report = new Customer_Statement_Rpt
                            {
                                CUSTOMER_ID = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUSTOMER_ID"]) : 0,
                                TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                CUSTOMER_NAME = reader["CUSTOMER_NAME"]?.ToString(),
                                INVOICE_DATE = reader["INVOICE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["INVOICE_DATE"]) : DateTime.MinValue,
                                INVOICE_NO = reader["INVOICE_NO"]?.ToString(),
                                NARRATION = reader["NARRATION"]?.ToString(),
                                REFERENCE_NO = reader["REFERENCE_NO"]?.ToString(),
                                RETURN_AMOUNT = reader["RETURN_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["RETURN_AMOUNT"]) : 0,
                                NET_AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0,
                                RECEIVED_AMOUNT = reader["RECEIVED_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["RECEIVED_AMOUNT"]) : 0,
                                ADJUSTED_AMOUNT = reader["ADJUSTED_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["ADJUSTED_AMOUNT"]) : 0,
                                BALANCE = reader["BALANCE"] != DBNull.Value ? Convert.ToDecimal(reader["BALANCE"]) : 0,
                                AGE = reader["AGE"] != DBNull.Value ? Convert.ToInt32(reader["AGE"]) : 0
                            };

                            response.Data.Add(report);
                        }
                    }
                }
            }

            response.flag = response.Data.Count > 0 ? 1 : 0;
            response.message = response.Data.Count > 0 ? "Success" : "No records found";

            return response;
        }
        public CustomerAgingResult GetCustomerAging(Customer_Aging request)
        {
            CustomerAgingResult response = new CustomerAgingResult
            {
                Data = new List<Customer_Aging_Rpt>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_CUSTOMER_AGING", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", request.CUSTOMER_ID ?? 0);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer_Aging_Rpt report = new Customer_Aging_Rpt
                            {
                                CUSTOMER_NAME = reader["CUSTOMER_NAME"]?.ToString(),
                                AGE_0_30 = reader["AGE_0_30"] != DBNull.Value ? Convert.ToDecimal(reader["AGE_0_30"]) : 0,
                                AGE_31_60 = reader["AGE_31_60"] != DBNull.Value ? Convert.ToDecimal(reader["AGE_31_60"]) : 0,
                                AGE_61_90 = reader["AGE_61_90"] != DBNull.Value ? Convert.ToDecimal(reader["AGE_61_90"]) : 0,
                                AGE_ABOVE_90 = reader["AGE_ABOVE_90"] != DBNull.Value ? Convert.ToDecimal(reader["AGE_ABOVE_90"]) : 0,
                                TOTAL_BALANCE = reader["TOTAL_BALANCE"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_BALANCE"]) : 0
                            };

                            response.Data.Add(report);
                        }
                    }
                }
            }

            response.flag = response.Data.Count > 0 ? 1 : 0;
            response.message = response.Data.Count > 0 ? "Success" : "No records found";

            return response;
        }

        public SupplierStatReportResponse GetSupplierStateReports(SupplierStatReportRequest request)
        {
            var res = new SupplierStatReportResponse();

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_SUPP_STATEMENT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@SUPP_ID", request.SUPP_ID ?? 0); // Handle nullable SUPP_ID

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var report = new SupplierStatementReport
                            {
                                SUPP_ID = Convert.ToInt32(reader["SUPP_ID"]),
                                SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                PURCH_ID = Convert.ToInt32(reader["PURCH_ID"]),
                                DOC_NO = reader["DOC_NO"]?.ToString(),
                                PURCH_DATE = Convert.ToDateTime(reader["PURCH_DATE"]),
                                SUPP_INV_NO = reader["SUPP_INV_NO"]?.ToString(),
                                PO_NO = reader["PO_NO"]?.ToString(),
                                NET_AMOUNT = Convert.ToDecimal(reader["NET_AMOUNT"]),
                                PAID_AMOUNT = Convert.ToDecimal(reader["PAID_AMOUNT"]),
                                RETURN_AMOUNT = Convert.ToDecimal(reader["RETURN_AMOUNT"]),
                                ADJ_AMOUNT = Convert.ToDecimal(reader["ADJ_AMOUNT"]),
                                BALANCE = Convert.ToDecimal(reader["BALANCE"]),
                                AGE = Convert.ToInt32(reader["AGE"])
                            };
                            res.data.Add(report);
                        }
                    }
                }
            }

            res.flag = res.data.Count > 0 ? 1 : 0;
            res.message = res.data.Count > 0 ? "Success" : "No records found";
            return res;
        }
        public AgedPayableReportResponse GetAgedPayableReports(AgedPayableReportRequest request)
        {
            var res = new AgedPayableReportResponse();

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_AGING_PAYABLES", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@SUPP_ID", request.SUPP_ID ?? 0); // Handle nullable SUPP_ID

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var report = new AgedPayableReport
                            {
                                SUPP_ID = Convert.ToInt32(reader["SUPP_ID"]),
                                SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                NET_AMOUNT = Convert.ToDecimal(reader["NET_AMOUNT"]),
                                PAID_AMOUNT = Convert.ToDecimal(reader["PAID_AMOUNT"]),
                                RETURN_AMOUNT = Convert.ToDecimal(reader["RETURN_AMOUNT"]),
                                ADJ_AMOUNT = Convert.ToDecimal(reader["ADJ_AMOUNT"]),
                                //BALANCE = Convert.ToDecimal(reader["BALANCE"]),
                                AGE_0_30 = Convert.ToDecimal(reader["AGE_0_30"]),
                                AGE_31_60 = Convert.ToDecimal(reader["AGE_31_60"]),
                                AGE_61_90 = Convert.ToDecimal(reader["AGE_61_90"]),
                                AGE_91_120 = Convert.ToDecimal(reader["AGE_91_120"]),
                                AGE_ABOVE_120 = Convert.ToDecimal(reader["AGE_ABOVE_120"])
                            };
                            res.data.Add(report);
                        }
                    }
                }
            }

            res.flag = res.data.Count > 0 ? 1 : 0;
            res.message = res.data.Count > 0 ? "Success" : "No records found";
            return res;
        }
    }
}
        

    

