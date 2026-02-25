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
