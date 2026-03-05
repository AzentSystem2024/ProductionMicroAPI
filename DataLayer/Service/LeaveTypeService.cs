using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MicroApi.DataLayer.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        public LeaveTypeLogListResponseData GetAllLeaveType()
        {
            LeaveTypeLogListResponseData loglist = new LeaveTypeLogListResponseData();
            loglist.data = new List<LeaveTypeLogListData>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_LEAVE_TYPE";
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    loglist.data.Add(new LeaveTypeLogListData
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        CODE = Convert.IsDBNull(dr["CODE"]) ? null : Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.IsDBNull(dr["DESCRIPTION"]) ? null : Convert.ToString(dr["DESCRIPTION"]),
                        LEAVE_SALARY_PAYABLE = Convert.IsDBNull(dr["IS_LS_PAYABLE"]) ? null : Convert.ToBoolean(dr["IS_LS_PAYABLE"]),
                        STATUS = Convert.IsDBNull(dr["STATUS"]) ? null : Convert.ToString(dr["STATUS"]),
                    });
                }
                connection.Close();
            }
            loglist.flag = "1";
            loglist.message = "Success";

            return loglist;
        }

        public saveLeaveTypeResponseData SaveData(saveLeaveTypeData type)
        {
            try
            {
                saveLeaveTypeResponseData res = new saveLeaveTypeResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LEAVE_TYPE";

                    cmd.Parameters.AddWithValue("@ACTION", 1);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);

                    cmd.Parameters.AddWithValue("CODE", (object)type.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DESCRIPTION", (object)type.DESCRIPTION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_SALARY_PAYABLE", (object)type.LEAVE_SALARY_PAYABLE ?? DBNull.Value);
                   

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveLeaveTypeData selectLeaveTypeData(int id)
        {
            saveLeaveTypeData type = new saveLeaveTypeData();

            try
            {
                string strSQL = " SELECT * FROM TB_LEAVE_TYPES WHERE ID = " + id;

                DataTable tblData = ADO.GetDataTable(strSQL, "LeaveType");

                if (tblData.Rows.Count > 0)
                {
                    DataRow dr = tblData.Rows[0];
                    type.ID = ADO.ToInt32(dr["ID"]);
                    type.CODE = ADO.ToString(dr["CODE"]);
                    type.DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]);
                    type.LEAVE_SALARY_PAYABLE = ADO.Toboolean(dr["IS_LS_PAYABLE"]); 
                    type.IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return type;
        }

        public saveLeaveTypeResponseData UpdateData(saveLeaveTypeData type)
        {
            try
            {
                saveLeaveTypeResponseData res = new saveLeaveTypeResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LEAVE_TYPE";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", type.ID);
                    cmd.Parameters.AddWithValue("CODE", (object)type.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DESCRIPTION", (object)type.DESCRIPTION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_SALARY_PAYABLE", (object)type.LEAVE_SALARY_PAYABLE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", (object)type.IS_INACTIVE ?? 0);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveLeaveTypeResponseData DeleteLeaveType(int id)
        {
            try
            {
                saveLeaveTypeResponseData res = new saveLeaveTypeResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LEAVE_TYPE";
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
    }
}
