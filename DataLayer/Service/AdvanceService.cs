using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.Json;

namespace MicroApi.DataLayer.Services
{
    public class AdvanceService : IAdvanceService
    {
        public AdvanceLogListResponseData GetAllPayAdvance()
        {
            AdvanceLogListResponseData loglist = new AdvanceLogListResponseData();
            loglist.data = new List<AdvanceLogListData>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PAY_ADVANCE";
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    loglist.data.Add(new AdvanceLogListData
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        ADV_NO = Convert.IsDBNull(dr["ADV_NO"]) ? null : Convert.ToString(dr["ADV_NO"]),
                        EMP_NO = Convert.IsDBNull(dr["EMP_CODE"]) ? null : Convert.ToString(dr["EMP_CODE"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        DATE = Convert.IsDBNull(dr["ADV_DATE"]) ? null : Convert.ToDateTime(dr["ADV_DATE"]).ToString("dd/MM/yy"),
                        ADV_TYPES = Convert.IsDBNull(dr["HEAD_NAME"]) ? null : Convert.ToString(dr["HEAD_NAME"]),
                        AMOUNT = Convert.IsDBNull(dr["ADVANCE_AMOUNT"]) ? null : Convert.ToDecimal(dr["ADVANCE_AMOUNT"]),
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

        public saveAdvanceResponseData SaveData(saveAdvanceData adv)
        {
            try
            {
                saveAdvanceResponseData res = new saveAdvanceResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAY_ADVANCE";

                    cmd.Parameters.AddWithValue("@ACTION", 1);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);

                    cmd.Parameters.AddWithValue("@EMP_ID", adv.EMP_ID);
                    cmd.Parameters.AddWithValue("@DATE", (object)adv.DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADV_TYPE_ID", (object)adv.ADV_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADVANCE_AMOUNT", (object)adv.ADVANCE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REC_AMOUNT", (object)adv.REC_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REC_START_MONTH", (object)adv.REC_START_MONTH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REC_INSTALL_COUNT", (object)adv.REC_INSTALL_COUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REC_INSTALL_AMOUNT", (object)adv.REC_INSTALL_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REMARKS", (object)adv.REMARKS ?? DBNull.Value);


                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveAdvanceData selectPayAdvanceData(int id)
        {
            saveAdvanceData rev = new saveAdvanceData();
            try
            {
                string strSQL = @"
            SELECT 
                adv.*, 
                salheader.HEAD_NAME, 
                emp.EMP_NAME, 
                trans.NARRATION AS REMARKS, 
                stat.STATUS_DESC AS STATUS
            FROM TB_PAY_ADVANCE adv
            LEFT JOIN TB_AC_TRANS_HEADER trans ON adv.TRANS_ID = trans.TRANS_ID
            LEFT JOIN TB_STATUS stat ON trans.TRANS_STATUS = stat.ID
            INNER JOIN TB_EMPLOYEE emp ON emp.ID = adv.EMP_ID
            INNER JOIN TB_SALARY_HEAD salheader ON salheader.ID = adv.ADV_HEAD_ID
            WHERE adv.ID = " + id;

                DataTable tblHeader = ADO.GetDataTable(strSQL, "PayAdvance");

                if (tblHeader.Rows.Count > 0)
                {
                    DataRow dr = tblHeader.Rows[0];
                    rev.ID = ADO.ToInt32(dr["ID"]);
                    rev.ADV_NO = Convert.ToString(dr["ADV_NO"]);
                    rev.DATE = dr["ADV_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["ADV_DATE"]) : null;
                    rev.EMP_ID = ADO.ToInt32(dr["EMP_ID"]);
                    rev.EMP_NAME = ADO.ToString(dr["EMP_NAME"]);
                    rev.ADV_TYPE_ID = ADO.ToInt32(dr["ADV_HEAD_ID"]);
                    rev.ADV_TYPE_NAME = ADO.ToString(dr["HEAD_NAME"]);
                    rev.ADVANCE_AMOUNT = ADO.ToFloat(dr["ADVANCE_AMOUNT"]);
                    rev.REC_AMOUNT = ADO.ToFloat(dr["REC_AMOUNT"]);
                    rev.REC_START_MONTH = dr["REC_START_MONTH"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["REC_START_MONTH"]) : null;
                    rev.REC_INSTALL_COUNT = ADO.ToInt32(dr["REC_INSTALL_COUNT"]);
                    rev.REC_INSTALL_AMOUNT = ADO.ToFloat(dr["REC_INSTALL_AMOUNT"]);
                    rev.RECOVERED_AMOUNT = ADO.ToDecimal(dr["RECOVERED_AMOUNT"]);
                    rev.REMARKS = ADO.ToString(dr["REMARKS"]);
                    rev.PAY_TRANS_ID = ADO.ToInt32(dr["PAY_TRANS_ID"]);
                    rev.TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]);
                    rev.STATUS = ADO.ToString(dr["STATUS"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching PayAdvance data: " + ex.Message);
            }
            return rev;
        }


        public saveAdvanceResponseData UpdateData(saveAdvanceData adv)
        {
            try
            {
                saveAdvanceResponseData res = new saveAdvanceResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAY_ADVANCE";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", adv.ID);
                    cmd.Parameters.AddWithValue("EMP_ID", (object)adv.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DATE", (object)adv.DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("ADV_TYPE_ID", (object)adv.ADV_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("ADVANCE_AMOUNT", (object)adv.ADVANCE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_AMOUNT", (object)adv.REC_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_START_MONTH", (object)adv.REC_START_MONTH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_INSTALL_COUNT", (object)adv.REC_INSTALL_COUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_INSTALL_AMOUNT", (object)adv.REC_INSTALL_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)adv.REMARKS ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveAdvanceResponseData DeletePayAdvance(int id)
        {
            try
            {
                saveAdvanceResponseData res = new saveAdvanceResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAY_ADVANCE";
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


        public saveAdvanceResponseData VerifyData(saveAdvanceData adv)
        {
            try
            {
                saveAdvanceResponseData res = new saveAdvanceResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAY_ADVANCE";

                    cmd.Parameters.AddWithValue("@ACTION", 4);

                    cmd.Parameters.AddWithValue("ID", adv.ID);
                    cmd.Parameters.AddWithValue("EMP_ID", (object)adv.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DATE", (object)adv.DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("ADV_TYPE_ID", (object)adv.ADV_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("ADVANCE_AMOUNT", (object)adv.ADVANCE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_AMOUNT", (object)adv.REC_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_START_MONTH", (object)adv.REC_START_MONTH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_INSTALL_COUNT", (object)adv.REC_INSTALL_COUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_INSTALL_AMOUNT", (object)adv.REC_INSTALL_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)adv.REMARKS ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveAdvanceResponseData ApproveData(saveAdvanceData adv)
        {
            try
            {
                saveAdvanceResponseData res = new saveAdvanceResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAY_ADVANCE";

                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    cmd.Parameters.AddWithValue("ID", adv.ID);
                    cmd.Parameters.AddWithValue("EMP_ID", (object)adv.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DATE", (object)adv.DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("ADV_TYPE_ID", (object)adv.ADV_TYPE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("ADVANCE_AMOUNT", (object)adv.ADVANCE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_AMOUNT", (object)adv.REC_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_START_MONTH", (object)adv.REC_START_MONTH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_INSTALL_COUNT", (object)adv.REC_INSTALL_COUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REC_INSTALL_AMOUNT", (object)adv.REC_INSTALL_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)adv.REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PAY_HEAD_ID", adv.PAY_HEAD_ID);

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
