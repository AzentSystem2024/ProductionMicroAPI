using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class DepreciationService :IDepreciationService
    {
        private static object ParseDate(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy" };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public DepreciationResponse GetFixedAssetsList()
        {
            DepreciationResponse response = new DepreciationResponse { Data = new List<FixedAssetLists>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_DEPRECIATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new FixedAssetLists
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    CODE = reader["CODE"] != DBNull.Value ? reader["CODE"].ToString() : null,
                                    DESCRIPTION = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null,
                                    ASSET_VALUE = reader["PURCHASE_VALUE"] != DBNull.Value ? Convert.ToSingle(reader["PURCHASE_VALUE"]) : 0f,
                                    USEFUL_LIFE = reader["USEFUL_LIFE"] != DBNull.Value ? Convert.ToInt32(reader["USEFUL_LIFE"]) : 0,
                                    RESIDUAL_VALUE = reader["RESIDUAL_VALUE"] != DBNull.Value ? Convert.ToDecimal(reader["RESIDUAL_VALUE"]) : 0m,                                   
                                    PURCH_DATE = reader["PURCHASE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCHASE_DATE"]).ToString("dd/MM/yyyy") : null,
                                    DEPR_PERCENT = reader["DEPR_PERCENT"] != DBNull.Value ? Convert.ToSingle(reader["DEPR_PERCENT"]) : 0f,
                                    LAST_DEPR_DATE = reader["LAST_DEPR_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["LAST_DEPR_DATE"]).ToString("dd/MM/yyyy") : null,
                                    NET_DEPRECIATION = reader["NET_DEPRECIATION"] != DBNull.Value ? Convert.ToSingle(reader["NET_DEPRECIATION"]) : 0f,
                                    CURRENT_VALUE = reader["CURRENT_VALUE"] != DBNull.Value ? Convert.ToSingle(reader["CURRENT_VALUE"]) : 0f,
                                });
                            }
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
    }
}
