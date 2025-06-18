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
                            // 1. Insert new Country if ID is 0
                            if (distributor.COUNTRY_ID == 0 && !string.IsNullOrWhiteSpace(distributor.COUNTRY_NAME))
                            {
                                string sql = @"INSERT INTO TB_COUNTRY (CODE, COUNTRY_NAME, FLAG_URL, IS_INACTIVE, IS_DELETED)
                                       OUTPUT INSERTED.ID
                                       VALUES (@CODE, @COUNTRY_NAME, @FLAG_URL, @IS_INACTIVE, 0)";
                                using (var cmd = new SqlCommand(sql, connection, tx))
                                {
                                    cmd.Parameters.AddWithValue("@CODE", distributor.CODE);
                                    cmd.Parameters.AddWithValue("@COUNTRY_NAME", distributor.COUNTRY_NAME);
                                    cmd.Parameters.AddWithValue("@FLAG_URL", distributor.FLAG_URL ?? (object)DBNull.Value);
                                    cmd.Parameters.AddWithValue("@IS_INACTIVE", distributor.IS_INACTIVE);
                                    distributor.COUNTRY_ID = (int)cmd.ExecuteScalar(); // Update ID
                                }
                            }

                            // 2. Insert new State if ID is 0
                            if (distributor.STATE_ID == 0 && !string.IsNullOrWhiteSpace(distributor.STATE_NAME))
                            {
                                string sql = @"INSERT INTO TB_STATE (STATE_NAME, COUNTRY_ID, IS_DELETED)
                                       OUTPUT INSERTED.ID
                                       VALUES (@NAME, @COUNTRY_ID, 0)";
                                using (var cmd = new SqlCommand(sql, connection, tx))
                                {
                                    cmd.Parameters.AddWithValue("@NAME", distributor.STATE_NAME);
                                    cmd.Parameters.AddWithValue("@COUNTRY_ID", distributor.COUNTRY_ID);
                                    distributor.STATE_ID = (int)cmd.ExecuteScalar();
                                }
                            }

                            // 3. Insert new District if ID is 0
                            if (distributor.DISTRICT_ID == 0 && !string.IsNullOrWhiteSpace(distributor.DISTRICT_NAME))
                            {
                                string sql = @"INSERT INTO TB_DISTRICT (DISTRICT, STATE_ID, COUNTRY_ID)
                                       OUTPUT INSERTED.ID
                                       VALUES (@NAME, @STATE_ID, @COUNTRY_ID)";
                                using (var cmd = new SqlCommand(sql, connection, tx))
                                {
                                    cmd.Parameters.AddWithValue("@NAME", distributor.DISTRICT_NAME);
                                    cmd.Parameters.AddWithValue("@STATE_ID", distributor.STATE_ID);
                                    cmd.Parameters.AddWithValue("@COUNTRY_ID", distributor.COUNTRY_ID);
                                    distributor.DISTRICT_ID = (int)cmd.ExecuteScalar();
                                }
                            }

                            // 4. Insert new City if ID is 0
                            if (distributor.CITY_ID == 0 && !string.IsNullOrWhiteSpace(distributor.CITY_NAME))
                            {
                                string sql = @"INSERT INTO TB_CITY (CITY_NAME, DISTRICT_ID, STATE_ID, COUNTRY_ID,STD_CODE)
                                       OUTPUT INSERTED.ID
                                       VALUES (@NAME, @DISTRICT_ID, @STATE_ID, @COUNTRY_ID,@STD_CODE)";
                                using (var cmd = new SqlCommand(sql, connection, tx))
                                {
                                    cmd.Parameters.AddWithValue("@NAME", distributor.CITY_NAME);
                                    cmd.Parameters.AddWithValue("@DISTRICT_ID", distributor.DISTRICT_ID);
                                    cmd.Parameters.AddWithValue("@STATE_ID", distributor.STATE_ID);
                                    cmd.Parameters.AddWithValue("@COUNTRY_ID", distributor.COUNTRY_ID);
                                    cmd.Parameters.AddWithValue("@STD_CODE", distributor.STD_CODE);
                                    distributor.CITY_ID = (int)cmd.ExecuteScalar();
                                }
                            }

                            // 5. Insert into TB_DISTRIBUTOR via SP
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
                                cmd.Parameters.AddWithValue("@EMAIL", distributor.EMAIL ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IS_ACTIVE", distributor.IS_ACTIVE);
                                cmd.Parameters.AddWithValue("@SALESMAN_EMAIL", distributor.SALESMAN_EMAIL ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@WAREHOUSE_ID", distributor.WAREHOUSE_ID);
                                cmd.Parameters.AddWithValue("@CONTACT_NAME", distributor.CONTACT_NAME ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@DISC_PERCENT", distributor.DISC_PERCENT);
                                cmd.Parameters.AddWithValue("@CREDIT_LIMIT", distributor.CREDIT_LIMIT);
                                cmd.Parameters.AddWithValue("@CREDIT_DAYS", distributor.CREDIT_DAYS);
                                cmd.Parameters.AddWithValue("@ZONE_ID", distributor.ZONE_ID);
                                cmd.Parameters.AddWithValue("@PARENT_ID", distributor.PARENT_ID);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    tx.Commit();
                                    res.flag = 1;
                                    res.Message = "Success";
                                }
                                else
                                {
                                    tx.Rollback();
                                    res.flag = 0;
                                    res.Message = "Distributor insertion failed.";
                                }
                            }
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




    }
}
