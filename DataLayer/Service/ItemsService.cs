using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ItemsService : IItemsService
    {
        public ItemResponse Insert(Item item)
        {
            ItemResponse res = new ItemResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ITEMS";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@ITEMSCODE", item.ITEM_CODE);
                        cmd.Parameters.AddWithValue("@ITEMSNAME", item.ITEM_NAME);
                        cmd.Parameters.AddWithValue("@ISFIXEDPRICE", item.IS_FIXED_PRICE);
                        cmd.Parameters.AddWithValue("@PRICE", item.PRICE);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", item.IS_INACTIVE);


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
        public ItemResponse Update(ItemUpdate item)
        {
            ItemResponse res = new ItemResponse();
            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    string procedureName = "SP_TB_ITEMS";
                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", item.ID);
                        cmd.Parameters.AddWithValue("@ITEMSCODE", item.ITEM_CODE);
                        cmd.Parameters.AddWithValue("@ITEMSNAME", item.ITEM_NAME);
                        cmd.Parameters.AddWithValue("@ISFIXEDPRICE", item.IS_FIXED_PRICE);
                        cmd.Parameters.AddWithValue("@PRICE", item.PRICE);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", item.IS_INACTIVE);
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
        public ItemResponse GetItemById(int id)
        {
            ItemResponse res = new ItemResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_ITEMS", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // SELECT
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@ITEMSCODE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ITEMSNAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ISFIXEDPRICE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PRICE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res.Data = new ItemUpdate
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    ITEM_CODE = reader["ITEM_CODE"].ToString(),
                                    ITEM_NAME = reader["ITEM_NAME"].ToString(),
                                    IS_FIXED_PRICE = Convert.ToBoolean(reader["IS_FIXED_PRICE"]),
                                    PRICE = Convert.ToDecimal(reader["PRICE"]),
                                    IS_INACTIVE = Convert.ToBoolean(reader["IS_INACTIVE"]),
                                    DEPARTMENT_ID = reader["DEPARTMENT_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPARTMENT_ID"]) : 0,
                                    DEPARTMENT_NAME = reader["DEPARTMENT_NAME"]?.ToString() ?? string.Empty
                                };
                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Item not found";
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

        public ItemListResponse GetLogList(int? id = null)
        {
            ItemListResponse res = new ItemListResponse();
            List<ItemUpdate> Lstitem = new List<ItemUpdate>();

            using (SqlConnection con = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_ITEMS", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", (object?)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ITEMSCODE", DBNull.Value);
                cmd.Parameters.AddWithValue("@ITEMSNAME", DBNull.Value);
                cmd.Parameters.AddWithValue("@ISFIXEDPRICE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PRICE", DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Load(reader);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Lstitem.Add(new ItemUpdate
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                ITEM_CODE = dr["ITEM_CODE"].ToString(),
                                ITEM_NAME = dr["ITEM_NAME"].ToString(),
                                IS_FIXED_PRICE = Convert.ToBoolean(dr["IS_FIXED_PRICE"]),
                                PRICE = Convert.ToInt32(dr["PRICE"]),
                                IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),
                                DEPARTMENT_ID = Convert.ToInt32(dr["DEPARTMENT_ID"]),
                                DEPARTMENT_NAME = dr["DEPARTMENT_NAME"].ToString()
                            });
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                    res.Data = Lstitem;
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = ex.Message;
                }
            }

            return res;
        }
        public ItemResponse DeleteItemData(int id)
        {
            ItemResponse res = new ItemResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_ITEMS";

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
