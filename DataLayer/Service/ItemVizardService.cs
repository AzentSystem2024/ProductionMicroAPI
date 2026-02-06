using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ItemVizardService:IItemVizardService
    {
        public List<ItemVizard> GetAllItems(ItemVizardInput vizardInput)
        {
            List<ItemVizard> itemsList = new List<ItemVizard>();

            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_ITEM_VIZARD_LIST"
            };

            cmd.Parameters.AddWithValue("STORE_ID", vizardInput.STORE_ID);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                DataTable itemsTable = dataSet.Tables[0];

                itemsList = itemsTable.AsEnumerable().Select(dr => new ItemVizard
                {
                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
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
                    ITEM_SALE_PRICE = Convert.IsDBNull(dr["ITEM_SALE_PRICE"]) ? (float?)null : Convert.ToSingle(dr["ITEM_SALE_PRICE"]),
                    ITEM_COST = Convert.IsDBNull(dr["ITEM_COST"]) ? (float?)null : Convert.ToSingle(dr["ITEM_COST"]),
                    PROFIT_MARGIN = Convert.IsDBNull(dr["PROFIT_MARGIN"]) ? (float?)null : Convert.ToSingle(dr["PROFIT_MARGIN"]),
                    QTY_STOCK = Convert.IsDBNull(dr["QTY_STOCK"]) ? (float?)null : Convert.ToSingle(dr["QTY_STOCK"]),
                    QTY_COMMITTED = Convert.IsDBNull(dr["QTY_COMMITTED"]) ? (float?)null : Convert.ToSingle(dr["QTY_COMMITTED"]),
                    CREATED_DATE = Convert.IsDBNull(dr["CREATED_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["CREATED_DATE"]),
                    LAST_PO_DATE = Convert.IsDBNull(dr["LAST_PO_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_PO_DATE"]),
                    LAST_GRN_DATE = Convert.IsDBNull(dr["LAST_GRN_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_GRN_DATE"]),
                    LAST_SALE_DATE = Convert.IsDBNull(dr["LAST_SALE_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_SALE_DATE"]),
                    RESTOCK_LEVEL = Convert.IsDBNull(dr["RESTOCK_LEVEL"]) ? (float?)null : Convert.ToSingle(dr["RESTOCK_LEVEL"]),
                    REORDER_POINT = Convert.IsDBNull(dr["REORDER_POINT"]) ? (float?)null : Convert.ToSingle(dr["REORDER_POINT"]),
                    PARENT_ITEM_ID = dr["PARENT_ITEM_ID"] != DBNull.Value ? Convert.ToInt32(dr["PARENT_ITEM_ID"]) : 0,
                    CHILD_QTY = Convert.IsDBNull(dr["CHILD_QTY"]) ? (float?)null : Convert.ToSingle(dr["CHILD_QTY"]),
                    ORIGIN_COUNTRY = dr["ORIGIN_COUNTRY"] != DBNull.Value ? Convert.ToInt32(dr["ORIGIN_COUNTRY"]) : 0, // or some default value
                    COUNTRY_NAME = dr["COUNTRY_NAME"] != DBNull.Value ? Convert.ToString(dr["COUNTRY_NAME"]) : string.Empty,
                    SHELF_LIFE = dr["SHELF_LIFE"] != DBNull.Value ? Convert.ToInt32(dr["SHELF_LIFE"]) : 0, // or some default value
                    BIN_LOCATION = dr["BIN_LOCATION"] != DBNull.Value ? Convert.ToString(dr["BIN_LOCATION"]) : string.Empty,
                    NOTES = dr["NOTES"] != DBNull.Value ? Convert.ToString(dr["NOTES"]) : string.Empty,
                    ITEM_IS_INACTIVE = Convert.IsDBNull(dr["ITEM_IS_INACTIVE"]) ? (bool?)null : Convert.ToBoolean(dr["ITEM_IS_INACTIVE"]),
                    ITEM_IS_PRICE_REQUIRED = Convert.IsDBNull(dr["ITEM_IS_PRICE_REQUIRED"]) ? (bool?)null : Convert.ToBoolean(dr["ITEM_IS_PRICE_REQUIRED"]),
                    ITEM_IS_NOT_DISCOUNTABLE = Convert.IsDBNull(dr["ITEM_IS_NOT_DISCOUNTABLE"]) ? (bool?)null : Convert.ToBoolean(dr["ITEM_IS_NOT_DISCOUNTABLE"]),
                    ITEM_IS_NOT_PURCH_ITEM = Convert.IsDBNull(dr["ITEM_IS_NOT_PURCH_ITEM"]) ? (bool?)null : Convert.ToBoolean(dr["ITEM_IS_NOT_PURCH_ITEM"]),
                    ITEM_IS_NOT_SALE_ITEM = Convert.IsDBNull(dr["ITEM_IS_NOT_SALE_ITEM"]) ? (bool?)null : Convert.ToBoolean(dr["ITEM_IS_NOT_SALE_ITEM"]),
                    ITEM_IS_NOT_SALE_RETURN = Convert.IsDBNull(dr["ITEM_IS_NOT_SALE_RETURN"]) ? (bool?)null : Convert.ToBoolean(dr["ITEM_IS_NOT_SALE_RETURN"]),
                    IS_BLOCKED = Convert.IsDBNull(dr["IS_BLOCKED"]) ? (bool?)null : Convert.ToBoolean(dr["IS_BLOCKED"]),
                    IMAGE_NAME = dr["IMAGE_NAME"] != DBNull.Value ? Convert.ToString(dr["IMAGE_NAME"]) : string.Empty,
                    ITEM_SL = dr["ITEM_SL"] != DBNull.Value ? Convert.ToInt32(dr["ITEM_SL"]) : 0,
                    ITEM_SALE_PRICE1 = Convert.IsDBNull(dr["ITEM_SALE_PRICE1"]) ? (float?)null : Convert.ToSingle(dr["ITEM_SALE_PRICE1"]),
                    ITEM_SALE_PRICE2 = Convert.IsDBNull(dr["ITEM_SALE_PRICE2"]) ? (float?)null : Convert.ToSingle(dr["ITEM_SALE_PRICE2"]),
                    ITEM_SALE_PRICE3 = Convert.IsDBNull(dr["ITEM_SALE_PRICE3"]) ? (float?)null : Convert.ToSingle(dr["ITEM_SALE_PRICE3"]),
                    ITEM_SALE_PRICE4 = Convert.IsDBNull(dr["ITEM_SALE_PRICE4"]) ? (float?)null : Convert.ToSingle(dr["ITEM_SALE_PRICE4"]),
                    ITEM_SALE_PRICE5 = Convert.IsDBNull(dr["ITEM_SALE_PRICE5"]) ? (float?)null : Convert.ToSingle(dr["ITEM_SALE_PRICE5"]),
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
                    POS_DESCRIPTION = dr["POS_DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["POS_DESCRIPTION"]) : string.Empty,

                    //STORE_ID = dr["STORE_ID"] != DBNull.Value ? Convert.ToInt32(dr["STORE_ID"]) : 0,
                    STORE_SALE_PRICE = Convert.IsDBNull(dr["STORE_SALE_PRICE"]) ? (float?)null : Convert.ToSingle(dr["STORE_SALE_PRICE"]),
                    STORE_SALE_PRICE1 = Convert.IsDBNull(dr["STORE_SALE_PRICE1"]) ? (float?)null : Convert.ToSingle(dr["STORE_SALE_PRICE1"]),
                    STORE_SALE_PRICE2 = Convert.IsDBNull(dr["STORE_SALE_PRICE2"]) ? (float?)null : Convert.ToSingle(dr["STORE_SALE_PRICE2"]),
                    STORE_SALE_PRICE3 = Convert.IsDBNull(dr["STORE_SALE_PRICE3"]) ? (float?)null : Convert.ToSingle(dr["STORE_SALE_PRICE3"]),
                    STORE_SALE_PRICE4 = Convert.IsDBNull(dr["STORE_SALE_PRICE4"]) ? (float?)null : Convert.ToSingle(dr["STORE_SALE_PRICE4"]),
                    STORE_SALE_PRICE5 = Convert.IsDBNull(dr["STORE_SALE_PRICE5"]) ? (float?)null : Convert.ToSingle(dr["STORE_SALE_PRICE5"]),

                    STORE_IS_INACTIVE = Convert.IsDBNull(dr["STORE_IS_INACTIVE"]) ? (bool?)null : Convert.ToBoolean(dr["STORE_IS_INACTIVE"]),
                    STORE_IS_PRICE_REQUIRED = Convert.IsDBNull(dr["STORE_IS_PRICE_REQUIRED"]) ? (bool?)null : Convert.ToBoolean(dr["STORE_IS_PRICE_REQUIRED"]),
                    STORE_IS_NOT_DISCOUNTABLE = Convert.IsDBNull(dr["STORE_IS_NOT_DISCOUNTABLE"]) ? (bool?)null : Convert.ToBoolean(dr["STORE_IS_NOT_DISCOUNTABLE"]),

                    STORE_IS_NOT_SALE_ITEM = Convert.IsDBNull(dr["STORE_IS_NOT_SALE_ITEM"]) ? (bool?)null : Convert.ToBoolean(dr["STORE_IS_NOT_SALE_ITEM"]),
                    STORE_IS_NOT_SALE_RETURN = Convert.IsDBNull(dr["STORE_IS_NOT_SALE_RETURN"]) ? (bool?)null : Convert.ToBoolean(dr["STORE_IS_NOT_SALE_RETURN"]),
                }).ToList();
            }

            return itemsList;
        }

        public Int32 Insert(ItemVizardStore vizardStore)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ITEM_VIZARD";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("ID", vizardStore.ID);
                cmd.Parameters.AddWithValue("ITEM_ID", vizardStore.ITEM_ID);
                cmd.Parameters.AddWithValue("STORE_ID", vizardStore.STORE_ID);
                cmd.Parameters.AddWithValue("SALE_PRICE", vizardStore.STORE_SALE_PRICE);
                cmd.Parameters.AddWithValue("SALE_PRICE1", vizardStore.STORE_SALE_PRICE1);
                cmd.Parameters.AddWithValue("SALE_PRICE2", vizardStore.STORE_SALE_PRICE2);
                cmd.Parameters.AddWithValue("SALE_PRICE3", vizardStore.STORE_SALE_PRICE3);
                cmd.Parameters.AddWithValue("SALE_PRICE4", vizardStore.STORE_SALE_PRICE4);
                cmd.Parameters.AddWithValue("SALE_PRICE5", vizardStore.STORE_SALE_PRICE5);
                cmd.Parameters.AddWithValue("COST", vizardStore.COST);
                cmd.Parameters.AddWithValue("IS_INACTIVE", vizardStore.STORE_IS_INACTIVE);
                cmd.Parameters.AddWithValue("IS_NOT_SALE_ITEM", vizardStore.STORE_IS_NOT_SALE_ITEM);
                cmd.Parameters.AddWithValue("IS_NOT_SALE_RETURN", vizardStore.STORE_IS_NOT_SALE_RETURN);
                cmd.Parameters.AddWithValue("IS_PRICE_REQUIRED", vizardStore.STORE_IS_PRICE_REQUIRED);
                cmd.Parameters.AddWithValue("IS_NOT_DISCOUNTABLE", vizardStore.STORE_IS_NOT_DISCOUNTABLE);

                Int32 VizardID = Convert.ToInt32(cmd.ExecuteScalar());
                return VizardID;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ItemPriceWizard> GetAllItemPriceWizard(ItemPriceWizardInput pricewizardinput)
        {
            List<ItemPriceWizard> wizardList = new List<ItemPriceWizard>();
            SqlConnection connection = ADO.GetConnection();

            // Create a new SQL command
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;  // Set the connection
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_ITEMPRICE_VIZARD_LIST";
            cmd.Parameters.AddWithValue("ITEM_TYPE_ID", pricewizardinput.ITEM_TYPE_ID);

            // Create and execute a data adapter to fill the dataset
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);  // Fill the dataset

            // Process Items
            if (dataSet.Tables.Count > 0)
            {
                DataTable itemsTable = dataSet.Tables[0];
                foreach (DataRow dr in itemsTable.Rows)
                {
                    wizardList.Add(new ItemPriceWizard
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        BARCODE = ADO.ToString(dr["BARCODE"]),
                        DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                        CAT_NAME = ADO.ToString(dr["CAT_NAME"]),
                        BRAND_NAME = ADO.ToString(dr["BRAND_NAME"]),
                        DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),
                        STORE_ID = ADO.ToString(dr["STORE_ID"]),
                        SALE_PRICE = ADO.ToFloat(dr["SALE_PRICE"]),
                        SALE_PRICE1 = ADO.ToFloat(dr["SALE_PRICE1"]),
                        SALE_PRICE2 = ADO.ToFloat(dr["SALE_PRICE2"]),
                        SALE_PRICE3 = ADO.ToFloat(dr["SALE_PRICE3"]),
                        SALE_PRICE4 = ADO.ToFloat(dr["SALE_PRICE4"]),
                        SALE_PRICE5 = ADO.ToFloat(dr["SALE_PRICE5"]),
                        COST = ADO.ToFloat(dr["COST"]),
                        ITEM_CODE = ADO.ToString(dr["ITEM_CODE"]),
                        ITEM_TYPE = ADO.ToString(dr["TYPE_NAME"])
                    });
                }
            }



            return wizardList;
        }
    }
}
