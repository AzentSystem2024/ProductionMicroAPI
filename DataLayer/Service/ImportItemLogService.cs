using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ImportItemLogService:IImportItemLogService
    {
        public List<ImportItemLog> GetAllItemLog()
        {
            List<ImportItemLog> LogList = new List<ImportItemLog>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_ITEM_LOG";
            cmd.Parameters.AddWithValue("ACTION", 0);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            foreach (DataRow dr in tbl.Rows)
            {
                LogList.Add(new ImportItemLog
                {
                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                    BATCH_NO = dr["BATCH_NO"] != DBNull.Value ? Convert.ToInt32(dr["BATCH_NO"]) : 0,
                    IMPORT_DATE = dr["IMPORT_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["IMPORT_DATE"]) : DateTime.MinValue,
                    TEMPLATE_ID = dr["TEMPLATE_ID"] != DBNull.Value ? Convert.ToInt32(dr["TEMPLATE_ID"]) : 0,
                    STORE_ID = dr["STORE_ID"] != DBNull.Value ? Convert.ToString(dr["STORE_ID"]) : string.Empty,
                    USER_ID = dr["USER_ID"] != DBNull.Value ? Convert.ToInt32(dr["USER_ID"]) : 0,
                    REMARKS = dr["REMARKS"] != DBNull.Value ? Convert.ToString(dr["REMARKS"]) : string.Empty,
                    USER_NAME = dr["USER_NAME"] != DBNull.Value ? Convert.ToString(dr["USER_NAME"]) : string.Empty,
                    STORE_NAME = dr["STORE_NAME"] != DBNull.Value ? Convert.ToString(dr["STORE_NAME"]) : string.Empty,
                    TEMPLATE_NAME = dr["TEMPLATE_NAME"] != DBNull.Value ? Convert.ToString(dr["TEMPLATE_NAME"]) : string.Empty

                });
            }
            connection.Close();

            return LogList;
        }


        public List<InsertImportItemLogEntry> GetAllItemLogEntry(ImportItemLog itemLog)
        {
            List<InsertImportItemLogEntry> LogEntryList = new List<InsertImportItemLogEntry>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_IMPORT_LOG_ENTRY";

            cmd.Parameters.AddWithValue("ID", itemLog.ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                LogEntryList.Add(new InsertImportItemLogEntry
                {
                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                    ITEM_CODE = dr["ITEM_CODE"] != DBNull.Value ? Convert.ToString(dr["ITEM_CODE"]) : string.Empty,
                    BARCODE = dr["BARCODE"] != DBNull.Value ? Convert.ToString(dr["BARCODE"]) : string.Empty,
                    DESCRIPTION = dr["DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["DESCRIPTION"]) : string.Empty,
                    POS_DESCRIPTION = dr["POS_DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["POS_DESCRIPTION"]) : string.Empty,
                    ARABIC_DESCRIPTION = dr["ARABIC_DESCRIPTION"] != DBNull.Value ? Convert.ToString(dr["ARABIC_DESCRIPTION"]) : string.Empty,
                    DEPT_CODE = dr["DEPT_CODE"] != DBNull.Value ? Convert.ToString(dr["DEPT_CODE"]) : string.Empty,
                    DEPT_NAME = dr["DEPT_NAME"] != DBNull.Value ? Convert.ToString(dr["DEPT_NAME"]) : string.Empty,
                    CAT_CODE = dr["CAT_CODE"] != DBNull.Value ? Convert.ToString(dr["CAT_CODE"]) : string.Empty,
                    CAT_NAME = dr["CAT_NAME"] != DBNull.Value ? Convert.ToString(dr["CAT_NAME"]) : string.Empty,
                    SUBCAT_CODE = dr["SUBCAT_CODE"] != DBNull.Value ? Convert.ToString(dr["SUBCAT_CODE"]) : string.Empty,
                    SUBCAT_NAME = dr["SUBCAT_NAME"] != DBNull.Value ? Convert.ToString(dr["SUBCAT_NAME"]) : string.Empty,
                    BRAND_CODE = dr["BRAND_CODE"] != DBNull.Value ? Convert.ToString(dr["BRAND_CODE"]) : string.Empty,
                    BRAND_NAME = dr["BRAND_NAME"] != DBNull.Value ? Convert.ToString(dr["BRAND_NAME"]) : string.Empty,
                    SUPP_CODE = dr["SUPP_CODE"] != DBNull.Value ? Convert.ToString(dr["SUPP_CODE"]) : string.Empty,
                    SUPP_NAME = dr["SUPP_NAME"] != DBNull.Value ? Convert.ToString(dr["SUPP_NAME"]) : string.Empty,
                    SUPP_PRICE = dr["SUPP_PRICE"] != DBNull.Value ? Convert.ToSingle(dr["SUPP_PRICE"]) : 0f,
                    COST = dr["COST"] != DBNull.Value ? Convert.ToSingle(dr["COST"]) : 0f,
                    VAT_PERCENT = dr["VAT_PERCENT"] != DBNull.Value ? Convert.ToSingle(dr["VAT_PERCENT"]) : 0f,
                    PRICE = dr["PRICE"] != DBNull.Value ? Convert.ToSingle(dr["PRICE"]) : 0f,
                    ITEM_TYPE = dr["ITEM_TYPE"] != DBNull.Value ? Convert.ToString(dr["ITEM_TYPE"]) : string.Empty,
                    IS_DISCOUNTABLE = dr["IS_DISCOUNTABLE"] != DBNull.Value ? Convert.ToString(dr["IS_DISCOUNTABLE"]) : string.Empty,
                    COSTING_METHOD = dr["COSTING_METHOD"] != DBNull.Value ? Convert.ToString(dr["COSTING_METHOD"]) : string.Empty,
                    IS_SELLABLE = dr["IS_SELLABLE"] != DBNull.Value ? Convert.ToString(dr["IS_SELLABLE"]) : string.Empty,
                    SHELF_LIFE = dr["SHELF_LIFE"] != DBNull.Value ? Convert.ToInt32(dr["SHELF_LIFE"]) : 0
                });
            }
            connection.Close();

            return LogEntryList;
        }

        public bool Insert(ImportItemLog importItemLog)
        {
            try
            {
                SqlConnection objCon = ADO.GetConnection();
                SqlTransaction objTrans = objCon.BeginTransaction();

                try
                {
                    // Get the next Batch No
                    string strSQL = "SELECT COALESCE(MAX(BATCH_NO), 0) + 1 FROM TB_ITEM_IMPORT_LOG";
                    object BatchNo = ADO.ExecuteScalar(strSQL, objCon, objTrans);


                    // Insert into TB_ITEM_IMPORT_LOG
                    strSQL = "INSERT INTO TB_ITEM_IMPORT_LOG (BATCH_NO, IMPORT_DATE, TEMPLATE_ID, STORE_ID, REMARKS, USER_ID)" +
                                "VALUES(" + BatchNo.ToString() + ", GETUTCDATE()," + importItemLog.TEMPLATE_ID + "," +
                                ADO.SQLString(importItemLog.STORE_ID) + "," + ADO.SQLString(importItemLog.REMARKS) + "," + importItemLog.USER_ID + ") SELECT SCOPE_IDENTITY();";

                    Int32 LogID = Convert.ToInt32(ADO.ExecuteScalar(strSQL, objCon, objTrans));

                    SqlCommand objCmd = new SqlCommand();
                    objCmd.Connection = objCon;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Transaction = objTrans;
                    objCmd.CommandText = "SP_IMPORT_ITEM";

                    // Adding parameters for SP_IMPORT_ITEM stored procedure
                    objCmd.Parameters.Add("@LOG_ID", SqlDbType.Int);
                    objCmd.Parameters.Add("@STORE_ID", SqlDbType.NVarChar);
                    objCmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@BARCODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@POS_DESCRIPTION", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar);
                    objCmd.Parameters.Add("@ARABIC_DESCRIPTION", SqlDbType.NVarChar);
                    objCmd.Parameters.Add("@DEPT_CODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@DEPT_NAME", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@CAT_CODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@CAT_NAME", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@SUBCAT_CODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@SUBCAT_NAME", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@BRAND_CODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@BRAND_NAME", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@SUPP_CODE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@SUPP_NAME", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@SUPP_PRICE", SqlDbType.Float);
                    objCmd.Parameters.Add("@COST", SqlDbType.Float);
                    objCmd.Parameters.Add("@VAT_PERCENT", SqlDbType.Float);
                    objCmd.Parameters.Add("@PRICE", SqlDbType.Float);
                    objCmd.Parameters.Add("@ITEM_TYPE", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@IS_NOT_DISCOUNTABLE", SqlDbType.Int);
                    objCmd.Parameters.Add("@COSTING_METHOD", SqlDbType.VarChar);
                    objCmd.Parameters.Add("@IS_NOT_SALE_ITEM", SqlDbType.Int);
                    objCmd.Parameters.Add("@SHELF_LIFE", SqlDbType.Int);

                    foreach (InsertImportItemLogEntry ur in importItemLog.importitem_logentry)
                    {
                        objCmd.Parameters["@LOG_ID"].Value = LogID;
                        objCmd.Parameters["@STORE_ID"].Value = importItemLog.STORE_ID;
                        objCmd.Parameters["@ITEM_CODE"].Value = ur.ITEM_CODE;
                        objCmd.Parameters["@BARCODE"].Value = ur.BARCODE;
                        objCmd.Parameters["@DESCRIPTION"].Value = ur.DESCRIPTION;
                        objCmd.Parameters["@POS_DESCRIPTION"].Value = ur.POS_DESCRIPTION;
                        objCmd.Parameters["@ARABIC_DESCRIPTION"].Value = ur.ARABIC_DESCRIPTION;
                        objCmd.Parameters["@DEPT_CODE"].Value = ur.DEPT_CODE;
                        objCmd.Parameters["@DEPT_NAME"].Value = ur.DEPT_NAME;
                        objCmd.Parameters["@CAT_CODE"].Value = ur.CAT_CODE;
                        objCmd.Parameters["@CAT_NAME"].Value = ur.CAT_NAME;
                        objCmd.Parameters["@SUBCAT_CODE"].Value = ur.SUBCAT_CODE;
                        objCmd.Parameters["@SUBCAT_NAME"].Value = ur.SUBCAT_NAME;
                        objCmd.Parameters["@BRAND_CODE"].Value = ur.BRAND_CODE;
                        objCmd.Parameters["@BRAND_NAME"].Value = ur.BRAND_NAME;
                        objCmd.Parameters["@SUPP_CODE"].Value = ur.SUPP_CODE;
                        objCmd.Parameters["@SUPP_NAME"].Value = ur.SUPP_NAME;
                        objCmd.Parameters["@SUPP_PRICE"].Value = ur.SUPP_PRICE;
                        objCmd.Parameters["@COST"].Value = ur.COST;
                        objCmd.Parameters["@VAT_PERCENT"].Value = ur.VAT_PERCENT;
                        objCmd.Parameters["@PRICE"].Value = ur.PRICE;
                        objCmd.Parameters["@ITEM_TYPE"].Value = ur.ITEM_TYPE;
                        objCmd.Parameters["@IS_NOT_DISCOUNTABLE"].Value = ur.IS_DISCOUNTABLE.ToUpper() == "YES" ? 0 : 1;
                        objCmd.Parameters["@COSTING_METHOD"].Value = ur.COSTING_METHOD;
                        objCmd.Parameters["@IS_NOT_SALE_ITEM"].Value = ur.IS_SELLABLE.ToUpper() == "YES" ? 0 : 1;
                        objCmd.Parameters["@SHELF_LIFE"].Value = ADO.ToInt32(ur.SHELF_LIFE);
                        objCmd.ExecuteNonQuery();

                        // Insert into TB_ITEM_IMPORT_LOG_ENTRY
                        strSQL = "INSERT INTO TB_ITEM_IMPORT_LOG_ENTRY(LOG_ID, ITEM_CODE, BARCODE, DESCRIPTION, POS_DESCRIPTION, " +
                                "ARABIC_DESCRIPTION, DEPT_CODE, DEPT_NAME, CAT_CODE, CAT_NAME, SUBCAT_CODE, SUBCAT_NAME, " +
                                "BRAND_CODE, BRAND_NAME, SUPP_CODE, SUPP_NAME, SUPP_PRICE, COST, VAT_PERCENT, PRICE, ITEM_TYPE, " +
                                "IS_DISCOUNTABLE, COSTING_METHOD, IS_SELLABLE, SHELF_LIFE) VALUES " +
                                "(" + LogID + "," + ADO.SQLString(ur.ITEM_CODE) + "," + ADO.SQLString(ur.BARCODE) + "," + ADO.SQLString(ur.DESCRIPTION) + "," +
                                ADO.SQLString(ur.POS_DESCRIPTION) + "," + ADO.SQLString(ur.ARABIC_DESCRIPTION) + "," + ADO.SQLString(ur.DEPT_CODE) + "," +
                                ADO.SQLString(ur.DEPT_NAME) + "," + ADO.SQLString(ur.CAT_CODE) + "," + ADO.SQLString(ur.CAT_NAME) + "," +
                                ADO.SQLString(ur.SUBCAT_CODE) + "," + ADO.SQLString(ur.SUBCAT_NAME) + "," + ADO.SQLString(ur.BRAND_CODE) + "," +
                                ADO.SQLString(ur.BRAND_NAME) + "," + ADO.SQLString(ur.SUPP_CODE) + "," + ADO.SQLString(ur.SUPP_NAME) + "," +
                                ADO.SQLString(ur.SUPP_PRICE) + "," + ADO.SQLString(ur.COST) + "," + ADO.SQLString(ur.VAT_PERCENT) + "," +
                                ADO.SQLString(ur.PRICE) + "," + ADO.SQLString(ur.ITEM_TYPE) + "," + ADO.SQLString(ur.IS_DISCOUNTABLE) + "," +
                                ADO.SQLString(ur.COSTING_METHOD) + "," + ADO.SQLString(ur.IS_SELLABLE) + "," + ADO.SQLString(ur.SHELF_LIFE) + ")";

                        ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                    }


                    objTrans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    objCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
