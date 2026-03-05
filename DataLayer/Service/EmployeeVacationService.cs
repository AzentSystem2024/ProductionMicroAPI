using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MicroApi.DataLayer.Services
{
    public class EmployeeVacationService : IEmployeeVacationService
    {
        public EmployeeVacationLogListResponseData GetAllEmployeeVacation()
        {
            EmployeeVacationLogListResponseData loglist = new EmployeeVacationLogListResponseData();
            loglist.data = new List<EmployeeVacationLogListData>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_EMPLOYEE_VACATION";
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    loglist.data.Add(new EmployeeVacationLogListData
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        DOC_NO = Convert.IsDBNull(dr["DOC_NO"]) ? null : Convert.ToString(dr["DOC_NO"]),
                        EMP_NO = Convert.IsDBNull(dr["EMP_CODE"]) ? null : Convert.ToString(dr["EMP_CODE"]),
                        DEPT_DATE = Convert.IsDBNull(dr["DEPT_DATE"]) ? null : Convert.ToString(dr["DEPT_DATE"]),
                        REJOIN_DATE = Convert.IsDBNull(dr["REJOIN_DATE"]) ? null : Convert.ToString(dr["REJOIN_DATE"]),
                        EXPECT_RETURN = Convert.IsDBNull(dr["EXPECT_RETURN"]) ? null : Convert.ToString(dr["EXPECT_RETURN"]),
                        EMP_ID = Convert.IsDBNull(dr["EMP_ID"]) ? null : Convert.ToInt32(dr["EMP_ID"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        DATE = Convert.IsDBNull(dr["DOC_DATE"]) ? null : Convert.ToDateTime(dr["DOC_DATE"]).ToString("dd/MM/yy"),
                        LEAVE_TYPE = Convert.IsDBNull(dr["LEAVE_TYPE"]) ? null : Convert.ToString(dr["LEAVE_TYPE"]),
                        REMARKS = Convert.IsDBNull(dr["REMARKS"]) ? null : Convert.ToString(dr["REMARKS"]),
                        STATUS = Convert.IsDBNull(dr["STATUS"]) ? null : Convert.ToString(dr["STATUS"]),
                    });
                }
                connection.Close();
            }
            loglist.flag = "1";
            loglist.message = "Success";

            return loglist;
        }

        public saveEmployeeVacationResponseData SaveData(saveEmployeeVacationData vacation)
        {
            try
            {
                saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_VACATION";

                    cmd.Parameters.AddWithValue("@ACTION", 1);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)vacation.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)vacation.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)vacation.EMP_ID ?? DBNull.Value);

                    // Convert DOC_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DOC_DATE", vacation.DOC_DATE != null ? Convert.ToDateTime(vacation.DOC_DATE) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_TYPE_ID", (object)vacation.LEAVE_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VAC_DAYS", (object)vacation.VAC_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LS_PAYABLE", (object)vacation.LS_PAYABLE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_CREDIT", (object)vacation.LEAVE_CREDIT ?? DBNull.Value);

                    // Convert DEPT_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DEPT_DATE", vacation.DEPT_DATE != null ? Convert.ToDateTime(vacation.DEPT_DATE) : DBNull.Value);

                    // Convert EXPECT_RETURN to DateTime if not null
                    cmd.Parameters.AddWithValue("@EXPECT_RETURN", vacation.EXPECT_RETURN != null ? Convert.ToDateTime(vacation.EXPECT_RETURN) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)vacation.REMARKS ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveEmployeeVacationData selectEmployeeVacationData(int id)
        {
            saveEmployeeVacationData vac = new saveEmployeeVacationData();

            try
            {
                string strSQL = "SELECT vac.*, leavetype.DESCRIPTION AS LEAVE_TYPE, emp.EMP_NAME,trans.NARRATION AS REMARKS ,stat.STATUS_DESC AS STATUS " +
                    " FROM TB_EMPLOYEE_LEAVE vac LEFT JOIN TB_AC_TRANS_HEADER trans " +
                    " ON vac.TRANS_ID = trans.TRANS_ID " +
                    " LEFT JOIN TB_STATUS stat " +
                    " ON trans.TRANS_STATUS = stat.ID " +
                    " LEFT JOIN TB_EMPLOYEE emp ON emp.ID = vac.EMP_ID " +
                    " LEFT JOIN TB_LEAVE_TYPES leavetype ON leavetype.ID = vac.LEAVE_TYPE_ID " +
                    " WHERE vac.ID = " + id;

                DataTable tblVacation = ADO.GetDataTable(strSQL, "EmployeeVacation");

                if (tblVacation.Rows.Count > 0)
                {
                    DataRow dr = tblVacation.Rows[0];
                    vac.ID = ADO.ToInt32(dr["ID"]);
                    vac.DOC_NO = ADO.ToInt32(dr["DOC_NO"]);
                    vac.DOC_DATE = dr["DOC_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DOC_DATE"]);
                    vac.EMP_ID = ADO.ToInt32(dr["EMP_ID"]);
                    vac.EMP_NAME = ADO.ToString(dr["EMP_NAME"]);
                    vac.VAC_DAYS = ADO.ToString(dr["VAC_DAYS"]);
                    vac.DEPT_DATE = dr["DEPT_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DEPT_DATE"]);
                    vac.EXPECT_RETURN = dr["EXPECT_RETURN"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["EXPECT_RETURN"]);
                    vac.LEAVE_TYPE_ID = ADO.ToInt32(dr["LEAVE_TYPE_ID"]);
                    vac.LEAVE_TYPE_NAME = ADO.ToString(dr["LEAVE_TYPE"]);
                    vac.LEAVE_CREDIT = ADO.ToString(dr["LEAVE_CREDIT"]);
                    vac.LS_PAYABLE = ADO.Toboolean(dr["LS_PAYABLE"]);
                    vac.IS_TICKET = ADO.Toboolean(dr["IS_TICKET"]);
                    vac.LAST_REJOIN_DATE = dr["LAST_REJOIN_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LAST_REJOIN_DATE"]);
                    vac.TRAVELLED_DATE = dr["TRAVELLED_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TRAVELLED_DATE"]);
                    vac.REJOIN_DATE = dr["REJOIN_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["REJOIN_DATE"]);
                    vac.ACTUAL_DAYS = ADO.ToString(dr["ACTUAL_DAYS"]);
                    vac.DEDUCT_DAYS = ADO.ToString(dr["DEDUCT_DAYS"]);
                    vac.LEFT_REASON = ADO.ToString(dr["LEFT_REASON"]);
                    vac.REMARKS = ADO.ToString(dr["REMARKS"]);
                    vac.STATUS = ADO.ToString(dr["STATUS"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vac;
        }

        public saveEmployeeVacationResponseData UpdateData(saveEmployeeVacationData vacation)
        {
            try
            {
                saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_VACATION";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", vacation.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)vacation.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)vacation.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)vacation.EMP_ID ?? DBNull.Value);

                    // Convert DOC_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DOC_DATE", vacation.DOC_DATE != null ? Convert.ToDateTime(vacation.DOC_DATE) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_TYPE_ID", (object)vacation.LEAVE_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VAC_DAYS", (object)vacation.VAC_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LS_PAYABLE", (object)vacation.LS_PAYABLE ?? DBNull.Value);

                    // Convert DEPT_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DEPT_DATE", vacation.DEPT_DATE != null ? Convert.ToDateTime(vacation.DEPT_DATE) : DBNull.Value);

                    // Convert EXPECT_RETURN to DateTime if not null
                    cmd.Parameters.AddWithValue("@EXPECT_RETURN", vacation.EXPECT_RETURN != null ? Convert.ToDateTime(vacation.EXPECT_RETURN) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)vacation.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_CREDIT", (object)vacation.LEAVE_CREDIT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_TICKET", vacation.IS_TICKET != null ? Convert.ToBoolean(vacation.IS_TICKET) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LAST_REJOIN_DATE", vacation.LAST_REJOIN_DATE != null ? Convert.ToDateTime(vacation.LAST_REJOIN_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRAVELLED_DATE", vacation.TRAVELLED_DATE != null ? Convert.ToDateTime(vacation.TRAVELLED_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@REJOIN_DATE", vacation.REJOIN_DATE != null ? Convert.ToDateTime(vacation.REJOIN_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ACTUAL_DAYS", (object)vacation.ACTUAL_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEDUCT_DAYS", (object)vacation.DEDUCT_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEFT_REASON", (object)vacation.LEFT_REASON ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public saveEmployeeVacationData DeleteEmployeeVacation(int id)
        {
            try
            {
                saveEmployeeVacationData res = new saveEmployeeVacationData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_VACATION";
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public saveEmployeeVacationResponseData VerifyData(saveEmployeeVacationData vacation)
        {
            try
            {
                saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_VACATION";

                    cmd.Parameters.AddWithValue("@ACTION", 4);

                    cmd.Parameters.AddWithValue("ID", vacation.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)vacation.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)vacation.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)vacation.EMP_ID ?? DBNull.Value);

                    // Convert DOC_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DOC_DATE", vacation.DOC_DATE != null ? Convert.ToDateTime(vacation.DOC_DATE) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_TYPE_ID", (object)vacation.LEAVE_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VAC_DAYS", (object)vacation.VAC_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LS_PAYABLE", (object)vacation.LS_PAYABLE ?? DBNull.Value);

                    // Convert DEPT_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DEPT_DATE", vacation.DEPT_DATE != null ? Convert.ToDateTime(vacation.DEPT_DATE) : DBNull.Value);

                    // Convert EXPECT_RETURN to DateTime if not null
                    cmd.Parameters.AddWithValue("@EXPECT_RETURN", vacation.EXPECT_RETURN != null ? Convert.ToDateTime(vacation.EXPECT_RETURN) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)vacation.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_CREDIT", (object)vacation.LEAVE_CREDIT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_TICKET", vacation.IS_TICKET != null ? Convert.ToBoolean(vacation.IS_TICKET) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LAST_REJOIN_DATE", vacation.LAST_REJOIN_DATE != null ? Convert.ToDateTime(vacation.LAST_REJOIN_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRAVELLED_DATE", vacation.TRAVELLED_DATE != null ? Convert.ToDateTime(vacation.TRAVELLED_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@REJOIN_DATE", vacation.REJOIN_DATE != null ? Convert.ToDateTime(vacation.REJOIN_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ACTUAL_DAYS", (object)vacation.ACTUAL_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEDUCT_DAYS", (object)vacation.DEDUCT_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEFT_REASON", (object)vacation.LEFT_REASON ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveEmployeeVacationResponseData ApproveData(saveEmployeeVacationData vacation)
        {
            try
            {
                saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_VACATION";

                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    cmd.Parameters.AddWithValue("ID", vacation.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)vacation.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)vacation.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)vacation.EMP_ID ?? DBNull.Value);

                    // Convert DOC_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DOC_DATE", vacation.DOC_DATE != null ? Convert.ToDateTime(vacation.DOC_DATE) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_TYPE_ID", (object)vacation.LEAVE_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VAC_DAYS", (object)vacation.VAC_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LS_PAYABLE", (object)vacation.LS_PAYABLE ?? DBNull.Value);

                    // Convert DEPT_DATE to DateTime if not null
                    cmd.Parameters.AddWithValue("@DEPT_DATE", vacation.DEPT_DATE != null ? Convert.ToDateTime(vacation.DEPT_DATE) : DBNull.Value);

                    // Convert EXPECT_RETURN to DateTime if not null
                    cmd.Parameters.AddWithValue("@EXPECT_RETURN", vacation.EXPECT_RETURN != null ? Convert.ToDateTime(vacation.EXPECT_RETURN) : DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)vacation.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@LEAVE_CREDIT", (object)vacation.LEAVE_CREDIT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_TICKET", vacation.IS_TICKET != null ? Convert.ToBoolean(vacation.IS_TICKET) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LAST_REJOIN_DATE", vacation.LAST_REJOIN_DATE != null ? Convert.ToDateTime(vacation.LAST_REJOIN_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRAVELLED_DATE", vacation.TRAVELLED_DATE != null ? Convert.ToDateTime(vacation.TRAVELLED_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@REJOIN_DATE", vacation.REJOIN_DATE != null ? Convert.ToDateTime(vacation.REJOIN_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ACTUAL_DAYS", (object)vacation.ACTUAL_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEDUCT_DAYS", (object)vacation.DEDUCT_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEFT_REASON", (object)vacation.LEFT_REASON ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
