using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace MicroApi.DataLayer.Service
{
        public class DropDownService : IDropDownService
        {
        public List<DropDown> GetDropDownData(DropDownInput input)
        {
            List<DropDown> vList = new List<DropDown>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_GET_DROPDOWN_DATA"
                };

                cmd.Parameters.AddWithValue("@NAME", input.NAME);
                cmd.Parameters.AddWithValue("@COMPANY_ID", (object)input.COMPANY_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PAGE_NUMBER", input.PAGE_NUMBER);
                cmd.Parameters.AddWithValue("@PAGE_SIZE", input.PAGE_SIZE);

                if (input.NAME == "STATE_NAME")
                    cmd.Parameters.AddWithValue("@COUNTRY_ID", input.COUNTRY_ID);

                if (input.NAME == "DISTRICT_NAME")
                    cmd.Parameters.AddWithValue("@STATE_ID", input.STATE_ID);

                if (input.NAME == "CITY_NAME")
                    cmd.Parameters.AddWithValue("@DISTRICT_ID", input.DISTRICT_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    vList.Add(new DropDown
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"])
                    });
                }
                connection.Close();
            }

            return vList;
        }


    }
}
