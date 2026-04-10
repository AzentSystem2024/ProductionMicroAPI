using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class PromotionSchemaService:IPromotionSchemaService
    {
        public List<PromotionSchema> GetAllItemBrand()
        {
            List<PromotionSchema> schemaList = new List<PromotionSchema>();
            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_PROMOTION_SCHEMA";
            cmd.Parameters.AddWithValue("ACTION", 0);
            // cmd.Parameters.AddWithValue("UserID", intUserID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                schemaList.Add(new PromotionSchema
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    QTY_BUY = ADO.ToFloat(dr["QTY_BUY"]),
                    QTY_GET = ADO.ToFloat(dr["QTY_GET"]),
                    DISC_PERCENT = ADO.ToFloat(dr["DISC_PERCENT"]),
                    IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"]),
                    SCHEMA_TYPE_ID = ADO.ToInt32(dr["SCHEMA_TYPE"]),
                    SCHEMA_TYPE = ADO.ToString(dr["SCHEMA_TYPE_NAME"]),
                    SCHEMA_ON_REGULAR_PRICE = ADO.Toboolean(dr["SCHEMA_ON_REGULAR_PRICE"]),
                    SCHEMA_ON_QUANTITY_MULTIPLE = ADO.Toboolean(dr["SCHEMA_ON_QUANTITY_MULTIPLE"])
                });
            }
            connection.Close();
            return schemaList;
        }

        public Int32 Insert(PromotionSchema promotionSchema)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("PROMOTION_ID", typeof(Int32));
                tbl.Columns.Add("QTY_BUY", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("DESCRIPTION", typeof(string));

                foreach (PromotionSchemaEntry ur in promotionSchema.promotionschema_entry)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["PROMOTION_ID"] = ur.PROMOTION_ID;
                    dRow["QTY_BUY"] = ur.QTY_BUY;
                    dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                    dRow["DESCRIPTION"] = ur.DESCRIPTION;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PROMOTION_SCHEMA";

                cmd.Parameters.AddWithValue("ACTION", 1);

                cmd.Parameters.AddWithValue("DESCRIPTION", promotionSchema.DESCRIPTION);
                cmd.Parameters.AddWithValue("QTY_BUY", promotionSchema.QTY_BUY);
                cmd.Parameters.AddWithValue("QTY_GET", promotionSchema.QTY_GET);
                cmd.Parameters.AddWithValue("DISC_PERCENT", promotionSchema.DISC_PERCENT);
                cmd.Parameters.AddWithValue("IS_INACTIVE", promotionSchema.IS_INACTIVE);
                cmd.Parameters.AddWithValue("SCHEMA_TYPE", promotionSchema.SCHEMA_TYPE_ID);
                cmd.Parameters.AddWithValue("SCHEMA_ON_REGULAR_PRICE", promotionSchema.SCHEMA_ON_REGULAR_PRICE);
                cmd.Parameters.AddWithValue("SCHEMA_ON_QUANTITY_MULTIPLE", promotionSchema.SCHEMA_ON_QUANTITY_MULTIPLE);

                cmd.Parameters.AddWithValue("@UDT_TB_PROMOTION_SCHEMA_ENTRY", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return 0;

            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public Int32 Update(PromotionSchema promotionSchema)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("PROMOTION_ID", typeof(Int32));
                tbl.Columns.Add("QTY_BUY", typeof(float));
                tbl.Columns.Add("DISC_PERCENT", typeof(float));
                tbl.Columns.Add("DESCRIPTION", typeof(string));

                foreach (PromotionSchemaEntry ur in promotionSchema.promotionschema_entry)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["ID"] = ur.ID;
                    dRow["PROMOTION_ID"] = ur.PROMOTION_ID;
                    dRow["QTY_BUY"] = ur.QTY_BUY;
                    dRow["DISC_PERCENT"] = ur.DISC_PERCENT;
                    dRow["DESCRIPTION"] = ur.DESCRIPTION;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PROMOTION_SCHEMA";

                cmd.Parameters.AddWithValue("ACTION", 3);

                cmd.Parameters.AddWithValue("ID", promotionSchema.ID);
                cmd.Parameters.AddWithValue("DESCRIPTION", promotionSchema.DESCRIPTION);
                cmd.Parameters.AddWithValue("QTY_BUY", promotionSchema.QTY_BUY);
                cmd.Parameters.AddWithValue("QTY_GET", promotionSchema.QTY_GET);
                cmd.Parameters.AddWithValue("DISC_PERCENT", promotionSchema.DISC_PERCENT);
                cmd.Parameters.AddWithValue("IS_INACTIVE", promotionSchema.IS_INACTIVE);
                cmd.Parameters.AddWithValue("SCHEMA_TYPE", promotionSchema.SCHEMA_TYPE_ID);
                cmd.Parameters.AddWithValue("SCHEMA_ON_REGULAR_PRICE", promotionSchema.SCHEMA_ON_REGULAR_PRICE);
                cmd.Parameters.AddWithValue("SCHEMA_ON_QUANTITY_MULTIPLE", promotionSchema.SCHEMA_ON_QUANTITY_MULTIPLE);

                cmd.Parameters.AddWithValue("@UDT_TB_PROMOTION_SCHEMA_ENTRY", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return 0;

            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public PromotionSchema GetSchema(int id)
        {
            PromotionSchema schema = new PromotionSchema();
            List<PromotionSchemaEntry> schemaEntries = new List<PromotionSchemaEntry>();

            try
            {
                string strSQL = "SELECT TB_PROMOTION_SCHEMA.*, TB_PROMOTION_SCHEMA_TYPES.DESCRIPTION as DESCRIPTION_TYPE " +
                    "FROM TB_PROMOTION_SCHEMA " +
                    "LEFT JOIN TB_PROMOTION_SCHEMA_TYPES ON TB_PROMOTION_SCHEMA.SCHEMA_TYPE = TB_PROMOTION_SCHEMA_TYPES.ID " +
                    "WHERE TB_PROMOTION_SCHEMA.IS_DELETED = 0 and TB_PROMOTION_SCHEMA.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "PromotionSchema");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    schema = new PromotionSchema
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                        QTY_BUY = ADO.ToFloat(dr["QTY_BUY"]),
                        QTY_GET = ADO.ToFloat(dr["QTY_GET"]),
                        DISC_PERCENT = ADO.ToFloat(dr["DISC_PERCENT"]),
                        IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"]),
                        SCHEMA_TYPE_ID = ADO.ToInt32(dr["SCHEMA_TYPE"]),
                        SCHEMA_TYPE = ADO.ToString(dr["DESCRIPTION_TYPE"]),
                        SCHEMA_ON_REGULAR_PRICE = ADO.Toboolean(dr["SCHEMA_ON_REGULAR_PRICE"]),
                        SCHEMA_ON_QUANTITY_MULTIPLE = ADO.Toboolean(dr["SCHEMA_ON_QUANTITY_MULTIPLE"])

                    };
                }

                strSQL = "SELECT * from TB_PROMOTION_SCHEMA_ENTRY WHERE TB_PROMOTION_SCHEMA_ENTRY.PROMOTION_ID = " + id;

                DataTable tblItemSuppliers1 = ADO.GetDataTable(strSQL, "SchemaEntry");

                foreach (DataRow dr3 in tblItemSuppliers1.Rows)
                {
                    schemaEntries.Add(new PromotionSchemaEntry
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        PROMOTION_ID = ADO.ToInt32(dr3["PROMOTION_ID"]),
                        QTY_BUY = ADO.ToFloat(dr3["QTY_BUY"]),
                        DISC_PERCENT = ADO.ToFloat(dr3["DISC_PERCENT"]),
                        DESCRIPTION = ADO.ToString(dr3["DESCRIPTION"]),

                    });
                }

                schema.promotionschema_entry = schemaEntries;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return schema;
        }

        public bool DeleteSchema(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PROMOTION_SCHEMA";
                cmd.Parameters.AddWithValue("ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", id);

                // connection.Open();

                object result = cmd.ExecuteScalar();
                connection.Close();

                if (result != null && result.ToString() == "1")
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(result?.ToString() ?? "Cannot delete: Promotion is in use.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
