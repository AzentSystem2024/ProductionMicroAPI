using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class WorksheetItemPropertyService:IWorksheetItemPropertyService
    {
        public List<WorksheetItem> GetAllWorksheetItem()
        {
            List<WorksheetItem> worksheetList = new List<WorksheetItem>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TB_WORKSHEET.ID, TB_WORKSHEET.WS_NO, TB_WORKSHEET.WS_DATE, " +
             "TB_AC_TRANS_HEADER.NARRATION, TB_STATUS.STATUS_DESC " +
             "FROM TB_WORKSHEET " +
             "LEFT JOIN TB_AC_TRANS_HEADER ON TB_WORKSHEET.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
             "LEFT JOIN TB_STATUS ON TB_AC_TRANS_HEADER.TRANS_STATUS = TB_STATUS.ID " +
             "WHERE WS_TYPE = 1";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new WorksheetItem
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    WS_NO = ADO.ToString(dr["WS_NO"]),
                    WS_DATE = Convert.ToDateTime(dr["WS_DATE"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    Status = ADO.ToString(dr["STATUS_DESC"])
                });
            }
            connection.Close();

            return worksheetList;
        }
        public bool Insert(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("IS_PRICE_REQUIRED_OLD", typeof(bool));
                tbl.Columns.Add("IS_PRICE_REQUIRED_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_NEW", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_OLD", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_NEW", typeof(bool));
                foreach (WorksheetItemProperties ur in worksheet.worksheet_item_property)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["IS_PRICE_REQUIRED_OLD"] = ur.IS_PRICE_REQUIRED;
                    dRow["IS_PRICE_REQUIRED_NEW"] = ur.IS_PRICE_REQUIRED_NEW;
                    dRow["IS_NOT_DISCOUNTABLE_OLD"] = ur.IS_NOT_DISCOUNTABLE;
                    dRow["IS_NOT_DISCOUNTABLE_NEW"] = ur.IS_NOT_DISCOUNTABLE_NEW;
                    dRow["IS_NOT_SALE_ITEM_OLD"] = ur.IS_NOT_SALE_ITEM;
                    dRow["IS_NOT_SALE_ITEM_NEW"] = ur.IS_NOT_SALE_ITEM_NEW;
                    dRow["IS_NOT_SALE_RETURN_OLD"] = ur.IS_NOT_SALE_RETURN;
                    dRow["IS_NOT_SALE_RETURN_NEW"] = ur.IS_NOT_SALE_RETURN_NEW;
                    dRow["IS_INACTIVE_OLD"] = ur.IS_INACTIVE;
                    dRow["IS_INACTIVE_NEW"] = ur.IS_INACTIVE_NEW;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PROPERTIES";

                cmd.Parameters.AddWithValue("ACTION", 1);
                //cmd.Parameters.AddWithValue("ID", items.ID);
                //cmd.Parameters.AddWithValue("WS_TYPE", worksheet.WS_TYPE);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROPERTIE", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public bool Update(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("IS_PRICE_REQUIRED_OLD", typeof(bool));
                tbl.Columns.Add("IS_PRICE_REQUIRED_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_NEW", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_OLD", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_NEW", typeof(bool));
                foreach (WorksheetItemProperties ur in worksheet.worksheet_item_property)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["IS_PRICE_REQUIRED_OLD"] = ur.IS_PRICE_REQUIRED;
                    dRow["IS_PRICE_REQUIRED_NEW"] = ur.IS_PRICE_REQUIRED_NEW;
                    dRow["IS_NOT_DISCOUNTABLE_OLD"] = ur.IS_NOT_DISCOUNTABLE;
                    dRow["IS_NOT_DISCOUNTABLE_NEW"] = ur.IS_NOT_DISCOUNTABLE_NEW;
                    dRow["IS_NOT_SALE_ITEM_OLD"] = ur.IS_NOT_SALE_ITEM;
                    dRow["IS_NOT_SALE_ITEM_NEW"] = ur.IS_NOT_SALE_ITEM_NEW;
                    dRow["IS_NOT_SALE_RETURN_OLD"] = ur.IS_NOT_SALE_RETURN;
                    dRow["IS_NOT_SALE_RETURN_NEW"] = ur.IS_NOT_SALE_RETURN_NEW;
                    dRow["IS_INACTIVE_OLD"] = ur.IS_INACTIVE;
                    dRow["IS_INACTIVE_NEW"] = ur.IS_INACTIVE_NEW;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PROPERTIES";

                cmd.Parameters.AddWithValue("ACTION", 2);
                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                //cmd.Parameters.AddWithValue("WS_TYPE", worksheet.WS_TYPE);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROPERTIE", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public bool DeleteItemProperty(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PROPERTIES";
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
        public bool Verify(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("IS_PRICE_REQUIRED_OLD", typeof(bool));
                tbl.Columns.Add("IS_PRICE_REQUIRED_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_NEW", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_OLD", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_NEW", typeof(bool));
                foreach (WorksheetItemProperties ur in worksheet.worksheet_item_property)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["IS_PRICE_REQUIRED_OLD"] = ur.IS_PRICE_REQUIRED;
                    dRow["IS_PRICE_REQUIRED_NEW"] = ur.IS_PRICE_REQUIRED_NEW;
                    dRow["IS_NOT_DISCOUNTABLE_OLD"] = ur.IS_NOT_DISCOUNTABLE;
                    dRow["IS_NOT_DISCOUNTABLE_NEW"] = ur.IS_NOT_DISCOUNTABLE_NEW;
                    dRow["IS_NOT_SALE_ITEM_OLD"] = ur.IS_NOT_SALE_ITEM;
                    dRow["IS_NOT_SALE_ITEM_NEW"] = ur.IS_NOT_SALE_ITEM_NEW;
                    dRow["IS_NOT_SALE_RETURN_OLD"] = ur.IS_NOT_SALE_RETURN;
                    dRow["IS_NOT_SALE_RETURN_NEW"] = ur.IS_NOT_SALE_RETURN_NEW;
                    dRow["IS_INACTIVE_OLD"] = ur.IS_INACTIVE;
                    dRow["IS_INACTIVE_NEW"] = ur.IS_INACTIVE_NEW;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PROPERTIES";

                cmd.Parameters.AddWithValue("ACTION", 5);
                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                //cmd.Parameters.AddWithValue("WS_TYPE", worksheet.WS_TYPE);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROPERTIE", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public bool Approval(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("IS_PRICE_REQUIRED_OLD", typeof(bool));
                tbl.Columns.Add("IS_PRICE_REQUIRED_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_DISCOUNTABLE_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_ITEM_NEW", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_OLD", typeof(bool));
                tbl.Columns.Add("IS_NOT_SALE_RETURN_NEW", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_OLD", typeof(bool));
                tbl.Columns.Add("IS_INACTIVE_NEW", typeof(bool));
                foreach (WorksheetItemProperties ur in worksheet.worksheet_item_property)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["IS_PRICE_REQUIRED_OLD"] = ur.IS_PRICE_REQUIRED;
                    dRow["IS_PRICE_REQUIRED_NEW"] = ur.IS_PRICE_REQUIRED_NEW;
                    dRow["IS_NOT_DISCOUNTABLE_OLD"] = ur.IS_NOT_DISCOUNTABLE;
                    dRow["IS_NOT_DISCOUNTABLE_NEW"] = ur.IS_NOT_DISCOUNTABLE_NEW;
                    dRow["IS_NOT_SALE_ITEM_OLD"] = ur.IS_NOT_SALE_ITEM;
                    dRow["IS_NOT_SALE_ITEM_NEW"] = ur.IS_NOT_SALE_ITEM_NEW;
                    dRow["IS_NOT_SALE_RETURN_OLD"] = ur.IS_NOT_SALE_RETURN;
                    dRow["IS_NOT_SALE_RETURN_NEW"] = ur.IS_NOT_SALE_RETURN_NEW;
                    dRow["IS_INACTIVE_OLD"] = ur.IS_INACTIVE;
                    dRow["IS_INACTIVE_NEW"] = ur.IS_INACTIVE_NEW;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PROPERTIES";

                cmd.Parameters.AddWithValue("ACTION", 6);
                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                //cmd.Parameters.AddWithValue("WS_TYPE", worksheet.WS_TYPE);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROPERTIE", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public WorksheetItem GetItems(int id)
        {
            WorksheetItem item = new WorksheetItem();
            List<WorksheetItemProperties> itemstores = new List<WorksheetItemProperties>();
            List<ItemStoreProperty> itemsuppliers = new List<ItemStoreProperty>();
            try
            {
                string strSQL = "SELECT ID,WS_TYPE, WS_NO,WS_DATE from TB_WORKSHEET WHERE TB_WORKSHEET.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    item = new WorksheetItem
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        WS_NO = ADO.ToString(dr["WS_NO"]),
                        WS_DATE = Convert.ToDateTime(dr["WS_DATE"]),
                        //Narration = ADO.ToString(dr["NARRATION"]),
                        //Status = ADO.ToString(dr["STATUS_DESC"])

                    };
                }


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = ADO.GetConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_STORE_ITEM_NEW";
                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@WS_ID", id);
                DataTable tblItemStores = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tblItemStores);


                // Process the results
                foreach (DataRow dr2 in tblItemStores.Rows)
                {
                    itemstores.Add(new WorksheetItemProperties
                    {
                        BARCODE = ADO.ToString(dr2["BARCODE"]),
                        DESCRIPTION = ADO.ToString(dr2["DESCRIPTION"]),
                        DEPT_NAME = ADO.ToString(dr2["DEPT_NAME"]),
                        CAT_NAME = ADO.ToString(dr2["CAT_NAME"]),
                        BRAND_NAME = ADO.ToString(dr2["BRAND_NAME"]),
                        ID = ADO.ToInt32(dr2["ID"]),
                        ITEM_ID = ADO.ToInt32(dr2["ITEM_ID"]),
                        IS_PRICE_REQUIRED_NEW = ADO.Toboolean(dr2["IS_PRICE_REQUIRED_NEW"]),
                        IS_NOT_DISCOUNTABLE_NEW = ADO.Toboolean(dr2["IS_NOT_DISCOUNTABLE_NEW"]),
                        IS_NOT_SALE_ITEM_NEW = ADO.Toboolean(dr2["IS_NOT_SALE_ITEM_NEW"]),
                        IS_NOT_SALE_RETURN_NEW = ADO.Toboolean(dr2["IS_NOT_SALE_RETURN_NEW"]),
                        IS_INACTIVE_NEW = ADO.Toboolean(dr2["IS_INACTIVE_NEW"]),
                        Selected = ADO.Toboolean(dr2["Selected"]),
                        STORE_ID = ADO.ToInt32(dr2["STORE_ID"]),
                        STORE_NAME = ADO.ToString(dr2["STORE_NAME"])

                    });
                }

                strSQL = "select * from TB_WORKSHEET_STORE WHERE TB_WORKSHEET_STORE.WS_ID = " + id;

                DataTable tblItemSuppliers1 = ADO.GetDataTable(strSQL, "ItemPropertyStore");

                foreach (DataRow dr3 in tblItemSuppliers1.Rows)
                {
                    itemsuppliers.Add(new ItemStoreProperty
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        WS_ID = ADO.ToInt32(dr3["WS_ID"]),
                        STORE_ID = ADO.ToInt32(dr3["STORE_ID"])

                    });
                }
                item.worksheet_item_property = itemstores;
                item.worksheet_item_store = itemsuppliers;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        public List<ItemStoreProperties> GetItemPropertyList()
        {
            List<ItemStoreProperties> ItemPropertyList = new List<ItemStoreProperties>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_STORE_ITEM_NEW";
            cmd.Parameters.AddWithValue("ACTION", 0);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                ItemPropertyList.Add(new ItemStoreProperties
                {
                    BARCODE = ADO.ToString(dr["BARCODE"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),
                    CAT_NAME = ADO.ToString(dr["CAT_NAME"]),
                    BRAND_NAME = ADO.ToString(dr["BRAND_NAME"]),
                    ITEM_ID = ADO.ToInt32(dr["ITEM_ID"]),
                    STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                    STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                    IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"]),
                    IS_NOT_SALE_ITEM = ADO.Toboolean(dr["IS_NOT_SALE_ITEM"]),
                    IS_NOT_SALE_RETURN = ADO.Toboolean(dr["IS_NOT_SALE_RETURN"]),
                    IS_PRICE_REQUIRED = ADO.Toboolean(dr["IS_PRICE_REQUIRED"]),
                    IS_NOT_DISCOUNTABLE = ADO.Toboolean(dr["IS_NOT_DISCOUNTABLE"]),
                    Selected = ADO.Toboolean(dr["Selected"])

                });
            }
            connection.Close();

            return ItemPropertyList;
        }
        public List<WorksheetItem> GetAllWorksheetItemPrice()
        {
            List<WorksheetItem> worksheetList = new List<WorksheetItem>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TB_WORKSHEET.ID, TB_WORKSHEET.WS_NO, TB_WORKSHEET.WS_DATE, " +
             "TB_AC_TRANS_HEADER.NARRATION, TB_STATUS.STATUS_DESC " +
             "FROM TB_WORKSHEET " +
             "LEFT JOIN TB_AC_TRANS_HEADER ON TB_WORKSHEET.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
             "LEFT JOIN TB_STATUS ON TB_AC_TRANS_HEADER.TRANS_STATUS = TB_STATUS.ID " +
             "WHERE WS_TYPE = 2";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new WorksheetItem
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    WS_NO = ADO.ToString(dr["WS_NO"]),
                    WS_DATE = Convert.ToDateTime(dr["WS_DATE"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    Status = ADO.ToString(dr["STATUS_DESC"])
                });
            }
            connection.Close();

            return worksheetList;
        }
        public Int32 InsertPrice(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32)).AllowDBNull = true;
                tbl.Columns.Add("ITEM_ID", typeof(Int32)).AllowDBNull = true;
                tbl.Columns.Add("PRICE", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL1", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL2", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL3", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL4", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL5", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL1_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL2_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL3_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL4_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL5_NEW", typeof(float)).AllowDBNull = true;

                foreach (WorksheetItemPrice ur in worksheet.worksheet_item_price)
                {
                    DataRow dRow = tbl.NewRow();
                    dRow["ID"] = ur.ID.HasValue ? (object)ur.ID.Value : DBNull.Value;
                    dRow["ITEM_ID"] = ur.ITEM_ID.HasValue ? (object)ur.ITEM_ID.Value : DBNull.Value;
                    dRow["PRICE"] = ur.SALE_PRICE.HasValue ? (object)ur.SALE_PRICE.Value : DBNull.Value;
                    dRow["PRICE_LEVEL1"] = ur.SALE_PRICE1.HasValue ? (object)ur.SALE_PRICE1.Value : DBNull.Value;
                    dRow["PRICE_LEVEL2"] = ur.SALE_PRICE2.HasValue ? (object)ur.SALE_PRICE2.Value : DBNull.Value;
                    dRow["PRICE_LEVEL3"] = ur.SALE_PRICE3.HasValue ? (object)ur.SALE_PRICE3.Value : DBNull.Value;
                    dRow["PRICE_LEVEL4"] = ur.SALE_PRICE4.HasValue ? (object)ur.SALE_PRICE4.Value : DBNull.Value;
                    dRow["PRICE_LEVEL5"] = ur.SALE_PRICE5.HasValue ? (object)ur.SALE_PRICE5.Value : DBNull.Value;
                    dRow["PRICE_NEW"] = ur.PRICE_NEW.HasValue ? (object)ur.PRICE_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL1_NEW"] = ur.PRICE_LEVEL1_NEW.HasValue ? (object)ur.PRICE_LEVEL1_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL2_NEW"] = ur.PRICE_LEVEL2_NEW.HasValue ? (object)ur.PRICE_LEVEL2_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL3_NEW"] = ur.PRICE_LEVEL3_NEW.HasValue ? (object)ur.PRICE_LEVEL3_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL4_NEW"] = ur.PRICE_LEVEL4_NEW.HasValue ? (object)ur.PRICE_LEVEL4_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL5_NEW"] = ur.PRICE_LEVEL5_NEW.HasValue ? (object)ur.PRICE_LEVEL5_NEW.Value : DBNull.Value;
                    tbl.Rows.Add(dRow);
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PRICE";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PRICE", tbl);

                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();

                cmd1.Connection = connection;
                cmd1.Transaction = objtrans;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(ID) FROM TB_WORKSHEET";


                Int32 UserID = Convert.ToInt32(cmd1.ExecuteScalar());

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
        public bool UpdatePrice(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL1", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL2", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL3", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL4", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL5", typeof(float));
                tbl.Columns.Add("PRICE_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL1_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL2_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL3_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL4_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL5_NEW", typeof(float));
                foreach (WorksheetItemPrice ur in worksheet.worksheet_item_price)
                {
                    DataRow dRow = tbl.NewRow();
                    dRow["ID"] = ur.ID.HasValue ? (object)ur.ID.Value : DBNull.Value;
                    dRow["ITEM_ID"] = ur.ITEM_ID.HasValue ? (object)ur.ITEM_ID.Value : DBNull.Value;
                    dRow["PRICE"] = ur.SALE_PRICE.HasValue ? (object)ur.SALE_PRICE.Value : DBNull.Value;
                    dRow["PRICE_LEVEL1"] = ur.SALE_PRICE1.HasValue ? (object)ur.SALE_PRICE1.Value : DBNull.Value;
                    dRow["PRICE_LEVEL2"] = ur.SALE_PRICE2.HasValue ? (object)ur.SALE_PRICE2.Value : DBNull.Value;
                    dRow["PRICE_LEVEL3"] = ur.SALE_PRICE3.HasValue ? (object)ur.SALE_PRICE3.Value : DBNull.Value;
                    dRow["PRICE_LEVEL4"] = ur.SALE_PRICE4.HasValue ? (object)ur.SALE_PRICE4.Value : DBNull.Value;
                    dRow["PRICE_LEVEL5"] = ur.SALE_PRICE5.HasValue ? (object)ur.SALE_PRICE5.Value : DBNull.Value;
                    dRow["PRICE_NEW"] = ur.PRICE_NEW.HasValue ? (object)ur.PRICE_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL1_NEW"] = ur.PRICE_LEVEL1_NEW.HasValue ? (object)ur.PRICE_LEVEL1_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL2_NEW"] = ur.PRICE_LEVEL2_NEW.HasValue ? (object)ur.PRICE_LEVEL2_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL3_NEW"] = ur.PRICE_LEVEL3_NEW.HasValue ? (object)ur.PRICE_LEVEL3_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL4_NEW"] = ur.PRICE_LEVEL4_NEW.HasValue ? (object)ur.PRICE_LEVEL4_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL5_NEW"] = ur.PRICE_LEVEL5_NEW.HasValue ? (object)ur.PRICE_LEVEL5_NEW.Value : DBNull.Value;
                    tbl.Rows.Add(dRow);
                    //tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PRICE";

                cmd.Parameters.AddWithValue("ACTION", 2);

                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", (object)worksheet.NARRATION ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PRICE", tbl);

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
        public WorksheetItem GetPriceItem(int id)
        {
            WorksheetItem item = new WorksheetItem();
            List<WorksheetItemPrice> itemstores = new List<WorksheetItemPrice>();
            List<ItemStoreProperty> itemsuppliers = new List<ItemStoreProperty>();
            try
            {
                string strSQL = "SELECT ID,WS_TYPE, WS_NO,WS_DATE,TB_AC_TRANS_HEADER.NARRATION from TB_WORKSHEET INNER JOIN TB_AC_TRANS_HEADER ON TB_WORKSHEET.TRANS_ID=TB_AC_TRANS_HEADER.TRANS_ID WHERE WS_TYPE = 2 and TB_WORKSHEET.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ItemProperty");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    item = new WorksheetItem
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        WS_NO = ADO.ToString(dr["WS_NO"]),
                        WS_DATE = Convert.ToDateTime(dr["WS_DATE"]),
                        NARRATION = ADO.ToString(dr["NARRATION"]),
                    };
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ADO.GetConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PRICE_ITEM_NEW";
                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@WS_ID", id);
                DataTable tblItemStores = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tblItemStores);

                // Process the results
                foreach (DataRow dr2 in tblItemStores.Rows)
                {
                    itemstores.Add(new WorksheetItemPrice
                    {
                        ID = ADO.ToInt32(dr2["ID"]),
                        ITEM_ID = ADO.ToInt32(dr2["ITEMS_ID"]),
                        SALE_PRICE = ADO.ToFloat(dr2["PRICE"]),
                        SALE_PRICE1 = ADO.ToFloat(dr2["PRICE_LEVEL1"]),
                        SALE_PRICE2 = ADO.ToFloat(dr2["PRICE_LEVEL2"]),
                        SALE_PRICE3 = ADO.ToFloat(dr2["PRICE_LEVEL3"]),
                        SALE_PRICE4 = ADO.ToFloat(dr2["PRICE_LEVEL4"]),
                        SALE_PRICE5 = ADO.ToFloat(dr2["PRICE_LEVEL5"]),
                        BARCODE = ADO.ToString(dr2["BARCODE"]),
                        DESCRIPTION = ADO.ToString(dr2["DESCRIPTION"]),
                        DEPT_NAME = ADO.ToString(dr2["DEPT_NAME"]),
                        CAT_NAME = ADO.ToString(dr2["CAT_NAME"]),
                        BRAND_NAME = ADO.ToString(dr2["BRAND_NAME"]),
                        Selected = ADO.Toboolean(dr2["Selected"]),
                        PRICE_NEW = ADO.ToFloat(dr2["PRICE_NEW"]),
                        PRICE_LEVEL1_NEW = ADO.ToFloat(dr2["PRICE_LEVEL1_NEW"]),
                        PRICE_LEVEL2_NEW = ADO.ToFloat(dr2["PRICE_LEVEL2_NEW"]),
                        PRICE_LEVEL3_NEW = ADO.ToFloat(dr2["PRICE_LEVEL3_NEW"]),
                        PRICE_LEVEL4_NEW = ADO.ToFloat(dr2["PRICE_LEVEL4_NEW"]),
                        PRICE_LEVEL5_NEW = ADO.ToFloat(dr2["PRICE_LEVEL5_NEW"]),
                        ITEM_CODE = ADO.ToString(dr2["ITEM_CODE"]),
                        ITEM_TYPE = ADO.ToString(dr2["TYPE_NAME"])

                    });
                }


                strSQL = "select * from TB_WORKSHEET_STORE WHERE TB_WORKSHEET_STORE.WS_ID = " + id;

                DataTable tblItemSuppliers1 = ADO.GetDataTable(strSQL, "ItemPriceStore");

                foreach (DataRow dr3 in tblItemSuppliers1.Rows)
                {
                    itemsuppliers.Add(new ItemStoreProperty
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        WS_ID = ADO.ToInt32(dr3["WS_ID"]),
                        STORE_ID = ADO.ToInt32(dr3["STORE_ID"])

                    });
                }
                item.worksheet_item_price = itemstores;
                item.worksheet_item_store = itemsuppliers;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        public List<ItemPriceProperties> GetItemPriceList()
        {
            List<ItemPriceProperties> ItemPropertyList = new List<ItemPriceProperties>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_PRICE_ITEM_NEW";
            cmd.Parameters.AddWithValue("ACTION", 0);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                ItemPropertyList.Add(new ItemPriceProperties
                {
                    BARCODE = ADO.ToString(dr["BARCODE"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),
                    CAT_NAME = ADO.ToString(dr["CAT_NAME"]),
                    BRAND_NAME = ADO.ToString(dr["BRAND_NAME"]),
                    ITEM_ID = ADO.ToInt32(dr["ITEM_ID"]),
                    STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                    STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                    SALE_PRICE = ADO.ToFloat(dr["SALE_PRICE"]),
                    SALE_PRICE1 = ADO.ToFloat(dr["SALE_PRICE1"]),
                    SALE_PRICE2 = ADO.ToFloat(dr["SALE_PRICE2"]),
                    SALE_PRICE3 = ADO.ToFloat(dr["SALE_PRICE3"]),
                    SALE_PRICE4 = ADO.ToFloat(dr["SALE_PRICE4"]),
                    SALE_PRICE5 = ADO.ToFloat(dr["SALE_PRICE5"]),
                    Selected = ADO.Toboolean(dr["Selected"])

                });
            }
            connection.Close();

            return ItemPropertyList;
        }
        public bool DeleteItemPrice(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PRICE";
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
        public bool VerifyPrice(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL1", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL2", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL3", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL4", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL5", typeof(float));
                tbl.Columns.Add("PRICE_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL1_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL2_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL3_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL4_NEW", typeof(float));
                tbl.Columns.Add("PRICE_LEVEL5_NEW", typeof(float));
                foreach (WorksheetItemPrice ur in worksheet.worksheet_item_price)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["PRICE"] = ur.SALE_PRICE;
                    dRow["PRICE_LEVEL1"] = ur.SALE_PRICE1;
                    dRow["PRICE_LEVEL2"] = ur.SALE_PRICE2;
                    dRow["PRICE_LEVEL3"] = ur.SALE_PRICE3;
                    dRow["PRICE_LEVEL4"] = ur.SALE_PRICE4;
                    dRow["PRICE_LEVEL5"] = ur.SALE_PRICE5;
                    dRow["PRICE_NEW"] = ur.PRICE_NEW;
                    dRow["PRICE_LEVEL1_NEW"] = ur.PRICE_LEVEL1_NEW;
                    dRow["PRICE_LEVEL2_NEW"] = ur.PRICE_LEVEL2_NEW;
                    dRow["PRICE_LEVEL3_NEW"] = ur.PRICE_LEVEL3_NEW;
                    dRow["PRICE_LEVEL4_NEW"] = ur.PRICE_LEVEL4_NEW;
                    dRow["PRICE_LEVEL5_NEW"] = ur.PRICE_LEVEL5_NEW;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PRICE";

                cmd.Parameters.AddWithValue("ACTION", 5);

                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PRICE", tbl);

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
        public bool ApprovalPrice(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32)).AllowDBNull = true;
                tbl.Columns.Add("ITEM_ID", typeof(Int32)).AllowDBNull = true;
                tbl.Columns.Add("PRICE", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL1", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL2", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL3", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL4", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL5", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL1_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL2_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL3_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL4_NEW", typeof(float)).AllowDBNull = true;
                tbl.Columns.Add("PRICE_LEVEL5_NEW", typeof(float)).AllowDBNull = true;
                foreach (WorksheetItemPrice ur in worksheet.worksheet_item_price)
                {
                    DataRow dRow = tbl.NewRow();
                    dRow["ID"] = ur.ID.HasValue ? (object)ur.ID.Value : DBNull.Value;
                    dRow["ITEM_ID"] = ur.ITEM_ID.HasValue ? (object)ur.ITEM_ID.Value : DBNull.Value;
                    dRow["PRICE"] = ur.SALE_PRICE.HasValue ? (object)ur.SALE_PRICE.Value : DBNull.Value;
                    dRow["PRICE_LEVEL1"] = ur.SALE_PRICE1.HasValue ? (object)ur.SALE_PRICE1.Value : DBNull.Value;
                    dRow["PRICE_LEVEL2"] = ur.SALE_PRICE2.HasValue ? (object)ur.SALE_PRICE2.Value : DBNull.Value;
                    dRow["PRICE_LEVEL3"] = ur.SALE_PRICE3.HasValue ? (object)ur.SALE_PRICE3.Value : DBNull.Value;
                    dRow["PRICE_LEVEL4"] = ur.SALE_PRICE4.HasValue ? (object)ur.SALE_PRICE4.Value : DBNull.Value;
                    dRow["PRICE_LEVEL5"] = ur.SALE_PRICE5.HasValue ? (object)ur.SALE_PRICE5.Value : DBNull.Value;
                    dRow["PRICE_NEW"] = ur.PRICE_NEW.HasValue ? (object)ur.PRICE_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL1_NEW"] = ur.PRICE_LEVEL1_NEW.HasValue ? (object)ur.PRICE_LEVEL1_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL2_NEW"] = ur.PRICE_LEVEL2_NEW.HasValue ? (object)ur.PRICE_LEVEL2_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL3_NEW"] = ur.PRICE_LEVEL3_NEW.HasValue ? (object)ur.PRICE_LEVEL3_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL4_NEW"] = ur.PRICE_LEVEL4_NEW.HasValue ? (object)ur.PRICE_LEVEL4_NEW.Value : DBNull.Value;
                    dRow["PRICE_LEVEL5_NEW"] = ur.PRICE_LEVEL5_NEW.HasValue ? (object)ur.PRICE_LEVEL5_NEW.Value : DBNull.Value;
                    tbl.Rows.Add(dRow);
                }
                tbl.AcceptChanges();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_ITEM_PRICE";

                cmd.Parameters.AddWithValue("ACTION", 6);

                cmd.Parameters.AddWithValue("ID", worksheet.ID.HasValue ? (object)worksheet.ID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID.HasValue ? (object)worksheet.COMPANY_ID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID.HasValue ? (object)worksheet.USER_ID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("STORE_ID", (object)worksheet.STORE_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("NARRATION", (object)worksheet.NARRATION ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PRICE", tbl);
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
        public List<WorksheetItem> GetAllWorksheetPromotionSchema()
        {
            List<WorksheetItem> worksheetList = new List<WorksheetItem>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TB_WORKSHEET.ID, TB_WORKSHEET.WS_NO, TB_WORKSHEET.WS_DATE, " +
             "TB_AC_TRANS_HEADER.NARRATION, TB_STATUS.STATUS_DESC " +
             "FROM TB_WORKSHEET " +
             "LEFT JOIN TB_AC_TRANS_HEADER ON TB_WORKSHEET.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
             "LEFT JOIN TB_STATUS ON TB_AC_TRANS_HEADER.TRANS_STATUS = TB_STATUS.ID " +
             "WHERE WS_TYPE = 3";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                worksheetList.Add(new WorksheetItem
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    WS_NO = ADO.ToString(dr["WS_NO"]),
                    WS_DATE = Convert.ToDateTime(dr["WS_DATE"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    Status = ADO.ToString(dr["STATUS_DESC"])
                });
            }
            connection.Close();

            return worksheetList;
        }
        public bool InsertPromotion(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("PROMOTION_PRICE", typeof(float));
                tbl.Columns.Add("DATE_FROM", typeof(DateTime));
                tbl.Columns.Add("DATE_TO", typeof(DateTime));
                tbl.Columns.Add("TIME_FROM", typeof(DateTime));
                tbl.Columns.Add("TIME_TO", typeof(DateTime));
                tbl.Columns.Add("PROMOTION_SCHEMA_ID", typeof(int));
                tbl.Columns.Add("PROMOTION_WEEKDAYS", typeof(string));
                tbl.Columns.Add("PROMOTION_LEVEL", typeof(int));
                tbl.Columns.Add("IS_INACTIVE", typeof(bool));
                tbl.Columns.Add("PROMOTION_NAME", typeof(string));
                tbl.Columns.Add("PROMOTION_GROUP_ID", typeof(int));
                tbl.Columns.Add("IS_BUY", typeof(bool));
                tbl.Columns.Add("IS_GET", typeof(bool));
                tbl.Columns.Add("IS_HAPPY_HOUR", typeof(bool));

                foreach (WorksheetPromotionSchema ur in worksheet.worksheet_promotion_schema)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["PRICE"] = ur.PRICE;
                    dRow["COST"] = ur.COST;
                    dRow["PROMOTION_PRICE"] = ur.PROMOTION_PRICE;
                    dRow["DATE_FROM"] = ur.DATE_FROM;
                    dRow["DATE_TO"] = ur.DATE_TO;
                    dRow["TIME_FROM"] = ur.TIME_FROM;
                    dRow["TIME_TO"] = ur.TIME_TO;
                    dRow["PROMOTION_SCHEMA_ID"] = ur.PROMOTION_SCHEMA_ID;
                    dRow["PROMOTION_WEEKDAYS"] = ur.PROMOTION_WEEKDAYS;
                    dRow["PROMOTION_LEVEL"] = ur.PROMOTION_LEVEL;
                    dRow["IS_INACTIVE"] = ur.IS_INACTIVE;
                    dRow["PROMOTION_NAME"] = ur.PROMOTION_NAME;
                    dRow["PROMOTION_GROUP_ID"] = ur.PROMOTION_GROUP_ID;
                    dRow["IS_BUY"] = ur.IS_BUY;
                    dRow["IS_GET"] = ur.IS_GET;
                    dRow["IS_HAPPY_HOUR"] = ur.IS_HAPPY_HOUR;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_PROMOTION_SCHEMA";

                cmd.Parameters.AddWithValue("ACTION", 1);
                //cmd.Parameters.AddWithValue("ID", items.ID);
                //cmd.Parameters.AddWithValue("WS_TYPE", worksheet.WS_TYPE);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROMOTION_SCHEMA", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public bool UpdatePromotion(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("PROMOTION_PRICE", typeof(float));
                tbl.Columns.Add("DATE_FROM", typeof(DateTime));
                tbl.Columns.Add("DATE_TO", typeof(DateTime));
                tbl.Columns.Add("TIME_FROM", typeof(DateTime));
                tbl.Columns.Add("TIME_TO", typeof(DateTime));
                tbl.Columns.Add("PROMOTION_SCHEMA_ID", typeof(int));
                tbl.Columns.Add("PROMOTION_WEEKDAYS", typeof(string));
                tbl.Columns.Add("PROMOTION_LEVEL", typeof(int));
                tbl.Columns.Add("IS_INACTIVE", typeof(bool));
                tbl.Columns.Add("PROMOTION_NAME", typeof(string));
                tbl.Columns.Add("PROMOTION_GROUP_ID", typeof(int));
                tbl.Columns.Add("IS_BUY", typeof(bool));
                tbl.Columns.Add("IS_GET", typeof(bool));
                tbl.Columns.Add("IS_HAPPY_HOUR", typeof(bool));


                foreach (WorksheetPromotionSchema ur in worksheet.worksheet_promotion_schema)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["PRICE"] = ur.PRICE;
                    dRow["COST"] = ur.COST;
                    dRow["PROMOTION_PRICE"] = ur.PROMOTION_PRICE;
                    dRow["DATE_FROM"] = ur.DATE_FROM;
                    dRow["DATE_TO"] = ur.DATE_TO;
                    dRow["TIME_FROM"] = ur.TIME_FROM;
                    dRow["TIME_TO"] = ur.TIME_TO;
                    dRow["PROMOTION_SCHEMA_ID"] = ur.PROMOTION_SCHEMA_ID;
                    dRow["PROMOTION_WEEKDAYS"] = ur.PROMOTION_WEEKDAYS;
                    dRow["PROMOTION_LEVEL"] = ur.PROMOTION_LEVEL;
                    dRow["IS_INACTIVE"] = ur.IS_INACTIVE;
                    dRow["PROMOTION_NAME"] = ur.PROMOTION_NAME;
                    dRow["PROMOTION_GROUP_ID"] = ur.PROMOTION_GROUP_ID;
                    dRow["IS_BUY"] = ur.IS_BUY;
                    dRow["IS_GET"] = ur.IS_GET;
                    dRow["IS_HAPPY_HOUR"] = ur.IS_HAPPY_HOUR;


                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_PROMOTION_SCHEMA";

                cmd.Parameters.AddWithValue("ACTION", 2);
                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROMOTION_SCHEMA", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public bool DeletePromotion(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_PROMOTION_SCHEMA";
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

        public bool VerifyPromotion(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("PROMOTION_PRICE", typeof(float));
                tbl.Columns.Add("DATE_FROM", typeof(DateTime));
                tbl.Columns.Add("DATE_TO", typeof(DateTime));
                tbl.Columns.Add("TIME_FROM", typeof(DateTime));
                tbl.Columns.Add("TIME_TO", typeof(DateTime));
                tbl.Columns.Add("PROMOTION_SCHEMA_ID", typeof(int));
                tbl.Columns.Add("PROMOTION_WEEKDAYS", typeof(string));
                tbl.Columns.Add("PROMOTION_LEVEL", typeof(int));
                tbl.Columns.Add("IS_INACTIVE", typeof(bool));
                tbl.Columns.Add("PROMOTION_NAME", typeof(string));
                tbl.Columns.Add("PROMOTION_GROUP_ID", typeof(int));
                tbl.Columns.Add("IS_BUY", typeof(bool));
                tbl.Columns.Add("IS_GET", typeof(bool));
                tbl.Columns.Add("IS_HAPPY_HOUR", typeof(bool));

                foreach (WorksheetPromotionSchema ur in worksheet.worksheet_promotion_schema)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["PRICE"] = ur.PRICE;
                    dRow["COST"] = ur.COST;
                    dRow["PROMOTION_PRICE"] = ur.PROMOTION_PRICE;
                    dRow["DATE_FROM"] = ur.DATE_FROM;
                    dRow["DATE_TO"] = ur.DATE_TO;
                    dRow["TIME_FROM"] = ur.TIME_FROM;
                    dRow["TIME_TO"] = ur.TIME_TO;
                    dRow["PROMOTION_SCHEMA_ID"] = ur.PROMOTION_SCHEMA_ID;
                    dRow["PROMOTION_WEEKDAYS"] = ur.PROMOTION_WEEKDAYS;
                    dRow["PROMOTION_LEVEL"] = ur.PROMOTION_LEVEL;
                    dRow["IS_INACTIVE"] = ur.IS_INACTIVE;
                    dRow["PROMOTION_NAME"] = ur.PROMOTION_NAME;
                    dRow["PROMOTION_GROUP_ID"] = ur.PROMOTION_GROUP_ID;
                    dRow["IS_BUY"] = ur.IS_BUY;
                    dRow["IS_GET"] = ur.IS_GET;
                    dRow["IS_HAPPY_HOUR"] = ur.IS_HAPPY_HOUR;


                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_PROMOTION_SCHEMA";

                cmd.Parameters.AddWithValue("ACTION", 5);
                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROMOTION_SCHEMA", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public bool ApprovePromotion(WorksheetItem worksheet)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("ITEM_ID", typeof(Int32));
                tbl.Columns.Add("PRICE", typeof(float));
                tbl.Columns.Add("COST", typeof(float));
                tbl.Columns.Add("PROMOTION_PRICE", typeof(float));
                tbl.Columns.Add("DATE_FROM", typeof(DateTime));
                tbl.Columns.Add("DATE_TO", typeof(DateTime));
                tbl.Columns.Add("TIME_FROM", typeof(DateTime));
                tbl.Columns.Add("TIME_TO", typeof(DateTime));
                tbl.Columns.Add("PROMOTION_SCHEMA_ID", typeof(int));
                tbl.Columns.Add("PROMOTION_WEEKDAYS", typeof(string));
                tbl.Columns.Add("PROMOTION_LEVEL", typeof(int));
                tbl.Columns.Add("IS_INACTIVE", typeof(bool));
                tbl.Columns.Add("PROMOTION_NAME", typeof(string));
                tbl.Columns.Add("PROMOTION_GROUP_ID", typeof(int));
                tbl.Columns.Add("IS_BUY", typeof(bool));
                tbl.Columns.Add("IS_GET", typeof(bool));
                tbl.Columns.Add("IS_HAPPY_HOUR", typeof(bool));


                foreach (WorksheetPromotionSchema ur in worksheet.worksheet_promotion_schema)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["ITEM_ID"] = ur.ITEM_ID;
                    dRow["PRICE"] = ur.PRICE;
                    dRow["COST"] = ur.COST;
                    dRow["PROMOTION_PRICE"] = ur.PROMOTION_PRICE;
                    dRow["DATE_FROM"] = ur.DATE_FROM;
                    dRow["DATE_TO"] = ur.DATE_TO;
                    dRow["TIME_FROM"] = ur.TIME_FROM;
                    dRow["TIME_TO"] = ur.TIME_TO;
                    dRow["PROMOTION_SCHEMA_ID"] = ur.PROMOTION_SCHEMA_ID;
                    dRow["PROMOTION_WEEKDAYS"] = ur.PROMOTION_WEEKDAYS;
                    dRow["PROMOTION_LEVEL"] = ur.PROMOTION_LEVEL;
                    dRow["IS_INACTIVE"] = ur.IS_INACTIVE;
                    dRow["PROMOTION_NAME"] = ur.PROMOTION_NAME;
                    dRow["PROMOTION_GROUP_ID"] = ur.PROMOTION_GROUP_ID;
                    dRow["IS_BUY"] = ur.IS_BUY;
                    dRow["IS_GET"] = ur.IS_GET;
                    dRow["IS_HAPPY_HOUR"] = ur.IS_HAPPY_HOUR;


                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WORKSHEET_PROMOTION_SCHEMA";

                cmd.Parameters.AddWithValue("ACTION", 6);
                cmd.Parameters.AddWithValue("ID", worksheet.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", worksheet.COMPANY_ID);
                cmd.Parameters.AddWithValue("USER_ID", worksheet.USER_ID);
                cmd.Parameters.AddWithValue("STORE_ID", worksheet.STORE_ID);
                cmd.Parameters.AddWithValue("NARRATION", worksheet.NARRATION);
                cmd.Parameters.AddWithValue("@UDT_TB_WORKSHEET_PROMOTION_SCHEMA", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public WorksheetItem GetPromotionItem(int id)
        {
            WorksheetItem item = new WorksheetItem();
            List<WorksheetPromotionSchema> itemstores = new List<WorksheetPromotionSchema>();
            List<ItemStoreProperty> itemsuppliers = new List<ItemStoreProperty>();
            try
            {
                string strSQL = "SELECT ID,WS_TYPE, WS_NO,WS_DATE from TB_WORKSHEET WHERE WS_TYPE = 3 and TB_WORKSHEET.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "WorksheetPromotion");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    item = new WorksheetItem
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        WS_NO = ADO.ToString(dr["WS_NO"]),
                        WS_DATE = Convert.ToDateTime(dr["WS_DATE"])
                    };
                }


                string strSQL1 = "select * from TB_WORKSHEET_STORE WHERE TB_WORKSHEET_STORE.WS_ID = " + id;

                DataTable tblItemSuppliers1 = ADO.GetDataTable(strSQL1, "ItemPriceStore");

                foreach (DataRow dr3 in tblItemSuppliers1.Rows)
                {
                    itemsuppliers.Add(new ItemStoreProperty
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        WS_ID = ADO.ToInt32(dr3["WS_ID"]),
                        STORE_ID = ADO.ToInt32(dr3["STORE_ID"])

                    });
                }

                ////string strSQL2 = "SELECT TB_WORKSHEET_PROMOTION.*, TB_PROMOTION_SCHEMA.description, " +
                ////  "TB_PROMOTION_LEVELS.PROMOTION_LEVEL " +
                ////  "FROM TB_WORKSHEET_PROMOTION " +
                ////  "LEFT JOIN TB_PROMOTION_SCHEMA ON " +
                ////  "TB_WORKSHEET_PROMOTION.PROMOTION_SCHEMA_ID = TB_PROMOTION_SCHEMA.ID " +
                ////  "LEFT JOIN TB_PROMOTION_LEVELS ON " +
                ////  "TB_WORKSHEET_PROMOTION.PROMOTION_LEVEL = TB_PROMOTION_LEVELS.ID " +

                string strSQL2 = "SELECT TB_WORKSHEET_PROMOTION.*, TB_PROMOTION_SCHEMA.description, " +
                 "TB_PROMOTION_LEVELS.PROMOTION_LEVEL, TB_ITEMS.CAT_ID, TB_ITEMS.DEPT_ID, TB_ITEM_DEPARTMENT.DEPT_NAME, " +
                 "TB_ITEM_CATEGORY.CAT_NAME, TB_ITEMS.BARCODE, TB_AC_TRANS_HEADER.NARRATION, TB_ITEMS.DESCRIPTION AS ITEM_DESCRIPTION " + // Added space here
                 "FROM TB_WORKSHEET_PROMOTION " + // Added space here
                 "LEFT JOIN TB_PROMOTION_SCHEMA ON TB_WORKSHEET_PROMOTION.PROMOTION_SCHEMA_ID = TB_PROMOTION_SCHEMA.ID " + // Added space here
                 "LEFT JOIN TB_ITEMS ON TB_WORKSHEET_PROMOTION.ITEM_ID = TB_ITEMS.ID " + // Added space here
                 "LEFT JOIN TB_ITEM_DEPARTMENT ON TB_ITEMS.DEPT_ID = TB_ITEM_DEPARTMENT.ID " + // Added space here
                 "LEFT JOIN TB_WORKSHEET ON TB_WORKSHEET_PROMOTION.WS_ID = TB_WORKSHEET.ID " + // Added space here
                 "LEFT JOIN TB_AC_TRANS_HEADER ON TB_WORKSHEET.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " + // Added space here
                 "LEFT JOIN TB_ITEM_CATEGORY ON TB_ITEMS.CAT_ID = TB_ITEM_CATEGORY.ID " + // Added space here
                 "LEFT JOIN TB_PROMOTION_LEVELS ON TB_WORKSHEET_PROMOTION.PROMOTION_LEVEL = TB_PROMOTION_LEVELS.ID " + // Added space here
                 "WHERE TB_WORKSHEET_PROMOTION.WS_ID = " + id; // No space needed here since it's the end of the query




                DataTable tblItemSuppliers2 = ADO.GetDataTable(strSQL2, "ItemPromotion");

                foreach (DataRow dr3 in tblItemSuppliers2.Rows)
                {
                    itemstores.Add(new WorksheetPromotionSchema
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        ITEM_ID = ADO.ToInt32(dr3["ITEM_ID"]),
                        PRICE = ADO.ToFloat(dr3["PRICE"]),
                        COST = ADO.ToFloat(dr3["COST"]),
                        PROMOTION_PRICE = ADO.ToFloat(dr3["PROMOTION_PRICE"]),
                        DATE_FROM = Convert.ToDateTime(dr3["DATE_FROM"]),
                        DATE_TO = Convert.ToDateTime(dr3["DATE_TO"]),
                        TIME_FROM = Convert.ToDateTime(dr3["TIME_FROM"]),
                        TIME_TO = Convert.ToDateTime(dr3["TIME_TO"]),
                        PROMOTION_SCHEMA_ID = ADO.ToInt32(dr3["PROMOTION_SCHEMA_ID"]),
                        PROMOTION_SCHEMA = ADO.ToString(dr3["description"]),
                        PROMOTION_WEEKDAYS = ADO.ToString(dr3["PROMOTION_WEEKDAYS"]),
                        PROMOTION_LEVEL = ADO.ToInt32(dr3["PROMOTION_LEVEL"]),
                        PROMOTION_LEVEL_NAME = ADO.ToString(dr3["PROMOTION_LEVEL"]),
                        IS_INACTIVE = ADO.Toboolean(dr3["IS_INACTIVE"]),
                        PROMOTION_NAME = ADO.ToString(dr3["PROMOTION_NAME"]),
                        PROMOTION_GROUP_ID = ADO.ToInt32(dr3["PROMOTION_GROUP_ID"]),
                        IS_BUY = ADO.Toboolean(dr3["IS_BUY"]),
                        IS_GET = ADO.Toboolean(dr3["IS_GET"]),
                        IS_HAPPY_HOUR = ADO.Toboolean(dr3["IS_HAPPY_HOUR"]),

                        CAT_ID = ADO.ToInt32(dr3["CAT_ID"]),
                        DEPT_ID = ADO.ToInt32(dr3["DEPT_ID"]),
                        DEPT_NAME = ADO.ToString(dr3["DEPT_NAME"]),
                        CAT_NAME = ADO.ToString(dr3["CAT_NAME"]),
                        BARCODE = ADO.ToString(dr3["BARCODE"]),
                        ITEM_DESCRIPTION = ADO.ToString(dr3["ITEM_DESCRIPTION"]),
                        NARRATION = ADO.ToString(dr3["NARRATION"]),

                    });
                }



                item.worksheet_promotion_schema = itemstores;
                item.worksheet_item_store = itemsuppliers;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
    }
}
