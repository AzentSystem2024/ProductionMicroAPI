using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MicroApi.DataLayer.Services
{
    public class CustomerService:ICustomerService
    {
        public List<CustomerUpdate> GetAllCustomers(CustomerListReq request)
        {
            List<CustomerUpdate> employeeList = new List<CustomerUpdate>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_CUSTOMER";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    employeeList.Add(new CustomerUpdate
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
                        //DOB = Convert.IsDBNull(dr["DOB"]) ? null : Convert.ToString(dr["DOB"]),
                        //NATIONALITY = Convert.IsDBNull(dr["NATIONALITY"]) ? 0 : Convert.ToInt32(dr["NATIONALITY"]),
                        NOTES = Convert.IsDBNull(dr["NOTES"]) ? null : Convert.ToString(dr["NOTES"]),
                        CUST_NAME = Convert.IsDBNull(dr["CUST_NAME"]) ? null : Convert.ToString(dr["CUST_NAME"]),
                        CREDIT_DAYS = Convert.IsDBNull(dr["CREDIT_DAYS"]) ? 0 : Convert.ToInt32(dr["CREDIT_DAYS"]),
                        PAY_TERM_ID = Convert.IsDBNull(dr["PAY_TERM_ID"]) ? 0 : Convert.ToInt32(dr["PAY_TERM_ID"]),
                        PRICE_CLASS_ID = Convert.IsDBNull(dr["PRICE_CLASS_ID"]) ? 0 : Convert.ToInt32(dr["PRICE_CLASS_ID"]),
                        DISCOUNT_PERCENT = Convert.IsDBNull(dr["DISCOUNT_PERCENT"]) ? (float?)null : Convert.ToSingle(dr["DISCOUNT_PERCENT"]),
                        DOJ = Convert.IsDBNull(dr["DOJ"]) ? null : Convert.ToString(dr["DOJ"]),
                        COMPANY_ID = Convert.IsDBNull(dr["COMPANY_ID"]) ? 0 : Convert.ToInt32(dr["COMPANY_ID"]),
                        STORE_ID = Convert.IsDBNull(dr["STORE_ID"]) ? 0 : Convert.ToInt32(dr["STORE_ID"]),
                        CUST_VAT_RULE_ID = Convert.IsDBNull(dr["CUST_VAT_RULE_ID"]) ? 0 : Convert.ToInt32(dr["CUST_VAT_RULE_ID"]),
                        VAT_REGNO = Convert.IsDBNull(dr["VAT_REGNO"]) ? null : Convert.ToString(dr["VAT_REGNO"]),
                        IS_DELETED = Convert.IsDBNull(dr["IS_DELETED"]) ? (bool?)null : Convert.ToBoolean(dr["IS_DELETED"]),
                        IS_COMPANY_BRANCH = Convert.IsDBNull(dr["IS_COMPANY_BRANCH"]) ? 0 : Convert.ToInt32(dr["IS_COMPANY_BRANCH"]),

                        LOYALTY_POINT = Convert.IsDBNull(dr["LOYALTY_POINT"]) ? (decimal?)null : Convert.ToDecimal(dr["LOYALTY_POINT"]),


                        STATE_NAME = Convert.IsDBNull(dr["STATE_NAME"]) ? null : Convert.ToString(dr["STATE_NAME"]),
                        COUNTRY_NAME = Convert.IsDBNull(dr["COUNTRY_NAME"]) ? null : Convert.ToString(dr["COUNTRY_NAME"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        CLASS_NAME = Convert.IsDBNull(dr["CLASS_NAME"]) ? null : Convert.ToString(dr["CLASS_NAME"]),
                        COMPANY_NAME = Convert.IsDBNull(dr["COMPANY_NAME"]) ? null : Convert.ToString(dr["COMPANY_NAME"]),
                        STORE_NAME = Convert.IsDBNull(dr["STORE_NAME"]) ? null : Convert.ToString(dr["STORE_NAME"]),
                        VAT_RULE_DESCRIPTION = Convert.IsDBNull(dr["VAT_RULE_DESCRIPTION"]) ? null : Convert.ToString(dr["VAT_RULE_DESCRIPTION"]),
                        CUST_TYPE = Convert.IsDBNull(dr["CUST_TYPE"]) ? 0 : Convert.ToInt32(dr["CUST_TYPE"]),
                        DEALER_ID = Convert.IsDBNull(dr["DEALER_ID"]) ? 0 : Convert.ToInt32(dr["DEALER_ID"]),
                       // WAREHOUSE_ID = Convert.IsDBNull(dr["WAREHOUSE_ID"]) ? 0 : Convert.ToInt32(dr["WAREHOUSE_ID"])


                    });
                }
                connection.Close();
            }
            return employeeList;
        }

        public int SaveData(Customer customer)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_CUSTOMER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 1); // INSERT

                        // Scalar Parameters
                        cmd.Parameters.AddWithValue("@HQID", customer.HQID ?? 0);
                        cmd.Parameters.AddWithValue("@AC_HEAD_ID", customer.AC_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUST_CODE", customer.CUST_CODE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@FIRST_NAME", customer.FIRST_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", customer.CONTACT_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS1", customer.ADDRESS1 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS2", customer.ADDRESS2 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS3", customer.ADDRESS3 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ZIP", customer.ZIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CITY", customer.CITY ?? string.Empty);
                        cmd.Parameters.AddWithValue("@STATE_ID", customer.STATE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", customer.COUNTRY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PHONE", customer.PHONE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@EMAIL", customer.EMAIL ?? string.Empty);
                        cmd.Parameters.AddWithValue("@SALESMAN_ID", customer.SALESMAN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CREDIT_LIMIT", customer.CREDIT_LIMIT ?? 0);
                        cmd.Parameters.AddWithValue("@CURRENT_CREDIT", customer.CURRENT_CREDIT ?? 0);
                        cmd.Parameters.AddWithValue("@IS_BLOCKED", customer.IS_BLOCKED ?? false);
                        cmd.Parameters.AddWithValue("@MOBILE_NO", customer.MOBILE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@FAX_NO", customer.FAX_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@LAST_NAME", customer.LAST_NAME ?? string.Empty);
                        //cmd.Parameters.AddWithValue("@DOB", ParseDate(customer.DOB));
                        //cmd.Parameters.AddWithValue("@NATIONALITY", customer.NATIONALITY ?? 0);
                        cmd.Parameters.AddWithValue("@NOTES", customer.NOTES ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CUST_NAME", customer.CUST_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREDIT_DAYS", customer.CREDIT_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TERM_ID", customer.PAY_TERM_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PRICE_CLASS_ID", customer.PRICE_CLASS_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DISCOUNT_PERCENT", customer.DISCOUNT_PERCENT ?? 0);
                        cmd.Parameters.AddWithValue("@DOJ", ParseDate(customer.DOJ));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", customer.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", customer.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUST_VAT_RULE_ID", customer.CUST_VAT_RULE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VAT_REGNO", customer.VAT_REGNO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_DELETED", customer.IS_DELETED ?? false);
                        cmd.Parameters.AddWithValue("@LOYALTY_POINT", customer.LOYALTY_POINT ?? 0);
                        cmd.Parameters.AddWithValue("@IS_COMPANY_BRANCH", customer.IS_COMPANY_BRANCH ?? 0);
                        cmd.Parameters.AddWithValue("@CUST_TYPE", customer.CUST_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@DEALER_ID", customer.DEALER_ID ?? 0);
                        //cmd.Parameters.AddWithValue("@WAREHOUSE_ID", customer.WAREHOUSE_ID ?? 0);

                        // 🔹 UDT parameter for delivery address
                        DataTable dtAddress = new DataTable();
                        dtAddress.Columns.Add("ADDRESS1", typeof(string));
                        dtAddress.Columns.Add("ADDRESS2", typeof(string));
                        dtAddress.Columns.Add("ADDRESS3", typeof(string));
                        dtAddress.Columns.Add("LOCATION", typeof(string));
                        dtAddress.Columns.Add("MOBILE", typeof(string));
                        dtAddress.Columns.Add("PHONE", typeof(string));

                        if (customer.DeliveryAddresses != null && customer.DeliveryAddresses.Count > 0)
                        {
                            foreach (var addr in customer.DeliveryAddresses)
                            {
                                dtAddress.Rows.Add(addr.ADDRESS1, addr.ADDRESS2, addr.ADDRESS3, addr.LOCATION, addr.MOBILE, addr.PHONE);
                            }
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_CUST_DELIVERY_ADDRESS", dtAddress);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_CUST_DELIVERY_ADDRESS";

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : throw new Exception("Insert failed: No ID returned.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SaveCustomer Error: " + ex.Message, ex);
            }
        }



        private static object ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return DBNull.Value;

            string[] formats = new[]
            {
                "dd-MM-yyyy HH:mm:ss",
                "dd-MM-yyyy",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-dd",
                "MM/dd/yyyy HH:mm:ss",
                "MM/dd/yyyy"
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;

            return DBNull.Value;
        }
        public int UpdateCustomer(CustomerUpdate customer)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_CUSTOMER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 2);
                        cmd.Parameters.AddWithValue("@ID", customer.ID);

                        // Same scalar parameters as SaveData
                        cmd.Parameters.AddWithValue("@HQID", customer.HQID ?? 0);
                        cmd.Parameters.AddWithValue("@AC_HEAD_ID", customer.AC_HEAD_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUST_CODE", customer.CUST_CODE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@FIRST_NAME", customer.FIRST_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", customer.CONTACT_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS1", customer.ADDRESS1 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS2", customer.ADDRESS2 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ADDRESS3", customer.ADDRESS3 ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ZIP", customer.ZIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CITY", customer.CITY ?? string.Empty);
                        cmd.Parameters.AddWithValue("@STATE_ID", customer.STATE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@COUNTRY_ID", customer.COUNTRY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PHONE", customer.PHONE ?? string.Empty);
                        cmd.Parameters.AddWithValue("@EMAIL", customer.EMAIL ?? string.Empty);
                        cmd.Parameters.AddWithValue("@SALESMAN_ID", customer.SALESMAN_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CREDIT_LIMIT", customer.CREDIT_LIMIT ?? 0);
                        cmd.Parameters.AddWithValue("@CURRENT_CREDIT", customer.CURRENT_CREDIT ?? 0);
                        cmd.Parameters.AddWithValue("@IS_BLOCKED", customer.IS_BLOCKED ?? false);
                        cmd.Parameters.AddWithValue("@MOBILE_NO", customer.MOBILE_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@FAX_NO", customer.FAX_NO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@LAST_NAME", customer.LAST_NAME ?? string.Empty);
                        //cmd.Parameters.AddWithValue("@DOB", ParseDate(customer.DOB));
                        //cmd.Parameters.AddWithValue("@NATIONALITY", customer.NATIONALITY ?? 0);
                        cmd.Parameters.AddWithValue("@NOTES", customer.NOTES ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CUST_NAME", customer.CUST_NAME ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CREDIT_DAYS", customer.CREDIT_DAYS ?? 0);
                        cmd.Parameters.AddWithValue("@PAY_TERM_ID", customer.PAY_TERM_ID ?? 0);
                        cmd.Parameters.AddWithValue("@PRICE_CLASS_ID", customer.PRICE_CLASS_ID ?? 0);
                        cmd.Parameters.AddWithValue("@DISCOUNT_PERCENT", customer.DISCOUNT_PERCENT ?? 0);
                        cmd.Parameters.AddWithValue("@DOJ", ParseDate(customer.DOJ));
                        cmd.Parameters.AddWithValue("@COMPANY_ID", customer.COMPANY_ID ?? 0);
                        cmd.Parameters.AddWithValue("@STORE_ID", customer.STORE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@CUST_VAT_RULE_ID", customer.CUST_VAT_RULE_ID ?? 0);
                        cmd.Parameters.AddWithValue("@VAT_REGNO", customer.VAT_REGNO ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IS_DELETED", customer.IS_DELETED ?? false);
                        cmd.Parameters.AddWithValue("@LOYALTY_POINT", customer.LOYALTY_POINT ?? 0m);
                        cmd.Parameters.AddWithValue("@IS_COMPANY_BRANCH", customer.IS_COMPANY_BRANCH ?? 0);
                        cmd.Parameters.AddWithValue("@CUST_TYPE", customer.CUST_TYPE ?? 0);
                        cmd.Parameters.AddWithValue("@DEALER_ID", customer.DEALER_ID ?? 0);
                        //cmd.Parameters.AddWithValue("@WAREHOUSE_ID", customer.WAREHOUSE_ID ?? 0);

                        // 🔹 Add UDT Delivery Address
                        DataTable dtAddress = new DataTable();
                        dtAddress.Columns.Add("ADDRESS1", typeof(string));
                        dtAddress.Columns.Add("ADDRESS2", typeof(string));
                        dtAddress.Columns.Add("ADDRESS3", typeof(string));
                        dtAddress.Columns.Add("LOCATION", typeof(string));
                        dtAddress.Columns.Add("MOBILE", typeof(string));
                        dtAddress.Columns.Add("PHONE", typeof(string));

                        if (customer.DeliveryAddresses != null && customer.DeliveryAddresses.Count > 0)
                        {
                            foreach (var addr in customer.DeliveryAddresses)
                            {
                                dtAddress.Rows.Add(addr.ADDRESS1, addr.ADDRESS2, addr.ADDRESS3, addr.LOCATION, addr.MOBILE, addr.PHONE);
                            }
                        }

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_CUST_DELIVERY_ADDRESS", dtAddress);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "UDT_CUST_DELIVERY_ADDRESS";

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        cmd.ExecuteNonQuery();
                        return customer.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateCustomer Error: " + ex.Message, ex);
            }
        }



        public CustomerUpdate GetItems(int id)
        {
            CustomerUpdate customer = new CustomerUpdate();
            customer.DeliveryAddresses = new List<CustDeliveryAddress>();

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
                    " TB_CUSTOMER.DOJ, TB_CUSTOMER.COMPANY_ID, TB_CUSTOMER.CUST_TYPE, TB_CUSTOMER.STORE_ID, TB_CUSTOMER.CUST_VAT_RULE_ID," +
                    " TB_CUSTOMER.VAT_REGNO, TB_CUSTOMER.IS_DELETED, TB_CUSTOMER.LOYALTY_POINT," +
                    " TB_STATE.STATE_NAME, TB_COUNTRY.COUNTRY_NAME, TB_EMPLOYEE.EMP_NAME, " +
                    " TB_PAYMENT_TERMS.CODE, TB_PRICE_CLASS.CLASS_NAME," +
                    " TB_COMPANY.COMPANY_NAME, TB_STORES.STORE_NAME,TB_CUSTOMER.WAREHOUSE_ID," +
                    " TB_VAT_RULE_CUSTOMER.DESCRIPTION AS VAT_RULE_DESCRIPTION, TB_CUSTOMER.CUST_VAT_RULE_ID, TB_CUSTOMER.IS_COMPANY_BRANCH, TB_CUSTOMER.DEALER_ID, TB_CUSTOMER.CUST_TYPE" +
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
                    //customer.DOB = Convert.ToString(dr["DOB"]);
                    //customer.NATIONALITY = Convert.ToInt32(dr["NATIONALITY"]);
                    customer.NOTES = Convert.ToString(dr["NOTES"]);
                    customer.CUST_NAME = Convert.ToString(dr["CUST_NAME"]);
                    customer.CREDIT_DAYS = Convert.ToInt32(dr["CREDIT_DAYS"]);
                    customer.PAY_TERM_ID = Convert.ToInt32(dr["PAY_TERM_ID"]);
                    customer.PRICE_CLASS_ID = Convert.ToInt32(dr["PRICE_CLASS_ID"]);
                    customer.DISCOUNT_PERCENT = float.Parse(dr["DISCOUNT_PERCENT"].ToString());
                    customer.DOJ = Convert.ToString(dr["DOJ"]);
                    customer.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    customer.STORE_ID = Convert.ToInt32(dr["STORE_ID"]);
                    customer.CUST_VAT_RULE_ID = Convert.ToInt32(dr["CUST_VAT_RULE_ID"]);
                    customer.VAT_REGNO = Convert.ToString(dr["VAT_REGNO"]);
                    customer.IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);
                    customer.LOYALTY_POINT = Convert.ToDecimal(dr["LOYALTY_POINT"]);
                    customer.IS_COMPANY_BRANCH = Convert.ToInt32(dr["IS_COMPANY_BRANCH"]);
                    customer.STATE_NAME = Convert.ToString(dr["STATE_NAME"]);
                    customer.COUNTRY_NAME = Convert.ToString(dr["COUNTRY_NAME"]);
                    customer.EMP_NAME = Convert.ToString(dr["EMP_NAME"]);
                    customer.CLASS_NAME = Convert.ToString(dr["CLASS_NAME"]);
                    customer.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                    customer.STORE_NAME = Convert.ToString(dr["STORE_NAME"]);
                    customer.VAT_RULE_DESCRIPTION = Convert.ToString(dr["VAT_RULE_DESCRIPTION"]);
                    customer.CUST_TYPE = Convert.ToInt32(dr["CUST_TYPE"]);
                    customer.DEALER_ID = Convert.ToInt32(dr["DEALER_ID"]);
                    //customer.WAREHOUSE_ID = Convert.ToInt32(dr["WAREHOUSE_ID"]);
                }
                string addressSQL = @"
                        SELECT ID, ADDRESS1, ADDRESS2, ADDRESS3, LOCATION, MOBILE, PHONE
                        FROM TB_CUST_DELIVERY_ADDRESS
                        WHERE CUST_ID = " + id;

                DataTable addrTbl = ADO.GetDataTable(addressSQL, "CustDeliveryAddress");
                foreach (DataRow adr in addrTbl.Rows)
                {
                    CustDeliveryAddress addr = new CustDeliveryAddress
                    {
                        ID = Convert.ToInt32(adr["ID"]),
                        ADDRESS1 = Convert.ToString(adr["ADDRESS1"]),
                        ADDRESS2 = Convert.ToString(adr["ADDRESS2"]),
                        ADDRESS3 = Convert.ToString(adr["ADDRESS3"]),
                        LOCATION = Convert.ToString(adr["LOCATION"]),
                        MOBILE = Convert.ToString(adr["MOBILE"]),
                        PHONE = Convert.ToString(adr["PHONE"])
                        
                    };
                    customer.DeliveryAddresses.Add(addr);
                }
            


            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching customer details: " + ex.Message);
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
        public List<DeliveryAddress> GetDeliveryAddressesForDealer(DELIVERYADDREQUEST custId)
        {
            var deliveryAddresses = new List<DeliveryAddress>();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_CUSTOMER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 5);
                        cmd.Parameters.AddWithValue("@ID", custId.CUST_ID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new DeliveryAddress
                                {
                                    Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    DELIVERYADDRESS = reader["DELIVERY_ADDRESS"] != DBNull.Value ? reader["DELIVERY_ADDRESS"].ToString() : string.Empty,
                                    ADDRESS = reader["DELIVERY_ADDRESS_FULL"] != DBNull.Value ? reader["DELIVERY_ADDRESS_FULL"].ToString() : string.Empty
                                };
                                deliveryAddresses.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching delivery addresses: " + ex.Message, ex);
            }
            return deliveryAddresses;
        }
        public List<Cust_stateName> Getcustlist(CustomerListReq request)
        {
            var Cust_stateName = new List<Cust_stateName>();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_CUSTOMER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 6);
                        cmd.Parameters.AddWithValue("COMPANY_ID", request.COMPANY_ID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new Cust_stateName
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    DESCRIPTION = reader["CUST_NAME"] != DBNull.Value ? reader["CUST_NAME"].ToString() : string.Empty,
                                    STATE_ID = reader["STATE_ID"] != DBNull.Value ? Convert.ToInt32(reader["STATE_ID"]) : 0,
                                    STATE_NAME = reader["STATE_NAME"] != DBNull.Value ? reader["STATE_NAME"].ToString() : string.Empty
                                };
                                Cust_stateName.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Cust_stateName;
        }
    }
}
