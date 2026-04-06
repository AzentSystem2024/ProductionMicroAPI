using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class PaymentTermService:IPaymentTermService
    {
        public List<PaymentTerms> GetAllPaymentTerms()
        {
            List<PaymentTerms> paymentList = new List<PaymentTerms>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PAYMENT_TERMS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    paymentList.Add(new PaymentTerms
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        IS_DELETED = Convert.ToString(dr["IS_DELETED"])

                    });
                }
                connection.Close();
            }
            return paymentList;
        }

        public Int32 SaveData(PaymentTerms paymentTerms)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAYMENT_TERMS";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", paymentTerms.ID);
                    cmd.Parameters.AddWithValue("CODE", paymentTerms.CODE);
                    cmd.Parameters.AddWithValue("DESCRIPTION", paymentTerms.DESCRIPTION);

                    Int32 PaymentTermID = Convert.ToInt32(cmd.ExecuteScalar());



                    return PaymentTermID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PaymentTerms GetItems(int id)
        {
            PaymentTerms paymentTerms = new PaymentTerms();

            try
            {


                string strSQL = "SELECT ID, CODE, DESCRIPTION, IS_DELETED FROM TB_PAYMENT_TERMS " +
                 "WHERE ID = " + id;




                DataTable tbl = ADO.GetDataTable(strSQL, "PaymentTerms");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    paymentTerms.ID = Convert.ToInt32(dr["ID"]);
                    paymentTerms.CODE = Convert.ToString(dr["CODE"]);
                    paymentTerms.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                    paymentTerms.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);

                }
            }
            catch (Exception ex)
            {

            }
            return paymentTerms;
        }
        public bool DeletePaymentTerms(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAYMENT_TERMS";
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
