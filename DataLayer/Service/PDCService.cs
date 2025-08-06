using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class PDCService : IPDCService
    {
        private static object ParseDate(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy" };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public PDCResponse GetPDCList()
        {
            PDCResponse response = new PDCResponse { Data = new List<PDCList>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PDC", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3); // List action

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new PDCList
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    ENTRY_NO = reader["ENTRY_NO"] != DBNull.Value ? reader["ENTRY_NO"].ToString() : null,
                                    ENTRY_DATE = reader["ENTRY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["ENTRY_DATE"]).ToString("dd/MM/yyyy") : null,
                                    BENEFICIARY_NAME = reader["BENEFICIARY_NAME"] != DBNull.Value ? reader["BENEFICIARY_NAME"].ToString() : null,
                                    CHEQUE_NO = reader["CHEQUE_NO"] != DBNull.Value ? reader["CHEQUE_NO"].ToString() : null,
                                    CHEQUE_DATE = reader["DUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DUE_DATE"]).ToString("dd/MM/yyyy") : null,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["AMOUNT"]) : 0m,
                                    REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null,
                                    IS_PAYMENT = reader["IS_PAYMENT"] != DBNull.Value ? Convert.ToBoolean(reader["IS_PAYMENT"]) : (bool?)null,
                                    ENTRY_STATUS = reader["ENTRY_STATUS"] != DBNull.Value ? reader["ENTRY_STATUS"].ToString() : null
                                });
                            }
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
        public PDCSaveResponse SaveData(PDCModel pdc)
        {
            PDCSaveResponse response = new PDCSaveResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_PDC", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1); // Insert action
                        cmd.Parameters.AddWithValue("@BANK_HEAD_ID", (object)pdc.BANK_HEAD_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CUST_ID", (object)pdc.CUST_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", (object)pdc.SUPP_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BENEFICIARY_NAME", (object)pdc.BENEFICIARY_NAME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BENEFICIARY_TYPE", (object)pdc.BENEFICIARY_TYPE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ENTRY_DATE", ParseDate(pdc.ENTRY_DATE));
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", (object)pdc.CHEQUE_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(pdc.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@AMOUNT", (object)pdc.AMOUNT ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@REMARKS", (object)pdc.REMARKS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PAYMENT", (object)pdc.IS_PAYMENT ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ENTRY_STATUS", (object)pdc.ENTRY_STATUS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AC_TRANS_ID", (object)pdc.AC_TRANS_ID ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                response.Flag = "1";
                response.Message = "Data saved successfully.";
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
        public PDCSaveResponse UpdateData(PDCModel pdc)
        {
            PDCSaveResponse response = new PDCSaveResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PDC", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2); // Update action
                        cmd.Parameters.AddWithValue("@ID", pdc.ID);
                        cmd.Parameters.AddWithValue("@BANK_HEAD_ID", (object)pdc.BANK_HEAD_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CUST_ID", (object)pdc.CUST_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SUPP_ID", (object)pdc.SUPP_ID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BENEFICIARY_NAME", (object)pdc.BENEFICIARY_NAME ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BENEFICIARY_TYPE", (object)pdc.BENEFICIARY_TYPE ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ENTRY_DATE", ParseDate(pdc.ENTRY_DATE));
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", (object)pdc.CHEQUE_NO ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", ParseDate(pdc.CHEQUE_DATE));
                        cmd.Parameters.AddWithValue("@AMOUNT", (object)pdc.AMOUNT ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@REMARKS", (object)pdc.REMARKS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IS_PAYMENT", (object)pdc.IS_PAYMENT ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ENTRY_STATUS", (object)pdc.ENTRY_STATUS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AC_TRANS_ID", (object)pdc.AC_TRANS_ID ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                response.Flag = "1";
                response.Message = "Data updated successfully.";
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }

        public PDCSelectResponse GetPDCById(int? id = null)
        {
            PDCSelectResponse response = new PDCSelectResponse { Data = new List<PDCModelSelect>() };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_TB_PDC", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // Select action
                        cmd.Parameters.AddWithValue("@ID", (object)id ?? DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Data.Add(new PDCModelSelect
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    BANK_HEAD_ID = reader["BANK_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(reader["BANK_HEAD_ID"]) : (int?)null,
                                    CUST_ID = reader["CUST_ID"] != DBNull.Value ? Convert.ToInt32(reader["CUST_ID"]) : (int?)null,
                                    SUPP_ID = reader["SUPP_ID"] != DBNull.Value ? Convert.ToInt32(reader["SUPP_ID"]) : (int?)null,
                                    BENEFICIARY_NAME = reader["BENEFICIARY_NAME"] != DBNull.Value ? reader["BENEFICIARY_NAME"].ToString() : null,
                                    BENEFICIARY_TYPE = reader["BENEFICIARY_TYPE"] != DBNull.Value ? Convert.ToInt32(reader["BENEFICIARY_TYPE"]) : 0,
                                    ENTRY_NO = reader["ENTRY_NO"] != DBNull.Value ? reader["ENTRY_NO"].ToString() : null,
                                    ENTRY_DATE = reader["ENTRY_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["ENTRY_DATE"]).ToString("dd/MM/yyyy") : null,
                                    CHEQUE_NO = reader["CHEQUE_NO"] != DBNull.Value ? reader["CHEQUE_NO"].ToString() : null,
                                    CHEQUE_DATE = reader["CHEQUE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CHEQUE_DATE"]).ToString("dd/MM/yyyy") : null,
                                    AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["AMOUNT"]) : (decimal?)null,
                                    REMARKS = reader["REMARKS"] != DBNull.Value ? reader["REMARKS"].ToString() : null,
                                    IS_PAYMENT = reader["IS_PAYMENT"] != DBNull.Value ? Convert.ToBoolean(reader["IS_PAYMENT"]) : (bool?)null,
                                    ENTRY_STATUS = reader["ENTRY_STATUS"] != DBNull.Value ? reader["ENTRY_STATUS"].ToString() : null,
                                    AC_TRANS_ID = reader["AC_TRANS_ID"] != DBNull.Value ? Convert.ToInt32(reader["AC_TRANS_ID"]) : (int?)null
                                });
                            }
                        }
                    }
                }

                response.Flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }

        public PDCSaveResponse Delete(int id)
        {
            PDCSaveResponse response = new PDCSaveResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_PDC", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 4); // Delete action
                        cmd.Parameters.AddWithValue("@ID", id);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                response.Flag = "1";
                response.Message = "Data deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
    }
}
