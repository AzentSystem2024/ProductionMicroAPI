using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class FixedAssetRegReportService : IFixedAssetRegReportService
    {
        public FixedAssetReportResponse GetFixedAssetReport(FixedAssetReportRequest request)
        {
            FixedAssetReportResponse response = new FixedAssetReportResponse
            {
                FixedAssetDetails = new List<FixedAssetRegReport>()
            };
            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_FIXED_ASSET_REGISTER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DEPARTMENT_ID );
                    //cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.FixedAssetDetails.Add(new FixedAssetRegReport
                            {
                                CODE = reader["CODE"]?.ToString(),
                                ASSET_NAME = reader["ASSET_NAME"]?.ToString(),
                                STORE_CODE = reader["STORECODE"]?.ToString(),
                                STORE_NAME = reader["STORE_NAME"]?.ToString(),
                                LOCATION = reader["LOCATION"]?.ToString(),
                                ASSET_TYPE_ID = Convert.ToInt32(reader["ASSET_TYPE_ID"]),
                                TRANS_DATE = Convert.ToDateTime(reader["TRANS_DATE"]),
                                PURCH_VALUE = Convert.ToDecimal(reader["PURCH_VALUE"]),
                                USEFUL_LIFE = reader["USEFUL_LIFE"]?.ToString(),
                                NET_DEPRECIATION = Convert.ToDecimal(reader["NET_DEPRECIATION"]),
                                CURRENT_ASSETVALUE = Convert.ToDecimal(reader["CURRENT_ASSETVALUE"])
                            });
                        }
                    }
                }
            }

            response.Flag = (response.FixedAssetDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
    }
}
