using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class GRNService:IGRNService
    {
        public List<PO> GetPendingPo(poinput input)
        {
            List<PO> POlist = new List<PO>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_PENDING_PO";
            cmd.Parameters.AddWithValue("@STORE_ID", input.STORE_ID);
            cmd.Parameters.AddWithValue("@SUPP_ID", input.SUPP_ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            foreach (DataRow dr in tbl.Rows)
            {
                // Debug: Log the raw values for SUPP_ID and SUPP_NAME
                Console.WriteLine($"Raw SUPP_ID: {dr["SUPP_ID"]}, Raw SUPP_NAME: {dr["SUPP_NAME"]}");

                POlist.Add(new PO
                {
                    PO_NO = ADO.ToString(dr["PO_NO"]),
                    PO_DATE = Convert.ToDateTime(dr["PO_DATE"]),
                    SUPP_NAME = dr["SUPP_NAME"] == DBNull.Value ? "N/A" : ADO.ToString(dr["SUPP_NAME"]),
                    PO_ID = ADO.ToInt32(dr["PO_ID"]),
                    SUPP_ID = dr["SUPP_ID"] == DBNull.Value ? 0 : ADO.ToInt32(dr["SUPP_ID"])
                });
            }

            connection.Close();
            return POlist;
        }
    
        public GRNResponse GetPoList(PODetailsInput input)
        {
            GRNResponse response = new GRNResponse();
            List<PODetails> poDetailsList = new List<PODetails>();
            List<LandedCosts> LandedCostsList = new List<LandedCosts>();

            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_PO";
            cmd.Parameters.AddWithValue("@PO_ID", input.PO_ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            // Populate PODetails
            foreach (DataRow dr in tbl.Rows)
            {
                PODetails poDetail = new PODetails
                {
                    ITEM_CODE = ADO.ToString(dr["ITEM_CODE"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    PRICE = ADO.ToDecimal(dr["PRICE"]),
                    QUANTITY = ADO.ToDecimal(dr["QUANTITY"]),
                    UOM = ADO.ToString(dr["UOM"]),
                    UOM_MULTIPLE = ADO.ToInt32(dr["UOM_MULTPLE"]),
                    UOM_PURCH = ADO.ToString(dr["UOM_PURCH"]),
                    GRN_QTY = ADO.ToFloat(dr["GRN_QTY"]),
                    CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]),
                    CURRENCY_NAME = ADO.ToString(dr["CURRENCY_NAME"]),
                    DISC_PERCENT = ADO.ToFloat(dr["DISC_PERCENT"]),
                    CURRENCY_SYMBOL = ADO.ToString(dr["CURRENCY_SYMBOL"]),
                    //SUPP_AMOUNT = ADO.ToFloat(dr["SUPP_AMOUNT"]),
                    SUPP_PRICE = ADO.ToFloat(dr["SUPP_PRICE"]),
                    ITEM_ID = ADO.ToInt32(dr["ITEM_ID"]),
                    PO_DETAIL_ID = ADO.ToInt32(dr["PO_DETAIL_ID"]),
                };
                poDetailsList.Add(poDetail);
            }

            // Populate LandedCostss from separate query
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = connection;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT DISTINCT TB_SUPPLIER_COSTS.COST_ID, TB_LANDED_COSTS.DESCRIPTION AS LANDED_COST, " +
            "TB_LANDED_COSTS.IS_LOCAL_CURRENCY, TB_LANDED_COSTS.IS_FIXED_AMOUNT, TB_LANDED_COSTS.VALUE FROM TB_PO_DETAIL " +
            "LEFT JOIN TB_PO_HEADER ON TB_PO_DETAIL.PO_ID = TB_PO_HEADER.ID " +
            "LEFT JOIN TB_SUPPLIER_COSTS ON TB_PO_HEADER.SUPP_ID = TB_SUPPLIER_COSTS.SUPP_ID " +
            "LEFT JOIN TB_LANDED_COSTS ON TB_SUPPLIER_COSTS.COST_ID = TB_LANDED_COSTS.ID ";

            SqlDataAdapter LandedCostsDa = new SqlDataAdapter(cmd1);
            DataTable LandedCostsTbl = new DataTable();
            LandedCostsDa.Fill(LandedCostsTbl);

            // Populate LandedCostss
            foreach (DataRow lcDr in LandedCostsTbl.Rows)
            {
                LandedCosts LandedCosts = new LandedCosts
                {
                    ID = ADO.ToInt32(lcDr["COST_ID"]),
                    DESCRIPTION = ADO.ToString(lcDr["LANDED_COST"]),
                    IS_LOCAL_CURRENCY = ADO.Toboolean(lcDr["IS_LOCAL_CURRENCY"]),
                    IS_FIXED_AMOUNT = ADO.Toboolean(lcDr["IS_FIXED_AMOUNT"]),
                    VALUE = ADO.ToFloat(lcDr["VALUE"])
                };
                LandedCostsList.Add(LandedCosts);
            }

            // Assign the populated data to the response object
            response.Flag = 1; // Or any flag you want to return
            response.Message = "Success"; // Any message you want to include
            response.Podetails = poDetailsList; // List of PODetails
            response.LandedCosts = LandedCostsList; // List of LandedCostss

            connection.Close();
            return response;
        }
        public Int32 Insert(GRN grnHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_ID", typeof(Int32));
                tbl.Columns.Add("PO_DETAIL_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(Int32));

                if (grnHeader.GRNDetails != null && grnHeader.GRNDetails.Any())
                {
                    foreach (GRNDetail ur in grnHeader.GRNDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_ID"] = ur.GRN_ID;
                        dRow["PO_DETAIL_ID"] = ur.PO_DETAIL_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["COST"] = ur.COST;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["RETURN_QTY"] = ur.RECEIVED_QTY;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        tbl.Rows.Add(dRow);
                    }
                }

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("GRN_ID", typeof(Int32));
                tbl1.Columns.Add("STORE_ID", typeof(Int32));
                tbl1.Columns.Add("ITEM_ID", typeof(Int32));
                tbl1.Columns.Add("COST_ID", typeof(Int32));
                tbl1.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Item_Cost != null && grnHeader.GRN_Item_Cost.Any())
                {
                    foreach (GRN_ITEM_COST ur1 in grnHeader.GRN_Item_Cost)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["GRN_ID"] = ur1.GRN_ID;
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["ITEM_ID"] = ur1.ITEM_ID;
                        dRow1["COST_ID"] = ur1.COST_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl1.Rows.Add(dRow1);
                    }
                }
                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("STORE_ID", typeof(Int32));
                tbl2.Columns.Add("GRN_ID", typeof(Int32));
                tbl2.Columns.Add("COST_ID", typeof(Int32));
                tbl2.Columns.Add("PERCENT", typeof(float));
                tbl2.Columns.Add("AMOUNT_FC", typeof(float));
                tbl2.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Cost != null && grnHeader.GRN_Cost.Any())
                {
                    foreach (GRN_COST ur2 in grnHeader.GRN_Cost)
                    {
                        DataRow dRow2 = tbl2.NewRow();
                        dRow2["STORE_ID"] = ur2.STORE_ID;
                        dRow2["GRN_ID"] = ur2.GRN_ID;
                        dRow2["COST_ID"] = ur2.COST_ID;
                        dRow2["PERCENT"] = ur2.PERCENT;
                        dRow2["AMOUNT_FC"] = ur2.AMOUNT_FC;
                        dRow2["AMOUNT"] = ur2.AMOUNT;
                        tbl2.Rows.Add(dRow2);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_GRN";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@COMPANY_ID", grnHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", grnHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PO_ID", grnHeader.PO_ID);
                cmd.Parameters.AddWithValue("@GRN_DATE", grnHeader.GRN_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", grnHeader.SUPP_ID);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", grnHeader.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@TOTAL_COST", grnHeader.TOTAL_COST);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", grnHeader.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", grnHeader.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", grnHeader.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("@NARRATION", grnHeader.NARRATION);
                cmd.Parameters.AddWithValue("@USER_ID", grnHeader.USER_ID);

                cmd.Parameters.AddWithValue("@UDT_TB_GRN_DETAIL", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_ITEM_COST", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_COST", tbl2);


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
        public Int32 Update(GRN grnHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_ID", typeof(Int32));
                tbl.Columns.Add("PO_DETAIL_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(Int32));

                if (grnHeader.GRNDetails != null && grnHeader.GRNDetails.Any())
                {
                    foreach (GRNDetail ur in grnHeader.GRNDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_ID"] = ur.GRN_ID;
                        dRow["PO_DETAIL_ID"] = ur.PO_DETAIL_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["COST"] = ur.COST;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["RETURN_QTY"] = ur.RECEIVED_QTY;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        tbl.Rows.Add(dRow);
                    }
                }

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("GRN_ID", typeof(Int32));
                tbl1.Columns.Add("STORE_ID", typeof(Int32));
                tbl1.Columns.Add("ITEM_ID", typeof(Int32));
                tbl1.Columns.Add("COST_ID", typeof(Int32));
                tbl1.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Item_Cost != null && grnHeader.GRN_Item_Cost.Any())
                {
                    foreach (GRN_ITEM_COST ur1 in grnHeader.GRN_Item_Cost)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["GRN_ID"] = ur1.GRN_ID;
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["ITEM_ID"] = ur1.ITEM_ID;
                        dRow1["COST_ID"] = ur1.COST_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl1.Rows.Add(dRow1);
                    }
                }
                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("STORE_ID", typeof(Int32));
                tbl2.Columns.Add("GRN_ID", typeof(Int32));
                tbl2.Columns.Add("COST_ID", typeof(Int32));
                tbl2.Columns.Add("PERCENT", typeof(float));
                tbl2.Columns.Add("AMOUNT_FC", typeof(float));
                tbl2.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Cost != null && grnHeader.GRN_Cost.Any())
                {
                    foreach (GRN_COST ur2 in grnHeader.GRN_Cost)
                    {
                        DataRow dRow2 = tbl2.NewRow();
                        dRow2["STORE_ID"] = ur2.STORE_ID;
                        dRow2["GRN_ID"] = ur2.GRN_ID;
                        dRow2["COST_ID"] = ur2.COST_ID;
                        dRow2["PERCENT"] = ur2.PERCENT;
                        dRow2["AMOUNT_FC"] = ur2.AMOUNT_FC;
                        dRow2["AMOUNT"] = ur2.AMOUNT;
                        tbl2.Rows.Add(dRow2);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_GRN";

                cmd.Parameters.AddWithValue("ACTION", 2);
                cmd.Parameters.AddWithValue("@ID", grnHeader.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", grnHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", grnHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PO_ID", grnHeader.PO_ID);
                cmd.Parameters.AddWithValue("@GRN_DATE", grnHeader.GRN_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", grnHeader.SUPP_ID);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", grnHeader.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@TOTAL_COST", grnHeader.TOTAL_COST);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", grnHeader.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", grnHeader.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", grnHeader.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("@NARRATION", grnHeader.NARRATION);

                cmd.Parameters.AddWithValue("@UDT_TB_GRN_DETAIL", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_ITEM_COST", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_COST", tbl2);


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
        public Int32 Verify(GRN grnHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_ID", typeof(Int32));
                tbl.Columns.Add("PO_DETAIL_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(Int32));

                if (grnHeader.GRNDetails != null && grnHeader.GRNDetails.Any())
                {
                    foreach (GRNDetail ur in grnHeader.GRNDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_ID"] = ur.GRN_ID;
                        dRow["PO_DETAIL_ID"] = ur.PO_DETAIL_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["COST"] = ur.COST;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["RETURN_QTY"] = ur.RECEIVED_QTY;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        tbl.Rows.Add(dRow);
                    }
                }

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("GRN_ID", typeof(Int32));
                tbl1.Columns.Add("STORE_ID", typeof(Int32));
                tbl1.Columns.Add("ITEM_ID", typeof(Int32));
                tbl1.Columns.Add("COST_ID", typeof(Int32));
                tbl1.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Item_Cost != null && grnHeader.GRN_Item_Cost.Any())
                {
                    foreach (GRN_ITEM_COST ur1 in grnHeader.GRN_Item_Cost)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["GRN_ID"] = ur1.GRN_ID;
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["ITEM_ID"] = ur1.ITEM_ID;
                        dRow1["COST_ID"] = ur1.COST_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl1.Rows.Add(dRow1);
                    }
                }
                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("STORE_ID", typeof(Int32));
                tbl2.Columns.Add("GRN_ID", typeof(Int32));
                tbl2.Columns.Add("COST_ID", typeof(Int32));
                tbl2.Columns.Add("PERCENT", typeof(float));
                tbl2.Columns.Add("AMOUNT_FC", typeof(float));
                tbl2.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Cost != null && grnHeader.GRN_Cost.Any())
                {
                    foreach (GRN_COST ur2 in grnHeader.GRN_Cost)
                    {
                        DataRow dRow2 = tbl2.NewRow();
                        dRow2["STORE_ID"] = ur2.STORE_ID;
                        dRow2["GRN_ID"] = ur2.GRN_ID;
                        dRow2["COST_ID"] = ur2.COST_ID;
                        dRow2["PERCENT"] = ur2.PERCENT;
                        dRow2["AMOUNT_FC"] = ur2.AMOUNT_FC;
                        dRow2["AMOUNT"] = ur2.AMOUNT;
                        tbl2.Rows.Add(dRow2);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_GRN";

                cmd.Parameters.AddWithValue("ACTION", 5);
                cmd.Parameters.AddWithValue("@ID", grnHeader.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", grnHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", grnHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PO_ID", grnHeader.PO_ID);
                cmd.Parameters.AddWithValue("@GRN_DATE", grnHeader.GRN_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", grnHeader.SUPP_ID);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", grnHeader.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@TOTAL_COST", grnHeader.TOTAL_COST);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", grnHeader.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", grnHeader.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", grnHeader.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("@NARRATION", grnHeader.NARRATION);

                cmd.Parameters.AddWithValue("@UDT_TB_GRN_DETAIL", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_ITEM_COST", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_COST", tbl2);


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
        public GRN GetGRN(int id)
        {
            GRN grn = new GRN();
            List<GRNDetail> grndetails = new List<GRNDetail>();
            List<GRN_ITEM_COST> grnitemcost = new List<GRN_ITEM_COST>();
            List<GRN_COST> grncost = new List<GRN_COST>();

            try
            {
                string strSQL = "SELECT TB_GRN_HEADER.*, TB_STORES.STORE_NAME, TB_SUPPLIER.SUPP_NAME, TB_PO_HEADER.PO_NO, " +
                    "TB_CURRENCY.ID AS CURRENCY_ID , TB_CURRENCY.SYMBOL , TB_STATUS.STATUS_DESC AS STATUS, " +
                    "TB_AC_TRANS_HEADER.NARRATION AS NARRATION FROM TB_GRN_HEADER " +
                    "LEFT JOIN TB_STORES ON TB_GRN_HEADER.STORE_ID = TB_STORES.ID " +
                    "LEFT JOIN TB_SUPPLIER on TB_GRN_HEADER.SUPP_ID = TB_SUPPLIER.ID " +
                    "LEFT JOIN TB_AC_TRANS_HEADER ON TB_GRN_HEADER.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
                    "LEFT JOIN TB_CURRENCY ON TB_SUPPLIER.CURRENCY_ID=TB_CURRENCY.ID " +
                    "LEFT JOIN TB_PO_HEADER ON TB_GRN_HEADER.PO_ID = TB_PO_HEADER.ID " +
                    "LEFT JOIN TB_STATUS ON TB_AC_TRANS_HEADER.TRANS_STATUS = TB_STATUS.id " +
                    "WHERE TB_GRN_HEADER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Grn");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    grn = new GRN
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),
                        STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                        STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                        PO_ID = ADO.ToInt32(dr["PO_ID"]),
                        PO_NO = ADO.ToInt32(dr["PO_NO"]),
                        GRN_NO = ADO.ToInt32(dr["GRN_NO"]),
                        GRN_DATE = Convert.ToDateTime(dr["GRN_DATE"]),
                        SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                        SUPPPLIER_NAME = ADO.ToString(dr["STORE_NAME"]),
                        NET_AMOUNT = ADO.ToFloat(dr["NET_AMOUNT"]),
                        TOTAL_COST = ADO.ToFloat(dr["TOTAL_COST"]),
                        SUPP_GROSS_AMOUNT = ADO.ToFloat(dr["SUPP_GROSS_AMOUNT"]),
                        SUPP_NET_AMOUNT = ADO.ToFloat(dr["SUPP_NET_AMOUNT"]),
                        EXCHANGE_RATE = ADO.ToFloat(dr["EXCHANGE_RATE"]),
                        NARRATION = ADO.ToString(dr["NARRATION"]),
                        CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]),
                        CURRENCY_SYMBOL = ADO.ToString(dr["SYMBOL"]),
                        STATUS = ADO.ToString(dr["STATUS"])
                    };
                }

                strSQL = "SELECT TB_GRN_DETAIL.*, TB_STORES.STORE_NAME, TB_ITEMS.DESCRIPTION,TB_ITEMS.ITEM_CODE , " +
                    "TB_PO_DETAIL.GRN_QTY, TB_PO_DETAIL.QUANTITY as PO_QUANTITY " +
                    "FROM TB_GRN_DETAIL " +
                    "LEFT JOIN TB_STORES ON TB_GRN_DETAIL.STORE_ID = TB_STORES.ID " +
                    "LEFT JOIN TB_ITEMS ON TB_GRN_DETAIL.ITEM_ID = TB_ITEMS.ID " +

                    "LEFT JOIN TB_PO_DETAIL ON TB_GRN_DETAIL.PO_DETAIL_ID = TB_PO_DETAIL.ID " +
                    " WHERE TB_GRN_DETAIL.GRN_ID = " + id;

                DataTable tblgrndetail = ADO.GetDataTable(strSQL, "GrnDetails");

                foreach (DataRow dr3 in tblgrndetail.Rows)
                {
                    grndetails.Add(new GRNDetail
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        COMPANY_ID = ADO.ToInt32(dr3["COMPANY_ID"]),
                        STORE_ID = ADO.ToInt32(dr3["STORE_ID"]),
                        PO_DETAIL_ID = ADO.ToInt32(dr3["PO_DETAIL_ID"]),
                        ITEM_ID = ADO.ToInt32(dr3["ITEM_ID"]),
                        QUANTITY = ADO.ToFloat(dr3["QUANTITY"]),
                        RATE = ADO.ToFloat(dr3["RATE"]),
                        AMOUNT = ADO.ToFloat(dr3["AMOUNT"]),
                        INVOICE_QTY = ADO.ToFloat(dr3["INVOICE_QTY"]),
                        DISC_PERCENT = ADO.ToFloat(dr3["DISC_PERCENT"]),
                        COST = ADO.ToFloat(dr3["COST"]),
                        SUPP_PRICE = ADO.ToFloat(dr3["SUPP_PRICE"]),
                        SUPP_AMOUNT = ADO.ToFloat(dr3["SUPP_AMOUNT"]),
                        RECEIVED_QTY = ADO.ToFloat(dr3["RETURN_QTY"]),
                        UOM_PURCH = ADO.ToString(dr3["UOM_PURCH"]),
                        UOM = ADO.ToString(dr3["UOM"]),
                        UOM_MULTIPLE = ADO.ToInt32(dr3["UOM_MULTIPLE"]),
                        STORE_NAME = ADO.ToString(dr3["STORE_NAME"]),
                        ITEM_NAME = ADO.ToString(dr3["DESCRIPTION"]),
                        ITEM_CODE = ADO.ToString(dr3["ITEM_CODE"]),
                        PO_QUANTITY = ADO.ToFloat(dr3["PO_QUANTITY"]),
                        GRN_QUANTITY = ADO.ToFloat(dr3["GRN_QTY"])
                    });
                }

                strSQL = "SELECT TB_GRN_ITEM_COSTS.*, TB_LANDED_COSTS.DESCRIPTION, TB_LANDED_COSTS.IS_LOCAL_CURRENCY," +
                    "TB_LANDED_COSTS.IS_FIXED_AMOUNT FROM TB_GRN_ITEM_COSTS " +
                    "LEFT JOIN TB_LANDED_COSTS ON TB_GRN_ITEM_COSTS.COST_ID = TB_LANDED_COSTS.ID " +
                    " WHERE TB_GRN_ITEM_COSTS.GRN_ID = " + id;

                DataTable Tbl_itemcost = ADO.GetDataTable(strSQL, "GrnItemCost");

                foreach (DataRow dr4 in Tbl_itemcost.Rows)
                {
                    grnitemcost.Add(new GRN_ITEM_COST
                    {
                        ID = ADO.ToInt32(dr4["ID"]),
                        GRN_ID = ADO.ToInt32(dr4["GRN_ID"]),
                        STORE_ID = ADO.ToInt32(dr4["STORE_ID"]),
                        ITEM_ID = ADO.ToInt32(dr4["ITEM_ID"]),
                        COST_ID = ADO.ToInt32(dr4["COST_ID"]),
                        AMOUNT = ADO.ToFloat(dr4["AMOUNT"]),
                        DESCRIPTION = ADO.ToString(dr4["DESCRIPTION"]),
                        IS_FIXED_AMOUNT = ADO.Toboolean(dr4["IS_FIXED_AMOUNT"]),
                        IS_LOCAL_CURRENCY = ADO.Toboolean(dr4["IS_LOCAL_CURRENCY"])
                    });
                }
                strSQL = "select TB_GRN_COSTS.ID, STORE_ID, GRN_ID, COST_ID, COALESCE(AMOUNT_FC, [PERCENT], AMOUNT, 0) AS AMOUNT_VALUE," +
                    " TB_LANDED_COSTS.DESCRIPTION, TB_LANDED_COSTS.IS_LOCAL_CURRENCY," +
                    " TB_LANDED_COSTS.IS_FIXED_AMOUNT FROM TB_GRN_COSTS" +
                    " LEFT JOIN TB_LANDED_COSTS ON TB_GRN_COSTS.COST_ID = TB_LANDED_COSTS.ID" +
                  " WHERE TB_GRN_COSTS.GRN_ID = " + id;

                DataTable tblgrncost = ADO.GetDataTable(strSQL, "GrnCost");

                foreach (DataRow dr5 in tblgrncost.Rows)
                {
                    grncost.Add(new GRN_COST
                    {
                        ID = ADO.ToInt32(dr5["ID"]),
                        STORE_ID = ADO.ToInt32(dr5["STORE_ID"]),
                        GRN_ID = ADO.ToInt32(dr5["GRN_ID"]),
                        COST_ID = ADO.ToInt32(dr5["COST_ID"]),
                        VALUE = ADO.ToFloat(dr5["AMOUNT_VALUE"]),

                        DESCRIPTION = ADO.ToString(dr5["DESCRIPTION"]),
                        IS_FIXED_AMOUNT = ADO.Toboolean(dr5["IS_FIXED_AMOUNT"]),
                        IS_LOCAL_CURRENCY = ADO.Toboolean(dr5["IS_LOCAL_CURRENCY"])
                    });
                }

                grn.GRNDetails = grndetails;
                grn.GRN_Item_Cost = grnitemcost;
                grn.GRN_Cost = grncost;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return grn;
        }
        public bool Delete(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_GRN";
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
        public List<GRN> GetGRNList(Int32 intUserID)
        {
            List<GRN> worksheetList = new List<GRN>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_GRN";
            cmd.Parameters.AddWithValue("@ACTION", 0);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new GRN
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    GRN_NO = ADO.ToInt32(dr["GRN_NO"]),
                    GRN_DATE = Convert.ToDateTime(dr["GRN_DATE"]),
                    SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                    STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                    STORE_NAME = ADO.ToString(dr["STORE"]),
                    SUPPPLIER_NAME = ADO.ToString(dr["SUPP_NAME"]),
                    NET_AMOUNT = ADO.ToFloat(dr["NET_AMOUNT"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    STATUS = ADO.ToString(dr["STATUS"]),
                    PO_NO = ADO.ToInt32(dr["PO_NO"])

                });
            }
            connection.Close();

            return worksheetList;
        }
        public Int32 Approve(GRN grnHeader)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COMPANY_ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("GRN_ID", typeof(Int32));
                tbl.Columns.Add("PO_DETAIL_ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("QUANTITY", typeof(float));
                tbl.Columns.Add("RATE", typeof(float));
                tbl.Columns.Add("AMOUNT", typeof(float));
                tbl.Columns.Add("INVOICE_QTY", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("SUPP_PRICE", typeof(float));
                tbl.Columns.Add("SUPP_AMOUNT", typeof(float));
                tbl.Columns.Add("RETURN_QTY", typeof(float));
                tbl.Columns.Add("UOM_PURCH", typeof(string));
                tbl.Columns.Add("UOM", typeof(string));
                tbl.Columns.Add("UOM_MULTIPLE", typeof(Int32));

                if (grnHeader.GRNDetails != null && grnHeader.GRNDetails.Any())
                {
                    foreach (GRNDetail ur in grnHeader.GRNDetails)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["COMPANY_ID"] = ur.COMPANY_ID;
                        dRow["STORE_ID"] = ur.STORE_ID;
                        dRow["GRN_ID"] = ur.GRN_ID;
                        dRow["PO_DETAIL_ID"] = ur.PO_DETAIL_ID;
                        dRow["ITEM_ID"] = ur.ITEM_ID;
                        dRow["QUANTITY"] = ur.QUANTITY;
                        dRow["RATE"] = ur.RATE;
                        dRow["AMOUNT"] = ur.AMOUNT;
                        dRow["INVOICE_QTY"] = ur.INVOICE_QTY;
                        dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                        dRow["COST"] = ur.COST;
                        dRow["SUPP_PRICE"] = ur.SUPP_PRICE;
                        dRow["SUPP_AMOUNT"] = ur.SUPP_AMOUNT;
                        dRow["RETURN_QTY"] = ur.RECEIVED_QTY;
                        dRow["UOM_PURCH"] = ur.UOM_PURCH;
                        dRow["UOM"] = ur.UOM;
                        dRow["UOM_MULTIPLE"] = ur.UOM_MULTIPLE;
                        tbl.Rows.Add(dRow);
                    }
                }

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("GRN_ID", typeof(Int32));
                tbl1.Columns.Add("STORE_ID", typeof(Int32));
                tbl1.Columns.Add("ITEM_ID", typeof(Int32));
                tbl1.Columns.Add("COST_ID", typeof(Int32));
                tbl1.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Item_Cost != null && grnHeader.GRN_Item_Cost.Any())
                {
                    foreach (GRN_ITEM_COST ur1 in grnHeader.GRN_Item_Cost)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["GRN_ID"] = ur1.GRN_ID;
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["ITEM_ID"] = ur1.ITEM_ID;
                        dRow1["COST_ID"] = ur1.COST_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl1.Rows.Add(dRow1);
                    }
                }
                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("STORE_ID", typeof(Int32));
                tbl2.Columns.Add("GRN_ID", typeof(Int32));
                tbl2.Columns.Add("COST_ID", typeof(Int32));
                tbl2.Columns.Add("PERCENT", typeof(float));
                tbl2.Columns.Add("AMOUNT_FC", typeof(float));
                tbl2.Columns.Add("AMOUNT", typeof(float));

                if (grnHeader.GRN_Cost != null && grnHeader.GRN_Cost.Any())
                {
                    foreach (GRN_COST ur2 in grnHeader.GRN_Cost)
                    {
                        DataRow dRow2 = tbl2.NewRow();
                        dRow2["STORE_ID"] = ur2.STORE_ID;
                        dRow2["GRN_ID"] = ur2.GRN_ID;
                        dRow2["COST_ID"] = ur2.COST_ID;
                        dRow2["PERCENT"] = ur2.PERCENT;
                        dRow2["AMOUNT_FC"] = ur2.AMOUNT_FC;
                        dRow2["AMOUNT"] = ur2.AMOUNT;
                        tbl2.Rows.Add(dRow2);
                    }
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_GRN";

                cmd.Parameters.AddWithValue("ACTION", 6);
                cmd.Parameters.AddWithValue("@ID", grnHeader.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", grnHeader.COMPANY_ID);
                cmd.Parameters.AddWithValue("@STORE_ID", grnHeader.STORE_ID);
                cmd.Parameters.AddWithValue("@PO_ID", grnHeader.PO_ID);
                cmd.Parameters.AddWithValue("@GRN_DATE", grnHeader.GRN_DATE);
                cmd.Parameters.AddWithValue("@SUPP_ID", grnHeader.SUPP_ID);
                cmd.Parameters.AddWithValue("@NET_AMOUNT", grnHeader.NET_AMOUNT);
                cmd.Parameters.AddWithValue("@TOTAL_COST", grnHeader.TOTAL_COST);
                cmd.Parameters.AddWithValue("@SUPP_GROSS_AMOUNT", grnHeader.SUPP_GROSS_AMOUNT);
                cmd.Parameters.AddWithValue("@SUPP_NET_AMOUNT", grnHeader.SUPP_NET_AMOUNT);
                cmd.Parameters.AddWithValue("@EXCHANGE_RATE", grnHeader.EXCHANGE_RATE);
                cmd.Parameters.AddWithValue("@NARRATION", grnHeader.NARRATION);

                cmd.Parameters.AddWithValue("@UDT_TB_GRN_DETAIL", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_ITEM_COST", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_GRN_COST", tbl2);


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
    }
}
