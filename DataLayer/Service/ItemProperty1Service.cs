using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemProperty1Service:IItemProperty1Service
    {
        public List<ItemProperty1> GetAllItemProperty1()
        {
            List<ItemProperty1> paymentList = new List<ItemProperty1>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_PROPERTY1";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    paymentList.Add(new ItemProperty1
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]),
                        IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"])
                        //IS_DELETED = Convert.ToString(dr["IS_DELETED"])

                    });
                }
                connection.Close();
            }
            return paymentList;
        }

        public Int32 SaveData(ItemProperty1 itemProperty1)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY1";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", itemProperty1.ID);
                    cmd.Parameters.AddWithValue("CODE", itemProperty1.CODE);
                    cmd.Parameters.AddWithValue("DESCRIPTION", itemProperty1.DESCRIPTION);
                    cmd.Parameters.AddWithValue("COMPANY_ID", itemProperty1.COMPANY_ID);

                    Int32 ItemProperty1D = Convert.ToInt32(cmd.ExecuteScalar());

                    return ItemProperty1D;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemProperty1 GetItems(int id)
        {
            ItemProperty1 itemProperty1 = new ItemProperty1();

            try
            {


                string strSQL = "SELECT TB_ITEM_PROPERTY1.ID,TB_ITEM_PROPERTY1.CODE,TB_ITEM_PROPERTY1.DESCRIPTION, " +

                "TB_ITEM_PROPERTY1.IS_DELETED,TB_ITEM_PROPERTY1.COMPANY_ID," +
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_ITEM_PROPERTY1 " +
               "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_PROPERTY1.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_ITEM_PROPERTY1.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty1");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemProperty1.ID = Convert.ToInt32(dr["ID"]);
                    itemProperty1.CODE = Convert.ToString(dr["CODE"]);
                    itemProperty1.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);

                    itemProperty1.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    itemProperty1.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    itemProperty1.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

            }
            return itemProperty1;
        }

        public bool DeleteItemProperty1(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_PROPERTY1";
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
