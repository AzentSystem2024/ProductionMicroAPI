using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemProperty3Service:IItemProperty3Service
    {
        public List<ItemProperty3> GetAllItemProperty3(ItemPropertyList request)
        {
            List<ItemProperty3> paymentList = new List<ItemProperty3>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_PROPERTY3";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    paymentList.Add(new ItemProperty3
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

        public Int32 SaveData(ItemProperty3 itemProperty2)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY3";

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


        public ItemProperty3 GetItems(int id)
        {
            ItemProperty3 itemProperty3 = new ItemProperty3();

            try
            {


                string strSQL = "SELECT TB_ITEM_PROPERTY3.ID,TB_ITEM_PROPERTY3.CODE,TB_ITEM_PROPERTY3.DESCRIPTION, " +

                "TB_ITEM_PROPERTY3.IS_DELETED,TB_ITEM_PROPERTY3.COMPANY_ID," +
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_ITEM_PROPERTY3 " +
               "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_PROPERTY3.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_ITEM_PROPERTY3.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty3");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemProperty3.ID = Convert.ToInt32(dr["ID"]);
                    itemProperty3.CODE = Convert.ToString(dr["CODE"]);
                    itemProperty3.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);

                    itemProperty3.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    itemProperty3.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    itemProperty3.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

            }
            return itemProperty3;
        }

        public bool DeleteItemProperty3(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY3";
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
