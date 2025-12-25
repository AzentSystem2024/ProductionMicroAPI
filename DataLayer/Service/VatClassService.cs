using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class VatClassService:IVatClassService
    {
        public List<VatClass> GetAllVatClass(VatClassList request)
        {

            List<VatClass> vatList = new List<VatClass>();
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
                    vatList.Add(new VatClass
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        VAT_NAME = Convert.ToString(dr["VAT_NAME"]),
                        // VAT_PERC = Convert.ToDecimal(dr["VAT_PERC"])
                        VAT_PERC = Convert.ToDecimal(dr["VAT_PERC"])

                    });
                }
                connection.Close();
            }
            return vatList;
        }

        public Int32 SaveData(VatClass vatClass)
        {
            try
            {

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
                    cmd.Parameters.AddWithValue("VAT_PERC", vatClass.VAT_PERC);
                    cmd.Parameters.AddWithValue("COMPANY_ID", vatClass.COMPANY_ID);

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


                string strSQL = "SELECT ID,CODE,VAT_NAME,VAT_PERC,IS_DELETED,COMPANY_ID from TB_VAT_CLASS WHERE TB_VAT_CLASS.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "VatClass");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    vatClass.ID = Convert.ToInt32(dr["ID"]);
                    vatClass.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    vatClass.CODE = Convert.ToString(dr["CODE"]);
                    vatClass.VAT_NAME = Convert.ToString(dr["VAT_NAME"]);
                    vatClass.VAT_PERC = Convert.ToDecimal(dr["VAT_PERC"]);

                    vatClass.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

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
