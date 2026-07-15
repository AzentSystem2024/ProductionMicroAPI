using MicroApi.Models;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;
using System.Globalization;
using System.Linq;
using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace MicroApi.Services
{
    public class SynchService : ISynchService
    {

        //       public SynchResponse UploadData(Synch model)
        //       {
        //           SynchResponse response = new SynchResponse();

        //           SqlConnection con = new SqlConnection();

        //           try
        //           {
        //               // DataTable tbl = JsonConvert.DeserializeObject<DataTable>(object.DATA);
        //               DataTable tbl = JsonConvert.DeserializeObject<DataTable>(
        //    model.DATA.ToString()
        //);
        //               con = ADO.GetConnection();

        //               if (con.State == ConnectionState.Closed)
        //                   con.Open();

        //               SqlCommand cmd = new SqlCommand();
        //               cmd.Connection = con;
        //               cmd.CommandType = CommandType.StoredProcedure;
        //               cmd.CommandText = "SP_SYNCH_UPDATE";
        //               cmd.CommandTimeout = 0;

        //               cmd.Parameters.AddWithValue("@TABLE_NAME", model.TABLE_NAME);
        //               cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);

        //               SqlParameter tvpParam =
        //                   cmd.Parameters.AddWithValue("@SYNCH_" + model.TABLE_NAME, tbl);

        //               tvpParam.SqlDbType = SqlDbType.Structured;
        //               tvpParam.TypeName = "dbo.SYNCH_" + model.TABLE_NAME;

        //               cmd.ExecuteNonQuery();

        //               response.flag = 1;
        //               response.Message = "Data Uploaded Successfully";
        //           }
        //           catch (Exception ex)
        //           {
        //               response.flag = 0;
        //               response.Message = ex.Message;
        //           }
        //           finally
        //           {
        //               if (con.State == ConnectionState.Open)
        //                   con.Close();
        //           }

        //           return response;
        //       }
        public SynchResponse UploadData(Synch model)
        {
            SynchResponse response = new SynchResponse();

            SqlConnection con = new SqlConnection();

            try
            {
                DataTable tbl = JsonConvert.DeserializeObject<DataTable>
                (

                    model.DATA.ToString()
                );

                con = ADO.GetConnection();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SYNCH_UPDATE";
                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@TABLE_NAME", model.TABLE_NAME);
                cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                cmd.Parameters.AddWithValue("@SYNCH_" + model.TABLE_NAME, tbl);
                //SqlParameter tvpParam =
                    

                //tvpParam.SqlDbType = SqlDbType.Structured;
                //tvpParam.TypeName = "dbo.SYNCH_" + model.TABLE_NAME;

                cmd.ExecuteNonQuery();

                //---------------------------------------------------
                // UPDATE TB_STORE_SYNCH_STATUS
                //---------------------------------------------------

                SqlCommand cmdStatus = new SqlCommand();
                cmdStatus.Connection = con;
                cmdStatus.CommandType = CommandType.Text;

                cmdStatus.CommandText = @"
                IF EXISTS(SELECT 1 FROM TB_STORE_SYNCH_STATUS WHERE STORE_ID = @STORE_ID)
                BEGIN
                    UPDATE TB_STORE_SYNCH_STATUS
                    SET LAST_SYNCH_TIME = GETUTCDATE()
                    WHERE STORE_ID = @STORE_ID
                END
                ELSE
                BEGIN
                INSERT INTO TB_STORE_SYNCH_STATUS
                (STORE_ID,LAST_SYNCH_TIME)
                VALUES(@STORE_ID,GETUTCDATE())
                END";

                cmdStatus.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);

                cmdStatus.ExecuteNonQuery();

                response.flag = 1;
                response.Message = "Data Uploaded Successfully";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return response;
        }
        public SynchDownloadResponse DownloadData(SynchDownload model)
        {
            SynchDownloadResponse response = new SynchDownloadResponse();

            SqlConnection con = new SqlConnection();

            try
            {
                con = ADO.GetConnection();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "SP_SYNCH_GETDATA";

                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@TABLE_NAME", model.TABLE_NAME);
                cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID);
                cmd.Parameters.AddWithValue("@TIMESTAMP", model.TIMESTAMP);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                response.Flag = 1;
                response.Message = "Success";

                // Convert DataTable to JSON String
                response.DATA = JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.DATA = "";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return response;
        }
        public List<PendingStoreResponse> GetSynchPendingStores()
        {
            List<PendingStoreResponse> list = new List<PendingStoreResponse>();

            using (SqlConnection con = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_GET_STORE_SYNCH_STATUS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                   // con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PendingStoreResponse
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                CODE = reader["CODE"].ToString(),
                                STORE_NAME = reader["STORE_NAME"].ToString(),
                                ADDRESS1 = reader["ADDRESS1"].ToString(),
                                TIME_DIFFERENCE = reader["DIFF_MINUTES"].ToString(),
                                LAST_SYNCH_TIME = Convert.ToDateTime(reader["LAST_SYNCH_TIME"]).ToString("dd-MM-yyyy hh:mm:ss tt")

                            });
                        }
                    }
                }
            }

            return list;
        }
        public SynchResponse UpdateLastSynchTime(UpdateLastSynchTimeRequest request)
        {
            SynchResponse response = new SynchResponse();

            try
            {
                using SqlConnection con = ADO.GetConnection();

                using SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                IF EXISTS (SELECT 1 FROM TB_STORE_SYNCH_STATUS WHERE STORE_ID = @STORE_ID)
                BEGIN
                    UPDATE TB_STORE_SYNCH_STATUS
                    SET LAST_SYNCH_TIME = GETUTCDATE()
                    WHERE STORE_ID = @STORE_ID
                END
                ELSE
                BEGIN
                    INSERT INTO TB_STORE_SYNCH_STATUS (STORE_ID, LAST_SYNCH_TIME)
                    VALUES (@STORE_ID, GETUTCDATE())
                END";

                cmd.Parameters.Add("@STORE_ID", SqlDbType.Int).Value = request.STORE_ID;

                cmd.ExecuteNonQuery();

                response.flag = 1;
                response.Message = "Success.";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }

}
