using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class GSTReportService:IGSTReportService
    {
        public GSTReportResponse GetGSTReport(GSTReportRequest request)
        {
            var response = new GSTReportResponse
            {
                DATA = new List<GSTReport>()
            };

            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_GST_REPORT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.DATA.Add(new GSTReport
                            {
                                HSN_CODE = rdr["HSN_CODE"].ToString(),
                                GSTIN = rdr["GSTIN"].ToString(),
                                RECEIVER_NAME = rdr["RECEIVER_NAME"].ToString(),
                                TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                                DOC_TYPE = rdr["DOC_TYPE"].ToString(),
                                DOC_NAME = rdr["DOC_NAME"].ToString(),
                                DOC_NO = rdr["DOC_NO"].ToString(),
                                DOC_DATE = rdr["DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["DOC_DATE"]),
                                INVOICE_AMOUNT = rdr["INVOICE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["INVOICE_AMOUNT"]),
                                PLACE_OF_SUPPLY = rdr["PLACE_OF_SUPPLY"].ToString(),
                                CGST = rdr["CGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CGST"]),
                                SGST = rdr["SGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["SGST"]),
                                IGST = rdr["IGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["IGST"]),
                                TOTAL_GST = rdr["TOTAL_GST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TOTAL_GST"]),
                                TAXABLE_AMOUNT = rdr["TAXABLE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TAXABLE_AMOUNT"])
                            });
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public GSTB2CLReportResponse GetGSTB2CLReport(GSTReportRequest request)
        {
            var response = new GSTB2CLReportResponse
            {
                DATA = new List<GSTReportB2CL>()
            };

            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_GST_REPORT_B2CL", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.DATA.Add(new GSTReportB2CL
                            {
                                HSN_CODE = rdr["HSN_CODE"].ToString(),
                                RECEIVER_NAME = rdr["RECEIVER_NAME"].ToString(),
                                TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                                DOC_TYPE = rdr["DOC_TYPE"].ToString(),
                                DOC_NAME = rdr["DOC_NAME"].ToString(),
                                DOC_NO = rdr["DOC_NO"].ToString(),
                                DOC_DATE = rdr["DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["DOC_DATE"]),
                                INVOICE_AMOUNT = rdr["INVOICE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["INVOICE_AMOUNT"]),
                                PLACE_OF_SUPPLY = rdr["PLACE_OF_SUPPLY"].ToString(),
                                CGST = rdr["CGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CGST"]),
                                SGST = rdr["SGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["SGST"]),
                                IGST = rdr["IGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["IGST"]),
                                TOTAL_GST = rdr["TOTAL_GST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TOTAL_GST"]),
                                TAXABLE_AMOUNT = rdr["TAXABLE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TAXABLE_AMOUNT"])
                            });
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public GSTReportResponse GetGSTReportCDNR(GSTReportRequest request)
        {
            var response = new GSTReportResponse
            {
                DATA = new List<GSTReport>()
            };

            try
            {
                request.DATE_FROM = request.DATE_FROM.Date;
                request.DATE_TO = request.DATE_TO.Date.AddDays(1).AddMilliseconds(-3);

                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_GST_REPORT_CDNR", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.DATA.Add(new GSTReport
                            {
                                HSN_CODE = rdr["HSN_CODE"].ToString(),
                                GSTIN = rdr["GSTIN"].ToString(),
                                RECEIVER_NAME = rdr["RECEIVER_NAME"].ToString(),
                                TRANS_ID = Convert.ToInt32(rdr["TRANS_ID"]),
                                DOC_TYPE = rdr["DOC_TYPE"].ToString(),
                                DOC_NAME = rdr["DOC_NAME"].ToString(),
                                DOC_NO = rdr["DOC_NO"].ToString(),
                                DOC_DATE = rdr["DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["DOC_DATE"]),
                                INVOICE_AMOUNT = rdr["INVOICE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["INVOICE_AMOUNT"]),
                                PLACE_OF_SUPPLY = rdr["PLACE_OF_SUPPLY"].ToString(),
                                CGST = rdr["CGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CGST"]),
                                SGST = rdr["SGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["SGST"]),
                                IGST = rdr["IGST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["IGST"]),
                                TOTAL_GST = rdr["TOTAL_GST"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TOTAL_GST"]),
                                TAXABLE_AMOUNT = rdr["TAXABLE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TAXABLE_AMOUNT"])
                            });
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
