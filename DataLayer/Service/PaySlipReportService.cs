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
                PaySlipDetails = new List<PaySlipReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PAYSLIP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Month", request.Month);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    // Employee filter
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
                        //-----------------------------------------
                        // 🔹 FIRST RESULT SET → HEADER
                        //-----------------------------------------
                        while (reader.Read())
                        {
                            response.PaySlipDetails.Add(new PaySlipReport
                            {
                                EMP_ID = Convert.ToInt32(reader["EMP_ID"]),
                                EMP_CODE = reader["EMP_CODE"]?.ToString(),
                                EMP_NAME = reader["EMP_NAME"]?.ToString(),

                                PP_NO = reader["PFAccountNo"]?.ToString(),
                                BANK_AC_NO = reader["BankAcNo"]?.ToString(),

                                BASIC_SALARY = reader["BasicSalary"] != DBNull.Value ? Convert.ToSingle(reader["BasicSalary"]) : 0f,

                                TS_MONTH = reader["TS_MONTH"] != DBNull.Value ? Convert.ToDateTime(reader["TS_MONTH"]) : (DateTime?)null,
                                TOTAL_DAYS = reader["TOTAL_DAYS"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_DAYS"]) : 0,
                                OT_HOURS = reader["NORMAL_OT"] != DBNull.Value ? Convert.ToSingle(reader["NORMAL_OT"]) : 0,
                                LESS_HOURS = reader["LESS_HOURS"] != DBNull.Value ? Convert.ToSingle(reader["LESS_HOURS"]) : 0,

                                SALARY_ID = reader["SALARY_ID"] != DBNull.Value ? Convert.ToInt32(reader["SALARY_ID"]) : 0,

                                SalaryHeads = new List<PaySlipReportData>() // חשוב
                            });
                        }

                        //-----------------------------------------
                        // 🔹 SECOND RESULT SET → DETAIL
                        //-----------------------------------------
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int empId = Convert.ToInt32(reader["EMP_ID"]);
                                int salaryId = Convert.ToInt32(reader["SALARY_ID"]);

                                var emp = response.PaySlipDetails
                                    .FirstOrDefault(e => e.EMP_ID == empId && e.SALARY_ID == salaryId);

                                if (emp != null)
                                {
                                    emp.SalaryHeads.Add(new PaySlipReportData
                                    {
                                        HEAD_ID = Convert.ToInt32(reader["HEAD_ID"]),
                                        HEAD_NAME = reader["HEAD_NAME"]?.ToString(),
                                        HEAD_TYPE = reader["HEAD_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_TYPE"]) : 0,
                                        HEAD_AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["AMOUNT"]) : 0
                                    });
                                }
                            }
                        }
                    }
                }
            }

            response.flag = response.PaySlipDetails.Count > 0 ? 1 : 0;
            response.message = response.flag == 1 ? "Success" : "No records found";

            return response;
        }

    }
}
