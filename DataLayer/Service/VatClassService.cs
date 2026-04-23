using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class VatClassService:IVatClassService
    {
        public string GetAppType()
        {
            string appType = "";

            using (SqlConnection con = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 APP_TYPE FROM TB_CONFIGURATION", con);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    appType = result.ToString();
                }
            }

            return appType;
        }

        public List<VatClass> GetAllVatClass(VatClassList request)
        {
            List<VatClass> vatList = new List<VatClass>();

            string appType = GetAppType();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_VAT_CLASS";

                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    VatClass vat = new VatClass();

                    vat.ID = Convert.ToInt32(dr["ID"]);
                    vat.CODE = Convert.ToString(dr["CODE"]);
                    vat.VAT_NAME = Convert.ToString(dr["VAT_NAME"]);

                    //if (appType == "VEZTA")
                    //{
                    //    vat.VAT_PERC = dr["VAT_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VAT_PERC"]);
                    //}
                    //else
                    
                        vat.VAT_PERC = dr["VAT_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VAT_PERC"]);
                        vat.CGST_PERC = dr["CGST_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CGST_PERC"]);
                        vat.SGST_PERC = dr["SGST_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SGST_PERC"]);
                        vat.IGST_PERC = dr["IGST_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["IGST_PERC"]);
                    

                    vatList.Add(vat);
                }

                connection.Close();
            }

            return vatList;
        }

        public Int32 SaveData(VatClass vatClass)
        {
            try
            {
                string appType = GetAppType();

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_VAT_CLASS";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", vatClass.ID);
                    cmd.Parameters.AddWithValue("CODE", vatClass.CODE);
                    cmd.Parameters.AddWithValue("VAT_NAME", vatClass.VAT_NAME);
                    cmd.Parameters.AddWithValue("COMPANY_ID", vatClass.COMPANY_ID);

                  
                        cmd.Parameters.AddWithValue("VAT_PERC", vatClass.VAT_PERC);
                    
                        cmd.Parameters.AddWithValue("CGST_PERC", vatClass.CGST_PERC);
                        cmd.Parameters.AddWithValue("SGST_PERC", vatClass.SGST_PERC);
                        cmd.Parameters.AddWithValue("IGST_PERC", vatClass.IGST_PERC);

                        cmd.Parameters.AddWithValue("CGST_INPUT_HEAD_ID", vatClass.CGST_INPUT_HEAD_ID);
                        cmd.Parameters.AddWithValue("CGST_OUTPUT_HEAD_ID", vatClass.CGST_OUTPUT_HEAD_ID);

                        cmd.Parameters.AddWithValue("SGST_INPUT_HEAD_ID", vatClass.SGST_INPUT_HEAD_ID);
                        cmd.Parameters.AddWithValue("SGST_OUTPUT_HEAD_ID", vatClass.SGST_OUTPUT_HEAD_ID);

                        cmd.Parameters.AddWithValue("IGST_INPUT_HEAD_ID", vatClass.IGST_INPUT_HEAD_ID);
                        cmd.Parameters.AddWithValue("IGST_OUTPUT_HEAD_ID", vatClass.IGST_OUTPUT_HEAD_ID);
                    

                    Int32 VatclassID = Convert.ToInt32(cmd.ExecuteScalar());

                    return VatclassID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public VatClass GetItems(int id)
        {
            VatClass vatClass = new VatClass();

            try
            {
                string appType = GetAppType();

                using (SqlConnection con = ADO.GetConnection())
                {
                    string query = "SELECT * FROM TB_VAT_CLASS WHERE ID = @ID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    if (tbl.Rows.Count > 0)
                    {
                        DataRow dr = tbl.Rows[0];

                        vatClass.ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]);
                        vatClass.COMPANY_ID = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);
                        vatClass.CODE = dr["CODE"] == DBNull.Value ? "" : dr["CODE"].ToString();
                        vatClass.VAT_NAME = dr["VAT_NAME"] == DBNull.Value ? "" : dr["VAT_NAME"].ToString();

                        if (appType == "MARK") // GST
                        {
                            vatClass.CGST_PERC = dr["CGST_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CGST_PERC"]);
                            vatClass.SGST_PERC = dr["SGST_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SGST_PERC"]);
                            vatClass.IGST_PERC = dr["IGST_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["IGST_PERC"]);

                            vatClass.CGST_INPUT_HEAD_ID = dr["CGST_INPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CGST_INPUT_HEAD_ID"]);
                            vatClass.CGST_OUTPUT_HEAD_ID = dr["CGST_OUTPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CGST_OUTPUT_HEAD_ID"]);

                            vatClass.SGST_INPUT_HEAD_ID = dr["SGST_INPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SGST_INPUT_HEAD_ID"]);
                            vatClass.SGST_OUTPUT_HEAD_ID = dr["SGST_OUTPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SGST_OUTPUT_HEAD_ID"]);

                            vatClass.IGST_INPUT_HEAD_ID = dr["IGST_INPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IGST_INPUT_HEAD_ID"]);
                            vatClass.IGST_OUTPUT_HEAD_ID = dr["IGST_OUTPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IGST_OUTPUT_HEAD_ID"]);
                        }
                        else // VAT
                        {
                            vatClass.VAT_PERC = dr["VAT_PERC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VAT_PERC"]);

                            vatClass.IGST_INPUT_HEAD_ID = dr["VAT_INPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VAT_INPUT_HEAD_ID"]);
                            vatClass.IGST_OUTPUT_HEAD_ID = dr["VAT_OUTPUT_HEAD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VAT_OUTPUT_HEAD_ID"]);
                        }

                        vatClass.IS_DELETED = dr["IS_DELETED"] == DBNull.Value ? "0" : dr["IS_DELETED"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vatClass;
        }


        public bool DeleteVatClass(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_VAT_CLASS";
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
