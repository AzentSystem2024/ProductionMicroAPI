using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemDepartmentService:IItemDepartmentService
    {
        public List<ItemDepartment> GetAllDepartment(ItemDepartmentListReq request)
        {
            List<ItemDepartment> departmentList = new List<ItemDepartment>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ITEM_DEPARTMENT";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                foreach (DataRow dr in tbl.Rows)
                {
                    departmentList.Add(new ItemDepartment
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DEPT_NAME = Convert.ToString(dr["DEPT_NAME"]),
                        COMPANY_NAME = dr["COMPANY_NAME"].ToString(),
                        COMPANY_ID = dr["COMPANY_ID"].ToString(),
                        //IS_DELETED=dr["IS_DELETED"].ToString()
                    });
                }
                connection.Close();
            }
            return departmentList;
        }

        public Int32 SaveData(ItemDepartment department)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_ITEM_DEPARTMENT";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", department.ID);
                    cmd.Parameters.AddWithValue("CODE", department.CODE);
                    cmd.Parameters.AddWithValue("DEPT_NAME", department.DEPT_NAME);
                    cmd.Parameters.AddWithValue("COMPANY_ID", department.COMPANY_ID);

                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());



                    return UserID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemDepartment GetItems(int id)
        {
            ItemDepartment department = new ItemDepartment();

            try
            {
                string strSQL = "SELECT TB_ITEM_DEPARTMENT.ID,TB_ITEM_DEPARTMENT.CODE,TB_ITEM_DEPARTMENT.DEPT_NAME, " +
               "TB_ITEM_DEPARTMENT.COMPANY_ID," +
               "TB_COMPANY_MASTER.COMPANY_NAME " +
               "FROM TB_ITEM_DEPARTMENT " +
               "INNER JOIN TB_COMPANY_MASTER ON TB_ITEM_DEPARTMENT.COMPANY_ID = TB_COMPANY_MASTER.ID " +
               "WHERE TB_ITEM_DEPARTMENT.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Department");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    department.ID = Convert.ToInt32(dr["ID"]);
                    department.CODE = Convert.ToString(dr["CODE"]);
                    department.DEPT_NAME = Convert.ToString(dr["DEPT_NAME"]);
                    department.COMPANY_ID = Convert.ToString(dr["COMPANY_ID"]);
                    department.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                }
            }
            catch (Exception ex)
            {

            }
            return department;
        }

        public bool DeleteDepartment(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_ITEM_DEPARTMENT";
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
