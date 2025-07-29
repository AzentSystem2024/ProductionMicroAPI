using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class AC_ReportService: IAC_ReportService
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


    }
}
