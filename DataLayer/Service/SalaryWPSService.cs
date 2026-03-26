using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SalaryWPSService:ISalaryWPSService
    {
        public SalaryWPSResponse GetSalaryWPS(SalaryWPSRequest request)
        {
            var response = new SalaryWPSResponse
            {
                data = new List<SalaryWPS>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_RPT_SALARY_WPS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID",string.IsNullOrEmpty(request.DEPARTMENT_ID) ? "" : request.DEPARTMENT_ID);

                    cmd.Parameters.AddWithValue("@SAL_MONTH", request.SAL_MONTH);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.data.Add(new SalaryWPS
                            {
                                MOL_NUMBER = rdr["MOL_NUMBER"]?.ToString(),
                                BANK_CODE = rdr["BANK_CODE"]?.ToString(),
                                BANK_AC_NO = rdr["BANK_AC_NO"]?.ToString(),
                                WORKED_DAYS = rdr["WORKED_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["WORKED_DAYS"]),
                                FIXED_SALARY = rdr["FIXED_SALARY"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["FIXED_SALARY"]),
                                VARIABLE_SALARY = rdr["VARIABLE_SALARY"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["VARIABLE_SALARY"]),
                                LEAVE_DAYS = rdr["LEAVE_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["LEAVE_DAYS"]),
                            });
                        }
                    }

                    response.flag = 1;
                    response.message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }

    }
}
