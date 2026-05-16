using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class DepartmentGroupService:IDepartmentGroupService
    {
        public List<DepartmentGroup> GetAllDepartmentGroups(DepartmentGroupList request)
        {
            List<DepartmentGroup> departmentList = new List<DepartmentGroup>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_DEPARTMENT_GROUP"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    departmentList.Add(new DepartmentGroup
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        CODE = Convert.IsDBNull(dr["CODE"]) ? null : Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.IsDBNull(dr["DESCRIPTION"]) ? null : Convert.ToString(dr["DESCRIPTION"]),
                        COMPANY_ID = Convert.IsDBNull(dr["COMPANY_ID"]) ? 0 : Convert.ToInt32(dr["COMPANY_ID"]),
                        IS_INACTIVE = Convert.IsDBNull(dr["IS_INACTIVE"]) ? true : Convert.ToBoolean(dr["IS_INACTIVE"]),
                        STATUS = Convert.IsDBNull(dr["STATUS"]) ? null : Convert.ToString(dr["STATUS"]),
                    });
                }

                connection.Close();
            }
            return departmentList;
        }

        public int SaveDepartmentGroup(DepartmentGroup department)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_TB_DEPARTMENT_GROUP"
                    };

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", (object)department.ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("CODE", (object)department.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DESCRIPTION", (object)department.DESCRIPTION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("COMPANY_ID", (object)department.COMPANY_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", (object)department.IS_INACTIVE ?? DBNull.Value);

                    int departmentID = Convert.ToInt32(cmd.ExecuteScalar());
                    return departmentID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DepartmentGroup GetDepartmentGroupById(int id)
        {
            DepartmentGroup department = new DepartmentGroup();

            try
            {
                string strSQL = "SELECT TB_DEPARTMENT_GROUP.ID, TB_DEPARTMENT_GROUP.CODE, TB_DEPARTMENT_GROUP.DESCRIPTION, TB_DEPARTMENT_GROUP.COMPANY_ID,TB_DEPARTMENT_GROUP.IS_INACTIVE FROM TB_DEPARTMENT_GROUP" +
                                " WHERE TB_DEPARTMENT_GROUP.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "DepartmentGroup");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    department.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
                    department.CODE = dr["CODE"] != DBNull.Value ? Convert.ToString(dr["CODE"]) : "";
                    department.DESCRIPTION = dr["DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["DESCRIPTION"]) : "";

                    department.COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COMPANY_ID"]) : 0;

                    department.IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(dr["IS_INACTIVE"]);
    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return department;
        }

        public bool DeleteDepartmentGroup(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_TB_DEPARTMENT_GROUP"
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
