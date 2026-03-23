using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class PaySettingsService:IPaySettingsService
    {
        public PaySettings GetPaySettings()
        {
            PaySettings settings = new PaySettings();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_PAY_SETTINGS"
                };

                cmd.Parameters.AddWithValue("ACTION", 0); // Select action
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    settings.DAILY_HOURS = dr["DAILY_HOURS"] as int?;
                    settings.MAX_OT_MTS = dr["MAX_OT_MTS"] as int?;
                    settings.NORMAL_OT_RATE = ADO.ToDecimal(dr["NORMAL_OT_RATE"]);
                    settings.HOLIDAY_OT_RATE = ADO.ToDecimal(dr["HOLIDAY_OT_RATE"]);
                    settings.LEAVE_SAL_DAYS = ADO.ToDecimal(dr["LEAVE_SAL_DAYS"]);
                    settings.UQ_LABOUR_ID = dr["UQ_LABOUR_ID"]?.ToString();
                    settings.BANK_AC_NO = dr["BANK_AC_NO"]?.ToString();
                    settings.BANK_CODE = dr["BANK_CODE"]?.ToString();
                    settings.SAL_EXPENSE_HEAD_ID = dr["SAL_EXPENSE_HEAD_ID"] as int?;
                    settings.SAL_PAYABLE_HEAD_ID = dr["SAL_PAYABLE_HEAD_ID"] as int?;
                    settings.LS_EXPENSE_HEAD_ID = dr["LS_EXPENSE_HEAD_ID"] as int?;
                    settings.LS_PAYABLE_HEAD_ID = dr["LS_PAYABLE_HEAD_ID"] as int?;
                    settings.EOS_EXPENSE_HEAD_ID = dr["EOS_EXPENSE_HEAD_ID"] as int?;
                    settings.EOS_PAYABLE_HEAD_ID = Convert.ToInt32(dr["EOS_PAYABLE_HEAD_ID"]);
                }

                connection.Close();
            }

            return settings;
        }

        public bool SavePaySettings(PaySettings settings)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_PAY_SETTINGS"
                    };

                    cmd.Parameters.AddWithValue("ACTION", 1); // Save action
                    cmd.Parameters.AddWithValue("DAILY_HOURS", (object?)settings.DAILY_HOURS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("MAX_OT_MTS", (object?)settings.MAX_OT_MTS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("NORMAL_OT_RATE", (object?)settings.NORMAL_OT_RATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("HOLIDAY_OT_RATE", (object?)settings.HOLIDAY_OT_RATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_SAL_DAYS", (object?)settings.LEAVE_SAL_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("UQ_LABOUR_ID", (object?)settings.UQ_LABOUR_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("BANK_AC_NO", (object?)settings.BANK_AC_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("BANK_CODE", (object?)settings.BANK_CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("SAL_EXPENSE_HEAD_ID", (object?)settings.SAL_EXPENSE_HEAD_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("SAL_PAYABLE_HEAD_ID", (object?)settings.SAL_PAYABLE_HEAD_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LS_EXPENSE_HEAD_ID", (object?)settings.LS_EXPENSE_HEAD_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LS_PAYABLE_HEAD_ID", (object?)settings.LS_PAYABLE_HEAD_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("EOS_EXPENSE_HEAD_ID", (object?)settings.EOS_EXPENSE_HEAD_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("EOS_PAYABLE_HEAD_ID", settings.EOS_PAYABLE_HEAD_ID);

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
