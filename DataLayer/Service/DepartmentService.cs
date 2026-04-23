using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class DepartmentService:IDepartmentService
    {
        public List<Department> GetAllDepartments(Departmentlist request)
        {
            List<Department> departmentList = new List<Department>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_DEPARTMENT"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    departmentList.Add(new Department
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        CODE = Convert.IsDBNull(dr["CODE"]) ? null : Convert.ToString(dr["CODE"]),
                        DEPT_NAME = Convert.IsDBNull(dr["DEPT_NAME"]) ? null : Convert.ToString(dr["DEPT_NAME"]),
                        COMPANY_NAME = Convert.IsDBNull(dr["COMPANY_NAME"]) ? null : Convert.ToString(dr["COMPANY_NAME"]),
                        COMPANY_ID = Convert.IsDBNull(dr["COMPANY_ID"]) ? 0 : Convert.ToInt32(dr["COMPANY_ID"]),
                        IS_ACTIVE = Convert.IsDBNull(dr["IS_ACTIVE"]) ? true : Convert.ToBoolean(dr["IS_ACTIVE"]),
                        COST_BUCKET_NAME = dr["CostBucket"].ToString(),
                    });
                }

                connection.Close();
            }
            return departmentList;
        }

        public int SaveDepartment(Department department)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_TB_DEPARTMENT"
                    };

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", (object)department.ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("CODE", (object)department.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DEPT_NAME", (object)department.DEPT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("COMPANY_ID", (object)department.COMPANY_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_ACTIVE", (object)department.IS_ACTIVE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("COST_BUCKET_ID", department.COST_BUCKET_ID);

                    int departmentID = Convert.ToInt32(cmd.ExecuteScalar());
                    return departmentID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Department GetDepartmentById(int id)
        {
            Department department = new Department();

            try
            {
                string strSQL = "SELECT TB_DEPARTMENT.ID, TB_DEPARTMENT.CODE, TB_DEPARTMENT.DEPT_NAME, TB_DEPARTMENT.COMPANY_ID,TB_DEPARTMENT.IS_ACTIVE, TB_DEPARTMENT.IS_DELETED,TB_COMPANY_MASTER.COMPANY_NAME,TB_COST_BUCKETS.ID AS COST_BUCKET_ID FROM TB_DEPARTMENT " +
                    "INNER JOIN TB_COMPANY_MASTER ON TB_DEPARTMENT.COMPANY_ID=TB_COMPANY_MASTER.ID " +
                    "LEFT JOIN TB_COST_BUCKETS ON TB_DEPARTMENT.COST_BUCKET_ID=TB_COST_BUCKETS.ID " +
                    " WHERE TB_DEPARTMENT.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Department");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    department.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
                    department.CODE = dr["CODE"] != DBNull.Value ? Convert.ToString(dr["CODE"]) : "";
                    department.DEPT_NAME = dr["DEPT_NAME"] != DBNull.Value ? Convert.ToString(dr["DEPT_NAME"]) : "";

                    department.COMPANY_NAME = dr["COMPANY_NAME"] != DBNull.Value ? Convert.ToString(dr["COMPANY_NAME"]) : "";
                    department.COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COMPANY_ID"]) : 0;

                    department.IS_ACTIVE = dr["IS_ACTIVE"] != DBNull.Value && Convert.ToBoolean(dr["IS_ACTIVE"]);
                    department.IS_DELETED = dr["IS_DELETED"] != DBNull.Value && Convert.ToBoolean(dr["IS_DELETED"]);

                    department.COST_BUCKET_ID = dr["COST_BUCKET_ID"] != DBNull.Value ? Convert.ToInt32(dr["COST_BUCKET_ID"]) : 0;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return department;
        }

        public bool DeleteDepartment(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_TB_DEPARTMENT"
                    };
                    cmd.Parameters.AddWithValue("ACTION", 4);//delete
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
