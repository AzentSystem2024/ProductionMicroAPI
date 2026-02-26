using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ImportChartOfAccountsService:IImportChartOfAccountsService
    {

        public bool Import(ImportAccountsInput vInput)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                // Create DataTables for each category
                DataTable tblAccounts = CreateChartOfAccountsTable(vInput);
        

                // SqlCommand setup
                SqlCommand cmd = new SqlCommand

                {
                    Connection = connection,
                    Transaction = objtrans,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_IMPORT_CHART_OF_ACCOUNTS",
                    CommandTimeout = 0
                };

                cmd.Parameters.AddWithValue("@CompanyID", vInput.CompanyID);
                cmd.Parameters.AddWithValue("@UserID", vInput.UserID);
                cmd.Parameters.AddWithValue("@BatchNo", vInput.BatchNo);
                cmd.Parameters.AddWithValue("@Action", vInput.Action);

                cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_LOG_ACCOUNTS", tblAccounts);


                cmd.ExecuteNonQuery();
                objtrans.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }


        public ImportAccountsResponse List(ImportAccountsInput vInput)
        {
            ImportAccountsResponse res = new ImportAccountsResponse();
            res.data = new List<ImportAccountsLog>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                                        SELECT L.*, 
                                               C.COMPANY_NAME, 
                                               U.USER_NAME
                                        FROM TB_IMPORT_ACCOUNTS_LOG L
                                        INNER JOIN TB_COMPANY_MASTER C 
                                            ON C.ID = L.COMPANY_ID
                                        INNER JOIN TB_USERS U 
                                            ON U.USER_ID = L.IMPORTED_USER_ID
                                        WHERE L.COMPANY_ID = @CompanyID
                                        ORDER BY L.IMPORTED_TIME DESC";

                        cmd.Parameters.AddWithValue("@CompanyID", vInput.CompanyID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new ImportAccountsLog
                                {
                                    ID = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                                    DocNo = reader["DOC_NO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DOC_NO"]),
                                    ImportedBy = reader["USER_NAME"]?.ToString() ?? "",
                                    ImportedTime = reader["IMPORTED_TIME"] == DBNull.Value
                                                    ? DateTime.MinValue
                                                    : Convert.ToDateTime(reader["IMPORTED_TIME"])
                                });
                            }
                        }
                    }
                }

                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
            }

            return res;
        }


        public viewImportAccountsDataResponse ViewDetails(viewImportAccountsInput vInput)
        {
            viewImportAccountsDataResponse res = new viewImportAccountsDataResponse();
            res.data = new List<ImportChartOfAccountsData>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT * FROM TB_IMPORT_LOG_ACCOUNTS_DETAILS WHERE LogID = @LogID";
                                        

                        cmd.Parameters.AddWithValue("@LogID", vInput.LogID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new ImportChartOfAccountsData
                                {
                                    ID = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                                    LogID = reader["LogID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LogID"]),
                                    MainGroup = reader["MainGroup"]?.ToString() ?? "",
                                    SubGroup = reader["SubGroup"]?.ToString() ?? "",
                                    Category = reader["Category"]?.ToString() ?? "",
                                    LedgerCode = reader["LedgerCode"]?.ToString() ?? "",
                                    LedgerName = reader["LedgerName"]?.ToString() ?? "",
                                    CostType = reader["CostType"]?.ToString() ?? "",

                                });
                            }
                        }
                    }
                }

                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
            }

            return res;
        }

        private DataTable CreateChartOfAccountsTable(ImportAccountsInput items)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("MainGroup", typeof(string));
            tbl.Columns.Add("SubGroup", typeof(string));
            tbl.Columns.Add("Category", typeof(string));
            tbl.Columns.Add("LedgerCode", typeof(string));
            tbl.Columns.Add("LedgerName", typeof(string));
            tbl.Columns.Add("CostType", typeof(string));


            items.data?.ForEach(ur => tbl.Rows.Add(
                ur.MainGroup, ur.SubGroup, ur.Category, ur.LedgerCode, ur.LedgerName, ur.CostType
            ));

            tbl.AcceptChanges();
            return tbl;
        }
    }
}
