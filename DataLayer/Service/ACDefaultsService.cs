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
                            AcDefaultsList obj = new AcDefaultsList
                            {
                                NAME = rdr["NAME"].ToString(),
                                HEAD_NAME = rdr["HEAD_NAME"] == DBNull.Value
                                                ? null
                                                : rdr["HEAD_NAME"].ToString(),
                                HEAD_ID = rdr["HEAD_ID"] == DBNull.Value
                                                ? null
                                                : Convert.ToInt32(rdr["HEAD_ID"])
                            };

                            response.DATA.Add(obj);
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
        public ACDefaultsResponse DeleteAcDefault(AcDefaultsDeleteReq request)
        {
            var response = new ACDefaultsResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_AC_DEFAULTS", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@HEAD_ID", request.HEAD_ID);

                    cmd.ExecuteNonQuery();
                }

                response.Flag = 1;
                response.Message = "Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }


        public ACDefaultSettingsResponse GetACDefaultSettingsList(ACDefaultSettingsInput request)
        {
            ACDefaultSettingsResponse response = new ACDefaultSettingsResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT *
            FROM TB_AC_DEFAULTS
            WHERE COMPANY_ID = @COMPANY_ID", con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@COMPANY_ID", request.CompanyID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            response.Data = new ACDefaultSettings
                            {
                                GP_SUPPLIER_ID = rdr["GP_SUPPLIER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["GP_SUPPLIER_ID"]),
                                GP_CUSTOMER_ID = rdr["GP_CUSTOMER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["GP_CUSTOMER_ID"]),
                                GP_ASSET_ID = rdr["GP_ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["GP_ASSET_ID"]),
                                GP_FIXED_ASSET_ID = rdr["GP_FIXED_ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["GP_FIXED_ASSET_ID"]),
                                GP_CASH_ID = rdr["GP_CASH_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["GP_CASH_ID"]),
                                GP_BANK_ID = rdr["GP_BANK_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["GP_BANK_ID"]),

                                AC_CASH_ID = rdr["AC_CASH_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_CASH_ID"]),
                                AC_PETTY_CASH_ID = rdr["AC_PETTY_CASH_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_PETTY_CASH_ID"]),

                                AC_SALE_ID = rdr["AC_SALE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_SALE_ID"]),
                                AC_OUTSIDE_SALE_ID = rdr["AC_OUTSIDE_SALE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_OUTSIDE_SALE_ID"]),
                                AC_PURCHASE_ID = rdr["AC_PURCHASE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_PURCHASE_ID"]),
                                AC_SALARY_PAYABLE_ID = rdr["AC_SALARY_PAYABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_SALARY_PAYABLE_ID"]),
                                AC_SALARY_EXPENSE_ID = rdr["AC_SALARY_EXPENSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_SALARY_EXPENSE_ID"]),
                                AC_LS_PAYABLE_ID = rdr["AC_LS_PAYABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_LS_PAYABLE_ID"]),
                                AC_LS_EXPENSE_ID = rdr["AC_LS_EXPENSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_LS_EXPENSE_ID"]),
                                AC_EOS_PAYABLE_ID = rdr["AC_EOS_PAYABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_EOS_PAYABLE_ID"]),

                                AC_EOS_EXPENSE_ID = rdr["AC_EOS_EXPENSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_EOS_EXPENSE_ID"]),
                                AC_INVENTORY_ID = rdr["AC_INVENTORY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_INVENTORY_ID"]),
                                AC_WIP_ID = rdr["AC_WIP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_WIP_ID"]),
                                AC_COST_INVENTORY_ID = rdr["AC_COST_INVENTORY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_COST_INVENTORY_ID"]),
                                AC_PDC_RECEIVABLE_ID = rdr["AC_PDC_RECEIVABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_PDC_RECEIVABLE_ID"]),
                                AC_PDC_PAYABLE_ID = rdr["AC_PDC_PAYABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_PDC_PAYABLE_ID"]),
                                AC_ASSET_ID = rdr["AC_ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_ASSET_ID"]),

                                AC_RECEIVABLE_ID = rdr["AC_RECEIVABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_RECEIVABLE_ID"]),
                                AC_PAYABLE_ID = rdr["AC_PAYABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_PAYABLE_ID"]),
                                AC_ADVANCE_PAYABLE_ID = rdr["AC_ADVANCE_PAYABLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_ADVANCE_PAYABLE_ID"]),
                                AC_OUTPUT_VAT = rdr["AC_OUTPUT_VAT"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_OUTPUT_VAT"]),
                                AC_DEPRECIATION_EXPENSE_ID = rdr["AC_DEPRECIATION_EXPENSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_DEPRECIATION_EXPENSE_ID"]),
                                AC_GOODS_TRANSIT = rdr["AC_GOODS_TRANSIT"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["AC_GOODS_TRANSIT"])
                            };
                        }
                    }
                }

                response.flag = "1";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = "0";
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
