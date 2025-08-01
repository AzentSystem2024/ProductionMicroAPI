using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Transactions;

namespace MicroApi.DataLayer.Service
{
    public class EmployeeSalaryService:IEmployeeSalaryService
    {
        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[]
            {
            "dd-MM-yyyy HH:mm:ss",
            "dd-MM-yyyy",
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd",
            "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy"
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public int SaveData(EmployeeSalarySave salary)
        {
            using SqlConnection conn = ADO.GetConnection();
            conn.Open();
            using SqlTransaction tr = conn.BeginTransaction();
            try
            {
                DataTable tvp = new DataTable();
                tvp.Columns.Add("HEAD_ID", typeof(int));
                tvp.Columns.Add("HEAD_PERCENT", typeof(decimal));
                tvp.Columns.Add("HEAD_AMOUNT", typeof(decimal));
                tvp.Columns.Add("HEAD_NATURE", typeof(int));
                tvp.Columns.Add("IS_INACTIVE", typeof(bool));

                foreach (var detail in salary.Details)
                {
                    tvp.Rows.Add(
                        detail.HEAD_ID ?? 0,
                        detail.HEAD_PERCENT ?? 0,
                        detail.HEAD_AMOUNT ?? 0,
                        string.IsNullOrEmpty(detail.HEAD_NATURE) ? 0 : Convert.ToInt32(detail.HEAD_NATURE),
                        detail.IS_INACTIVE ?? false);
                }

                using SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", conn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 1);
                cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID ?? 0);
                cmd.Parameters.AddWithValue("@EMP_ID", salary.EMP_ID ?? 0);
                cmd.Parameters.AddWithValue("@FIN_ID", salary.FIN_ID ?? 0);
                cmd.Parameters.AddWithValue("@SALARY", salary.SALARY ?? 0);
                cmd.Parameters.AddWithValue("@EFFECT_FROM", ParseDate(salary.EFFECT_FROM));
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@HEAD_DETAILS", tvp);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.UDT_TB_SALARY_HEAD_DETAIL";

                cmd.ExecuteNonQuery();
                tr.Commit();
                return salary.EMP_ID ?? 0;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw new Exception("Error saving data: " + ex.Message);
            }
        }

        public int EditData(EmployeeSalarySave salary)
        {
            using SqlConnection conn = ADO.GetConnection();
            conn.Open();
            using SqlTransaction tr = conn.BeginTransaction();
            try
            {
                DataTable tvp = new DataTable();
                tvp.Columns.Add("HEAD_ID", typeof(int));
                tvp.Columns.Add("HEAD_PERCENT", typeof(decimal));
                tvp.Columns.Add("HEAD_AMOUNT", typeof(decimal));
                tvp.Columns.Add("HEAD_NATURE", typeof(int));
                tvp.Columns.Add("IS_INACTIVE", typeof(bool));

                foreach (var detail in salary.Details)
                {
                    tvp.Rows.Add(
                        detail.HEAD_ID ?? 0,
                        detail.HEAD_PERCENT ?? 0,
                        detail.HEAD_AMOUNT ?? 0,
                        string.IsNullOrEmpty(detail.HEAD_NATURE) ? 0 : Convert.ToInt32(detail.HEAD_NATURE),
                        detail.IS_INACTIVE ?? false);
                }

                using SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", conn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 2);
                cmd.Parameters.AddWithValue("@BATCH_ID", salary.BATCH_ID ?? 0);
                cmd.Parameters.AddWithValue("@EMP_ID", salary.EMP_ID ?? 0);
                cmd.Parameters.AddWithValue("@COMPANY_ID", salary.COMPANY_ID ?? 0);
                cmd.Parameters.AddWithValue("@FIN_ID", salary.FIN_ID ?? 0);
                cmd.Parameters.AddWithValue("@SALARY", salary.SALARY ?? 0);
                cmd.Parameters.AddWithValue("@EFFECT_FROM", ParseDate(salary.EFFECT_FROM));
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@HEAD_DETAILS", tvp);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.UDT_TB_SALARY_HEAD_DETAIL";

                cmd.ExecuteNonQuery();
                tr.Commit();
                return salary.EMP_ID ?? 0;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw new Exception("Error editing data: " + ex.Message);
            }
        }


        public EmployeeListResponse GetAllEmployeeSalaries(int empId, int companyId)
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
                        cmd.Parameters.AddWithValue("@EMP_ID", empId);
                       // cmd.Parameters.AddWithValue("@EFFECT_FROM", effectFrom);
                        //cmd.Parameters.AddWithValue("@BATCH_ID", BATCHID);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmployeeSalaryUpdate employeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : (int?)null,
                                    BATCH_ID = reader["BATCH_ID"] != DBNull.Value ? Convert.ToInt32(reader["BATCH_ID"]) : (int?)null,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                    EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
                                    EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? Convert.ToString(reader["EMP_NAME"]) : null,
                                    EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToString(reader["EFFECT_FROM"]) : null,
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
                        SH.HEAD_NATURE,
                        HEAD_AMOUNT,
                        HEAD_PERCENT
                    FROM TB_SALARY_HEAD SH
                    WHERE SH.IS_DELETED = 0 AND IS_INACTIVE = 0 AND HEAD_NATURE IN (1, 2)
                    ORDER BY SH.HEAD_ORDER", connection))
                        {
                            using (SqlDataReader Reader = defaultCmd.ExecuteReader())
                            {
                                EmployeeSalaryUpdate defaultEmployeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = empId,
                                    BATCH_ID = 0,
                                    COMPANY_ID = companyId,
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


        public EmployeeListResponse GetItem(int empId, string effectfrom, int batchId)
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
                        cmd.Parameters.AddWithValue("@EMP_ID", empId);
                        cmd.Parameters.AddWithValue("@EFFECT_FROM", effectfrom);
                        cmd.Parameters.AddWithValue("@BATCH_ID", batchId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmployeeSalaryUpdate employeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : (int?)null,
                                    BATCH_ID = reader["BATCH_ID"] != DBNull.Value ? Convert.ToInt32(reader["BATCH_ID"]) : (int?)null,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                    EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? Convert.ToString(reader["EMP_CODE"]) : null,
                                    EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? Convert.ToString(reader["EMP_NAME"]) : null,
                                    DESG_NAME = reader["Designation"] != DBNull.Value ? Convert.ToString(reader["Designation"]) : null,
                                    EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToString(reader["EFFECT_FROM"]) : null,
                                    PREVIOUS_EFFECT_FROM = reader["PREVIOUS_EFFECT_FROM"] != DBNull.Value ? Convert.ToString(reader["PREVIOUS_EFFECT_FROM"]) : null,
                                    SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : 0,
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
                        SH.HEAD_NATURE,
                         HEAD_AMOUNT,
                         HEAD_PERCENT
                    FROM TB_SALARY_HEAD SH
                    WHERE SH.IS_DELETED = 0 AND IS_INACTIVE = 0 AND HEAD_NATURE IN (1, 2)
                    ORDER BY SH.HEAD_ORDER", connection))
                        {
                            using (SqlDataReader Reader = defaultCmd.ExecuteReader())
                            {
                                EmployeeSalaryUpdate defaultEmployeeSalaryUpdate = new EmployeeSalaryUpdate
                                {
                                    ID = empId,
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
        



        public bool DeleteEmployeeSalary(int batchId)
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
                        cmd.Parameters.AddWithValue("@BATCH_ID", batchId);
                       // cmd.Parameters.AddWithValue("@EFFECT_FROM", effectfrom);



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

        public EmployeeSalarySettingsListResponse GetEmployeeSalarySettings(int filterAction, int companyId)
        {
            EmployeeSalarySettingsListResponse response = new EmployeeSalarySettingsListResponse();
            response.Data = new List<EmployeeSalarySettingsList>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_SALARY", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", filterAction);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new EmployeeSalarySettingsList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : (int?)null,
                                    BATCH_ID = reader["BATCH_ID"] != DBNull.Value ? Convert.ToInt32(reader["BATCH_ID"]) : (int?)null,
                                    EMP_CODE = reader["EMP_CODE"] != DBNull.Value ? reader["EMP_CODE"].ToString() : string.Empty,
                                    EMP_NAME = reader["EMP_NAME"] != DBNull.Value ? reader["EMP_NAME"].ToString() : string.Empty,
                                    DESG_NAME = reader["Designation"] != DBNull.Value ? reader["Designation"].ToString() : string.Empty,
                                    COMPANY_ID = reader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_ID"]) : (int?)null,
                                    SALARY = reader["SALARY"] != DBNull.Value ? Convert.ToDecimal(reader["SALARY"]) : 0,
                                    EFFECT_FROM = reader["EFFECT_FROM"] != DBNull.Value ? Convert.ToString(reader["EFFECT_FROM"]) : null
                                };
                                response.Data.Add(item);
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



    }
}
