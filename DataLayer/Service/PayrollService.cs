using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class PayrollService : IPayrollService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PayrollService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public PayrollReportResponse GetPayrollReport(PayrollReportRequest request)
        {
            PayrollReportResponse response = new PayrollReportResponse();

            try
            {
                response.flag = 1;
                response.Message = "Success";
                response.Data = new List<PayrollReport>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_PAYROLL_REPORT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FROM_DATE", request.FromDate);
                        cmd.Parameters.AddWithValue("@TO_DATE", request.ToDate);
                        cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DepartmentId);
                        cmd.Parameters.AddWithValue("@PAYMENT_MODE", request.PaymentMode);

                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            response.Data.Add(new PayrollReport
                            {
                                SalaryMonth = dr["SalaryMonth"].ToString(),
                                EmployeeNo = dr["EmployeeNo"].ToString(),
                                EmployeeName = dr["EmployeeName"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                Department = dr["Department"].ToString(),
                                BasicPay = Convert.ToDecimal(dr["BasicPay"]),
                                Allowance = Convert.ToDecimal(dr["Allowance"]),
                                TotalDue = Convert.ToDecimal(dr["TotalDue"]),
                                Overtime = Convert.ToDecimal(dr["Overtime"]),
                                Advance = Convert.ToDecimal(dr["Advance"]),
                                Deductions = Convert.ToDecimal(dr["Deductions"]),
                                NetPayable = Convert.ToDecimal(dr["NetPayable"])
                              //  ,PaymentMode = dr["PaymentMode"].ToString()
                            });
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

        public List<PaymentModeModel> GetPaymentMode()
        {
            List<PaymentModeModel> list = new List<PaymentModeModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ID,DESCRIPTION FROM TB_SALARY_PAYMENT_TYPE ORDER BY ID", con))
                {
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        list.Add(new PaymentModeModel
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DESCRIPTION = dr["DESCRIPTION"].ToString()
                        });
                    }

                    con.Close();
                }
            }

            return list;
        }
        public PayrollOTResponse GetPayrollOTReport(PayrollReportRequest request)
        {
            PayrollOTResponse response = new PayrollOTResponse();

            try
            {
                response.flag = 1;
                response.Message = "Success";
                response.Data = new List<PayrollOT>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_PAYROLL_OT_REPORT", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FROM_DATE", request.FromDate);
                    cmd.Parameters.AddWithValue("@TO_DATE", request.ToDate);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DepartmentId);
                    cmd.Parameters.AddWithValue("@PAYMENT_MODE", request.PaymentMode);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        response.Data.Add(new PayrollOT
                        {
                            SalaryMonth = dr["SalaryMonth"].ToString(),
                            EmployeeCode = dr["EmployeeNo"].ToString(),
                            Name = dr["Name"].ToString(),
                            Designation = dr["Designation"].ToString(),
                            Department= dr["Department"].ToString(),
                            BasicPay = Convert.ToDecimal(dr["BasicPay"]),
                            Allowance = Convert.ToDecimal(dr["Allowance"]),
                            Salary = Convert.ToDecimal(dr["Salary"]),
                            NormalOT = Convert.ToDecimal(dr["NormalOT"]),
                            HolidayOT = Convert.ToDecimal(dr["HolidayOT"]),
                            NormalOTAmount = Convert.ToDecimal(dr["NormalOTAmount"]),
                            HolidayOTAmount = Convert.ToDecimal(dr["HolidayOTAmount"]),
                            TotalOT = Convert.ToDecimal(dr["TotalOT"]),
                            TotalOTAmount = Convert.ToDecimal(dr["TotalOTAmount"])
                        });
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}