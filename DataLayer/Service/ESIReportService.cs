using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ESIReportService : IESIReportService
    {
        public ESIReportResponse GetESIReport(ESIReportRequest request)
        {
            ESIReportResponse response = new ESIReportResponse
            {
                ESIDetails = new List<ESIReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_ESI_REPORT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", request.Month);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.ESIDetails.Add(new ESIReport
                            {
                                ESI_No = reader["ESI_No"] != DBNull.Value ? reader["ESI_No"].ToString() : string.Empty,
                                Staff_Name = reader["Staff_Name"] != DBNull.Value ? reader["Staff_Name"].ToString() : string.Empty,
                                Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                Employee_Share = reader["Employee_Share"] != DBNull.Value ? Convert.ToDecimal(reader["Employee_Share"]) : 0,
                                Employer_Share = reader["Employer_Share"] != DBNull.Value ? Convert.ToDecimal(reader["Employer_Share"]) : 0
                            });
                        }
                    }
                }
            }

            response.Flag = (response.ESIDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
    }
}
    

