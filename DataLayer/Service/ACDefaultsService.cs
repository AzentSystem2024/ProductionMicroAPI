using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class ACDefaultsService:IACDefaultsService
    {
        public AcDefaultsListResponse GetACDefaultsList(AcDefaultsListReq request)
        {
            var response = new AcDefaultsListResponse
            {
                DATA = new List<AcDefaultsList>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_AC_DEFAULTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            response.DATA.Add(new AcDefaultsList
                            {
                                NAME = rdr["NAME"].ToString(),
                                HEAD_ID = Convert.ToInt32(rdr["HEAD_ID"])
                            });
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        public AcDefaultsListResponse Save(ACDefaults request)
        {
            var response = new AcDefaultsListResponse
            {
                DATA = new List<AcDefaultsList>()
            };

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_AC_DEFAULTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);

                    cmd.Parameters.AddWithValue("@AC_SALE_ID", (object)request.AC_SALE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AC_PURCHASE_ID", (object)request.AC_PURCHASE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AC_INVENTORY_ID", (object)request.AC_INVENTORY_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AC_INPUT_VAT", (object)request.AC_INPUT_VAT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AC_OUTPUT_VAT", (object)request.AC_OUTPUT_VAT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AC_DEPRECIATION_EXPENSE_ID", (object)request.AC_DEPRECIATION_EXPENSE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AC_GOODS_TRANSIT", (object)request.AC_GOODS_TRANSIT ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                response.Flag = 1;
                response.Message = "AC Defaults saved successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
