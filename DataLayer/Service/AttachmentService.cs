using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class AttachmentService:IAttachmentService
    {

        public Int32 Insert(Attachments attachments)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ATTACHMENTS";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("@TRANS_TYPE", attachments.DOC_TYPE);
                cmd.Parameters.AddWithValue("@TRANS_ID", attachments.DOC_ID);
                cmd.Parameters.AddWithValue("@FILE_NAME", attachments.FILE_NAME);
                cmd.Parameters.AddWithValue("@FILE_DATA", attachments.FILE_DATA);
                cmd.Parameters.AddWithValue("@REMARKS", attachments.REMARKS);
                cmd.Parameters.AddWithValue("@USER_ID", attachments.USER_ID);

                Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());

                return CountryID;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Attachments attachments)
        {
            try
            {
                //Attachments attachments = new Attachments();
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_ATTACHMENTS";
                cmd.Parameters.AddWithValue("ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", attachments.ID);
                cmd.Parameters.AddWithValue("@USER_ID", attachments.USER_ID);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Attachments> list(AttachmentInput input)
        {
            List<Attachments> attachmentList = new List<Attachments>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_ATTACHMENTS";
            cmd.Parameters.AddWithValue("ACTION", 0);
            cmd.Parameters.AddWithValue("@TRANS_ID", input.DOC_ID);
            cmd.Parameters.AddWithValue("@TRANS_TYPE", input.DOC_TYPE);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow dr in tbl.Rows)
            {
                attachmentList.Add(new Attachments
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    FILE_NAME = ADO.ToString(dr["FILE_NAME"]),
                    FILE_DATA = dr["FILE_DATA"] as byte[],
                    USER_NAME = ADO.ToString(dr["USER_NAME"]),
                    CREATED_DATE_TIME = Convert.ToDateTime(dr["CREATED_TIME"]),
                    REMARKS = ADO.ToString(dr["REMARKS"]),
                    USER_ID = ADO.ToInt32(dr["USER_ID"])
                });
            }
            connection.Close();

            return attachmentList;
        }
    }
}
