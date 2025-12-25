using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemsService:IItemsService
    {
        public List<Items> GetAllItems(ItemListRequest request)
        {
            List<Items> list = new List<Items>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_ITEMS", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // === REQUIRED PARAMETERS FOR ACTION 2 ===
                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    // OPTIONAL / UNUSED PARAMETERS (PASS NULL)
                    cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Items obj = new Items
                        {
                            ID = ADO.ToInt32(dr["ID"]),
                            HQID = ADO.ToInt32(dr["HQID"]),
                            COMPANY_ID = ADO.ToInt32(dr["COMPANY_ID"]),

                            ITEM_CODE = ADO.ToString(dr["ITEM_CODE"]),
                            BARCODE = ADO.ToString(dr["BARCODE"]),
                            DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                            ARABIC_DESCRIPTION = ADO.ToString(dr["ARABIC_DESCRIPTION"]),
                            POS_DESCRIPTION = ADO.ToString(dr["POS_DESCRIPTION"]),

                            TYPE_ID = ADO.ToInt32(dr["TYPE_ID"]),
                            TYPE_NAME = ADO.ToString(dr["TYPE_NAME"]),

                            DEPT_ID = ADO.ToInt32(dr["DEPT_ID"]),
                            DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),

                            CAT_ID = ADO.ToInt32(dr["CAT_ID"]),
                            CAT_NAME = ADO.ToString(dr["CAT_NAME"]),

                            SUBCAT_ID = ADO.ToInt32(dr["SUBCAT_ID"]),
                            SUBCAT_NAME = ADO.ToString(dr["SUBCAT_NAME"]),

                            BRAND_ID = ADO.ToInt32(dr["BRAND_ID"]),
                            BRAND_NAME = ADO.ToString(dr["BRAND_NAME"]),

                            PACKING = ADO.ToString(dr["PACKING"]),
                            PACKING_ID = ADO.ToInt32(dr["PACKING_ID"]),

                            UNIT_ID = ADO.ToInt32(dr["UNIT_ID"]),
                            UOM = ADO.ToString(dr["UOM"]),

                            SALE_PRICE = ADO.ToFloat(dr["SALE_PRICE"]),
                            COST = dr["COST"] == DBNull.Value ? (float?)null : Convert.ToSingle(dr["COST"]),
                            PROFIT_MARGIN = dr["PROFIT_MARGIN"] == DBNull.Value ? (float?)null : Convert.ToSingle(dr["PROFIT_MARGIN"]),

                            QTY_STOCK = dr["QTY_STOCK"] == DBNull.Value ? (float?)null : Convert.ToSingle(dr["QTY_STOCK"]),
                            QTY_COMMITTED = dr["QTY_COMMITTED"] == DBNull.Value ? (float?)null : Convert.ToSingle(dr["QTY_COMMITTED"]),

                            CREATED_DATE = dr["CREATED_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CREATED_DATE"]),
                            LAST_PO_DATE = dr["LAST_PO_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LAST_PO_DATE"]),
                            LAST_GRN_DATE = dr["LAST_GRN_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LAST_GRN_DATE"]),
                            LAST_SALE_DATE = dr["LAST_SALE_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LAST_SALE_DATE"]),

                            ORIGIN_COUNTRY = ADO.ToInt32(dr["ORIGIN_COUNTRY"]),
                            COUNTRY_NAME = ADO.ToString(dr["COUNTRY_NAME"]),

                            VAT_CLASS_ID = ADO.ToInt32(dr["VAT_CLASS_ID"]),
                            VAT_NAME = ADO.ToString(dr["VAT_NAME"]),

                            COSTING_METHOD = ADO.ToInt32(dr["COSTING_METHOD"]),
                            COSTINGMETHOD = ADO.ToString(dr["COSTING_METHOD"]),

                            MATRIX_CODE = ADO.ToString(dr["MATRIX_CODE"]),
                            GST_PERC = ADO.ToDecimal(dr["GST_PERC"]),
                            HSN_CODE = ADO.ToString(dr["HSN_CODE"]),

                            item_stores = new List<ITEM_STORES>(),
                            item_alias = new List<ITEM_ALIAS>(),
                            item_suppliers = new List<ITEM_SUPPLIERS>()
                        };

                        list.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching item list: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return list;
        }

       
        public (int flag, string message) Insert(Items items)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("SALE_PRICE", typeof(float));
                tbl.Columns.Add("SALE_PRICE1", typeof(float));
                tbl.Columns.Add("SALE_PRICE2", typeof(float));
                tbl.Columns.Add("SALE_PRICE3", typeof(float));
                tbl.Columns.Add("SALE_PRICE4", typeof(float));
                tbl.Columns.Add("SALE_PRICE5", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("IS_INACTIVE", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN", typeof(bool));
                tbl.Columns.Add("IS_PRICE_REQUIRED", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE", typeof(bool));


                foreach (ITEM_STORES ur in items.item_stores)
                {
                    if (ur.STORE_ID == 0) 
                    {
                        return (0, "Select Store.");
                    }
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = DBNull.Value;
                    dRow["STORE_ID"] = ur.STORE_ID;
                    dRow["SALE_PRICE"] = ur.SALE_PRICE;
                    dRow["SALE_PRICE1"] = ur.SALE_PRICE1;
                    dRow["SALE_PRICE2"] = ur.SALE_PRICE2;
                    dRow["SALE_PRICE3"] = ur.SALE_PRICE3;
                    dRow["SALE_PRICE4"] = ur.SALE_PRICE4;
                    dRow["SALE_PRICE5"] = ur.SALE_PRICE5;
                    dRow["COST"] = ur.COST;
                    dRow["IS_INACTIVE"] = ur.IS_INACTIVE;
                    dRow["IS_NOT_SALE_ITEM"] = ur.IS_NOT_SALE_ITEM;
                    dRow["IS_NOT_SALE_RETURN"] = ur.IS_NOT_SALE_RETURN;
                    dRow["IS_PRICE_REQUIRED"] = ur.IS_PRICE_REQUIRED;
                    dRow["IS_NOT_DISCOUNTABLE"] = ur.IS_NOT_DISCOUNTABLE;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }
                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("ID", typeof(Int32));
                tbl1.Columns.Add("ALIAS", typeof(string));
                tbl1.Columns.Add("IS_DEFAULT", typeof(bool));
                tbl1.Columns.Add("ALIAS_TYPE_ID", typeof(Int32));

                if (items.item_alias != null && items.item_alias.Any())
                {
                    foreach (ITEM_ALIAS ur in items.item_alias)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["ID"] = DBNull.Value;
                        dRow1["ALIAS"] = ur.ALIAS;
                        dRow1["IS_DEFAULT"] = 0;
                        dRow1["ALIAS_TYPE_ID"] = ur.ALIAS_TYPE_ID;
                        tbl1.Rows.Add(dRow1);
                        tbl1.AcceptChanges();
                    }
                }
                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("ID", typeof(Int32));
                tbl2.Columns.Add("SUPP_ID", typeof(Int32));
                tbl2.Columns.Add("REORDER_NO", typeof(string));
                tbl2.Columns.Add("COST", typeof(float));
                tbl2.Columns.Add("IS_PRIMARY", typeof(bool));
                tbl2.Columns.Add("IS_CONSIGNMENT", typeof(bool));

                if (items.item_suppliers != null && items.item_suppliers.Any())
                {
                    foreach (ITEM_SUPPLIERS ur in items.item_suppliers)
                    {
                        if (items.TYPE_ID != 8)
                        {
                            if (!ur.SUPP_ID.HasValue || ur.SUPP_ID.Value == 0)
                            {
                                return (0, "Select Supplier.");
                            }
                        }
                        DataRow dRow2 = tbl2.NewRow();

                        dRow2["ID"] = DBNull.Value;
                        dRow2["SUPP_ID"] = ur.SUPP_ID;
                        dRow2["REORDER_NO"] = ur.REORDER_NO;
                        dRow2["COST"] = ur.COST;
                        dRow2["IS_PRIMARY"] = ur.IS_PRIMARY;
                        dRow2["IS_CONSIGNMENT"] = ur.IS_CONSIGNMENT;

                        tbl2.Rows.Add(dRow2);
                        tbl2.AcceptChanges();
                    }
                }

                DataTable tbl3 = new DataTable();

                tbl3.Columns.Add("ID", typeof(Int32));
                tbl3.Columns.Add("ITEM_ID", typeof(Int32));
                tbl3.Columns.Add("COMPONENT_ITEM_ID", typeof(Int32));
                tbl3.Columns.Add("QUANTITY", typeof(float));
                tbl3.Columns.Add("UOM", typeof(string));


                if (items.item_components != null && items.item_components.Any())
                {
                    foreach (ITEM_COMPONENTS ur in items.item_components)
                    {
                        DataRow dRow3 = tbl3.NewRow();

                        dRow3["ID"] = DBNull.Value;
                        dRow3["ITEM_ID"] = DBNull.Value;
                        dRow3["COMPONENT_ITEM_ID"] = ur.COMPONENT_ITEM_ID;
                        dRow3["QUANTITY"] = ur.QUANTITY;
                        dRow3["UOM"] = ur.UOM;


                        tbl3.Rows.Add(dRow3);
                        tbl3.AcceptChanges();
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEMS";

                cmd.Parameters.AddWithValue("ACTION", 1);
                //cmd.Parameters.AddWithValue("ID", items.ID);
                cmd.Parameters.AddWithValue("HQID", items.HQID);
                cmd.Parameters.AddWithValue("ITEM_CODE", items.ITEM_CODE);
                cmd.Parameters.AddWithValue("BARCODE", items.BARCODE);
                cmd.Parameters.AddWithValue("DESCRIPTION", items.DESCRIPTION);
                cmd.Parameters.AddWithValue("ARABIC_DESCRIPTION", items.ARABIC_DESCRIPTION);
                cmd.Parameters.AddWithValue("TYPE_ID", items.TYPE_ID);
                cmd.Parameters.AddWithValue("DEPT_ID", items.DEPT_ID);
                cmd.Parameters.AddWithValue("CAT_ID", items.CAT_ID);
                cmd.Parameters.AddWithValue("SUBCAT_ID", items.SUBCAT_ID);
                cmd.Parameters.AddWithValue("BRAND_ID", items.BRAND_ID);
                cmd.Parameters.AddWithValue("PACKING_ID", items.PACKING_ID);
                cmd.Parameters.AddWithValue("UNIT_ID", items.UNIT_ID);
                cmd.Parameters.AddWithValue("LONG_DESCRIPTION", items.LONG_DESCRIPTION);
                cmd.Parameters.AddWithValue("SALE_PRICE", items.SALE_PRICE);
                cmd.Parameters.AddWithValue("COST", items.COST);
                cmd.Parameters.AddWithValue("PROFIT_MARGIN", items.PROFIT_MARGIN);
                cmd.Parameters.AddWithValue("QTY_STOCK", items.QTY_STOCK);
                cmd.Parameters.AddWithValue("QTY_COMMITTED", items.QTY_COMMITTED);
                cmd.Parameters.AddWithValue("CREATED_DATE", items.CREATED_DATE);
                cmd.Parameters.AddWithValue("LAST_PO_DATE", items.LAST_PO_DATE);
                cmd.Parameters.AddWithValue("LAST_GRN_DATE", items.LAST_GRN_DATE);
                cmd.Parameters.AddWithValue("LAST_SALE_DATE", items.LAST_SALE_DATE);
                cmd.Parameters.AddWithValue("PARENT_ITEM_ID", items.PARENT_ITEM_ID);
                cmd.Parameters.AddWithValue("CHILD_QTY", items.CHILD_QTY);
                cmd.Parameters.AddWithValue("ORIGIN_COUNTRY", items.ORIGIN_COUNTRY);
                cmd.Parameters.AddWithValue("SHELF_LIFE", items.SHELF_LIFE);
                cmd.Parameters.AddWithValue("NOTES", items.NOTES);
                cmd.Parameters.AddWithValue("IS_INACTIVE", items.IS_INACTIVE);
                cmd.Parameters.AddWithValue("IS_PRICE_REQUIRED", items.IS_PRICE_REQUIRED);
                cmd.Parameters.AddWithValue("IS_NOT_DISCOUNTABLE", items.IS_NOT_DISCOUNTABLE);
                cmd.Parameters.AddWithValue("IS_NOT_PURCH_ITEM", items.IS_NOT_PURCH_ITEM);
                cmd.Parameters.AddWithValue("IS_NOT_SALE_ITEM", items.IS_NOT_SALE_ITEM);
                cmd.Parameters.AddWithValue("IS_NOT_SALE_RETURN", items.IS_NOT_SALE_RETURN);
                cmd.Parameters.AddWithValue("IS_BLOCKED", items.IS_BLOCKED);
                cmd.Parameters.AddWithValue("IMAGE_NAME", items.IMAGE_NAME);
                cmd.Parameters.AddWithValue("ITEM_SL", items.ITEM_SL);
                cmd.Parameters.AddWithValue("SALE_PRICE1", items.SALE_PRICE1);
                cmd.Parameters.AddWithValue("SALE_PRICE2", items.SALE_PRICE2);
                cmd.Parameters.AddWithValue("SALE_PRICE3", items.SALE_PRICE3);
                cmd.Parameters.AddWithValue("SALE_PRICE4", items.SALE_PRICE4);
                cmd.Parameters.AddWithValue("SALE_PRICE5", items.SALE_PRICE5);
                cmd.Parameters.AddWithValue("PURCH_PRICE", items.PURCH_PRICE);
                cmd.Parameters.AddWithValue("PURCH_CURRENCY", items.PURCH_CURRENCY);
                cmd.Parameters.AddWithValue("IS_CONSIGNMENT", items.IS_CONSIGNMENT);
                cmd.Parameters.AddWithValue("VAT_CLASS_ID", items.VAT_CLASS_ID);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY1", items.ITEM_PROPERTY1);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY2", items.ITEM_PROPERTY2);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY3", items.ITEM_PROPERTY3);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY4", items.ITEM_PROPERTY4);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY5", items.ITEM_PROPERTY5);
                cmd.Parameters.AddWithValue("BIN_LOCATION", items.BIN_LOCATION);
                cmd.Parameters.AddWithValue("RESTOCK_LEVEL", items.RESTOCK_LEVEL);
                cmd.Parameters.AddWithValue("REORDER_POINT", items.REORDER_POINT);
                cmd.Parameters.AddWithValue("COSTING_METHOD", items.COSTING_METHOD);
                cmd.Parameters.AddWithValue("POS_DESCRIPTION", items.POS_DESCRIPTION);
                cmd.Parameters.AddWithValue("IS_DIFFERENT_UOM_PURCH", items.IS_DIFFERENT_UOM_PURCH);
                cmd.Parameters.AddWithValue("UOM_PURCH", items.UOM_PURCH);
                cmd.Parameters.AddWithValue("UOM_MULTPLE", items.UOM_MULTPLE);
                cmd.Parameters.AddWithValue("MATRIX_CODE", items.MATRIX_CODE);
                cmd.Parameters.AddWithValue("COMPANY_ID", items.COMPANY_ID);
                cmd.Parameters.AddWithValue("GST_PERC", items.GST_PERC);
                cmd.Parameters.AddWithValue("HSN_CODE", items.HSN_CODE);


                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_STORE", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_ALIAS", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_SUPPLIER", tbl2);
                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_COMPONENT", tbl3);

                cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                objtrans.Commit();

                int flag = Convert.ToInt32(cmd.Parameters["@flag"].Value);
                string msg = Convert.ToString(cmd.Parameters["@message"].Value);

                connection.Close();
                return (flag, msg);
            }
            catch (SqlException sqlEx)
            {
                objtrans.Rollback();
                connection.Close();
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    return (0, "Duplicate entry: The alias you are trying to insert already exists.");
                }
                else
                {
                    return (0, "Database error: " + sqlEx.Message);
                }
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                return (0, "Error: " + ex.Message);
            }
        }
        public bool Update(Items items)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                if (items.item_stores != null && items.item_stores.Any(ur => ur.STORE_ID == 0))
                {
                    throw new Exception("Alert:Select Store...!");
                }

                if (items.TYPE_ID != 8)
                {
                    if (items.item_suppliers != null && items.item_suppliers.Any(ur => !ur.SUPP_ID.HasValue || ur.SUPP_ID.Value == 0))
                    {
                        throw new Exception("Alert:Select Supplier...!");
                    }
                }

                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));
                tbl.Columns.Add("SALE_PRICE", typeof(float));
                tbl.Columns.Add("SALE_PRICE1", typeof(float));
                tbl.Columns.Add("SALE_PRICE2", typeof(float));
                tbl.Columns.Add("SALE_PRICE3", typeof(float));
                tbl.Columns.Add("SALE_PRICE4", typeof(float));
                tbl.Columns.Add("SALE_PRICE5", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("IS_INACTIVE", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN", typeof(bool));
                tbl.Columns.Add("IS_PRICE_REQUIRED", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE", typeof(bool));

                foreach (ITEM_STORES ur in items.item_stores)
                {
                    DataRow dRow = tbl.NewRow();
                    dRow["ID"] = ur.ID;
                    dRow["STORE_ID"] = ur.STORE_ID;
                    dRow["SALE_PRICE"] = ur.SALE_PRICE;
                    dRow["SALE_PRICE1"] = ur.SALE_PRICE1;
                    dRow["SALE_PRICE2"] = ur.SALE_PRICE2;
                    dRow["SALE_PRICE3"] = ur.SALE_PRICE3;
                    dRow["SALE_PRICE4"] = ur.SALE_PRICE4;
                    dRow["SALE_PRICE5"] = ur.SALE_PRICE5;
                    dRow["COST"] = ur.COST;
                    dRow["IS_INACTIVE"] = ur.IS_INACTIVE;
                    dRow["IS_NOT_SALE_ITEM"] = ur.IS_NOT_SALE_ITEM;
                    dRow["IS_NOT_SALE_RETURN"] = ur.IS_NOT_SALE_RETURN;
                    dRow["IS_PRICE_REQUIRED"] = ur.IS_PRICE_REQUIRED;
                    dRow["IS_NOT_DISCOUNTABLE"] = ur.IS_NOT_DISCOUNTABLE;
                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                DataTable tbl1 = new DataTable();
                tbl1.Columns.Add("ID", typeof(Int32));
                tbl1.Columns.Add("ALIAS", typeof(string));
                tbl1.Columns.Add("IS_DEFAULT", typeof(bool));
                tbl1.Columns.Add("ALIAS_TYPE_ID", typeof(Int32));

                if (items.item_alias != null && items.item_alias.Any())
                {
                    foreach (ITEM_ALIAS ur in items.item_alias)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["ID"] = ur.ID;
                        dRow1["ALIAS"] = ur.ALIAS;
                        dRow1["IS_DEFAULT"] = 0;
                        dRow1["ALIAS_TYPE_ID"] = ur.ALIAS_TYPE_ID;
                        tbl1.Rows.Add(dRow1);
                        tbl1.AcceptChanges();
                    }
                }

                DataTable tbl2 = new DataTable();
                tbl2.Columns.Add("ID", typeof(Int32));
                tbl2.Columns.Add("SUPP_ID", typeof(Int32));
                tbl2.Columns.Add("REORDER_NO", typeof(string));
                tbl2.Columns.Add("COST", typeof(float));
                tbl2.Columns.Add("IS_PRIMARY", typeof(bool));
                tbl2.Columns.Add("IS_CONSIGNMENT", typeof(bool));

                if (items.item_suppliers != null && items.item_suppliers.Any())
                {
                    foreach (ITEM_SUPPLIERS ur in items.item_suppliers)
                    {
                        DataRow dRow2 = tbl2.NewRow();
                        dRow2["ID"] = ur.ID;
                        dRow2["SUPP_ID"] = ur.SUPP_ID;
                        dRow2["REORDER_NO"] = ur.REORDER_NO;
                        dRow2["COST"] = ur.COST;
                        dRow2["IS_PRIMARY"] = ur.IS_PRIMARY;
                        dRow2["IS_CONSIGNMENT"] = ur.IS_CONSIGNMENT;
                        tbl2.Rows.Add(dRow2);
                        tbl2.AcceptChanges();
                    }
                }

                DataTable tbl3 = new DataTable();
                tbl3.Columns.Add("ID", typeof(Int32));
                tbl3.Columns.Add("ITEM_ID", typeof(Int32));
                tbl3.Columns.Add("COMPONENT_ITEM_ID", typeof(Int32));
                tbl3.Columns.Add("QUANTITY", typeof(float));
                tbl3.Columns.Add("UOM", typeof(string));

                if (items.item_components != null && items.item_components.Any())
                {
                    foreach (ITEM_COMPONENTS ur in items.item_components)
                    {
                        DataRow dRow3 = tbl3.NewRow();
                        dRow3["ID"] = ur.ID;
                        dRow3["ITEM_ID"] = ur.ITEM_ID;
                        dRow3["COMPONENT_ITEM_ID"] = ur.COMPONENT_ITEM_ID;
                        dRow3["QUANTITY"] = ur.QUANTITY;
                        dRow3["UOM"] = ur.UOM;
                        tbl3.Rows.Add(dRow3);
                        tbl3.AcceptChanges();
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ITEMS";
                cmd.Parameters.AddWithValue("ACTION", 3);
                cmd.Parameters.AddWithValue("ID", items.ID);
                cmd.Parameters.AddWithValue("HQID", items.HQID);
                cmd.Parameters.AddWithValue("ITEM_CODE", items.ITEM_CODE);
                cmd.Parameters.AddWithValue("BARCODE", items.BARCODE);
                cmd.Parameters.AddWithValue("DESCRIPTION", items.DESCRIPTION);
                cmd.Parameters.AddWithValue("ARABIC_DESCRIPTION", items.ARABIC_DESCRIPTION);
                cmd.Parameters.AddWithValue("TYPE_ID", items.TYPE_ID);
                cmd.Parameters.AddWithValue("DEPT_ID", items.DEPT_ID);
                cmd.Parameters.AddWithValue("CAT_ID", items.CAT_ID);
                cmd.Parameters.AddWithValue("SUBCAT_ID", items.SUBCAT_ID);
                cmd.Parameters.AddWithValue("BRAND_ID", items.BRAND_ID);
                cmd.Parameters.AddWithValue("PACKING_ID", items.PACKING_ID);
                cmd.Parameters.AddWithValue("UNIT_ID", items.UNIT_ID);
                cmd.Parameters.AddWithValue("LONG_DESCRIPTION", items.LONG_DESCRIPTION);
                cmd.Parameters.AddWithValue("SALE_PRICE", items.SALE_PRICE);
                cmd.Parameters.AddWithValue("COST", items.COST);
                cmd.Parameters.AddWithValue("PROFIT_MARGIN", items.PROFIT_MARGIN);
                cmd.Parameters.AddWithValue("QTY_STOCK", items.QTY_STOCK);
                cmd.Parameters.AddWithValue("QTY_COMMITTED", items.QTY_COMMITTED);
                cmd.Parameters.AddWithValue("CREATED_DATE", items.CREATED_DATE);
                cmd.Parameters.AddWithValue("LAST_PO_DATE", items.LAST_PO_DATE);
                cmd.Parameters.AddWithValue("LAST_GRN_DATE", items.LAST_GRN_DATE);
                cmd.Parameters.AddWithValue("LAST_SALE_DATE", items.LAST_SALE_DATE);
                cmd.Parameters.AddWithValue("PARENT_ITEM_ID", items.PARENT_ITEM_ID);
                cmd.Parameters.AddWithValue("CHILD_QTY", items.CHILD_QTY);
                cmd.Parameters.AddWithValue("ORIGIN_COUNTRY", items.ORIGIN_COUNTRY);
                cmd.Parameters.AddWithValue("SHELF_LIFE", items.SHELF_LIFE);
                cmd.Parameters.AddWithValue("NOTES", items.NOTES);
                cmd.Parameters.AddWithValue("IS_INACTIVE", items.IS_INACTIVE);
                cmd.Parameters.AddWithValue("IS_PRICE_REQUIRED", items.IS_PRICE_REQUIRED);
                cmd.Parameters.AddWithValue("IS_NOT_DISCOUNTABLE", items.IS_NOT_DISCOUNTABLE);
                cmd.Parameters.AddWithValue("IS_NOT_PURCH_ITEM", items.IS_NOT_PURCH_ITEM);
                cmd.Parameters.AddWithValue("IS_NOT_SALE_ITEM", items.IS_NOT_SALE_ITEM);
                cmd.Parameters.AddWithValue("IS_NOT_SALE_RETURN", items.IS_NOT_SALE_RETURN);
                cmd.Parameters.AddWithValue("IS_BLOCKED", items.IS_BLOCKED);
                cmd.Parameters.AddWithValue("IMAGE_NAME", items.IMAGE_NAME);
                cmd.Parameters.AddWithValue("ITEM_SL", items.ITEM_SL);
                cmd.Parameters.AddWithValue("SALE_PRICE1", items.SALE_PRICE1);
                cmd.Parameters.AddWithValue("SALE_PRICE2", items.SALE_PRICE2);
                cmd.Parameters.AddWithValue("SALE_PRICE3", items.SALE_PRICE3);
                cmd.Parameters.AddWithValue("SALE_PRICE4", items.SALE_PRICE4);
                cmd.Parameters.AddWithValue("SALE_PRICE5", items.SALE_PRICE5);
                cmd.Parameters.AddWithValue("PURCH_PRICE", items.PURCH_PRICE);
                cmd.Parameters.AddWithValue("PURCH_CURRENCY", items.PURCH_CURRENCY);
                cmd.Parameters.AddWithValue("IS_CONSIGNMENT", items.IS_CONSIGNMENT);
                cmd.Parameters.AddWithValue("VAT_CLASS_ID", items.VAT_CLASS_ID);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY1", items.ITEM_PROPERTY1);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY2", items.ITEM_PROPERTY2);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY3", items.ITEM_PROPERTY3);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY4", items.ITEM_PROPERTY4);
                cmd.Parameters.AddWithValue("ITEM_PROPERTY5", items.ITEM_PROPERTY5);
                cmd.Parameters.AddWithValue("BIN_LOCATION", items.BIN_LOCATION);
                cmd.Parameters.AddWithValue("RESTOCK_LEVEL", items.RESTOCK_LEVEL);
                cmd.Parameters.AddWithValue("REORDER_POINT", items.REORDER_POINT);
                cmd.Parameters.AddWithValue("COSTING_METHOD", items.COSTING_METHOD);
                cmd.Parameters.AddWithValue("POS_DESCRIPTION", items.POS_DESCRIPTION);
                cmd.Parameters.AddWithValue("IS_DIFFERENT_UOM_PURCH", items.IS_DIFFERENT_UOM_PURCH);
                cmd.Parameters.AddWithValue("UOM_PURCH", items.UOM_PURCH);
                cmd.Parameters.AddWithValue("UOM_MULTPLE", items.UOM_MULTPLE);
                cmd.Parameters.AddWithValue("MATRIX_CODE", items.MATRIX_CODE);
                cmd.Parameters.AddWithValue("COMPANY_ID", items.COMPANY_ID);
                cmd.Parameters.AddWithValue("GST_PERC", items.GST_PERC);
                cmd.Parameters.AddWithValue("HSN_CODE", items.HSN_CODE);

                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_STORE", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_ALIAS", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_SUPPLIER", tbl2);
                cmd.Parameters.AddWithValue("@UDT_TB_ITEMS_COMPONENT", tbl3);

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
        public Items GetItems(int id)
        {
            Items item = new Items();
            List<ITEM_STORES> itemstores = new List<ITEM_STORES>();
            List<ITEM_SUPPLIERS> itemsuppliers = new List<ITEM_SUPPLIERS>();
            List<ITEM_ALIAS> itemalias = new List<ITEM_ALIAS>();
            List<ITEM_COMPONENTS> itemcomponent = new List<ITEM_COMPONENTS>();

            try
            {
                string strSQL = "SELECT TB_ITEMS.ID, TB_ITEMS.HQID, TB_ITEMS.ITEM_CODE, TB_ITEMS.BARCODE, TB_ITEMS.DESCRIPTION, " +
                                "TB_ITEMS.ARABIC_DESCRIPTION, TB_ITEMS.TYPE_ID, TB_ITEMS.DEPT_ID, TB_ITEMS.CAT_ID, TB_ITEMS.SUBCAT_ID, " +
                                "TB_ITEMS.BRAND_ID, TB_ITEMS.PACKING_ID, TB_ITEMS.UNIT_ID, TB_ITEMS.LONG_DESCRIPTION, TB_ITEMS.SALE_PRICE, " +
                                "TB_ITEMS.COST, TB_ITEMS.PROFIT_MARGIN, TB_ITEMS.QTY_STOCK, TB_ITEMS.QTY_COMMITTED, TB_ITEMS.CREATED_DATE, " +
                                "TB_ITEMS.LAST_PO_DATE, TB_ITEMS.LAST_GRN_DATE, TB_ITEMS.LAST_SALE_DATE, TB_ITEMS.RESTOCK_LEVEL, " +
                                "TB_ITEMS.REORDER_POINT, TB_ITEMS.PARENT_ITEM_ID, TB_ITEMS.CHILD_QTY, TB_ITEMS.ORIGIN_COUNTRY, TB_ITEMS.SHELF_LIFE, " +
                                "TB_ITEMS.BIN_LOCATION, TB_ITEMS.NOTES, TB_ITEMS.IS_INACTIVE, TB_ITEMS.IS_PRICE_REQUIRED, TB_ITEMS.IS_NOT_DISCOUNTABLE, " +
                                "TB_ITEMS.IS_NOT_PURCH_ITEM, TB_ITEMS.IS_NOT_SALE_ITEM, TB_ITEMS.IS_NOT_SALE_RETURN, TB_ITEMS.IS_BLOCKED, TB_ITEMS.IMAGE_NAME, " +
                                "TB_ITEMS.ITEM_SL, TB_ITEMS.SALE_PRICE1, TB_ITEMS.SALE_PRICE2, TB_ITEMS.SALE_PRICE3, TB_ITEMS.SALE_PRICE4, TB_ITEMS.SALE_PRICE5, " +
                                "TB_ITEMS.PURCH_PRICE, TB_ITEMS.PURCH_CURRENCY, TB_ITEMS.IS_CONSIGNMENT, TB_ITEMS.VAT_CLASS_ID, TB_ITEMS.ITEM_PROPERTY1, " +
                                "TB_ITEMS.IS_DELETED, TB_ITEMS.ITEM_PROPERTY2, TB_ITEMS.ITEM_PROPERTY3, TB_ITEMS.ITEM_PROPERTY4, TB_ITEMS.ITEM_PROPERTY5, " +
                                "TB_ITEMS.COSTING_METHOD,TB_ITEMS.COMPANY_ID,TB_ITEMS.GST_PERC,TB_ITEMS.HSN_CODE, " +
                                "TB_ITEMS.RESTOCK_LEVEL, TB_ITEMS.REORDER_POINT, TB_ITEMS.BIN_LOCATION, TB_ITEMS.POS_DESCRIPTION, " +

                                "TB_ITEMS.IS_DIFFERENT_UOM_PURCH, TB_ITEMS.UOM_PURCH, TB_ITEMS.UOM_MULTPLE,TB_ITEMS.MATRIX_CODE, " +

                                "TB_ITEM_TYPE.TYPE_NAME, TB_ITEM_DEPARTMENT.DEPT_NAME, TB_ITEM_CATEGORY.CAT_NAME, " +
                                "TB_ITEM_SUBCATEGORY.SUBCAT_NAME, TB_ITEM_BRAND.BRAND_NAME, TB_COUNTRY.COUNTRY_NAME, TB_VAT_CLASS.VAT_NAME, " +
                                "TB_COSTING_METHOD.COSTING_METHOD, TB_PACKING.DESCRIPTION, TB_UOM.UOM " +
                                "FROM TB_ITEMS " +
                                "LEFT JOIN TB_ITEM_TYPE ON TB_ITEMS.TYPE_ID = TB_ITEM_TYPE.ID " +
                                "LEFT JOIN TB_ITEM_DEPARTMENT ON TB_ITEMS.DEPT_ID = TB_ITEM_DEPARTMENT.ID " +
                                "LEFT JOIN TB_ITEM_CATEGORY ON TB_ITEMS.CAT_ID = TB_ITEM_CATEGORY.ID " +
                                "LEFT JOIN TB_ITEM_SUBCATEGORY ON TB_ITEMS.SUBCAT_ID = TB_ITEM_SUBCATEGORY.ID " +
                                "LEFT JOIN TB_ITEM_BRAND ON TB_ITEMS.BRAND_ID = TB_ITEM_BRAND.ID " +
                                "LEFT JOIN TB_COUNTRY ON TB_ITEMS.ORIGIN_COUNTRY = TB_COUNTRY.ID " +
                                "LEFT JOIN TB_VAT_CLASS ON TB_ITEMS.VAT_CLASS_ID = TB_VAT_CLASS.ID " +
                                "LEFT JOIN TB_COSTING_METHOD ON TB_ITEMS.COSTING_METHOD = TB_COSTING_METHOD.ID " +
                                "LEFT JOIN TB_UOM ON TB_ITEMS.UNIT_ID = TB_UOM.ID " +
                                "LEFT JOIN TB_PACKING ON TB_ITEMS.PACKING_ID = TB_PACKING.ID " +
                                "WHERE TB_ITEMS.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Items");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    item = new Items
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                        COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COMPANY_ID"]) : 0,
                        HQID = dr["HQID"] != DBNull.Value ? Convert.ToInt32(dr["HQID"]) : 0,
                        ITEM_CODE = dr["ITEM_CODE"] != DBNull.Value ? Convert.ToString(dr["ITEM_CODE"]) : string.Empty,
                        BARCODE = dr["BARCODE"] != DBNull.Value ? Convert.ToString(dr["BARCODE"]) : string.Empty,
                        DESCRIPTION = dr["DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["DESCRIPTION"]) : string.Empty,
                        ARABIC_DESCRIPTION = dr["ARABIC_DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["ARABIC_DESCRIPTION"]) : string.Empty,
                        TYPE_ID = dr["TYPE_ID"] != DBNull.Value ? Convert.ToInt32(dr["TYPE_ID"]) : 0,
                        TYPE_NAME = dr["TYPE_NAME"] != DBNull.Value ? Convert.ToString(dr["TYPE_NAME"]) : string.Empty,
                        DEPT_ID = dr["DEPT_ID"] != DBNull.Value ? Convert.ToInt32(dr["DEPT_ID"]) : 0,
                        DEPT_NAME = dr["DEPT_NAME"] != DBNull.Value ? Convert.ToString(dr["DEPT_NAME"]) : string.Empty,
                        CAT_ID = dr["CAT_ID"] != DBNull.Value ? Convert.ToInt32(dr["CAT_ID"]) : 0,
                        CAT_NAME = dr["CAT_NAME"] != DBNull.Value ? Convert.ToString(dr["CAT_NAME"]) : string.Empty,
                        SUBCAT_ID = dr["SUBCAT_ID"] != DBNull.Value ? Convert.ToInt32(dr["SUBCAT_ID"]) : 0,
                        SUBCAT_NAME = dr["SUBCAT_NAME"] != DBNull.Value ? Convert.ToString(dr["SUBCAT_NAME"]) : string.Empty,
                        BRAND_ID = dr["BRAND_ID"] != DBNull.Value ? Convert.ToInt32(dr["BRAND_ID"]) : 0,
                        BRAND_NAME = dr["BRAND_NAME"] != DBNull.Value ? Convert.ToString(dr["BRAND_NAME"]) : string.Empty,
                        PACKING = dr["DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["DESCRIPTION"]) : string.Empty,
                        PACKING_ID = dr["PACKING_ID"] != DBNull.Value ? Convert.ToInt32(dr["PACKING_ID"]) : 0,
                        UNIT_ID = dr["UNIT_ID"] != DBNull.Value ? Convert.ToInt32(dr["UNIT_ID"]) : 0,
                        UOM = dr["UOM"] != DBNull.Value ? Convert.ToString(dr["UOM"]) : string.Empty,
                        LONG_DESCRIPTION = dr["LONG_DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["LONG_DESCRIPTION"]) : string.Empty,
                        SALE_PRICE = Convert.IsDBNull(dr["SALE_PRICE"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE"]),
                        COST = Convert.IsDBNull(dr["COST"]) ? (float?)null : Convert.ToSingle(dr["COST"]),
                        PROFIT_MARGIN = Convert.IsDBNull(dr["PROFIT_MARGIN"]) ? (float?)null : Convert.ToSingle(dr["PROFIT_MARGIN"]),
                        QTY_STOCK = Convert.IsDBNull(dr["QTY_STOCK"]) ? (float?)null : Convert.ToSingle(dr["QTY_STOCK"]),
                        QTY_COMMITTED = Convert.IsDBNull(dr["SALE_PRICE"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE"]),
                        CREATED_DATE = Convert.IsDBNull(dr["CREATED_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["CREATED_DATE"]),
                        LAST_PO_DATE = Convert.IsDBNull(dr["LAST_PO_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_PO_DATE"]),
                        LAST_GRN_DATE = Convert.IsDBNull(dr["LAST_GRN_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_GRN_DATE"]),
                        LAST_SALE_DATE = Convert.IsDBNull(dr["LAST_SALE_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_SALE_DATE"]),
                        PARENT_ITEM_ID = dr["PARENT_ITEM_ID"] != DBNull.Value ? Convert.ToInt32(dr["PARENT_ITEM_ID"]) : 0,
                        CHILD_QTY = Convert.IsDBNull(dr["CHILD_QTY"]) ? (float?)null : Convert.ToSingle(dr["CHILD_QTY"]),
                        ORIGIN_COUNTRY = dr["ORIGIN_COUNTRY"] != DBNull.Value ? Convert.ToInt32(dr["ORIGIN_COUNTRY"]) : 0, // or some default value
                        COUNTRY_NAME = dr["COUNTRY_NAME"] != DBNull.Value ? Convert.ToString(dr["COUNTRY_NAME"]) : string.Empty,
                        SHELF_LIFE = dr["SHELF_LIFE"] != DBNull.Value ? Convert.ToInt32(dr["SHELF_LIFE"]) : 0, // or some default value
                        NOTES = dr["NOTES"] != DBNull.Value ? Convert.ToString(dr["NOTES"]) : string.Empty,
                        IS_INACTIVE = Convert.IsDBNull(dr["IS_INACTIVE"]) ? (bool?)null : Convert.ToBoolean(dr["IS_INACTIVE"]),
                        IS_PRICE_REQUIRED = Convert.IsDBNull(dr["IS_PRICE_REQUIRED"]) ? (bool?)null : Convert.ToBoolean(dr["IS_PRICE_REQUIRED"]),
                        IS_NOT_DISCOUNTABLE = Convert.IsDBNull(dr["IS_NOT_DISCOUNTABLE"]) ? (bool?)null : Convert.ToBoolean(dr["IS_NOT_DISCOUNTABLE"]),
                        IS_NOT_PURCH_ITEM = Convert.IsDBNull(dr["IS_NOT_PURCH_ITEM"]) ? (bool?)null : Convert.ToBoolean(dr["IS_NOT_PURCH_ITEM"]),
                        IS_NOT_SALE_ITEM = Convert.IsDBNull(dr["IS_NOT_SALE_ITEM"]) ? (bool?)null : Convert.ToBoolean(dr["IS_NOT_SALE_ITEM"]),
                        IS_NOT_SALE_RETURN = Convert.IsDBNull(dr["IS_NOT_SALE_RETURN"]) ? (bool?)null : Convert.ToBoolean(dr["IS_NOT_SALE_RETURN"]),
                        IS_BLOCKED = Convert.IsDBNull(dr["IS_BLOCKED"]) ? (bool?)null : Convert.ToBoolean(dr["IS_BLOCKED"]),
                        IMAGE_NAME = dr["IMAGE_NAME"] != DBNull.Value ? Convert.ToString(dr["IMAGE_NAME"]) : string.Empty,
                        ITEM_SL = dr["ITEM_SL"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_SL"]) : 0,
                        SALE_PRICE1 = Convert.IsDBNull(dr["SALE_PRICE1"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE1"]),
                        SALE_PRICE2 = Convert.IsDBNull(dr["SALE_PRICE2"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE2"]),
                        SALE_PRICE3 = Convert.IsDBNull(dr["SALE_PRICE3"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE3"]),
                        SALE_PRICE4 = Convert.IsDBNull(dr["SALE_PRICE4"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE4"]),
                        SALE_PRICE5 = Convert.IsDBNull(dr["SALE_PRICE5"]) ? (float?)null : Convert.ToSingle(dr["SALE_PRICE5"]),
                        PURCH_PRICE = Convert.IsDBNull(dr["PURCH_PRICE"]) ? (float?)null : Convert.ToSingle(dr["PURCH_PRICE"]),
                        PURCH_CURRENCY = dr["PURCH_CURRENCY"] != DBNull.Value ? Convert.ToInt32(dr["PURCH_CURRENCY"]) : 0,
                        IS_CONSIGNMENT = Convert.IsDBNull(dr["IS_CONSIGNMENT"]) ? (bool?)null : Convert.ToBoolean(dr["IS_CONSIGNMENT"]),
                        VAT_CLASS_ID = dr["VAT_CLASS_ID"] != DBNull.Value ? Convert.ToInt32(dr["VAT_CLASS_ID"]) : 0,
                        VAT_NAME = dr["VAT_NAME"] != DBNull.Value ? Convert.ToString(dr["VAT_NAME"]) : string.Empty,
                        ITEM_PROPERTY1 = dr["ITEM_PROPERTY1"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_PROPERTY1"]) : 0,
                        ITEM_PROPERTY2 = dr["ITEM_PROPERTY2"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_PROPERTY2"]) : 0,
                        ITEM_PROPERTY3 = dr["ITEM_PROPERTY3"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_PROPERTY3"]) : 0,
                        ITEM_PROPERTY4 = dr["ITEM_PROPERTY4"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_PROPERTY4"]) : 0,
                        ITEM_PROPERTY5 = dr["ITEM_PROPERTY5"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_PROPERTY5"]) : 0,
                        COSTING_METHOD = dr["COSTING_METHOD"] != DBNull.Value ? Convert.ToInt32(dr["COSTING_METHOD"]) : 0,
                        COSTINGMETHOD = dr["COSTING_METHOD"] != DBNull.Value ? Convert.ToString(dr["COSTING_METHOD"]) : string.Empty,

                        RESTOCK_LEVEL = Convert.IsDBNull(dr["RESTOCK_LEVEL"]) ? (float?)null : Convert.ToSingle(dr["RESTOCK_LEVEL"]),
                        REORDER_POINT = Convert.IsDBNull(dr["REORDER_POINT"]) ? (float?)null : Convert.ToSingle(dr["REORDER_POINT"]),
                        BIN_LOCATION = dr["BIN_LOCATION"] != DBNull.Value ? Convert.ToString(dr["BIN_LOCATION"]) : string.Empty,
                        POS_DESCRIPTION = dr["POS_DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["POS_DESCRIPTION"]) : string.Empty,

                        IS_DIFFERENT_UOM_PURCH = Convert.IsDBNull(dr["IS_DIFFERENT_UOM_PURCH"]) ? (bool?)null : Convert.ToBoolean(dr["IS_DIFFERENT_UOM_PURCH"]),
                        UOM_PURCH = dr["UOM_PURCH"] != DBNull.Value ? Convert.ToString(dr["UOM_PURCH"]) : string.Empty,
                        UOM_MULTPLE = dr["UOM_MULTPLE"] != DBNull.Value ? Convert.ToInt32(dr["UOM_MULTPLE"]) : 0,
                        MATRIX_CODE = dr["MATRIX_CODE"] != DBNull.Value ? Convert.ToString(dr["MATRIX_CODE"]) : string.Empty,
                        GST_PERC = dr["GST_PERC"] != DBNull.Value ? Convert.ToDecimal(dr["GST_PERC"]) : 0,
                        HSN_CODE = dr["HSN_CODE"] != DBNull.Value ? Convert.ToString(dr["HSN_CODE"]) : string.Empty,


                    };
                }

                // Configure the SqlCommand
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = ADO.GetConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_ITEM_STORE_DATA";

                cmd.Parameters.AddWithValue("@ITEM_ID", id);
                DataTable tblItemStores = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tblItemStores);


                // Process the results
                foreach (DataRow dr1 in tblItemStores.Rows)
                {
                    itemstores.Add(new ITEM_STORES
                    {
                        ID = dr1["ID"] != DBNull.Value ? Convert.ToInt32(dr1["ID"]) : 0,
                        STORE_ID = dr1["STORE_ID"] != DBNull.Value ? Convert.ToInt32(dr1["STORE_ID"]) : 0,
                        SALE_PRICE = dr1["SALE_PRICE"] != DBNull.Value ? float.Parse(dr1["SALE_PRICE"].ToString()) : 0f,
                        SALE_PRICE1 = dr1["SALE_PRICE1"] != DBNull.Value ? float.Parse(dr1["SALE_PRICE1"].ToString()) : 0f,
                        SALE_PRICE2 = dr1["SALE_PRICE2"] != DBNull.Value ? float.Parse(dr1["SALE_PRICE2"].ToString()) : 0f,
                        SALE_PRICE3 = dr1["SALE_PRICE3"] != DBNull.Value ? float.Parse(dr1["SALE_PRICE3"].ToString()) : 0f,
                        SALE_PRICE4 = dr1["SALE_PRICE4"] != DBNull.Value ? float.Parse(dr1["SALE_PRICE4"].ToString()) : 0f,
                        SALE_PRICE5 = dr1["SALE_PRICE5"] != DBNull.Value ? float.Parse(dr1["SALE_PRICE5"].ToString()) : 0f,
                        IS_INACTIVE = dr1["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr1["IS_INACTIVE"]) : false,
                        IS_NOT_SALE_ITEM = dr1["IS_NOT_SALE_ITEM"] != DBNull.Value ? Convert.ToBoolean(dr1["IS_NOT_SALE_ITEM"]) : false,
                        IS_NOT_SALE_RETURN = dr1["IS_NOT_SALE_RETURN"] != DBNull.Value ? Convert.ToBoolean(dr1["IS_NOT_SALE_RETURN"]) : false,
                        IS_PRICE_REQUIRED = dr1["IS_PRICE_REQUIRED"] != DBNull.Value ? Convert.ToBoolean(dr1["IS_PRICE_REQUIRED"]) : false,
                        IS_NOT_DISCOUNTABLE = dr1["IS_NOT_DISCOUNTABLE"] != DBNull.Value ? Convert.ToBoolean(dr1["IS_NOT_DISCOUNTABLE"]) : false,
                        STORE_CODE = dr1["STORE_CODE"] != DBNull.Value ? Convert.ToString(dr1["STORE_CODE"]) : string.Empty,
                        STORE_NAME = dr1["STORE_NAME"] != DBNull.Value ? Convert.ToString(dr1["STORE_NAME"]) : string.Empty,
                        COST = dr1["COST"] != DBNull.Value ? float.Parse(dr1["COST"].ToString()) : 0f,
                        LAST_MODIFIED_DATE = Convert.IsDBNull(dr1["LAST_MODIFIED_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr1["LAST_MODIFIED_DATE"]),
                        QTY_AVAILABLE = dr1["QTY_AVAILABLE"] != DBNull.Value ? Convert.ToString(dr1["QTY_AVAILABLE"]) : string.Empty,
                        IS_SELECTED = dr1["SELECTED"] != DBNull.Value ? Convert.ToBoolean(dr1["SELECTED"]) : false,
                    });
                }

                // Query to get item suppliers
                strSQL = "SELECT TB_ITEM_SUPPLIER.ID, TB_ITEM_SUPPLIER.SUPP_ID, TB_ITEM_SUPPLIER.REORDER_NO, " +
                    "TB_ITEM_SUPPLIER.COST, TB_ITEM_SUPPLIER.IS_PRIMARY, TB_ITEM_SUPPLIER.IS_CONSIGNMENT, " +
                    "TB_SUPPLIER.SUPP_NAME, TB_SUPPLIER.CURRENCY_ID " +
             "FROM TB_ITEM_SUPPLIER " +
             "INNER JOIN TB_SUPPLIER ON TB_ITEM_SUPPLIER.SUPP_ID = TB_SUPPLIER.ID " +
             "WHERE TB_ITEM_SUPPLIER.ITEM_ID = " + id;

                DataTable tblItemSuppliers = ADO.GetDataTable(strSQL, "ItemSupplier");

                foreach (DataRow dr2 in tblItemSuppliers.Rows)
                {
                    itemsuppliers.Add(new ITEM_SUPPLIERS
                    {
                        ID = dr2["ID"] != DBNull.Value ? Convert.ToInt32(dr2["ID"]) : 0,
                        SUPP_ID = dr2["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(dr2["SUPP_ID"]) : 0,
                        SUPP_NAME = dr2["SUPP_NAME"] != DBNull.Value ? Convert.ToString(dr2["SUPP_NAME"]) : string.Empty,
                        REORDER_NO = dr2["REORDER_NO"] != DBNull.Value ? Convert.ToString(dr2["REORDER_NO"]) : string.Empty,
                        COST = dr2["COST"] != DBNull.Value ? Convert.ToSingle(dr2["COST"]) : 0f,
                        IS_PRIMARY = dr2["IS_PRIMARY"] != DBNull.Value ? Convert.ToBoolean(dr2["IS_PRIMARY"]) : false,
                        IS_CONSIGNMENT = dr2["IS_CONSIGNMENT"] != DBNull.Value ? Convert.ToBoolean(dr2["IS_CONSIGNMENT"]) : false

                    });
                }

                // Query to get item aliases
                strSQL = "SELECT * FROM TB_ITEM_ALIAS WHERE  ITEM_ID =" + id;

                DataTable tblItemAlias = ADO.GetDataTable(strSQL, "ItemAlias");

                foreach (DataRow dr1 in tblItemAlias.Rows)
                {
                    itemalias.Add(new ITEM_ALIAS
                    {
                        ID = dr1["ID"] != DBNull.Value ? Convert.ToInt32(dr1["ID"]) : 0,
                        ALIAS = dr1["ALIAS"] != DBNull.Value ? Convert.ToString(dr1["ALIAS"]) : string.Empty,
                        IS_DEFAULT = dr1["IS_DEFAULT"] != DBNull.Value ? Convert.ToBoolean(dr1["IS_DEFAULT"]) : (bool?)null,
                        ALIAS_TYPE_ID = dr1["ALIAS_TYPE_ID"] != DBNull.Value ? Convert.ToInt32(dr1["ALIAS_TYPE_ID"]) : (int?)null
                        //IS_DEFAULT = Convert.IsDBNull(dr["IS_DEFAULT"]) ? (bool?)null : Convert.ToBoolean(dr["IS_DEFAULT"])
                    });
                }



                strSQL = "SELECT TB_ITEM_COMPONENTS.*, TB_ITEMS.DESCRIPTION, TB_ITEMS.ITEM_CODE " +
                    "FROM TB_ITEM_COMPONENTS" +
                    " LEFT JOIN TB_ITEMS ON TB_ITEM_COMPONENTS.COMPONENT_ITEM_ID = TB_ITEMS.ID" +
                    " WHERE TB_ITEM_COMPONENTS.ITEM_ID =" + id;

                DataTable tblItemComponent = ADO.GetDataTable(strSQL, "ItemComponent");

                foreach (DataRow dr3 in tblItemComponent.Rows)
                {
                    itemcomponent.Add(new ITEM_COMPONENTS
                    {
                        ID = dr3["ID"] != DBNull.Value ? Convert.ToInt32(dr3["ID"]) : (int?)null,
                        ITEM_ID = dr3["ITEM_ID"] != DBNull.Value ? Convert.ToInt32(dr3["ITEM_ID"]) : (int?)null,
                        COMPONENT_ITEM_ID = dr3["COMPONENT_ITEM_ID"] != DBNull.Value ? Convert.ToInt32(dr3["COMPONENT_ITEM_ID"]) : (int?)null,
                        QUANTITY = dr3["QUANTITY"] != DBNull.Value ? Convert.ToSingle(dr3["QUANTITY"]) : (float?)null,
                        UOM = dr3["UOM"] != DBNull.Value ? Convert.ToString(dr3["UOM"]) : string.Empty,
                        ITEM_CODE = dr3["ITEM_CODE"] != DBNull.Value ? Convert.ToString(dr3["ITEM_CODE"]) : string.Empty,
                        DESCRIPTION = dr3["DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr3["DESCRIPTION"]) : string.Empty


                    });
                }

                item.item_stores = itemstores;
                item.item_suppliers = itemsuppliers;
                item.item_alias = itemalias;
                item.item_components = itemcomponent;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        public bool DeleteItem(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_ITEMS";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ItemsResponse Alias(ALIAS_DUPLICATE vInput)
        {
            SqlConnection connection = ADO.GetConnection();
            ItemsResponse res = new ItemsResponse();
            try
            {
                DataTable tbl = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ALIAS_CHECKING";
                cmd.Parameters.AddWithValue("@ALIAS", vInput.ALIAS);


                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(tbl);

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    {
                        res.flag = "1";
                        res.message = "Succes";
                        // res.ITEM_CODE = dr["ITEM_CODE"].ToString();
                        //res.DESCRIPTION = dr["DESCRIPTION"].ToString();

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

    }

}
