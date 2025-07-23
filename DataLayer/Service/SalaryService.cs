using MicroApi.Helper;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Models;
using MicroApi.DataLayer.Interface;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        cmd.Parameters.AddWithValue("@ACTION", 1);

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
        public PayrollViewResponse GetPayrollDetails(int id)
        {
            PayrollViewResponse response = null;

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_SALARY_LIST", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@PAYDETAIL_ID", id);

                    //con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (response == null)
                                {
                                    response = new PayrollViewResponse
                                    {
                                        PAYDETAIL_ID = Convert.ToInt32(reader["PAYDETAIL_ID"]),
                                        EMPLOYEE_ID = Convert.ToInt32(reader["EMP_ID"]),
                                        EMPLOYEE_CODE = reader["EMP_CODE"].ToString(),
                                        EMPLOYEE_NAME = reader["EMP_NAME"].ToString(),
                                        MONTH = Convert.ToDateTime(reader["SAL_MONTH"]).ToString("dd-MM-yyyy"),
                                        BASIC_SALARY = Convert.ToDecimal(reader["BASIC_PAY"]),
                                        WORKED_DAYS = Convert.ToDecimal(reader["WORKED_DAYS"]),
                                        OT_HOURS = Convert.ToDecimal(reader["NOT_HOURS"]),
                                        LESS_HOURS = Convert.ToDecimal(reader["LESS_HOURS"]),
                                        DATA = new List<SalaryHeadData>()
                                    };
                                }

                                SalaryHeadData head = new SalaryHeadData
                                {
                                    HEAD_ID = Convert.ToInt32(reader["HEAD_ID"]),
                                    HEAD_NAME = reader["HEAD_NAME"].ToString(),
                                    HEAD_TYPE = Convert.ToInt32(reader["HEAD_TYPE"]),
                                    GROSS_AMOUNT = Convert.ToDecimal(reader["GROSS_AMOUNT"]),
                                    DEDUCTION_AMOUNT = Convert.ToDecimal(reader["DEDUCTION_AMOUNT"])
                                };

                                response.DATA.Add(head);
                            }

                            // ✅ Set flag and message after successful read
                            response.flag = 1;
                            response.Message = "Success";
                        }
                    }
                }
            }

            // If no data found, return a response with flag = 0
            return response ?? new PayrollViewResponse
            {
                flag = 0,
                Message = "No data found"
            };
        }

        public PayrollResponse Edit(UpdateItemRequest model)
        {
            PayrollResponse response = new PayrollResponse();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Step 1: Fetch existing LOAN_ID and REMARKS
                    Dictionary<int, (int loanId, string remarks)> existingData = new Dictionary<int, (int, string)>();

                    using (SqlCommand fetchCmd = new SqlCommand(@"
                        SELECT HEAD_ID, LOAN_ID, REMARKS 
                        FROM TB_PAY_SALARY_ITEMS 
                        WHERE PAY_DETAIL_ID = @PayDetailId", conn))
                    {
                        fetchCmd.Parameters.AddWithValue("@PayDetailId", model.PAYDETAIL_ID);

                        using (SqlDataReader reader = fetchCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int headId = Convert.ToInt32(reader["HEAD_ID"]);
                                int loanId = reader["LOAN_ID"] != DBNull.Value ? Convert.ToInt32(reader["LOAN_ID"]) : 0;
                                string remarks = reader["REMARKS"]?.ToString() ?? "";
                                existingData[headId] = (loanId, remarks);
                            }
                        }
                    }

                    // Step 2: Prepare UDT with preserved LOAN_ID and REMARKS
                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_LIST", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@PAYDETAIL_ID", model.PAYDETAIL_ID);
                        cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT);


                        // Define UDT structure
                        DataTable dt = new DataTable();
                        dt.Columns.Add("PAY_DETAIL_ID", typeof(int));
                        dt.Columns.Add("HEAD_ID", typeof(int));
                        dt.Columns.Add("AMOUNT", typeof(decimal));
                        dt.Columns.Add("LOAN_ID", typeof(int));
                        dt.Columns.Add("REMARKS", typeof(string));

                        foreach (var item in model.SALARY)
                        {
                            int loanId = 0;
                            string remarks = "";

                            // If existing data has entry, use it
                            if (existingData.TryGetValue(item.HEAD_ID, out var existing))
                            {
                                loanId = existing.loanId;
                                remarks = existing.remarks;
                            }

                            dt.Rows.Add(model.PAYDETAIL_ID, item.HEAD_ID, item.AMOUNT, loanId, remarks);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_SALARY_ITEMS", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_SALARY_ITEMS";

                        cmd.ExecuteNonQuery();
                    }

                    response.flag = 1;
                    response.Message = "Salary updated";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error updating salary items: " + ex.Message;
            }

            return response;
        }
        public SalaryApproveResponse CommitGenerateSalary(SalaryApprove request)
        {
            SalaryApproveResponse response = new SalaryApproveResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_SALARY_APPROVE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Create UDT
                        DataTable payDetailTable = new DataTable();
                        payDetailTable.Columns.Add("PAYDETAIL_ID", typeof(int));

                        foreach (int id in request.PAYDETAIL_ID)
                        {
                            payDetailTable.Rows.Add(id);
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_SALARY_DETAIL_ID", payDetailTable);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_SALARY_DETAIL_ID";
                        cmd.Parameters.AddWithValue("@ACTION", 1);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            response.flag = 1;
                            response.Message = "Success";
                        }
                        else
                        {
                            response.flag = 0;
                            response.Message = "No Data Processed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = -1;
                response.Message = ex.Message;
            }

            return response;
        }


    }
}
