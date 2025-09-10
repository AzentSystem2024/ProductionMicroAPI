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
                //Details = new List<PaySlipReportData>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PAYSLIP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", request.Month);
                    //cmd.Parameters.AddWithValue("@EmployeeID", (object)request.EmployeeID ?? DBNull.Value);

                    // If list is null or empty, pass DBNull
                    if (request.EmployeeIDs != null && request.EmployeeIDs.Any())
                    {
                        string employeeIdCsv = string.Join(",", request.EmployeeIDs);
                        cmd.Parameters.AddWithValue("@EmployeeIDs", employeeIdCsv);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EmployeeIDs", DBNull.Value);
                    }

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
                                BASIC_SALARY = reader["BasicSalary"] != DBNull.Value ? Convert.ToSingle(reader["BasicSalary"]) : 0f
                                // TS_MONTH, TOTAL_DAYS, OT_HOURS, LESS_HOURS, SALARY_ID are NOT in this result set
                            });
                        }

                        // Move to Timesheet Details (Second Result Set)
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int empId = Convert.ToInt32(reader["EMP_ID"]);
                                var emp = response.PaySlipDetails.FirstOrDefault(e => e.EMP_ID == empId);
                                if (emp != null)
                                {
                                    emp.TS_MONTH = Convert.ToDateTime(reader["TS_MONTH"]);
                                    emp.TOTAL_DAYS = Convert.ToSingle(reader["TOTAL_DAYS"]);
                                    emp.OT_HOURS = Convert.ToSingle(reader["OT_HOURS"]);
                                    emp.LESS_HOURS = Convert.ToSingle(reader["LESS_HOURS"]);
                                    emp.SALARY_ID = Convert.ToInt32(reader["SALARY_ID"]);
                                }
                            }
                        }

                        // Move to Salary Head Details (Third Result Set)
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int empId = Convert.ToInt32(reader["EMP_ID"]); // <-- You need EMP_ID in SP here
                                var emp = response.PaySlipDetails.FirstOrDefault(e => e.EMP_ID == empId);
                                if (emp != null)
                                {
                                    emp.SalaryHeads.Add(new PaySlipReportData
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
            }

            response.flag = (response.PaySlipDetails.Count > 0) ? 1 : 0;
            response.message = response.flag == 1 ? "Success" : "No records found";
            return response;
        }
    }
}
