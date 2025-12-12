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
                    QUANTITY_ISSUED = dr["QTY_ISSUED"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_ISSUED"]),
                    ISSUE_ID = ADO.ToInt32(dr["ISSUE_ID"]),
                    ISSUE_DETAIL_ID = ADO.ToInt32(dr["ISSUE_DETAIL_ID"]),
                }).ToList();
            }

            return items;
        }

        public Int32 Insert(TransferIn transferIn)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    // Build DataTable for UDT
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("ISSUE_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("COST", typeof(double));
                    tbl.Columns.Add("ISSUE_QTY", typeof(double));
                    tbl.Columns.Add("QUANTITY", typeof(double));
                    tbl.Columns.Add("BATCH_NO", typeof(string));
                    tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                    if (transferIn.DETAILS != null && transferIn.DETAILS.Any())
                    {
                        foreach (var d in transferIn.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["ISSUE_DETAIL_ID"] = d.ISSUE_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["UOM"] = d.UOM;
                            dRow["COST"] = d.COST;
                            dRow["ISSUE_QTY"] = d.QUANTITY_ISSUED;
                            dRow["QUANTITY"] = d.QUANTITY_RECEIVED;
                            dRow["BATCH_NO"] = (object?)d.BATCH_NO ?? DBNull.Value;
                            dRow["EXPIRY_DATE"] = (object?)d.EXPIRY_DATE ?? DBNull.Value;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_IN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@TRANS_ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", transferIn.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", transferIn.STORE_ID);
                    cmd.Parameters.AddWithValue("@REC_DATE", (object?)transferIn.REC_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", transferIn.ORIGIN_STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", transferIn.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", transferIn.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)transferIn.NARRATION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ISSUE_ID",transferIn.ISSUE_ID); 
                    cmd.Parameters.AddWithValue("@REASON_ID", transferIn.REASON_ID);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", transferIn.NET_AMOUNT);
                    cmd.Parameters.AddWithValue("@IS_APPROVED", transferIn.IS_APPROVED == true ? 1 : 0);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_TRANSFERINV_IN", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

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
                        connection.Close();
                }
            }
        }
        public Int32 Update(TransferInUpdate transferIn)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    // Build DataTable for UDT
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("ISSUE_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("COST", typeof(double));
                    tbl.Columns.Add("ISSUE_QTY", typeof(double));
                    tbl.Columns.Add("QUANTITY", typeof(double));
                    tbl.Columns.Add("BATCH_NO", typeof(string));
                    tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                    if (transferIn.DETAILS != null && transferIn.DETAILS.Any())
                    {
                        foreach (var d in transferIn.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["ISSUE_DETAIL_ID"] = d.ISSUE_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["UOM"] = d.UOM;
                            dRow["COST"] = d.COST;
                            dRow["ISSUE_QTY"] = d.QUANTITY_ISSUED;
                            dRow["QUANTITY"] = d.QUANTITY_RECEIVED;
                            dRow["BATCH_NO"] = (object?)d.BATCH_NO ?? DBNull.Value;
                            dRow["EXPIRY_DATE"] = (object?)d.EXPIRY_DATE ?? DBNull.Value;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_IN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 2); // UPDATE
                    cmd.Parameters.AddWithValue("@TRANS_ID", transferIn.TRANS_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", transferIn.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", transferIn.STORE_ID);
                    cmd.Parameters.AddWithValue("@REC_DATE", (object?)transferIn.REC_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", transferIn.ORIGIN_STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", transferIn.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", transferIn.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)transferIn.NARRATION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ISSUE_ID", transferIn.ISSUE_ID);
                    cmd.Parameters.AddWithValue("@REASON_ID", transferIn.REASON_ID);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", transferIn.NET_AMOUNT);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_TRANSFERINV_IN", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

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
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public TransferInListsResponse List()
        {
            TransferInListsResponse res = new TransferInListsResponse();
            List<TransferInList> list = new List<TransferInList>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_IN", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 0); 
                    cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        TransferInList obj = new TransferInList
                        {
                            TRANSFER_ID = ADO.ToInt32(dr["TRANSFER_ID"]),
                            TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]),
                            COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),
                            STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                            TRANSFER_DATE = dr["TRANSFER_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TRANSFER_DATE"]),
                            ORIGIN_STORE_ID = ADO.ToInt32(dr["ORIGIN_STORE_ID"]),
                            NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDouble(dr["NET_AMOUNT"]),
                            NARRATION = ADO.ToString(dr["NARRATION"]),
                            REASON_ID = ADO.ToInt32(dr["REASON_ID"]),
                            ISSUE_ID = ADO.ToInt32(dr["ISSUE_ID"]),
                            STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                            STATUS = ADO.ToString(dr["STATUS"]),
                            DOC_NO = ADO.ToInt32(dr["TRANSFER_NO"])

                        };
                        list.Add(obj);
                    }

                    res.Flag = 1;
                    res.Message = "Success";
                    res.data = list;
                }
                catch (Exception ex)
                {
                    res.Flag = 0;
                    res.Message = "Error: " + ex.Message;
                    res.data = new List<TransferInList>();
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return res;
        }
        public TransferInInvUpdate GetTransferIn(int id)
        {
            TransferInInvUpdate transfer = new TransferInInvUpdate();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_IN", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // Fetch by ID
                        cmd.Parameters.AddWithValue("@TRANS_ID", id);

                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);

                        if (tbl.Rows.Count > 0)
                        {
                            // Initialize header from first row
                            DataRow firstRow = tbl.Rows[0];
                            transfer = new TransferInInvUpdate
                            {
                                ID = ADO.ToInt32(firstRow["TRANSFER_ID"]),
                                TRANS_ID = ADO.ToInt32(firstRow["TRANS_ID"]),
                                COMPANY_ID = firstRow["COMPANY_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["COMPANY_ID"]),
                                STORE_ID = firstRow["STORE_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["STORE_ID"]),
                                TRANSFER_DATE = firstRow["TRANSFER_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(firstRow["TRANSFER_DATE"]),
                                ORIGIN_STORE_ID = firstRow["ORIGIN_STORE_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["ORIGIN_STORE_ID"]),
                                NET_AMOUNT = firstRow["NET_AMOUNT"] == DBNull.Value ? null : Convert.ToDouble(firstRow["NET_AMOUNT"]),
                                FIN_ID = firstRow["FIN_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["FIN_ID"]),
                                USER_ID = firstRow["USER_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["USER_ID"]),
                                NARRATION = firstRow["NARRATION"] == DBNull.Value ? null : ADO.ToString(firstRow["NARRATION"]),
                                REASON_ID = firstRow["REASON_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["REASON_ID"]),
                                DOC_NO = firstRow["TRANSFER_NO"] == DBNull.Value ? 0 : ADO.ToInt32(firstRow["TRANSFER_NO"]),
                                ISSUE_ID = ADO.ToInt32(firstRow["ISSUE_ID"]),
                                COMPANY_NAME = firstRow["COMPANY_NAME"]?.ToString(),
                                ADDRESS1 = firstRow["ADDRESS1"]?.ToString(),
                                ADDRESS2 = firstRow["ADDRESS2"]?.ToString(),
                                ADDRESS3 = firstRow["ADDRESS3"]?.ToString(),
                                COMPANY_CODE = firstRow["COMPANY_CODE"]?.ToString(),
                                EMAIL = firstRow["EMAIL"]?.ToString(),
                                PHONE = firstRow["PHONE"]?.ToString(),
                                STORE_CODE = firstRow["CODE"]?.ToString(),
                                STORE_ADDRESS1 = firstRow["STORE_ADDRESS1"]?.ToString(),
                                STORE_ADDRESS2 = firstRow["STORE_ADDRESS2"]?.ToString(),
                                STORE_ADDRESS3 = firstRow["STORE_ADDRESS3"]?.ToString(),
                                STORE_ZIP = firstRow["ZIP_CODE"]?.ToString(),
                                STORE_CITY = firstRow["CITY"]?.ToString(),
                                STORE_STATE = firstRow["STATE_NAME"]?.ToString(),
                                STORE_PHONE = firstRow["STORE_PHONE"]?.ToString(),
                                STORE_EMAIL = firstRow["STORE_EMAIL"]?.ToString(),
                                DETAILS = new List<TransferInDetailUpdate>()
                            };

                            // Loop through all rows to fill detail list
                            foreach (DataRow dr in tbl.Rows)
                            {
                                var detail = new TransferInDetailUpdate
                                {
                                    ID = dr["DETAIL_ID"] == DBNull.Value ? 0 : ADO.ToInt32(dr["DETAIL_ID"]),
                                    ITEM_ID = dr["ITEM_ID"] == DBNull.Value ? null : ADO.ToInt32(dr["ITEM_ID"]),
                                    UOM = dr["UOM"] == DBNull.Value ? null : ADO.ToString(dr["UOM"]),
                                    QUANTITY_ISSUED= dr["QTY_ISSUED"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_ISSUED"]),
                                    COST = dr["UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDouble(dr["UNIT_COST"]),
                                    BATCH_NO = dr["BATCH_NO"] == DBNull.Value ? null : ADO.ToString(dr["BATCH_NO"]),
                                    EXPIRY_DATE = dr["EXPIRY_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["EXPIRY_DATE"]),
                                    QUANTITY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_AVAILABLE"]),
                                    QUANTITY_RECEIVED = dr["QUANTITY"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QUANTITY"]),
                                    BARCODE = dr["BARCODE"] == DBNull.Value ? null : ADO.ToString(dr["BARCODE"]),
                                    DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? null : ADO.ToString(dr["DESCRIPTION"]),
                                    ISSUE_DETAIL_ID = ADO.ToInt32(dr["ISSUE_DETAIL_ID"]),

                                };
                                transfer.DETAILS.Add(detail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return transfer;
        }
        public TransferinDoc GetLastDocNo()
        {
            TransferinDoc res = new TransferinDoc();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 15
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.TRANSFER_NO = result != null ? Convert.ToInt32(result) : 0;
                        res.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
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
        public Int32 approve(TransferInUpdate transferIn)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    // Build DataTable for UDT
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("ISSUE_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("COST", typeof(double));
                    tbl.Columns.Add("ISSUE_QTY", typeof(double));
                    tbl.Columns.Add("QUANTITY", typeof(double));
                    tbl.Columns.Add("BATCH_NO", typeof(string));
                    tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));

                    if (transferIn.DETAILS != null && transferIn.DETAILS.Any())
                    {
                        foreach (var d in transferIn.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["ISSUE_DETAIL_ID"] = d.ISSUE_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["UOM"] = d.UOM;
                            dRow["COST"] = d.COST;
                            dRow["ISSUE_QTY"] = d.QUANTITY_ISSUED;
                            dRow["QUANTITY"] = d.QUANTITY_RECEIVED;
                            dRow["BATCH_NO"] = (object?)d.BATCH_NO ?? DBNull.Value;
                            dRow["EXPIRY_DATE"] = (object?)d.EXPIRY_DATE ?? DBNull.Value;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_IN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 5); // UPDATE
                    cmd.Parameters.AddWithValue("@TRANS_ID", transferIn.TRANS_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", transferIn.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", transferIn.STORE_ID);
                    cmd.Parameters.AddWithValue("@REC_DATE", (object?)transferIn.REC_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ORIGIN_STORE_ID", transferIn.ORIGIN_STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", transferIn.FIN_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", transferIn.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)transferIn.NARRATION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ISSUE_ID", transferIn.ISSUE_ID);
                    cmd.Parameters.AddWithValue("@REASON_ID", transferIn.REASON_ID);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", transferIn.NET_AMOUNT);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_TRANSFERINV_IN", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

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
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

    }
}
