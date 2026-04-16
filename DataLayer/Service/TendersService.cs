using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class TendersService:ITendersService
    {
        public List<Tenders> GetAllTender()
        {
            List<Tenders> tendersList = new List<Tenders>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TENDERS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    tendersList.Add(new Tenders
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        TENDER_TYPE = Convert.ToInt32(dr["TENDER_TYPE"]),
                        CURRENCY_ID = Convert.ToInt32(dr["CURRENCY_ID"]),
                        REGISTER_ID = Convert.ToInt32(dr["REGISTER_ID"]),
                        DISPLAY_ORDER = Convert.ToInt32(dr["DISPLAY_ORDER"]),
                        //ROUND_VALUE = Convert.ToBoolean(dr["ROUND_VALUE"]),
                        //ALLOW_MULTIPLE = Convert.ToBoolean(dr["ALLOW_MULTIPLE"]),
                        //ALLOW_OPENING = Convert.ToBoolean(dr["ALLOW_OPENING"]),
                        //ALLOW_DECLARATION = Convert.ToBoolean(dr["ALLOW_DECLARATION"]),
                        //AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]),
                        //ENTER_CARD_INFO = Convert.ToBoolean(dr["ENTER_CARD_INFO"]),
                        //PRINT_CUSTMER_COPY = Convert.ToBoolean(dr["PRINT_CUSTMER_COPY"]),
                        //CAPTURE_CARD_INFO = Convert.ToBoolean(dr["CAPTURE_CARD_INFO"]),
                        //ADDITIONAL_INFO_REQUIRED = Convert.ToBoolean(dr["ADDITIONAL_INFO_REQUIRED"]),
                        IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),
                        ARABIC_DESCRIPTION = Convert.ToString(dr["ARABIC_DESCRIPTION"]),

                        IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]),
                        TENDERTYPE_DESCRIPTION = Convert.ToString(dr["TENDERTYPE_DESCRIPTION"]),
                        CURRENCY_DESCRIPTION = Convert.ToString(dr["CURRENCY_DESCRIPTION"]),
                        STATUS = Convert.ToString(dr["Status"]),


                    });
                }
                connection.Close();
            }
            return tendersList;
        }

        public Int32 SaveData(Tenders tenders)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TENDERS";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", tenders.ID);
                    cmd.Parameters.AddWithValue("CODE", tenders.CODE);
                    cmd.Parameters.AddWithValue("DESCRIPTION", tenders.DESCRIPTION);
                    cmd.Parameters.AddWithValue("TENDER_TYPE", tenders.TENDER_TYPE);
                    cmd.Parameters.AddWithValue("CURRENCY_ID", tenders.CURRENCY_ID);
                    cmd.Parameters.AddWithValue("REGISTER_ID", tenders.REGISTER_ID);
                    cmd.Parameters.AddWithValue("DISPLAY_ORDER", tenders.DISPLAY_ORDER);
                    cmd.Parameters.AddWithValue("ROUND_VALUE", tenders.ROUND_VALUE);
                    cmd.Parameters.AddWithValue("ALLOW_MULTIPLE", tenders.ALLOW_MULTIPLE);
                    cmd.Parameters.AddWithValue("ALLOW_OPENING", tenders.ALLOW_OPENING);
                    cmd.Parameters.AddWithValue("ALLOW_DECLARATION", tenders.ALLOW_DECLARATION);
                    cmd.Parameters.AddWithValue("AC_HEAD_ID", tenders.AC_HEAD_ID);
                    cmd.Parameters.AddWithValue("ENTER_CARD_INFO", tenders.ENTER_CARD_INFO);
                    cmd.Parameters.AddWithValue("PRINT_CUSTMER_COPY", tenders.PRINT_CUSTMER_COPY);
                    cmd.Parameters.AddWithValue("CAPTURE_CARD_INFO", tenders.CAPTURE_CARD_INFO);
                    cmd.Parameters.AddWithValue("ADDITIONAL_INFO_REQUIRED", tenders.ADDITIONAL_INFO_REQUIRED);
                    cmd.Parameters.AddWithValue("ARABIC_DESCRIPTION", tenders.ARABIC_DESCRIPTION);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", tenders.IS_INACTIVE);

                    Int32 TenderID = Convert.ToInt32(cmd.ExecuteScalar());

                    return TenderID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tenders GetItems(int id)
        {
            Tenders tenders = new Tenders();

            try
            {


                string strSQL = "SELECT TB_TENDERS.ID,TB_TENDERS.CODE,TB_TENDERS.DESCRIPTION, " +
              "TB_TENDERS.TENDER_TYPE,TB_TENDERS.CURRENCY_ID,TB_TENDERS.REGISTER_ID,TB_TENDERS.DISPLAY_ORDER," +
              "TB_TENDERS.ROUND_VALUE,TB_TENDERS.ALLOW_MULTIPLE, TB_TENDERS.ALLOW_OPENING, " +
              "TB_TENDERS.ALLOW_DECLARATION," +
              "TB_TENDERS.AC_HEAD_ID, TB_TENDERS.ENTER_CARD_INFO, TB_TENDERS.PRINT_CUSTMER_COPY, " +
              "TB_TENDERS.CAPTURE_CARD_INFO, TB_TENDERS.ADDITIONAL_INFO_REQUIRED, TB_TENDERS.ARABIC_DESCRIPTION," +
              " TB_TENDERS.IS_INACTIVE," +

               " TB_TENDER_TYPES.DESCRIPTION as TENDERTYPE_DESCRIPTION," +
               " TB_CURRENCY.DESCRIPTION AS CURRENCY_DESCRIPTION " +

               "FROM TB_TENDERS " +

               "INNER JOIN TB_TENDER_TYPES ON TB_TENDERS.TENDER_TYPE = TB_TENDER_TYPES.ID " +
                "INNER JOIN TB_CURRENCY ON TB_TENDERS.CURRENCY_ID = TB_CURRENCY.ID " +

               "WHERE TB_TENDERS.ID =" + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "Tenders");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    tenders.ID = Convert.ToInt32(dr["ID"]);
                    tenders.CODE = Convert.ToString(dr["CODE"]);
                    tenders.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                    tenders.TENDER_TYPE = Convert.ToInt32(dr["TENDER_TYPE"]);
                    tenders.CURRENCY_ID = Convert.ToInt32(dr["CURRENCY_ID"]);
                    tenders.REGISTER_ID = Convert.ToInt32(dr["REGISTER_ID"]);
                    tenders.DISPLAY_ORDER = Convert.ToInt32(dr["DISPLAY_ORDER"]);
                    //tenders.ROUND_VALUE = Convert.ToBoolean(dr["ROUND_VALUE"]);
                    //tenders.ALLOW_MULTIPLE = Convert.ToBoolean(dr["ALLOW_MULTIPLE"]);
                    tenders.ALLOW_OPENING = Convert.ToBoolean(dr["ALLOW_OPENING"]);
                    tenders.ALLOW_DECLARATION = Convert.ToBoolean(dr["ALLOW_DECLARATION"]);
                    //tenders.AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]);
                    //tenders.ENTER_CARD_INFO = Convert.ToBoolean(dr["ENTER_CARD_INFO"]);
                    //tenders.PRINT_CUSTMER_COPY = Convert.ToBoolean(dr["PRINT_CUSTMER_COPY"]);
                    //tenders.CAPTURE_CARD_INFO = Convert.ToBoolean(dr["CAPTURE_CARD_INFO"]);
                    tenders.ADDITIONAL_INFO_REQUIRED = Convert.ToBoolean(dr["ADDITIONAL_INFO_REQUIRED"]);
                    tenders.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);
                    tenders.ARABIC_DESCRIPTION = Convert.ToString(dr["ARABIC_DESCRIPTION"]);

                    //tenders.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);
                    tenders.TENDERTYPE_DESCRIPTION = Convert.ToString(dr["TENDERTYPE_DESCRIPTION"]);
                    tenders.CURRENCY_DESCRIPTION = Convert.ToString(dr["CURRENCY_DESCRIPTION"]);

                }
            }
            catch (Exception ex)
            {

            }
            return tenders;
        }

        public bool DeleteTenders(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TENDERS";
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
