using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Net.Mail;

namespace MicroApi.DataLayer.Service
{
    public class TimeSheetService : ITimeSheetService
    {
        public TimeSheetLogListResponseData GetAllTimeSheet()
        {
            TimeSheetLogListResponseData loglist = new TimeSheetLogListResponseData();
            loglist.data = new List<TimeSheetLogListData>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_TIMESHEET";
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    loglist.data.Add(new TimeSheetLogListData
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        TS_MONTH = Convert.IsDBNull(dr["TS_MONTH"]) ? null : Convert.ToDateTime(dr["TS_MONTH"]).ToString("MMMM yyyy"),
                        EMP_NO = Convert.IsDBNull(dr["EMP_CODE"]) ? null : Convert.ToString(dr["EMP_CODE"]),
                        EMP_ID = Convert.IsDBNull(dr["EMP_ID"]) ? null : Convert.ToString(dr["EMP_ID"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        WORKED_DAYS = Convert.IsDBNull(dr["WORKED_DAYS"]) ? null : Convert.ToString(dr["WORKED_DAYS"]),
                        OT_HOURS = Convert.IsDBNull(dr["NORMAL_OT"]) ? null : Convert.ToString(dr["NORMAL_OT"]),
                        STATUS = Convert.IsDBNull(dr["STATUS_DESC"]) ? null : Convert.ToString(dr["STATUS_DESC"]),
                    });
                }
                connection.Close();
            }
            loglist.flag = "1";
            loglist.message = "Success";

            return loglist;
        }

        public saveTimeSheetResponseData SaveData(saveTimeSheetData ts)
        {
            try
            {
                saveTimeSheetResponseData res = new saveTimeSheetResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TIMESHEET";

                    cmd.Parameters.AddWithValue("@ACTION", 1);

                    //cmd.Parameters.AddWithValue("ID", employee.ID);

                    cmd.Parameters.AddWithValue("EMP_ID", (object)ts.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("TS_MONTH", string.IsNullOrEmpty(ts.TS_MONTH) ? DBNull.Value : DateTime.ParseExact(ts.TS_MONTH, "MMMM yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("DAYS", (object)ts.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("NORMAL_OT", (object)ts.NORMAL_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("HOLIDAY_OT", (object)ts.HOLIDAY_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_FROM", string.IsNullOrEmpty(ts.LEAVE_FROM) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_FROM, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("LEAVE_TO", string.IsNullOrEmpty(ts.LEAVE_TO) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_TO, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("WORKED_DAYS", (object)ts.WORKED_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DAYS_DEDUCTED", (object)ts.DAYS_DEDUCTED ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)ts.REMARKS ?? DBNull.Value);

                    DataTable tbl1 = new DataTable();

                    tbl1.Columns.Add("STORE_ID", typeof(Int32));
                    tbl1.Columns.Add("DAYS", typeof(float));
                    tbl1.Columns.Add("NORMAL_OT", typeof(float));
                    tbl1.Columns.Add("HOLIDAY_OT", typeof(float));

                    foreach (saveTimeSheetDetailData ur1 in ts.TIMESHEET_DETAIL)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["DAYS"] = ur1.DAYS;
                        dRow1["NORMAL_OT"] = ur1.NORMAL_OT;
                        dRow1["HOLIDAY_OT"] = ur1.HOLIDAY_OT;
                        tbl1.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_DETAIL", tbl1);

                    DataTable tbl2 = new DataTable();

                    tbl2.Columns.Add("SALARY_HEAD_ID", typeof(Int32));
                    tbl2.Columns.Add("AMOUNT", typeof(float));

                    foreach (saveTimeSheetSalaryData ur1 in ts.TIMESHEET_SALARY)
                    {
                        DataRow dRow1 = tbl2.NewRow();
                        dRow1["SALARY_HEAD_ID"] = ur1.SALARY_HEAD_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl2.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_SALARY", tbl2);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveTimeSheetData selectTimeSheetData(int id)
        {
            saveTimeSheetData ts = new saveTimeSheetData();
            ts.TIMESHEET_DETAIL = new List<saveTimeSheetDetailData>();
            ts.TIMESHEET_SALARY = new List<saveTimeSheetSalaryData>();

            try
            {
                string strSQL = " SELECT header.ID, header.TS_MONTH , header.EMP_ID, header.TOTAL_DAYS, header.NORMAL_OT , " +
                    " header.HOLIDAY_OT, header.LEAVE_FROM, header.LEAVE_TO, header.WORKED_DAYS, " +
                    " header.DEDUCT_DAYS, header.REMARKS , status.STATUS_DESC as STATUS " +
                    " FROM TB_PAY_TS_HEADER header " +
                    " LEFT JOIN TB_STATUS status ON " +
                    " status.ID = header.TS_STATUS " +
                    " WHERE header.ID =" + id;

                DataTable tblHeader = ADO.GetDataTable(strSQL, "TSHeader");

                if (tblHeader.Rows.Count > 0)
                {
                    DataRow dr = tblHeader.Rows[0];
                    ts.ID = ADO.ToInt32(dr["ID"]);
                    ts.TS_MONTH = Convert.ToDateTime(dr["TS_MONTH"]).ToString("MMMM yyyy");
                    ts.EMP_ID = ADO.ToString(dr["EMP_ID"]);
                    ts.DAYS = ADO.ToString(dr["TOTAL_DAYS"]);
                    ts.NORMAL_OT = ADO.ToString(dr["NORMAL_OT"]);
                    ts.HOLIDAY_OT = ADO.ToString(dr["HOLIDAY_OT"]);
                    ts.LEAVE_FROM = ADO.ToString(dr["LEAVE_FROM"]);
                    ts.LEAVE_TO = ADO.ToString(dr["LEAVE_TO"]);
                    ts.WORKED_DAYS = ADO.ToString(dr["WORKED_DAYS"]);
                    ts.DAYS_DEDUCTED = ADO.ToString(dr["DEDUCT_DAYS"]);
                    ts.REMARKS = ADO.ToString(dr["REMARKS"]);
                    ts.STATUS = ADO.ToString(dr["STATUS"]);
                }


                string strSQL1 = " SELECT detail.STORE_ID, store.STORE_NAME , detail.DAYS, detail.NORMAL_OT, " +
                    "detail.HOLIDAY_OT FROM TB_PAY_TS_DETAIL detail INNER JOIN TB_PAY_TS_HEADER header " +
                    " ON detail.TS_ID = header.ID  " +
                    "INNER JOIN TB_STORES store ON store.ID = detail.STORE_ID " +
                    " WHERE detail.TS_ID = " + id;

                DataTable tblDetail = ADO.GetDataTable(strSQL1, "TSDetail");

                if (tblDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in tblDetail.Rows)
                    {
                        ts.TIMESHEET_DETAIL.Add(new saveTimeSheetDetailData
                        {
                            STORE_ID = ADO.ToString(dr["STORE_ID"]),
                            STORE_NAME = ADO.ToString(dr["STORE_NAME"]),
                            DAYS = ADO.ToString(dr["DAYS"]),
                            NORMAL_OT = ADO.ToString(dr["NORMAL_OT"]),
                            HOLIDAY_OT = ADO.ToString(dr["HOLIDAY_OT"])
                        });
                    }
 
                }

                string strSQL2 = " SELECT salary.HEAD_ID, salhead.HEAD_NAME,salary.AMOUNT " +
                    " from TB_PAY_TS_SALARY salary INNER JOIN TB_PAY_TS_HEADER header ON " +
                    " header.ID = salary.TS_ID " +
                    " INNER JOIN TB_SALARY_HEAD salhead " +
                    " ON salhead.ID = salary.HEAD_ID " +
                    " WHERE salary.TS_ID = " + id;

                DataTable tblSalary = ADO.GetDataTable(strSQL2, "TSSalary");

                if (tblSalary.Rows.Count > 0)
                {
                    foreach (DataRow dr in tblSalary.Rows)
                    {
                        ts.TIMESHEET_SALARY.Add(new saveTimeSheetSalaryData
                        {
                   
                            SALARY_HEAD_ID = ADO.ToString(dr["HEAD_ID"]),
                            SALARY_HEAD_NAME = ADO.ToString(dr["HEAD_NAME"]),
                            AMOUNT = ADO.ToString(dr["AMOUNT"]),
                           
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ts;
        }

        public saveTimeSheetResponseData UpdateData(saveTimeSheetData ts)
        {
            try
            {
                saveTimeSheetResponseData res = new saveTimeSheetResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TIMESHEET";

                    cmd.Parameters.AddWithValue("@ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", ts.ID);

                    cmd.Parameters.AddWithValue("EMP_ID", (object)ts.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("TS_MONTH", string.IsNullOrEmpty(ts.TS_MONTH) ? DBNull.Value : DateTime.ParseExact(ts.TS_MONTH, "MMMM yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("DAYS", (object)ts.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("NORMAL_OT", (object)ts.NORMAL_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("HOLIDAY_OT", (object)ts.HOLIDAY_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_FROM", string.IsNullOrEmpty(ts.LEAVE_FROM) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_FROM, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("LEAVE_TO", string.IsNullOrEmpty(ts.LEAVE_TO) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_TO, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("WORKED_DAYS", (object)ts.WORKED_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DAYS_DEDUCTED", (object)ts.DAYS_DEDUCTED ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)ts.REMARKS ?? DBNull.Value);

                    DataTable tbl1 = new DataTable();

                    tbl1.Columns.Add("STORE_ID", typeof(Int32));
                    tbl1.Columns.Add("DAYS", typeof(float));
                    tbl1.Columns.Add("NORMAL_OT", typeof(float));
                    tbl1.Columns.Add("HOLIDAY_OT", typeof(float));

                    foreach (saveTimeSheetDetailData ur1 in ts.TIMESHEET_DETAIL)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["DAYS"] = ur1.DAYS;
                        dRow1["NORMAL_OT"] = ur1.NORMAL_OT;
                        dRow1["HOLIDAY_OT"] = ur1.HOLIDAY_OT;
                        tbl1.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_DETAIL", tbl1);

                    DataTable tbl2 = new DataTable();

                    tbl2.Columns.Add("SALARY_HEAD_ID", typeof(Int32));
                    tbl2.Columns.Add("AMOUNT", typeof(float));

                    foreach (saveTimeSheetSalaryData ur1 in ts.TIMESHEET_SALARY)
                    {
                        DataRow dRow1 = tbl2.NewRow();
                        dRow1["SALARY_HEAD_ID"] = ur1.SALARY_HEAD_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl2.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_SALARY", tbl2);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveTimeSheetResponseData DeleteTimeSheet(int id)
        {
            try
            {
                saveTimeSheetResponseData res = new saveTimeSheetResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TIMESHEET";
                    cmd.Parameters.AddWithValue("@ACTION", 3);
                    cmd.Parameters.AddWithValue("@ID", id);
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


        public saveTimeSheetResponseData VerifyData(saveTimeSheetData ts)
        {
            try
            {
                saveTimeSheetResponseData res = new saveTimeSheetResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TIMESHEET";

                    cmd.Parameters.AddWithValue("@ACTION", 4);

                    cmd.Parameters.AddWithValue("ID", ts.ID);

                    cmd.Parameters.AddWithValue("EMP_ID", (object)ts.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("TS_MONTH", string.IsNullOrEmpty(ts.TS_MONTH) ? DBNull.Value : DateTime.ParseExact(ts.TS_MONTH, "MMMM yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("DAYS", (object)ts.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("NORMAL_OT", (object)ts.NORMAL_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("HOLIDAY_OT", (object)ts.HOLIDAY_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_FROM", string.IsNullOrEmpty(ts.LEAVE_FROM) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_FROM, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("LEAVE_TO", string.IsNullOrEmpty(ts.LEAVE_TO) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_TO, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("WORKED_DAYS", (object)ts.WORKED_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DAYS_DEDUCTED", (object)ts.DAYS_DEDUCTED ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)ts.REMARKS ?? DBNull.Value);

                    DataTable tbl1 = new DataTable();

                    tbl1.Columns.Add("STORE_ID", typeof(Int32));
                    tbl1.Columns.Add("DAYS", typeof(float));
                    tbl1.Columns.Add("NORMAL_OT", typeof(float));
                    tbl1.Columns.Add("HOLIDAY_OT", typeof(float));

                    foreach (saveTimeSheetDetailData ur1 in ts.TIMESHEET_DETAIL)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["DAYS"] = ur1.DAYS;
                        dRow1["NORMAL_OT"] = ur1.NORMAL_OT;
                        dRow1["HOLIDAY_OT"] = ur1.HOLIDAY_OT;
                        tbl1.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_DETAIL", tbl1);

                    DataTable tbl2 = new DataTable();

                    tbl2.Columns.Add("SALARY_HEAD_ID", typeof(Int32));
                    tbl2.Columns.Add("AMOUNT", typeof(float));

                    foreach (saveTimeSheetSalaryData ur1 in ts.TIMESHEET_SALARY)
                    {
                        DataRow dRow1 = tbl2.NewRow();
                        dRow1["SALARY_HEAD_ID"] = ur1.SALARY_HEAD_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl2.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_SALARY", tbl2);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public saveTimeSheetResponseData ApproveData(saveTimeSheetData ts)
        {
            try
            {
                saveTimeSheetResponseData res = new saveTimeSheetResponseData();
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_TIMESHEET";

                    cmd.Parameters.AddWithValue("@ACTION", 5);

                    cmd.Parameters.AddWithValue("ID", ts.ID);

                    cmd.Parameters.AddWithValue("EMP_ID", (object)ts.EMP_ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("TS_MONTH", string.IsNullOrEmpty(ts.TS_MONTH) ? DBNull.Value : DateTime.ParseExact(ts.TS_MONTH, "MMMM yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("DAYS", (object)ts.DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("NORMAL_OT", (object)ts.NORMAL_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("HOLIDAY_OT", (object)ts.HOLIDAY_OT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("LEAVE_FROM", string.IsNullOrEmpty(ts.LEAVE_FROM) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_FROM, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("LEAVE_TO", string.IsNullOrEmpty(ts.LEAVE_TO) ? DBNull.Value : DateTime.ParseExact(ts.LEAVE_TO, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("WORKED_DAYS", (object)ts.WORKED_DAYS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("DAYS_DEDUCTED", (object)ts.DAYS_DEDUCTED ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("REMARKS", (object)ts.REMARKS ?? DBNull.Value);

                    DataTable tbl1 = new DataTable();

                    tbl1.Columns.Add("STORE_ID", typeof(Int32));
                    tbl1.Columns.Add("DAYS", typeof(float));
                    tbl1.Columns.Add("NORMAL_OT", typeof(float));
                    tbl1.Columns.Add("HOLIDAY_OT", typeof(float));

                    foreach (saveTimeSheetDetailData ur1 in ts.TIMESHEET_DETAIL)
                    {
                        DataRow dRow1 = tbl1.NewRow();
                        dRow1["STORE_ID"] = ur1.STORE_ID;
                        dRow1["DAYS"] = ur1.DAYS;
                        dRow1["NORMAL_OT"] = ur1.NORMAL_OT;
                        dRow1["HOLIDAY_OT"] = ur1.HOLIDAY_OT;
                        tbl1.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_DETAIL", tbl1);

                    DataTable tbl2 = new DataTable();

                    tbl2.Columns.Add("SALARY_HEAD_ID", typeof(Int32));
                    tbl2.Columns.Add("AMOUNT", typeof(float));

                    foreach (saveTimeSheetSalaryData ur1 in ts.TIMESHEET_SALARY)
                    {
                        DataRow dRow1 = tbl2.NewRow();
                        dRow1["SALARY_HEAD_ID"] = ur1.SALARY_HEAD_ID;
                        dRow1["AMOUNT"] = ur1.AMOUNT;
                        tbl2.Rows.Add(dRow1);
                    }

                    cmd.Parameters.AddWithValue("@UDT_TB_TIMESHEET_SALARY", tbl2);

                    cmd.ExecuteNonQuery();
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TimeSheetHeaderListResponseData GetTimeSheetByCompanyAndMonth(int companyId, DateTime month)
        {
            TimeSheetHeaderListResponseData logList = new TimeSheetHeaderListResponseData();
            logList.data = new List<TimeSheetHeader>();

            using (SqlConnection connection = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TIMESHEET_HEADER", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", 1);
                cmd.Parameters.AddWithValue("@CompanyId", companyId);
                cmd.Parameters.AddWithValue("@Month", month);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    logList.data.Add(new TimeSheetHeader
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        EMP_NAME = Convert.IsDBNull(dr["EMP_NAME"]) ? null : Convert.ToString(dr["EMP_NAME"]),
                        EMP_NO = Convert.IsDBNull(dr["EMP_NO"]) ? null : Convert.ToString(dr["EMP_NO"]),
                        EMP_ID = Convert.IsDBNull(dr["EMP_ID"]) ? null : Convert.ToString(dr["EMP_ID"]),
                        WORKED_DAYS = Convert.IsDBNull(dr["WORKED_DAYS"]) ? null : Convert.ToString(dr["WORKED_DAYS"]),
                        OT_HOURS = Convert.IsDBNull(dr["OT_HOURS"]) ? null : Convert.ToString(dr["OT_HOURS"]),
                        LESS_HOURS = Convert.IsDBNull(dr["LESS_HOURS"]) ? null : Convert.ToString(dr["LESS_HOURS"]),
                        STATUS = Convert.IsDBNull(dr["STATUS"]) ? null : Convert.ToString(dr["STATUS"])
                    });
                }
                connection.Close();
            }

            logList.flag = "1";
            logList.message = "Success";
            return logList;
        }

        public TimeSheetHeaderListResponseData ApproveTimeSheets(ApproveRequest request)
        {
            TimeSheetHeaderListResponseData response = new TimeSheetHeaderListResponseData();
            response.data = new List<TimeSheetHeader>();

            using (SqlConnection connection = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TIMESHEET_HEADER", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 2);
                cmd.Parameters.AddWithValue("@IDs", request.IDs);

                connection.Close();
            }
            response.flag = "1";
            response.message = "Approval completed successfully.";
            return response;
        }

        public TimeSheetHeaderListResponseData GetSalaryPendingTimeSheets(TimeSheetRequest request)
        {
            TimeSheetHeaderListResponseData response = new TimeSheetHeaderListResponseData();
            response.data = new List<TimeSheetHeader>();

            using (SqlConnection connection = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TIMESHEET_HEADER", connection))
            {
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@CompanyId", request.CompanyId);
                cmd.Parameters.AddWithValue("@Month", request.Month);

               
                SqlDataReader reader = cmd.ExecuteReader();
                response.data = new List<TimeSheetHeader>();
                while (reader.Read())
                {
                    response.data.Add(new TimeSheetHeader
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        EMP_NAME = reader["EMP_NAME"].ToString(),
                        EMP_NO = reader["EMP_NO"].ToString(),
                        EMP_ID = Convert.ToInt32(reader["EMP_ID"]),
                        //COMPANY_ID = Convert.ToInt32(reader["COMPANY_ID"]),
                        //TS_MONTH = Convert.ToDateTime(reader["TS_MONTH"]),
                        WORKED_DAYS = Convert.ToSingle(reader["WORKED_DAYS"]),
                        OT_HOURS = Convert.ToSingle(reader["OT_HOURS"]),
                        LESS_HOURS = Convert.ToSingle(reader["LESS_HOURS"]),
                        STATUS = reader["STATUS"].ToString(),
                        //SALARY_ID = Convert.ToInt32(reader["SALARY_ID"]),
                        //AMOUNT = Convert.ToDecimal(reader["AMOUNT"])
                    });
                }
                connection.Close();
            }
            response.flag = "1";
            response.message = "Success";
            return response;
        }


    }

}
