using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace MicroApi.DataLayer.Service
{
    public class EmployeeSalaryService:IEmployeeSalaryService
    {
        public Int32 SaveData(EmployeeSalarySave salary)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Fetch EMP_ID based on EMP_CODE
                        int employeeId = 0;
                        using (SqlCommand fetchCmd = new SqlCommand("SELECT ID FROM TB_EMPLOYEE WHERE EMP_CODE = @EMP_CODE", connection, transaction))
                        {
                            fetchCmd.Parameters.AddWithValue("@EMP_CODE", salary.EMP_CODE);
                            employeeId = (int)fetchCmd.ExecuteScalar();
                        }

                        // Insert each HEAD_ID detail as a new row in TB_EMPLOYEE_SALARY
                        foreach (var detail in salary.Details)
                        {
                            using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ACTION", 1); // Action for insert
                                cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                                cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID);
                                cmd.Parameters.AddWithValue("@FIN_ID", salary.FIN_ID);
                                cmd.Parameters.AddWithValue("@EMP_ID", employeeId); // Use the fetched EMP_ID
                                cmd.Parameters.AddWithValue("@SALARY", salary.SALARY);
                                cmd.Parameters.AddWithValue("@HEAD_ID", detail.HEAD_ID);
                                cmd.Parameters.AddWithValue("@HEAD_PERCENT", detail.HEAD_PERCENT);
                                cmd.Parameters.AddWithValue("@HEAD_AMOUNT", detail.HEAD_AMOUNT);
                                cmd.Parameters.AddWithValue("@EFFECT_FROM", salary.EFFECT_FROM);
                                cmd.Parameters.AddWithValue("@IS_INACTIVE", detail.IS_INACTIVE);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return employeeId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }




        public Int32 EditData(EmployeeSalarySave salary)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Fetch EMP_ID based on EMP_CODE
                        int employeeId = 0;
                        using (SqlCommand fetchCmd = new SqlCommand("SELECT ID FROM TB_EMPLOYEE WHERE EMP_CODE = @EMP_CODE", connection, transaction))
                        {
                            fetchCmd.Parameters.AddWithValue("@EMP_CODE", salary.EMP_CODE);
                            employeeId = (int)fetchCmd.ExecuteScalar();
                        }

                        foreach (var detail in salary.Details)
                        {
                            using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection, transaction))
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

                                cmd.ExecuteNonQuery();
                            }
                        }


                        transaction.Commit();
                        return employeeId;
                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }

//public EmployeeListResponse GetAllEmployeeSalaries(int EMPID,int COMAPNYID)
//{
//    EmployeeListResponse response = new EmployeeListResponse { Data = new List<EmployeeSalaryUpdate>() };
//    Dictionary<int, EmployeeSalaryUpdate> employeeDict = new Dictionary<int, EmployeeSalaryUpdate>();

//    try
//    {
//        using (SqlConnection connection = ADO.GetConnection())
//        {
//            if (connection.State == ConnectionState.Closed)
//                connection.Open();

//            EmployeeSalaryUpdate employeeSalaryUpdate = null;
//            using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
//            {
//                cmd.CommandType = CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@ACTION", 8); // List action
//                cmd.Parameters.AddWithValue("@EMP_ID", EMPID);
//                cmd.Parameters.AddWithValue("@COMPANY_ID", COMAPNYID);

//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        employeeSalaryUpdate = new EmployeeSalaryUpdate
//                        {
//                            ID = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : (int?)null,
//                            COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
//                            EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
//                            EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? Convert.ToString(reader["EMP_NAME"]) : null,
//                            DESG_NAME = reader["Designation"] != DBNull.Value ? Convert.ToString(reader["Designation"]) : null,
//                            Details = new List<SalaryHeadDetail>()
//                        };
//                    }
//                }
//            }

//            if (employeeSalaryUpdate != null)
//            {
//                // Check if the employee has a record in TB_EMPLOYEE_SALARY
//                using (SqlCommand cmd = new SqlCommand("SELECT 1 FROM TB_EMPLOYEE_SALARY WHERE EMP_ID = @EMP_ID AND IS_DELETED = 0", connection))
//                {
//                    cmd.Parameters.AddWithValue("@EMP_ID", EMPID);
//                    bool hasSalaryRecord = cmd.ExecuteScalar() != null;

//                    if (hasSalaryRecord)
//                    {
//                        // If yes, get the latest effective from
//                        using (SqlCommand cmdDetails = new SqlCommand(@"
//                            SELECT ES.SALARY, ES.EFFECT_FROM, ES.IS_INACTIVE,
//                                   SH.ID AS HEAD_ID, SH.HEAD_NAME,
//                                   CASE SH.HEAD_NATURE WHEN 1 THEN 'FixedAmount' WHEN 2 THEN 'Percentage' END AS HEAD_NATURE,
//                                   SH.HEAD_AMOUNT AS Amount, SH.HEAD_PERCENT AS Percentage, SH.IS_INACTIVE AS HEAD_IS_INACTIVE,
//                    CASE WHEN ESH.HEAD_ID IS NOT NULL THEN 1 ELSE 0 END AS Selected
//                            FROM TB_SALARY_HEAD SH
//                            INNER JOIN TB_EMPLOYEE_SALARY ES ON SH.ID = ES.HEAD_ID AND ES.IS_DELETED = 0
//                            LEFT JOIN TB_EMPLOYEE_SALARY ESH ON ES.EFFECT_FROM = ESH.EFFECT_FROM AND ESH.IS_DELETED = 0
//                            LEFT JOIN TB_SALARY_HEAD SHT ON ESH.HEAD_ID = SHT.ID AND SHT.IS_DELETED = 0
//                            WHERE (SH.IS_DELETED = 0 AND SH.HEAD_NATURE IN (1, 2))
//                            ORDER BY SH.HEAD_ORDER", connection))
//                        {
//                            cmdDetails.Parameters.AddWithValue("@EMP_ID", EMPID);
//                            using (SqlDataReader reader = cmdDetails.ExecuteReader())
//                            {
//                                while (reader.Read())
//                                {
//                                    var detail = new SalaryHeadDetail
//                                    {
//                                        HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : (int?)null,
//                                        HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
//                                        HEAD_NATURE = reader["HEAD_NATURE"] != DBNull.Value ? reader["HEAD_NATURE"].ToString() : null,
//                                        HEAD_AMOUNT = reader["Amount"] != DBNull.Value ? Convert.ToSingle(reader["Amount"]) : (float?)null,
//                                        HEAD_PERCENT = reader["Percentage"] != DBNull.Value ? Convert.ToSingle(reader["Percentage"]) : (float?)null,
//                                        IS_INACTIVE = reader["HEAD_IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["HEAD_IS_INACTIVE"]) : (bool?)null,
//                                        Selected = reader["Selected"] != DBNull.Value ? Convert.ToBoolean(reader["Selected"]) : false
//                                    };
//                                    employeeSalaryUpdate.Details.Add(detail);
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        // If no, list all salary heads with default values
//                        using (SqlCommand cmdDefault = new SqlCommand(@"
//                            SELECT 0 AS SALARY, NULL AS EFFECT_FROM,
//                                   SH.ID AS HEAD_ID, SH.HEAD_NAME,
//                                   CASE SH.HEAD_NATURE WHEN 1 THEN 'FixedAmount' WHEN 2 THEN 'Percentage' END AS HEAD_NATURE,
//                                   0 AS Amount, 0 AS Percentage,
//                                   0 AS Selected
//                            FROM TB_SALARY_HEAD SH
//                            WHERE SH.IS_DELETED = 0 AND IS_INACTIVE = 0 AND HEAD_NATURE IN (1, 2)
//                            ORDER BY SH.HEAD_ORDER", connection))
//                        {
//                            using (SqlDataReader reader = cmdDefault.ExecuteReader())
//                            {
//                                while (reader.Read())
//                                {
//                                    var detail = new SalaryHeadDetail
//                                    {
//                                        HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : (int?)null,
//                                        HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
//                                        HEAD_NATURE = reader["HEAD_NATURE"] != DBNull.Value ? reader["HEAD_NATURE"].ToString() : null,
//                                        HEAD_AMOUNT = 0,
//                                        HEAD_PERCENT = 0,
//                                        IS_INACTIVE = false,
//                                        Selected = reader["Selected"] != DBNull.Value ? Convert.ToBoolean(reader["Selected"]) : false

//                                    };
//                                    employeeSalaryUpdate.Details.Add(detail);
//                                }
//                            }
//                        }
//                    }
//                }

//                response.Data.Add(employeeSalaryUpdate);
//            }

//            response.flag = 1;
//            response.Message = "Success";
//        }
//    }
//    catch (Exception ex)
//    {
//        response.flag = 0;
//        response.Message = "Error: " + ex.Message;
//        response.Data = null;
//    }

//    return response;
//}        
        public EmployeeListResponse GetAllEmployeeSalaries(int EMPID, int COMPANYID)
        {
            EmployeeListResponse response = new EmployeeListResponse { Data = new List<EmployeeSalaryUpdate>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // First query to check if the employee has a salary record
                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 8);
                        cmd.Parameters.AddWithValue("@EMP_ID", EMPID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", COMPANYID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmployeeSalaryUpdate employeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : (int?)null,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                    EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
                                    EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? Convert.ToString(reader["EMP_NAME"]) : null,
                                    DESG_NAME = reader["Designation"] != DBNull.Value ? Convert.ToString(reader["Designation"]) : null,
                                    SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null,
                                    Details = new List<SalaryHeadDetail>()
                                };

                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        var detail = new SalaryHeadDetail
                                        {
                                            HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : (int?)null,
                                            HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
                                            HEAD_NATURE = reader["HEAD_NATURE"] != DBNull.Value ? reader["HEAD_NATURE"].ToString() : null,
                                            HEAD_AMOUNT = reader["HEAD_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_AMOUNT"]) : (float?)0,
                                            HEAD_PERCENT = reader["HEAD_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_PERCENT"]) : (float?)0
                                        };
                                        employeeSalaryUpdate.Details.Add(detail);
                                    }
                                }

                                response.Data.Add(employeeSalaryUpdate);
                            }
                        }
                    }

                    // If no records were found, execute the second query to get default salary heads
                    if (response.Data.Count == 0)
                    {
                        using (SqlCommand defaultCmd = new SqlCommand(@"
                    SELECT
                        0 AS SALARY,
                        NULL AS EFFECT_FROM,
                        SH.ID AS HEAD_ID,
                        SH.HEAD_NAME,
                        CASE SH.HEAD_NATURE
                            WHEN 1 THEN 'FixedAmount'
                            WHEN 2 THEN 'Percentage'
                        END AS HEAD_NATURE,
                        0 AS HEAD_AMOUNT,
                        0 AS HEAD_PERCENT
                    FROM TB_SALARY_HEAD SH
                    WHERE SH.IS_DELETED = 0 AND IS_INACTIVE = 0 AND HEAD_NATURE IN (1, 2)
                    ORDER BY SH.HEAD_ORDER", connection))
                        {
                            using (SqlDataReader Reader = defaultCmd.ExecuteReader())
                            {
                                EmployeeSalaryUpdate defaultEmployeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = EMPID,
                                    COMPANY_ID = COMPANYID,
                                    EMP_CODE = null,
                                    EMP_NAME = null,
                                    DESG_NAME = null,
                                    SALARY = 0,
                                    Details = new List<SalaryHeadDetail>()
                                };

                                if (Reader.Read())
                                {
                                    var detail = new SalaryHeadDetail
                                    {
                                        HEAD_ID = Reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(Reader["HEAD_ID"]) : (int?)null,
                                        HEAD_NAME = Reader["HEAD_NAME"] != DBNull.Value ? Reader["HEAD_NAME"].ToString() : null,
                                        HEAD_NATURE = Reader["HEAD_NATURE"] != DBNull.Value ? Reader["HEAD_NATURE"].ToString() : null,
                                        HEAD_AMOUNT = 0,
                                        HEAD_PERCENT = 0
                                    };
                                    defaultEmployeeSalaryUpdate.Details.Add(detail);
                                }

                                response.Data.Add(defaultEmployeeSalaryUpdate);
                            }
                        }
                    }
                }

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


        public EmployeeListResponse GetItem(int id)
        {
            EmployeeListResponse responses = new EmployeeListResponse { Data = new List<EmployeeSalaryUpdate>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // First query to check if the employee has a salary record
                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 7);
                        cmd.Parameters.AddWithValue("@EMP_ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmployeeSalaryUpdate employeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : (int?)null,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                    EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
                                    EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? Convert.ToString(reader["EMP_NAME"]) : null,
                                    DESG_NAME = reader["Designation"] != DBNull.Value ? Convert.ToString(reader["Designation"]) : null,
                                    SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : (decimal?)null,
                                    Details = new List<SalaryHeadDetail>()
                                };

                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        var detail = new SalaryHeadDetail
                                        {
                                            HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : (int?)null,
                                            HEAD_NAME = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : null,
                                            HEAD_NATURE = reader["HEAD_NATURE"] != DBNull.Value ? reader["HEAD_NATURE"].ToString() : null,
                                            HEAD_AMOUNT = reader["HEAD_AMOUNT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_AMOUNT"]) : (float?)0,
                                            HEAD_PERCENT = reader["HEAD_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["HEAD_PERCENT"]) : (float?)0
                                        };
                                        employeeSalaryUpdate.Details.Add(detail);
                                    }
                                }

                                responses.Data.Add(employeeSalaryUpdate);
                            }
                        }
                    }

                    // If no records were found, execute the second query to get default salary heads
                    if (responses.Data.Count == 0)
                    {
                        using (SqlCommand defaultCmd = new SqlCommand(@"
                    SELECT
                        0 AS SALARY,
                        NULL AS EFFECT_FROM,
                        SH.ID AS HEAD_ID,
                        SH.HEAD_NAME,
                        CASE SH.HEAD_NATURE
                            WHEN 1 THEN 'FixedAmount'
                            WHEN 2 THEN 'Percentage'
                        END AS HEAD_NATURE,
                        0 AS HEAD_AMOUNT,
                        0 AS HEAD_PERCENT
                    FROM TB_SALARY_HEAD SH
                    WHERE SH.IS_DELETED = 0 AND IS_INACTIVE = 0 AND HEAD_NATURE IN (1, 2)
                    ORDER BY SH.HEAD_ORDER", connection))
                        {
                            using (SqlDataReader Reader = defaultCmd.ExecuteReader())
                            {
                                EmployeeSalaryUpdate defaultEmployeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = id,
                                    COMPANY_ID = null,
                                    EMP_CODE = null,
                                    EMP_NAME = null,
                                    DESG_NAME = null,
                                    SALARY = 0,
                                    Details = new List<SalaryHeadDetail>()
                                };

                                if (Reader.Read())
                                {
                                    var detail = new SalaryHeadDetail
                                    {
                                        HEAD_ID = Reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(Reader["HEAD_ID"]) : (int?)null,
                                        HEAD_NAME = Reader["HEAD_NAME"] != DBNull.Value ? Reader["HEAD_NAME"].ToString() : null,
                                        HEAD_NATURE = Reader["HEAD_NATURE"] != DBNull.Value ? Reader["HEAD_NATURE"].ToString() : null,
                                        HEAD_AMOUNT = 0,
                                        HEAD_PERCENT = 0
                                    };
                                    defaultEmployeeSalaryUpdate.Details.Add(detail);
                                }

                                responses.Data.Add(defaultEmployeeSalaryUpdate);
                            }
                        }
                    }
                }

                responses.flag = 1;
                responses.Message = "Success";
            }
            catch (Exception ex)
            {
                responses.flag = 0;
                responses.Message = "Error: " + ex.Message;
                responses.Data = null;
            }

            return responses;
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
