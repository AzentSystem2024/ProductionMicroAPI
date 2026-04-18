using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ImportTemplateService:IImportTemplateService
    {
        public List<ImportTemplate> GetAllimport(Int32 intUserID)
        {
            List<ImportTemplate> importTemplates = new List<ImportTemplate>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_IMPORT_TEMPLATE";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    importTemplates.Add(new ImportTemplate
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        TEMPLATE_NAME = Convert.ToString(dr["TEMPLATE_NAME"]),
                        REMARKS = Convert.ToString(dr["REMARKS"]),

                    });
                }
                connection.Close();
            }
            return importTemplates;
        }

        public Int32 Insert(ImportTemplate importTemplate, Int32 userID)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COLUMN_ID", typeof(Int32));

                if (importTemplate.import_entry != null && importTemplate.import_entry.Any())
                {
                    foreach (ImportTemplateEntry ur in importTemplate.import_entry)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID > 0 ? ur.ID : (object)DBNull.Value;
                        dRow["COLUMN_ID"] = ur.COLUMN_ID;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_IMPORT_TEMPLATE";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("ID", importTemplate.ID);
                    cmd.Parameters.AddWithValue("TEMPLATE_NAME", importTemplate.TEMPLATE_NAME);
                    cmd.Parameters.AddWithValue("REMARKS", importTemplate.REMARKS);
                    cmd.Parameters.AddWithValue("UserID", importTemplate.UserID);

                    cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_TEMPLATE_ENTRYS", tbl);

                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());

                    //cmd.ExecuteNonQuery();

                    objtrans.Commit();

                    connection.Close();
                    return UserID;
                    //return 0;
                }
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public Int32 Update(ImportTemplate importTemplate, Int32 userID)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COLUMN_ID", typeof(Int32));

                if (importTemplate.import_entry != null && importTemplate.import_entry.Any())
                {
                    foreach (ImportTemplateEntry ur in importTemplate.import_entry)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID > 0 ? ur.ID : (object)DBNull.Value;
                        dRow["COLUMN_ID"] = ur.COLUMN_ID;


                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_IMPORT_TEMPLATE";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", importTemplate.ID);
                    cmd.Parameters.AddWithValue("TEMPLATE_NAME", importTemplate.TEMPLATE_NAME);
                    cmd.Parameters.AddWithValue("REMARKS", importTemplate.REMARKS);
                    cmd.Parameters.AddWithValue("UserID", importTemplate.UserID);

                    cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_TEMPLATE_ENTRYS", tbl);

                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());

                    //cmd.ExecuteNonQuery();

                    objtrans.Commit();

                    connection.Close();
                    return UserID;
                    //return 0;


                }
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public List<ImportTemplate> GetItems(int id)
        {
            List<ImportTemplate> importTemplates = new List<ImportTemplate>();
            List<ImportTemplateEntry> importEntries = new List<ImportTemplateEntry>();
            try
            {
                string strSQL = "SELECT ID, TEMPLATE_NAME, REMARKS,LAST_MODIFIED_USER_ID FROM TB_IMPORT_TEMPLATE" +
                                " WHERE TB_IMPORT_TEMPLATE.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ImportTemplate");
                //if (tbl.Rows.Count > 0)
                //{
                DataRow dr = tbl.Rows[0];
                ImportTemplate importTemplate = new ImportTemplate
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    TEMPLATE_NAME = Convert.ToString(dr["TEMPLATE_NAME"]),
                    REMARKS = Convert.ToString(dr["REMARKS"]),
                    UserID = Convert.ToInt32(dr["LAST_MODIFIED_USER_ID"])
                };
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = ADO.GetConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_IMPORT_TEMPLATE_COLUMNS";

                cmd.Parameters.AddWithValue("@TEMPLATE_ID", id);
                DataTable tblimporttemplate = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tblimporttemplate);

                // Process the results
                foreach (DataRow dr1 in tblimporttemplate.Rows)
                {
                    importEntries.Add(new ImportTemplateEntry
                    {
                        COLUMN_ID = dr1["COLUMN_ID"] != DBNull.Value ? Convert.ToInt32(dr1["COLUMN_ID"]) : 0,
                        COLUMN_NAME = dr1["COLUMN_NAME"] != DBNull.Value ? Convert.ToString(dr1["COLUMN_NAME"]) : null,
                        COLUMN_TITLE = dr1["COLUMN_TITLE"] != DBNull.Value ? Convert.ToString(dr1["COLUMN_TITLE"]) : null,
                        MAX_LENGTH = dr1["MAX_LENGTH"] != DBNull.Value ? Convert.ToInt32(dr1["MAX_LENGTH"]) : 0,
                        IS_MANDATORY = Convert.ToBoolean(dr1["IS_MANDATORY"]),
                        SELECTED = Convert.ToBoolean(dr1["SELECTED"]),
                        // LIST = new List<LIST>()
                        LIST = ""
                    });
                }
                importTemplate.import_entry = importEntries;
                importTemplates.Add(importTemplate);
                //}
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
            }

            return importTemplates;
        }




        //public List<ImportTemplate> GetItems(int id)
        //{
        //    List<ImportTemplate> importTemplates = new List<ImportTemplate>();
        //    List<ImportTemplateEntry> importEntries = new List<ImportTemplateEntry>();
        //    try
        //    {
        //        string strSQL = "SELECT ID, TEMPLATE_NAME, REMARKS FROM TB_IMPORT_TEMPLATE" +
        //                        " WHERE TB_IMPORT_TEMPLATE.ID = " + id;

        //        DataTable tbl = ADO.GetDataTable(strSQL, "ImportTemplate");

        //        //if (tbl.Rows.Count > 0)
        //        //{
        //        DataRow dr = tbl.Rows[0];

        //        ImportTemplate importTemplate = new ImportTemplate
        //        {
        //            ID = Convert.ToInt32(dr["ID"]),
        //            TEMPLATE_NAME = Convert.ToString(dr["TEMPLATE_NAME"]),
        //            REMARKS = Convert.ToString(dr["REMARKS"])
        //        };

        //        //strSQL = "SELECT * FROM TB_IMPORT_TEMPLATE_ENTRY WHERE TEMPLATE_ID = " + id;

        //        strSQL = "SELECT ID,TEMPLATE_ID, COLUMN_NAME, COLUMN_TITLE, " +
        //                 "CASE WHEN TEMPLATE_ID = " + id + " THEN 1 ELSE 0 END AS selected " +
        //                  "FROM TB_IMPORT_TEMPLATE_ENTRY";


        //        DataTable tblDetail = ADO.GetDataTable(strSQL, "ImportTemplateEntry");

        //        foreach (DataRow drDetail in tblDetail.Rows)
        //        {
        //            importEntries.Add(new ImportTemplateEntry
        //            {
        //                ID = Convert.ToInt32(drDetail["ID"]),
        //                COLOUMN_ID = Convert.ToInt32(drDetail["COLOUMN_ID"]),

        //                SELECTED = Convert.ToBoolean(drDetail["selected"])
        //            });
        //        }

        //        importTemplate.import_entry = importEntries;
        //        importTemplates.Add(importTemplate);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception (e.g., log the error)
        //    }

        //    return importTemplates;
        //}


        public bool DeleteImportTemplate(int id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_IMPORT_TEMPLATE";
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
    }
}
