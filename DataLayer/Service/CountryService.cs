using MicroApi.Helper;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Runtime;
using MicroApi.Models;
using System.Data.SqlClient;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Services
{
    public class CountryService:ICountryService
    {
        public List<Country> GetAllCountry()
        {
            List<Country> countryList = new List<Country>();


            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_COUNTRY";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    countryList.Add(new Country
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]),
                        FLAG_URL = dr["FLAG_URL"] == DBNull.Value ? "N/A" : ADO.ToString(dr["FLAG_URL"]),
                        IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),
                        COMPANY_ID = dr["COMPANY_ID"] == DBNull.Value ? 0 : ADO.ToInt32(dr["COMPANY_ID"])

                    });
                }
                connection.Close();
            }
            return countryList;
        }
        public Int32 SaveData(Country country)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_COUNTRY";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", country.ID);
                    cmd.Parameters.AddWithValue("COMPANY_ID", country.COMPANY_ID);
                    cmd.Parameters.AddWithValue("CODE", country.CODE);
                    cmd.Parameters.AddWithValue("COUNTRY_NAME", country.COUNTRY_NAME);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", country.IS_INACTIVE);
                    cmd.Parameters.AddWithValue("FLAG_URL", country.FLAG_URL);

                    Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                    return CountryID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Country GetItems(int id)
        {
            Country country = new Country();

            try
            {


                string strSQL = "SELECT ID,CODE,COUNTRY_NAME,FLAG_URL,IS_INACTIVE,COMPANY_ID FROM TB_COUNTRY WHERE TB_COUNTRY.ID =" + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "Country");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    country.ID = Convert.ToInt32(dr["ID"]);
                    country.CODE = Convert.ToString(dr["CODE"]);
                    country.COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]);
                    country.FLAG_URL = dr["FLAG_URL"] == DBNull.Value ? "N/A" : ADO.ToString(dr["FLAG_URL"]);
                    country.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);
                    country.COMPANY_ID = dr["COMPANY_ID"] == DBNull.Value ? 0 : ADO.ToInt32(dr["COMPANY_ID"]);

                }
            }
            catch (Exception ex)
            {

            }
            return country;
        }
        public bool DeleteCountry(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_COUNTRY";
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
