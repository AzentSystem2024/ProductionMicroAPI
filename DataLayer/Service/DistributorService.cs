using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class DistributorService:IDistributorService
    {
        public DistributorResponse Insert(Distributor distributor)
        {
            DistributorResponse res = new DistributorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var tx = connection.BeginTransaction())
                    {
                        try
                        {                           
                            int distributorId = 0;
                            using (var cmd = new SqlCommand("SP_TB_DISTRIBUTOR", connection, tx))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                                cmd.Parameters.AddWithValue("@ACTION", 1);
                                cmd.Parameters.AddWithValue("@CODE", distributor.CODE);
                                cmd.Parameters.AddWithValue("@DISTRIBUTOR_NAME", distributor.DISTRIBUTOR_NAME);
                                cmd.Parameters.AddWithValue("@ADDRESS", distributor.ADDRESS);
                                cmd.Parameters.AddWithValue("@COUNTRY_ID", distributor.COUNTRY_ID);
                                cmd.Parameters.AddWithValue("@STATE_ID", distributor.STATE_ID);
                                cmd.Parameters.AddWithValue("@DISTRICT_ID", distributor.DISTRICT_ID);
                                cmd.Parameters.AddWithValue("@CITY_ID", distributor.CITY_ID);
                                cmd.Parameters.AddWithValue("@TELEPHONE", distributor.TELEPHONE ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@FAX", distributor.FAX ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@MOBILE", distributor.MOBILE ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@WHATSAPP_NO", distributor.WHATSAPP_NO ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@EMAIL", distributor.EMAIL ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IS_INACTIVE", distributor.IS_INACTIVE);
                                cmd.Parameters.AddWithValue("@SALESMAN_EMAIL", distributor.SALESMAN_EMAIL ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@WAREHOUSE_ID", distributor.WAREHOUSE_ID);
                                cmd.Parameters.AddWithValue("@CONTACT_NAME", distributor.CONTACT_NAME ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@DISC_PERCENT", distributor.DISC_PERCENT);
                                cmd.Parameters.AddWithValue("@CREDIT_LIMIT", distributor.CREDIT_LIMIT);
                                cmd.Parameters.AddWithValue("@CREDIT_DAYS", distributor.CREDIT_DAYS);
                                cmd.Parameters.AddWithValue("@ZONE_ID", distributor.ZONE_ID);
                                cmd.Parameters.AddWithValue("@LOGIN_NAME", distributor.LOGIN_NAME);
                                cmd.Parameters.AddWithValue("@LOGIN_PASSWORD", distributor.LOGIN_PASSWORD);
                                cmd.Parameters.AddWithValue("@PARENT_ID", distributor.PARENT_ID);

                                object insertedId = cmd.ExecuteScalar();
                                distributorId = insertedId != null ? Convert.ToInt32(insertedId) : 0;
                            }

                            // ✅ 4. Insert into TB_DISTRIBUTOR_LOCATIONS
                            if (distributor.LOCATIONS != null && distributor.LOCATIONS.Any())
                            {
                                string insertLocSql = @"
                            INSERT INTO TB_DISTRIBUTOR_LOCATIONS 
                            (DISTRIBUTOR_ID, LOCATION, ADDRESS, TELEPHONE, MOBILE, IS_INACTIVE)
                            VALUES (@DISTRIBUTOR_ID, @LOCATION, @ADDRESS, @TELEPHONE, @MOBILE, @IS_INACTIVE)";

                                foreach (var location in distributor.LOCATIONS)
                                {
                                    using (var locCmd = new SqlCommand(insertLocSql, connection, tx))
                                    {
                                        locCmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", distributorId);
                                        locCmd.Parameters.AddWithValue("@LOCATION", location.LOCATION);
                                        locCmd.Parameters.AddWithValue("@ADDRESS", location.ADDRESS ?? (object)DBNull.Value);
                                        locCmd.Parameters.AddWithValue("@TELEPHONE", location.TELEPHONE ?? (object)DBNull.Value);
                                        locCmd.Parameters.AddWithValue("@MOBILE", location.MOBILE ?? (object)DBNull.Value);
                                        locCmd.Parameters.AddWithValue("@IS_INACTIVE", location.IS_INACTIVE);
                                        locCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            tx.Commit();
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        catch (Exception ex1)
                        {
                            tx.Rollback();
                            res.flag = 0;
                            res.Message = "Error during transaction: " + ex1.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Database connection failed: " + ex.Message;
            }

            return res;
        }



        public DistributorResponse UpdateDistributor(DistributorUpdate distributor)
        {
            DistributorResponse res = new DistributorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var tx = connection.BeginTransaction())
                    {
                        try
                        {

                            using (var cmd = new SqlCommand("SP_TB_DISTRIBUTOR", connection, tx))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@ID", distributor.ID);
                                cmd.Parameters.AddWithValue("@ACTION", 2); 
                                cmd.Parameters.AddWithValue("@CODE", distributor.CODE);
                                cmd.Parameters.AddWithValue("@DISTRIBUTOR_NAME", distributor.DISTRIBUTOR_NAME);
                                cmd.Parameters.AddWithValue("@ADDRESS", distributor.ADDRESS);
                                cmd.Parameters.AddWithValue("@COUNTRY_ID", distributor.COUNTRY_ID);
                                cmd.Parameters.AddWithValue("@STATE_ID", distributor.STATE_ID);
                                cmd.Parameters.AddWithValue("@DISTRICT_ID", distributor.DISTRICT_ID);
                                cmd.Parameters.AddWithValue("@CITY_ID", distributor.CITY_ID);
                                cmd.Parameters.AddWithValue("@TELEPHONE", distributor.TELEPHONE ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@FAX", distributor.FAX ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@MOBILE", distributor.MOBILE ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@WHATSAPP_NO", distributor.WHATSAPP_NO ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@EMAIL", distributor.EMAIL ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IS_INACTIVE", distributor.IS_INACTIVE);
                                cmd.Parameters.AddWithValue("@SALESMAN_EMAIL", distributor.SALESMAN_EMAIL ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@WAREHOUSE_ID", distributor.WAREHOUSE_ID);
                                cmd.Parameters.AddWithValue("@CONTACT_NAME", distributor.CONTACT_NAME ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@DISC_PERCENT", distributor.DISC_PERCENT);
                                cmd.Parameters.AddWithValue("@CREDIT_LIMIT", distributor.CREDIT_LIMIT);
                                cmd.Parameters.AddWithValue("@CREDIT_DAYS", distributor.CREDIT_DAYS);
                                cmd.Parameters.AddWithValue("@ZONE_ID", distributor.ZONE_ID);
                                cmd.Parameters.AddWithValue("@LOGIN_NAME", distributor.LOGIN_NAME);
                                cmd.Parameters.AddWithValue("@LOGIN_PASSWORD", distributor.LOGIN_PASSWORD);
                                cmd.Parameters.AddWithValue("@PARENT_ID", distributor.PARENT_ID ?? (object)DBNull.Value);

                                cmd.ExecuteNonQuery();
                            }

                            // ✅ Step 2: Update Distributor Location(s)
                            if (distributor.LOCATIONS != null && distributor.LOCATIONS.Any())
                            {
                                foreach (var loc in distributor.LOCATIONS)
                                {
                                    string sql = @"
                                UPDATE TB_DISTRIBUTOR_LOCATIONS
                                SET
                                    DISTRIBUTOR_ID = @DISTRIBUTOR_ID,
                                    LOCATION = @LOCATION,
                                    ADDRESS = @ADDRESS,
                                    TELEPHONE = @TELEPHONE,
                                    MOBILE = @MOBILE,
                                    IS_INACTIVE = @IS_INACTIVE
                                WHERE ID = @ID";

                                    using (var cmd = new SqlCommand(sql, connection, tx))
                                    {
                                        cmd.Parameters.AddWithValue("@ID", loc.ID);
                                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", distributor.ID);
                                        cmd.Parameters.AddWithValue("@LOCATION", loc.LOCATION);
                                        cmd.Parameters.AddWithValue("@ADDRESS", loc.ADDRESS ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@TELEPHONE", loc.TELEPHONE ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@MOBILE", loc.MOBILE ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@IS_INACTIVE", loc.IS_INACTIVE);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            tx.Commit();
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        catch (Exception ex1)
                        {
                            tx.Rollback();
                            res.flag = 0;
                            res.Message = "Error during transaction: " + ex1.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Database connection error: " + ex.Message;
            }

            return res;
        }

        public DistributorListResponse GetDistributorList()
        {
            DistributorListResponse res = new DistributorListResponse
            {
                Data = new List<DistributorUpdate>(),
                flag = 0,
                Message = "Failed"
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    List<DistributorUpdate> distributors = new List<DistributorUpdate>();

                    // First, read all distributors
                    using (var cmd = new SqlCommand("SP_TB_DISTRIBUTOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dist = new DistributorUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CODE = reader["CODE"]?.ToString(),
                                    DISTRIBUTOR_NAME = reader["DISTRIBUTOR_NAME"]?.ToString(),
                                    ADDRESS = reader["ADDRESS"]?.ToString(),
                                    COUNTRY_ID = reader["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COUNTRY_ID"]) : 0,
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    STATE_NAME = reader["STATE_NAME"] != DBNull.Value ? reader["STATE_NAME"].ToString() : string.Empty,
                                    DISTRICT_ID = reader["DISTRICT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DISTRICT_ID"]) : 0,
                                    CITY_ID = reader["CITY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CITY_ID"]) : 0,
                                    TELEPHONE = reader["TELEPHONE"]?.ToString(),
                                    FAX = reader["FAX"]?.ToString(),
                                    MOBILE = reader["MOBILE"]?.ToString(),
                                    WHATSAPP_NO = reader["WHATSAPP_NO"]?.ToString(),
                                    EMAIL = reader["EMAIL"]?.ToString(),
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : false,
                                    SALESMAN_EMAIL = reader["SALESMAN_EMAIL"]?.ToString(),
                                    WAREHOUSE_ID = reader["WAREHOUSE_ID"] != DBNull.Value ? Convert.ToInt32(reader["WAREHOUSE_ID"]) : 0,
                                    CONTACT_NAME = reader["CONTACT_NAME"]?.ToString(),
                                    DISC_PERCENT = reader["DISC_PERCENT"] != DBNull.Value ? Convert.ToDecimal(reader["DISC_PERCENT"]) : 0,
                                    CREDIT_LIMIT = reader["CREDIT_LIMIT"] != DBNull.Value ? Convert.ToDecimal(reader["CREDIT_LIMIT"]) : 0,
                                    CREDIT_DAYS = reader["CREDIT_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["CREDIT_DAYS"]) : 0,
                                    ZONE_ID = reader["ZONE_ID"] != DBNull.Value ? Convert.ToInt32(reader["ZONE_ID"]) : 0,
                                    LOGIN_NAME = reader["LOGIN_NAME"]?.ToString(),
                                    LOGIN_PASSWORD = reader["LOGIN_PASSWORD"]?.ToString(),
                                    PARENT_ID = reader["PARENT_ID"] != DBNull.Value ? Convert.ToInt32(reader["PARENT_ID"]) : (int?)null
                                };

                                distributors.Add(dist);
                            }
                        }
                    }

                    // Second, fetch location list for each distributor (after first reader is closed)
                    foreach (var dist in distributors)
                    {
                        dist.LOCATIONS = new List<DistributorLocationUpdate>();

                        using (var locCmd = new SqlCommand(@"
                    SELECT ID, DISTRIBUTOR_ID, LOCATION, ADDRESS, TELEPHONE, MOBILE, IS_INACTIVE 
                    FROM TB_DISTRIBUTOR_LOCATIONS 
                    WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID", connection))
                        {
                            locCmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", dist.ID);

                            using (var locReader = locCmd.ExecuteReader())
                            {
                                while (locReader.Read())
                                {
                                    dist.LOCATIONS.Add(new DistributorLocationUpdate
                                    {
                                        ID = Convert.ToInt32(locReader["ID"]),
                                        DISTRIBUTOR_ID = Convert.ToInt32(locReader["DISTRIBUTOR_ID"]),
                                        LOCATION = locReader["LOCATION"]?.ToString(),
                                        ADDRESS = locReader["ADDRESS"]?.ToString(),
                                        TELEPHONE = locReader["TELEPHONE"]?.ToString(),
                                        MOBILE = locReader["MOBILE"]?.ToString(),
                                        IS_INACTIVE = locReader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(locReader["IS_INACTIVE"]) : false
                                    });
                                }
                            }
                        }
                    }

                    res.Data = distributors;
                    res.flag = 1;
                    res.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }


        public DistributorResponse GetDistributorById(int id)
        {
            DistributorResponse res = new DistributorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // STEP 1: Get Distributor by ID
                    using (var cmd = new SqlCommand("SP_TB_DISTRIBUTOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@CODE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DISTRIBUTOR_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADDRESS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", 0);
                        cmd.Parameters.AddWithValue("@STATE_ID", 0);
                        cmd.Parameters.AddWithValue("@DISTRICT_ID", 0);
                        cmd.Parameters.AddWithValue("@CITY_ID", 0);
                        cmd.Parameters.AddWithValue("@TELEPHONE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FAX", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MOBILE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WHATSAPP_NO", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SALESMAN_EMAIL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WAREHOUSE_ID", 0);
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DISC_PERCENT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREDIT_LIMIT", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CREDIT_DAYS", 0);
                        cmd.Parameters.AddWithValue("@ZONE_ID", 0);
                        cmd.Parameters.AddWithValue("@LOGIN_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LOGIN_PASSWORD", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PARENT_ID", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var distributor = new DistributorUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CODE = reader["CODE"]?.ToString(),
                                    DISTRIBUTOR_NAME = reader["DISTRIBUTOR_NAME"]?.ToString(),
                                    ADDRESS = reader["ADDRESS"]?.ToString(),
                                    COUNTRY_ID = reader["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(reader["COUNTRY_ID"]) : 0,
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    DISTRICT_ID = reader["DISTRICT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DISTRICT_ID"]) : 0,
                                    CITY_ID = reader["CITY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CITY_ID"]) : 0,
                                    TELEPHONE = reader["TELEPHONE"]?.ToString(),
                                    FAX = reader["FAX"]?.ToString(),
                                    MOBILE = reader["MOBILE"]?.ToString(),
                                    WHATSAPP_NO = reader["WHATSAPP_NO"]?.ToString(),
                                    EMAIL = reader["EMAIL"]?.ToString(),
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : false,
                                    SALESMAN_EMAIL = reader["SALESMAN_EMAIL"]?.ToString(),
                                    WAREHOUSE_ID = reader["WAREHOUSE_ID"] != DBNull.Value ? Convert.ToInt32(reader["WAREHOUSE_ID"]) : 0,
                                    CONTACT_NAME = reader["CONTACT_NAME"]?.ToString(),
                                    DISC_PERCENT = reader["DISC_PERCENT"] != DBNull.Value ? Convert.ToDecimal(reader["DISC_PERCENT"]) : 0m,
                                    CREDIT_LIMIT = reader["CREDIT_LIMIT"] != DBNull.Value ? Convert.ToDecimal(reader["CREDIT_LIMIT"]) : 0m,
                                    CREDIT_DAYS = reader["CREDIT_DAYS"] != DBNull.Value ? Convert.ToInt32(reader["CREDIT_DAYS"]) : 0,
                                    ZONE_ID = reader["ZONE_ID"] != DBNull.Value ? Convert.ToInt32(reader["ZONE_ID"]) : 0,
                                    LOGIN_NAME = reader["LOGIN_NAME"]?.ToString(),
                                    LOGIN_PASSWORD = reader["LOGIN_PASSWORD"]?.ToString(),
                                    PARENT_ID = reader["PARENT_ID"] != DBNull.Value ? Convert.ToInt32(reader["PARENT_ID"]) : (int?)null
                                };

                                // STEP 2: Get Distributor Location(s)
                                reader.Close();

                                string locationQuery = @"
                            SELECT ID, DISTRIBUTOR_ID, LOCATION, ADDRESS, TELEPHONE, MOBILE, IS_INACTIVE 
                            FROM TB_DISTRIBUTOR_LOCATIONS 
                            WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID";

                                using (var locCmd = new SqlCommand(locationQuery, connection))
                                {
                                    locCmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", distributor.ID);
                                    using (var locReader = locCmd.ExecuteReader())
                                    {
                                        distributor.LOCATIONS = new List<DistributorLocationUpdate>();

                                        while (locReader.Read())
                                        {
                                            distributor.LOCATIONS.Add(new DistributorLocationUpdate
                                            {
                                                ID = Convert.ToInt32(locReader["ID"]),
                                                DISTRIBUTOR_ID = Convert.ToInt32(locReader["DISTRIBUTOR_ID"]),
                                                LOCATION = locReader["LOCATION"]?.ToString(),
                                                ADDRESS = locReader["ADDRESS"]?.ToString(),
                                                TELEPHONE = locReader["TELEPHONE"]?.ToString(),
                                                MOBILE = locReader["MOBILE"]?.ToString(),
                                                IS_INACTIVE = Convert.ToBoolean(locReader["IS_INACTIVE"])
                                            });
                                        }
                                    }
                                }

                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = distributor;
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Distributor not found";
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
        public DistributorResponse DeleteDistributorData(int id)
        {
            DistributorResponse res = new DistributorResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_DISTRIBUTOR";

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

        public InsertResponse InsertDistrict(District district)
        {
            InsertResponse res = new InsertResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string sql = @"INSERT INTO TB_DISTRICT (DISTRICT, STATE_ID, COUNTRY_ID)
                           VALUES (@DISTRICT, @STATE_ID, @COUNTRY_ID)";

                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@DISTRICT", district.DISTRICT_NAME?.Trim());
                        cmd.Parameters.AddWithValue("@STATE_ID", district.STATE_ID);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", district.COUNTRY_ID);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error inserting district: " + ex.Message;
            }

            return res;
        }

        public InsertResponse InsertCity(City city)
        {
            InsertResponse res = new InsertResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string sql = @"INSERT INTO TB_CITY (CITY_NAME, DISTRICT_ID, STATE_ID, COUNTRY_ID, STD_CODE)
                           VALUES (@CITY_NAME, @DISTRICT_ID, @STATE_ID, @COUNTRY_ID, @STD_CODE)";

                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@CITY_NAME", city.CITY_NAME?.Trim());
                        cmd.Parameters.AddWithValue("@DISTRICT_ID", city.DISTRICT_ID);
                        cmd.Parameters.AddWithValue("@STATE_ID", city.STATE_ID);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", city.COUNTRY_ID);
                        cmd.Parameters.AddWithValue("@STD_CODE", city.STD_CODE);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "failed.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error inserting city: " + ex.Message;
            }

            return res;
        }


    }
}
