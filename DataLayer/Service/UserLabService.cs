using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class UserLabService:IUserLabService
    {
        public List<UserLab> GetAllUsers()
        {
            List<UserLab> userList = new List<UserLab>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_LIST";
                cmd.Parameters.AddWithValue("ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<Clinician> without explicit loop
                userList = tbl.AsEnumerable().Select(dr => new UserLab
                {
                    USER_ID = ADO.ToInt32(dr["USER_ID"]),
                    DEPT_ID = ADO.ToInt32(dr["DEPT_ID"]),
                    USER_NAME = ADO.ToString(dr["USER_NAME"]),
                    LOGIN_NAME = ADO.ToString(dr["LOGIN_NAME"]),
                    // PASSWORD = ADO.ToString(dr["PASSWORD"]),
                    PASSWORD = AzentLibrary.Library.DecryptString(ADO.ToString(dr["PASSWORD"])),
                    IS_ADMIN = ADO.Toboolean(dr["IS_ADMIN"]),
                    IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"])
                }).ToList();
            }
            finally
            {
                connection.Close();
            }
            return userList;
        }
        public UserLabLoginResponse VerifyLogin(UserLabVerificationInput vLoginInput)
        {
            var result = new UserLabLoginResponse();

            try
            {
                using (var objCon = ADO.GetConnection())
                using (var cmd = new SqlCommand("SP_VERIFY_LOGIN", objCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LOGIN_NAME", vLoginInput.LOGIN_NAME);
                    cmd.Parameters.AddWithValue("@PASSWORD", AzentLibrary.Library.EncryptString(vLoginInput.PASSWORD));

                    using (var sqlDA = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        sqlDA.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result.flag = Convert.ToInt32(ds.Tables[0].Rows[0]["FLAG"]);
                            result.message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                            if (result.flag == 1 && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            {
                                var row = ds.Tables[1].Rows[0];
                                result.data = new UserLab
                                {
                                    USER_ID = Convert.ToInt32(row["USER_ID"]),
                                    USER_NAME = row["USER_NAME"].ToString(),
                                    LOGIN_NAME = row["LOGIN_NAME"].ToString(),
                                    IS_INACTIVE = Convert.ToBoolean(row["IS_INACTIVE"]),
                                    IS_ADMIN = Convert.ToBoolean(row["IS_ADMIN"]),
                                    IS_LAB_USER = Convert.ToBoolean(row["IS_LAB_USER"]),
                                    IS_COLLECTION_USER = Convert.ToBoolean(row["IS_COLLECTION_USER"]),
                                    IS_VERIFY_REPORT = Convert.ToBoolean(row["IS_VERIFY_REPORT"])
                                };
                            }
                        }
                        else
                        {
                            result.flag = 0;
                            result.message = "No data available.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.flag = 0;
                result.message = ex.Message;
            }

            return result;
        }
    }
}
