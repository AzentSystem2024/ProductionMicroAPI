using MicroApi.DataLayer.Interface;
using System.Data;
using System.Data.SqlClient;
using MicroApi.Models;
using MicroApi.Helper;

namespace MicroApi.DataLayer.Services
{
    public class DenialMasterService:IDenialMasterService
    {
        public IEnumerable<DenialMaster> GetAllDenial(int intUserID)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand("SP_TB_DENIAL_MASTER", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 120
            };

            cmd.Parameters.Add("@ACTION", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = intUserID;
            SqlDataReader reader = cmd.ExecuteReader(); // NO SequentialAccess

            try
            {
                while (reader.Read())
                {
                    yield return new DenialMaster
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        DenialCode = reader["DenialCode"].ToString(),
                        DenialCategoryID = reader.GetInt32(reader.GetOrdinal("DenialCategoryID")),
                        DenialCategory = reader["DenialCategory"].ToString(),
                        DenialTypeID = reader.GetInt32(reader.GetOrdinal("DenialTypeID")),
                        DenialType = reader["DenialType"].ToString(),
                        Description = reader["Description"].ToString(),
                        DenialName = reader["DenialName"].ToString(),
                        IsInactive = reader.GetBoolean(reader.GetOrdinal("IsInactive"))
                    };
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
        }

        public Int32 Insert(DenialMaster denialMaster, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("DenialCode", denialMaster.DenialCode);
                    cmd.Parameters.AddWithValue("Description", denialMaster.Description);
                    cmd.Parameters.AddWithValue("DenialCategoryID", denialMaster.DenialCategoryID);
                    cmd.Parameters.AddWithValue("DenialTypeID", denialMaster.DenialTypeID);
                    cmd.Parameters.AddWithValue("IsInactive", denialMaster.IsInactive);
                    cmd.Parameters.AddWithValue("DenialName", denialMaster.DenialName);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_MASTER";
                    Int32 DenialID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DenialMaster> GetItems(int id)
        {
            List<DenialMaster> denialmaster = new List<DenialMaster>();

            try
            {
                string strSQL = "SELECT TB_DENIAL_MASTER.ID,TB_DENIAL_MASTER.DenialCode, TB_DENIAL_MASTER.Description, " +
                    "TB_DENIAL_MASTER.DenialName, TB_DENIAL_MASTER.DenialTypeID, TB_DENIAL_MASTER.DenialCategoryID, TB_DENIAL_CATEGORY.DenialCategory, " +
                    "TB_DENIAL_TYPES.DenialType " +
                "FROM TB_DENIAL_MASTER " +
                "INNER JOIN TB_DENIAL_TYPES ON TB_DENIAL_MASTER.DenialTypeID = TB_DENIAL_TYPES.ID " +
                "INNER JOIN TB_DENIAL_CATEGORY ON TB_DENIAL_MASTER.DenialCategoryID = TB_DENIAL_CATEGORY.ID " +
                "WHERE TB_DENIAL_MASTER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "DenialMaster");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    denialmaster.Add(new DenialMaster
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        DenialCode = dr["DenialCode"].ToString(),
                        DenialCategoryID = Convert.ToInt32(dr["DenialCategoryID"]),
                        DenialCategory = dr["DenialCategory"].ToString(),
                        DenialTypeID = Convert.ToInt32(dr["DenialTypeID"]),
                        DenialType = dr["DenialType"].ToString(),
                        Description = dr["Description"].ToString(),
                        DenialName = dr["DenialName"].ToString()

                    });

                }
            }
            catch (Exception ex)
            {

            }

            return denialmaster;
        }
        public Int32 Update(DenialMaster denialMaster, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", denialMaster.ID);
                    cmd.Parameters.AddWithValue("DenialCode", denialMaster.DenialCode);
                    cmd.Parameters.AddWithValue("Description", denialMaster.Description);
                    cmd.Parameters.AddWithValue("DenialCategoryID", denialMaster.DenialCategoryID);
                    cmd.Parameters.AddWithValue("DenialTypeID", denialMaster.DenialTypeID);
                    cmd.Parameters.AddWithValue("IsInactive", denialMaster.IsInactive);
                    cmd.Parameters.AddWithValue("DenialName", denialMaster.DenialName);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_MASTER";
                    Int32 DenialID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteDenialMaster(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

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

