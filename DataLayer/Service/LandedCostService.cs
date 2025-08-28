using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class LandedCostService:ILandedCostService
    {
        public List<LandedCost> GetAllLandedCost()
        {
            List<LandedCost> currencyList = new List<LandedCost>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_LANDED_COSTS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    currencyList.Add(new LandedCost
                    {
                        ID = Convert.ToInt32(dr["ID"]),

                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        IS_LOCAL_CURRENCY = Convert.ToBoolean(dr["IS_LOCAL_CURRENCY"]),
                        IS_FIXED_AMOUNT = Convert.ToBoolean(dr["IS_FIXED_AMOUNT"]),
                        AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]),
                        VALUE = float.Parse(dr["VALUE"].ToString()),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]),
                        IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),
                        IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"])


                    });
                }
                connection.Close();
            }
            return currencyList;
        }

        public Int32 SaveData(LandedCost landedCost)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LANDED_COSTS";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", landedCost.ID);
                    cmd.Parameters.AddWithValue("IS_LOCAL_CURRENCY", landedCost.IS_LOCAL_CURRENCY);
                    cmd.Parameters.AddWithValue("DESCRIPTION", landedCost.DESCRIPTION);
                    cmd.Parameters.AddWithValue("IS_FIXED_AMOUNT", landedCost.IS_FIXED_AMOUNT);
                    cmd.Parameters.AddWithValue("AC_HEAD_ID", landedCost.AC_HEAD_ID);
                    cmd.Parameters.AddWithValue("VALUE", landedCost.VALUE);
                    cmd.Parameters.AddWithValue("COMPANY_ID", landedCost.COMPANY_ID);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", landedCost.IS_INACTIVE);

                    Int32 CostID = Convert.ToInt32(cmd.ExecuteScalar());
                    return CostID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LandedCost GetItems(int id)
        {
            LandedCost landedCost = new LandedCost();

            try
            {
                string strSQL = "SELECT TB_LANDED_COSTS.ID,TB_LANDED_COSTS.IS_LOCAL_CURRENCY," +
                    "TB_LANDED_COSTS.DESCRIPTION, " +
              "TB_LANDED_COSTS.IS_FIXED_AMOUNT,TB_LANDED_COSTS.VALUE," +
              "TB_LANDED_COSTS.IS_INACTIVE,TB_LANDED_COSTS.COMPANY_ID," +
              "TB_LANDED_COSTS.IS_DELETED," +
             "TB_LANDED_COSTS.COMPANY_ID,TB_LANDED_COSTS.AC_HEAD_ID," +
               "TB_COMPANY.COMPANY_NAME " +
               "FROM TB_LANDED_COSTS " +
               "INNER JOIN TB_COMPANY ON TB_LANDED_COSTS.COMPANY_ID = TB_COMPANY.ID " +
               "WHERE TB_LANDED_COSTS.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "LandedCost");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    landedCost.ID = Convert.ToInt32(dr["ID"]);
                    landedCost.IS_LOCAL_CURRENCY = Convert.ToBoolean(dr["IS_LOCAL_CURRENCY"]);
                    landedCost.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);

                    landedCost.IS_FIXED_AMOUNT = Convert.ToBoolean(dr["IS_FIXED_AMOUNT"]);

                    landedCost.VALUE = float.Parse(dr["VALUE"].ToString());
                    landedCost.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);
                    landedCost.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);

                    landedCost.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    landedCost.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    landedCost.AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]);


                }
            }
            catch (Exception ex)
            {

            }
            return landedCost;
        }
        public bool DeleteLandedCost(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LANDED_COSTS";
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
