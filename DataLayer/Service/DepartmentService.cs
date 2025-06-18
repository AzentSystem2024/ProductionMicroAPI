using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class DepartmentService : IDepartmentService
    {
        public DepartmentResponse Insert(Department dept)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_DEPARTMENT";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@DEPARTMENT", dept.DEPARTMENT);
                        cmd.Parameters.AddWithValue("@HOSPITAL", dept.HOSPITAL_ID);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", dept.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@BILLPREFIX", dept.BILL_PREFIX);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";

                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Insert failed";
                        }
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        public DepartmentResponse Update(DepartmentUpdate dept)
        {
            DepartmentResponse res = new DepartmentResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    string procedureName = "SP_TB_DEPARTMENT";
                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", dept.ID);
                        cmd.Parameters.AddWithValue("@DEPARTMENT", dept.DEPARTMENT);
                        cmd.Parameters.AddWithValue("@HOSPITAL", dept.HOSPITAL_ID);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", dept.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@BILLPREFIX", dept.BILL_PREFIX);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Update failed";
                        }
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        public DepartmentResponse GetDepartmentById(int id)
        {
            DepartmentResponse res = new DepartmentResponse();
            res.Data = null;

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_DEPARTMENT";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DepartmentUpdate dept = new DepartmentUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    DEPARTMENT = reader["DEPARTMENT"].ToString(),
                                    HOSPITAL_ID = Convert.ToInt32(reader["HOSPITAL_ID"]),
                                    HOSPITAL_NAME = reader["HOSPITAL_NAME"].ToString(),
                                    BILL_PREFIX = reader["BILL_PREFIX"].ToString(),
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"])
                                };

                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = dept;
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = $"Department not found for ID = {id}";
                                res.Data = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }



        //public DepartmentListResponse GetLogList(int? id = null)  
        //{
        //    DepartmentListResponse res = new DepartmentListResponse();
        //    List<Department> Lstdepartment = new List<Department>();

        //    using (SqlConnection con = ADO.GetConnection())
        //    using (SqlCommand cmd = new SqlCommand("SP_TB_DEPARTMENT", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ACTION", 0);  

        //        if (id.HasValue)
        //            cmd.Parameters.AddWithValue("@ID", id.Value);
        //        else
        //            cmd.Parameters.AddWithValue("@ID", DBNull.Value);

        //        try
        //        {
        //            //con.Open();
        //            DataTable tbl = new DataTable();
        //            tbl.Load(cmd.ExecuteReader());

        //            foreach (DataRow dr in tbl.Rows)
        //            {
        //                Lstdepartment.Add(new Department
        //                {
        //                    ID = Convert.ToInt32(dr["ID"]),
        //                    DEPARTMENT = dr["DEPARTMENT"].ToString(),
        //                    HOSPITAL_ID = Convert.ToInt32(dr["HOSPITAL_ID"]),
        //                    HOSPITAL_NAME = dr["HOSPITAL_NAME"].ToString(),
        //                    IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])
        //                });
        //            }

        //            res.flag = 1;
        //            res.Message = "Success";
        //            res.Data = Lstdepartment;
        //        }
        //        catch (Exception ex)
        //        {
        //            res.flag = 0;
        //            res.Message = ex.Message;
        //        }
        //    }

        //    return res;
        //}

        public DepartmentListResponse GetLogList(int? id = null)
        {
            DepartmentListResponse res = new DepartmentListResponse();
            List<DepartmentUpdate> Lstdepartment = new List<DepartmentUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_DEPARTMENT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", (object?)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DEPARTMENT", DBNull.Value);
                cmd.Parameters.AddWithValue("@HOSPITAL", DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);
                cmd.Parameters.AddWithValue("@BILLPREFIX", DBNull.Value);

                try
                {
                    //con.Open(); // Ensure connection is open
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstdepartment.Add(new DepartmentUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                DEPARTMENT = dr["DEPARTMENT"].ToString(),
                                HOSPITAL_ID = Convert.ToInt32(dr["HOSPITAL_ID"]),
                                HOSPITAL_NAME = dr["HOSPITAL_NAME"].ToString(),
                                IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])
                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstdepartment;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }



        public DepartmentResponse DeleteDepartmentData(int id)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_DEPARTMENT";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID",id);
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        

                        int rowsAffected = cmd.ExecuteNonQuery();


                    }

                }
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
