using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class EmployeeSalaryService:IEmployeeSalaryService
    {
        public Int32 SaveData(EmployeeSalarySave salary)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        object GetDbValue(object? val)
                        {
                            return val == null || (val is string s && string.IsNullOrWhiteSpace(s))
                                ? DBNull.Value
                                : val;
                        }

                        cmd.Parameters.AddWithValue("@ACTION", 1); 
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value); 

                        cmd.Parameters.AddWithValue("@COMPANY_ID", GetDbValue(salary.COMPANY_ID));
                        cmd.Parameters.AddWithValue("@EMP_ID", GetDbValue(salary.EMP_ID));
                        cmd.Parameters.AddWithValue("@HEAD_ID", GetDbValue(salary.HEAD_ID));
                        cmd.Parameters.AddWithValue("@HEAD_PERCENT", GetDbValue(salary.HEAD_PERCENT));
                        cmd.Parameters.AddWithValue("@HEAD_AMOUNT", GetDbValue(salary.HEAD_AMOUNT));
                        cmd.Parameters.AddWithValue("@FIN_ID", GetDbValue(salary.FIN_ID));
                        cmd.Parameters.AddWithValue("@EFFECT_FROM", GetDbValue(salary.EFFECT_FROM));
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", GetDbValue(salary.IS_INACTIVE));

                        Int32 result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Int32 EditData(EmployeeSalaryUpdate salary)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        object GetDbValue(object? val)
                        {
                            return val == null || (val is string s && string.IsNullOrWhiteSpace(s))
                                ? DBNull.Value
                                : val;
                        }

                        cmd.Parameters.AddWithValue("@ACTION", 2); // Update
                        cmd.Parameters.AddWithValue("@ID", salary.ID);

                        cmd.Parameters.AddWithValue("@COMPANY_ID", GetDbValue(salary.COMPANY_ID));
                        cmd.Parameters.AddWithValue("@EMP_ID", GetDbValue(salary.EMP_ID));
                        cmd.Parameters.AddWithValue("@HEAD_ID", GetDbValue(salary.HEAD_ID));
                        cmd.Parameters.AddWithValue("@HEAD_PERCENT", GetDbValue(salary.HEAD_PERCENT));
                        cmd.Parameters.AddWithValue("@HEAD_AMOUNT", GetDbValue(salary.HEAD_AMOUNT));
                        cmd.Parameters.AddWithValue("@FIN_ID", GetDbValue(salary.FIN_ID));
                        cmd.Parameters.AddWithValue("@EFFECT_FROM", GetDbValue(salary.EFFECT_FROM));
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", GetDbValue(salary.IS_INACTIVE));

                        object result = cmd.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public EmployeeListResponse GetAllEmployeeSalaries()
        {
            EmployeeListResponse response = new EmployeeListResponse();
            response.Data = new List<EmployeeSalaryUpdate>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new EmployeeSalaryUpdate
                                {
                                    ID = reader["ID"] as int?,
                                    COMPANY_ID = reader["COMPANY_ID"] as int?,
                                    EMP_ID = reader["EMP_ID"] as int?,
                                    HEAD_ID = reader["HEAD_ID"] as int?,
                                    HEAD_PERCENT = reader["HEAD_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_PERCENT"]) : 0,
                                    HEAD_AMOUNT = reader["HEAD_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_AMOUNT"]) : 0,
                                    FIN_ID = reader["FIN_ID"] as int?,
                                    EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]).ToString("yyyy-MM-dd") : null,
                                    IS_INACTIVE = reader["IS_INACTIVE"] as bool?
                                });
                            }
                        }
                    }

                    // Set success response
                    response.flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
                response.Data = null;
            }

            return response;
        }

        public EmployeeSalaryUpdate GetItem(int id)
        {
            EmployeeSalaryUpdate salary = new EmployeeSalaryUpdate();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // SELECT
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                salary.ID = reader["ID"] as int?;
                                salary.COMPANY_ID = reader["COMPANY_ID"] as int?;
                                salary.EMP_ID = reader["EMP_ID"] as int?;
                                salary.HEAD_ID = reader["HEAD_ID"] as int?;
                                salary.HEAD_PERCENT = reader["HEAD_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_PERCENT"]) : 0;
                                salary.HEAD_AMOUNT = reader["HEAD_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_AMOUNT"]) : 0;
                                salary.FIN_ID = reader["FIN_ID"] as int?;
                                salary.EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]).ToString("yyyy-MM-dd") : null;
                                salary.IS_INACTIVE = reader["IS_INACTIVE"] as bool?;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return salary;
        }

        public bool DeleteEmployeeSalary(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
