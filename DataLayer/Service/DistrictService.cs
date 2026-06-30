using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class DistrictService:IDistrictService
    {
        public DistrictResponse Insert(District district)
        {
            DistrictResponse res = new DistrictResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DISTRICT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", Convert.ToInt32(district.COUNTRY_ID));
                        cmd.Parameters.AddWithValue("@STATE_ID", Convert.ToInt32(district.STATE_ID));
                        cmd.Parameters.AddWithValue("@DISTRICT", district.DISTRICT_NAME ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["FLAG"]);
                                res.Message = reader["MESSAGE"].ToString();
                            }
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
        public DistrictResponse Update(District district)
        {
            DistrictResponse res = new DistrictResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DISTRICT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", district.ID);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", Convert.ToInt32(district.COUNTRY_ID));
                        cmd.Parameters.AddWithValue("@STATE_ID", Convert.ToInt32(district.STATE_ID));
                        cmd.Parameters.AddWithValue("@DISTRICT", district.DISTRICT_NAME ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["FLAG"]);
                                res.Message = reader["MESSAGE"].ToString();
                            }
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
        public DistrictListResponse GetDistrictList()
        {
            DistrictListResponse res = new DistrictListResponse
            {
                Data = new List<DistrictList>(),
                flag = 0,
                Message = "Failed"
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DISTRICT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DistrictList district = new DistrictList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    COUNTRY_ID = reader["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COUNTRY_ID"]) : 0,
                                    COUNTRY_NAME = reader["COUNTRY_NAME"] != DBNull.Value ? Convert.ToString(reader["COUNTRY_NAME"]) : "",
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    STATE_NAME = reader["STATE_NAME"] != DBNull.Value ? Convert.ToString(reader["STATE_NAME"]) : "",
                                    DISTRICT_NAME = reader["DISTRICT"] != DBNull.Value ? Convert.ToString(reader["DISTRICT"]) : ""
                                };

                                res.Data.Add(district);
                            }
                        }
                    }
                }

                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        public DistrictResponse GetDistrictById(int id)
        {
            DistrictResponse res = new DistrictResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DISTRICT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DistrictList district = new DistrictList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    COUNTRY_ID = reader["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COUNTRY_ID"]) : 0,
                                    COUNTRY_NAME = reader["COUNTRY_NAME"] != DBNull.Value ? Convert.ToString(reader["COUNTRY_NAME"]) : "",
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    STATE_NAME = reader["STATE_NAME"] != DBNull.Value ? Convert.ToString(reader["STATE_NAME"]) : "",
                                    DISTRICT_NAME = reader["DISTRICT"] != DBNull.Value ? Convert.ToString(reader["DISTRICT"]) : ""
                                };

                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = district;
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "District not found";
                                res.Data = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = null;
            }

            return res;
        }
        public DistrictResponse Delete(int id)
        {
            DistrictResponse res = new DistrictResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_DISTRICT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 3);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.flag = Convert.ToInt32(reader["FLAG"]);
                                res.Message = Convert.ToString(reader["MESSAGE"]);
                            }
                        }
                    }
                }
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
