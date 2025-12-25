using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class StoresService:IStoresService
    {
        public List<Stores> GetAllStores(StoresList request)
        {
            List<Stores> storeList = new List<Stores>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_STORES";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    storeList.Add(new Stores
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        STORE_NAME = Convert.ToString(dr["STORE_NAME"]),
                        AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]),
                        IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]),
                        IS_PRODUCTION = Convert.ToBoolean(dr["IS_PRODUCTION"]),
                        IS_DEFAULT_STORE = Convert.ToBoolean(dr["IS_DEFAULT_STORE"]),
                        ADDRESS1 = Convert.ToString(dr["ADDRESS1"]),
                        ADDRESS2 = Convert.ToString(dr["ADDRESS2"]),
                        ADDRESS3 = Convert.ToString(dr["ADDRESS3"]),
                        CITY = Convert.ToString(dr["CITY"]),
                        STATE_ID = Convert.ToInt32(dr["STATE_ID"]),
                        COUNTRY_ID = Convert.ToInt32(dr["COUNTRY_ID"]),
                        ZIP_CODE = Convert.ToString(dr["ZIP_CODE"]),
                        PHONE = Convert.ToString(dr["PHONE"]),
                        EMAIL = Convert.ToString(dr["EMAIL"]),
                        STORE_NO = Convert.ToString(dr["STORE_NO"]),
                        VAT_REGNO = Convert.ToString(dr["VAT_REGNO"]),
                        GROUP_ID = Convert.ToInt32(dr["GROUP_ID"]),

                        COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]),
                        COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]),
                        GROUP_NAME = Convert.ToString(dr["GROUP_NAME"]),
                        STATE_NAME = Convert.ToString(dr["STATE_NAME"]),


                    });
                }
                connection.Close();
            }
            return storeList;
        }

        public Int32 SaveData(Stores stores)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_STORES";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", stores.ID);
                    cmd.Parameters.AddWithValue("CODE", stores.CODE);
                    cmd.Parameters.AddWithValue("COMPANY_ID", stores.COMPANY_ID);
                    cmd.Parameters.AddWithValue("STORE_NAME", stores.STORE_NAME);
                    cmd.Parameters.AddWithValue("AC_HEAD_ID", stores.AC_HEAD_ID);
                    cmd.Parameters.AddWithValue("IS_ACTIVE", stores.IS_ACTIVE);
                    cmd.Parameters.AddWithValue("IS_PRODUCTION", stores.IS_PRODUCTION);
                    cmd.Parameters.AddWithValue("IS_DEFAULT_STORE", stores.IS_DEFAULT_STORE);
                    cmd.Parameters.AddWithValue("ADDRESS1", stores.ADDRESS1);
                    cmd.Parameters.AddWithValue("ADDRESS2", stores.ADDRESS2);
                    cmd.Parameters.AddWithValue("ADDRESS3", stores.ADDRESS3);
                    cmd.Parameters.AddWithValue("STATE_ID", stores.STATE_ID);
                    cmd.Parameters.AddWithValue("CITY", stores.CITY);
                    cmd.Parameters.AddWithValue("COUNTRY_ID", stores.COUNTRY_ID);
                    cmd.Parameters.AddWithValue("ZIP_CODE", stores.ZIP_CODE);

                    cmd.Parameters.AddWithValue("PHONE", stores.PHONE);
                    cmd.Parameters.AddWithValue("EMAIL", stores.EMAIL);
                    cmd.Parameters.AddWithValue("STORE_NO", stores.STORE_NO);
                    cmd.Parameters.AddWithValue("VAT_REGNO", stores.VAT_REGNO);
                    cmd.Parameters.AddWithValue("GROUP_ID", stores.GROUP_ID);


                    Int32 StoreID = Convert.ToInt32(cmd.ExecuteScalar());

                    return StoreID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Stores GetItems(int id)
        {
            Stores stores = new Stores();

            try
            {
                string strSQL = "SELECT TB_STORES.ID, TB_STORES.CODE, TB_STORES.STORE_NAME, TB_STORES.AC_HEAD_ID, TB_STORES.IS_ACTIVE, " +
                 "TB_STORES.IS_PRODUCTION, TB_STORES.IS_DEFAULT_STORE, TB_STORES.ADDRESS1, TB_STORES.ADDRESS2, TB_STORES.ADDRESS3, " +
                 "TB_STORES.CITY,TB_STORES.ZIP_CODE, TB_STORES.PHONE, TB_STORES.EMAIL, TB_STORES.STORE_NO, TB_STORES.VAT_REGNO, " +
                 "TB_STORES.VAT_REGNO, TB_STORES.IS_DELETED, TB_STORES.COMPANY_ID, TB_STORES.COUNTRY_ID, TB_STORES.GROUP_ID, TB_STORES.STATE_ID, " +
                 "TB_COMPANY_MASTER.COMPANY_NAME, TB_COUNTRY.COUNTRY_NAME, TB_STORE_GROUP.GROUP_NAME, TB_STATE.STATE_NAME " +
                 "FROM TB_STORES " +  // Added space here
                 "INNER JOIN TB_COMPANY_MASTER ON TB_STORES.COMPANY_ID = TB_COMPANY_MASTER.ID " +
                 "INNER JOIN TB_COUNTRY ON TB_STORES.COUNTRY_ID = TB_COUNTRY.ID " +
                 "INNER JOIN TB_STORE_GROUP ON TB_STORES.GROUP_ID = TB_STORE_GROUP.ID " +
                 "INNER JOIN TB_STATE ON TB_STORES.STATE_ID = TB_STATE.ID " +
                 "WHERE TB_STORES.ID = " + id;  // Added space here





                DataTable tbl = ADO.GetDataTable(strSQL, "Stores");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    stores.ID = Convert.ToInt32(dr["ID"]);
                    stores.CODE = Convert.ToString(dr["CODE"]);

                    stores.STORE_NAME = Convert.ToString(dr["STORE_NAME"]);
                    stores.AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]);
                    stores.IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]);
                    stores.IS_PRODUCTION = Convert.ToBoolean(dr["IS_PRODUCTION"]);
                    stores.IS_DEFAULT_STORE = Convert.ToBoolean(dr["IS_DEFAULT_STORE"]);
                    stores.ADDRESS1 = Convert.ToString(dr["ADDRESS1"]);
                    stores.ADDRESS2 = Convert.ToString(dr["ADDRESS2"]);
                    stores.ADDRESS3 = Convert.ToString(dr["ADDRESS3"]);

                    stores.CITY = Convert.ToString(dr["CITY"]);
                    stores.ZIP_CODE = Convert.ToString(dr["ZIP_CODE"]);


                    stores.PHONE = Convert.ToString(dr["PHONE"]);
                    stores.EMAIL = Convert.ToString(dr["EMAIL"]);
                    stores.STORE_NO = Convert.ToString(dr["STORE_NO"]);
                    stores.VAT_REGNO = Convert.ToString(dr["VAT_REGNO"]);

                    stores.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);
                    stores.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    stores.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    stores.COUNTRY_ID = Convert.ToInt32(dr["COUNTRY_ID"]);
                    stores.COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]);
                    stores.GROUP_ID = Convert.ToInt32(dr["GROUP_ID"]);
                    stores.GROUP_NAME = Convert.ToString(dr["GROUP_NAME"]);
                    stores.STATE_ID = Convert.ToInt32(dr["STATE_ID"]);
                    stores.STATE_NAME = Convert.ToString(dr["STATE_NAME"]);

                }
            }
            catch (Exception ex)
            {

            }
            return stores;
        }

        public bool DeleteStores(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_STORES";
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
