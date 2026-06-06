using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class TrialBalanceReportService : ITrialBalanceReportService
    {
        public List<TrialBalanceReport> GetTrialBalanceReport(int companyId, int finId, DateTime dateFrom, DateTime dateTo)
        {
            List<TrialBalanceReport> reportList = new List<TrialBalanceReport>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_RPT_TRIAL_BALANCE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);
                        cmd.Parameters.AddWithValue("@FIN_ID", finId);
                        cmd.Parameters.AddWithValue("@DATE_FROM", dateFrom);
                        cmd.Parameters.AddWithValue("@DATE_TO", dateTo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportList.Add(new TrialBalanceReport
                                {
                                    HeadID = reader["HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["HEAD_ID"]) : 0,
                                    MainGroup = reader["MAIN_GROUP_NAME"] != DBNull.Value ? reader["MAIN_GROUP_NAME"].ToString() : string.Empty,
                                    SubGroup = reader["SUB_GROUP_NAME"] != DBNull.Value ? reader["SUB_GROUP_NAME"].ToString() : string.Empty,
                                    Category = reader["CATEGORY_NAME"] != DBNull.Value ? reader["CATEGORY_NAME"].ToString() : string.Empty,
                                    LedgerCode = reader["HEAD_CODE"] != DBNull.Value ? reader["HEAD_CODE"].ToString() : string.Empty,
                                    LedgerName = reader["HEAD_NAME"] != DBNull.Value ? reader["HEAD_NAME"].ToString() : string.Empty,
                                    DEPT_NAME = reader["DEPT_NAME"] != DBNull.Value ? reader["DEPT_NAME"].ToString() : string.Empty,
                                    OpeningBalanceDebit = reader["OB_DR"] != DBNull.Value ? Convert.ToDecimal(reader["OB_DR"]) : 0,
                                    OpeningBalanceCredit = reader["OB_CR"] != DBNull.Value ? Convert.ToDecimal(reader["OB_CR"]) : 0,
                                    DuringThePeriodDebit = reader["TR_DR"] != DBNull.Value ? Convert.ToDecimal(reader["TR_DR"]) : 0,
                                    DuringThePeriodCredit = reader["TR_CR"] != DBNull.Value ? Convert.ToDecimal(reader["TR_CR"]) : 0,
                                    ClosingBalanceDebit = reader["CL_DR"] != DBNull.Value ? Convert.ToDecimal(reader["CL_DR"]) : 0,
                                    ClosingBalanceCredit = reader["CL_CR"] != DBNull.Value ? Convert.ToDecimal(reader["CL_CR"]) : 0,
                                    STORE_NAME = reader["STORE_NAME"] != DBNull.Value ? reader["STORE_NAME"].ToString() : string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception("An error occurred while fetching the trial balance report. See logs for details.", ex);
            }

            return reportList;
        }
        public List<TrialBalanceDimensionReport> GetTrialBalanceDimensionReport(
    int companyId,
    int finId,
    DateTime dateFrom,
    DateTime dateTo,
    string dimensionCode
)
        {
            List<TrialBalanceDimensionReport> reportList =
                new List<TrialBalanceDimensionReport>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand(
                        "SP_RPT_TRIAL_BALANCE_DIMENSION",
                        connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);
                        cmd.Parameters.AddWithValue("@FIN_ID", finId);
                        cmd.Parameters.AddWithValue("@DATE_FROM", dateFrom);
                        cmd.Parameters.AddWithValue("@DATE_TO", dateTo);

                        cmd.Parameters.AddWithValue(
                            "@DIMENSION_CODE",
                            dimensionCode
                        );

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportList.Add(
                                    new TrialBalanceDimensionReport
                                    {
                                        HeadID =
                                            reader["HEAD_ID"] != DBNull.Value
                                            ? Convert.ToInt32(reader["HEAD_ID"])
                                            : 0,

                                        MainGroup =
                                            reader["MAIN_GROUP_NAME"]?.ToString(),

                                        SubGroup =
                                            reader["SUB_GROUP_NAME"]?.ToString(),

                                        Category =
                                            reader["CATEGORY_NAME"]?.ToString(),

                                        LedgerCode =
                                            reader["HEAD_CODE"]?.ToString(),

                                        LedgerName =
                                            reader["HEAD_NAME"]?.ToString(),


                                        Code =
    reader["CODE"]?.ToString(),

                                        Description =
    reader["DESCRIPTION"]?.ToString(),

                                        OpeningBalanceDebit =
                                            reader["OB_DR"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["OB_DR"])
                                            : 0,

                                        OpeningBalanceCredit =
                                            reader["OB_CR"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["OB_CR"])
                                            : 0,

                                        DuringThePeriodDebit =
                                            reader["TR_DR"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["TR_DR"])
                                            : 0,

                                        DuringThePeriodCredit =
                                            reader["TR_CR"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["TR_CR"])
                                            : 0,

                                        ClosingBalanceDebit =
                                            reader["CL_DR"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["CL_DR"])
                                            : 0,

                                        ClosingBalanceCredit =
                                            reader["CL_CR"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["CL_CR"])
                                            : 0
                                    });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while fetching dimension trial balance.",
                    ex
                );
            }

            return reportList;
        }
        public List<TrialBalanceAsOnDate> GetTrialBalanceAsOnDate(
       int companyId,
       int finId,
       DateTime dateTo
   )
        {
            List<TrialBalanceAsOnDate> reportList =
                new List<TrialBalanceAsOnDate>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand(
                        "SP_RPT_TRIAL_BALANCE_STOREWISE",
                        connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);
                        cmd.Parameters.AddWithValue("@FIN_ID", finId);
                        cmd.Parameters.AddWithValue("@DATE_TO", dateTo);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow dr in dt.Rows)
                            {
                                TrialBalanceAsOnDate obj =
                                    new TrialBalanceAsOnDate();

                                obj.HeadID =
                                    dr["HEAD_ID"] != DBNull.Value
                                    ? Convert.ToInt32(dr["HEAD_ID"])
                                    : 0;

                                obj.MainGroup =
                                    dr["MAIN_GROUP_NAME"]?.ToString();

                                obj.SubGroup =
                                    dr["SUB_GROUP_NAME"]?.ToString();

                                obj.Category =
                                    dr["CATEGORY_NAME"]?.ToString();

                                obj.LedgerCode =
                                    dr["HEAD_CODE"]?.ToString();

                                obj.LedgerName =
                                    dr["HEAD_NAME"]?.ToString();

                                //-----------------------------------
                                // Dynamic Debit/Credit Store Columns
                                //-----------------------------------

                                foreach (DataColumn col in dt.Columns)
                                {
                                    string colName = col.ColumnName;

                                    if (colName.StartsWith("Debit-"))
                                    {
                                        decimal value = 0;

                                        if (dr[colName] != DBNull.Value)
                                            value = Convert.ToDecimal(dr[colName]);

                                        obj.Debit.Add(
                                            colName.Replace("Debit-", ""),
                                            value
                                        );
                                    }

                                    else if (colName.StartsWith("Credit-"))
                                    {
                                        decimal value = 0;

                                        if (dr[colName] != DBNull.Value)
                                            value = Convert.ToDecimal(dr[colName]);

                                        obj.Credit.Add(
                                            colName.Replace("Credit-", ""),
                                            value
                                        );
                                    }
                                }

                                reportList.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while fetching storewise trial balance.",
                    ex
                );
            }

            return reportList;
        }
    }
}
