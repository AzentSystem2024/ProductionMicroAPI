using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class PurchaseOrderService:IPurchaseOrderService
    {
        public List<SupplierList> GetSupplier(SupplierInput input)
        {
            List<SupplierList> worksheetList = new List<SupplierList>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_SUPPLIER_LIST";
            cmd.Parameters.AddWithValue("@SUPP_ID", input.SUPP_ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new SupplierList
                {
                    ITEM_CODE = ADO.ToString(dr["ITEM_CODE"]),
                    LAST_PO_DATE = ADO.TODateString(dr["LAST_PO_DATE"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    UOM = ADO.ToString(dr["UOM"]),
                    PURCH_PRICE = ADO.ToString(dr["PURCH_PRICE"]),
                    COST = ADO.ToString(dr["COST"]),
                    VAT_CLASS_ID = ADO.ToInt32(dr["VAT_CLASS_ID"]),
                    VAT_NAME = ADO.ToString(dr["VAT_NAME"]),
                    VAT_PERC = ADO.ToString(dr["VAT_PERC"]),
                    CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]),
                    CURRENCY_NAME = ADO.ToString(dr["CURRENCY_NAME"]),
                    VAT_RULE_ID = ADO.ToInt32(dr["VAT_RULE_ID"]),
                    VAT_RULE_NAME = ADO.ToString(dr["VAT_RULE_NAME"]),
                    EXCHANGE = ADO.ToString(dr["EXCHANGE"]),
                    CURRENCY_CODE = ADO.ToString(dr["CODE"]),
                    CURRENCY_SYMBOL = ADO.ToString(dr["SYMBOL"]),
                    ITEM_ID = ADO.ToInt32(dr["ITEM_ID"]),
                    PACKING_ID = ADO.ToInt32(dr["PACKING_ID"]),
                    PACKING_NAME = ADO.ToString(dr["PACKING_NAME"]),
                    SUPPLIER_MAIL = ADO.ToString(dr["EMAIL"]),
                });
            }
            connection.Close();

            return worksheetList;
        }

        public Int32 Insert(PurchaseOrderHeader worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("PO_ID", typeof(Int32));
                tbl.Columns.Add("JOB_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("TAX_PERCENT", typeof(decimal));
                tbl.Columns.Add("TAX_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("CREATE_STORE_ID", typeof(Int32));

                if (worksheet.PoDetails != null && worksheet.PoDetails.Any())
                {
                    foreach (PurchaseOrderDetail ur in worksheet.PoDetails)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID ?? (object)DBNull.Value;
                        dRow["PO_ID"] = ur.PO_ID;
                        dRow["JOB_ID"] = ur.JOB_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["PACKING"] = ur.PACKING;
                        dRow["PRICE"] = ur.PRICE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["TAX_PERCENT"] = ur.TAX_PERCENT;
                        dRow["TAX_AMOUNT"] = ur.TAX_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC;
                        dRow["UOM"] = ur.UOM;
                        dRow["GRN_QTY"] = ur.GRN_QTY;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["CREATE_STORE_ID"] = ur.CREATE_STORE_ID ?? (object)DBNull.Value;

                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_ORDER";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@COMPANY_ID", worksheet.COMPANY_ID);
                //cmd.Parameters.AddWithValue("@STORE_ID", worksheet.STORE_ID);
                // cmd.Parameters.AddWithValue("PO_NO", worksheet.PO_NO);
                cmd.Parameters.AddWithValue("@PO_DATE", worksheet.PO_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", worksheet.SUPP_ID);
                cmd.Parameters.AddWithValue("@SUPP_CONTACT", worksheet.SUPP_CONTACT);
                cmd.Parameters.AddWithValue("@SUPP_ADDRESS", worksheet.SUPP_ADDRESS);
                cmd.Parameters.AddWithValue("@SUPP_MOBILE", worksheet.SUPP_MOBILE);
                cmd.Parameters.AddWithValue("@REF_NO", worksheet.REF_NO);
                cmd.Parameters.AddWithValue("@PAY_TERM_ID", worksheet.PAY_TERM_ID);
                cmd.Parameters.AddWithValue("@DELIVERY_TERM_ID", worksheet.DELIVERY_TERM_ID);

                // cmd.Parameters.AddWithValue("PO_STATUS", worksheet.PO_STATUS);
                cmd.Parameters.AddWithValue("@NOTES", worksheet.NOTES);
                cmd.Parameters.AddWithValue("@GROSS_AMOUNT", worksheet.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@TAX_AMOUNT", worksheet.TAX_AMOUNT);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", worksheet.NET_AMOUNT);
                //cmd.Parameters.AddWithValue("TRANS_ID", worksheet.TRANS_ID);
                cmd.Parameters.AddWithValue("@FIN_ID", worksheet.FIN_ID);
                cmd.Parameters.AddWithValue("@SHIP_TO", worksheet.SHIP_TO);
                cmd.Parameters.AddWithValue("@PURPOSE", worksheet.PURPOSE);
                cmd.Parameters.AddWithValue("@LOCATION", worksheet.LOCATION);
                cmd.Parameters.AddWithValue("@CONTACT_NAME", worksheet.CONTACT_NAME);
                cmd.Parameters.AddWithValue("@CONTACT_MOBILE", worksheet.CONTACT_MOBILE);
                cmd.Parameters.AddWithValue("@DELIVERY_DESC", worksheet.DELIVERY_DESC);
                cmd.Parameters.AddWithValue("@ISSUED_EMP_ID", worksheet.ISSUED_EMP_ID);
                // cmd.Parameters.AddWithValue("PO_TYPE", worksheet.PO_TYPE);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", worksheet.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", worksheet.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", worksheet.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("@CURRENCY_ID", worksheet.CURRENCY_ID);
                cmd.Parameters.AddWithValue("@NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@DELIVERY_DATE", worksheet.DELIVERY_DATE);
                cmd.Parameters.AddWithValue("@USER_ID", worksheet.USER_ID);

                //cmd.Parameters.AddWithValue("CREATED_STORE_ID", worksheet.CREATED_STORE_ID);

                cmd.Parameters.AddWithValue("@UDT_TB_PO_DETAIL", tbl);

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

        public List<PurchaseOrderHeader> GetPOList(Int32 intUserID)
        {
            List<PurchaseOrderHeader> worksheetList = new List<PurchaseOrderHeader>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_PURCHASE_ORDER";
            cmd.Parameters.AddWithValue("@ACTION", 0);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new PurchaseOrderHeader
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    PO_NO = ADO.ToString(dr["PO_NO"]),
                    PO_DATE = Convert.ToDateTime(dr["PO_DATE"]),
                    SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                    CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]),
                    //STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                    STATUS_ID = ADO.ToInt32(dr["STATUS_ID"]),
                    SUPP_NAME = ADO.ToString(dr["SUPP_NAME"]),
                    NET_AMOUNT = ADO.ToFloat(dr["NET_AMOUNT"]),
                    CURRENCY = ADO.ToString(dr["CURRENCY"]),
                    STORE = ADO.ToString(dr["STORE"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    STATUS = ADO.ToString(dr["STATUS"]),
                    TRANS_ID = dr["TRANS_ID"] != DBNull.Value ? Convert.ToInt32(dr["TRANS_ID"]) : 0
                });
            }
            connection.Close();

            return worksheetList;
        }

        public Int32 Update(PurchaseOrderHeader worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("PO_ID", typeof(Int32));
                tbl.Columns.Add("JOB_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("TAX_PERCENT", typeof(decimal));
                tbl.Columns.Add("TAX_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("CREATE_STORE_ID", typeof(Int32));

                if (worksheet.PoDetails != null && worksheet.PoDetails.Any())
                {
                    foreach (PurchaseOrderDetail ur in worksheet.PoDetails)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID ?? (object)DBNull.Value;
                        dRow["PO_ID"] = ur.PO_ID;
                        dRow["JOB_ID"] = ur.JOB_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["PACKING"] = ur.PACKING;
                        dRow["PRICE"] = ur.PRICE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["TAX_PERCENT"] = ur.TAX_PERCENT;
                        dRow["TAX_AMOUNT"] = ur.TAX_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC;
                        dRow["UOM"] = ur.UOM;
                        dRow["GRN_QTY"] = ur.GRN_QTY;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["CREATE_STORE_ID"] = ur.CREATE_STORE_ID ?? (object)DBNull.Value;

                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_ORDER";

                cmd.Parameters.AddWithValue("ACTION", 2);

                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                //cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                // cmd.Parameters.AddWithValue("PO_NO", worksheet.PO_NO);
                cmd.Parameters.AddWithValue("PO_DATE", worksheet.PO_DATE);
                cmd.Parameters.AddWithValue("SUPP_ID", worksheet.SUPP_ID);
                cmd.Parameters.AddWithValue("SUPP_CONTACT", worksheet.SUPP_CONTACT);
                cmd.Parameters.AddWithValue("SUPP_ADDRESS", worksheet.SUPP_ADDRESS);
                cmd.Parameters.AddWithValue("SUPP_MOBILE", worksheet.SUPP_MOBILE);
                cmd.Parameters.AddWithValue("REF_NO", worksheet.REF_NO);
                cmd.Parameters.AddWithValue("PAY_TERM_ID", worksheet.PAY_TERM_ID);
                cmd.Parameters.AddWithValue("DELIVERY_TERM_ID", worksheet.DELIVERY_TERM_ID);
                //cmd.Parameters.AddWithValue("VALID_DAYS", worksheet.VALID_DAYS);
                // cmd.Parameters.AddWithValue("PO_STATUS", worksheet.PO_STATUS);
                cmd.Parameters.AddWithValue("NOTES", worksheet.NOTES);
                cmd.Parameters.AddWithValue("GROSS_AMOUNT", worksheet.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("TAX_AMOUNT", worksheet.TAX_AMOUNT);
                cmd.Parameters.AddWithValue("NET_AMOUNT", worksheet.NET_AMOUNT);
                //cmd.Parameters.AddWithValue("TRANS_ID", worksheet.TRANS_ID);
                cmd.Parameters.AddWithValue("FIN_ID", worksheet.FIN_ID);
                cmd.Parameters.AddWithValue("SHIP_TO", worksheet.SHIP_TO);
                cmd.Parameters.AddWithValue("PURPOSE", worksheet.PURPOSE);
                cmd.Parameters.AddWithValue("LOCATION", worksheet.LOCATION);
                cmd.Parameters.AddWithValue("CONTACT_NAME", worksheet.CONTACT_NAME);
                cmd.Parameters.AddWithValue("CONTACT_MOBILE", worksheet.CONTACT_MOBILE);
                cmd.Parameters.AddWithValue("DELIVERY_DESC", worksheet.DELIVERY_DESC);
                cmd.Parameters.AddWithValue("ISSUED_EMP_ID", worksheet.ISSUED_EMP_ID);
                // cmd.Parameters.AddWithValue("PO_TYPE", worksheet.PO_TYPE);
                cmd.Parameters.AddWithValue("SUPP_GROSS_AMOUNT", worksheet.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("SUPP_NET_AMOUNT", worksheet.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("EXCHANGE_RATE", worksheet.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("CURRENCY_ID", worksheet.CURRENCY_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("DELIVERY_DATE", worksheet.DELIVERY_DATE);
                cmd.Parameters.AddWithValue("@USER_ID", worksheet.USER_ID);

                //cmd.Parameters.AddWithValue("CREATED_STORE_ID", worksheet.CREATED_STORE_ID);

                cmd.Parameters.AddWithValue("@UDT_TB_PO_DETAIL", tbl);

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

        public Int32 Verify(PurchaseOrderHeader worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("PO_ID", typeof(Int32));
                tbl.Columns.Add("JOB_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("TAX_PERCENT", typeof(decimal));
                tbl.Columns.Add("TAX_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("CREATE_STORE_ID", typeof(Int32));

                if (worksheet.PoDetails != null && worksheet.PoDetails.Any())
                {
                    foreach (PurchaseOrderDetail ur in worksheet.PoDetails)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["PO_ID"] = ur.PO_ID;
                        dRow["JOB_ID"] = ur.JOB_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["PACKING"] = ur.PACKING;
                        dRow["PRICE"] = ur.PRICE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["TAX_PERCENT"] = ur.TAX_PERCENT;
                        dRow["TAX_AMOUNT"] = ur.TAX_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC;
                        dRow["UOM"] = ur.UOM;
                        dRow["GRN_QTY"] = ur.GRN_QTY;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["CREATE_STORE_ID"] = ur.CREATE_STORE_ID;

                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_ORDER";

                cmd.Parameters.AddWithValue("ACTION", 5);

                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
               // cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                // cmd.Parameters.AddWithValue("PO_NO", worksheet.PO_NO);
                cmd.Parameters.AddWithValue("PO_DATE", worksheet.PO_DATE);
                cmd.Parameters.AddWithValue("SUPP_ID", worksheet.SUPP_ID);
                cmd.Parameters.AddWithValue("SUPP_CONTACT", worksheet.SUPP_CONTACT);
                cmd.Parameters.AddWithValue("SUPP_ADDRESS", worksheet.SUPP_ADDRESS);
                cmd.Parameters.AddWithValue("SUPP_MOBILE", worksheet.SUPP_MOBILE);
                cmd.Parameters.AddWithValue("REF_NO", worksheet.REF_NO);
                cmd.Parameters.AddWithValue("PAY_TERM_ID", worksheet.PAY_TERM_ID);
                cmd.Parameters.AddWithValue("DELIVERY_TERM_ID", worksheet.DELIVERY_TERM_ID);
                //cmd.Parameters.AddWithValue("VALID_DAYS", worksheet.VALID_DAYS);
                // cmd.Parameters.AddWithValue("PO_STATUS", worksheet.PO_STATUS);
                cmd.Parameters.AddWithValue("NOTES", worksheet.NOTES);
                cmd.Parameters.AddWithValue("GROSS_AMOUNT", worksheet.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("TAX_AMOUNT", worksheet.TAX_AMOUNT);
                cmd.Parameters.AddWithValue("NET_AMOUNT", worksheet.NET_AMOUNT);
                //cmd.Parameters.AddWithValue("TRANS_ID", worksheet.TRANS_ID);
                cmd.Parameters.AddWithValue("FIN_ID", worksheet.FIN_ID);
                cmd.Parameters.AddWithValue("SHIP_TO", worksheet.SHIP_TO);
                cmd.Parameters.AddWithValue("PURPOSE", worksheet.PURPOSE);
                cmd.Parameters.AddWithValue("LOCATION", worksheet.LOCATION);
                cmd.Parameters.AddWithValue("CONTACT_NAME", worksheet.CONTACT_NAME);
                cmd.Parameters.AddWithValue("CONTACT_MOBILE", worksheet.CONTACT_MOBILE);
                cmd.Parameters.AddWithValue("DELIVERY_DESC", worksheet.DELIVERY_DESC);
                cmd.Parameters.AddWithValue("ISSUED_EMP_ID", worksheet.ISSUED_EMP_ID);
                // cmd.Parameters.AddWithValue("PO_TYPE", worksheet.PO_TYPE);
                cmd.Parameters.AddWithValue("SUPP_GROSS_AMOUNT", worksheet.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("SUPP_NET_AMOUNT", worksheet.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("EXCHANGE_RATE", worksheet.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("CURRENCY_ID", worksheet.CURRENCY_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("DELIVERY_DATE", worksheet.DELIVERY_DATE);
                cmd.Parameters.AddWithValue("@USER_ID", worksheet.USER_ID);

                //cmd.Parameters.AddWithValue("CREATED_STORE_ID", worksheet.CREATED_STORE_ID);

                cmd.Parameters.AddWithValue("@UDT_TB_PO_DETAIL", tbl);

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


        public Int32 Approval(PurchaseOrderHeader worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("PO_ID", typeof(Int32));
                tbl.Columns.Add("JOB_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("PACKING", typeof(string));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("TAX_PERCENT", typeof(decimal));
                tbl.Columns.Add("TAX_AMOUNT", typeof(decimal));
                tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
                tbl.Columns.Add("ITEM_DESC", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("GRN_QTY", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("CREATE_STORE_ID", typeof(Int32));

                if (worksheet.PoDetails != null && worksheet.PoDetails.Any())
                {
                    foreach (PurchaseOrderDetail ur in worksheet.PoDetails)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID ?? (object)DBNull.Value;
                        dRow["PO_ID"] = ur.PO_ID;
                        dRow["JOB_ID"] = ur.JOB_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["PACKING"] = ur.PACKING;
                        dRow["PRICE"] = ur.PRICE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["TAX_PERCENT"] = ur.TAX_PERCENT;
                        dRow["TAX_AMOUNT"] = ur.TAX_AMOUNT;
                        dRow["TOTAL_AMOUNT"] = ur.TOTAL_AMOUNT;
                        dRow["ITEM_DESC"] = ur.ITEM_DESC;
                        dRow["UOM"] = ur.UOM;
                        dRow["GRN_QTY"] = ur.GRN_QTY;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["CREATE_STORE_ID"] = ur.CREATE_STORE_ID ?? (object)DBNull.Value;

                        tbl.Rows.Add(dRow);
                    }
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PURCHASE_ORDER";

                cmd.Parameters.AddWithValue("ACTION", 6);

                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                //cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                // cmd.Parameters.AddWithValue("PO_NO", worksheet.PO_NO);
                cmd.Parameters.AddWithValue("PO_DATE", worksheet.PO_DATE);
                cmd.Parameters.AddWithValue("SUPP_ID", worksheet.SUPP_ID);
                cmd.Parameters.AddWithValue("SUPP_CONTACT", worksheet.SUPP_CONTACT);
                cmd.Parameters.AddWithValue("SUPP_ADDRESS", worksheet.SUPP_ADDRESS);
                cmd.Parameters.AddWithValue("SUPP_MOBILE", worksheet.SUPP_MOBILE);
                cmd.Parameters.AddWithValue("REF_NO", worksheet.REF_NO);
                cmd.Parameters.AddWithValue("PAY_TERM_ID", worksheet.PAY_TERM_ID);
                cmd.Parameters.AddWithValue("DELIVERY_TERM_ID", worksheet.DELIVERY_TERM_ID);
                //cmd.Parameters.AddWithValue("VALID_DAYS", worksheet.VALID_DAYS);
                // cmd.Parameters.AddWithValue("PO_STATUS", worksheet.PO_STATUS);
                cmd.Parameters.AddWithValue("NOTES", worksheet.NOTES);
                cmd.Parameters.AddWithValue("GROSS_AMOUNT", worksheet.GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("TAX_AMOUNT", worksheet.TAX_AMOUNT);
                cmd.Parameters.AddWithValue("NET_AMOUNT", worksheet.NET_AMOUNT);
                //cmd.Parameters.AddWithValue("TRANS_ID", worksheet.TRANS_ID);
                cmd.Parameters.AddWithValue("FIN_ID", worksheet.FIN_ID);
                cmd.Parameters.AddWithValue("SHIP_TO", worksheet.SHIP_TO);
                cmd.Parameters.AddWithValue("PURPOSE", worksheet.PURPOSE);
                cmd.Parameters.AddWithValue("LOCATION", worksheet.LOCATION);
                cmd.Parameters.AddWithValue("CONTACT_NAME", worksheet.CONTACT_NAME);
                cmd.Parameters.AddWithValue("CONTACT_MOBILE", worksheet.CONTACT_MOBILE);
                cmd.Parameters.AddWithValue("DELIVERY_DESC", worksheet.DELIVERY_DESC);
                cmd.Parameters.AddWithValue("ISSUED_EMP_ID", worksheet.ISSUED_EMP_ID);
                // cmd.Parameters.AddWithValue("PO_TYPE", worksheet.PO_TYPE);
                cmd.Parameters.AddWithValue("SUPP_GROSS_AMOUNT", worksheet.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("SUPP_NET_AMOUNT", worksheet.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("EXCHANGE_RATE", worksheet.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("CURRENCY_ID", worksheet.CURRENCY_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("DELIVERY_DATE", worksheet.DELIVERY_DATE);
                cmd.Parameters.AddWithValue("@USER_ID", worksheet.USER_ID);

                //cmd.Parameters.AddWithValue("CREATED_STORE_ID", worksheet.CREATED_STORE_ID);

                cmd.Parameters.AddWithValue("@UDT_TB_PO_DETAIL", tbl);

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
        public List<ItemList> GetItemList(ItemInput input)
        {
            List<ItemList> worksheetList = new List<ItemList>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_PURCHASE_ORDER";
            cmd.Parameters.AddWithValue("ACTION", 3);
            cmd.Parameters.AddWithValue("@ITEM_ID", input.ITEM_ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new ItemList
                {
                    PO_DATE = Convert.ToDateTime(dr["PO_DATE"]),
                    PO_NO = ADO.ToString(dr["PO_NO"]),
                    QUANTITY = ADO.ToString(dr["QUANTITY"]),
                    SUPP_PRICE = ADO.ToString(dr["SUPP_PRICE"]),
                    PRICE = ADO.ToString(dr["PRICE"]),
                    CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]),
                    CURRENCY_NAME = ADO.ToString(dr["CURRENCY_NAME"])

                });
            }
            connection.Close();
            return worksheetList;
        }

        public PurchaseOrderHeader GetPurchaseOrder(int id)
        {
            PurchaseOrderHeader schema = new PurchaseOrderHeader();
            List<PurchaseOrderDetail> schemaEntries = new List<PurchaseOrderDetail>();

            try
            {
                string strSQL = "SELECT TB_PO_HEADER.*, TB_STORES.STORE_NAME, TB_SUPPLIER.SUPP_NAME," +
                    "TB_PAYMENT_TERMS.DESCRIPTION as PAYMENT_NAME, " +
                    "TB_DELIVERY_TERMS.DESCRIPTION AS DELIVERY_TERM, TB_STATUS.STATUS_DESC, " +
                    "TB_AC_TRANS_HEADER.NARRATION AS NARRATION, " +
                    "TB_EMPLOYEE.EMP_NAME FROM TB_PO_HEADER " +
                    "LEFT JOIN TB_STORES ON TB_PO_HEADER.STORE_ID = TB_STORES.ID " +
                    "LEFT JOIN TB_SUPPLIER on TB_PO_HEADER.SUPP_ID = TB_SUPPLIER.ID " +
                    "LEFT JOIN TB_PAYMENT_TERMS on TB_PO_HEADER.PAY_TERM_ID = TB_PAYMENT_TERMS.ID " +
                    "LEFT JOIN TB_DELIVERY_TERMS on TB_PO_HEADER.DELIVERY_TERM_ID = TB_DELIVERY_TERMS.ID " +
                    "LEFT JOIN TB_STATUS on TB_PO_HEADER.PO_STATUS = TB_STATUS.ID " +
                    "LEFT JOIN TB_EMPLOYEE on TB_PO_HEADER.ISSUED_EMP_ID = TB_EMPLOYEE.ID " +
                    "LEFT JOIN TB_CURRENCY on TB_PO_HEADER.CURRENCY_ID = TB_CURRENCY.ID " +
                    "LEFT JOIN TB_AC_TRANS_HEADER ON TB_PO_HEADER.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +

                    "WHERE TB_PO_HEADER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "PromotionSchema");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    schema = new PurchaseOrderHeader
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),
                        //STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                        STORE = ADO.ToString(dr["STORE_NAME"]),
                        PO_NO = ADO.ToString(dr["PO_NO"]),
                        PO_DATE = Convert.ToDateTime(dr["PO_DATE"]),
                        SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                        SUPP_NAME = ADO.ToString(dr["SUPP_NAME"]),
                        SUPP_CONTACT = ADO.ToString(dr["SUPP_CONTACT"]),
                        SUPP_ADDRESS = ADO.ToString(dr["SUPP_ADDRESS"]),
                        SUPP_MOBILE = ADO.ToString(dr["SUPP_MOBILE"]),
                        REF_NO = ADO.ToString(dr["REF_NO"]),
                        PAY_TERM_ID = ADO.ToInt32(dr["PAY_TERM_ID"]),
                        DELIVERY_TERM_ID = ADO.ToInt32(dr["DELIVERY_TERM_ID"]),
                        STATUS_ID = ADO.ToInt32(dr["PO_STATUS"]),
                        STATUS = ADO.ToString(dr["STATUS_DESC"]),
                        NOTES = ADO.ToString(dr["NOTES"]),
                        GROSS_AMOUNT = ADO.ToFloat(dr["GROSS_AMOUNT"]),
                        TAX_AMOUNT = ADO.ToFloat(dr["TAX_AMOUNT"]),
                        NET_AMOUNT = ADO.ToFloat(dr["NET_AMOUNT"]),
                        FIN_ID = ADO.ToInt32(dr["FIN_ID"]),
                        SHIP_TO = ADO.ToString(dr["SHIP_TO"]),
                        PURPOSE = ADO.ToString(dr["PURPOSE"]),
                        LOCATION = ADO.ToString(dr["LOCATION"]),
                        CONTACT_NAME = ADO.ToString(dr["CONTACT_NAME"]),
                        CONTACT_MOBILE = ADO.ToString(dr["CONTACT_MOBILE"]),
                        DELIVERY_DESC = ADO.ToString(dr["DELIVERY_DESC"]),
                        ISSUED_EMP_ID = ADO.ToInt32(dr["ISSUED_EMP_ID"]),
                        PO_TYPE = ADO.ToInt32(dr["PO_TYPE"]),
                        SUPP_GROSS_AMOUNT = ADO.ToFloat(dr["SUPP_GROSS_AMOUNT"]),
                        SUPP_NET_AMOUNT = ADO.ToFloat(dr["SUPP_NET_AMOUNT"]),
                        EXCHANGE_RATE = ADO.ToFloat(dr["EXCHANGE_RATE"]),
                        CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]),
                        //CURRENCY = ADO.ToString(dr["CURRENCY"]),
                        //NARRATION = ADO.ToString(dr["NARRATION"]),
                        DELIVERY_DATE = Convert.ToDateTime(dr["DELIVERY_DATE"]),
                        PAY_TERM = ADO.ToString(dr["PAYMENT_NAME"]),
                        DELIVERY_TERM = ADO.ToString(dr["DELIVERY_TERM"]),
                        NARRATION = ADO.ToString(dr["NARRATION"]),
                        TRANS_ID = ADO.ToInt32(dr["TRANS_ID"])

                    };
                }

                strSQL = "SELECT TB_PO_DETAIL.*, TB_ITEMS.DESCRIPTION, TB_ITEMS.ITEM_CODE ," +
                    "TB_STORES.STORE_NAME FROM TB_PO_DETAIL " +
                    " LEFT JOIN TB_ITEMS ON TB_PO_DETAIL.ITEM_ID = TB_ITEMS.ID" +
                    " LEFT JOIN TB_STORES ON TB_PO_DETAIL.STORE_ID = TB_STORES.ID" +
                    " WHERE TB_PO_DETAIL.PO_ID = " + id;

                DataTable tblItemSuppliers1 = ADO.GetDataTable(strSQL, "SchemaEntry");

                foreach (DataRow dr3 in tblItemSuppliers1.Rows)
                {
                    schemaEntries.Add(new PurchaseOrderDetail
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        COMPANY_ID = ADO.ToInt32(dr3["COMPANY_ID"]),
                        PO_ID = ADO.ToInt32(dr3["PO_ID"]),
                        JOB_ID = ADO.ToInt32(dr3["JOB_ID"]),
                        ITEM_ID = ADO.ToInt32(dr3["ITEM_ID"]),
                        QUANTITY = ADO.ToFloat(dr3["QUANTITY"]),
                        PACKING = ADO.ToString(dr3["PACKING"]),
                        PRICE = ADO.ToFloat(dr3["PRICE"]),
                        AMOUNT = ADO.ToFloat(dr3["AMOUNT"]),
                        DISC_PERCENT = ADO.ToFloat(dr3["DISC_PERCENT"]),
                        TAX_PERCENT = ADO.ToDecimal(dr3["TAX_PERCENT"]),
                        TAX_AMOUNT = ADO.ToDecimal(dr3["TAX_AMOUNT"]),
                        TOTAL_AMOUNT = ADO.ToDecimal(dr3["TOTAL_AMOUNT"]),
                        ITEM_DESC = ADO.ToString(dr3["ITEM_DESC"]),
                        UOM = ADO.ToString(dr3["UOM"]),
                        GRN_QTY = ADO.ToFloat(dr3["GRN_QTY"]),
                        INVOICE_QTY = ADO.ToFloat(dr3["INVOICE_QTY"]),
                        SUPP_PRICE = ADO.ToFloat(dr3["SUPP_PRICE"]),
                        SUPP_AMOUNT = ADO.ToFloat(dr3["SUPP_AMOUNT"]),
                        ITEM_CODE = ADO.ToString(dr3["ITEM_CODE"]),

                    });
                }

                schema.PoDetails = schemaEntries;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return schema;
        }
    }
}
