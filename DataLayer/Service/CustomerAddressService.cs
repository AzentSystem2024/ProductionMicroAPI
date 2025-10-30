using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class CustomerAddressService:ICustomerAddressService
    {
        public int Insert(CustomerAddress address)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_CUST_DELIVERY_ADDRESS", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters for the stored procedure
                        cmd.Parameters.AddWithValue("@ACTION", 1); // Insert
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADDRESS1", address.ADDRESS1 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS2", address.ADDRESS2 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS3", address.ADDRESS3 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@LOCATION", address.LOCATION ?? string.Empty);
                        cmd.Parameters.AddWithValue("@MOBILE", address.MOBILE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PHONE", address.PHONE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", address.IS_INACTIVE ?? false);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InsertCustomerAddress Error: " + ex.Message, ex);
            }
        }
        public int Update(CustomerAddressUpdate address)
        {
            int result = 0;
            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_CUST_DELIVERY_ADDRESS", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@ID", address.ID);
                    cmd.Parameters.AddWithValue("@ADDRESS1", (object?)address.ADDRESS1 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADDRESS2", (object?)address.ADDRESS2 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADDRESS3", (object?)address.ADDRESS3 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LOCATION", (object?)address.LOCATION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOBILE", (object?)address.MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PHONE", (object?)address.PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_INACTIVE", (object?)address.IS_INACTIVE ?? false);

                    // connection.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public CustomerAddressUpdate GetById(int id)
        {
            CustomerAddressUpdate address = null;

            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_CUST_DELIVERY_ADDRESS", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", id);

                    //connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            address = new CustomerAddressUpdate
                            {
                                ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                                ADDRESS1 = dr["ADDRESS1"] != DBNull.Value ? dr["ADDRESS1"].ToString() : null,
                                ADDRESS2 = dr["ADDRESS2"] != DBNull.Value ? dr["ADDRESS2"].ToString() : null,
                                ADDRESS3 = dr["ADDRESS3"] != DBNull.Value ? dr["ADDRESS3"].ToString() : null,
                                LOCATION = dr["LOCATION"] != DBNull.Value ? dr["LOCATION"].ToString() : null,
                                MOBILE = dr["MOBILE"] != DBNull.Value ? dr["MOBILE"].ToString() : null,
                                PHONE = dr["PHONE"] != DBNull.Value ? dr["PHONE"].ToString() : null,
                                IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(dr["IS_INACTIVE"])
                            };
                        }
                    }
                }
            }
            return address;
        }
        public List<CustomerAddressUpdate> GetAllCustomers()
        {
            List<CustomerAddressUpdate> list = new List<CustomerAddressUpdate>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_CUST_DELIVERY_ADDRESS", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                    //con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new CustomerAddressUpdate
                            {
                                ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                                ADDRESS1 = dr["ADDRESS1"] != DBNull.Value ? dr["ADDRESS1"].ToString() : null,
                                ADDRESS2 = dr["ADDRESS2"] != DBNull.Value ? dr["ADDRESS2"].ToString() : null,
                                ADDRESS3 = dr["ADDRESS3"] != DBNull.Value ? dr["ADDRESS3"].ToString() : null,
                                LOCATION = dr["LOCATION"] != DBNull.Value ? dr["LOCATION"].ToString() : null,
                                MOBILE = dr["MOBILE"] != DBNull.Value ? dr["MOBILE"].ToString() : null,
                                PHONE = dr["PHONE"] != DBNull.Value ? dr["PHONE"].ToString() : null,
                                IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(dr["IS_INACTIVE"])
                            });
                        }
                    }
                }
            }

            return list;
        }
        public bool DeleteCustomers(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CUST_DELIVERY_ADDRESS";
                    cmd.Parameters.AddWithValue("ACTION", 3);
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
