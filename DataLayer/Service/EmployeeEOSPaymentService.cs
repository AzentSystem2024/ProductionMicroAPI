using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MicroApi.DAL.Services
{
    public class EmployeeEOSPaymentService : IEmployeeEOSPaymentService
    {


        public EOSPaymentResponse GetEmployeeEOSPayment(EOSPaymentRequest request)
              
        {
            EOSPaymentResponse response = new EOSPaymentResponse();

            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_EOS_PAYMENT_NEW", connection))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@EMP_ID", request.EmployeeId);
                   // SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_EOS_PAYMENT_NEW", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // Basic Details
                            response.EmployeeCode = dr["EMP_CODE"].ToString();
                            response.EmployeeName = dr["EMP_NAME"].ToString();

                            response.Reason = dr["REASON"].ToString();
                            response.DocNo = dr["DOC_NO"].ToString();

                            response.JoinDate =
                                dr["DOJ"] == DBNull.Value
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(dr["DOJ"]);

                            response.LastWorkingDay =
                                dr["RELIEVING_DATE"] == DBNull.Value
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(dr["RELIEVING_DATE"]);

                            // Service Days
                            response.TotalServiceDays =
                                dr["WORKED_DAYS"] == DBNull.Value
                                    ? 0
                                    : Convert.ToInt32(dr["WORKED_DAYS"]);

                            response.Days =
                                dr["DAYS"] == DBNull.Value
                                    ? 0
                                    : Convert.ToInt32(dr["DAYS"]);

                            // Amounts
                            response.PendingSalary =
                                dr["PENDING_SALARY"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["PENDING_SALARY"]);

                            response.UnPaidLeave =
                                dr["UNPAID_LEAVE_DAYS"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["UNPAID_LEAVE_DAYS"]);

                            response.UnPaidLeaveSalary =
                                dr["LEAVE_AMOUNT"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["LEAVE_AMOUNT"]);

                            response.EOSAmount =
                                dr["EOS_AMOUNT"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["EOS_AMOUNT"]);

                            response.Additions =
                                dr["ADD_AMOUNT"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["ADD_AMOUNT"]);

                            response.Deductions =
                                dr["DED_AMOUNT"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["DED_AMOUNT"]);

                            response.NetAmount =
                                dr["NET_AMOUNT"] == DBNull.Value
                                    ? 0
                                    : Convert.ToDecimal(dr["NET_AMOUNT"]);

                            // Remarks
                            string addRemarks = dr["ADD_REMARKS"] == DBNull.Value ? "" : dr["ADD_REMARKS"].ToString();
                            string dedRemarks = dr["DED_REMARKS"] == DBNull.Value ? "" : dr["DED_REMARKS"].ToString();

                            response.Remarks = $"{addRemarks} {dedRemarks}".Trim();
                        }
                    }
                }
            }

            return response;
        }
        public EmployeeEOSPaymentListResponseData GetAllEmployeeEOSPayments()
        {
            EmployeeEOSPaymentListResponseData response = new EmployeeEOSPaymentListResponseData();

            try
            {
                // response.data = new List<EmployeeEOSPaymentLogListData>();
                response.data = new List<EmployeeEOSPaymentData>();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_EMPLOYEE_EOS_PAYMENT_NEW", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            EmployeeEOSPaymentData item = new EmployeeEOSPaymentData();

                            item.ID = ADO.ToInt32(dr["ID"]);
                            item.DOC_NO = ADO.ToString(dr["DOC_NO"]);

                            item.DOC_DATE = dr["EOS_DATE"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(dr["EOS_DATE"]);

                            item.EMP_CODE = ADO.ToString(dr["EMP_CODE"]);
                            item.EMP_NAME = ADO.ToString(dr["EMP_NAME"]);

                          //  item.REASON = ADO.ToString(dr["REASON"]);

                            item.NET_AMOUNT = ADO.ToDecimal(dr["NET_AMOUNT"]);

                        //    item.DOC_STATUS = ADO.ToInt32(dr["DOC_STATUS"]);

                        //    item.TRANS_ID = ADO.ToInt64(dr["TRANS_ID"]);

                            response.data.Add(item);
                        }
                    }
                }

                response.flag = "1";
                response.message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = "0";
                response.message = ex.Message;
            }

            return response;
        }


        public SaveEmployeeEOSPaymentResponseData SaveData(SaveEmployeeEOSPaymentData eos)
        {
            
            try
            {
                SaveEmployeeEOSPaymentResponseData res =      new SaveEmployeeEOSPaymentResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS_PAYMENT_NEW";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);
                  

                    cmd.Parameters.AddWithValue("@EOS_ID", eos.EOS_ID);
                    cmd.Parameters.AddWithValue("@REASON_ID", eos.REASON_ID);

                     cmd.Parameters.AddWithValue("@COMPANY_ID", eos.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", eos.STORE_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", eos.FIN_ID);

                    cmd.Parameters.AddWithValue("@TRANS_ID", eos.TRANS_ID);

                    cmd.Parameters.AddWithValue("@TRANS_DATE",
                        (object)eos.TRANS_DATE ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@VOUCHER_NO",
                        (object)eos.VOUCHER_NO ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@PAY_HEAD_ID",
                        (object)eos.PAY_HEAD_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@PAY_TYPE_ID",
                        (object)eos.PAY_TYPE_ID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@CHEQUE_NO",
                        (object)eos.CHEQUE_NO ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@CHEQUE_DATE",
                        (object)eos.CHEQUE_DATE ?? DBNull.Value);

                    //cmd.Parameters.AddWithValue("@NARRATION",
                    //    (object)eos.NARRATION ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", eos.EOS_AMOUNT);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", eos.LEAVE_AMOUNT);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", eos.PENDING_SALARY);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", eos.ADD_AMOUNT);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", eos.DED_AMOUNT);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", eos.NET_AMOUNT);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", eos.ADD_REMARKS);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", eos.DED_REMARKS);

                    cmd.Parameters.AddWithValue("@USER_ID", eos.USER_ID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmdHold = new SqlCommand(
    "UPDATE TB_EMPLOYEE SET HOLD_SALARY = 1 WHERE ID=@EMP_ID",
    connection);

                    cmdHold.Parameters.AddWithValue("@EMP_ID", eos.EMP_ID);
                    cmdHold.ExecuteNonQuery();


                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaveEmployeeEOSPaymentData SelectEmployeeEOSPaymentData(int id)
        {
            SaveEmployeeEOSPaymentData eos = new SaveEmployeeEOSPaymentData();

            try
            {
                string strSQL = "SELECT eos.*, reason.DESCRIPTION as REASON, emp.EMP_NAME,trans.NARRATION AS REMARKS , " +
                    " stat.STATUS_DESC AS STATUS FROM TB_EMPLOYEE_EOS eos LEFT JOIN TB_AC_TRANS_HEADER trans " +
                    " ON eos.TRANS_ID = trans.TRANS_ID " +
                    " LEFT JOIN TB_STATUS stat " +
                    " ON trans.TRANS_STATUS = stat.ID " +
                    " INNER JOIN TB_EMPLOYEE emp ON emp.ID = eos.EMP_ID " +
                    " INNER JOIN TB_EOS_REASON reason ON reason.ID = eos.REASON_ID " +
                    " WHERE eos.ID = " + id;

                DataTable tblEos = ADO.GetDataTable(strSQL, "EmployeeEOS");

                if (tblEos.Rows.Count > 0)
                {
                    DataRow dr = tblEos.Rows[0];
                    eos.ID = ADO.ToInt32(dr["ID"]);
                    eos.DOC_NO = ADO.ToString(dr["DOC_NO"]);
                   // eos.EOS_DATE = Convert.ToDateTime(dr["EOS_DATE"]);
                    eos.EMP_ID = ADO.ToInt32(dr["EMP_ID"]);
                    eos.EMP_NAME = ADO.ToString(dr["EMP_NAME"]);
                    //eos.REASON_ID = ADO.ToInt32(dr["REASON_ID"]);
                    //eos.REASON_NAME = ADO.ToString(dr["REASON"]);
                    //eos.DAYS = ADO.ToString(dr["DAYS"]);
                    eos.EOS_AMOUNT = Convert.ToDecimal(ADO.ToString(dr["EOS_AMOUNT"]));
                    eos.LEAVE_AMOUNT = Convert.ToDecimal(ADO.ToString(dr["LEAVE_AMOUNT"]));
                    eos.PENDING_SALARY = Convert.ToDecimal(ADO.ToString(dr["PENDING_SALARY"]));
                    eos.ADD_AMOUNT = Convert.ToDecimal(ADO.ToString(dr["ADD_AMOUNT"]));
                    eos.DED_AMOUNT = Convert.ToDecimal(ADO.ToString(dr["DED_AMOUNT"]));
                    eos.ADD_REMARKS = ADO.ToString(dr["ADD_REMARKS"]);
                    eos.DED_REMARKS = ADO.ToString(dr["DED_REMARKS"]);
                    eos.REMARKS = ADO.ToString(dr["REMARKS"]);
                    eos.STATUS = ADO.ToString(dr["STATUS"]);
                    eos.TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]);
                    //eos.PAY_TRANS_ID = ADO.ToInt32(dr["PAY_TRANS_ID"]);
                    //eos.RELIEVING_DATE = dr["RELIEVING_DATE"] == DBNull.Value ? (DateTime?)null: Convert.ToDateTime(dr["RELIEVING_DATE"]);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return eos;
        }

        public SaveEmployeeEOSPaymentResponseData UpdateData(SaveEmployeeEOSPaymentData eos)
        {
            try
            {
                SaveEmployeeEOSPaymentResponseData res = new SaveEmployeeEOSPaymentResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS_PAYMENT_NEW";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    cmd.Parameters.AddWithValue("@EOS_ID", eos.EOS_ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FIN_ID", (object)eos.FIN_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    //cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    //cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                   // cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", (object)eos.EOS_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", (object)eos.LEAVE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", (object)eos.PENDING_SALARY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", (object)eos.ADD_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", (object)eos.DED_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", (object)eos.ADD_REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", (object)eos.DED_REMARKS ?? DBNull.Value);
                    //cmd.Parameters.AddWithValue("@RELIEVING_DATE", (object)eos.RELIEVING_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", (object)eos.NET_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TRANS_ID", (object)eos.TRANS_ID ?? DBNull.Value);
                   // cmd.Parameters.AddWithValue("@DOC_STATUS", (object)eos.DOC_STATUS ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                    if (eos.DOC_STATUS == 3)   // Approved3
                    {
                        SqlCommand cmdEmp = new SqlCommand(
                            @"UPDATE TB_EMPLOYEE SET EMP_STATUS = @STATUS WHERE ID = @EMP_ID", connection);

                        cmdEmp.Parameters.AddWithValue("@STATUS", 2);   // Left Service
                        cmdEmp.Parameters.AddWithValue("@EMP_ID", eos.EMP_ID);

                        cmdEmp.ExecuteNonQuery();
                    }
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaveEmployeeEOSPaymentResponseData DeleteEmployeeEOSPayment(int id)
        {
            try
            {
                SaveEmployeeEOSPaymentResponseData res = new SaveEmployeeEOSPaymentResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS_PAYMENT_NEW";
                    //cmd.Parameters.AddWithValue("@ACTION", 3);
                    //cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@ACTION", 6);
                    cmd.Parameters.AddWithValue("@EOS_ID", id);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaveEmployeeEOSPaymentResponseData VerifyData(SaveEmployeeEOSPaymentData eos)
        {
            try
            {
                SaveEmployeeEOSPaymentResponseData res = new SaveEmployeeEOSPaymentResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS_PAYMENT_NEW";

                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@EOS_ID", eos.ID);
                   // cmd.Parameters.AddWithValue("ID", eos.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    //cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    //cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                     cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                   // cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", (object)eos.EOS_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", (object)eos.LEAVE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", (object)eos.PENDING_SALARY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", (object)eos.ADD_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", (object)eos.DED_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", (object)eos.ADD_REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", (object)eos.DED_REMARKS ?? DBNull.Value);
                   // cmd.Parameters.AddWithValue("@RELIEVING_DATE", (object)eos.RELIEVING_DATE ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaveEmployeeEOSPaymentResponseData ApproveData(SaveEmployeeEOSPaymentData eos)
        {
            try
            {
                SaveEmployeeEOSPaymentResponseData res = new SaveEmployeeEOSPaymentResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_EMPLOYEE_EOS_PAYMENT_NEW";

                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    // cmd.Parameters.AddWithValue("ID", eos.ID);
                    cmd.Parameters.AddWithValue("@EOS_ID", eos.ID);
                    cmd.Parameters.AddWithValue("@USER_ID", (object)eos.USER_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STORE_ID", (object)eos.STORE_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EMP_ID", (object)eos.EMP_ID ?? DBNull.Value);

                    //cmd.Parameters.AddWithValue("@EOS_DATE", eos.EOS_DATE != null ? Convert.ToDateTime(eos.EOS_DATE) : DBNull.Value);
                    //cmd.Parameters.AddWithValue("@DAYS", (object)eos.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REASON_ID", (object)eos.REASON_ID ?? DBNull.Value);

                //    cmd.Parameters.AddWithValue("@REMARKS", (object)eos.REMARKS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@EOS_AMOUNT", (object)eos.EOS_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LEAVE_AMOUNT", (object)eos.LEAVE_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PENDING_SALARY", (object)eos.PENDING_SALARY ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_AMOUNT", (object)eos.ADD_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_AMOUNT", (object)eos.DED_AMOUNT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ADD_REMARKS", (object)eos.ADD_REMARKS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DED_REMARKS", (object)eos.DED_REMARKS ?? DBNull.Value);
                  //  cmd.Parameters.AddWithValue("@RELIEVING_DATE", (object)eos.RELIEVING_DATE ?? DBNull.Value);

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
