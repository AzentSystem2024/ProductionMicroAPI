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
                            cmd.Parameters.AddWithValue("@ACTION", 1); // Action for insert
                            cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID);
                            cmd.Parameters.AddWithValue("@FIN_ID", salary.FIN_ID);
                            cmd.Parameters.AddWithValue("@EMP_CODE", salary.EMP_CODE); // Use EMP_CODE to fetch EMP_ID
                            cmd.Parameters.AddWithValue("@SALARY", salary.SALARY);
                            cmd.Parameters.AddWithValue("@HEAD_ID", detail.HEAD_ID);
                            cmd.Parameters.AddWithValue("@HEAD_PERCENT", detail.HEAD_PERCENT);
                            cmd.Parameters.AddWithValue("@HEAD_AMOUNT", detail.HEAD_AMOUNT);
                            cmd.Parameters.AddWithValue("@EFFECT_FROM", salary.EFFECT_FROM);
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
            return 0;
        }

        public Int32 EditData(EmployeeSalarySave salary)
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
                            cmd.Parameters.AddWithValue("@ID", salary.ID);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID);
                            cmd.Parameters.AddWithValue("@FIN_ID", salary.FIN_ID);
                            cmd.Parameters.AddWithValue("@EMP_CODE", salary.EMP_CODE);
                            cmd.Parameters.AddWithValue("@SALARY", salary.SALARY);
                            cmd.Parameters.AddWithValue("@HEAD_ID", detail.HEAD_ID);
                            cmd.Parameters.AddWithValue("@HEAD_PERCENT", detail.HEAD_PERCENT);
                            cmd.Parameters.AddWithValue("@HEAD_AMOUNT", detail.HEAD_AMOUNT);
                            cmd.Parameters.AddWithValue("@EFFECT_FROM", salary.EFFECT_FROM);
                            cmd.Parameters.AddWithValue("@IS_INACTIVE", detail.IS_INACTIVE);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    salary.ID = reader["EMP_ID"] != DBNull.Value ? Convert.ToInt32(reader["EMP_ID"]) : (int?)null;
                                    salary.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null;
                                   // salary.EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? reader["EMP_CODE"].ToString() : null;
                                    salary.SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null;
                                    salary.EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]) : (DateTime?)null;
                                    //salary.IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null;
                                }
                            }
                        }
                    }
                }
            }
                catch (Exception ex)
            {
                throw new Exception("Error updating data: " + ex.Message);
            }
            return salary.ID ?? 0;
        }

        public EmployeeListResponse GetAllEmployeeSalaries(int EMPID,int COMAPNYID)
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
                        cmd.Parameters.AddWithValue("@EMP_ID", EMPID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", COMAPNYID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int empId = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;

                                if (!employeeDict.ContainsKey(empId))
                                {
                                    var employeeSalaryUpdate = new EmployeeSalaryUpdate
                                    {
                                        ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                        COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                        EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
                                        EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? Convert.ToString(reader["EMP_NAME"]) : null,
                                        DESG_NAME = reader["Designation"] !=DBNull.Value ? Convert.ToString(reader["Designation"]) : null,
                                        SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null,
                                        EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]) : (DateTime?)null,
                                        IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null,
                                        Details = new List<SalaryHeadDetail>()
                                    };
                                    employeeDict[empId] = employeeSalaryUpdate;
                                }

                                var detail = new SalaryHeadDetail
                                {
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : (int?)null,
                                    HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
                                    HEAD_NATURE = reader["HEAD_NATURE"] != DBNull.Value ? reader["HEAD_NATURE"].ToString() : null,
                                    HEAD_AMOUNT = reader["Amount"] != DBNull.Value ? Convert.ToSingle(reader["Amount"]) : (float?)null,
                                    HEAD_PERCENT = reader["Percentage"] != DBNull.Value ? Convert.ToSingle(reader["Percentage"]) : (float?)null,
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null,

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
            EmployeeSalaryUpdate salary = new EmployeeSalaryUpdate
            {
                Details = new List<SalaryHeadDetail>() // Initialize the Details list
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 7); // Select by ID action
                        cmd.Parameters.AddWithValue("@EMP_ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Initialize salary object properties if not already set
                                if (salary.ID == null)
                                {
                                    salary.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null;
                                    salary.EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? reader["EMP_CODE"].ToString() : null;
                                    salary.EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? reader["EMP_NAME"].ToString() : null;
                                    salary.COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null;
                                    salary.DESG_NAME = reader["Designation"] != DBNull.Value ? reader["Designation"].ToString() : null;
                                    salary.SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null;
                                    salary.EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToDateTime(reader["EFFECT_FROM"]) : (DateTime?)null;
                                    salary.IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null;
                                }

                                // Add each detail to the Details list
                                var detail = new SalaryHeadDetail
                                {
                                    HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : (int?)null,
                                    HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
                                    HEAD_NATURE = reader["HEAD_NATURE"] != DBNull.Value ? reader["HEAD_NATURE"].ToString() : null,
                                    HEAD_AMOUNT = reader["Amount"] != DBNull.Value ? Convert.ToSingle(reader["Amount"]) : (float?)null,
                                    HEAD_PERCENT = reader["Percentage"] != DBNull.Value ? Convert.ToSingle(reader["Percentage"]) : (float?)null,
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : (bool?)null,

                                };

                                salary.Details.Add(detail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine(ex.ToString());
                throw new Exception("Error retrieving data: " + ex.Message);
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
