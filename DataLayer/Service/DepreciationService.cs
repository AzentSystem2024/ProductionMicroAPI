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

        public int InsertDepreciation(DepreciationInsertRequest request)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                //// Check for open records before proceeding
                //using (SqlCommand checkCmd = new SqlCommand(
                //    "SELECT 1 FROM TB_AC_TRANS_HEADER th JOIN TB_AC_DEPRECIATION_HEADER dh ON th.TRANS_ID = dh.TRANS_ID " +
                //    " WHERE th.TRANS_TYPE = 9   AND th.TRANS_STATUS != 5   AND th.COMPANY_ID = @COMPANY_ID", connection))
                //{
                //    var exists = checkCmd.ExecuteScalar();
                //    if (exists != null)
                //    {
                //        throw new Exception("Cannot insert: An open depreciation record already exists.");
                //    }
                //}

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Prepare DataTable for TVP
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("Asset_ID", typeof(int));
                        tvp.Columns.Add("Days", typeof(int));
                        tvp.Columns.Add("Depr_Amount", typeof(float));

                        foreach (var detail in request.ASSET_IDS)
                        {
                            tvp.Rows.Add(detail.Asset_ID, detail.Days, detail.Depr_Amount);
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_DEPRECIATION", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 2);
                            cmd.Parameters.AddWithValue("@DEPR_DATE", request.DEPR_DATE ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@NARRATION", request.NARRATION ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@AMOUNT", request.AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@LAST_DEPR_DATE", request.LAST_DEPR_DATE ?? (object)DBNull.Value);

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ASSET_IDS", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_ASSETID_LIST";

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return request.COMPANY_ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving data: " + ex.Message);
                    }
                }
            }
        }
        public int UpdateDepreciation(DepreciationUpdateRequest request)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Prepare DataTable for TVP
                        DataTable tvp = new DataTable();
                        tvp.Columns.Add("Asset_ID", typeof(int));
                        tvp.Columns.Add("Days", typeof(int));
                        tvp.Columns.Add("Depr_Amount", typeof(float));

                        foreach (var detail in request.ASSET_IDS)
                        {
                            tvp.Rows.Add(detail.Asset_ID, detail.Days, detail.Depr_Amount);
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_DEPRECIATION", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", 3);
                            cmd.Parameters.AddWithValue("@ID", request.ID);
                            cmd.Parameters.AddWithValue("@TRANS_ID", request.TRANS_ID);
                            cmd.Parameters.AddWithValue("@DEPR_DATE", request.DEPR_DATE ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@NARRATION", request.NARRATION ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@AMOUNT", request.AMOUNT ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@LAST_DEPR_DATE", request.LAST_DEPR_DATE ?? (object)DBNull.Value);

                            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ASSET_IDS", tvp);
                            tvpParam.SqlDbType = SqlDbType.Structured;
                            tvpParam.TypeName = "dbo.UDT_ASSETID_LIST";

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return request.ID ?? 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error updating data: " + ex.Message);
                    }
                }
            }
        }
        public int ApproveDepreciation(DepreciationApproveRequest request)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_DEPRECIATION"
                };

                cmd.Parameters.AddWithValue("@ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", request.ID);
                cmd.Parameters.AddWithValue("@TRANS_ID", request.TRANS_ID);
                cmd.Parameters.AddWithValue("@DEPR_DATE", string.IsNullOrEmpty(request.DEPR_DATE) ? (object)DBNull.Value : request.DEPR_DATE);
                //cmd.Parameters.AddWithValue("@LAST_DEPR_DATE", string.IsNullOrEmpty(request.LAST_DEPR_DATE) ? (object)DBNull.Value : request.LAST_DEPR_DATE);
                cmd.Parameters.AddWithValue("@NARRATION", string.IsNullOrEmpty(request.NARRATION) ? (object)DBNull.Value : request.NARRATION);
                cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AMOUNT", request.AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@NET_DEPRECIATION", request.NET_DEPRECIATION ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@CURRENT_VALUE", request.CURRENT_VALUE ?? (object)DBNull.Value);

                // Prepare DataTable for TVP
                DataTable tvp = new DataTable();
                tvp.Columns.Add("Asset_ID", typeof(int));
                tvp.Columns.Add("Days", typeof(int));
                tvp.Columns.Add("Depr_Amount", typeof(float));

                foreach (var detail in request.ASSET_IDS)
                {
                    tvp.Rows.Add(detail.Asset_ID, detail.Days, detail.Depr_Amount);
                }

                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ASSET_IDS", tvp);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.UDT_ASSETID_LIST";

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error approving data: " + ex.Message);
                }
            }
        }

        public DepreciationDetailsResponse GetDepreciationById(int id)
        {
            DepreciationDetailsResponse response = new DepreciationDetailsResponse
            {
                Data = new DepreciationDetails
                {
                    ASSET_IDS = new List<AssetDepreciationDetail>()
                }
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_DEPRECIATION", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 5); 
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read header information
                            if (reader.Read())
                            {
                                response.Data = new DepreciationDetails
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    DOC_NO = reader["DOC_NO"] != DBNull.Value ? reader["DOC_NO"].ToString() : null,
                                    DEPR_DATE = reader["DEPR_DATE"] != DBNull.Value ? reader["DEPR_DATE"].ToString() : null,
                                    NARRATION = reader["NARRATION"] != DBNull.Value ? reader["NARRATION"].ToString() : null,
                                    AMOUNT = reader["NET_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["NET_AMOUNT"]) : 0m,
                                    VOUCHER_NO = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : null,
                                    TRANS_STATUS = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : null,
                                    TRANS_ID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    LAST_DEPR_DATE = reader["LAST_DEPR_DATE"] != DBNull.Value ? reader["LAST_DEPR_DATE"].ToString(): null,
                                    ASSET_IDS = new List<AssetDepreciationDetail>()
                                };
                            }

                            // Move to the next result set for asset details
                            reader.NextResult();

                            // Read asset details
                            while (reader.Read())
                            {
                                response.Data.ASSET_IDS.Add(new AssetDepreciationDetail
                                {
                                    Asset_ID = reader["Asset_ID"] != DBNull.Value ? Convert.ToInt32(reader["Asset_ID"]) : 0,
                                    Days = reader["Days"] != DBNull.Value ? Convert.ToInt32(reader["Days"]) : 0,
                                    Depr_Amount = reader["Depr_Amount"] != DBNull.Value ? Convert.ToSingle(reader["Depr_Amount"]) : 0f
                                });
                            }
                        }
                    }
                    response.Flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
        public int DeleteDepreciation(int id)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_DEPRECIATION"
                };
                cmd.Parameters.AddWithValue("@ACTION", 6); 
                cmd.Parameters.AddWithValue("@ID", id);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    return 1; 
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting data: " + ex.Message);
                }
            }
        }

    }
}


    

    

