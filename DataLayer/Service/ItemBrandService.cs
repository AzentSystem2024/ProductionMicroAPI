using MicroApi.Helper;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace RetailApi.DAL.Services
{
    public class ItemBrandService : IItemBrandService
    {
        public List<ItemBrand> GetAllItemBrand(Int32 intUserID)
        {
            List<ItemBrand> departmentList = new List<ItemBrand>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEM_BRAND";
                cmd.Parameters.AddWithValue("ACTION", 0);
                //cmd.Parameters.AddWithValue("UserID", intUserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                foreach (DataRow dr in tbl.Rows)
                {
                    departmentList.Add(new ItemBrand
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                        CODE = dr["CODE"] != DBNull.Value ? Convert.ToString(dr["CODE"]) : "",
                        BRAND_NAME = dr["BRAND_NAME"] != DBNull.Value ? Convert.ToString(dr["BRAND_NAME"]) : "",

                        COMPANY_NAME = dr["COMPANY_NAME"] != DBNull.Value ? Convert.ToString(dr["COMPANY_NAME"]) : "",

                        COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToString(dr["COMPANY_ID"]) : ""
                        //IS_DELETED=dr["IS_DELETED"].ToString()
                    });
                }
                connection.Close();
            }
            return departmentList;
        }

        public Int32 SaveData(ItemBrand itembrand, Int32 userID)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_BRAND";

                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("ID", itembrand.ID);
                    cmd.Parameters.AddWithValue("CODE", itembrand.CODE);
                    cmd.Parameters.AddWithValue("BRAND_NAME", itembrand.BRAND_NAME);
                    cmd.Parameters.AddWithValue("COMPANY_ID", itembrand.COMPANY_ID);
                    //cmd.Parameters.AddWithValue("UserID", userID);

                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());



                    return UserID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ItemBrand> GetItems(int id)
        {
            List<ItemBrand> itemBrands = new List<ItemBrand>();

            try
            {
                string strSQL = "SELECT TB_ITEM_BRAND.ID,TB_ITEM_BRAND.CODE,TB_ITEM_BRAND.BRAND_NAME, " +
                "TB_ITEM_BRAND.COMPANY_ID," +
                "TB_COMPANY_MASTER.COMPANY_NAME " +
                "FROM TB_ITEM_BRAND " +
                "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_BRAND.COMPANY_ID = TB_COMPANY_MASTER.ID " +
                "WHERE TB_ITEM_BRAND.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Clinician");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    itemBrands.Add(new ItemBrand
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                        CODE = dr["CODE"] != DBNull.Value ? Convert.ToString(dr["CODE"]) : "",
                        BRAND_NAME = dr["BRAND_NAME"] != DBNull.Value ? Convert.ToString(dr["BRAND_NAME"]) : "",

                        COMPANY_NAME = dr["COMPANY_NAME"] != DBNull.Value ? Convert.ToString(dr["COMPANY_NAME"]) : "",

                        COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToString(dr["COMPANY_ID"]) : ""
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return itemBrands;
        }

        public bool DeleteItemBrand(int id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEM_BRAND";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id);
                    // cmd.Parameters.AddWithValue("UserID", userID);
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
