using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace MicroApi.DataLayer.Service
{
    public class TransferOutInvService: ITransferOutInvService
    {
        public Int32 Insert(TransferOutInv transferOut)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("QUANTITY", typeof(double));
                tbl.Columns.Add("COST", typeof(double));
                tbl.Columns.Add("AMOUNT", typeof(double));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                if (transferOut.DETAILS != null && transferOut.DETAILS.Any())
                {
                    foreach (TransferOutDetail ur in transferOut.DETAILS)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["UOM"] = ur.UOM;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["COST"] = ur.COST;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["BATCH_NO"] = (object?)ur.BATCH_NO ?? DBNull.Value;
                        dRow["EXPIRY_DATE"] = (object?)ur.EXPIRY_DATE ?? DBNull.Value;
                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TRANSFER_OUT";

                cmd.Parameters.AddWithValue("@ACTION", 1);
                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@COMPANY_ID", transferOut.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", transferOut.STORE_ID);
                cmd.Parameters.AddWithValue("@TRANSFER_DATE", (object?)transferOut.TRANSFER_DATE ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DEST_STORE_ID", transferOut.DEST_STORE_ID);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", transferOut.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@FIN_ID", transferOut.FIN_ID);
                cmd.Parameters.AddWithValue("@USER_ID", transferOut.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", (object?)transferOut.NARRATION ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@REASON_ID", (object?)transferOut.REASON_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UDT_TB_TRANSFER_DETAIL", tbl);

                object result = cmd.ExecuteScalar();
                Int32 newId = ADO.ToInt32(result);

                objtrans.Commit();
                return newId;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public Int32 Update(TransferOutInvUpdate transferOut)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    // Build DataTable for detail updates
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));
                    tbl.Columns.Add("COST", typeof(double));
                    tbl.Columns.Add("AMOUNT", typeof(double));
                    tbl.Columns.Add("BATCH_NO", typeof(string));
                    tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                    if (transferOut.DETAILS != null && transferOut.DETAILS.Any())
                    {
                        foreach (TransferOutDetailUpdate ur in transferOut.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["ID"] = ur.ID;
                            dRow["ITEM_ID"] = ur.ITEM_ID ?? (object)DBNull.Value;
                            dRow["UOM"] = (object?)ur.UOM ?? DBNull.Value;
                            dRow["QUANTITY"] = ur.QUANTITY ?? 0;
                            dRow["COST"] = ur.COST ?? 0;
                            dRow["AMOUNT"] = ur.AMOUNT ?? 0;
                            dRow["BATCH_NO"] = (object?)ur.BATCH_NO ?? DBNull.Value;
                            dRow["EXPIRY_DATE"] = (object?)ur.EXPIRY_DATE ?? DBNull.Value;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_OUT", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 2); 
                    cmd.Parameters.AddWithValue("@ID", transferOut.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", (object?)transferOut.COMPANY_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object?)transferOut.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRANSFER_DATE", (object?)transferOut.TRANSFER_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEST_STORE_ID", (object?)transferOut.DEST_STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", (object?)transferOut.NET_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FIN_ID", (object?)transferOut.FIN_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@USER_ID", (object?)transferOut.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)transferOut.NARRATION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object?)transferOut.REASON_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UDT_TB_TRANSFER_DETAIL", tbl);

                    object result = cmd.ExecuteScalar();
                    Int32 updatedId = ADO.ToInt32(result);

                    objtrans.Commit();
                    return updatedId;
                }
                catch (Exception ex)
                {
                    objtrans.Rollback();
                    throw;
                }
            }
        }

        public List<ItemInfo> GetItemInfo(ItemRequest request)
        {
            List<ItemInfo> items = new List<ItemInfo>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_OUT", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@STORE_ID", (object)request.STORE_ID ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    items.Add(new ItemInfo
                    {
                        BARCODE = ADO.ToString(dr["BARCODE"]),
                        DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                        UOM = ADO.ToString(dr["UOM"]),
                        UNIT_COST = dr["UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDouble(dr["UNIT_COST"]),
                        QTY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_AVAILABLE"])
                    });
                }
            }
            return items;
        }


    }
}
