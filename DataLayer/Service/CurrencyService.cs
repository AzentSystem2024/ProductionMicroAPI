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
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    currencyList.Add(new Currency
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,

                        CODE = dr["CODE"] != DBNull.Value
                            ? Convert.ToString(dr["CODE"])
                            : null,

                        SYMBOL = dr["SYMBOL"] != DBNull.Value
                            ? Convert.ToString(dr["SYMBOL"])
                            : null,

                        DESCRIPTION = dr["DESCRIPTION"] != DBNull.Value
                            ? Convert.ToString(dr["DESCRIPTION"])
                            : null,

                        EXCHANGE = dr["EXCHANGE"] != DBNull.Value
                            ? Convert.ToString(dr["EXCHANGE"])
                            : null,

                        FRACTION_UNIT = dr["FRACTION_UNIT"] != DBNull.Value
                            ? Convert.ToString(dr["FRACTION_UNIT"])
                            : null,

                        COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value
                            ? Convert.ToInt32(dr["COMPANY_ID"])
                            : 0,

                        COMPANY_NAME = dr["COMPANY_NAME"] != DBNull.Value
                            ? Convert.ToString(dr["COMPANY_NAME"])
                            : null,

                        IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value
                            ? Convert.ToBoolean(dr["IS_INACTIVE"])
                            : false
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
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_CURRENCY " +
               "LEFT JOIN TB_COMPANY_MASTER ON TB_CURRENCY.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_CURRENCY.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Currency");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    currency.ID = dr["ID"] != DBNull.Value
     ? Convert.ToInt32(dr["ID"])
     : 0;

                    currency.CODE = dr["CODE"] != DBNull.Value
                        ? Convert.ToString(dr["CODE"])
                        : null;

                    currency.SYMBOL = dr["SYMBOL"] != DBNull.Value
                        ? Convert.ToString(dr["SYMBOL"])
                        : null;

                    currency.DESCRIPTION = dr["DESCRIPTION"] != DBNull.Value
                        ? Convert.ToString(dr["DESCRIPTION"])
                        : null;

                    currency.FRACTION_UNIT = dr["FRACTION_UNIT"] != DBNull.Value
                        ? Convert.ToString(dr["FRACTION_UNIT"])
                        : null;

                    currency.EXCHANGE = dr["EXCHANGE"] != DBNull.Value
                        ? Convert.ToString(dr["EXCHANGE"])
                        : null;

                    currency.IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value
                        ? Convert.ToBoolean(dr["IS_INACTIVE"])
                        : false;

                    currency.COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value
                        ? Convert.ToInt32(dr["COMPANY_ID"])
                        : 0;

                    currency.COMPANY_NAME = dr["COMPANY_NAME"] != DBNull.Value
                        ? Convert.ToString(dr["COMPANY_NAME"])
                        : null;

                    currency.IS_DELETED = dr["IS_DELETED"] != DBNull.Value
                        ? Convert.ToString(dr["IS_DELETED"])
                        : null;

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
