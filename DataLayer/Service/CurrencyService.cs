using MicroApi.Helper;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace RetailApi.DAL.Services
{
    public class CurrencyService : ICurrencyService
    {
        public List<Currency> GetAllCurrency()
        {
            List<Currency> currencyList = new List<Currency>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_CURRENCY";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    currencyList.Add(new Currency
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        SYMBOL = Convert.ToString(dr["SYMBOL"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        EXCHANGE = Convert.ToString(dr["EXCHANGE"]),

                        FRACTION_UNIT = Convert.ToString(dr["FRACTION_UNIT"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]),
                        IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])

                    });
                }
                connection.Close();
            }
            return currencyList;
        }
        public Int32 SaveData(Currency currency)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CURRENCY";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", currency.ID);
                    cmd.Parameters.AddWithValue("CODE", currency.CODE);
                    cmd.Parameters.AddWithValue("SYMBOL", currency.SYMBOL);
                    cmd.Parameters.AddWithValue("DESCRIPTION", currency.DESCRIPTION);
                    cmd.Parameters.AddWithValue("FRACTION_UNIT", currency.FRACTION_UNIT);
                    cmd.Parameters.AddWithValue("EXCHANGE", currency.EXCHANGE);
                    cmd.Parameters.AddWithValue("COMPANY_ID", currency.COMPANY_ID);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", currency.IS_INACTIVE);


                    Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                    return CountryID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Currency GetItems(int id)
        {
            Currency currency = new Currency();

            try
            {


                string strSQL = "SELECT TB_CURRENCY.ID,TB_CURRENCY.CODE, TB_CURRENCY.SYMBOL, TB_CURRENCY.DESCRIPTION, " +
              "TB_CURRENCY.FRACTION_UNIT,TB_CURRENCY.EXCHANGE,TB_CURRENCY.IS_INACTIVE,TB_CURRENCY.IS_DELETED,TB_CURRENCY.COMPANY_ID," +
                "TB_CURRENCY.COMPANY_ID," +
               "TB_COMPANY.COMPANY_NAME " +
               "FROM TB_CURRENCY " +
               "INNER JOIN TB_COMPANY ON TB_CURRENCY.COMPANY_ID = TB_COMPANY.ID " +
               "WHERE TB_CURRENCY.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Currency");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    currency.ID = Convert.ToInt32(dr["ID"]);
                    currency.CODE = Convert.ToString(dr["CODE"]);
                    currency.SYMBOL = Convert.ToString(dr["SYMBOL"]);
                    currency.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                    currency.FRACTION_UNIT = Convert.ToString(dr["FRACTION_UNIT"]);
                    currency.EXCHANGE = Convert.ToString(dr["EXCHANGE"]);
                    currency.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);
                    currency.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    currency.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    currency.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

            }
            return currency;
        }
        public bool DeleteCurrency(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CURRENCY";
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
