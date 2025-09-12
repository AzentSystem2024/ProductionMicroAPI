using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class EPFReportService : IEPFReportService
    {
        public EPFReportResponse GetEPFReport(EPFReportRequest request)
        {
            EPFReportResponse response = new EPFReportResponse
            {
                EPFDetails = new List<EPFReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_EPF_REPORT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", request.Month);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.EPFDetails.Add(new EPFReport
                            {
                                EMP_ID = Convert.ToInt32(reader["EMP_ID"]),
                                EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                PFAccountNo = reader["PFAccountNo"]?.ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                EmployeeShare = Convert.ToDecimal(reader["EmployeeShare"]),
                                A_C_01 = Convert.ToDecimal(reader["A/C.01"]),
                                A_C_10 = Convert.ToDecimal(reader["A/C.10"]),
                                EPFContributionOfEmployer = Convert.ToDecimal(reader["EPFContributionOfEmployer"]),
                                EmployeesPensionFund = Convert.ToDecimal(reader["EmployeesPensionFund"]),
                                A_C_No_21 = Convert.ToDecimal(reader["A/C No. 21"]),
                                AdministrativeChargesA_C_2 = Convert.ToDecimal(reader["Administrative Charges A/C. 2"]),
                                A_C_No_22 = Convert.ToDecimal(reader["A/C No. 22"])
                            });
                        }
                    }
                }
            }

            response.Flag = (response.EPFDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";
            return response;
        }
    }
}
    

