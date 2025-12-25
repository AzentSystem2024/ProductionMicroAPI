using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemProperty2Service:IItemProperty2Service
    {
        public List<ItemProperty2> GetAllItemProperty2(ItemPropertyList request)
        {
            List<ItemProperty2> paymentList = new List<ItemProperty2>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_PROPERTY2";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    paymentList.Add(new ItemProperty2
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

        public Int32 SaveData(ItemProperty2 itemProperty2)
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


        public ItemProperty2 GetItems(int id)
        {
            ItemProperty2 itemProperty2 = new ItemProperty2();

            try
            {


                string strSQL = "SELECT TB_ITEM_PROPERTY2.ID,TB_ITEM_PROPERTY2.CODE,TB_ITEM_PROPERTY2.DESCRIPTION, " +

                "TB_ITEM_PROPERTY2.IS_DELETED,TB_ITEM_PROPERTY2.COMPANY_ID," +
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_ITEM_PROPERTY2 " +
               "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_PROPERTY2.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_ITEM_PROPERTY2.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty2");
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

        public bool DeleteItemProperty2(int id)
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
