using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace MicroApi.DataLayer.Service
{
        public class DropDownService : IDropDownService
        {
            public List<DropDown> GetDropDownData(string vName)
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

                    cmd.Parameters.AddWithValue("@NAME", vName);

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
