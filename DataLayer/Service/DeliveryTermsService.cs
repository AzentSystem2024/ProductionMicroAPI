using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class DeliveryTermsService:IDeliveryTermsService
    {
        public List<DeliveryTerms> GetAllDeliveryTerms()
        {
            List<DeliveryTerms> deliveryList = new List<DeliveryTerms>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_DELIVERY_TERMS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    deliveryList.Add(new DeliveryTerms
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        IS_DELETED = Convert.ToString(dr["IS_DELETED"])

                    });
                }
                connection.Close();
            }
            return deliveryList;
        }

        public Int32 SaveData(DeliveryTerms deliveryTerms)
        {
            try
            {

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DELIVERY_TERMS";

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", deliveryTerms.ID);
                    cmd.Parameters.AddWithValue("CODE", deliveryTerms.CODE);
                    cmd.Parameters.AddWithValue("DESCRIPTION", deliveryTerms.DESCRIPTION);

                    Int32 DeliveryTermID = Convert.ToInt32(cmd.ExecuteScalar());



                    return DeliveryTermID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DeliveryTerms GetItems(int id)
        {
            DeliveryTerms paymentTerms = new DeliveryTerms();

            try
            {


                string strSQL = "SELECT ID, CODE, DESCRIPTION, IS_DELETED FROM TB_DELIVERY_TERMS " +
                 "WHERE ID = " + id;




                DataTable tbl = ADO.GetDataTable(strSQL, "DeliveryTems");
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
        public bool DeleteDeliveryTerms(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DELIVERY_TERMS";
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
