using MicroApi.DataLayer.Service;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class SupplierService:ISupplierService
    {
        public List<Suppliers> GetAllSuppliers()
        {
            List<Suppliers> supplierList = new List<Suppliers>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_SUPPLIER", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    foreach (DataRow dr in tbl.Rows)
                    {
                        Suppliers supplier = new Suppliers
                        {
                            ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : null,
                            HQID = dr["HQID"] != DBNull.Value ? Convert.ToInt32(dr["HQID"]) : null,
                            AC_HEAD_ID = dr["AC_HEAD_ID"] != DBNull.Value ? Convert.ToInt32(dr["AC_HEAD_ID"]) : null,
                            SUPP_CODE = dr["SUPP_CODE"]?.ToString(),
                            SUPP_NAME = dr["SUPP_NAME"]?.ToString(),
                            CONTACT_NAME = dr["CONTACT_NAME"]?.ToString(),
                            ADDRESS1 = dr["ADDRESS1"]?.ToString(),
                            ADDRESS2 = dr["ADDRESS2"]?.ToString(),
                            ADDRESS3 = dr["ADDRESS3"]?.ToString(),
                            ZIP = dr["ZIP"]?.ToString(),
                            STATE_ID = dr["STATE_ID"] != DBNull.Value ? Convert.ToInt32(dr["STATE_ID"]) : null,
                            CITY = dr["CITY"]?.ToString(),
                            COUNTRY_ID = dr["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COUNTRY_ID"]) : null,
                            PHONE = dr["PHONE"]?.ToString(),
                            EMAIL = dr["EMAIL"]?.ToString(),
                            IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value ? Convert.ToBoolean(dr["IS_INACTIVE"]) : null,
                            MOBILE_NO = dr["MOBILE_NO"]?.ToString(),
                            NOTES = dr["NOTES"]?.ToString(),
                            FAX_NO = dr["FAX_NO"]?.ToString(),
                            VAT_REGNO = dr["VAT_REGNO"]?.ToString(),
                            CURRENCY_ID = dr["CURRENCY_ID"] != DBNull.Value ? Convert.ToInt32(dr["CURRENCY_ID"]) : null,
                            PAY_TERM_ID = dr["PAY_TERM_ID"] != DBNull.Value ? Convert.ToInt32(dr["PAY_TERM_ID"]) : null,
                            VAT_RULE_ID = dr["VAT_RULE_ID"] != DBNull.Value ? Convert.ToInt32(dr["VAT_RULE_ID"]) : null,
                            COUNTRY_NAME = dr["COUNTRY_NAME"]?.ToString(),
                            CURRENCY_CODE = dr["CURRENCY_CODE"]?.ToString(),
                            PAYMENT_CODE = dr["PAYMENT_CODE"]?.ToString(),
                            DESCRIPTION = dr["DESCRIPTION"]?.ToString(),
                            STATE_NAME = dr["STATE_NAME"]?.ToString(),
                            IS_DELETED = dr.Table.Columns.Contains("IS_DELETED") ? dr["IS_DELETED"]?.ToString() : null,

                            // Always initialize the list to avoid null reference exceptions
                            Supplier_cost = new List<SupplierCost>()
                        };

                        supplierList.Add(supplier);
                    }
                }
            }

            return supplierList;
        }

        public bool SaveData(Suppliers supplier)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("SUPP_ID", typeof(Int32));
                tbl.Columns.Add("COST_ID", typeof(Int32));

                foreach (SupplierCost ur in supplier.Supplier_cost)
                {
                    DataRow dRow = tbl.NewRow();


                    dRow["SUPP_ID"] = ur.SUPP_ID;
                    dRow["COST_ID"] = ur.COST_ID;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_SUPPLIER";

                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("ID", supplier.ID);
                cmd.Parameters.AddWithValue("HQID", supplier.HQID);
                cmd.Parameters.AddWithValue("COMPANY_ID", (object)supplier.COMPANY_ID ?? DBNull.Value);
                //cmd.Parameters.AddWithValue("ID", (object)suppliers.ID ?? DBNull.Value);
                //cmd.Parameters.AddWithValue("HQID", (object)suppliers.HQID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SUPP_CODE", (object)supplier.SUPP_CODE ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SUPP_NAME", (object)supplier.SUPP_NAME ?? DBNull.Value);
                cmd.Parameters.AddWithValue("CONTACT_NAME", (object)supplier.CONTACT_NAME ?? DBNull.Value);
                cmd.Parameters.AddWithValue("ADDRESS1", (object)supplier.ADDRESS1 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("ADDRESS2", (object)supplier.ADDRESS2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("ADDRESS3", (object)supplier.ADDRESS3 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("ZIP", (object)supplier.ZIP ?? DBNull.Value);
                cmd.Parameters.AddWithValue("CITY", (object)supplier.CITY ?? DBNull.Value);
                cmd.Parameters.AddWithValue("STATE_ID", (object)supplier.STATE_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("COUNTRY_ID", (object)supplier.COUNTRY_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("PHONE", (object)supplier.PHONE ?? DBNull.Value);
                cmd.Parameters.AddWithValue("EMAIL", (object)supplier.EMAIL ?? DBNull.Value);
                cmd.Parameters.AddWithValue("IS_INACTIVE", (object)supplier.IS_INACTIVE ?? DBNull.Value);
                cmd.Parameters.AddWithValue("MOBILE_NO", (object)supplier.MOBILE_NO ?? DBNull.Value);
                cmd.Parameters.AddWithValue("FAX_NO", (object)supplier.FAX_NO ?? DBNull.Value);
                cmd.Parameters.AddWithValue("NOTES", (object)supplier.NOTES ?? DBNull.Value);
                cmd.Parameters.AddWithValue("CURRENCY_ID", (object)supplier.CURRENCY_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("PAY_TERM_ID", (object)supplier.PAY_TERM_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("VAT_RULE_ID", (object)supplier.VAT_RULE_ID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("VAT_REGNO", (object)supplier.VAT_REGNO ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@UDT_TB_SUPPLIER_COST", tbl);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                connection.Close();
                return true;

                //Int32 CountryID = Convert.ToInt32(cmd.ExecuteScalar());



                //return CountryID;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        public Suppliers GetItems(int id)
        {
            Suppliers supplier = new Suppliers();
            List<SupplierCost> supplierCosts = new List<SupplierCost>();

            try
            {
                string strSQL = "SELECT TB_SUPPLIER.ID, TB_SUPPLIER.HQID, TB_SUPPLIER.AC_HEAD_ID, TB_SUPPLIER.SUPP_CODE, TB_SUPPLIER.SUPP_NAME, " +
                   "TB_SUPPLIER.CONTACT_NAME, TB_SUPPLIER.ADDRESS1, TB_SUPPLIER.ADDRESS2, TB_SUPPLIER.ADDRESS3, TB_SUPPLIER.ZIP, " +
                   "TB_SUPPLIER.CITY, TB_SUPPLIER.PHONE, TB_SUPPLIER.EMAIL, TB_SUPPLIER.IS_INACTIVE, TB_SUPPLIER.MOBILE_NO, " +
                   "TB_SUPPLIER.NOTES, TB_SUPPLIER.FAX_NO, TB_SUPPLIER.VAT_REGNO,TB_SUPPLIER.IS_DELETED," +
                   "TB_SUPPLIER.COUNTRY_ID,TB_SUPPLIER.CURRENCY_ID,TB_SUPPLIER.PAY_TERM_ID,TB_SUPPLIER.VAT_RULE_ID," +
                   "TB_SUPPLIER.STATE_ID," +

                   "TB_COUNTRY.COUNTRY_NAME,TB_CURRENCY.CODE AS CURRENCY_CODE,TB_PAYMENT_TERMS.CODE AS PAYMENT_CODE," +
                   "TB_VAT_RULE_SUPPLIER.DESCRIPTION ,TB_STATE.STATE_NAME " +
                   "FROM TB_SUPPLIER " +

                   "LEFT JOIN TB_COUNTRY ON TB_SUPPLIER.COUNTRY_ID = TB_COUNTRY.ID " +
                   "LEFT JOIN TB_CURRENCY ON TB_SUPPLIER.CURRENCY_ID = TB_CURRENCY.ID " +
                   "LEFT JOIN TB_PAYMENT_TERMS ON TB_SUPPLIER.PAY_TERM_ID = TB_PAYMENT_TERMS.ID " +
                   "LEFT JOIN TB_VAT_RULE_SUPPLIER ON TB_SUPPLIER.VAT_RULE_ID = TB_VAT_RULE_SUPPLIER.ID " +
                   "LEFT JOIN TB_STATE ON TB_SUPPLIER.STATE_ID = TB_STATE.ID " +

                   "WHERE TB_SUPPLIER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Suppliers");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    supplier.ID = ADO.ToInt32(dr["ID"]);
                    supplier.HQID = ADO.ToInt32(dr["HQID"]);
                    supplier.AC_HEAD_ID = ADO.ToInt32(dr["AC_HEAD_ID"]);
                    supplier.SUPP_CODE = Convert.ToString(dr["SUPP_CODE"]);
                    supplier.SUPP_NAME = Convert.ToString(dr["SUPP_NAME"]);
                    supplier.CONTACT_NAME = Convert.ToString(dr["CONTACT_NAME"]);
                    supplier.ADDRESS1 = Convert.ToString(dr["ADDRESS1"]);
                    supplier.ADDRESS2 = Convert.ToString(dr["ADDRESS2"]);
                    supplier.ADDRESS3 = Convert.ToString(dr["ADDRESS3"]);
                    supplier.ZIP = Convert.ToString(dr["ZIP"]);
                    supplier.CITY = Convert.ToString(dr["CITY"]);
                    supplier.STATE_ID = ADO.ToInt32(dr["STATE_ID"]);
                    supplier.COUNTRY_ID = ADO.ToInt32(dr["COUNTRY_ID"]);
                    supplier.PHONE = Convert.ToString(dr["PHONE"]);
                    supplier.EMAIL = Convert.ToString(dr["EMAIL"]);
                    supplier.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);
                    supplier.MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                    supplier.NOTES = Convert.ToString(dr["NOTES"]);
                    supplier.FAX_NO = Convert.ToString(dr["FAX_NO"]);
                    supplier.VAT_REGNO = Convert.ToString(dr["VAT_REGNO"]);
                    supplier.IS_DELETED = Convert.ToString(dr["IS_DELETED"]);
                    supplier.CURRENCY_ID = ADO.ToInt32(dr["CURRENCY_ID"]);
                    supplier.CURRENCY_CODE = Convert.ToString(dr["CURRENCY_CODE"]);
                    supplier.PAY_TERM_ID = ADO.ToInt32(dr["PAY_TERM_ID"]);
                    supplier.PAYMENT_CODE = Convert.ToString(dr["PAYMENT_CODE"]);
                    supplier.VAT_RULE_ID = ADO.ToInt32(dr["VAT_RULE_ID"]);
                    supplier.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                    supplier.STATE_NAME = Convert.ToString(dr["STATE_NAME"]);
                    supplier.COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]);
                }

                strSQL = "SELECT * FROM TB_SUPPLIER_COSTS WHERE SUPP_ID = " + id;
                DataTable tblItemComponent = ADO.GetDataTable(strSQL, "Supplier Cost");

                foreach (DataRow dr3 in tblItemComponent.Rows)
                {
                    supplierCosts.Add(new SupplierCost
                    {
                        SUPP_ID = ADO.ToInt32(dr3["SUPP_ID"]),
                        COST_ID = ADO.ToInt32(dr3["COST_ID"])
                    });
                }

                supplier.Supplier_cost = supplierCosts;
            }
            catch (Exception ex)
            {
                // Optional: log error
            }

            return supplier;
        }



        public bool DeleteSupplier(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_SUPPLIER"
                };
                cmd.Parameters.AddWithValue("ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();
                return true; // Deletion succeeded

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("This supplier is used in a purchase order"))
                {
                    // Handle the specific error message
                    Console.WriteLine("This supplier is used in a purchase order and cannot be deleted.");
                    return false;
                }
                else
                {
                    // Rethrow any other SQL exceptions
                    throw;
                }
            }
        }


        //public bool DeleteSupplier(int id)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = ADO.GetConnection())
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "SP_TB_SUPPLIER";
        //            cmd.Parameters.AddWithValue("ACTION", 4);
        //            cmd.Parameters.AddWithValue("@ID", id);
        //            cmd.ExecuteNonQuery();

        //            connection.Close();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
