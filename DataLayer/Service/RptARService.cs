using MicroApi.DataLayer.Interface;
using System.Data;
using System.Data.SqlClient;
using MicroApi.Models;
using MicroApi.Helper;

namespace MicroApi.DAL.Services
{
    public class RptARService : IRptARService
    {
        public RptARResponseOutput claimDetailsReport(RptARInput vInput)
        {
            RptARResponseOutput vReport = new RptARResponseOutput();

            SqlConnection conn = new SqlConnection();

            try
            {
                conn = ADO.GetConnection();

                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RPT_AR";
                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(vInput.DateFrom));
                cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(vInput.DateTo));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                // REPORT
                RPTARHeader ClaimReport = new RPTARHeader();

                DataTable tblClaim = ds.Tables[0];

                ClaimReport.ReportID = "rpt-ar";

                if (tblClaim.Rows.Count > 0)
                {
                    IList<RptARData> lstSummary = tblClaim.AsEnumerable().Select(row => new RptARData
                    {
                        InvoiceType = row.Field<string?>("InvoiceType"),

                        TransactionType = row.Field<string?>("TransactionType"),

                        TransactionIncomeGroup = row.Field<string?>("TransactionIncomeGroup"),

                        ApexTransactionNumber = row.Field<string?>("ApexTransactionNumber"),

                        ApexTransactionDate = row.Field<DateTime?>("ApexTransactionDate")?
                            .ToString("dd/MM/yyyy"),

                        ApexPatientCode = row.Field<string?>("ApexPatientCode"),

                        ApexTPACode = row.Field<string?>("ApexTPACode"),

                        ApexInsuCode = row.Field<string?>("ApexInsuCode"),

                        ApexInstCode = row.Field<string?>("ApexInstCode"),

                        ApexReportingDoctor = row.Field<string?>("ApexReportingDoctor"),

                        ApexReferringDoctor = row.Field<string?>("ApexReferringDoctor"),

                        ApexReportingDoctorDept = row.Field<string?>("ApexReportingDoctorDept"),

                        ApexReferringDoctorDept = row.Field<string?>("ApexReferringDoctorDept"),

                        IncomeGrossAmount = row.Field<decimal?>("IncomeGrossAmount") ?? 0,

                        IncomePolicyConcAmount = row.Field<decimal?>("IncomePolicyConcAmount") ?? 0,

                        IncomeAddlConcAmount = row.Field<decimal?>("IncomeAddlConcAmount") ?? 0,

                        IncomeNetAmount = row.Field<decimal?>("IncomeNetAmount") ?? 0,

                        IncomePatientAmount = row.Field<decimal?>("IncomePatientAmount") ?? 0,

                        IncomeInstAmount = row.Field<decimal?>("IncomeInstAmount") ?? 0,

                        IncomeInsuAmount = row.Field<decimal?>("IncomeInsuAmount") ?? 0,

                        ServiceCategory = row.Field<string?>("ServiceCategory"),

                        ServiceCode = row.Field<string?>("ServiceCode"),

                        ServiceName = row.Field<string?>("ServiceName"),

                        CPTCode = row.Field<string?>("CPTCode"),

                        OrgnBranchCode = row.Field<string?>("OrgnBranchCode"),

                        Paymode = row.Field<string?>("Paymode"),

                        PaymodeGateway = row.Field<string?>("PaymodeGateway"),

                        PaymodeAmount = row["PaymodeAmount"] == DBNull.Value
                            ? null
                            : Convert.ToDecimal(row["PaymodeAmount"]).ToString("0.00"),

                        PaymentRefNo = row.Field<string?>("PaymentRefNo"),

                        DeniedAmount = row.Field<decimal?>("DeniedAmount") ?? 0,

                        DenialCode = row.Field<string?>("DenialCode"),

                        PaymentApexTransctionNumber = row.Field<string?>("PaymentApexTransctionNumber"),

                        PaymentApexTransactionDate = row.Field<DateTime?>("PaymentApexTransactionDate")?
                            .ToString("dd/MM/yyyy")

                    }).ToList();

                    ClaimReport.ReportData = lstSummary.ToList();
                }
                else
                {
                    List<RptARData> LstData = new List<RptARData>();

                    ClaimReport.ReportData = LstData.ToList();
                }

                string strSQL = @"
            SELECT 
                ColTitle,
                ColName,
                ColToolTip,
                ColType,
                ColIsVisible,
                ColIsGroup,
                ColIsSummary
            FROM TB_REPORT_COLUMNS
            WHERE ReportID = " + ADO.SQLString(ClaimReport.ReportID) + @"
            ORDER BY ID";

                DataTable tblSummaryCols = ADO.GetDataTable(strSQL);

                IList<ReportColumns> lstSummaryCols =
                    tblSummaryCols.AsEnumerable().Select(row => new ReportColumns
                    {
                        Title = row.Field<string>("ColTitle"),

                        Name = row.Field<string>("ColName"),

                        ToolTip = row.Field<string>("ColToolTip"),

                        Type = row.Field<string>("ColType"),

                        Visibility = row.Field<bool>("ColIsVisible"),

                        Summary = row.Field<bool>("ColIsSummary"),

                        Group = row.Field<bool>("ColIsGroup"),

                    }).ToList();

                ClaimReport.ReportColumns = lstSummaryCols.ToList();

                vReport.header = ClaimReport;

                vReport.flag = "1";
                vReport.message = "Success";
            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return vReport;
        }








        public string fToString(object vInput)
        {
            try
            {
                return Convert.ToString(vInput);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
