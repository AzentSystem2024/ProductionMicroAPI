using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class PurchaseReturnService:IPurchaseReturnService
    {
        public List<GRN_List> GetGrn(Input input)
        {
            List<GRN_List> grnlist = new List<GRN_List>();
            SqlConnection connection = ADO.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " SELECT TB_GRN_HEADER.ID, TB_GRN_HEADER.GRN_NO, TB_GRN_HEADER.GRN_DATE, " +
                    " TB_GRN_HEADER.SUPP_ID, TB_SUPPLIER.SUPP_NAME , TB_CURRENCY.DESCRIPTION, " +
                    " TB_CURRENCY.SYMBOL FROM TB_GRN_HEADER " +
                    " LEFT JOIN TB_SUPPLIER ON TB_GRN_HEADER.SUPP_ID = TB_SUPPLIER.ID " +
                    " LEFT JOIN TB_CURRENCY ON TB_SUPPLIER.CURRENCY_ID = TB_CURRENCY.ID " +
                    " WHERE TB_GRN_HEADER.SUPP_ID = @SUPP_ID";
                cmd.Parameters.AddWithValue("@SUPP_ID", input.SUPP_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    grnlist.Add(new GRN_List
                    {
                        GRN_ID = ADO.ToInt32(dr["ID"]),
                        GRN_NO = ADO.ToString(dr["GRN_NO"]),
                        GRN_DATE = Convert.ToDateTime(dr["GRN_DATE"]),
                        SUPP_NAME = ADO.ToString(dr["SUPP_NAME"]),
                        SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                        CURRENCY = ADO.ToString(dr["DESCRIPTION"]),
                        CURRENCY_SYMBOL = ADO.ToString(dr["SYMBOL"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching GRN list", ex);
            }

            return grnlist;
        }
        public List<GRN_DATA> GetGrnDetails(GrnInput input)
        {
            List<GRN_DATA> grnlist = new List<GRN_DATA>();
            SqlConnection connection = ADO.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " SELECT DISTINCT GD.ITEM_ID,GD.STORE_ID, GD.GRN_ID, IT.BARCODE, IT.DESCRIPTION, GD.ID, " +
                    " IT.UOM, IT.UOM_MULTPLE, IT.UOM_PURCH , " +
                    " GD.SUPP_PRICE, GD.QUANTITY, GD.RATE, GD.DISC_PERCENT, GD.RETURN_QTY, VC.VAT_PERC, ISK.QTY_STOCK" +
                    " FROM TB_GRN_DETAIL GD LEFT JOIN TB_ITEMS IT ON GD.ITEM_ID = IT.ID" +
                    " LEFT JOIN TB_VAT_CLASS VC ON IT.VAT_CLASS_ID = VC.ID" +
                    " LEFT JOIN TB_ITEM_STOCK ISK ON ISK.ITEM_ID = IT.ID  and ISK.STORE_ID = GD.STORE_ID " +
                    " WHERE GD.GRN_ID = @GRN_ID";

                cmd.Parameters.AddWithValue("@GRN_ID", input.GRN_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    grnlist.Add(new GRN_DATA
                    {
                        ITEM_ID = ADO.ToInt32(dr["ITEM_ID"]),
                        STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                        GRN_ID = ADO.ToInt32(dr["GRN_ID"]),
                        BAR_CODE = ADO.ToString(dr["BARCODE"]),
                        ITEM_DES = ADO.ToString(dr["DESCRIPTION"]),
                        DETAIL_ID = ADO.ToInt32(dr["ID"]),
                        QUANTITY = ADO.ToFloat(dr["QUANTITY"]),
                        RETURN_QUANTITY = ADO.ToFloat(dr["RETURN_QTY"]),
                        RATE = ADO.ToFloat(dr["RATE"]),
                        DISC_PERCENT = ADO.ToFloat(dr["DISC_PERCENT"]),
                        VAT_PERCENT = ADO.ToFloat(dr["VAT_PERC"]),
                        SUPP_PRICE = ADO.ToFloat(dr["SUPP_PRICE"]),
                        QTY_STOCK = ADO.ToFloat(dr["QTY_STOCK"]),
                        UOM = ADO.ToString(dr["UOM"]),
                        UOM_MULTPLE = ADO.ToInt32(dr["UOM_MULTPLE"]),
                        UOM_PURCH = ADO.ToString(dr["UOM_PURCH"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching GRN list", ex);
            }

            return grnlist;
        }
        public Int32 Insert(PurchaseReturn purchaseReturn)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_DET_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("PURCH_DET_ID", typeof(int));

                if (purchaseReturn.PurchDetail != null && purchaseReturn.PurchDetail.Any())
                {
                    foreach (PurchaseReturnDetail ur in purchaseReturn.PurchDetail)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_DET_ID"] = ur.GRN_DET_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["BATCH_NO"] = ur.BATCH_NO;
                        dRow["EXPIRY_DATE"] = ur.EXPIRY_DATE;
                        dRow["GRN_QTY"] = ur.PENDING_QTY;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["VAT_PERC"] = ur.VAT_PERC;
                        dRow["VAT_AMOUNT"] = ur.VAT_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        dRow["PURCH_DET_ID"] = ur.PURCH_DET_ID;
                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_RETURN";
                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchaseReturn.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", purchaseReturn.FIN_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchaseReturn.STORE_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", purchaseReturn.RET_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchaseReturn.SUPP_ID);
                cmd.Parameters.AddWithValue("@GRN_ID", purchaseReturn.GRN_ID);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchaseReturn.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchaseReturn.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchaseReturn.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchaseReturn.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", purchaseReturn.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", purchaseReturn.NARRATION);
                cmd.Parameters.AddWithValue("@GRN_NO", purchaseReturn.GRN_NO);

                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_RET_DETAIL", tbl);

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
        public Int32 Update(PurchaseReturn purchaseReturn)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_DET_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("PURCH_DET_ID", typeof(int));

                if (purchaseReturn.PurchDetail != null && purchaseReturn.PurchDetail.Any())
                {
                    foreach (PurchaseReturnDetail ur in purchaseReturn.PurchDetail)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_DET_ID"] = ur.GRN_DET_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["BATCH_NO"] = ur.BATCH_NO;
                        dRow["EXPIRY_DATE"] = ur.EXPIRY_DATE;
                        dRow["GRN_QTY"] = ur.PENDING_QTY;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["VAT_PERC"] = ur.VAT_PERC;
                        dRow["VAT_AMOUNT"] = ur.VAT_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        dRow["PURCH_DET_ID"] = ur.PURCH_DET_ID;
                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_RETURN";
                cmd.Parameters.AddWithValue("ACTION", 2);
                cmd.Parameters.AddWithValue("@ID", purchaseReturn.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchaseReturn.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", purchaseReturn.FIN_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchaseReturn.STORE_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", purchaseReturn.RET_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchaseReturn.SUPP_ID);
                cmd.Parameters.AddWithValue("@GRN_ID", purchaseReturn.GRN_ID);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchaseReturn.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchaseReturn.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchaseReturn.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchaseReturn.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", purchaseReturn.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", purchaseReturn.NARRATION);
                cmd.Parameters.AddWithValue("@GRN_NO", purchaseReturn.GRN_NO);


                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_RET_DETAIL", tbl);

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

        public PurchaseReturn GetPurchaseReturn(int id)
        {
            PurchaseReturn purchaseReturn = new PurchaseReturn();
            List<PurchaseReturnDetail> purchaseReturnDetails = new List<PurchaseReturnDetail>();
            try
            {
                string strSQL = " SELECT TB_PURCH_RET_HEADER.*, TB_COMPANY_MASTER.COMPANY_NAME,TB_STORES.STORE_NAME, " +
                    " TB_SUPPLIER.SUPP_NAME, TB_AC_TRANS_HEADER.NARRATION AS NARRATION,TB_CURRENCY.SYMBOL FROM TB_PURCH_RET_HEADER " +
                    " LEFT JOIN TB_COMPANY_MASTER ON TB_PURCH_RET_HEADER.COMPANY_ID = TB_COMPANY_MASTER.ID " +
                    " LEFT JOIN TB_STORES ON TB_PURCH_RET_HEADER.STORE_ID = TB_STORES.ID " +
                    " LEFT JOIN TB_SUPPLIER on TB_PURCH_RET_HEADER.SUPP_ID = TB_SUPPLIER.ID " +
                    " LEFT JOIN TB_CURRENCY on TB_SUPPLIER.CURRENCY_ID = TB_CURRENCY.ID " +
                    " LEFT JOIN TB_AC_TRANS_HEADER ON TB_PURCH_RET_HEADER.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
                    " WHERE TB_PURCH_RET_HEADER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Grn");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    purchaseReturn = new PurchaseReturn
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),
                        COMPANY_NAME = ADO.ToString(dr["COMPANY_NAME"]),
                        STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                        STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                        RET_NO = ADO.ToString(dr["RET_NO"]),
                        RET_DATE = Convert.ToDateTime(dr["RET_DATE"]),
                        SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                        SUPPLIER_NAME = ADO.ToString(dr["SUPP_NAME"]),
                        GRN_ID = ADO.ToInt32(dr["GRN_ID"]),
                        GRN_NO = ADO.ToString(dr["GRN_NO"]),
                        IS_CREDIT = ADO.Toboolean(dr["IS_CREDIT"]),
                        GROSS_AMOUNT = ADO.ToDecimal(dr["GROSS_AMOUNT"]),
                        VAT_AMOUNT = ADO.ToDecimal(dr["VAT_AMOUNT"]),
                        NET_AMOUNT = ADO.ToDecimal(dr["NET_AMOUNT"]),
                        CURRENCY_SYMBOL = ADO.ToString(dr["SYMBOL"]),
                        NARRATION = ADO.ToString(dr["NARRATION"])
                    };
                }

                strSQL = " SELECT distinct TB_PURCH_RET_DETAIL.*,  TB_COMPANY_MASTER.COMPANY_NAME,TB_STORES.STORE_NAME, " +
                " TB_ITEMS.DESCRIPTION,TB_ITEMS.BARCODE,TB_GRN_DETAIL.DISC_PERCENT,TB_GRN_DETAIL.SUPP_PRICE,TB_GRN_DETAIL.RETURN_QTY,TB_ITEM_STOCK.QTY_STOCK FROM TB_PURCH_RET_DETAIL " +
                " LEFT JOIN TB_COMPANY_MASTER ON TB_PURCH_RET_DETAIL.COMPANY_ID = TB_COMPANY_MASTER.ID " +
                " LEFT JOIN TB_STORES ON TB_PURCH_RET_DETAIL.STORE_ID = TB_STORES.ID " +
                " LEFT JOIN TB_ITEMS ON TB_PURCH_RET_DETAIL.ITEM_ID = TB_ITEMS.ID " +
                " LEFT JOIN TB_GRN_DETAIL ON TB_PURCH_RET_DETAIL.ITEM_ID = TB_GRN_DETAIL.ITEM_ID and TB_PURCH_RET_DETAIL.GRN_DET_ID = TB_GRN_DETAIL.ID " +
                " LEFT JOIN TB_ITEM_STOCK ON TB_ITEM_STOCK.ITEM_ID = TB_ITEMS.ID and TB_ITEM_STOCK.STORE_ID = TB_GRN_DETAIL.STORE_ID " +
                " WHERE TB_PURCH_RET_DETAIL.RET_ID = " + id;

                DataTable tblgrndetail = ADO.GetDataTable(strSQL, "GrnDetails");

                foreach (DataRow dr3 in tblgrndetail.Rows)
                {
                    purchaseReturnDetails.Add(new PurchaseReturnDetail
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        COMPANY_ID = ADO.ToInt32(dr3["COMPANY_ID"]),
                        COMPANY_NAME = ADO.ToString(dr3["COMPANY_NAME"]),
                        STORE_ID = ADO.ToInt32(dr3["STORE_ID"]),
                        STORE_NAME = ADO.ToString(dr3["STORE_NAME"]),
                        RET_ID = ADO.ToInt32(dr3["RET_ID"]),
                        GRN_DET_ID = ADO.ToInt32(dr3["GRN_DET_ID"]),
                        ITEM_ID = ADO.ToInt32(dr3["ITEM_ID"]),
                        BATCH_NO = ADO.ToString(dr3["BATCH_NO"]),
                        EXPIRY_DATE = Convert.ToDateTime(dr3["EXPIRY_DATE"]),
                        PENDING_QTY = ADO.ToFloat(dr3["GRN_QTY"]),
                        QUANTITY = ADO.ToFloat(dr3["QUANTITY"]),
                        RATE = ADO.ToDecimal(dr3["RATE"]),
                        AMOUNT = ADO.ToDecimal(dr3["AMOUNT"]),
                        VAT_PERC = ADO.ToDecimal(dr3["VAT_PERC"]),
                        VAT_AMOUNT = ADO.ToDecimal(dr3["VAT_AMOUNT"]),
                        TOTAL_AMOUNT = ADO.ToDecimal(dr3["TOTAL_AMOUNT"]),
                        UOM_PURCH = ADO.ToString(dr3["UOM_PURCH"]),
                        UOM = ADO.ToString(dr3["UOM"]),
                        UOM_MULTIPLE = ADO.ToInt32(dr3["UOM_MULTIPLE"]),
                        ITEM_NAME = ADO.ToString(dr3["DESCRIPTION"]),
                        BAR_CODE = ADO.ToString(dr3["BARCODE"]),
                        DISC_PERCENT = ADO.ToFloat(dr3["DISC_PERCENT"]),
                        SUPP_PRICE = ADO.ToDecimal(dr3["SUPP_PRICE"]),
                        RETURN_QTY = ADO.ToFloat(dr3["RETURN_QTY"]),
                        QTY_STOCK = ADO.ToFloat(dr3["QTY_STOCK"]),
                        PURCH_DET_ID = ADO.ToInt32(dr3["PURCH_DET_ID"])

                    });
                }
                purchaseReturn.PurchDetail = purchaseReturnDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return purchaseReturn;
        }

        public PurchaseReturnListResponse List()
        {
            var res = new PurchaseReturnListResponse
            {
                Flag = 0,
                Message = string.Empty,
                Data = new List<PurchaseReturnList>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_PURCHASE_RETURN", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        da.Fill(tbl);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            var item = new PurchaseReturnList
                            {
                                ID = ADO.ToInt32(dr["ID"]),
                                RET_NO = ADO.ToString(dr["RET_NO"]),
                                RET_DATE = dr["RET_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["RET_DATE"]),
                                SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                                SUPP_NAME = ADO.ToString(dr["SUPP_NAME"]),
                                GROSS_AMOUNT = dr["GROSS_AMOUNT"] == DBNull.Value ? 0m : ADO.ToDecimal(dr["GROSS_AMOUNT"]),
                                VAT_AMOUNT = dr["VAT_AMOUNT"] == DBNull.Value ? 0m : ADO.ToDecimal(dr["VAT_AMOUNT"]),
                                NET_AMOUNT = dr["NET_AMOUNT"] == DBNull.Value ? 0m : ADO.ToDecimal(dr["NET_AMOUNT"]),
                                TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]),
                                TRANS_STATUS = ADO.ToInt32(dr["TRANS_STATUS"])
                            };

                            res.Data.Add(item);
                        }
                    }
                }

                res.Flag = res.Data.Count > 0 ? 1 : 0;
                res.Message = res.Flag == 1 ? "Success" : "No records found";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PurchaseReturnList>();
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
                cmd.CommandText = "SP_TB_PURCHASE_RETURN";
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

        public Int32 Verify(PurchaseReturn purchaseReturn)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_DET_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("PURCH_DET_ID", typeof(int));

                if (purchaseReturn.PurchDetail != null && purchaseReturn.PurchDetail.Any())
                {
                    foreach (PurchaseReturnDetail ur in purchaseReturn.PurchDetail)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_DET_ID"] = ur.GRN_DET_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["BATCH_NO"] = ur.BATCH_NO;
                        dRow["EXPIRY_DATE"] = ur.EXPIRY_DATE;
                        dRow["GRN_QTY"] = ur.PENDING_QTY;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["VAT_PERC"] = ur.VAT_PERC;
                        dRow["VAT_AMOUNT"] = ur.VAT_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        dRow["PURCH_DET_ID"] = ur.PURCH_DET_ID;
                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_RETURN";
                cmd.Parameters.AddWithValue("ACTION", 5);
                cmd.Parameters.AddWithValue("@ID", purchaseReturn.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchaseReturn.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", purchaseReturn.FIN_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchaseReturn.STORE_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", purchaseReturn.RET_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchaseReturn.SUPP_ID);
                cmd.Parameters.AddWithValue("@GRN_ID", purchaseReturn.GRN_ID);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchaseReturn.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchaseReturn.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchaseReturn.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchaseReturn.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", purchaseReturn.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", purchaseReturn.NARRATION);
                cmd.Parameters.AddWithValue("@GRN_NO", purchaseReturn.GRN_NO);


                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_RET_DETAIL", tbl);

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

        public Int32 Approve(PurchaseReturn purchaseReturn)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_DET_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("BATCH_NO", typeof(string));
                tbl.Columns.Add("EXPIRY_DATE", typeof(DateTime));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(decimal));
                tbl.Columns.Add("AMOUNT", typeof(decimal));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(int));
                tbl.Columns.Add("PURCH_DET_ID", typeof(int));

                if (purchaseReturn.PurchDetail != null && purchaseReturn.PurchDetail.Any())
                {
                    foreach (PurchaseReturnDetail ur in purchaseReturn.PurchDetail)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_DET_ID"] = ur.GRN_DET_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["BATCH_NO"] = ur.BATCH_NO;
                        dRow["EXPIRY_DATE"] = ur.EXPIRY_DATE;
                        dRow["GRN_QTY"] = ur.PENDING_QTY;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["VAT_PERC"] = ur.VAT_PERC;
                        dRow["VAT_AMOUNT"] = ur.VAT_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        dRow["PURCH_DET_ID"] = ur.PURCH_DET_ID;
                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_RETURN";
                cmd.Parameters.AddWithValue("ACTION", 6);
                cmd.Parameters.AddWithValue("@ID", purchaseReturn.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchaseReturn.COMPANY_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", purchaseReturn.FIN_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchaseReturn.STORE_ID);
                cmd.Parameters.AddWithValue("@RET_DATE", purchaseReturn.RET_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchaseReturn.SUPP_ID);
                cmd.Parameters.AddWithValue("@GRN_ID", purchaseReturn.GRN_ID);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchaseReturn.IS_CREDIT);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchaseReturn.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchaseReturn.VAT_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchaseReturn.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@USER_ID", purchaseReturn.USER_ID);
                cmd.Parameters.AddWithValue("@NARRATION", purchaseReturn.NARRATION);
                cmd.Parameters.AddWithValue("@GRN_NO", purchaseReturn.GRN_NO);


                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_RET_DETAIL", tbl);

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
        public PurchaseInvoiceDetailResponse GetPurchaseInvoiceDetail(InvoiceInput request)
        {
            PurchaseInvoiceDetailResponse res = new PurchaseInvoiceDetailResponse();
            res.Data = new List<PurchaseInvoiceDetail>();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_PURCHASE_RETURN", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 7);       
                    cmd.Parameters.AddWithValue("@SUPP_ID", request.SUPP_ID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        res.Data.Add(new PurchaseInvoiceDetail
                        {
                            ID = row["ID"] != DBNull.Value ? Convert.ToInt32(row["ID"]) : 0,
                            DOC_NO = row["DOC_NO"] != DBNull.Value ? row["DOC_NO"].ToString() : "",
                            PURCH_DATE = row["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(row["PURCH_DATE"]) : DateTime.MinValue,
                            DETAIL_ID = row["DETAIL_ID"] != DBNull.Value ? Convert.ToInt32(row["DETAIL_ID"]) : 0,
                            GRN_DET_ID = row["GRN_DET_ID"] != DBNull.Value ? Convert.ToInt32(row["GRN_DET_ID"]) : 0,
                            ITEM_ID = row["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                            ITEM_NAME = row["DESCRIPTION"] != DBNull.Value ? row["DESCRIPTION"].ToString() : "",
                            PENDING_QTY = row["QUANTITY"] != DBNull.Value ? Convert.ToSingle(row["QUANTITY"]) : 0,
                            RATE = row["RATE"] != DBNull.Value ? Convert.ToDecimal(row["RATE"]) : 0,
                            AMOUNT = row["AMOUNT"] != DBNull.Value ? Convert.ToDecimal(row["AMOUNT"]) : 0,
                            TAX_AMOUNT = row["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(row["TAX_AMOUNT"]) : 0,
                            VAT_PERC = row["VAT_PERC"] != DBNull.Value ? Convert.ToDecimal(row["VAT_PERC"]) : 0,
                            QTY_STOCK = row["QTY_STOCK"] != DBNull.Value ? Convert.ToSingle(row["QTY_STOCK"]) : 0,
                            BARCODE = row["BARCODE"] != DBNull.Value ? row["BARCODE"].ToString() : "",
                            UOM = row["UOM"] != DBNull.Value ? row["UOM"].ToString() : "",
                            UOM_PURCH = row["UOM_PURCH"] != DBNull.Value ? row["UOM_PURCH"].ToString() : "",
                            UOM_MULTIPLE = row["UOM_MULTPLE"] != DBNull.Value ? Convert.ToInt32(row["UOM_MULTPLE"]) : 0

                        });
                    }

                    res.Flag = 1;
                    res.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PurchaseInvoiceDetail>();
            }

            return res;
        }

    }
}
