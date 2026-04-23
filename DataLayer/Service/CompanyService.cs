using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class CompanyService:ICompanyService
    {
        public CompanyResponse Insert(Company company)
        {
            CompanyResponse res = new CompanyResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_COMPANY_MASTER";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 1);
                        cmd.Parameters.AddWithValue("@COMPANY_CODE", company.COMPANY_CODE ?? "");
                        cmd.Parameters.AddWithValue("@COMPANY_NAME", company.COMPANY_NAME ?? "");
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", company.CONTACT_NAME ?? "");
                        cmd.Parameters.AddWithValue("@ADDRESS1", company.ADDRESS1 ?? "");
                        cmd.Parameters.AddWithValue("@ADDRESS2", company.ADDRESS2 ?? "");
                        cmd.Parameters.AddWithValue("@ADDRESS3", company.ADDRESS3 ?? "");
                        cmd.Parameters.AddWithValue("@PHONE", company.PHONE ?? "");
                        cmd.Parameters.AddWithValue("@MOBILE", company.MOBILE ?? "");
                        cmd.Parameters.AddWithValue("@EMAIL", company.EMAIL ?? "");
                        cmd.Parameters.AddWithValue("@WHATSAPP", company.WHATSAPP ?? "");
                        cmd.Parameters.AddWithValue("@COMPANY_TYPE", company.COMPANY_TYPE);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", company.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@STATE_ID", company.STATE_ID);
                        cmd.Parameters.AddWithValue("@GST_NO", company.GST_NO ?? "");
                        cmd.Parameters.AddWithValue("@PAN_NO", company.PAN_NO ?? "");
                        cmd.Parameters.AddWithValue("@CIN", company.CIN ?? "");

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int newCompanyId = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;

                                res.flag = 1;
                                res.Message = "Success";
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Failed to insert company.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Exception: " + ex.Message;
            }

            return res;
        }


        public CompanyResponse UpdateCompany(CompanyUpdate company)
        {
            CompanyResponse res = new CompanyResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_COMPANY_MASTER";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 2); 
                        cmd.Parameters.AddWithValue("@ID", company.ID);
                        cmd.Parameters.AddWithValue("@COMPANY_CODE", company.COMPANY_CODE ?? "");
                        cmd.Parameters.AddWithValue("@COMPANY_NAME", company.COMPANY_NAME ?? "");
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", company.CONTACT_NAME ?? "");
                        cmd.Parameters.AddWithValue("@ADDRESS1", company.ADDRESS1 ?? "");
                        cmd.Parameters.AddWithValue("@ADDRESS2", company.ADDRESS2 ?? "");
                        cmd.Parameters.AddWithValue("@ADDRESS3", company.ADDRESS3 ?? "");
                        cmd.Parameters.AddWithValue("@PHONE", company.PHONE ?? "");
                        cmd.Parameters.AddWithValue("@MOBILE", company.MOBILE ?? "");
                        cmd.Parameters.AddWithValue("@EMAIL", company.EMAIL ?? "");
                        cmd.Parameters.AddWithValue("@WHATSAPP", company.WHATSAPP ?? "");
                        cmd.Parameters.AddWithValue("@COMPANY_TYPE", company.COMPANY_TYPE);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", company.IS_INACTIVE);
                        cmd.Parameters.AddWithValue("@STATE_ID", company.STATE_ID);
                        cmd.Parameters.AddWithValue("@GST_NO", company.GST_NO ?? "");
                        cmd.Parameters.AddWithValue("@PAN_NO", company.PAN_NO ?? "");
                        cmd.Parameters.AddWithValue("@CIN", company.CIN ?? "");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "failed.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        public CompanyListResponse GetCompanyList()
        {
            CompanyListResponse res = new CompanyListResponse
            {
                Data = new List<CompanyUpdate>(),
                flag = 0,
                Message = "Failed"
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_COMPANY_MASTER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0);
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value); 

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CompanyUpdate company = new CompanyUpdate
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,

                                    COMPANY_CODE = reader["COMPANY_CODE"] != DBNull.Value ? Convert.ToString(reader["COMPANY_CODE"]) : "",
                                    COMPANY_NAME = reader["COMPANY_NAME"] != DBNull.Value ? Convert.ToString(reader["COMPANY_NAME"]) : "",
                                    CONTACT_NAME = reader["CONTACT_NAME"] != DBNull.Value ? Convert.ToString(reader["CONTACT_NAME"]) : "",

                                    ADDRESS1 = reader["ADDRESS1"] != DBNull.Value ? Convert.ToString(reader["ADDRESS1"]) : "",
                                    ADDRESS2 = reader["ADDRESS2"] != DBNull.Value ? Convert.ToString(reader["ADDRESS2"]) : "",
                                    ADDRESS3 = reader["ADDRESS3"] != DBNull.Value ? Convert.ToString(reader["ADDRESS3"]) : "",

                                    PHONE = reader["PHONE"] != DBNull.Value ? Convert.ToString(reader["PHONE"]) : "",
                                    MOBILE = reader["MOBILE"] != DBNull.Value ? Convert.ToString(reader["MOBILE"]) : "",
                                    EMAIL = reader["EMAIL"] != DBNull.Value ? Convert.ToString(reader["EMAIL"]) : "",
                                    WHATSAPP = reader["WHATSAPP"] != DBNull.Value ? Convert.ToString(reader["WHATSAPP"]) : "",

                                    COMPANY_TYPE = reader["COMPANY_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_TYPE"]) : 0,
                                    COMPANY_TYPE_NAME = reader["COMPANY_TYPE_NAME"] != DBNull.Value ? Convert.ToString(reader["COMPANY_TYPE_NAME"]) : "",

                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"]),

                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,

                                    GST_NO = reader["GST_NO"] != DBNull.Value ? Convert.ToString(reader["GST_NO"]) : "",
                                    PAN_NO = reader["PAN_NO"] != DBNull.Value ? Convert.ToString(reader["PAN_NO"]) : "",
                                    CIN = reader["CIN"] != DBNull.Value ? Convert.ToString(reader["CIN"]) : ""
                                };

                                res.Data.Add(company);
                            }
                        }
                    }

                    res.flag = 1;
                    res.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

        public CompanyResponse GetCompanyById(int id)
        {
            CompanyResponse res = new CompanyResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_TB_COMPANY_MASTER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACTION", 0); 
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@COMPANY_CODE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADDRESS1", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADDRESS2", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADDRESS3", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PHONE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MOBILE", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WHATSAPP", DBNull.Value);
                        cmd.Parameters.AddWithValue("@COMPANY_TYPE", 0);
                        cmd.Parameters.AddWithValue("@COMPANY_TYPE_NAME", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", DBNull.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var company = new CompanyUpdate
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,

                                    COMPANY_CODE = reader["COMPANY_CODE"] != DBNull.Value ? Convert.ToString(reader["COMPANY_CODE"]) : "",
                                    COMPANY_NAME = reader["COMPANY_NAME"] != DBNull.Value ? Convert.ToString(reader["COMPANY_NAME"]) : "",
                                    CONTACT_NAME = reader["CONTACT_NAME"] != DBNull.Value ? Convert.ToString(reader["CONTACT_NAME"]) : "",

                                    ADDRESS1 = reader["ADDRESS1"] != DBNull.Value ? Convert.ToString(reader["ADDRESS1"]) : "",
                                    ADDRESS2 = reader["ADDRESS2"] != DBNull.Value ? Convert.ToString(reader["ADDRESS2"]) : "",
                                    ADDRESS3 = reader["ADDRESS3"] != DBNull.Value ? Convert.ToString(reader["ADDRESS3"]) : "",

                                    PHONE = reader["PHONE"] != DBNull.Value ? Convert.ToString(reader["PHONE"]) : "",
                                    MOBILE = reader["MOBILE"] != DBNull.Value ? Convert.ToString(reader["MOBILE"]) : "",
                                    EMAIL = reader["EMAIL"] != DBNull.Value ? Convert.ToString(reader["EMAIL"]) : "",
                                    WHATSAPP = reader["WHATSAPP"] != DBNull.Value ? Convert.ToString(reader["WHATSAPP"]) : "",

                                    COMPANY_TYPE = reader["COMPANY_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_TYPE"]) : 0,
                                    COMPANY_TYPE_NAME = reader["COMPANY_TYPE_NAME"] != DBNull.Value ? Convert.ToString(reader["COMPANY_TYPE_NAME"]) : "",

                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"]),

                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,

                                    GST_NO = reader["GST_NO"] != DBNull.Value ? Convert.ToString(reader["GST_NO"]) : "",
                                    PAN_NO = reader["PAN_NO"] != DBNull.Value ? Convert.ToString(reader["PAN_NO"]) : "",
                                    CIN = reader["CIN"] != DBNull.Value ? Convert.ToString(reader["CIN"]) : ""
                                };

                                res.flag = 1;
                                res.Message = "Success";
                                res.Data = company;
                            }
                            else
                            {
                                res.flag = 0;
                                res.Message = "Company not found";
                                res.Data = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        public CompanyResponse DeleteCompany(int id)
        {
            CompanyResponse res = new CompanyResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_COMPANY_MASTER";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3); 
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        public MobileDigitsResponse GetMobileDigits(MobileDigitsRequest request)
        {
            MobileDigitsResponse res = new MobileDigitsResponse();
            res.Data = new List<MobileDigits>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT DISTINCT MOBILE_DIGITS, COUNTRY_NAME, COUNTRY_CODE
                                    FROM TB_COUNTRY_PHONE_CODE
                                    WHERE REPLACE(COUNTRY_CODE,'+','') = REPLACE(@COUNTRY_CODE,'+','')";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@COUNTRY_CODE", request.COUNTRY_CODE);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Data.Add(new MobileDigits
                                {
                                    COUNTRY_NAME = reader["COUNTRY_NAME"].ToString(),
                                    COUNTRY_CODE = reader["COUNTRY_CODE"].ToString(),
                                    MOBILE_DIGITS = reader["MOBILE_DIGITS"].ToString()
                                });
                            }
                        }

                        if (res.Data.Count > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Country code not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }


    }
}
