using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class PayTimeEntryService : IPayTimeEntryService
    {
        public PayTimeResponse Save(PayTimeEntry pay)
        {
            try
            {
                PayTimeResponse res = new PayTimeResponse();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PAYTIME_ENTRY";

                    cmd.Parameters.AddWithValue("@ACTION", 1);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);

                    cmd.Parameters.AddWithValue("@COMP_ID ", pay.COMP_ID);
                    cmd.Parameters.AddWithValue("@SAL_MONTH", (object)pay.SAL_MONTH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)pay.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HEAD_ID", (object)pay.HEAD_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AMOUNT", (object)pay.AMOUNT ?? DBNull.Value);
                    //cmd.Parameters.AddWithValue("@EXG_TEAM_ID", (object)pay.EXG_TEAM_ID ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
