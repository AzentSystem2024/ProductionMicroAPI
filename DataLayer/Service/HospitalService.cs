using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.DataLayer.Service
{
    public class HospitalService : IHospitalService
    {
        public HospitalResponse Insert(Hospital hospital)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_HOSPITAL";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@HOSPITAL", hospital.HOSPITAL_NAME);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", hospital.IS_INACTIVE);

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
        public HospitalResponse Update(HospitalUpdate hospital)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_HOSPITAL", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", hospital.ID);
                        cmd.Parameters.AddWithValue("@HOSPITAL", hospital.HOSPITAL_NAME);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", hospital.IS_INACTIVE);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                            return res;
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Update failed";
                        }
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
        public HospitalResponse GetHospitalById(int id)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_HOSPITAL", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // SELECT
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new HospitalUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    HOSPITAL_NAME = reader["HOSPITAL_NAME"].ToString(),
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"])
                                };
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = $"Hospital not found for ID = {id}";
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

        public HospitalListResponse GetLogList(int? id = null)
        {
            HospitalListResponse res = new HospitalListResponse();
            List<HospitalUpdate> Lsthospital = new List<HospitalUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_HOSPITAL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", (object?)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@HOSPITAL", DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lsthospital.Add(new HospitalUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                HOSPITAL_NAME = dr["HOSPITAL_NAME"].ToString(),
                                IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])
                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lsthospital;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }
        public HospitalResponse DeleteHospitalData(int id)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_HOSPITAL";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

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
