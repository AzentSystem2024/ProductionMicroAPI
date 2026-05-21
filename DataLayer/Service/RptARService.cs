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
            
                // First Stored Procedure
                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RPT_CLAIM_DETAILS";
                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(vInput.DateFrom));
                cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(vInput.DateTo));


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                //balance
                RPTARHeader ClaimReport = new RPTARHeader();
                DataTable tblClaim = ds.Tables[0];

                ClaimReport.ReportID = "claim-details-claim";
                if (tblClaim.Rows.Count > 0)
                {
                    IList<RptARData> lstSummary = tblClaim.AsEnumerable().Select(row => new RptARData
                    {

                        ClaimUID = row.Field<int>("ClaimUID"),
                        FacilityID = row.Field<string>("FacilityID"),
                        FacilityName = row.Field<string>("FacilityName"),
                        ClaimNumber = row.Field<string>("ClaimNumber"),
                        ReceiverID = row.Field<string?>("ReceiverID"),
                        PayerID = row.Field<string?>("PayerID"),
                        PatientID = row.Field<string?>("PatientID"),
                        EncounterType = row.Field<string?>("EncounterType"),
                        EncounterStartDate = row.Field<DateTime>("EncounterStartDate").ToString("dd/MM/yyyy"),
                        StartType = row.Field<string?>("StartType"),
                        EncounterEndDate = row.Field<DateTime>("EncounterEndDate").ToString("dd/MM/yyyy"),
                        EndType = row.Field<string?>("EndType"),
                        MemberID = row.Field<string?>("MemberID"),
                        EmiratesID = row.Field<string?>("EmiratesID"),
                        TransactionDate = row.Field<DateTime>("TransactionDate").ToString("dd/MM/yyyy"),
                        GrossAmount = row.Field<decimal?>("GrossAmount") ?? 0,
                        PatientShare = row.Field<decimal?>("PatientShare") ?? 0,
                        NetAmount = row.Field<decimal?>("NetAmount") ?? 0,
                        XMLFileName = row.Field<string?>("XMLFileName"),

                    }).ToList();
                    ClaimReport.ReportData = lstSummary.ToList();
                }
                else
                {
                    List<RptARData> LstData = new List<RptARData>();
                    ClaimReport.ReportData = LstData.ToList();
                }

                string strSQL = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                                "TB_REPORT_COLUMNS WHERE ReportID = " + ADO.SQLString(ClaimReport.ReportID) + " ORDER BY ID";

                DataTable tblSummaryCols = ADO.GetDataTable(strSQL);

                IList<ReportColumns> lstSummaryCols = tblSummaryCols.AsEnumerable().Select(row => new ReportColumns
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
