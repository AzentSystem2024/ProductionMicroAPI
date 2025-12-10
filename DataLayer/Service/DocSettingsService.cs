using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class DocSettingsService:IDocSettingsService
    {
        public DocSettingsResponse Insert(DocSettings model)
        {
            DocSettingsResponse RESPONSE = new DocSettingsResponse();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_TB_DOC_SETTINGS", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 1);
                        CMD.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        CMD.Parameters.AddWithValue("@FIN_ID", model.FIN_ID);
                        CMD.Parameters.AddWithValue("@USER_ID", model.USER_ID);

                        // UDT for DOC_SETTINGS
                        DataTable DT = new DataTable();
                        DT.Columns.Add("TRANS_TYPE", typeof(int));
                        DT.Columns.Add("PREFIX", typeof(string));
                        DT.Columns.Add("START", typeof(int));
                        DT.Columns.Add("WIDTH", typeof(int));
                        DT.Columns.Add("VERIFY_REQUIRED", typeof(bool));
                        DT.Columns.Add("GROUP_CODE", typeof(string));

                        foreach (var ITEM in model.DOC_SETTINGS)
                        {
                            DT.Rows.Add(
                                ITEM.TRANS_TYPE,
                                ITEM.PREFIX ?? string.Empty,
                                ITEM.START,
                                ITEM.WIDTH,
                                ITEM.VERIFY_REQUIRED,
                                ITEM.GROUP_CODE ?? string.Empty
                            );
                        }

                        SqlParameter TVP_PARAM = CMD.Parameters.AddWithValue("@UDT_TB_DOC_SETTINGS", DT);
                        TVP_PARAM.SqlDbType = SqlDbType.Structured;
                        TVP_PARAM.TypeName = "UDT_TB_DOC_SETTINGS";

                        CMD.ExecuteNonQuery();

                        RESPONSE.flag = 1;
                        RESPONSE.Message = "Success.";
                    }
                }
            }
            catch (Exception ex)
            {
                RESPONSE.flag = 0;
                RESPONSE.Message = "ERROR: " + ex.Message;
            }

            return RESPONSE;
        }
        public DocSettingsListResponse List(DocSettingsListRequest request)
        {
            DocSettingsListResponse RESPONSE = new DocSettingsListResponse();
            RESPONSE.Data = new List<DocSettingsList>();

            try
            {
                using (SqlConnection CONNECTION = ADO.GetConnection())
                {
                    if (CONNECTION.State == ConnectionState.Closed)
                        CONNECTION.Open();

                    using (SqlCommand CMD = new SqlCommand("SP_TB_DOC_SETTINGS", CONNECTION))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;

                        CMD.Parameters.AddWithValue("@ACTION", 2);
                        CMD.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                        //CMD.Parameters.AddWithValue("@FIN_ID", request.FIN_ID);

                        using (SqlDataReader DR = CMD.ExecuteReader())
                        {
                            while (DR.Read())
                            {
                                RESPONSE.Data.Add(new DocSettingsList
                                {
                                    ID = DR["ID"] != DBNull.Value ? Convert.ToInt32(DR["ID"]) : 0,
                                    CODE = DR["CODE"]?.ToString(),
                                    DESCRIPTION = DR["DESCRIPTION"]?.ToString(),
                                    PREFIX = DR["PREFIX"]?.ToString(),
                                    START = DR["START"] != DBNull.Value ? Convert.ToInt32(DR["START"]) : (int?)null,
                                    WIDTH = DR["WIDTH"] != DBNull.Value ? Convert.ToInt32(DR["WIDTH"]) : (int?)null,
                                    VERIFY_REQUIRED = DR["VERIFY_REQUIRED"] != DBNull.Value ? Convert.ToBoolean(DR["VERIFY_REQUIRED"]) : (bool?)null,
                                    LAST_NO = DR["LastNo"]?.ToString(),
                                    NEXT_VOUCHER_NO = DR["NextVoucherNo"]?.ToString()
                                });
                            }
                        }

                        RESPONSE.flag = 1;
                        RESPONSE.Message = "Success.";
                    }
                }
            }
            catch (Exception ex)
            {
                RESPONSE.flag = 0;
                RESPONSE.Message = "ERROR: " + ex.Message;
            }

            return RESPONSE;
        }
        //public DocSettingsVoucherResponse GetNextVoucherNumber(DocSettingsVoucherRequest request)
        //{
        //    DocSettingsVoucherResponse response = new DocSettingsVoucherResponse();

        //    try
        //    {
        //        using (SqlConnection con = ADO.GetConnection())
        //        {
        //            if (con.State == ConnectionState.Closed)
        //                con.Open();

        //            using (SqlCommand cmd = new SqlCommand("SP_GET_NEXT_VOUCHER_NO", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
        //                cmd.Parameters.AddWithValue("@TRANS_TYPE", request.TRANS_TYPE);

        //                SqlParameter outParam = new SqlParameter("@NEXT_VOUCHER_NO", SqlDbType.VarChar, 50);
        //                outParam.Direction = ParameterDirection.Output;

        //                cmd.Parameters.Add(outParam);

        //                cmd.ExecuteNonQuery();

        //                string nextNo = Convert.ToString(outParam.Value);

        //                response.flag = 1;
        //                response.Message = "Success";
        //                response.VOUCHER_NO = nextNo;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.flag = 0;
        //        response.Message = "ERROR: " + ex.Message;
        //    }

        //    return response;
        //}

    }
}
