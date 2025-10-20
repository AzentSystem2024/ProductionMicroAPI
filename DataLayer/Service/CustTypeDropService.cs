using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class CustTypeDropService : ICustTypeDropService
    {
        public List<CustTypeDrop> GetCustomerTypeDropdown()
        {
            List<CustTypeDrop> vList = new List<CustTypeDrop>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_DROPDOWN_CUSTTYPE"
                };

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    vList.Add(new CustTypeDrop
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CUST_NAME = Convert.ToString(dr["CUST_NAME"]),
                        CUST_TYPE = Convert.ToString(dr["CUST_TYPE"])
                    });
                }
            }
            return vList;
        }
    }
}
