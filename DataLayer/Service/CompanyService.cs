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
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    string procedureName = "SP_TB_COMPANY_MASTER";

                    using (var cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Failed.";
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
                                    ID = Convert.ToInt32(reader["ID"]),
                                    COMPANY_CODE = reader["COMPANY_CODE"]?.ToString(),
                                    COMPANY_NAME = reader["COMPANY_NAME"]?.ToString(),
                                    CONTACT_NAME = reader["CONTACT_NAME"]?.ToString(),
                                    ADDRESS1 = reader["ADDRESS1"]?.ToString(),
                                    ADDRESS2 = reader["ADDRESS2"]?.ToString(),
                                    ADDRESS3 = reader["ADDRESS3"]?.ToString(),
                                    PHONE = reader["PHONE"]?.ToString(),
                                    MOBILE = reader["MOBILE"]?.ToString(),
                                    EMAIL = reader["EMAIL"]?.ToString(),
                                    WHATSAPP = reader["WHATSAPP"]?.ToString(),
                                    COMPANY_TYPE = reader["COMPANY_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_TYPE"]) : 0,
                                    COMPANY_TYPE_NAME = reader["COMPANY_TYPE_NAME"]?.ToString(), 
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : false
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
                                    ID = Convert.ToInt32(reader["ID"]),
                                    COMPANY_CODE = reader["COMPANY_CODE"]?.ToString(),
                                    COMPANY_NAME = reader["COMPANY_NAME"]?.ToString(),
                                    CONTACT_NAME = reader["CONTACT_NAME"]?.ToString(),
                                    ADDRESS1 = reader["ADDRESS1"]?.ToString(),
                                    ADDRESS2 = reader["ADDRESS2"]?.ToString(),
                                    ADDRESS3 = reader["ADDRESS3"]?.ToString(),
                                    PHONE = reader["PHONE"]?.ToString(),
                                    MOBILE = reader["MOBILE"]?.ToString(),
                                    EMAIL = reader["EMAIL"]?.ToString(),
                                    WHATSAPP = reader["WHATSAPP"]?.ToString(),
                                    COMPANY_TYPE = reader["COMPANY_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["COMPANY_TYPE"]) : 0,
                                    COMPANY_TYPE_NAME = reader["COMPANY_TYPE_NAME"]?.ToString(),
                                    IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(reader["IS_INACTIVE"]) : false
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


    }
}
