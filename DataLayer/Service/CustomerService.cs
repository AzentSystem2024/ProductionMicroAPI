using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class CustomerService:ICustomerService
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> employeeList = new List<Customer>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_CUSTOMER";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    employeeList.Add(new Customer
                    {

                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        HQID = Convert.IsDBNull(dr["HQID"]) ? 0 : Convert.ToInt32(dr["HQID"]),
                        AC_HEAD_ID = Convert.IsDBNull(dr["AC_HEAD_ID"]) ? 0 : Convert.ToInt32(dr["AC_HEAD_ID"]),
                        FIRST_NAME = Convert.IsDBNull(dr["FIRST_NAME"]) ? null : Convert.ToString(dr["FIRST_NAME"]),
                        CONTACT_NAME = Convert.IsDBNull(dr["CONTACT_NAME"]) ? null : Convert.ToString(dr["CONTACT_NAME"]),
                        CUST_CODE = Convert.IsDBNull(dr["CUST_CODE"]) ? null : Convert.ToString(dr["CUST_CODE"]),
                        ADDRESS1 = Convert.IsDBNull(dr["ADDRESS1"]) ? null : Convert.ToString(dr["ADDRESS1"]),
                        ADDRESS2 = Convert.IsDBNull(dr["ADDRESS2"]) ? null : Convert.ToString(dr["ADDRESS2"]),
                        ADDRESS3 = Convert.IsDBNull(dr["ADDRESS3"]) ? null : Convert.ToString(dr["ADDRESS3"]),
                        ZIP = Convert.IsDBNull(dr["ZIP"]) ? null : Convert.ToString(dr["ZIP"]),
                        CITY = Convert.IsDBNull(dr["CITY"]) ? null : Convert.ToString(dr["CITY"]),
                        STATE_ID = Convert.IsDBNull(dr["STATE_ID"]) ? 0 : Convert.ToInt32(dr["STATE_ID"]),
                        COUNTRY_ID = Convert.IsDBNull(dr["COUNTRY_ID"]) ? 0 : Convert.ToInt32(dr["COUNTRY_ID"]),
                        PHONE = Convert.IsDBNull(dr["PHONE"]) ? null : Convert.ToString(dr["PHONE"]),
                        EMAIL = Convert.IsDBNull(dr["EMAIL"]) ? null : Convert.ToString(dr["EMAIL"]),
                        SALESMAN_ID = Convert.IsDBNull(dr["SALESMAN_ID"]) ? 0 : Convert.ToInt32(dr["SALESMAN_ID"]),
                        CREDIT_LIMIT = Convert.IsDBNull(dr["CREDIT_LIMIT"]) ? (float?)null : Convert.ToSingle(dr["CREDIT_LIMIT"]),
                        CURRENT_CREDIT = Convert.IsDBNull(dr["CURRENT_CREDIT"]) ? (float?)null : Convert.ToSingle(dr["CURRENT_CREDIT"]),
                        IS_BLOCKED = Convert.IsDBNull(dr["IS_BLOCKED"]) ? (bool?)null : Convert.ToBoolean(dr["IS_BLOCKED"]),
                        MOBILE_NO = Convert.IsDBNull(dr["MOBILE_NO"]) ? null : Convert.ToString(dr["MOBILE_NO"]),
                        FAX_NO = Convert.IsDBNull(dr["FAX_NO"]) ? null : Convert.ToString(dr["FAX_NO"]),
                        LAST_NAME = Convert.IsDBNull(dr["LAST_NAME"]) ? null : Convert.ToString(dr["LAST_NAME"]),
                        DOB = Convert.IsDBNull(dr["DOB"]) ? (DateTime?)null : Convert.ToDateTime(dr["DOB"]),
                        NATIONALITY = Convert.IsDBNull(dr["NATIONALITY"]) ? 0 : Convert.ToInt32(dr["NATIONALITY"]),
                        NOTES = Convert.IsDBNull(dr["NOTES"]) ? null : Convert.ToString(dr["NOTES"]),
                        CUST_NAME = Convert.IsDBNull(dr["CUST_NAME"]) ? null : Convert.ToString(dr["CUST_NAME"]),
                        CREDIT_DAYS = Convert.IsDBNull(dr["CREDIT_DAYS"]) ? 0 : Convert.ToInt32(dr["CREDIT_DAYS"]),
                        PAY_TERM_ID = Convert.IsDBNull(dr["PAY_TERM_ID"]) ? 0 : Convert.ToInt32(dr["PAY_TERM_ID"]),
                        PRICE_CLASS_ID = Convert.IsDBNull(dr["PRICE_CLASS_ID"]) ? 0 : Convert.ToInt32(dr["PRICE_CLASS_ID"]),
                        DISCOUNT_PERCENT = Convert.IsDBNull(dr["DISCOUNT_PERCENT"]) ? (float?)null : Convert.ToSingle(dr["DISCOUNT_PERCENT"]),
                        DOJ = Convert.IsDBNull(dr["DOJ"]) ? (DateTime?)null : Convert.ToDateTime(dr["DOJ"]),
                        COMPANY_ID = Convert.IsDBNull(dr["COMPANY_ID"]) ? 0 : Convert.ToInt32(dr["COMPANY_ID"]),
                        STORE_ID = Convert.IsDBNull(dr["STORE_ID"]) ? 0 : Convert.ToInt32(dr["STORE_ID"]),
                        CUST_VAT_RULE_ID = Convert.IsDBNull(dr["CUST_VAT_RULE_ID"]) ? 0 : Convert.ToInt32(dr["CUST_VAT_RULE_ID"]),
                        VAT_REGNO = Convert.IsDBNull(dr["VAT_REGNO"]) ? null : Convert.ToString(dr["VAT_REGNO"]),
                        IS_DELETED = Convert.IsDBNull(dr["IS_DELETED"]) ? (bool?)null : Convert.ToBoolean(dr["IS_DELETED"]),


                        LOYALTY_POINT = Convert.IsDBNull(dr["LOYALTY_POINT"]) ? (decimal?)null : Convert.ToDecimal(dr["LOYALTY_POINT"]),


                        STATE_NAME = Convert.IsDBNull(dr["STATE_NAME"]) ? null : Convert.ToString(dr["STATE_NAME"]),
                        COUNTRY_NAME = Convert.IsDBNull(dr["COUNTRY_NAME"]) ? null : Convert.ToString(dr["COUNTRY_NAME"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        CLASS_NAME = Convert.IsDBNull(dr["CLASS_NAME"]) ? null : Convert.ToString(dr["CLASS_NAME"]),
                        COMPANY_NAME = Convert.IsDBNull(dr["COMPANY_NAME"]) ? null : Convert.ToString(dr["COMPANY_NAME"]),
                        STORE_NAME = Convert.IsDBNull(dr["STORE_NAME"]) ? null : Convert.ToString(dr["STORE_NAME"]),
                        VAT_RULE_DESCRIPTION = Convert.IsDBNull(dr["VAT_RULE_DESCRIPTION"]) ? null : Convert.ToString(dr["VAT_RULE_DESCRIPTION"]),


                    });
                }
                connection.Close();
            }
            return employeeList;
        }

        public Int32 SaveData(Customer customer)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_CUSTOMER", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("ID", customer.ID > 0 ? customer.ID : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("HQID", customer.HQID);
                    cmd.Parameters.AddWithValue("AC_HEAD_ID", customer.AC_HEAD_ID);
                    cmd.Parameters.AddWithValue("CUST_CODE", string.IsNullOrEmpty(customer.CUST_CODE) ? (object)DBNull.Value : customer.CUST_CODE);
                    cmd.Parameters.AddWithValue("FIRST_NAME", customer.FIRST_NAME ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("CONTACT_NAME", customer.CONTACT_NAME ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ADDRESS1", customer.ADDRESS1 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ADDRESS2", customer.ADDRESS2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ADDRESS3", customer.ADDRESS3 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ZIP", customer.ZIP ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("CITY", customer.CITY ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("STATE_ID", customer.STATE_ID);
                    cmd.Parameters.AddWithValue("COUNTRY_ID", customer.COUNTRY_ID);
                    cmd.Parameters.AddWithValue("PHONE", customer.PHONE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("EMAIL", customer.EMAIL ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("SALESMAN_ID", customer.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("CREDIT_LIMIT", customer.CREDIT_LIMIT ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("CURRENT_CREDIT", customer.CURRENT_CREDIT ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_BLOCKED", customer.IS_BLOCKED ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("MOBILE_NO", customer.MOBILE_NO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("FAX_NO", customer.FAX_NO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LAST_NAME", customer.LAST_NAME ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("DOB", customer.DOB ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NATIONALITY", customer.NATIONALITY);
                    cmd.Parameters.AddWithValue("NOTES", customer.NOTES ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("CUST_NAME", customer.CUST_NAME ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("CREDIT_DAYS", customer.CREDIT_DAYS);
                    cmd.Parameters.AddWithValue("PAY_TERM_ID", customer.PAY_TERM_ID);
                    cmd.Parameters.AddWithValue("PRICE_CLASS_ID", customer.PRICE_CLASS_ID);
                    cmd.Parameters.AddWithValue("DISCOUNT_PERCENT", customer.DISCOUNT_PERCENT ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("DOJ", customer.DOJ ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("COMPANY_ID", customer.COMPANY_ID);
                    cmd.Parameters.AddWithValue("STORE_ID", customer.STORE_ID);
                    cmd.Parameters.AddWithValue("CUST_VAT_RULE_ID", customer.CUST_VAT_RULE_ID);
                    cmd.Parameters.AddWithValue("VAT_REGNO", customer.VAT_REGNO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IS_DELETED", customer.IS_DELETED ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LOYALTY_POINT", customer.LOYALTY_POINT ?? (object)DBNull.Value);

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        return Convert.ToInt32(result);
                    else
                        throw new Exception("Customer insert failed: No ID returned.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SaveData Error: " + ex.Message, ex);
            }
        }



        public Customer GetItems(int id)
        {
            Customer customer = new Customer();

            try
            {
                string strSQL = "SELECT TB_CUSTOMER.ID, TB_CUSTOMER.HQID, TB_CUSTOMER.AC_HEAD_ID, TB_CUSTOMER.FIRST_NAME," +
                   " TB_CUSTOMER.CONTACT_NAME, TB_CUSTOMER.CUST_CODE," +
                   " TB_CUSTOMER.ADDRESS1, TB_CUSTOMER.ADDRESS2, TB_CUSTOMER.ADDRESS3, TB_CUSTOMER.ZIP, " +
                   " TB_CUSTOMER.CITY, TB_CUSTOMER.STATE_ID, TB_CUSTOMER.COUNTRY_ID, TB_CUSTOMER.PHONE, " +
                   " TB_CUSTOMER.EMAIL, TB_CUSTOMER.SALESMAN_ID, TB_CUSTOMER.CREDIT_LIMIT, TB_CUSTOMER.CURRENT_CREDIT," +
                   " TB_CUSTOMER.IS_BLOCKED, TB_CUSTOMER.MOBILE_NO, TB_CUSTOMER.FAX_NO, TB_CUSTOMER.LAST_NAME, " +
                   " TB_CUSTOMER.DOB, TB_CUSTOMER.NATIONALITY, TB_CUSTOMER.NOTES, TB_CUSTOMER.CUST_NAME, " +
                   " TB_CUSTOMER.CREDIT_DAYS, TB_CUSTOMER.PAY_TERM_ID, TB_CUSTOMER.PRICE_CLASS_ID, TB_CUSTOMER.DISCOUNT_PERCENT," +
                   " TB_CUSTOMER.DOJ, TB_CUSTOMER.COMPANY_ID, TB_CUSTOMER.STORE_ID, TB_CUSTOMER.CUST_VAT_RULE_ID," +
                   " TB_CUSTOMER.VAT_REGNO, TB_CUSTOMER.IS_DELETED, TB_CUSTOMER.LOYALTY_POINT," +
                   " TB_STATE.STATE_NAME, TB_COUNTRY.COUNTRY_NAME, TB_EMPLOYEE.EMP_NAME, " +
                   " TB_PAYMENT_TERMS.CODE, TB_PRICE_CLASS.CLASS_NAME,   " +
                   " TB_COMPANY.COMPANY_NAME, TB_STORES.STORE_NAME," +
                   " TB_VAT_RULE_CUSTOMER.DESCRIPTION AS VAT_RULE_DESCRIPTION" +
                   " FROM TB_CUSTOMER " +
                   " LEFT JOIN TB_STATE ON TB_CUSTOMER.STATE_ID = TB_STATE.ID " +
                   " LEFT JOIN TB_COUNTRY ON TB_CUSTOMER.COUNTRY_ID = TB_COUNTRY.ID " +
                   " LEFT JOIN TB_COUNTRY AS TB_NATIONALITY ON TB_CUSTOMER.NATIONALITY = TB_NATIONALITY.ID " +
                   " LEFT JOIN TB_EMPLOYEE ON TB_CUSTOMER.SALESMAN_ID = TB_EMPLOYEE.ID " +
                   " LEFT JOIN TB_PAYMENT_TERMS ON TB_CUSTOMER.PAY_TERM_ID = TB_PAYMENT_TERMS.ID " +
                   " LEFT JOIN TB_PRICE_CLASS ON TB_CUSTOMER.PRICE_CLASS_ID = TB_PRICE_CLASS.ID " +
                   " LEFT JOIN TB_COMPANY ON TB_CUSTOMER.COMPANY_ID = TB_COMPANY.ID " +
                   " LEFT JOIN TB_STORES ON TB_CUSTOMER.STORE_ID = TB_STORES.ID " +
                   " LEFT JOIN TB_VAT_RULE_CUSTOMER ON TB_CUSTOMER.CUST_VAT_RULE_ID = TB_VAT_RULE_CUSTOMER.ID " +
                   " WHERE TB_CUSTOMER.ID = " + id;





                DataTable tbl = ADO.GetDataTable(strSQL, "Customer");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];


                    customer.ID = Convert.ToInt32(dr["ID"]);
                    customer.HQID = Convert.ToInt32(dr["HQID"]);
                    customer.AC_HEAD_ID = Convert.ToInt32(dr["AC_HEAD_ID"]);
                    customer.FIRST_NAME = Convert.ToString(dr["FIRST_NAME"]);
                    customer.CONTACT_NAME = Convert.ToString(dr["CONTACT_NAME"]);
                    customer.CUST_CODE = Convert.ToString(dr["CUST_CODE"]);
                    customer.ADDRESS1 = Convert.ToString(dr["ADDRESS1"]);
                    customer.ADDRESS2 = Convert.ToString(dr["ADDRESS2"]);
                    customer.ADDRESS3 = Convert.ToString(dr["ADDRESS3"]);
                    customer.ZIP = Convert.ToString(dr["ZIP"]);
                    customer.CITY = Convert.ToString(dr["CITY"]);
                    customer.STATE_ID = Convert.ToInt32(dr["STATE_ID"]);
                    customer.COUNTRY_ID = Convert.ToInt32(dr["COUNTRY_ID"]);
                    customer.PHONE = Convert.ToString(dr["PHONE"]);
                    customer.EMAIL = Convert.ToString(dr["EMAIL"]);
                    customer.SALESMAN_ID = Convert.ToInt32(dr["SALESMAN_ID"]);
                    customer.CREDIT_LIMIT = float.Parse(dr["CREDIT_LIMIT"].ToString());
                    customer.CURRENT_CREDIT = float.Parse(dr["CURRENT_CREDIT"].ToString());
                    customer.IS_BLOCKED = Convert.ToBoolean(dr["IS_BLOCKED"]);
                    customer.MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                    customer.FAX_NO = Convert.ToString(dr["FAX_NO"]);
                    customer.LAST_NAME = Convert.ToString(dr["LAST_NAME"]);
                    customer.DOB = Convert.ToDateTime(dr["DOB"]);
                    customer.NATIONALITY = Convert.ToInt32(dr["NATIONALITY"]);
                    customer.NOTES = Convert.ToString(dr["NOTES"]);
                    customer.CUST_NAME = Convert.ToString(dr["CUST_NAME"]);
                    customer.CREDIT_DAYS = Convert.ToInt32(dr["CREDIT_DAYS"]);
                    customer.PAY_TERM_ID = Convert.ToInt32(dr["PAY_TERM_ID"]);
                    customer.PRICE_CLASS_ID = Convert.ToInt32(dr["PRICE_CLASS_ID"]);
                    customer.DISCOUNT_PERCENT = float.Parse(dr["DISCOUNT_PERCENT"].ToString());
                    customer.DOJ = Convert.ToDateTime(dr["DOJ"]);
                    customer.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    customer.STORE_ID = Convert.ToInt32(dr["STORE_ID"]);
                    customer.CUST_VAT_RULE_ID = Convert.ToInt32(dr["CUST_VAT_RULE_ID"]);
                    customer.VAT_REGNO = Convert.ToString(dr["VAT_REGNO"]);
                    customer.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);
                    customer.LOYALTY_POINT = Convert.ToDecimal(dr["LOYALTY_POINT"]);

                    customer.STATE_NAME = Convert.ToString(dr["STATE_NAME"]);
                    customer.COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]);
                    customer.EMP_NAME = Convert.ToString(dr["EMP_NAME"]);
                    customer.CLASS_NAME = Convert.ToString(dr["CLASS_NAME"]);
                    customer.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    customer.STORE_NAME = Convert.ToString(dr["STORE_NAME"]);
                    customer.VAT_RULE_DESCRIPTION = Convert.ToString(dr["VAT_RULE_DESCRIPTION"]);

                }
            }
            catch (Exception ex)
            {

            }
            return customer;
        }

        public bool DeleteCustomers(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CUSTOMER";
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
    }
}
