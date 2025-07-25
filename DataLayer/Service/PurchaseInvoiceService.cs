﻿using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        public List<PIDropdownData> GetPendingPoList(PIDropdownInput input)
        {
            List<PIDropdownData> POlist = new List<PIDropdownData>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_PENDING_PO_LIST";
            cmd.Parameters.AddWithValue("@SUPP_ID", input.SUPP_ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            foreach (DataRow dr in tbl.Rows)
            {
                POlist.Add(new PIDropdownData
                {
                    PO_ID = ADO.ToInt32(dr["ID"]),
                    PO_NO = ADO.ToString(dr["PO_NO"]),
                    PO_DATE = Convert.ToDateTime(dr["PO_DATE"]),
                    SUPP_NAME = ADO.ToString(dr["SUPP_NAME"]),
                });
            }
            connection.Close();

            return POlist;
        }
        public GRNDetailResponce GetSelectedPoDetailS(GRNDetailInput input)
        {
            GRNDetailResponce response = new GRNDetailResponce();
            List<GRNDetails> poDetailsList = new List<GRNDetails>();

            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_PO_DETAILS";
            cmd.Parameters.AddWithValue("@PO_ID", input.PO_ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            // Populate PODetails
            foreach (DataRow dr in tbl.Rows)
            {
                GRNDetails poDetail = new GRNDetails
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    GRN_NO = ADO.ToInt32(dr["GRN_NO"]),
                    ITEM_ID = ADO.ToInt32(dr["ITEM_ID"]),
                    BARCODE = ADO.ToString(dr["BARCODE"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    UOM = ADO.ToString(dr["UOM"]),
                    PRICE = ADO.ToFloat(dr["PRICE"]),
                    PENDING_QTY = ADO.ToFloat(dr["PENDING_QTY"]),
                    DISC_PERCENT = ADO.ToFloat(dr["DISC_PERCENT"]),
                    TAX_PERCENT = ADO.ToDecimal(dr["TAX_PERCENT"]),

                    COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),
                    PACKING = ADO.ToString(dr["PACKING"]),//MANAGE NULL
                    QUANTITY = ADO.ToFloat(dr["QUANTITY"]),
                    RATE = ADO.ToFloat(dr["RATE"]),
                    RETURN_QTY = ADO.ToFloat(dr["RETURN_QTY"]),
                    ITEM_DESC = ADO.ToString(dr["ITEM_DESC"]),
                    PO_DET_ID = ADO.ToInt32(dr["PO_DET_ID"]),
                    COST = ADO.ToFloat(dr["COST"]),
                    SUPP_PRICE = ADO.ToFloat(dr["SUPP_PRICE"]),
                    SUPP_AMOUNT = ADO.ToFloat(dr["SUPP_AMOUNT"]),
                    GRN_STORE_ID = ADO.ToInt32(dr["GRN_STORE_ID"]),
                    SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                };
                poDetailsList.Add(poDetail);
            }

            // Assign the populated data to the response object
            response.Flag = 1; // Or any flag you want to return
            response.Message = "Success"; // Any message you want to include
            response.GRNDetails = poDetailsList; // List of PODetails

            connection.Close();
            return response;
        }

        public PurchHeader GetPurchaseInvoiceById(int id)
        {
            PurchHeader header = new PurchHeader();
            List<PurchDetails> detail = new List<PurchDetails>();

            try
            {
                string strSQL = "SELECT TB_PURCH_HEADER.*, " +
                  "TB_STORES.STORE_NAME, " +
                  "TB_SUPPLIER.SUPP_NAME, " +
                  "TB_PO_HEADER.PO_NO, " +
                  "TB_CURRENCY.ID AS CURRENCY_ID, " +
                  "TB_CURRENCY.SYMBOL, " +
                  "TB_STATUS.STATUS_DESC AS STATUS, " +
                  "TB_AC_TRANS_HEADER.NARRATION AS NARRATION " +
                  "FROM TB_PURCH_HEADER " +
                  "LEFT JOIN TB_STORES ON TB_PURCH_HEADER.STORE_ID = TB_STORES.ID " +
                  "LEFT JOIN TB_SUPPLIER ON TB_PURCH_HEADER.SUPP_ID = TB_SUPPLIER.ID " +
                  "LEFT JOIN TB_AC_TRANS_HEADER ON TB_PURCH_HEADER.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
                   "LEFT JOIN TB_CURRENCY ON TB_SUPPLIER.CURRENCY_ID = TB_CURRENCY.ID " +
                  "LEFT JOIN TB_PO_HEADER ON TB_PURCH_HEADER.PO_ID = TB_PO_HEADER.ID " +
                  "LEFT JOIN TB_STATUS ON TB_AC_TRANS_HEADER.TRANS_STATUS = TB_STATUS.ID " +
                  "WHERE TB_PURCH_HEADER.ID = " + id + ";";

                DataTable tbl = ADO.GetDataTable(strSQL, "PurchHeader");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                     header = new PurchHeader
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        STORE_ID = Convert.ToInt32(dr["STORE_ID"]),
                        PURCH_NO = dr["PURCH_NO"] != DBNull.Value ? dr["PURCH_NO"].ToString() : null,
                        STORE_NAME = dr["STORE_NAME"] != DBNull.Value ? dr["STORE_NAME"].ToString() : null,
                        SUPPPLIER_NAME = dr["SUPP_NAME"] != DBNull.Value ? dr["SUPP_NAME"].ToString() : null,
                        NARRATION = dr["NARRATION"] != DBNull.Value ? dr["NARRATION"].ToString() : null,
                        STATUS = dr["STATUS"] != DBNull.Value ? dr["STATUS"].ToString() : null,
                        PURCH_DATE = dr["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["PURCH_DATE"]) : null,
                        IS_CREDIT = dr["IS_CREDIT"] != DBNull.Value && Convert.ToBoolean(dr["IS_CREDIT"]),
                        SUPP_ID = dr["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(dr["SUPP_ID"]) : (int?)null,
                        SUPP_INV_NO = dr["SUPP_INV_NO"] != DBNull.Value ? dr["SUPP_INV_NO"].ToString() : null,
                        SUPP_INV_DATE = dr["SUPP_INV_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["SUPP_INV_DATE"]) : null,
                        PO_ID = dr["PO_ID"] != DBNull.Value ? Convert.ToInt32(dr["PO_ID"]) : (int?)null,
                        PO_NO = dr["PO_NO"] != DBNull.Value ? dr["PO_NO"].ToString() : null,
                        FIN_ID = dr["FIN_ID"] != DBNull.Value ? Convert.ToInt32(dr["FIN_ID"]) : (int?)null,
                        TRANS_ID = dr["TRANS_ID"] != DBNull.Value ? Convert.ToInt64(dr["TRANS_ID"]) : (long?)null,
                        PURCH_TYPE = dr["PURCH_TYPE"] != DBNull.Value ? Convert.ToInt16(dr["PURCH_TYPE"]) : (short?)null,
                        DISCOUNT_AMOUNT = dr["DISCOUNT_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["DISCOUNT_AMOUNT"]) : (float?)null,
                        SUPP_GROSS_AMOUNT = dr["SUPP_GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["SUPP_GROSS_AMOUNT"]) : (float?)null,
                        SUPP_NET_AMOUNT = dr["SUPP_NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["SUPP_NET_AMOUNT"]) : (float?)null,
                        EXCHANGE_RATE = dr["EXCHANGE_RATE"] != DBNull.Value ? Convert.ToSingle(dr["EXCHANGE_RATE"]) : (float?)null,
                        GROSS_AMOUNT = dr["GROSS_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["GROSS_AMOUNT"]) : (float?)null,
                        CHARGE_DESCRIPTION = dr["CHARGE_DESCRIPTION"] != DBNull.Value ? dr["CHARGE_DESCRIPTION"].ToString() : null,
                        CHARGE_AMOUNT = dr["CHARGE_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["CHARGE_AMOUNT"]) : (float?)null,
                        VAT_AMOUNT = dr["VAT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(dr["VAT_AMOUNT"]) : (decimal?)null,
                        NET_AMOUNT = dr["NET_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["NET_AMOUNT"]) : (float?)null,
                        RETURN_AMOUNT = dr["RETURN_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(dr["RETURN_AMOUNT"]) : (decimal?)null,
                        ADJ_AMOUNT = dr["ADJ_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["ADJ_AMOUNT"]) : (float?)null,
                        PAID_AMOUNT = dr["PAID_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["PAID_AMOUNT"]) : 0f
                    };
                }

                string strDetailSQL = "SELECT " +
                      "TB_PURCH_DETAIL.*, " +
                      "TB_STORES.STORE_NAME, " +
                      "TB_ITEMS.DESCRIPTION, " +
                      "TB_ITEMS.ITEM_CODE, " +
                      "TB_PO_DETAIL.GRN_QTY, " +
                      "TB_PO_DETAIL.QUANTITY AS PO_QUANTITY " +
                       "FROM TB_PURCH_DETAIL " +
                       "LEFT JOIN TB_STORES ON TB_PURCH_DETAIL.STORE_ID = TB_STORES.ID " +
                       "LEFT JOIN TB_ITEMS ON TB_PURCH_DETAIL.ITEM_ID = TB_ITEMS.ID " +
                        "LEFT JOIN TB_PO_DETAIL ON TB_PURCH_DETAIL.PO_DET_ID = TB_PO_DETAIL.ID " +
                        "WHERE TB_PURCH_DETAIL.PURCH_ID = " + id + ";";

                DataTable tblpurchdetail = ADO.GetDataTable(strDetailSQL, "PurchDetails");

                foreach (DataRow dr in tblpurchdetail.Rows)
                {
                    detail.Add(new PurchDetails
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt64(dr["ID"]) : 0,
                        COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COMPANY_ID"]) : 0,
                        STORE_ID = dr["STORE_ID"] != DBNull.Value ? Convert.ToInt32(dr["STORE_ID"]) : 0,
                        PURCH_ID = dr["PURCH_ID"] != DBNull.Value ? Convert.ToInt32(dr["PURCH_ID"]) : 0,
                        GRN_DET_ID = dr["GRN_DET_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["GRN_DET_ID"]) : null,
                        ITEM_ID = dr["ITEM_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["ITEM_ID"]) : null,
                        PACKING = dr["PACKING"] != DBNull.Value ? Convert.ToString(dr["PACKING"]) : null,
                        QUANTITY = dr["QUANTITY"] != DBNull.Value ? (float?)Convert.ToSingle(dr["QUANTITY"]) : null,
                        RATE = dr["RATE"] != DBNull.Value ? (float?)Convert.ToSingle(dr["RATE"]) : null,
                        AMOUNT = dr["AMOUNT"] != DBNull.Value ? (float?)Convert.ToSingle(dr["AMOUNT"]) : null,
                        RETURN_QTY = dr["RETURN_QTY"] != DBNull.Value ? (float?)Convert.ToSingle(dr["RETURN_QTY"]) : null,
                        ITEM_DESC = dr["ITEM_DESC"] != DBNull.Value ? Convert.ToString(dr["ITEM_DESC"]) : null,
                        PO_DET_ID = dr["PO_DET_ID"] != DBNull.Value ? (long?)Convert.ToInt64(dr["PO_DET_ID"]) : null,
                        UOM = dr["UOM"] != DBNull.Value ? Convert.ToString(dr["UOM"]) : null,
                        DISC_PERCENT = dr["DISC_PERCENT"] != DBNull.Value ? (float?)Convert.ToSingle(dr["DISC_PERCENT"]) : null,
                        COST = dr["COST"] != DBNull.Value ? (float?)Convert.ToSingle(dr["COST"]) : null,
                        SUPP_PRICE = dr["SUPP_PRICE"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUPP_PRICE"]) : null,
                        SUPP_AMOUNT = dr["SUPP_AMOUNT"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUPP_AMOUNT"]) : null,
                        VAT_PERC = dr["VAT_PERC"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["VAT_PERC"]) : null,
                        VAT_AMOUNT = dr["VAT_AMOUNT"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["VAT_AMOUNT"]) : null,
                        GRN_STORE_ID = dr["GRN_STORE_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["GRN_STORE_ID"]) : null,
                        RETURN_AMOUNT = dr["RETURN_AMOUNT"] != DBNull.Value ? Convert.ToSingle(dr["RETURN_AMOUNT"]) : 0f,

                        // New fields from JOINs
                        STORE_NAME = dr["STORE_NAME"] != DBNull.Value ? Convert.ToString(dr["STORE_NAME"]) : null,
                        ITEM_NAME = dr["DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["DESCRIPTION"]) : null,
                        ITEM_CODE = dr["ITEM_CODE"] != DBNull.Value ? Convert.ToString(dr["ITEM_CODE"]) : null,
                        PO_QUANTITY = dr["PO_QUANTITY"] != DBNull.Value ? Convert.ToDecimal(dr["PO_QUANTITY"]) : 0,
                        GRN_QUANTITY = dr["GRN_QTY"] != DBNull.Value ? Convert.ToDecimal(dr["GRN_QTY"]) : 0
                    });
                }

                header.PurchDetails = detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return header;
        }
        public Int32 Insert(PurchHeader purchHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(long));
                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("PURCH_ID", typeof(int));
                tbl.Columns.Add("GRN_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("PO_DET_ID", typeof(long));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("GRN_STORE_ID", typeof(int));
                tbl.Columns.Add("RETURN_AMOUNT", typeof(float));

                if (purchHeader.PurchDetails != null && purchHeader.PurchDetails.Any())
                {
                    foreach (var ur in purchHeader.PurchDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["PURCH_ID"] = ur.PURCH_ID;
                        dRow["GRN_DET_ID"] = (object?)ur.GRN_DET_ID ?? DBNull.Value;
                        dRow["ITEM_ID"] = (object?)ur.ITEM_ID ?? DBNull.Value;
                        dRow["PACKING"] = ur.PACKING ?? string.Empty;
                        dRow["QUANTITY"] = (object?)ur.QUANTITY ?? DBNull.Value;
                        dRow["RATE"] = (object?)ur.RATE ?? DBNull.Value;
                        dRow["AMOUNT"] = (object?)ur.AMOUNT ?? DBNull.Value;
                        dRow["RETURN_QTY"] = (object?)ur.RETURN_QTY ?? DBNull.Value;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC ?? string.Empty;
                        dRow["PO_DET_ID"] = (object?)ur.PO_DET_ID ?? DBNull.Value;
                        dRow["UOM"] = ur.UOM ?? string.Empty;
                        dRow["DISC_PERCENT"] = (object?)ur.DISC_PERCENT ?? DBNull.Value;
                        dRow["COST"] = (object?)ur.COST ?? DBNull.Value;
                        dRow["SUPP_PRICE"] = (object?)ur.SUPP_PRICE ?? DBNull.Value;
                        dRow["SUPP_AMOUNT"] = (object?)ur.SUPP_AMOUNT ?? DBNull.Value;
                        dRow["VAT_PERC"] = (object?)ur.VAT_PERC ?? DBNull.Value;
                        dRow["VAT_AMOUNT"] = (object?)ur.VAT_AMOUNT ?? DBNull.Value;
                        dRow["GRN_STORE_ID"] = (object?)ur.GRN_STORE_ID ?? DBNull.Value;
                        dRow["RETURN_AMOUNT"] = ur.RETURN_AMOUNT;

                        tbl.Rows.Add(dRow);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCH";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@USER_ID", purchHeader.USER_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PURCH_DATE", purchHeader.PURCH_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchHeader.IS_CREDIT);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchHeader.SUPP_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_NO", purchHeader.SUPP_INV_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_DATE", purchHeader.SUPP_INV_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_ID", purchHeader.PO_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_NO", purchHeader.PO_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FIN_ID", purchHeader.FIN_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PURCH_TYPE", purchHeader.PURCH_TYPE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", purchHeader.DISCOUNT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", purchHeader.SUPP_GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", purchHeader.SUPP_NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", purchHeader.EXCHANGE_RATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchHeader.GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", purchHeader.CHARGE_DESCRIPTION ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", purchHeader.CHARGE_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchHeader.VAT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchHeader.NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RETURN_AMOUNT", purchHeader.RETURN_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ADJ_AMOUNT", purchHeader.ADJ_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PAID_AMOUNT", purchHeader.PAID_AMOUNT);

                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_DETAIL", tbl);


                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();

                cmd1.Connection = connection;
                cmd1.Transaction = objtrans;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(ID) FROM TB_WORKSHEET";


                Int32 UserID = ADO.ToInt32(cmd1.ExecuteScalar());

                // Commit the transaction if everything is successful
                objtrans.Commit();

                return UserID;
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
        public Int32 Update(PurchHeader purchHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(long));
                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("PURCH_ID", typeof(int));
                tbl.Columns.Add("GRN_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("PO_DET_ID", typeof(long));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("GRN_STORE_ID", typeof(int));
                tbl.Columns.Add("RETURN_AMOUNT", typeof(float));

                if (purchHeader.PurchDetails != null && purchHeader.PurchDetails.Any())
                {
                    foreach (var ur in purchHeader.PurchDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["PURCH_ID"] = ur.PURCH_ID;
                        dRow["GRN_DET_ID"] = (object?)ur.GRN_DET_ID ?? DBNull.Value;
                        dRow["ITEM_ID"] = (object?)ur.ITEM_ID ?? DBNull.Value;
                        dRow["PACKING"] = ur.PACKING ?? string.Empty;
                        dRow["QUANTITY"] = (object?)ur.QUANTITY ?? DBNull.Value;
                        dRow["RATE"] = (object?)ur.RATE ?? DBNull.Value;
                        dRow["AMOUNT"] = (object?)ur.AMOUNT ?? DBNull.Value;
                        dRow["RETURN_QTY"] = (object?)ur.RETURN_QTY ?? DBNull.Value;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC ?? string.Empty;
                        dRow["PO_DET_ID"] = (object?)ur.PO_DET_ID ?? DBNull.Value;
                        dRow["UOM"] = ur.UOM ?? string.Empty;
                        dRow["DISC_PERCENT"] = (object?)ur.DISC_PERCENT ?? DBNull.Value;
                        dRow["COST"] = (object?)ur.COST ?? DBNull.Value;
                        dRow["SUPP_PRICE"] = (object?)ur.SUPP_PRICE ?? DBNull.Value;
                        dRow["SUPP_AMOUNT"] = (object?)ur.SUPP_AMOUNT ?? DBNull.Value;
                        dRow["VAT_PERC"] = (object?)ur.VAT_PERC ?? DBNull.Value;
                        dRow["VAT_AMOUNT"] = (object?)ur.VAT_AMOUNT ?? DBNull.Value;
                        dRow["GRN_STORE_ID"] = (object?)ur.GRN_STORE_ID ?? DBNull.Value;
                        dRow["RETURN_AMOUNT"] = ur.RETURN_AMOUNT;

                        tbl.Rows.Add(dRow);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCH";

                cmd.Parameters.AddWithValue("ACTION", 2);
                cmd.Parameters.AddWithValue("@ID", purchHeader.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@USER_ID", purchHeader.USER_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PURCH_DATE", purchHeader.PURCH_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchHeader.IS_CREDIT);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchHeader.SUPP_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_NO", purchHeader.SUPP_INV_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_DATE", purchHeader.SUPP_INV_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_ID", purchHeader.PO_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_NO", purchHeader.PO_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FIN_ID", purchHeader.FIN_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PURCH_TYPE", purchHeader.PURCH_TYPE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", purchHeader.DISCOUNT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", purchHeader.SUPP_GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", purchHeader.SUPP_NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", purchHeader.EXCHANGE_RATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchHeader.GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", purchHeader.CHARGE_DESCRIPTION ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", purchHeader.CHARGE_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchHeader.VAT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchHeader.NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RETURN_AMOUNT", purchHeader.RETURN_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ADJ_AMOUNT", purchHeader.ADJ_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PAID_AMOUNT", purchHeader.PAID_AMOUNT);

                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_DETAIL", tbl);


                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();

                cmd1.Connection = connection;
                cmd1.Transaction = objtrans;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(ID) FROM TB_WORKSHEET";


                Int32 UserID = ADO.ToInt32(cmd1.ExecuteScalar());

                // Commit the transaction if everything is successful
                objtrans.Commit();

                return UserID;
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
        public Int32 Verify(PurchHeader purchHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(long));
                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("PURCH_ID", typeof(int));
                tbl.Columns.Add("GRN_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("PO_DET_ID", typeof(long));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("GRN_STORE_ID", typeof(int));
                tbl.Columns.Add("RETURN_AMOUNT", typeof(float));

                if (purchHeader.PurchDetails != null && purchHeader.PurchDetails.Any())
                {
                    foreach (var ur in purchHeader.PurchDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["PURCH_ID"] = ur.PURCH_ID;
                        dRow["GRN_DET_ID"] = (object?)ur.GRN_DET_ID ?? DBNull.Value;
                        dRow["ITEM_ID"] = (object?)ur.ITEM_ID ?? DBNull.Value;
                        dRow["PACKING"] = ur.PACKING ?? string.Empty;
                        dRow["QUANTITY"] = (object?)ur.QUANTITY ?? DBNull.Value;
                        dRow["RATE"] = (object?)ur.RATE ?? DBNull.Value;
                        dRow["AMOUNT"] = (object?)ur.AMOUNT ?? DBNull.Value;
                        dRow["RETURN_QTY"] = (object?)ur.RETURN_QTY ?? DBNull.Value;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC ?? string.Empty;
                        dRow["PO_DET_ID"] = (object?)ur.PO_DET_ID ?? DBNull.Value;
                        dRow["UOM"] = ur.UOM ?? string.Empty;
                        dRow["DISC_PERCENT"] = (object?)ur.DISC_PERCENT ?? DBNull.Value;
                        dRow["COST"] = (object?)ur.COST ?? DBNull.Value;
                        dRow["SUPP_PRICE"] = (object?)ur.SUPP_PRICE ?? DBNull.Value;
                        dRow["SUPP_AMOUNT"] = (object?)ur.SUPP_AMOUNT ?? DBNull.Value;
                        dRow["VAT_PERC"] = (object?)ur.VAT_PERC ?? DBNull.Value;
                        dRow["VAT_AMOUNT"] = (object?)ur.VAT_AMOUNT ?? DBNull.Value;
                        dRow["GRN_STORE_ID"] = (object?)ur.GRN_STORE_ID ?? DBNull.Value;
                        dRow["RETURN_AMOUNT"] = ur.RETURN_AMOUNT;

                        tbl.Rows.Add(dRow);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCH";

                cmd.Parameters.AddWithValue("ACTION", 5);
                cmd.Parameters.AddWithValue("@ID", purchHeader.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@USER_ID", purchHeader.USER_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PURCH_DATE", purchHeader.PURCH_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchHeader.IS_CREDIT);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchHeader.SUPP_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_NO", purchHeader.SUPP_INV_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_DATE", purchHeader.SUPP_INV_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_ID", purchHeader.PO_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_NO", purchHeader.PO_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FIN_ID", purchHeader.FIN_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PURCH_TYPE", purchHeader.PURCH_TYPE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", purchHeader.DISCOUNT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", purchHeader.SUPP_GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", purchHeader.SUPP_NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", purchHeader.EXCHANGE_RATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchHeader.GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", purchHeader.CHARGE_DESCRIPTION ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", purchHeader.CHARGE_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchHeader.VAT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchHeader.NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RETURN_AMOUNT", purchHeader.RETURN_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ADJ_AMOUNT", purchHeader.ADJ_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PAID_AMOUNT", purchHeader.PAID_AMOUNT);

                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_DETAIL", tbl);


                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();

                cmd1.Connection = connection;
                cmd1.Transaction = objtrans;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(ID) FROM TB_WORKSHEET";


                Int32 UserID = ADO.ToInt32(cmd1.ExecuteScalar());

                // Commit the transaction if everything is successful
                objtrans.Commit();

                return UserID;
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

        public Int32 Approve(PurchHeader purchHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(long));
                tbl.Columns.Add("COMPANY_ID", typeof(int));
                tbl.Columns.Add("STORE_ID", typeof(int));
                tbl.Columns.Add("PURCH_ID", typeof(int));
                tbl.Columns.Add("GRN_DET_ID", typeof(int));
                tbl.Columns.Add("ITEM_ID", typeof(int));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("PO_DET_ID", typeof(long));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("VAT_PERC", typeof(decimal));
                tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
                tbl.Columns.Add("GRN_STORE_ID", typeof(int));
                tbl.Columns.Add("RETURN_AMOUNT", typeof(float));

                if (purchHeader.PurchDetails != null && purchHeader.PurchDetails.Any())
                {
                    foreach (var ur in purchHeader.PurchDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["PURCH_ID"] = ur.PURCH_ID;
                        dRow["GRN_DET_ID"] = (object?)ur.GRN_DET_ID ?? DBNull.Value;
                        dRow["ITEM_ID"] = (object?)ur.ITEM_ID ?? DBNull.Value;
                        dRow["PACKING"] = ur.PACKING ?? string.Empty;
                        dRow["QUANTITY"] = (object?)ur.QUANTITY ?? DBNull.Value;
                        dRow["RATE"] = (object?)ur.RATE ?? DBNull.Value;
                        dRow["AMOUNT"] = (object?)ur.AMOUNT ?? DBNull.Value;
                        dRow["RETURN_QTY"] = (object?)ur.RETURN_QTY ?? DBNull.Value;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC ?? string.Empty;
                        dRow["PO_DET_ID"] = (object?)ur.PO_DET_ID ?? DBNull.Value;
                        dRow["UOM"] = ur.UOM ?? string.Empty;
                        dRow["DISC_PERCENT"] = (object?)ur.DISC_PERCENT ?? DBNull.Value;
                        dRow["COST"] = (object?)ur.COST ?? DBNull.Value;
                        dRow["SUPP_PRICE"] = (object?)ur.SUPP_PRICE ?? DBNull.Value;
                        dRow["SUPP_AMOUNT"] = (object?)ur.SUPP_AMOUNT ?? DBNull.Value;
                        dRow["VAT_PERC"] = (object?)ur.VAT_PERC ?? DBNull.Value;
                        dRow["VAT_AMOUNT"] = (object?)ur.VAT_AMOUNT ?? DBNull.Value;
                        dRow["GRN_STORE_ID"] = (object?)ur.GRN_STORE_ID ?? DBNull.Value;
                        dRow["RETURN_AMOUNT"] = ur.RETURN_AMOUNT;

                        tbl.Rows.Add(dRow);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCH";

                cmd.Parameters.AddWithValue("ACTION", 6);
                cmd.Parameters.AddWithValue("@ID", purchHeader.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", purchHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@USER_ID", purchHeader.USER_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", purchHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PURCH_DATE", purchHeader.PURCH_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IS_CREDIT", purchHeader.IS_CREDIT);
                cmd.Parameters.AddWithValue("@SUPP_ID", purchHeader.SUPP_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_NO", purchHeader.SUPP_INV_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_INV_DATE", purchHeader.SUPP_INV_DATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_ID", purchHeader.PO_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PO_NO", purchHeader.PO_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FIN_ID", purchHeader.FIN_ID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PURCH_TYPE", purchHeader.PURCH_TYPE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", purchHeader.DISCOUNT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", purchHeader.SUPP_GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", purchHeader.SUPP_NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", purchHeader.EXCHANGE_RATE ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", purchHeader.GROSS_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_DESCRIPTION", purchHeader.CHARGE_DESCRIPTION ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CHARGE_AMOUNT", purchHeader.CHARGE_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@VAT_AMOUNT", purchHeader.VAT_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", purchHeader.NET_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RETURN_AMOUNT", purchHeader.RETURN_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ADJ_AMOUNT", purchHeader.ADJ_AMOUNT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PAID_AMOUNT", purchHeader.PAID_AMOUNT);

                cmd.Parameters.AddWithValue("@UDT_TB_PURCH_DETAIL", tbl);


                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();

                cmd1.Connection = connection;
                cmd1.Transaction = objtrans;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(ID) FROM TB_WORKSHEET";


                Int32 UserID = ADO.ToInt32(cmd1.ExecuteScalar());

                // Commit the transaction if everything is successful
                objtrans.Commit();

                return UserID;
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
                cmd.CommandText = "SP_TB_PURCH";
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
        public List<PurchaseInvoice> GetPurchaseInvoiceList()
        {
            List<PurchaseInvoice> invoiceList = new List<PurchaseInvoice>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_PURCH";
            cmd.Parameters.AddWithValue("@ACTION", 0);  

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            foreach (DataRow dr in tbl.Rows)
            {
                invoiceList.Add(new PurchaseInvoice
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    PURCH_NO = ADO.ToString(dr["PURCH_NO"]),
                    PURCH_DATE = Convert.ToDateTime(dr["PURCH_DATE"]),
                    SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                    STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                    STORE_NAME = ADO.ToString(dr["STORE"]),
                    SUPPPLIER_NAME = ADO.ToString(dr["SUPP_NAME"]),
                    NET_AMOUNT = ADO.ToFloat(dr["NET_AMOUNT"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    STATUS = ADO.ToString(dr["STATUS"]),
                    PO_NO = ADO.ToString(dr["PO_NO"])
                });
            }

            connection.Close();
            return invoiceList;
        }

    }
}