﻿using MicroApi.DataLayer.Interface;
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
                        dRow["AMOUNT"] = 0;
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
                        foreach (TransferOutDetailUpdate ur in transferOut.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["ITEM_ID"] = ur.ITEM_ID ?? (object)DBNull.Value;
                            dRow["UOM"] = (object?)ur.UOM ?? DBNull.Value;
                            dRow["QUANTITY"] = ur.QUANTITY ?? 0;
                            dRow["COST"] = ur.COST ?? 0;
                            dRow["AMOUNT"] = 0;
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

                    // Pass DataTable as TVP
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_TRANSFER_DETAIL", tbl);
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
                        ID = ADO.ToInt32(dr["ID"]),
                        BARCODE = ADO.ToString(dr["BARCODE"]),
                        DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                        UOM = ADO.ToString(dr["UOM"]),
                        COST = dr["UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDouble(dr["UNIT_COST"]),
                        QUANTITY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_AVAILABLE"])
                    });
                }
            }
            return items;
        }
        public List<TransferOutDetailList> GetTransferOutList()
        {
            List<TransferOutDetailList> list = new List<TransferOutDetailList>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_OUT", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", DBNull.Value);// Action=0 to get list

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        var header = new TransferOutDetailList
                        {
                            ID = ADO.ToInt32(dr["TRANSFER_ID"]),
                            COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),
                            STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                            TRANSFER_DATE = dr["TRANSFER_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TRANSFER_DATE"]),
                            DEST_STORE_ID = ADO.ToInt32(dr["DEST_STORE_ID"]),
                            NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDouble(dr["NET_AMOUNT"]),
                            NARRATION = ADO.ToString(dr["NARRATION"]),
                            REASON_ID = dr["REASON_ID"] == DBNull.Value ? 0 : ADO.ToInt32(dr["REASON_ID"]),
                            ISSUE_NO = ADO.ToInt32(dr["ISSUE_NO"]),
                            STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                            STATUS = ADO.ToString(dr["STATUS"])

                            // No DETAILS list needed since only header info
                        };

                        list.Add(header);
                    }
                }
            }

            return list;
        }

      
        public TransferOutInvUpdate GetTransferOut(int id)
        {
            TransferOutInvUpdate transfer = new TransferOutInvUpdate();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_OUT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // Fetch by ID
                        cmd.Parameters.AddWithValue("@ID", id);

                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);

                        if (tbl.Rows.Count > 0)
                        {
                            // Initialize header object
                            DataRow firstRow = tbl.Rows[0];
                            transfer = new TransferOutInvUpdate
                            {
                                ID = ADO.ToInt32(firstRow["TRANSFER_ID"]),
                                COMPANY_ID = firstRow["COMPANY_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["COMPANY_ID"]),
                                STORE_ID = firstRow["STORE_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["STORE_ID"]),
                                TRANSFER_DATE = firstRow["TRANSFER_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(firstRow["TRANSFER_DATE"]),
                                DEST_STORE_ID = firstRow["DEST_STORE_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["DEST_STORE_ID"]),
                                NET_AMOUNT = firstRow["NET_AMOUNT"] == DBNull.Value ? null : Convert.ToDouble(firstRow["NET_AMOUNT"]),
                                FIN_ID = firstRow["FIN_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["FIN_ID"]),
                                USER_ID = firstRow["USER_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["USER_ID"]),
                                NARRATION = firstRow["NARRATION"] == DBNull.Value ? null : ADO.ToString(firstRow["NARRATION"]),
                                REASON_ID = firstRow["REASON_ID"] == DBNull.Value ? null : ADO.ToInt32(firstRow["REASON_ID"]),
                                TRANSFER_NO = ADO.ToInt32(firstRow["TRANSFER_NO"]),
                                DETAILS = new List<TransferOutDetailUpdate>()
                            };

                            // Loop through all rows to populate DETAILS
                            foreach (DataRow dr in tbl.Rows)
                            {
                                var detail = new TransferOutDetailUpdate
                                {
                                    ID = dr["DETAIL_ID"] == DBNull.Value ? 0 : ADO.ToInt32(dr["DETAIL_ID"]),
                                    ITEM_ID = dr["ITEM_ID"] == DBNull.Value ? null : ADO.ToInt32(dr["ITEM_ID"]),
                                    UOM = dr["UOM"] == DBNull.Value ? null : ADO.ToString(dr["UOM"]),
                                    QUANTITY = dr["QTY_ISSUED"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_ISSUED"]),
                                    COST = dr["UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDouble(dr["UNIT_COST"]),
                                    AMOUNT = dr["DETAIL_NET_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDouble(dr["DETAIL_NET_AMOUNT"]),
                                    BATCH_NO = dr["BATCH_NO"] == DBNull.Value ? null : ADO.ToString(dr["BATCH_NO"]),
                                    EXPIRY_DATE = dr["EXPIRY_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["EXPIRY_DATE"]),
                                    QUANTITY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToDouble(dr["QTY_AVAILABLE"]),
                                    BARCODE = dr["BARCODE"] == DBNull.Value ? null : ADO.ToString(dr["BARCODE"]),
                                    DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? null : ADO.ToString(dr["DESCRIPTION"])

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

        public bool Delete(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TRANSFER_OUT";
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
        public TransferDoc GetLastDocNo()
        {
            TransferDoc res = new TransferDoc();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 14
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
        public Int32 approve(TransferOutInvUpdate transferOut)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
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
                        foreach (TransferOutDetailUpdate ur in transferOut.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["ITEM_ID"] = ur.ITEM_ID ?? (object)DBNull.Value;
                            dRow["UOM"] = (object?)ur.UOM ?? DBNull.Value;
                            dRow["QUANTITY"] = ur.QUANTITY ?? 0;
                            dRow["COST"] = ur.COST ?? 0;
                            dRow["AMOUNT"] = 0;
                            dRow["BATCH_NO"] = (object?)ur.BATCH_NO ?? DBNull.Value;
                            dRow["EXPIRY_DATE"] = (object?)ur.EXPIRY_DATE ?? DBNull.Value;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_TRANSFER_OUT", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 5);
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

                    // Pass DataTable as TVP
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_TRANSFER_DETAIL", tbl);
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
            }
        }
    }
}
