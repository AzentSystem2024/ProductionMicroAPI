using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class EOSService:IEOSService
    {
        public List<EosReason> GetAllEosReasons()
        {
            try
            {
                List<EosReason> eosReasonList = new List<EosReason>();

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_EOS_REASON"
                    };
                    cmd.Parameters.AddWithValue("ACTION", 0);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    foreach (DataRow dr in tbl.Rows)
                    {
                        eosReasonList.Add(new EosReason
                        {
                            ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                            CODE = Convert.IsDBNull(dr["CODE"]) ? null : Convert.ToString(dr["CODE"]),
                            DESCRIPTION = Convert.IsDBNull(dr["DESCRIPTION"]) ? null : Convert.ToString(dr["DESCRIPTION"]),
                            COMPANY_ID = Convert.IsDBNull(dr["COMPANY_ID"]) ? 0 : Convert.ToInt32(dr["COMPANY_ID"]),
                            COMPANY_NAME = dr["COMPANY_NAME"] as string,
                            IS_INACTIVE = Convert.IsDBNull(dr["IS_INACTIVE"]) ? false : Convert.ToBoolean(dr["IS_INACTIVE"]),
                            //IS_DELETED = Convert.IsDBNull(dr["IS_DELETED"]) ? false : Convert.ToBoolean(dr["IS_DELETED"])
                        });
                    }
                }
                return eosReasonList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int SaveEosReason(EosReason eosReason)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_EOS_REASON"
                    };

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", (object)eosReason.ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("CODE", (object)eosReason.CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DESCRIPTION", (object)eosReason.DESCRIPTION ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("COMPANY_ID", (object)eosReason.COMPANY_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_SYSTEM", (object)eosReason.IS_SYSTEM ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", (object)eosReason.IS_INACTIVE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_DELETED", (object)eosReason.IS_DELETED ?? DBNull.Value);

                    int eosReasonID = Convert.ToInt32(cmd.ExecuteScalar());
                    return eosReasonID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EosReason GetEosReasonById(int id)
        {
            EosReason eosReason = new EosReason();
            try
            {
                string strSQL = "SELECT * FROM TB_EOS_REASON WHERE ID = " + id;
                DataTable tbl = ADO.GetDataTable(strSQL, "EosReason");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    eosReason.ID = Convert.ToInt32(dr["ID"]);
                    eosReason.CODE = Convert.ToString(dr["CODE"]);
                    eosReason.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                    eosReason.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    //eosReason.IS_SYSTEM = Convert.ToBoolean(dr["IS_SYSTEM"]);
                    eosReason.IS_SYSTEM = Convert.IsDBNull(dr["IS_SYSTEM"]) ? false : Convert.ToBoolean(dr["IS_SYSTEM"]);
                    eosReason.IS_INACTIVE = Convert.IsDBNull(dr["IS_INACTIVE"]) ? false : Convert.ToBoolean(dr["IS_INACTIVE"]);
                    //eosReason.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);
                    eosReason.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eosReason;
        }

        public bool DeleteEosReason(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_EOS_REASON"
                    };
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("ID", id);
                    cmd.ExecuteNonQuery();
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
