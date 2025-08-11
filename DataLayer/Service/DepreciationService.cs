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
        public DepreciationListResponse GetList()
        {
            DepreciationListResponse response = new DepreciationListResponse { Data = new List<DepreciationList>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_DEPRECIATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new DepreciationList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    DOC_NO = reader["DOC_NO"] != DBNull.Value ? reader["DOC_NO"].ToString() : null,
                                    DEPR_DATE = reader["DEPR_DATE"] != DBNull.Value ? reader["DEPR_DATE"].ToString() : null,
                                    NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null,
                                    AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0m,
                                    VOUCHER_NO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null,
                                    TRANS_STATUS = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : null
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

        public DepreciationResponse InsertDepreciation(DepreciationInsertRequest request)
        {
            DepreciationResponse response = new DepreciationResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_DEPRECIATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@DEPR_DATE", ParseDate(request.DEPR_DATE));
                        cmd.Parameters.AddWithValue("@NARRATION", request.NARRATION);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);

                        // Create a DataTable for the Table-Valued Parameter
                        DataTable assetIdsTable = new DataTable();
                        assetIdsTable.Columns.Add("AssetID", typeof(int));

                        // Add the asset IDs to the DataTable
                        foreach (var assetId in request.ASSET_IDS)
                        {
                            assetIdsTable.Rows.Add(assetId);
                        }

                        // Add the Table-Valued Parameter to the command
                        SqlParameter assetIdsParam = new SqlParameter("@ASSET_IDS", SqlDbType.Structured);
                        assetIdsParam.TypeName = "dbo.UDT_ASSETID_LIST";
                        assetIdsParam.Value = assetIdsTable;
                        cmd.Parameters.Add(assetIdsParam);

                        SqlParameter transIdParam = new SqlParameter("@TRANS_ID", SqlDbType.BigInt)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(transIdParam);

                        cmd.ExecuteNonQuery();

                        response.Flag = 1;
                        response.Message = "Depreciation inserted successfully.";
                    }
                }
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
    

