using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SubDepartmentService:ISubDepartmentService
    {
        public List<SubDepartment> GetAllDepartments()
        {
            List<SubDepartment> departmentList = new List<SubDepartment>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_SUB_DEPARTMENT"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("ID", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    departmentList.Add(new SubDepartment
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        CODE = Convert.IsDBNull(dr["CODE"]) ? null : Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.IsDBNull(dr["DESCRIPTION"]) ? null : Convert.ToString(dr["DESCRIPTION"]),
                        DEPARTMENT_ID = Convert.IsDBNull(dr["DEPARTMENT_ID"]) ? 0 : Convert.ToInt32(dr["DEPARTMENT_ID"]),
                        DEPARTMENT_NAME = Convert.IsDBNull(dr["DEPT_NAME"]) ? null : Convert.ToString(dr["DEPT_NAME"])
                    });
                }

                connection.Close();
            }
            return departmentList;
        }
        public int SaveDepartment(SubDepartment subdepartment)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_TB_SUB_DEPARTMENT"
                    };

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", (object)subdepartment.ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("CODE", (object)subdepartment.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DESCRIPTION", (object)subdepartment.DESCRIPTION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DEPARTMENT_ID", (object)subdepartment.DEPARTMENT_ID ?? DBNull.Value);

                    int departmentID = Convert.ToInt32(cmd.ExecuteScalar());
                    return departmentID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SubDepartment GetDepartmentById(int id)
        {
            SubDepartment department = new SubDepartment();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_SUB_DEPARTMENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                department.ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                                department.CODE = reader["CODE"]?.ToString();
                                department.DESCRIPTION = reader["DESCRIPTION"]?.ToString();
                                department.DEPARTMENT_ID = reader["DEPARTMENT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPARTMENT_ID"]) : 0;
                                department.DEPARTMENT_NAME = reader["DEPT_NAME"] != DBNull.Value ? Convert.ToString(reader["DEPT_NAME"]) : null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw; 
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
                        CommandText = "SP_TB_SUB_DEPARTMENT"
                    };
                    cmd.Parameters.AddWithValue("ACTION", 2);
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
