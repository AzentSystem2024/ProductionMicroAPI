using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class InsuranceService: IInsuranceService
    {
        public InsuranceResponse Insert(Insurance insurance)
        {
            InsuranceResponse res = new InsuranceResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_INSURANCE";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@INSURANCE", insurance.INSURANCE_NAME);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", insurance.IS_INACTIVE);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        res.flag = 1;
                        res.Message = "Success";
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
        public InsuranceResponse Update(InsuranceUpdate insurance)
        {
            InsuranceResponse res = new InsuranceResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    string procedureName = "SP_TB_INSURANCE";
                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", insurance.ID);
                        cmd.Parameters.AddWithValue("@INSURANCE", insurance.INSURANCE_NAME);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", insurance.IS_INACTIVE);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        //res.flag = rowsAffected > 0 ? 1 : 0;
                        //res.Message = rowsAffected > 0 ? "Insert Success" : "Insert Failed";
                        res.flag = 1;
                        res.Message = "Success";
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

        public InsuranceResponse GetInsuranceById(int id)
        {
            InsuranceResponse res = new InsuranceResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_INSURANCE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                InsuranceUpdate ins = new InsuranceUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    INSURANCE_NAME = reader["INSURANCE_NAME"].ToString(),
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"])
                                };

                                res.Data = ins;
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Insurance not found.";
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


        public InsuranceListResponse GetLogList(int? id = null)
        {
            InsuranceListResponse res = new InsuranceListResponse();
            List<InsuranceUpdate> Lstinsurance = new List<InsuranceUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_INSURANCE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", (object?)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@INSURANCE", DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstinsurance.Add(new InsuranceUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                INSURANCE_NAME = dr["INSURANCE_NAME"].ToString(),
                                IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"])
                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstinsurance;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }
        public InsuranceResponse DeleteInsuranceData(int id)
        {
            InsuranceResponse res = new InsuranceResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_INSURANCE";

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
