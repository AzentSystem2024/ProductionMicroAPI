using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class JournalBookService : IJournalBookService
    {
        public List<JournalBook> GetJournalBookData(int companyId, int finId, DateTime dateFrom, DateTime dateTo)
        {
            List<JournalBook> journalBookList = new List<JournalBook>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_RPT_JOURNAL_BOOK", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);
                        cmd.Parameters.AddWithValue("@FIN_ID", finId);
                        cmd.Parameters.AddWithValue("@DATE_FROM", dateFrom.Date);
                        cmd.Parameters.AddWithValue("@DATE_TO", dateTo.Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                journalBookList.Add(new JournalBook
                                {
                                    TransID = reader["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_ID"]) : 0,
                                    TransType = reader["TRANS_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["TRANS_TYPE"]) : 0, 
                                    Date = reader["TRANS_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["TRANS_DATE"]).Date : DateTime.MinValue,
                                    DocumentType = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : string.Empty,
                                    VoucherNo = reader["VOUCHER_NO"] != DBNull.Value ? reader["VOUCHER_NO"].ToString() : string.Empty,
                                    Particulars = reader["OPP_HEAD_NAME"] != DBNull.Value ? reader["OPP_HEAD_NAME"].ToString() : string.Empty,
                                    Remarks = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : string.Empty,
                                    DebitAmount = reader[7] != DBNull.Value ? Convert.ToDecimal(reader[7]) : 0, 
                                    CreditAmount = reader[8] != DBNull.Value ? Convert.ToDecimal(reader[8]) : 0 
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
                throw new Exception("An error occurred while fetching the journal book data. See logs for details.", ex);
            }

            return journalBookList;
        }
    }
}
