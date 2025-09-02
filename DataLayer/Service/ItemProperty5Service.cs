using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemProperty5Service:IItemProperty5Service
    {
        public List<ItemProperty5> GetAllItemProperty5()
        {
            List<ItemProperty5> paymentList = new List<ItemProperty5>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_PROPERTY2";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    paymentList.Add(new ItemProperty5
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]),

                        IS_DELETED = Convert.ToString(dr["IS_DELETED"])

                    });
                }
                connection.Close();
            }
            return paymentList;
        }

        public Int32 SaveData(ItemProperty5 itemProperty2)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY2";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", itemProperty2.ID);
                    cmd.Parameters.AddWithValue("CODE", itemProperty2.CODE);
                    cmd.Parameters.AddWithValue("DESCRIPTION", itemProperty2.DESCRIPTION);
                    cmd.Parameters.AddWithValue("COMPANY_ID", itemProperty2.COMPANY_ID);

                    Int32 ItemProperty1D = Convert.ToInt32(cmd.ExecuteScalar());

                    return ItemProperty1D;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ItemProperty5 GetItems(int id)
        {
            ItemProperty5 itemProperty2 = new ItemProperty5();

            try
            {


                string strSQL = "SELECT TB_ITEM_PROPERTY2.ID,TB_ITEM_PROPERTY2.CODE,TB_ITEM_PROPERTY2.DESCRIPTION, " +

                "TB_ITEM_PROPERTY2.IS_DELETED,TB_ITEM_PROPERTY2.COMPANY_ID," +
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_ITEM_PROPERTY2 " +
               "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_PROPERTY2.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_ITEM_PROPERTY2.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty1");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemProperty2.ID = Convert.ToInt32(dr["ID"]);
                    itemProperty2.CODE = Convert.ToString(dr["CODE"]);
                    itemProperty2.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);

                    itemProperty2.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    itemProperty2.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    itemProperty2.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

            }
            return itemProperty2;
        }

        public bool DeleteItemProperty5(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY2";
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
