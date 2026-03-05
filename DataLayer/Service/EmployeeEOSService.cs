using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MicroApi.DAL.Services
{
    public class EmployeeEOSService : IEmployeeEOSService
    {
        public EmployeeEOSLogListResponseData GetAllEmployeeEOS()
        {
            EmployeeEOSLogListResponseData loglist = new EmployeeEOSLogListResponseData();
            loglist.data = new List<EmployeeEOSLogListData>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_EMPLOYEE_EOS";
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    loglist.data.Add(new EmployeeEOSLogListData
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        DOC_NO = Convert.IsDBNull(dr["DOC_NO"]) ? null : Convert.ToString(dr["DOC_NO"]),
                        EMP_NO = Convert.IsDBNull(dr["EMP_CODE"]) ? null : Convert.ToString(dr["EMP_CODE"]),
                        EMP_ID = Convert.IsDBNull(dr["EMP_ID"]) ? null : Convert.ToInt32(dr["EMP_ID"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        DATE = Convert.IsDBNull(dr["EOS_DATE"]) ? null : Convert.ToDateTime(dr["EOS_DATE"]).ToString("dd/MM/yyyy"),
                        REASON = Convert.IsDBNull(dr["REASON"]) ? null : Convert.ToString(dr["REASON"]),
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

        public saveEmployeeEOSResponseData SaveData(saveEmployeeEOSData eos)
        {
            try
            {
                saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS";

                    cmd.Parameters.AddWithValue("@ACTION", 1);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveEmployeeEOSData selectEmployeeEOSData(int id)
        {
            saveEmployeeEOSData eos = new saveEmployeeEOSData();

            try
            {
                string strSQL = "SELECT eos.*, reason.DESCRIPTION as REASON, emp.EMP_NAME,trans.NARRATION AS REMARKS , " +
                    " stat.STATUS_DESC AS STATUS FROM TB_EMPLOYEE_EOS eos LEFT JOIN TB_AC_TRANS_HEADER trans " +
                    " ON eos.TRANS_ID = trans.TRANS_ID " +
                    " LEFT JOIN TB_STATUS stat " +
                    " ON trans.TRANS_STATUS = stat.ID " +
                    " INNER JOIN TB_EMPLOYEE emp ON emp.ID = eos.EMP_ID " +
                    " INNER JOIN TB_EOS_REASON reason ON reason.ID = eos.REASON_ID " +
                    " WHERE eos.ID = " + id;

                DataTable tblEos = ADO.GetDataTable(strSQL, "EmployeeEOS");

                if (tblEos.Rows.Count > 0)
                {
                    DataRow dr = tblEos.Rows[0];
                    eos.ID = ADO.ToInt32(dr["ID"]);
                    eos.DOC_NO = ADO.ToInt32(dr["DOC_NO"]);
                    eos.EOS_DATE = Convert.ToDateTime(dr["EOS_DATE"]);
                    eos.EMP_ID = ADO.ToInt32(dr["EMP_ID"]);
                    eos.EMP_NAME = ADO.ToString(dr["EMP_NAME"]);
                    eos.REASON_ID = ADO.ToInt32(dr["REASON_ID"]);
                    eos.REASON_NAME = ADO.ToString(dr["REASON"]);
                    eos.DAYS = ADO.ToString(dr["DAYS"]);
                    eos.EOS_AMOUNT = ADO.ToString(dr["EOS_AMOUNT"]);
                    eos.LEAVE_AMOUNT = ADO.ToString(dr["LEAVE_AMOUNT"]);
                    eos.PENDING_SALARY = ADO.ToString(dr["PENDING_SALARY"]);
                    eos.ADD_AMOUNT = ADO.ToString(dr["ADD_AMOUNT"]);
                    eos.DED_AMOUNT = ADO.ToString(dr["DED_AMOUNT"]);
                    eos.ADD_REMARKS = ADO.ToString(dr["ADD_REMARKS"]);
                    eos.DED_REMARKS = ADO.ToString(dr["DED_REMARKS"]);
                    eos.REMARKS = ADO.ToString(dr["REMARKS"]);
                    eos.STATUS = ADO.ToString(dr["STATUS"]);
                    eos.TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]);
                    eos.PAY_TRANS_ID = ADO.ToInt32(dr["PAY_TRANS_ID"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return eos;
        }

        public saveEmployeeEOSResponseData UpdateData(saveEmployeeEOSData eos)
        {
            try
            {
                saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", eos.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", (object)eos.EOS_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", (object)eos.LEAVE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", (object)eos.PENDING_SALARY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", (object)eos.ADD_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", (object)eos.DED_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", (object)eos.ADD_REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", (object)eos.DED_REMARKS ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveEmployeeEOSResponseData DeleteEmployeeEOS(int id)
        {
            try
            {
                saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS";
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


        public saveEmployeeEOSResponseData VerifyData(saveEmployeeEOSData eos)
        {
            try
            {
                saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS";

                    cmd.Parameters.AddWithValue("@ACTION", 4);

                    cmd.Parameters.AddWithValue("ID", eos.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", (object)eos.EOS_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", (object)eos.LEAVE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", (object)eos.PENDING_SALARY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", (object)eos.ADD_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", (object)eos.DED_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", (object)eos.ADD_REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", (object)eos.DED_REMARKS ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveEmployeeEOSResponseData ApproveData(saveEmployeeEOSData eos)
        {
            try
            {
                saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS";

                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    cmd.Parameters.AddWithValue("ID", eos.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", (object)eos.EOS_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", (object)eos.LEAVE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", (object)eos.PENDING_SALARY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", (object)eos.ADD_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", (object)eos.DED_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", (object)eos.ADD_REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", (object)eos.DED_REMARKS ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EOSEmployeeData getEOSEmployeeData(EOSEmployeeInput inp)
        {
            EOSEmployeeData emp = new EOSEmployeeData();

            try
            {
                string strSQL = "SELECT DOJ , LESS_SERVICE_DAYS FROM TB_EMPLOYEE WHERE ID = " + inp.EMP_ID;

                DataTable tblEos = ADO.GetDataTable(strSQL, "EmployeeEOS");

                if (tblEos.Rows.Count > 0)
                {
                    DataRow dr = tblEos.Rows[0];
                    emp.JOIN_DATE = Convert.ToDateTime(dr["DOJ"]).ToString("dd/MM/yyyy");
                    emp.LESS_SERVICE_DAYS = ADO.ToString(dr["LESS_SERVICE_DAYS"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return emp;
        }

    }

}
