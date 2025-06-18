using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class SchemaService : ISchemaService
    {
        public SchemaResponse Insert(Schema schema)
        {
            SchemaResponse res = new SchemaResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_SCHEMA";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@SCHEMA", schema.SCHEMA_NAME);
                        cmd.Parameters.AddWithValue("@DISCOUNT", schema.DISCOUNT);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", schema.IS_INACTIVE);

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
        public SchemaResponse Update(SchemaUpdate schema)
        {
            SchemaResponse res = new SchemaResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    string procedureName = "SP_TB_SCHEMA";
                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", schema.ID);
                        cmd.Parameters.AddWithValue("@SCHEMA", schema.SCHEMA_NAME);
                        cmd.Parameters.AddWithValue("@DISCOUNT", schema.DISCOUNT);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", schema.IS_INACTIVE);
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
        public SchemaResponse GetSchemaById(int id)
        {
            SchemaResponse res = new SchemaResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_SCHEMA", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // SELECT
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new SchemaUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    SCHEMA_NAME = reader["SCHEMA_NAME"].ToString(),
                                    DISCOUNT = Convert.ToInt32(reader["DISCOUNT"]),
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"])
                                };
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Schema not found";
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

        public SchemaListResponse GetLogList(int? id = null)
        {
            SchemaListResponse res = new SchemaListResponse();
            List<SchemaUpdate> Lstschema = new List<SchemaUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_SCHEMA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", (object?)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SCHEMA", DBNull.Value);
                cmd.Parameters.AddWithValue("@DISCOUNT", DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstschema.Add(new SchemaUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                SCHEMA_NAME = dr["SCHEMA_NAME"].ToString(),
                                DISCOUNT = Convert.ToInt32(dr["DISCOUNT"]),
                                IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])
                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstschema;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }

        public SchemaResponse DeleteSchemaData(int id)
        {
            SchemaResponse res = new SchemaResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_SCHEMA";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
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
