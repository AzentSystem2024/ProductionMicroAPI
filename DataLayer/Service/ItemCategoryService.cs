using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemCategoryService:IItemCategoryService
    {
        public List<ItemCategory> GetAllItemCategory()
        {
            List<ItemCategory> itemcategoryList = new List<ItemCategory>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_CATEGORY";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    itemcategoryList.Add(new ItemCategory
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        CAT_NAME = Convert.ToString(dr["CAT_NAME"]),
                        LOYALTY_POINT = Convert.ToInt32(dr["LOYALTY_POINT"]),
                        COST_HEAD_ID = Convert.ToInt32(dr["COST_HEAD_ID"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]),
                        DEPT_ID = Convert.ToInt32(dr["DEPT_ID"]),
                        DEPT_NAME = Convert.ToString(dr["DEPT_NAME"])
                    });
                }
                connection.Close();
            }
            return itemcategoryList;
        }
        public Int32 SaveData(ItemCategory itemCategory)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_CATEGORY";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", itemCategory.ID);
                    cmd.Parameters.AddWithValue("CODE", itemCategory.CODE);
                    cmd.Parameters.AddWithValue("CAT_NAME", itemCategory.CAT_NAME);
                    cmd.Parameters.AddWithValue("LOYALTY_POINT", itemCategory.LOYALTY_POINT);
                    cmd.Parameters.AddWithValue("COST_HEAD_ID", itemCategory.COST_HEAD_ID);
                    cmd.Parameters.AddWithValue("COMPANY_ID", itemCategory.COMPANY_ID);
                    cmd.Parameters.AddWithValue("DEPT_ID", itemCategory.DEPT_ID);

                    Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                    return CountryID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ItemCategory GetItems(int id)
        {
            ItemCategory itemCategory = new ItemCategory();

            try
            {


                string strSQL = "SELECT TB_ITEM_CATEGORY.ID, TB_ITEM_CATEGORY.CODE, TB_ITEM_CATEGORY.CAT_NAME, " +
                  "TB_ITEM_CATEGORY.LOYALTY_POINT, TB_ITEM_CATEGORY.COST_HEAD_ID,TB_ITEM_CATEGORY.COMPANY_ID, TB_ITEM_CATEGORY.DEPT_ID, " +
                  "TB_COMPANY_MASTER.COMPANY_NAME, TB_DEPARTMENT.DEPT_NAME,TB_ITEM_CATEGORY.IS_DELETED " +
                  "FROM TB_ITEM_CATEGORY " +
                  "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_CATEGORY.COMPANY_ID = TB_COMPANY_MASTER.ID " +
                  "INNER JOIN TB_DEPARTMENT ON TB_ITEM_CATEGORY.DEPT_ID = TB_DEPARTMENT.ID " +
                  "WHERE TB_ITEM_CATEGORY.ID =" + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "c");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemCategory.ID = Convert.ToInt32(dr["ID"]);
                    itemCategory.CODE = Convert.ToString(dr["CODE"]);
                    itemCategory.CAT_NAME = Convert.ToString(dr["CAT_NAME"]);
                    itemCategory.LOYALTY_POINT = Convert.ToInt32(dr["LOYALTY_POINT"]);
                    itemCategory.COST_HEAD_ID = Convert.ToInt32(dr["COST_HEAD_ID"]);
                    itemCategory.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    itemCategory.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    itemCategory.DEPT_ID = Convert.ToInt32(dr["DEPT_ID"]);
                    itemCategory.DEPT_NAME = Convert.ToString(dr["DEPT_NAME"]);
                    itemCategory.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);


                }
            }
            catch (Exception ex)
            {

            }
            return itemCategory;
        }
        public bool DeleteItemCategory(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_CATEGORY";
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
