using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class FinalSettlementService : IFinalSettlementService
    {

        public FinalSettlementResponse GetFinalSettlement(FinalSettlementRequest request)
        {
            FinalSettlementResponse response = new FinalSettlementResponse();
            response.SalaryComponents = new List<FinalSettlementComponent>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_FINAL_SETTLEMENT_REPORT", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEE_ID", request.EmployeeId);

                    SqlDataReader dr = cmd.ExecuteReader();

                    // First Result Set (Header)
                    if (dr.Read())
                    {
                        response.EmployeeCode = dr["EmployeeCode"].ToString();
                        response.EmployeeName = dr["EmployeeName"].ToString();
                        response.SettlementType = dr["SettlementType"].ToString();

                        response.DateOfJoining = dr["DateOfJoining"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DateOfJoining"]);
                        response.LastWorkingDay = dr["LastWorkingDay"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LastWorkingDay"]);

                        response.BasicSalary = dr["BasicSalary"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["BasicSalary"]);
                        response.Allowance = dr["Allowance"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Allowance"]);

                        response.UnPaidLeaveDays = dr["UnPaidLeaveDays"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["UnPaidLeaveDays"]);
                        response.TotalServiceDays = dr["TotalServiceDays"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TotalServiceDays"]);

                        response.IndemnityFirstFiveYears = dr["IndemnityFirstFiveYears"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["IndemnityFirstFiveYears"]);
                        response.IndemnityAfterFiveYears = dr["IndemnityAfterFiveYears"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["IndemnityAfterFiveYears"]);
                        response.TotalIndemnityDays = dr["TotalIndemnityDays"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TotalIndemnityDays"]);

                        response.EntitledLeaveDays = dr["EntitledLeaveDays"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["EntitledLeaveDays"]);

                        response.IndemnityAmount = dr["IndemnityAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["IndemnityAmount"]);
                        response.LeaveSalary = dr["LeaveSalary"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["LeaveSalary"]);
                    }

                    // Second Result Set (Salary Components)
                    if (dr.NextResult())
                    {
                        while (dr.Read())
                        {
                            response.SalaryComponents.Add(new FinalSettlementComponent
                            {
                                HeadName = dr["HEAD_NAME"].ToString(),
                                SalaryMonth = dr["SalaryMonth"].ToString(),
                                Amount = dr["HEAD_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["HEAD_AMOUNT"])
                            });
                        }
                    }

                    dr.Close();
                }

                response.TotalAmount =
                    response.IndemnityAmount +
                    response.LeaveSalary +
                    response.SalaryComponents.Sum(x => x.Amount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return response;
        }


    }
}
