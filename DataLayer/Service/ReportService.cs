using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ReportService : IReportService
    {
        public PDCListReportResponse GetPDCList(PDCListReportRequest request)
        {
            PDCListReportResponse response = new PDCListReportResponse
            {
                PDCDetails = new List<PDCListReport>()
            };
            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PDC_LIST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.PDCDetails.Add(new PDCListReport
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                CHEQUE_NO = reader["CHEQUE_NO"]?.ToString(),
                                CHEQUE_DATE = Convert.ToDateTime(reader["CHEQUE_DATE"]),
                                BENEFICIARY_NAME = reader["BENEFICIARY_NAME"]?.ToString(),
                                RECEIVED = Convert.ToDecimal(reader["RECEIVED"]),
                                PAID = Convert.ToDecimal(reader["PAID"]),
                                BANK = reader["BANK"]?.ToString(),
                                REMARKS = reader["REMARKS"]?.ToString(),
                                PDC_STATUS = reader["PDC_STATUS"]?.ToString(),
                                //TRANS_TYPE = reader["TRANS_TYPE"]?.ToString(),

                            });
                        }
                    }
                }
            }

            response.Flag = (response.PDCDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
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
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DEPARTMENT_ID);
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
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DEPARTMENT_ID);

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
        public PrepaymentReportResponse GetPrepaymentReport(PrepaymentReportRequest request)
        {
            PrepaymentReportResponse response = new PrepaymentReportResponse
            {
                PrepaymentDetails = new List<PrepaymentReport>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PREPAYMENT_REPORT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", request.DEPARTMENT_ID ?? "");
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.PrepaymentDetails.Add(new PrepaymentReport
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                INV_NO = reader["INV_NO"]?.ToString(),
                                SUPP_CODE = reader["SUPP_CODE"]?.ToString(),
                                SUPP_NAME = reader["SUPP_NAME"]?.ToString(),
                                DEPT_NAME = reader["DEPT_NAME"]?.ToString(),
                                EXPENSE_LEDGER = reader["EXPENSE_LEDGER"]?.ToString(),
                                GROUP_NAME = reader["GROUP_NAME"]?.ToString(),
                                PREPAYMENT_LEDGER = reader["PREPAYMENT_LEDGER"]?.ToString(),
                                TRANSACTON_DATE = Convert.ToDateTime(reader["TRANSACTON_DATE"]),
                                NO_OF_MONTHS = Convert.ToInt32(reader["NO_OF_MONTHS"]),
                                NO_OF_DAYS = Convert.ToInt32(reader["NO_OF_DAYS"]),
                                DATE_FROM = Convert.ToDateTime(reader["DATE_FROM"]),
                                DATE_TO = Convert.ToDateTime(reader["DATE_TO"]),
                                PURCH_VALUE = Convert.ToDecimal(reader["PURCH_VALUE"]),
                                OPENING_ACCUR = Convert.ToDecimal(reader["OPENING_ACCUR"]),
                                CURRENT_ACCUR = Convert.ToDecimal(reader["CURRENT_ACCUR"]),
                                TOTAL_ACCUR = Convert.ToDecimal(reader["TOTAL_ACCUR"]),
                                BALANCE_ACCUR = Convert.ToDecimal(reader["BALANCE_ACCUR"])
                            });
                        }
                    }
                }
            }

            response.Flag = (response.PrepaymentDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
        public ProfitLossBranchResponse GetProfitLossBranch(ProfitLossBranchRequest request)
        {
            ProfitLossBranchResponse response = new ProfitLossBranchResponse
            {
                ProfitLossDetails = new List<ProfitLossBranch>()
            };

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_PROFIT_LOSS_BRANCH", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);
                    cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM);
                    cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO);
                    cmd.Parameters.AddWithValue("@STORE_ID", request.STORE_ID ?? "");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ProfitLossBranch
                            {
                                TYPE_ID = Convert.ToInt32(reader["TYPE_ID"]),
                                TYPE_NAME = reader["TYPE_NAME"]?.ToString(),
                                GROUP_ID = Convert.ToInt32(reader["GROUP_ID"]),
                                GROUP_NAME = reader["GROUP_NAME"]?.ToString(),
                                CATEGORY_ID = Convert.ToInt32(reader["CATEGORY_ID"]),
                                CATEGORY_NAME = reader["CATEGORY_NAME"]?.ToString(),
                                HEAD_ID = Convert.ToInt32(reader["HEAD_ID"]),
                                HEAD_CODE = reader["HEAD_CODE"]?.ToString(),
                                HEAD_NAME = reader["HEAD_NAME"]?.ToString(),
                                StoreAmounts = new Dictionary<string, decimal?>()
                            };

                            // Handle dynamic columns (stores + TOTAL)
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);

                                // Skip fixed columns
                                if (!new[]
                                {
                            "TYPE_ID","TYPE_NAME","GROUP_ID","GROUP_NAME",
                            "CATEGORY_ID","CATEGORY_NAME","HEAD_ID",
                            "HEAD_CODE","HEAD_NAME"
                        }.Contains(columnName))
                                {
                                    item.StoreAmounts[columnName] =
                                        reader.IsDBNull(i) ? (decimal?)null : Convert.ToDecimal(reader.GetValue(i));
                                }
                            }

                            response.ProfitLossDetails.Add(item);
                        }
                    }
                }
            }

            response.Flag = (response.ProfitLossDetails.Count > 0) ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
    }
}

