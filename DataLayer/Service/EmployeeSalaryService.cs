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

                    foreach (var detail in salary.Details)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 1);
                            cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID);
                            cmd.Parameters.AddWithValue("@EMP_ID", salary.EMP_CODE);
                            cmd.Parameters.AddWithValue("@SALARY", salary.SALARY);
                            cmd.Parameters.AddWithValue("@HEAD_ID", detail.HEAD_ID);
                            cmd.Parameters.AddWithValue("@HEAD_PERCENT", detail.HEAD_PERCENT);
                            cmd.Parameters.AddWithValue("@HEAD_AMOUNT", detail.HEAD_AMOUNT);
                            //cmd.Parameters.AddWithValue("@FIN_ID", detail.FIN_ID);
                            cmd.Parameters.AddWithValue("@EFFECT_FROM", detail.EFFECT_FROM);
                            cmd.Parameters.AddWithValue("@IS_INACTIVE", detail.IS_INACTIVE);

                            Int32 result = Convert.ToInt32(cmd.ExecuteScalar());
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving data: " + ex.Message);
            }
            return 0; // Return a default value if no records are processed
        }
        public Int32 EditData(EmployeeSalaryUpdate salary)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    foreach (var detail in salary.Details)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2); // Update action
                            cmd.Parameters.AddWithValue("@ID", salary.ID); // Assuming each detail has an ID
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID);
                            cmd.Parameters.AddWithValue("@EMP_ID", salary.EMP_CODE);
                            cmd.Parameters.AddWithValue("@HEAD_ID", detail.HEAD_ID);
                            cmd.Parameters.AddWithValue("@HEAD_PERCENT", detail.HEAD_PERCENT);
                            cmd.Parameters.AddWithValue("@HEAD_AMOUNT", detail.HEAD_AMOUNT);
                            //cmd.Parameters.AddWithValue("@FIN_ID", detail.FIN_ID);
                            cmd.Parameters.AddWithValue("@EFFECT_FROM", detail.EFFECT_FROM);
                            cmd.Parameters.AddWithValue("@IS_INACTIVE", detail.IS_INACTIVE);

                            object result = cmd.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                return Convert.ToInt32(result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating data: " + ex.Message);
            }
            return 0; // Return a default value if no records are processed
        }

        public EmployeeListResponse GetAllEmployeeSalaries()
        {
            EmployeeListResponse response = new EmployeeListResponse { Data = new List<EmployeeSalaryUpdate>() };
            Dictionary<int, EmployeeSalaryUpdate> employeeDict = new Dictionary<int, EmployeeSalaryUpdate>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 8); // List action

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int empId = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;

                                if (!employeeDict.ContainsKey(empId))
                                {
                                    var employeeSalaryUpdate = new EmployeeSalaryUpdate
                                    {
                                        ID = empId,
                                        COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                        EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
                                        SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null,
                                        EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]) : (DateTime?)null,
                                        IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null,
                                        Details = new List<SalaryHeadDetail>()
                                    };
                                    employeeDict[empId] = employeeSalaryUpdate;
                                }

                                var detail = new SalaryHeadDetail
                                {
                                    HEAD_ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
                                    HEAD_AMOUNT = reader["Amount"] != DBNull.Value ? Convert.ToSingle(reader["Amount"]) : (float?)null,
                                    HEAD_PERCENT = reader["Percentage"] != DBNull.Value ? Convert.ToSingle(reader["Percentage"]) : (float?)null
                                };

                                employeeDict[empId].Details.Add(detail);
                            }
                        }
                    }
                }


                response.Data = employeeDict.Values.ToList();
                response.flag = 1;
                response.Message = "Success";
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
                        cmd.Parameters.AddWithValue("@ACTION", 7); 
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                salary.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null;
                                salary.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null;
                                salary.EMP_CODE = reader["EMP_ID"] != DBNull.Value ? Convert.ToString(reader["EMP_ID"]) : null;
                                salary.SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null;
                                salary.EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]) : (DateTime?)null;
                                salary.IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null;

                                // Assuming you have a JSON column for details
                                if (reader["Details"] != DBNull.Value)
                                {
                                    string detailsJson = reader["Details"].ToString();
                                    salary.Details = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SalaryHeadDetail>>(detailsJson);
                                }
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

        public EmployeeSalarySettingsListResponse GetEmployeeSalarySettings(int filterAction)
        {
            EmployeeSalarySettingsListResponse response = new EmployeeSalarySettingsListResponse();
            response.Data = new List<EmployeeSalarySettingsList>();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", filterAction); 
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new EmployeeSalarySettingsList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? reader["EMP_CODE"].ToString() : string.Empty,
                                    EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? reader["EMP_NAME"].ToString() : string.Empty,
                                    DESG_NAME = reader["Designation"] != DBNull.Value ? reader["Designation"].ToString() : string.Empty,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : 0,
                                    SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : 0,
                                    EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]) : (DateTime?)null
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
                response.Message = "Error: " + ex.Message;
                response.Data = null;
            }
            return response;
        }
       

    }
}
