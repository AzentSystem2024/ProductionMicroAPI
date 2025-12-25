using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemSubCategoryService:IItemSubCategoryService
    {
        public List<ItemSubCategory> GetAllItemSubCategory(ItemSubCategoryList request)
        {
            List<ItemSubCategory> itemsubcategoryList = new List<ItemSubCategory>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_SUBCATEGORY";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    itemsubcategoryList.Add(new ItemSubCategory
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        SUBCAT_NAME = Convert.ToString(dr["SUBCAT_NAME"]),

                        CAT_ID = Convert.ToInt32(dr["CAT_ID"]),

                        CAT_NAME = Convert.ToString(dr["CAT_NAME"]),
                        DEPT_ID = Convert.ToInt32(dr["DEPT_ID"]),
                        DEPT_NAME = Convert.ToString(dr["DEPT_NAME"])

                    });
                }
                connection.Close();
            }
            return itemsubcategoryList;
        }
        public Int32 SaveData(ItemSubCategory itemSubCategory)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_SUBCATEGORY";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", itemSubCategory.ID);
                    cmd.Parameters.AddWithValue("COMPANY_ID", itemSubCategory.COMPANY_ID);
                    cmd.Parameters.AddWithValue("CODE", itemSubCategory.CODE);
                    cmd.Parameters.AddWithValue("SUBCAT_NAME", itemSubCategory.SUBCAT_NAME);
                    cmd.Parameters.AddWithValue("CAT_ID", itemSubCategory.CAT_ID);
                    //cmd.Parameters.AddWithValue("DEPT_NAME", itemSubCategory.DEPT_NAME);
                    //cmd.Parameters.AddWithValue("DEPT_ID", itemSubCategory.DEPT_ID);

                    Int32 SubCatID = Convert.ToInt32(cmd.ExecuteScalar());
                    return SubCatID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ItemSubCategory GetItems(int id)
        {
            ItemSubCategory itemSubCategory = new ItemSubCategory();

            try
            {

                string strSQL = "SELECT TB_ITEM_SUBCATEGORY.ID, TB_ITEM_SUBCATEGORY.CODE, TB_ITEM_SUBCATEGORY.SUBCAT_NAME, " +
                  " TB_ITEM_SUBCATEGORY.CAT_ID, TB_ITEM_CATEGORY.CAT_NAME,TB_ITEM_SUBCATEGORY.COMPANY_ID, " +
                  " TB_ITEM_DEPARTMENT.ID AS DEPT_ID, TB_ITEM_DEPARTMENT.DEPT_NAME " +
                  " FROM TB_ITEM_SUBCATEGORY " +
                  " INNER JOIN TB_ITEM_CATEGORY ON TB_ITEM_SUBCATEGORY.CAT_ID = TB_ITEM_CATEGORY.ID " +
                  " INNER JOIN TB_ITEM_DEPARTMENT ON TB_ITEM_CATEGORY.DEPT_ID = TB_ITEM_DEPARTMENT.ID " +
                  " WHERE TB_ITEM_SUBCATEGORY.ID = " + id;



                DataTable tbl = ADO.GetDataTable(strSQL, "ItemSubCategory");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemSubCategory.ID = Convert.ToInt32(dr["ID"]);
                    itemSubCategory.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    itemSubCategory.CODE = Convert.ToString(dr["CODE"]);
                    itemSubCategory.SUBCAT_NAME = Convert.ToString(dr["SUBCAT_NAME"]);
                    itemSubCategory.CAT_ID = Convert.ToInt32(dr["CAT_ID"]);
                    itemSubCategory.CAT_NAME = Convert.ToString(dr["CAT_NAME"]);
                    itemSubCategory.DEPT_ID = Convert.ToInt32(dr["DEPT_ID"]);
                    itemSubCategory.DEPT_NAME = Convert.ToString(dr["DEPT_NAME"]);

                }
            }
            catch (Exception ex)
            {

            }
            return itemSubCategory;
        }
        public bool DeleteItemSubCategory(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_SUBCATEGORY";
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
