﻿using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MicroApi.DataLayer.Service
{
    public class EmployeeService:IEmployeeService
    {
        public List<Employee> GetAllEmployees(int? companyId)
        {
            List<Employee> employeeList = new List<Employee>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_EMPLOYEE";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("COMPANY_ID", (object)companyId ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    employeeList.Add(new Employee
                    {

                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        EMP_CODE = Convert.IsDBNull(dr["EMP_CODE"]) ? null : Convert.ToString(dr["EMP_CODE"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        DOB = Convert.IsDBNull(dr["DOB"]) ? (DateTime?)null : Convert.ToDateTime(dr["DOB"]),
                        ADDRESS1 = Convert.IsDBNull(dr["ADDRESS1"]) ? null : Convert.ToString(dr["ADDRESS1"]),
                        ADDRESS2 = Convert.IsDBNull(dr["ADDRESS2"]) ? null : Convert.ToString(dr["ADDRESS2"]),
                        ADDRESS3 = Convert.IsDBNull(dr["ADDRESS3"]) ? null : Convert.ToString(dr["ADDRESS3"]),
                        STATE_ID = Convert.IsDBNull(dr["STATE_ID"]) ? 0 : Convert.ToInt32(dr["STATE_ID"]),
                        CITY = Convert.IsDBNull(dr["CITY"]) ? null : Convert.ToString(dr["CITY"]),
                        COUNTRY_ID = Convert.IsDBNull(dr["COUNTRY_ID"]) ? 0 : Convert.ToInt32(dr["COUNTRY_ID"]),
                        MOBILE = Convert.IsDBNull(dr["MOBILE"]) ? null : Convert.ToString(dr["MOBILE"]),
                        EMAIL = Convert.IsDBNull(dr["EMAIL"]) ? null : Convert.ToString(dr["EMAIL"]),
                        IS_MALE = Convert.IsDBNull(dr["IS_MALE"]) ? (bool?)null : Convert.ToBoolean(dr["IS_MALE"]),
                        DEPT_ID = Convert.IsDBNull(dr["DEPT_ID"]) ? 0 : Convert.ToInt32(dr["DEPT_ID"]),
                        DESG_ID = Convert.IsDBNull(dr["DESG_ID"]) ? 0 : Convert.ToInt32(dr["DESG_ID"]),
                        DOJ = Convert.IsDBNull(dr["DOJ"]) ? (DateTime?)null : Convert.ToDateTime(dr["DOJ"]),
                        BANK_CODE = Convert.IsDBNull(dr["BANK_CODE"]) ? null : Convert.ToString(dr["BANK_CODE"]),
                        BANK_AC_NO = Convert.IsDBNull(dr["BANK_AC_NO"]) ? null : Convert.ToString(dr["BANK_AC_NO"]),
                        PP_NO = Convert.IsDBNull(dr["PP_NO"]) ? null : Convert.ToString(dr["PP_NO"]),
                        PP_EXPIRY = Convert.IsDBNull(dr["PP_EXPIRY"]) ? (DateTime?)null : Convert.ToDateTime(dr["PP_EXPIRY"]),
                        VISA_NO = Convert.IsDBNull(dr["VISA_NO"]) ? null : Convert.ToString(dr["VISA_NO"]),
                        VISA_EXPIRY = Convert.IsDBNull(dr["VISA_EXPIRY"]) ? (DateTime?)null : Convert.ToDateTime(dr["VISA_EXPIRY"]),
                        LICENSE_NO = Convert.IsDBNull(dr["LICENSE_NO"]) ? null : Convert.ToString(dr["LICENSE_NO"]),
                        LICENSE_EXPIRY = Convert.IsDBNull(dr["LICENSE_EXPIRY"]) ? (DateTime?)null : Convert.ToDateTime(dr["LICENSE_EXPIRY"]),
                        IS_SALESMAN = Convert.IsDBNull(dr["IS_SALESMAN"]) ? (bool?)null : Convert.ToBoolean(dr["IS_SALESMAN"]),
                        IMAGE_NAME = Convert.IsDBNull(dr["IMAGE_NAME"]) ? null : Convert.ToString(dr["IMAGE_NAME"]),
                        WORK_PERMIT_NO = Convert.IsDBNull(dr["WORK_PERMIT_NO"]) ? null : Convert.ToString(dr["WORK_PERMIT_NO"]),
                        WORK_PERMIT_EXPIRY = Convert.IsDBNull(dr["WORK_PERMIT_EXPIRY"]) ? (DateTime?)null : Convert.ToDateTime(dr["WORK_PERMIT_EXPIRY"]),
                        IBAN_NO = Convert.IsDBNull(dr["IBAN_NO"]) ? null : Convert.ToString(dr["IBAN_NO"]),
                        DAMAN_NO = Convert.IsDBNull(dr["DAMAN_NO"]) ? null : Convert.ToString(dr["DAMAN_NO"]),
                        DAMAN_CATEGORY = Convert.IsDBNull(dr["DAMAN_CATEGORY"]) ? null : Convert.ToString(dr["DAMAN_CATEGORY"]),
                        MOL_NUMBER = Convert.IsDBNull(dr["MOL_NUMBER"]) ? null : Convert.ToString(dr["MOL_NUMBER"]),
                        LAST_REJOIN_DATE = Convert.IsDBNull(dr["LAST_REJOIN_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["LAST_REJOIN_DATE"]),
                        IS_DELETED = Convert.IsDBNull(dr["IS_DELETED"]) ? false : Convert.ToBoolean(dr["IS_DELETED"]),
                       // STORE_NAME = Convert.IsDBNull(dr["STORE_NAME"]) ? null : Convert.ToString(dr["STORE_NAME"]),
                        STATE_NAME = Convert.IsDBNull(dr["STATE_NAME"]) ? null : Convert.ToString(dr["STATE_NAME"]),
                        DEPT_NAME = Convert.IsDBNull(dr["DEPT_NAME"]) ? null : Convert.ToString(dr["DEPT_NAME"]),
                        DESG_NAME = Convert.IsDBNull(dr["DESG_NAME"]) ? null : Convert.ToString(dr["DESG_NAME"]),
                        BANK_NAME = Convert.IsDBNull(dr["BANK_NAME"]) ? null : Convert.ToString(dr["BANK_NAME"]),
                        PAYMENT_TYPE = Convert.IsDBNull(dr["PAYMENT_TYPE"]) ? 0 : Convert.ToInt32(dr["PAYMENT_TYPE"]),
                        IS_INACTIVE = Convert.IsDBNull(dr["IS_INACTIVE"]) ? false : Convert.ToBoolean(dr["IS_INACTIVE"]),
                        LEAVE_DAY_BALANCE = Convert.IsDBNull(dr["LEAVE_DAY_BALANCE"]) ? 0 : Convert.ToDecimal(dr["LEAVE_DAY_BALANCE"]),
                        DAYS_DEDUCTED = Convert.IsDBNull(dr["DAYS_DEDUCTED"]) ? 0 : Convert.ToDecimal(dr["DAYS_DEDUCTED"]),
                        LEAVE_CREDIT = Convert.IsDBNull(dr["LEAVE_CREDIT"]) ? 0 : Convert.ToDecimal(dr["LEAVE_CREDIT"]),
                        //COMPANY_ID = Convert.IsDBNull(dr["COMPANY_ID"]) ? 0 : Convert.ToInt32(dr["COMPANY_ID"]),
                    });
                }
                connection.Close();
            }
            return employeeList;
        }

        //public Int32 SaveData(Employee employee)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = ADO.GetConnection())
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "SP_TB_EMPLOYEE";

        //            cmd.Parameters.AddWithValue("ACTION", 1);

        //            //cmd.Parameters.AddWithValue("ID", employee.ID);
        //            cmd.Parameters.AddWithValue("ID", (object)employee.ID ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("EMP_CODE", (object)employee.EMP_CODE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("EMP_NAME", (object)employee.EMP_NAME ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DOB", (object)employee.DOB ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("ADDRESS1", (object)employee.ADDRESS1 ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("ADDRESS2", (object)employee.ADDRESS2 ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("ADDRESS3", (object)employee.ADDRESS3 ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("STATE_ID", (object)employee.STATE_ID ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("CITY", (object)employee.CITY ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("COUNTRY_ID", (object)employee.COUNTRY_ID ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("MOBILE", (object)employee.MOBILE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("EMAIL", (object)employee.EMAIL ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("IS_MALE", (object)employee.IS_MALE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DEPT_ID", (object)employee.DEPT_ID ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DESG_ID", (object)employee.DESG_ID ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DOJ", (object)employee.DOJ ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("BANK_CODE", (object)employee.BANK_CODE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("BANK_AC_NO", (object)employee.BANK_AC_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("BANK_NAME", (object)employee.BANK_NAME ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("PAYMENT_TYPE", (object)employee.PAYMENT_TYPE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("PP_NO", (object)employee.PP_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("PP_EXPIRY", (object)employee.PP_EXPIRY ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("VISA_NO", (object)employee.VISA_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("VISA_EXPIRY", (object)employee.VISA_EXPIRY ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("LICENSE_NO", (object)employee.LICENSE_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("LICENSE_EXPIRY", (object)employee.LICENSE_EXPIRY ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("IS_SALESMAN", (object)employee.IS_SALESMAN ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("IMAGE_NAME", (object)employee.IMAGE_NAME ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("WORK_PERMIT_NO", (object)employee.WORK_PERMIT_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("WORK_PERMIT_EXPIRY", (object)employee.WORK_PERMIT_EXPIRY ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("IBAN_NO", (object)employee.IBAN_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DAMAN_NO", (object)employee.DAMAN_NO ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DAMAN_CATEGORY", (object)employee.DAMAN_CATEGORY ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("MOL_NUMBER", (object)employee.MOL_NUMBER ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("LAST_REJOIN_DATE", (object)employee.LAST_REJOIN_DATE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("IS_INACTIVE", (object)employee.IS_INACTIVE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("LEAVE_DAY_BALANCE", (object)employee.LEAVE_DAY_BALANCE ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("DAYS_DEDUCTED", (object)employee.DAYS_DEDUCTED ?? DBNull.Value);


        //            DataTable tbl1 = new DataTable();

        //            tbl1.Columns.Add("EMP_ID", typeof(Int32));
        //            tbl1.Columns.Add("HEAD_ID", typeof(Int32));
        //            tbl1.Columns.Add("HEAD_AMOUNT", typeof(float));

        //            if (employee.EmployeeSalary != null && employee.EmployeeSalary.Any())
        //            {
        //                foreach (EmployeeSalary ur1 in employee.EmployeeSalary)
        //                {
        //                    DataRow dRow1 = tbl1.NewRow();
        //                    dRow1["EMP_ID"] = ur1.EMP_ID;
        //                    dRow1["HEAD_ID"] = ur1.HEAD_ID;
        //                    dRow1["HEAD_AMOUNT"] = ur1.HEAD_AMOUNT;
        //                    tbl1.Rows.Add(dRow1);
        //                }
        //            }

        //            cmd.Parameters.AddWithValue("@UDT_TB_EMPLOYEE_SALARY", tbl1);

        //            DataTable tblAttachments = new DataTable();

        //            tblAttachments.Columns.Add("TRANS_TYPE", typeof(short));
        //            tblAttachments.Columns.Add("TRANS_ID", typeof(int));
        //            tblAttachments.Columns.Add("FILE_NAME", typeof(string));
        //            tblAttachments.Columns.Add("FILE_DATA", typeof(byte[]));
        //            tblAttachments.Columns.Add("REMARKS", typeof(string));
        //            tblAttachments.Columns.Add("CREATED_USER_ID", typeof(int));
        //            tblAttachments.Columns.Add("CREATED_TIME", typeof(DateTime));

        //            if (employee.Attachment != null && employee.Attachment.Any())
        //            {
        //                foreach (var file in employee.Attachment)
        //                {
        //                    DataRow dRow = tblAttachments.NewRow();
        //                    dRow["TRANS_TYPE"] = file.DOC_TYPE;
        //                    dRow["TRANS_ID"] = file.DOC_ID;
        //                    dRow["FILE_NAME"] = file.FILE_NAME;
        //                    dRow["FILE_DATA"] = file.FILE_DATA; // should be a byte[]
        //                    dRow["REMARKS"] = file.REMARKS ?? string.Empty;
        //                    dRow["CREATED_USER_ID"] = file.USER_ID;
        //                    dRow["CREATED_TIME"] = file.CREATED_DATE_TIME;

        //                    tblAttachments.Rows.Add(dRow);
        //                }
        //            }

        //            cmd.Parameters.AddWithValue("@UDT_TB_ATTACHMENTS", tblAttachments);


        //            Int32 StoreID = Convert.ToInt32(cmd.ExecuteScalar());

        //            return StoreID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public int SaveEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE";
                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@ID", (object)employee.ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_CODE", (object)employee.EMP_CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_NAME", (object)employee.EMP_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DOB", (object)employee.DOB ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADDRESS1", (object)employee.ADDRESS1 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADDRESS2", (object)employee.ADDRESS2 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADDRESS3", (object)employee.ADDRESS3 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STATE_ID", (object)employee.STATE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CITY", (object)employee.CITY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@COUNTRY_ID", (object)employee.COUNTRY_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOBILE", (object)employee.MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMAIL", (object)employee.EMAIL ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_MALE", (object)employee.IS_MALE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DEPT_ID", (object)employee.DEPT_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DESG_ID", (object)employee.DESG_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DOJ", (object)employee.DOJ ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BANK_CODE", (object)employee.BANK_CODE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BANK_AC_NO", (object)employee.BANK_AC_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BANK_NAME", (object)employee.BANK_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PAYMENT_TYPE", (object)employee.PAYMENT_TYPE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PP_NO", (object)employee.PP_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PP_EXPIRY", (object)employee.PP_EXPIRY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VISA_NO", (object)employee.VISA_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VISA_EXPIRY", (object)employee.VISA_EXPIRY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LICENSE_NO", (object)employee.LICENSE_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LICENSE_EXPIRY", (object)employee.LICENSE_EXPIRY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_SALESMAN", (object)employee.IS_SALESMAN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IMAGE_NAME", (object)employee.IMAGE_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@WORK_PERMIT_NO", (object)employee.WORK_PERMIT_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@WORK_PERMIT_EXPIRY", (object)employee.WORK_PERMIT_EXPIRY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IBAN_NO", (object)employee.IBAN_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAMAN_NO", (object)employee.DAMAN_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAMAN_CATEGORY", (object)employee.DAMAN_CATEGORY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOL_NUMBER", (object)employee.MOL_NUMBER ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LAST_REJOIN_DATE", (object)employee.LAST_REJOIN_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IS_INACTIVE", (object)employee.IS_INACTIVE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_DAY_BALANCE", (object)employee.LEAVE_DAY_BALANCE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DAYS_DEDUCTED", (object)employee.DAYS_DEDUCTED ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", (object)employee.COMPANY_ID ?? DBNull.Value);

                    Int32 employeeID = Convert.ToInt32(cmd.ExecuteScalar());
                    return employeeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateEmployee(EmployeeUpdate employee)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 2); // Update action
                cmd.Parameters.AddWithValue("@ID", employee.ID);

                // Add parameters only if they are not null
                if (!string.IsNullOrEmpty(employee.EMP_CODE))
                    cmd.Parameters.AddWithValue("@EMP_CODE", employee.EMP_CODE);
                if (!string.IsNullOrEmpty(employee.EMP_NAME))
                    cmd.Parameters.AddWithValue("@EMP_NAME", employee.EMP_NAME);
                if (employee.DOB.HasValue)
                    cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                if (!string.IsNullOrEmpty(employee.ADDRESS1))
                    cmd.Parameters.AddWithValue("@ADDRESS1", employee.ADDRESS1);
                if (!string.IsNullOrEmpty(employee.ADDRESS2))
                    cmd.Parameters.AddWithValue("@ADDRESS2", employee.ADDRESS2);
                if (!string.IsNullOrEmpty(employee.ADDRESS3))
                    cmd.Parameters.AddWithValue("@ADDRESS3", employee.ADDRESS3);
                if (!string.IsNullOrEmpty(employee.CITY))
                    cmd.Parameters.AddWithValue("@CITY", employee.CITY);
                if (employee.STATE_ID.HasValue)
                    cmd.Parameters.AddWithValue("@STATE_ID", employee.STATE_ID);
                if (!string.IsNullOrEmpty(employee.MOBILE))
                    cmd.Parameters.AddWithValue("@MOBILE", employee.MOBILE);
                if (!string.IsNullOrEmpty(employee.EMAIL))
                    cmd.Parameters.AddWithValue("@EMAIL", employee.EMAIL);
                if (employee.IS_MALE.HasValue)
                    cmd.Parameters.AddWithValue("@IS_MALE", employee.IS_MALE);
                if (employee.DEPT_ID.HasValue)
                    cmd.Parameters.AddWithValue("@DEPT_ID", employee.DEPT_ID);
                if (employee.DESG_ID.HasValue)
                    cmd.Parameters.AddWithValue("@DESG_ID", employee.DESG_ID);
                if (employee.DOJ.HasValue)
                    cmd.Parameters.AddWithValue("@DOJ", employee.DOJ);
                if (!string.IsNullOrEmpty(employee.BANK_CODE))
                    cmd.Parameters.AddWithValue("@BANK_CODE", employee.BANK_CODE);
                if (!string.IsNullOrEmpty(employee.BANK_AC_NO))
                    cmd.Parameters.AddWithValue("@BANK_AC_NO", employee.BANK_AC_NO);
                if (!string.IsNullOrEmpty(employee.BANK_NAME))
                    cmd.Parameters.AddWithValue("@BANK_NAME", employee.BANK_NAME);
                if (employee.PAYMENT_TYPE.HasValue)
                    cmd.Parameters.AddWithValue("@PAYMENT_TYPE", employee.PAYMENT_TYPE);
                if (!string.IsNullOrEmpty(employee.PP_NO))
                    cmd.Parameters.AddWithValue("@PP_NO", employee.PP_NO);
                if (employee.PP_EXPIRY.HasValue)
                    cmd.Parameters.AddWithValue("@PP_EXPIRY", employee.PP_EXPIRY);
                if (!string.IsNullOrEmpty(employee.VISA_NO))
                    cmd.Parameters.AddWithValue("@VISA_NO", employee.VISA_NO);
                if (employee.VISA_EXPIRY.HasValue)
                    cmd.Parameters.AddWithValue("@VISA_EXPIRY", employee.VISA_EXPIRY);
                if (!string.IsNullOrEmpty(employee.LICENSE_NO))
                    cmd.Parameters.AddWithValue("@LICENSE_NO", employee.LICENSE_NO);
                if (employee.LICENSE_EXPIRY.HasValue)
                    cmd.Parameters.AddWithValue("@LICENSE_EXPIRY", employee.LICENSE_EXPIRY);
                if (employee.IS_SALESMAN.HasValue)
                    cmd.Parameters.AddWithValue("@IS_SALESMAN", employee.IS_SALESMAN);
                if (!string.IsNullOrEmpty(employee.IMAGE_NAME))
                    cmd.Parameters.AddWithValue("@IMAGE_NAME", employee.IMAGE_NAME);
                if (!string.IsNullOrEmpty(employee.WORK_PERMIT_NO))
                    cmd.Parameters.AddWithValue("@WORK_PERMIT_NO", employee.WORK_PERMIT_NO);
                if (employee.WORK_PERMIT_EXPIRY.HasValue)
                    cmd.Parameters.AddWithValue("@WORK_PERMIT_EXPIRY", employee.WORK_PERMIT_EXPIRY);
                if (!string.IsNullOrEmpty(employee.IBAN_NO))
                    cmd.Parameters.AddWithValue("@IBAN_NO", employee.IBAN_NO);
                if (!string.IsNullOrEmpty(employee.DAMAN_NO))
                    cmd.Parameters.AddWithValue("@DAMAN_NO", employee.DAMAN_NO);
                if (!string.IsNullOrEmpty(employee.DAMAN_CATEGORY))
                    cmd.Parameters.AddWithValue("@DAMAN_CATEGORY", employee.DAMAN_CATEGORY);
                if (employee.LEAVE_CREDIT != default(decimal))
                    cmd.Parameters.AddWithValue("@LEAVE_CREDIT", employee.LEAVE_CREDIT);
                //if (employee.LESS_SERVICE_DAYS.HasValue)
                //    cmd.Parameters.AddWithValue("@LESS_SERVICE_DAYS", employee.LESS_SERVICE_DAYS);
                //if (employee.HOLD_SALARY.HasValue)
                //    cmd.Parameters.AddWithValue("@HOLD_SALARY", employee.HOLD_SALARY);
                if (!string.IsNullOrEmpty(employee.MOL_NUMBER))
                    cmd.Parameters.AddWithValue("@MOL_NUMBER", employee.MOL_NUMBER);
                if (employee.LAST_REJOIN_DATE.HasValue)
                    cmd.Parameters.AddWithValue("@LAST_REJOIN_DATE", employee.LAST_REJOIN_DATE);
                if (employee.IS_INACTIVE != default(bool))
                    cmd.Parameters.AddWithValue("@IS_INACTIVE", employee.IS_INACTIVE);
                if (employee.LEAVE_DAY_BALANCE != default(decimal))
                    cmd.Parameters.AddWithValue("@LEAVE_DAY_BALANCE", employee.LEAVE_DAY_BALANCE);
                if (employee.DAYS_DEDUCTED != default(decimal))
                    cmd.Parameters.AddWithValue("@DAYS_DEDUCTED", employee.DAYS_DEDUCTED);
                if (employee.COMPANY_ID.HasValue)
                    cmd.Parameters.AddWithValue("@COMPANY_ID", employee.COMPANY_ID);

                // Handle attachments only during update
                if (employee.Attachment != null && employee.Attachment.Any())
                {
                    DataTable attachmentTable = new DataTable();
                    attachmentTable.Columns.Add("TRANS_TYPE", typeof(short));
                    attachmentTable.Columns.Add("TRANS_ID", typeof(int));
                    attachmentTable.Columns.Add("FILE_NAME", typeof(string));
                    attachmentTable.Columns.Add("FILE_DATA", typeof(byte[]));
                    attachmentTable.Columns.Add("REMARKS", typeof(string));
                    attachmentTable.Columns.Add("CREATED_USER_ID", typeof(int));
                    attachmentTable.Columns.Add("CREATED_TIME", typeof(DateTime));

                    foreach (var attachment in employee.Attachment)
                    {
                        DataRow row = attachmentTable.NewRow();
                        row["TRANS_TYPE"] = 100; 
                        row["TRANS_ID"] = employee.ID; 
                        row["FILE_NAME"] = attachment.FILE_NAME;
                        row["FILE_DATA"] = attachment.FILE_DATA; 
                        row["REMARKS"] = attachment.REMARKS;
                        row["CREATED_USER_ID"] = 1; 
                        row["CREATED_TIME"] = DateTime.Now; 
                        attachmentTable.Rows.Add(row);
                    }

                    SqlParameter attachmentParam = cmd.Parameters.AddWithValue("@UDT_TB_ATTACHMENTS", attachmentTable);
                    attachmentParam.SqlDbType = SqlDbType.Structured;
                }

               
                cmd.ExecuteNonQuery();
            }
            return true;
        }



        public EmployeeUpdate GetItems(int id)
        {
            EmployeeUpdate employee = new EmployeeUpdate();
            //List<EmployeeSalary> employeeSalary = new List<EmployeeSalary>();
            List<EmpAttachment> empAttachment = new List<EmpAttachment>();

            try
            {
                string strSQL = @"SELECT TB_EMPLOYEE.ID, TB_EMPLOYEE.EMP_CODE, TB_EMPLOYEE.EMP_NAME, TB_EMPLOYEE.DOB, " +
                "TB_EMPLOYEE.ADDRESS1, TB_EMPLOYEE.ADDRESS2, TB_EMPLOYEE.ADDRESS3, TB_EMPLOYEE.CITY, TB_EMPLOYEE.STATE_ID, " +
                "TB_EMPLOYEE.COUNTRY_ID, TB_EMPLOYEE.MOBILE, TB_EMPLOYEE.EMAIL,TB_EMPLOYEE.IS_MALE,  " +
                "TB_EMPLOYEE.DEPT_ID, TB_EMPLOYEE.DESG_ID, TB_EMPLOYEE.DOJ, TB_EMPLOYEE.BANK_CODE, TB_EMPLOYEE.BANK_AC_NO, " +
                "TB_EMPLOYEE.PP_NO, TB_EMPLOYEE.PP_EXPIRY, TB_EMPLOYEE.IQAMA_NO, TB_EMPLOYEE.IQAMA_EXPIRY, " +
                "TB_EMPLOYEE.VISA_NO, TB_EMPLOYEE.VISA_EXPIRY, TB_EMPLOYEE.LICENSE_NO, " +
                "TB_EMPLOYEE.LICENSE_EXPIRY, TB_EMPLOYEE.EMP_STATUS, TB_EMPLOYEE.IS_SALESMAN, " +
                "TB_EMPLOYEE.IMAGE_NAME, TB_EMPLOYEE.WORK_PERMIT_NO, TB_EMPLOYEE.WORK_PERMIT_EXPIRY, " +
                "TB_EMPLOYEE.IBAN_NO, TB_EMPLOYEE.DAMAN_NO, TB_EMPLOYEE.DAMAN_CATEGORY, " +
                "TB_EMPLOYEE.LEAVE_CREDIT, TB_EMPLOYEE.LESS_SERVICE_DAYS, TB_EMPLOYEE.HOLD_SALARY, " +
                "TB_EMPLOYEE.MOL_NUMBER, TB_EMPLOYEE.LAST_REJOIN_DATE, TB_EMPLOYEE.INCENTIVE_PERCENT, " +
                "TB_EMPLOYEE.IS_DELETED, " +
                "TB_EMPLOYEE.BANK_NAME, TB_EMPLOYEE.PAYMENT_TYPE, " +
                "TB_EMPLOYEE.IS_INACTIVE, TB_EMPLOYEE.LEAVE_DAY_BALANCE, TB_EMPLOYEE.DAYS_DEDUCTED, " +

                "TB_STATE.STATE_NAME, TB_COUNTRY.COUNTRY_NAME, TB_EMPLOYEE_DEPARTMENT.DEPT_NAME, " +
                "TB_EMPLOYEE_DESIGNATION.DESG_NAME, TB_STORES.STORE_NAME " + // Added space here
                "FROM TB_EMPLOYEE " +
                "LEFT JOIN TB_STATE ON TB_EMPLOYEE.STATE_ID = TB_STATE.ID " +
                "LEFT JOIN TB_COUNTRY ON TB_EMPLOYEE.COUNTRY_ID = TB_COUNTRY.ID " +
                "LEFT JOIN TB_EMPLOYEE_DEPARTMENT ON TB_EMPLOYEE.DEPT_ID = TB_EMPLOYEE_DEPARTMENT.ID " +
                "LEFT JOIN TB_EMPLOYEE_DESIGNATION ON TB_EMPLOYEE.DESG_ID = TB_EMPLOYEE_DESIGNATION.ID " +
                "LEFT JOIN TB_STORES ON TB_EMPLOYEE.STORE_ID = TB_STORES.ID " +
                "WHERE TB_EMPLOYEE.ID = " + id;

                //string strSalarySQL = "SELECT TB_EMPLOYEE_SALARY.ID,TB_EMPLOYEE_SALARY.EMP_ID,TB_EMPLOYEE_SALARY.HEAD_ID,TB_SALARY_HEAD.HEAD_NAME,TB_EMPLOYEE_SALARY.HEAD_AMOUNT " +
                //    "FROM TB_EMPLOYEE_SALARY " +
                //    "LEFT JOIN TB_SALARY_HEAD ON TB_EMPLOYEE_SALARY.HEAD_ID = TB_SALARY_HEAD.ID " +
                //    "WHERE EMP_ID = " + id;

                //DataTable tblsalary = ADO.GetDataTable(strSalarySQL, "EmployeeSalary");

                //if (tblsalary.Rows.Count > 0)
                //{
                //    foreach (DataRow drsalary in tblsalary.Rows)
                //    {
                //        employeeSalary.Add(new EmployeeSalary
                //        {
                //            ID = ADO.ToInt32(drsalary["ID"]),
                //            HEAD_ID = ADO.ToInt32(drsalary["HEAD_ID"]),
                //            HEAD_NAME = ADO.ToString(drsalary["HEAD_NAME"]),
                //            EMP_ID = ADO.ToInt32(drsalary["EMP_ID"]),
                //            HEAD_AMOUNT = ADO.ToInt32(drsalary["HEAD_AMOUNT"]),
                //        });
                //    }

                //    employee.EmployeeSalary = employeeSalary;
                //}


                DataTable tbl = ADO.GetDataTable(strSQL, "EmployeeUpdate");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    employee.ID = Convert.ToInt32(dr["ID"]);
                    employee.EMP_CODE = dr["EMP_CODE"] != DBNull.Value ? Convert.ToString(dr["EMP_CODE"]) : "";
                    employee.EMP_NAME = dr["EMP_NAME"] != DBNull.Value ? Convert.ToString(dr["EMP_NAME"]) : "";
                    employee.DOB = dr["DOB"] != DBNull.Value ? Convert.ToDateTime(dr["DOB"]) : (DateTime?)null;

                    employee.ADDRESS1 = dr["ADDRESS1"] != DBNull.Value ? Convert.ToString(dr["ADDRESS1"]) : "";
                    employee.ADDRESS2 = dr["ADDRESS2"] != DBNull.Value ? Convert.ToString(dr["ADDRESS2"]) : "";
                    employee.ADDRESS3 = dr["ADDRESS3"] != DBNull.Value ? Convert.ToString(dr["ADDRESS3"]) : "";
                    employee.CITY = dr["CITY"] != DBNull.Value ? Convert.ToString(dr["CITY"]) : "";

                    employee.STATE_ID = dr["STATE_ID"] != DBNull.Value ? Convert.ToInt32(dr["STATE_ID"]) : null;
                    employee.COUNTRY_ID = dr["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COUNTRY_ID"]) : (int?)null;

                    employee.MOBILE = dr["MOBILE"] != DBNull.Value ? Convert.ToString(dr["MOBILE"]) : "";
                    employee.EMAIL = dr["EMAIL"] != DBNull.Value ? Convert.ToString(dr["EMAIL"]) : "";

                    employee.IS_MALE = dr["IS_MALE"] != DBNull.Value && Convert.ToBoolean(dr["IS_MALE"]);
                    employee.DEPT_ID = dr["DEPT_ID"] != DBNull.Value ? Convert.ToInt32(dr["DEPT_ID"]) : (int?)null;
                    employee.DESG_ID = dr["DESG_ID"] != DBNull.Value ? Convert.ToInt32(dr["DESG_ID"]) : (int?)null;
                    employee.DOJ = dr["DOJ"] != DBNull.Value ? Convert.ToDateTime(dr["DOJ"]) : (DateTime?)null;

                    employee.BANK_CODE = dr["BANK_CODE"] != DBNull.Value ? Convert.ToString(dr["BANK_CODE"]) : "";
                    employee.BANK_AC_NO = dr["BANK_AC_NO"] != DBNull.Value ? Convert.ToString(dr["BANK_AC_NO"]) : "";
                    employee.PP_NO = dr["PP_NO"] != DBNull.Value ? Convert.ToString(dr["PP_NO"]) : "";
                    employee.PP_EXPIRY = dr["PP_EXPIRY"] != DBNull.Value ? Convert.ToDateTime(dr["PP_EXPIRY"]) : (DateTime?)null;

                    employee.VISA_NO = dr["VISA_NO"] != DBNull.Value ? Convert.ToString(dr["VISA_NO"]) : "";
                    employee.VISA_EXPIRY = dr["VISA_EXPIRY"] != DBNull.Value ? Convert.ToDateTime(dr["VISA_EXPIRY"]) : (DateTime?)null;

                    employee.LICENSE_NO = dr["LICENSE_NO"] != DBNull.Value ? Convert.ToString(dr["LICENSE_NO"]) : "";
                    employee.LICENSE_EXPIRY = dr["LICENSE_EXPIRY"] != DBNull.Value ? Convert.ToDateTime(dr["LICENSE_EXPIRY"]) : (DateTime?)null;

                    employee.IS_SALESMAN = dr["IS_SALESMAN"] != DBNull.Value && Convert.ToBoolean(dr["IS_SALESMAN"]);
                    employee.IMAGE_NAME = dr["IMAGE_NAME"] != DBNull.Value ? Convert.ToString(dr["IMAGE_NAME"]) : "";

                    employee.WORK_PERMIT_NO = dr["WORK_PERMIT_NO"] != DBNull.Value ? Convert.ToString(dr["WORK_PERMIT_NO"]) : "";
                    employee.WORK_PERMIT_EXPIRY = dr["WORK_PERMIT_EXPIRY"] != DBNull.Value ? Convert.ToDateTime(dr["WORK_PERMIT_EXPIRY"]) : (DateTime?)null;

                    employee.IBAN_NO = dr["IBAN_NO"] != DBNull.Value ? Convert.ToString(dr["IBAN_NO"]) : "";
                    employee.DAMAN_NO = dr["DAMAN_NO"] != DBNull.Value ? Convert.ToString(dr["DAMAN_NO"]) : "";
                    employee.DAMAN_CATEGORY = dr["DAMAN_CATEGORY"] != DBNull.Value ? Convert.ToString(dr["DAMAN_CATEGORY"]) : "";
                    employee.MOL_NUMBER = dr["MOL_NUMBER"] != DBNull.Value ? Convert.ToString(dr["MOL_NUMBER"]) : "";

                    employee.LAST_REJOIN_DATE = dr["LAST_REJOIN_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["LAST_REJOIN_DATE"]) : (DateTime?)null;

                    employee.IS_DELETED = dr["IS_DELETED"] != DBNull.Value && Convert.ToBoolean(dr["IS_DELETED"]);

                    //employee.STORE_NAME = dr["STORE_NAME"] != DBNull.Value ? Convert.ToString(dr["STORE_NAME"]) : null;
                    employee.STATE_NAME = dr["STATE_NAME"] != DBNull.Value ? Convert.ToString(dr["STATE_NAME"]) : "";
                    employee.DEPT_NAME = dr["DEPT_NAME"] != DBNull.Value ? Convert.ToString(dr["DEPT_NAME"]) : "";
                    employee.DESG_NAME = dr["DESG_NAME"] != DBNull.Value ? Convert.ToString(dr["DESG_NAME"]) : "";
                    employee.BANK_NAME = dr["BANK_NAME"] != DBNull.Value ? Convert.ToString(dr["BANK_NAME"]) : "";

                    employee.PAYMENT_TYPE = dr["PAYMENT_TYPE"] != DBNull.Value ? Convert.ToInt32(dr["PAYMENT_TYPE"]) : (int?)null;
                    employee.IS_INACTIVE = dr["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(dr["IS_INACTIVE"]);

                    employee.LEAVE_DAY_BALANCE = dr["LEAVE_DAY_BALANCE"] != DBNull.Value ? Convert.ToDecimal(dr["LEAVE_DAY_BALANCE"]) : 0;
                    employee.DAYS_DEDUCTED = dr["DAYS_DEDUCTED"] != DBNull.Value ? Convert.ToDecimal(dr["DAYS_DEDUCTED"]) : 0;
                    //employee.COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COMPANY_ID"]) : (int?)null;



                }
                string strAttachmentsSQL = @"SELECT ID, TRANS_TYPE, TRANS_ID, FILE_NAME, FILE_DATA, REMARKS, CREATED_USER_ID, CREATED_TIME, IS_DELETED, DELETED_USER_ID, DELETED_TIME FROM TB_ATTACHEMENTS WHERE TRANS_ID = " + id;
                DataTable tblAttachments = ADO.GetDataTable(strAttachmentsSQL, "EmployeeAttachments");

                if (tblAttachments.Rows.Count > 0)
                {
                    foreach (DataRow drAttachment in tblAttachments.Rows)
                    {
                        empAttachment.Add(new EmpAttachment
                        {
                            ID = ADO.ToInt32(drAttachment["ID"]),
                            DOC_TYPE = ADO.ToInt32(drAttachment["TRANS_TYPE"]),
                            DOC_ID = ADO.ToInt32(drAttachment["TRANS_ID"]),
                            FILE_NAME = ADO.ToString(drAttachment["FILE_NAME"]),
                            FILE_DATA = drAttachment["FILE_DATA"] as byte[],
                            REMARKS = ADO.ToString(drAttachment["REMARKS"]),
                            USER_ID = ADO.ToInt32(drAttachment["CREATED_USER_ID"]),
                            CREATED_DATE_TIME = Convert.ToDateTime(drAttachment["CREATED_TIME"])
                        });
                    }
                    employee.Attachment = empAttachment;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return employee;
        }

        public bool DeleteEmployees(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE";
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
