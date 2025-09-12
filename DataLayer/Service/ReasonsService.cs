using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ReasonsService:IReasonsService
    {
        public List<Reasons> GetAllReasons()
        {
            List<Reasons> productList = new List<Reasons>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_REASONS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    productList.Add(new Reasons
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        //COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]),
                        CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                        REASON_TYPE = Convert.ToInt32(dr["REASON_TYPE"]),
                        START_DATE = Convert.ToDateTime(dr["START_DATE"]),
                        END_DATE = Convert.ToDateTime(dr["END_DATE"]),
                        ARABIC_DESCRIPTION = Convert.ToString(dr["ARABIC_DESCRIPTION"]),
                        DISCOUNT_TYPE = Convert.ToInt32(dr["DISCOUNT_TYPE"]),
                        DISCOUNT_PERCENT = float.Parse(dr["DISCOUNT_PERCENT"].ToString()),
                        AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]),
                        //IS_DELETED=Convert.ToBoolean(dr["IS_DELETED"])
                    });
                }
                connection.Close();
            }
            return productList;
        }
        public bool Insert(Reasons reasons)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));

                if (reasons.reason_stores != null && reasons.reason_stores.Any())
                {
                    foreach (REASON_STORES ur in reasons.reason_stores)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["STORE_ID"] = ur.STORE_ID;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans; // Associate the command with the transaction
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_REASONS";

                cmd.Parameters.AddWithValue("ACTION", 1);

                cmd.Parameters.AddWithValue("ID", reasons.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", reasons.COMPANY_ID);
                cmd.Parameters.AddWithValue("CODE", reasons.CODE);
                cmd.Parameters.AddWithValue("DESCRIPTION", reasons.DESCRIPTION);
                cmd.Parameters.AddWithValue("REASON_TYPE", reasons.REASON_TYPE);
                cmd.Parameters.AddWithValue("START_DATE", reasons.START_DATE);
                cmd.Parameters.AddWithValue("END_DATE", reasons.END_DATE);
                cmd.Parameters.AddWithValue("ARABIC_DESCRIPTION", reasons.ARABIC_DESCRIPTION);
                cmd.Parameters.AddWithValue("DISCOUNT_TYPE", reasons.DISCOUNT_TYPE);
                cmd.Parameters.AddWithValue("DISCOUNT_PERCENT", reasons.DISCOUNT_PERCENT);
                cmd.Parameters.AddWithValue("AC_HEAD_ID", reasons.AC_HEAD_ID);

                cmd.Parameters.AddWithValue("@UDT_TB_REASON_STORES", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public bool Update(Reasons reasons)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("STORE_ID", typeof(Int32));

                if (reasons.reason_stores != null && reasons.reason_stores.Any())
                {
                    foreach (REASON_STORES ur in reasons.reason_stores)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["STORE_ID"] = ur.STORE_ID;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans; // Associate the command with the transaction
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_REASONS";

                cmd.Parameters.AddWithValue("ACTION", 3);

                cmd.Parameters.AddWithValue("ID", reasons.ID);
                cmd.Parameters.AddWithValue("COMPANY_ID", reasons.COMPANY_ID);
                cmd.Parameters.AddWithValue("CODE", reasons.CODE);
                cmd.Parameters.AddWithValue("DESCRIPTION", reasons.DESCRIPTION);
                cmd.Parameters.AddWithValue("REASON_TYPE", reasons.REASON_TYPE);
                cmd.Parameters.AddWithValue("START_DATE", reasons.START_DATE);
                cmd.Parameters.AddWithValue("END_DATE", reasons.END_DATE);
                cmd.Parameters.AddWithValue("ARABIC_DESCRIPTION", reasons.ARABIC_DESCRIPTION);
                cmd.Parameters.AddWithValue("DISCOUNT_TYPE", reasons.DISCOUNT_TYPE);
                cmd.Parameters.AddWithValue("DISCOUNT_PERCENT", reasons.DISCOUNT_PERCENT);
                cmd.Parameters.AddWithValue("AC_HEAD_ID", reasons.AC_HEAD_ID);
                cmd.Parameters.AddWithValue("@UDT_TB_REASON_STORES", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public bool DeleteReasons(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_REASONS";
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
        public Reasons GetItems(int id)
        {
            Reasons reasons = new Reasons();
            List<REASON_STORES> reasonstores = new List<REASON_STORES>();

            try
            {
                string strSQL = @"
            SELECT 
                R.ID,
                R.COMPANY_ID,
                R.CODE,
                R.DESCRIPTION,
                R.REASON_TYPE,
                R.START_DATE,
                R.END_DATE,
                R.ARABIC_DESCRIPTION,
                R.DISCOUNT_TYPE,
                R.DISCOUNT_PERCENT,
                R.IS_DELETED,
                C.COMPANY_NAME,
                RT.DESCRIPTION AS REASON_TYPE_NAME,
                DT.DISCOUNT_TYPE AS DISCOUNT_TYPE_NAME,
                R.AC_HEAD_ID
            FROM TB_REASONS R
            LEFT JOIN TB_COMPANY C ON R.COMPANY_ID = C.ID
            LEFT JOIN TB_REASON_TYPES RT ON R.REASON_TYPE = RT.ID
            LEFT JOIN TB_DISCOUNT_TYPES DT ON R.DISCOUNT_TYPE = DT.ID
            WHERE R.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Products");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    reasons.ID = Convert.ToInt32(dr["ID"]);
                    reasons.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    reasons.CODE = Convert.ToString(dr["CODE"]);
                    reasons.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                    reasons.REASON_TYPE = Convert.ToInt32(dr["REASON_TYPE"]);
                    reasons.START_DATE = Convert.ToDateTime(dr["START_DATE"]);
                    reasons.END_DATE = Convert.ToDateTime(dr["END_DATE"]);
                    reasons.ARABIC_DESCRIPTION = Convert.ToString(dr["ARABIC_DESCRIPTION"]);
                    reasons.DISCOUNT_TYPE = Convert.ToInt32(dr["DISCOUNT_TYPE"]);
                    reasons.DISCOUNT_PERCENT = float.Parse(dr["DISCOUNT_PERCENT"].ToString());
                    reasons.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);
                    reasons.AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]);

                    strSQL = @"
                SELECT 
                    RS.ID,
                    RS.STORE_ID,
                    S.STORE_NAME
                FROM TB_REASON_STORES RS
                INNER JOIN TB_STORES S ON RS.STORE_ID = S.ID
                WHERE RS.REASON_ID = " + id + " AND S.IS_DELETED = 0";

                    DataTable tblDetail = ADO.GetDataTable(strSQL, "ReasonStores");

                    if (tblDetail.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in tblDetail.Rows)
                        {
                            reasonstores.Add(new REASON_STORES
                            {
                                ID = Convert.ToInt32(dr1["ID"]),
                                STORE_ID = Convert.ToString(dr1["STORE_ID"]), 
                            });
                        }
                    }

                    reasons.reason_stores = reasonstores;
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
            }

            return reasons;
        }


    }
}
