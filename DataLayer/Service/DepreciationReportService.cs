using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class DepreciationReportService : IDepreciationReportService
    {
    public DepreciationReportResponse GetDepreciationReport(DepreciationReportRequest request)
        {
            DepreciationReportResponse response = new DepreciationReportResponse
            {
                DepreciationDetails = new List<DepreciationReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_DEPRECIATION", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DEPARTMENT_ID );

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.DepreciationDetails.Add(new DepreciationReport
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                CODE = reader["CODE"]?.ToString(),
                                DESCRIPTION = reader["DESCRIPTION"]?.ToString(),
                                ASSET_TYPE = reader["ASSET_TYPE"]?.ToString(),
                                PURCH_DATE = Convert.ToDateTime(reader["PURCH_DATE"]),
                                STORE_NAME = reader["STORE_NAME"]?.ToString(),
                                LOCATION = reader["LOCATION"]?.ToString(),
                                ASSET_VALUE = Convert.ToDecimal(reader["ASSET_VALUE"]),
                                USEFUL_LIFE = Convert.ToInt32(reader["USEFUL_LIFE"]),
                                OPENING_DEPR = Convert.ToDecimal(reader["OPENING_DEPR"]),
                                DURING_DEPR = Convert.ToDecimal(reader["DURING_DEPR"]),
                                CLOSING_DEPR = Convert.ToDecimal(reader["CLOSING_DEPR"]),
                                CURRENT_VALUE = Convert.ToDecimal(reader["CURRENT_VALUE"])
                            });
                        }
                    }
                }
            }

            response.Flag = (response.DepreciationDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
}
}
