using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace MicroApi.DataLayer.Service
{
    public class TransferInService:ITransferInService
    {
    public List<TransferInItemList> GetTransferInItems(TransferInInput input)
        {
            List<TransferInItemList> items = new List<TransferInItemList>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_IN", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parameters
                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                items = tbl.AsEnumerable().Select(dr => new TransferInItemList
                {
                    ITEM_ID = ADO.ToInt32(dr["ID"]),
                    BARCODE = ADO.ToString(dr["BARCODE"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    UOM = ADO.ToString(dr["UOM"]),
                    COST = dr["UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDouble(dr["UNIT_COST"]),
                    QUANTITY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_AVAILABLE"]),
                    QUANTITY_ISSUED = dr["QTY_ISSUED"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_ISSUED"])
                }).ToList();
            }

            return items;
        }

        public Int32 Insert(TransferIn transfeIn)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("TROUT_DETAIL_ID", typeof(Int32));
                tbl.Columns.Add("ORIGIN_STORE_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("ISSUE_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                if (transfeIn.TransferInDetail != null && transfeIn.TransferInDetail.Any())
                {
                    foreach (TransferInDetails ur in transfeIn.TransferInDetail)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["TROUT_DETAIL_ID"] = ur.TROUT_DETAIL_ID;
                        dRow["ORIGIN_STORE_ID"] = ur.ORIGIN_STORE_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["UOM"] = ur.UOM;
                        dRow["COST"] = ur.COST;
                        dRow["ISSUE_QTY"] = ur.ISSUE_QTY;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["BATCH_NO"] = string.IsNullOrEmpty(ur.BATCH_NO) ? string.Empty : ur.BATCH_NO;
                        dRow["EXPIRY_DATE"] = ur.EXPIRY_DATE ?? new DateTime(1753, 1, 1);

                        tbl.Rows.Add(dRow);
                    }
                }
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TRANSFER_IN";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@COMPANY_ID", transfeIn.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", transfeIn.STORE_ID);
                cmd.Parameters.AddWithValue("@TRIN_DATE", transfeIn.TRIN_DATE);
                cmd.Parameters.AddWithValue("@TROUT_ID", transfeIn.TROUT_ID);
                cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", transfeIn.ORIGIN_STORE_ID);
                cmd.Parameters.AddWithValue("@NARRATION", transfeIn.NARRATION);
                cmd.Parameters.AddWithValue("@USER_ID", transfeIn.USER_ID);
                cmd.Parameters.AddWithValue("@UDT_TB_TRANSFER_IN", tbl);

                cmd.ExecuteNonQuery();
                objtrans.Commit();

                return 0;
            }
            catch (Exception ex)
            {
                // Rollback the transaction if an error occurs
                objtrans.Rollback();
                throw;
            }
            finally
            {
                // Close the connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public Int32 Update(TransferIn transfeIn)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("TROUT_DETAIL_ID", typeof(Int32));
                tbl.Columns.Add("ORIGIN_STORE_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("ISSUE_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                if (transfeIn.TransferInDetail != null && transfeIn.TransferInDetail.Any())
                {
                    foreach (TransferInDetails ur in transfeIn.TransferInDetail)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["TROUT_DETAIL_ID"] = ur.TROUT_DETAIL_ID;
                        dRow["ORIGIN_STORE_ID"] = ur.ORIGIN_STORE_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["UOM"] = ur.UOM;
                        dRow["COST"] = ur.COST;
                        dRow["ISSUE_QTY"] = ur.ISSUE_QTY;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["BATCH_NO"] = string.IsNullOrEmpty(ur.BATCH_NO) ? string.Empty : ur.BATCH_NO;
                        dRow["EXPIRY_DATE"] = ur.EXPIRY_DATE ?? new DateTime(1753, 1, 1);

                        tbl.Rows.Add(dRow);
                    }
                }
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TRANSFER_IN";

                cmd.Parameters.AddWithValue("ACTION", 2);

                cmd.Parameters.AddWithValue("@ID", transfeIn.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", transfeIn.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", transfeIn.STORE_ID);
                cmd.Parameters.AddWithValue("@TRIN_DATE", transfeIn.TRIN_DATE);
                cmd.Parameters.AddWithValue("@TROUT_ID", transfeIn.TROUT_ID);
                cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", transfeIn.ORIGIN_STORE_ID);
                cmd.Parameters.AddWithValue("@NARRATION", transfeIn.NARRATION);
                cmd.Parameters.AddWithValue("@USER_ID", transfeIn.USER_ID);
                cmd.Parameters.AddWithValue("@UDT_TB_TRANSFER_IN", tbl);

                cmd.ExecuteNonQuery();
                objtrans.Commit();

                return 0;
            }
            catch (Exception ex)
            {
                // Rollback the transaction if an error occurs
                objtrans.Rollback();
                throw;
            }
            finally
            {
                // Close the connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
     
        public bool Delete(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TRANSFER_IN";
                cmd.Parameters.AddWithValue("ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
