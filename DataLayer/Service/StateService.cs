using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class StateService:IStateService
    {
        public List<State> GetAllState()
        {
            List<State> stateList = new List<State>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_STATE";
                cmd.Parameters.AddWithValue("ACTION", 0);
                //cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                foreach (DataRow dr in tbl.Rows)
                {
                    stateList.Add(new State
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                       // COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        STATE_NAME = Convert.ToString(dr["STATE_NAME"]),
                        COUNTRY_NAME = dr["COUNTRY_NAME"].ToString(),

                        COUNTRY_ID = Convert.ToInt32(dr["COUNTRY_ID"]),
                        IS_DELETED = dr["IS_DELETED"].ToString(),
                        STATE_CODE = dr["CODE"].ToString(),
                    });
                }
                connection.Close();
            }
            return stateList;
        }

        public Int32 SaveData(State state)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_STATE";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", state.ID);

                    cmd.Parameters.AddWithValue("STATE_NAME", state.STATE_NAME);
                    cmd.Parameters.AddWithValue("STATE_CODE", state.STATE_CODE);
                    cmd.Parameters.AddWithValue("COUNTRY_ID", state.COUNTRY_ID);
                   // cmd.Parameters.AddWithValue("COMPANY_ID", state.COMPANY_ID);

                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());



                    return UserID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public State GetItems(int id)
        {
            State state = new State();

            try
            {
                string strSQL = "SELECT TB_STATE.ID,TB_STATE.STATE_NAME,TB_STATE.CODE, " +
               "TB_STATE.COUNTRY_ID,TB_STATE.IS_DELETED, " +
               "TB_COUNTRY.COUNTRY_NAME " +
               "FROM TB_STATE " +
               "INNER JOIN TB_COUNTRY ON TB_STATE.COUNTRY_ID = TB_COUNTRY.ID " +
               "WHERE TB_STATE.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "State");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    state.ID = Convert.ToInt32(dr["ID"]);
                    //state.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    state.STATE_NAME = Convert.ToString(dr["STATE_NAME"]);

                    state.COUNTRY_ID = Convert.ToInt32(dr["COUNTRY_ID"]);
                    state.COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]);

                    state.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);
                    state.STATE_CODE = dr["CODE"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return state;
        }

        public bool DeleteState(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_STATE";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
