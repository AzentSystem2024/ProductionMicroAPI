using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemProperty5Service:IItemProperty5Service
    {
        public List<ItemProperty5> GetAllItemProperty5(ItemPropertyList request)
        {
            List<ItemProperty5> paymentList = new List<ItemProperty5>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_PROPERTY5";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
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
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY5";

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
            ItemProperty5 itemProperty5 = new ItemProperty5();

            try
            {


                string strSQL = "SELECT TB_ITEM_PROPERTY5.ID,TB_ITEM_PROPERTY5.CODE,TB_ITEM_PROPERTY5.DESCRIPTION, " +

                "TB_ITEM_PROPERTY5.IS_DELETED,TB_ITEM_PROPERTY5.COMPANY_ID," +
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_ITEM_PROPERTY5 " +
               "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_PROPERTY5.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_ITEM_PROPERTY5.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty5");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemProperty5.ID = Convert.ToInt32(dr["ID"]);
                    itemProperty5.CODE = Convert.ToString(dr["CODE"]);
                    itemProperty5.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);

                    itemProperty5.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    itemProperty5.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    itemProperty5.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

            }
            return itemProperty5;
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
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY5";
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
