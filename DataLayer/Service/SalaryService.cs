using MicroApi.Helper;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Models;
using MicroApi.DataLayer.Interface;
using System.Reflection;

namespace MicroApi.DataLayer.Service
{
    public class SalaryService:ISalaryService
    {
        public GenerateSalaryResponse GenerateSalary(List<Salary> salaryList)
        {
            GenerateSalaryResponse response = new GenerateSalaryResponse();
            int successCount = 0;

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    foreach (var model in salaryList)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_GENERATE_SALARY", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TS_ID", model.TS_ID);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                            cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID);
                            cmd.ExecuteNonQuery();
                            successCount++;
                        }
                    }
                }

                response.flag = 1;
                response.Message = "Salary generated";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public SalaryLookupResponse GetSalaryLookup(SalaryLookupRequest request)
        {
            SalaryLookupResponse response = new SalaryLookupResponse();
            response.Data = new List<SalaryLookup>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_LIST", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new SalaryLookup
                                {
                                    SALARY_BILL_NO = Convert.ToInt32(reader["SALARY_BILL_NO"]),
                                    SAL_MONTH = Convert.ToDateTime(reader["SAL_MONTH"]),
                                    EMPLOYEE_NO = Convert.ToString(reader["EMP_CODE"]),
                                    EMPLOYEE_NAME = Convert.ToString(reader["EMP_NAME"]),
                                    WORKED_DAYS = Convert.ToDecimal(reader["WORKED_DAYS"]),
                                    NET_AMOUNT = Convert.ToDecimal(reader["NET_AMOUNT"])
                                });
                            }
                        }
                    }

                    response.flag = 1;
                    response.Message = "Success";
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
