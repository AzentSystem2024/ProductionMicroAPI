using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ImportTemplateColoumnService:IImportTemplateColoumnService
    {
        public List<ImportTemplateColoumns> GetAllTemplateColoumns(Int32 intUserID)
        {
            List<ImportTemplateColoumns> templatecoloumnList = new List<ImportTemplateColoumns>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_IMPORT_TEMPLATE_COLUMNS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                //cmd.Parameters.AddWithValue("UserID", intUserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                foreach (DataRow dr in tbl.Rows)
                {
                    templatecoloumnList.Add(new ImportTemplateColoumns
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                        COLUMN_NAME = dr["COLUMN_NAME"] != DBNull.Value ? Convert.ToString(dr["COLUMN_NAME"]) : string.Empty,
                        COLUMN_TITLE = dr["COLUMN_TITLE"] != DBNull.Value ? Convert.ToString(dr["COLUMN_TITLE"]) : string.Empty,
                        IS_MANDATORY = dr["IS_MANDATORY"] != DBNull.Value ? Convert.ToBoolean(dr["IS_MANDATORY"]) : false

                    });
                }
                connection.Close();
            }
            return templatecoloumnList;
        }
    }
}
