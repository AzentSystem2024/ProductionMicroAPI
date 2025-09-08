using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class PaySlipReportService : IPaySlipReportService
    {
        public PayslipReportResponse GetPayslipReport(PayslipReportRequest request)
        {
            PayslipReportResponse response = new PayslipReportResponse
            {
                PaySlipDetails = new List<PaySlipReport>(),
                Details = new List<PaySlipReportData>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PAYSLIP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", request.Month);
                    cmd.Parameters.AddWithValue("@EmployeeID", (object)request.EmployeeID ?? DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read Employee Details (First Result Set)
                        while (reader.Read())
                        {
                            response.PaySlipDetails.Add(new PaySlipReport
                            {
                                EMP_ID = Convert.ToInt32(reader["EMP_ID"]),
                                EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                PP_NO = reader["PFAccountNo"]?.ToString(),
                                DAMAN_NO = reader["DAMAN_NO"]?.ToString(),
                                BANK_AC_NO = reader["BankAcNo"]?.ToString(),
                                BASIC_SALARY = reader["BasicSalary"] != DBNull.Value ? Convert.ToSingle(reader["BasicSalary"]) : 0f,
                                // TS_MONTH, TOTAL_DAYS, OT_HOURS, LESS_HOURS, SALARY_ID are NOT in this result set
                            });
                        }

                        // Move to Timesheet Details (Second Result Set)
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                // Update PaySlipDetails with timesheet info
                                foreach (var item in response.PaySlipDetails)
                                {
                                    item.TS_MONTH = Convert.ToDateTime(reader["TS_MONTH"]);
                                    item.TOTAL_DAYS = Convert.ToSingle(reader["TOTAL_DAYS"]);
                                    item.OT_HOURS = Convert.ToSingle(reader["OT_HOURS"]);
                                    item.LESS_HOURS = Convert.ToSingle(reader["LESS_HOURS"]);
                                    item.SALARY_ID = Convert.ToInt32(reader["SALARY_ID"]);
                                }
                            }
                        }

                        // Move to Salary Head Details (Third Result Set)
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                response.Details.Add(new PaySlipReportData
                                {
                                    HEAD_ID = Convert.ToInt32(reader["HEAD_ID"]),
                                    HEAD_NAME = reader["HEAD_NAME"]?.ToString(),
                                    HEAD_TYPE = Convert.ToInt32(reader["HEAD_TYPE"]),
                                    HEAD_AMOUNT = Convert.ToDecimal(reader["HEAD_AMOUNT"]),
                                    SALARY = Convert.ToDecimal(reader["SALARY"])
                                });
                            }
                        }
                    }
                }
            }

            response.flag = (response.PaySlipDetails.Count > 0 || response.Details.Count > 0) ? 1 : 0;
            response.message = response.flag == 1 ? "Success" : "No records found";
            return response;
        }
    }
}
