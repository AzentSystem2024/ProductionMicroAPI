using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class FixedAssetService : IFixedAssetService
    {
        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[]
            {
            "dd-MM-yyyy HH:mm:ss",
            "dd-MM-yyyy",
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd",
            "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy"
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public FixedAssetListResponse GetFixedAssetList()
        {
            FixedAssetListResponse response = new FixedAssetListResponse
            {
                Data = new List<FixedAssetList>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_AC_FIXEDASSET", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var asset = new FixedAssetList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    CODE = reader["CODE"] != DBNull.Value ? reader["CODE"].ToString() : null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    ASSET_TYPE = reader["ASSET_TYPE"] != DBNull.Value ? reader["ASSET_TYPE"].ToString() : null,
                                    //ASSET_LEDGER_ID = reader["ASSET_LEDGER_ID"] != DBNull.Value ? Convert.ToInt32(reader["ASSET_LEDGER_ID"]) : 0,
                                    ASSET_VALUE = reader["PURCHASE_VALUE"] != DBNull.Value ? Convert.ToSingle(reader["PURCHASE_VALUE"]) : 0f,
                                    USEFUL_LIFE = reader["USEFUL_LIFE"] != DBNull.Value ? Convert.ToInt32(reader["USEFUL_LIFE"]) : 0,
                                    //RESIDUAL_VALUE = reader["RESIDUAL_VALUE"] != DBNull.Value ? Convert.ToDecimal(reader["RESIDUAL_VALUE"]) : 0m,
                                   // DEPR_LEDGER_ID = reader["DEPR_LEDGER_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPR_LEDGER_ID"]) : 0,
                                    //DEPR_PERCENT = reader["DEPR_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DEPR_PERCENT"]) : 0f,
                                    PURCH_DATE = reader["PURCHASE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCHASE_DATE"]).ToString("dd/MM/yyyy") : null,
                                    //LAST_DEPR_DATE = reader["LAST_DEPR_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["LAST_DEPR_DATE"]).ToString("dd/MM/yyyy") : null,
                                    NET_DEPRECIATION = reader["TOTAL_DEPRECIATION"] != DBNull.Value ? Convert.ToSingle(reader["TOTAL_DEPRECIATION"]) : 0f,
                                    CURRENT_VALUE = reader["CURRENT_VALUE"] != DBNull.Value ? Convert.ToSingle(reader["CURRENT_VALUE"]) : 0f,
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"])
                                };
                                response.Data.Add(asset);
                            }
                        }
                    }
                }

                response.flag = 1; 
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = 0; 
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }

        public FixedAssetSaveResponse SaveData(FixedAsset fixedAsset)
        {
            FixedAssetSaveResponse res = new FixedAssetSaveResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_AC_FIXEDASSET", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@CODE", (object)fixedAsset.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", (object)fixedAsset.DESCRIPTION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ASSET_TYPE_NAME", (object)fixedAsset.ASSET_TYPE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ASSET_LEDGER_ID", (object)fixedAsset.ASSET_LEDGER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ASSET_VALUE", (object)fixedAsset.ASSET_VALUE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@USEFUL_LIFE", (object)fixedAsset.USEFUL_LIFE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RESIDUAL_VALUE", (object)fixedAsset.RESIDUAL_VALUE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEPR_LEDGER_ID", (object)fixedAsset.DEPR_LEDGER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEPR_PERCENT", (object)fixedAsset.DEPR_PERCENT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PURCH_DATE", (object)fixedAsset.PURCH_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_INACTIVE", (object)fixedAsset.IS_INACTIVE ?? DBNull.Value);

                    cmd.ExecuteNonQuery();

                }
                res.flag = "1";
                res.message = "Data saved successfully.";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = "An error occurred: " + ex.Message;
            }

            return res;
        }

        public FixedAssetSaveResponse UpdateData(FixedAsset fixedAsset)
        {
            FixedAssetSaveResponse response = new FixedAssetSaveResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_AC_FIXEDASSET", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", fixedAsset.ID);
                        cmd.Parameters.AddWithValue("@CODE", (object)fixedAsset.CODE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", (object)fixedAsset.DESCRIPTION ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ASSET_TYPE_NAME", (object)fixedAsset.ASSET_TYPE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ASSET_LEDGER_ID", (object)fixedAsset.ASSET_LEDGER_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ASSET_VALUE", (object)fixedAsset.ASSET_VALUE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@USEFUL_LIFE", (object)fixedAsset.USEFUL_LIFE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@RESIDUAL_VALUE", (object)fixedAsset.RESIDUAL_VALUE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DEPR_LEDGER_ID", (object)fixedAsset.DEPR_LEDGER_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DEPR_PERCENT", (object)fixedAsset.DEPR_PERCENT ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PURCH_DATE", (object)fixedAsset.PURCH_DATE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", (object)fixedAsset.IS_INACTIVE ?? DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response.flag = "1";
                                response.message = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = "0";
                response.message = "An error occurred: " + ex.Message;
            }

            return response;
        }

        public FixedAssetSelectResponse GetFixedAssetbyId(int? id = null)
        {
            FixedAssetSelectResponse response = new FixedAssetSelectResponse
            {
                Data = new List<FixedAssetSelect>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_AC_FIXEDASSET", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", (object)id ?? DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var asset = new FixedAssetSelect
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    CODE = reader["CODE"] != DBNull.Value ? reader["CODE"].ToString() : null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    ASSET_TYPE_ID = reader["ASSET_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["ASSET_TYPE_ID"]) : 0,
                                    ASSET_TYPE = reader["ASSET_TYPE"] != DBNull.Value ? reader["ASSET_TYPE"].ToString() : null,
                                    PURCH_DATE = reader["PURCHASE_DATE"] != DBNull.Value ? reader["PURCHASE_DATE"].ToString() : null,
                                    ASSET_VALUE = reader["PURCHASE_VALUE"] != DBNull.Value ? Convert.ToSingle(reader["PURCHASE_VALUE"]) : 0,
                                    ASSET_LEDGER_ID = reader["ASSET_LEDGER_ID"] != DBNull.Value ? Convert.ToInt32(reader["ASSET_LEDGER_ID"]) : 0,
                                    RESIDUAL_VALUE = reader["RESIDUAL_VALUE"] != DBNull.Value ? Convert.ToDecimal(reader["RESIDUAL_VALUE"]) :0,
                                    DEPR_LEDGER_ID = reader["DEPR_LEDGER_ID"] != DBNull.Value ? Convert.ToInt32(reader["DEPR_LEDGER_ID"]) : 0,
                                    DEPR_PERCENT = reader["DEPR_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DEPR_PERCENT"]) : 0,
                                    USEFUL_LIFE = reader["USEFUL_LIFE"] != DBNull.Value ? Convert.ToInt32(reader["USEFUL_LIFE"]) : 0,
                                    LAST_DEPR_DATE = reader["LAST_DEPR_DATE"] != DBNull.Value ? reader["LAST_DEPR_DATE"].ToString() : null,
                                    NET_DEPRECIATION = reader["NET_DEPRECIATION"] != DBNull.Value ? Convert.ToSingle(reader["NET_DEPRECIATION"]) : 0f,
                                   // CURRENT_VALUE = reader["CURRENT_VALUE"] != DBNull.Value ? Convert.ToSingle(reader["CURRENT_VALUE"]) : 0f,
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"])
                                };
                                response.Data.Add(asset);
                            }
                        }
                    }
                }

                response.flag = 1;
                response.message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = "An error occurred: " + ex.Message;
            }

            return response;
        }

        public FixedAssetSaveResponse Delete(int id)
        {
            try
            {
                FixedAssetSaveResponse res = new FixedAssetSaveResponse();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_AC_FIXEDASSET";
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FixedResponse Save(ASSET asset)
        {
            FixedResponse res = new FixedResponse();

            try
            {
                // ✅ 1. Validate DISTRICT_NAME
                if (string.IsNullOrWhiteSpace(asset.ASSET_TYPE))
                {
                    res.flag = 0;
                    res.Message = "Asset name is required.";
                    return res;
                }

                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string sql = @"INSERT INTO TB_AC_ASSET_TYPE (ASSET_TYPE)
                           VALUES (@ASSET_TYPE)";

                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@ASSET_TYPE", asset.ASSET_TYPE);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error inserting Asset Type: " + ex.Message;
            }

            return res;
        }
    }
}
