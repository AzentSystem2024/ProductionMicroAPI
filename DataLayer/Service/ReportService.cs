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
               // conn.Open();

                Console.WriteLine("Database : " + conn.Database);

                using (SqlCommand cmd = new SqlCommand("SP_RPT_PROFIT_LOSS_BRANCH", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                    cmd.Parameters.Add("@FIN_ID", SqlDbType.Int).Value = request.FIN_ID;
                    cmd.Parameters.Add("@DATE_FROM", SqlDbType.DateTime).Value = request.DATE_FROM;
                    cmd.Parameters.Add("@DATE_TO", SqlDbType.DateTime).Value = request.DATE_TO;
                    cmd.Parameters.Add("@STORE_ID", SqlDbType.VarChar).Value = request.STORE_ID ?? "";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Print all column names returned by SQL
                        Console.WriteLine("Columns Returned:");
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine($"{i} : {reader.GetName(i)}");
                        }

                        while (reader.Read())
                        {
                            ProfitLossBranch item = new ProfitLossBranch
                            {
                                TYPE_ID = reader["TYPE_ID"] != DBNull.Value ? Convert.ToInt32(reader["TYPE_ID"]) : 0,
                                TYPE_NAME = reader["TYPE_NAME"]?.ToString(),
                                GROUP_ID = reader["GROUP_ID"] != DBNull.Value ? Convert.ToInt32(reader["GROUP_ID"]) : 0,
                                GROUP_NAME = reader["GROUP_NAME"]?.ToString(),
                                CATEGORY_ID = reader["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(reader["CATEGORY_ID"]) : 0,
                                CATEGORY_NAME = reader["CATEGORY_NAME"]?.ToString(),
                                HEAD_ID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                HEAD_CODE = reader["HEAD_CODE"]?.ToString(),
                                HEAD_NAME = reader["HEAD_NAME"]?.ToString(),
                                StoreAmounts = new Dictionary<string, decimal?>()
                            };

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string column = reader.GetName(i);

                                if (column == "TYPE_ID" ||
                                    column == "TYPE_NAME" ||
                                    column == "GROUP_ID" ||
                                    column == "GROUP_NAME" ||
                                    column == "CATEGORY_ID" ||
                                    column == "CATEGORY_NAME" ||
                                    column == "HEAD_ID" ||
                                    column == "HEAD_CODE" ||
                                    column == "HEAD_NAME" ||
                                    column == "BL_ORDER")
                                {
                                    continue;
                                }

                                decimal? value = null;

                                if (!reader.IsDBNull(i))
                                    value = Convert.ToDecimal(reader.GetValue(i));

                                item.StoreAmounts.Add(column, value);
                            }

                            response.ProfitLossDetails.Add(item);
                        }
                    }
                }
            }

            response.Flag = response.ProfitLossDetails.Any() ? 1 : 0;
            response.Message = response.Flag == 1 ? "Success" : "No records found";

            return response;
        }
        public List<TimesheetReportResponse> GetTimesheetReport(TimesheetReportRequest request)
        {
            List<TimesheetReportResponse> list = new List<TimesheetReportResponse>();

            using (SqlConnection conn = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_RPT_TIMESHEET", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = request.COMPANY_ID;
                    cmd.Parameters.Add("@DEPARTMENT_ID", SqlDbType.VarChar).Value = request.DEPARTMENT_ID ?? "";
                    cmd.Parameters.Add("@TS_MONTH", SqlDbType.DateTime).Value =
                        request.TS_MONTH.HasValue ? (object)request.TS_MONTH.Value : DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new TimesheetReportResponse
                            {
                                EMP_CODE = dr["EMP_CODE"]?.ToString(),
                                EMP_NAME = dr["EMP_NAME"]?.ToString(),
                                DEPT_NAME = dr["DEPT_NAME"]?.ToString(),
                                TS_MONTH = dr["TS_MONTH"] != DBNull.Value ? Convert.ToDateTime(dr["TS_MONTH"]) : (DateTime?)null,

                                TOTAL_DAYS = dr["TOTAL_DAYS"] != DBNull.Value ? Convert.ToDecimal(dr["TOTAL_DAYS"]) : 0,
                                WORKED_DAYS = dr["WORKED_DAYS"] != DBNull.Value ? Convert.ToDecimal(dr["WORKED_DAYS"]) : 0,
                                DEDUCT_DAYS = dr["DEDUCT_DAYS"] != DBNull.Value ? Convert.ToDecimal(dr["DEDUCT_DAYS"]) : 0,
                                ABSENTEES = dr["ABSENTEES"] != DBNull.Value ? Convert.ToDecimal(dr["ABSENTEES"]) : 0,

                                NORMAL_OT = dr["NORMAL_OT"] != DBNull.Value ? Convert.ToDecimal(dr["NORMAL_OT"]) : 0,
                                HOLIDAY_OT = dr["HOLIDAY_OT"] != DBNull.Value ? Convert.ToDecimal(dr["HOLIDAY_OT"]) : 0,

                                BASIC = dr["BASIC"] != DBNull.Value ? Convert.ToDecimal(dr["BASIC"]) : 0,
                                ALLOWANCE = dr["ALLOWANCE"] != DBNull.Value ? Convert.ToDecimal(dr["ALLOWANCE"]) : 0,
                                ADDITIONS = dr["ADDITIONS"] != DBNull.Value ? Convert.ToDecimal(dr["ADDITIONS"]) : 0,
                                DEDUCTIONS = dr["DEDUCTIONS"] != DBNull.Value ? Convert.ToDecimal(dr["DEDUCTIONS"]) : 0,

                                LEAVE_FROM = dr["LEAVE_FROM"] != DBNull.Value ? Convert.ToDateTime(dr["LEAVE_FROM"]) : (DateTime?)null,
                                LEAVE_TO = dr["LEAVE_TO"] != DBNull.Value ? Convert.ToDateTime(dr["LEAVE_TO"]) : (DateTime?)null,

                                REMARKS = dr["REMARKS"]?.ToString(),
                                STATUS_DESC = dr["STATUS_DESC"]?.ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }
    }
}

