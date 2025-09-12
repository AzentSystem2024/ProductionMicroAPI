using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace MicroApi.DataLayer.Service
{
    public class WageRegReportService : IWageRegReportService
    {
        public WageReportResponse GetWageReport(WageReportRequest request)
        {
            WageReportResponse response = new WageReportResponse
            {
                WageDetails = new List<WageRegReport>(),
                SalaryDetails = new List<SalaryReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_WAGEREGISTER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", request.Month);
                    cmd.Parameters.AddWithValue("@ReportType", request.ReportType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (request.ReportType == "Wages")
                        {
                            while (reader.Read())
                            {
                                response.WageDetails.Add(new WageRegReport
                                {
                                    SL_NO = Convert.ToInt32(reader["SL_NO"]),
                                    EMP_ID = Convert.ToInt32(reader["EMP_ID"]),
                                    EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                    EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                    DESIGNATION = reader["DESIGNATION"]?.ToString(),
                                    TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE = reader["TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE"]) : 0,
                                    OVERTIME_WORKED = reader["OVERTIME_WORKED"] != DBNull.Value ? Convert.ToSingle(reader["OVERTIME_WORKED"]) : 0,
                                    MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC = reader["MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC"] != DBNull.Value ? Convert.ToSingle(reader["MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC"]) : 0,
                                    MINIMUM_RATE_OF_WAGES_PAYABLE_DA = reader["MINIMUM_RATE_OF_WAGES_PAYABLE_DA"] != DBNull.Value ? Convert.ToSingle(reader["MINIMUM_RATE_OF_WAGES_PAYABLE_DA"]) : 0,
                                    RATE_OF_WAGES_ACTUALLY_PAID_BASIC = reader["RATE_OF_WAGES_ACTUALLY_PAID_BASIC"] != DBNull.Value ? Convert.ToSingle(reader["RATE_OF_WAGES_ACTUALLY_PAID_BASIC"]) : 0,
                                    RATE_OF_WAGES_ACTUALLY_PAID_DA = reader["RATE_OF_WAGES_ACTUALLY_PAID_DA"] != DBNull.Value ? Convert.ToSingle(reader["RATE_OF_WAGES_ACTUALLY_PAID_DA"]) : 0,
                                    GROSS_WAGES_PAYABLE = reader["GROSS_WAGES_PAYABLE"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_WAGES_PAYABLE"]) : 0,
                                    EMPLOYEE_CONTRIBUTION_TO_PF = reader["EMPLOYEE_CONTRIBUTION_TO_PF"] != DBNull.Value ? Convert.ToSingle(reader["EMPLOYEE_CONTRIBUTION_TO_PF"]) : 0,
                                    HR = reader["HR"] != DBNull.Value ? Convert.ToSingle(reader["HR"]) : 0,
                                    OTHER_DEDUCTIONS = reader["OTHER_DEDUCTION"] != DBNull.Value ? Convert.ToSingle(reader["OTHER_DEDUCTION"]) : 0,
                                    TOTAL_DEDUCTIONS = reader["TOTAL_DEDUCTION"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_DEDUCTION"]) : 0,
                                    WAGES_PAID = reader["WAGES_PAID"] != DBNull.Value ? Convert.ToSingle(reader["WAGES_PAID"]) : 0,
                                });
                            }
                        }
                        else if (request.ReportType == "Salary")
                        {
                            while (reader.Read())
                            {
                                response.SalaryDetails.Add(new SalaryReport
                                {
                                    SL_NO = Convert.ToInt32(reader["SL_NO"]),
                                    EMP_NAME = reader["EMP_NAME"]?.ToString(),
                                    DESIGNATION = reader["DESIGNATION"]?.ToString(),
                                    MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC = reader["MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC"] != DBNull.Value ? Convert.ToSingle(reader["MINIMUM_RATE_OF_WAGES_PAYABLE_BASIC"]) : 0,
                                    MINIMUM_RATE_OF_WAGES_PAYABLE_DA = reader["MINIMUM_RATE_OF_WAGES_PAYABLE_DA"] != DBNull.Value ? Convert.ToSingle(reader["MINIMUM_RATE_OF_WAGES_PAYABLE_DA"]) : 0,
                                    RATE_OF_WAGES_ACTUALLY_PAID_BASIC = reader["RATE_OF_WAGES_ACTUALLY_PAID_BASIC"] != DBNull.Value ? Convert.ToSingle(reader["RATE_OF_WAGES_ACTUALLY_PAID_BASIC"]) : 0,
                                    RATE_OF_WAGES_ACTUALLY_PAID_DA = reader["RATE_OF_WAGES_ACTUALLY_PAID_DA"] != DBNull.Value ? Convert.ToSingle(reader["RATE_OF_WAGES_ACTUALLY_PAID_DA"]) : 0,
                                    TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE = reader["TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_ATTENDANCE_UNITS_OF_WORK_DONE"]) : 0,
                                    OVERTIME_WORKED = reader["OVERTIME_WORKED"] != DBNull.Value ? Convert.ToSingle(reader["OVERTIME_WORKED"]) : 0,
                                    GROSS_WAGES_PAYABLE = reader["GROSS_WAGES_PAYABLE"] != DBNull.Value ? Convert.ToSingle(reader["GROSS_WAGES_PAYABLE"]) : 0,
                                    EMPLOYEE_CONTRIBUTION_TO_PF = reader["EMPLOYEE_CONTRIBUTION_TO_PF"] != DBNull.Value ? Convert.ToSingle(reader["EMPLOYEE_CONTRIBUTION_TO_PF"]) : 0,
                                    HR = reader["HR"] != DBNull.Value ? Convert.ToSingle(reader["HR"]) : 0,
                                    OTHER_DEDUCTIONS = reader["OTHER_DEDUCTION"] != DBNull.Value ? Convert.ToSingle(reader["OTHER_DEDUCTION"]) : 0,
                                    TOTAL_DEDUCTIONS = reader["TOTAL_DEDUCTION"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_DEDUCTION"]) : 0,
                                    WAGES_PAID = reader["WAGES_PAID"] != DBNull.Value ? Convert.ToSingle(reader["WAGES_PAID"]) : 0,
                                });
                            }
                        }
                    }
                }
            }

            response.flag = (response.WageDetails.Count > 0 || response.SalaryDetails.Count > 0) ? 1 : 0;
            response.message = response.flag == 1 ? "Success" : "No records found";
            return response;
        }
    }
}



