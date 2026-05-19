using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ImportARService:IImportARService
    {


        public ImportArColumnsResponse columnsList()
        {
            ImportArColumnsResponse res = new ImportArColumnsResponse();
            res.data = new List<ImportArColumns>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;

                        string reportId = "import-ar"; 

                        cmd.CommandText = @"
                    SELECT 
                        ColName,
                        ColTitle,
                        ColType
                    FROM TB_REPORT_COLUMNS
                    WHERE ReportID = @ReportID
                    ORDER BY ID";

                        cmd.Parameters.AddWithValue("@ReportID", reportId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new ImportArColumns
                                {
                                    ColumnName = reader["ColName"]?.ToString() ?? "",
                                    ColumnTitle = reader["ColTitle"]?.ToString() ?? "",
                                    ColumnType = reader["ColType"]?.ToString() ?? ""
                                });
                            }
                        }
                    }
                }

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = "Error: " + ex.Message;
            }

            return res;
        }

        public bool Import(ImportARInput vInput)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                // Create DataTables for each category
                DataTable tblAR = CreateARDataTable(vInput);

                // SqlCommand setup
                SqlCommand cmd = new SqlCommand

                {
                    Connection = connection,
                    Transaction = objtrans,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_IMPORT_AR",
                    CommandTimeout = 0
                };

                cmd.Parameters.AddWithValue("@CompanyID", vInput.CompanyID);
                cmd.Parameters.AddWithValue("@UserID", vInput.UserID);
                cmd.Parameters.AddWithValue("@FileName", vInput.FileName);
                cmd.Parameters.AddWithValue("@BatchNo", vInput.BatchNo);
                cmd.Parameters.AddWithValue("@Action", 1);

                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_AR_DATA", tblAR);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.UDT_TB_IMPORT_AR_DATA";


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


        public ImportARResponse List(ImportARInput vInput)
        {
            ImportARResponse res = new ImportARResponse();
            res.data = new List<ImportARLog>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                                        SELECT L.*, 
                                               C.COMPANY_NAME, 
                                               U.USER_NAME
                                        FROM TB_IMPORT_AR_LOG L
                                        INNER JOIN TB_COMPANY_MASTER C 
                                            ON C.ID = L.CompanyID
                                        INNER JOIN TB_USERS U 
                                            ON U.USER_ID = L.UserID
                                        WHERE L.CompanyID = @CompanyID
                                        ORDER BY L.Time DESC";

                        cmd.Parameters.AddWithValue("@CompanyID", vInput.CompanyID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new ImportARLog
                                {
                                    ID = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                                    DocNo = reader["DocNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DocNo"]),
                                    FileName = reader["FileName"]?.ToString() ?? "",
                                    ImportedBy = reader["USER_NAME"]?.ToString() ?? "",
                                    ImportedTime = reader["Time"] == DBNull.Value
                                                    ? DateTime.MinValue
                                                    : Convert.ToDateTime(reader["Time"])
                                });
                            }
                        }
                    }
                }

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = "Error: " + ex.Message;
            }

            return res;
        }


        public viewImportARDataResponse ViewDetails(viewImportARInput vInput)
        {
            viewImportARDataResponse res = new viewImportARDataResponse();
            res.data = new List<ImportARData>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT D.*
                                            FROM TB_IMPORT_AR_DATA D
                                            INNER JOIN TB_IMPORT_AR_HEADER H
                                                ON H.ID = D.HeaderID
                                            WHERE H.LogID = @LogID";

                        cmd.Parameters.AddWithValue("@LogID", vInput.LogID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new ImportARData
                                {
                                    ID = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),

                                    TransactionID = reader["TransactionID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionID"]),

                                    InvoiceType = reader["InvoiceType"]?.ToString() ?? "",

                                    TransactionType = reader["TransactionType"]?.ToString() ?? "",

                                    TransactionIncomeGroup = reader["TransactionIncomeGroup"]?.ToString() ?? "",

                                    ApexTransactionNumber = reader["ApexTransactionNumber"]?.ToString() ?? "",

                                    ApexTransactionDate = reader["ApexTransactionDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["ApexTransactionDate"]),

                                    ApexPatientCode = reader["ApexPatientCode"]?.ToString() ?? "",

                                    ApexTPACode = reader["ApexTPACode"]?.ToString() ?? "",

                                    ApexInsuCode = reader["ApexInsuCode"]?.ToString() ?? "",

                                    ApexInstCode = reader["ApexInstCode"]?.ToString() ?? "",

                                    ApexReportingDoctor = reader["ApexReportingDoctor"]?.ToString() ?? "",

                                    ApexReferringDoctor = reader["ApexReferringDoctor"]?.ToString() ?? "",

                                    ApexReportingDoctorDept = reader["ApexReportingDoctorDept"]?.ToString() ?? "",

                                    ApexReferringDoctorDept = reader["ApexReferringDoctorDept"]?.ToString() ?? "",

                                    IncomeGroupServiceCount = reader["IncomeGroupServiceCount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["IncomeGroupServiceCount"]),

                                    IncomeGrossAmount = reader["IncomeGrossAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeGrossAmount"]),

                                    IncomePolicyConcAmount = reader["IncomePolicyConcAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomePolicyConcAmount"]),

                                    IncomeAddlConcAmount = reader["IncomeAddlConcAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeAddlConcAmount"]),

                                    IncomeNetAmount = reader["IncomeNetAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeNetAmount"]),

                                    IncomePatientAmount = reader["IncomePatientAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomePatientAmount"]),

                                    IncomeInstAmount = reader["IncomeInstAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeInstAmount"]),

                                    IncomeInsuAmount = reader["IncomeInsuAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeInsuAmount"]),

                                    PatientInsuCardNumber = reader["PatientInsuCardNumber"]?.ToString() ?? "",

                                    PatientEmployeeCode = reader["PatientEmployeeCode"]?.ToString() ?? "",

                                    Paymode = reader["Paymode"]?.ToString() ?? "",

                                    PaymodeGateway = reader["PaymodeGateway"]?.ToString() ?? "",

                                    PaymodeAmount = reader["PaymodeAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["PaymodeAmount"]),

                                    PaymentRefNo = reader["PaymentRefNo"]?.ToString() ?? "",

                                    DenialCode = reader["DenialCode"]?.ToString() ?? "",

                                    AmtInLocalCurrency = reader["AmtInLocalCurrency"]?.ToString() ?? "",

                                    RecordEnteredBy = reader["RecordEnteredBy"]?.ToString() ?? "",

                                    RecordStatus = reader["RecordStatus"]?.ToString() ?? "",

                                    PostedFlag = reader["PostedFlag"]?.ToString() ?? "",

                                    GPRefNo = reader["GPRefNo"]?.ToString() ?? "",

                                    ReferenceDocNumber = reader["ReferenceDocNumber"]?.ToString() ?? "",

                                    ReferenceDocDate = reader["ReferenceDocDate"]?.ToString() ?? "",

                                    PatientName = reader["PatientName"]?.ToString() ?? "",

                                    ServiceCategory = reader["ServiceCategory"]?.ToString() ?? "",

                                    ApexTransferDate = reader["ApexTransferDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["ApexTransferDate"]),

                                    AXTransferDate = reader["AXTransferDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["AXTransferDate"]),

                                    PatientVATAmt = reader["PatientVATAmt"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["PatientVATAmt"]),

                                    InsuranceVATAmt = reader["InsuranceVATAmt"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["InsuranceVATAmt"]),

                                    CorporateVATAmt = reader["CorporateVATAmt"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["CorporateVATAmt"]),

                                    VATPerc = reader["VATPerc"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["VATPerc"]),

                                    VATType = reader["VATType"]?.ToString() ?? "",

                                    VATLink = reader["VATLink"]?.ToString() ?? "",

                                    ServiceCode = reader["ServiceCode"]?.ToString() ?? "",

                                    ServiceName = reader["ServiceName"]?.ToString() ?? "",

                                    CPTCode = reader["CPTCode"]?.ToString() ?? "",

                                    OrgnBranchCode = reader["OrgnBranchCode"]?.ToString() ?? "",

                                    Verified = reader["Verified"] == DBNull.Value
                                        ? false
                                        : Convert.ToBoolean(reader["Verified"]),

                                    PatientCreditCardHolderName = reader["PatientCreditCardHolderName"]?.ToString() ?? "",

                                    ReceiptCardRemarks = reader["ReceiptCardRemarks"]?.ToString() ?? "",

                                    CurrencyACode = reader["CurrencyACode"]?.ToString() ?? "",

                                    CurrencyRateConversion = reader["CurrencyRateConversion"] == DBNull.Value
                                                            ? 0
                                                            : Convert.ToDecimal(reader["CurrencyRateConversion"]),

                                    AmtInForeignCurrency = reader["AmtInForeignCurrency"] == DBNull.Value
                                                            ? 0
                                                            : Convert.ToDecimal(reader["AmtInForeignCurrency"]),
                                    PatientCreditCardNumber = reader["PatientCreditCardNumber"]?.ToString() ?? "",
                                    PatientCreditCardExpDt = reader["PatientCreditCardExpDt"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["PatientCreditCardExpDt"]),
                                    Status = reader["Status"] == DBNull.Value
                                            ? "Open"
                                            : Convert.ToInt32(reader["Status"]) == 1
                                                ? "Posted"
                                                : Convert.ToInt32(reader["Status"]) == -1
                                                    ? "Failed"
                                                    : "Open",
                                    FailureReason = reader["FailureReason"]?.ToString() ?? "",
                                });
                            }
                        }
                    }
                }

                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
            }

            return res;
        }


        public processARResponse processAR(processARInput vinput)
        {
            processARResponse res = new processARResponse();

            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    Transaction = objtrans,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_PROCESS_AR",
                    CommandTimeout = 0
                };

                cmd.Parameters.AddWithValue("@TransactionID", vinput.TransactionID);
                cmd.Parameters.AddWithValue("@COMPANY_ID ", vinput.CompanyID);
                cmd.Parameters.AddWithValue("@STORE_ID ", vinput.StoreID);
                cmd.Parameters.AddWithValue("@FIN_ID  ", vinput.FinID);
                cmd.Parameters.AddWithValue("@USER_ID  ", vinput.UserID);

                cmd.ExecuteNonQuery();

                objtrans.Commit();

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                objtrans.Rollback();

                res.flag = "0";
                res.message = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }



        public ARResponse arList()
        {
            ARResponse res = new ARResponse();

            res.header = new List<ARHeader>();
            res.detail = new List<ARDetail>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    // HEADER
                    using (SqlCommand cmdHeader = new SqlCommand())
                    {
                        cmdHeader.Connection = connection;
                        cmdHeader.CommandType = CommandType.Text;

                        cmdHeader.CommandText = @"
                SELECT *
                FROM TB_IMPORT_AR_HEADER";

                        using (SqlDataReader reader = cmdHeader.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.header.Add(new ARHeader
                                {
                                    HeaderID = reader["ID"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["ID"]),

                                    InvoiceType = reader["InvoiceType"]?.ToString(),

                                    TransactionType = reader["TransactionType"]?.ToString(),

                                    ApexTransactionNumber = reader["ApexTransactionNumber"]?.ToString(),

                                    ApexTransactionDate = reader["ApexTransactionDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["ApexTransactionDate"]),

                                    ApexPatientCode = reader["ApexPatientCode"]?.ToString(),

                                    ApexTPACode = reader["ApexTPACode"]?.ToString(),

                                    ApexInsuCode = reader["ApexInsuCode"]?.ToString(),

                                    ApexInstCode = reader["ApexInstCode"]?.ToString(),
                                    AcTransID = reader["AcTransID"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["AcTransID"]),

                                    Status = reader["Status"] == DBNull.Value
                                        ? "Open"
                                        : Convert.ToInt32(reader["Status"]) == 1
                                            ? "Posted"
                                            : Convert.ToInt32(reader["Status"]) == -1
                                                ? "Failed"
                                                : "Open",

                                    FailureReason = reader["FailureReason"]?.ToString()
                                });
                            }
                        }
                    }

                    // DETAIL
                    using (SqlCommand cmdDetail = new SqlCommand())
                    {
                        cmdDetail.Connection = connection;
                        cmdDetail.CommandType = CommandType.Text;

                        cmdDetail.CommandText = @"
                SELECT 
                    D.*,
                    L.FileName,
                    L.Time AS ImportedDate
                FROM TB_IMPORT_AR_DATA D
                INNER JOIN TB_IMPORT_AR_HEADER H
                    ON H.ID = D.HeaderID
                INNER JOIN TB_IMPORT_AR_LOG L
                    ON L.ID = H.LogID";

                        using (SqlDataReader reader = cmdDetail.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.detail.Add(new ARDetail
                                {
                                    HeaderID = reader["HeaderID"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["HeaderID"]),

                                    TransactionID = reader["TransactionID"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["TransactionID"]),

                                    InvoiceType = reader["InvoiceType"]?.ToString(),

                                    TransactionType = reader["TransactionType"]?.ToString(),

                                    TransactionIncomeGroup = reader["TransactionIncomeGroup"]?.ToString(),

                                    ApexTransactionNumber = reader["ApexTransactionNumber"]?.ToString(),

                                    ApexTransactionDate = reader["ApexTransactionDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["ApexTransactionDate"]),

                                    ApexPatientCode = reader["ApexPatientCode"]?.ToString(),

                                    ApexTPACode = reader["ApexTPACode"]?.ToString(),

                                    ApexInsuCode = reader["ApexInsuCode"]?.ToString(),

                                    ApexInstCode = reader["ApexInstCode"]?.ToString(),

                                    ApexReportingDoctor = reader["ApexReportingDoctor"]?.ToString(),

                                    ApexReferringDoctor = reader["ApexReferringDoctor"]?.ToString(),

                                    ApexReportingDoctorDept = reader["ApexReportingDoctorDept"]?.ToString(),

                                    ApexReferringDoctorDept = reader["ApexReferringDoctorDept"]?.ToString(),

                                    IncomeGroupServiceCount = reader["IncomeGroupServiceCount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["IncomeGroupServiceCount"]),

                                    IncomeGrossAmount = reader["IncomeGrossAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeGrossAmount"]),

                                    IncomePolicyConcAmount = reader["IncomePolicyConcAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomePolicyConcAmount"]),

                                    IncomeAddlConcAmount = reader["IncomeAddlConcAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeAddlConcAmount"]),

                                    IncomeNetAmount = reader["IncomeNetAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeNetAmount"]),

                                    IncomePatientAmount = reader["IncomePatientAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomePatientAmount"]),

                                    IncomeInstAmount = reader["IncomeInstAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeInstAmount"]),

                                    IncomeInsuAmount = reader["IncomeInsuAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["IncomeInsuAmount"]),

                                    PatientInsuCardNumber = reader["PatientInsuCardNumber"]?.ToString(),

                                    PatientEmployeeCode = reader["PatientEmployeeCode"]?.ToString(),

                                    Paymode = reader["Paymode"]?.ToString(),

                                    PaymodeGateway = reader["PaymodeGateway"]?.ToString(),

                                    PaymodeAmount = reader["PaymodeAmount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["PaymodeAmount"]),

                                    PaymentRefNo = reader["PaymentRefNo"]?.ToString(),

                                    DenialCode = reader["DenialCode"]?.ToString(),

                                    AmtInLocalCurrency = reader["AmtInLocalCurrency"]?.ToString(),

                                    RecordEnteredBy = reader["RecordEnteredBy"]?.ToString(),

                                    RecordStatus = reader["RecordStatus"]?.ToString(),

                                    PostedFlag = reader["PostedFlag"]?.ToString(),

                                    GPRefNo = reader["GPRefNo"]?.ToString(),

                                    ReferenceDocNumber = reader["ReferenceDocNumber"]?.ToString(),

                                    ReferenceDocDate = reader["ReferenceDocDate"]?.ToString(),

                                    PatientName = reader["PatientName"]?.ToString(),

                                    ServiceCategory = reader["ServiceCategory"]?.ToString(),

                                    ApexTransferDate = reader["ApexTransferDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["ApexTransferDate"]),

                                    AXTransferDate = reader["AXTransferDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["AXTransferDate"]),

                                    PatientVATAmt = reader["PatientVATAmt"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["PatientVATAmt"]),

                                    InsuranceVATAmt = reader["InsuranceVATAmt"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["InsuranceVATAmt"]),

                                    CorporateVATAmt = reader["CorporateVATAmt"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["CorporateVATAmt"]),

                                    VATPerc = reader["VATPerc"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["VATPerc"]),

                                    VATType = reader["VATType"]?.ToString(),

                                    VATLink = reader["VATLink"]?.ToString(),

                                    ServiceCode = reader["ServiceCode"]?.ToString(),

                                    ServiceName = reader["ServiceName"]?.ToString(),

                                    CPTCode = reader["CPTCode"]?.ToString(),

                                    OrgnBranchCode = reader["OrgnBranchCode"]?.ToString(),

                                    Verified = reader["Verified"] == DBNull.Value
                                        ? false
                                        : Convert.ToBoolean(reader["Verified"]),

                                    PatientCreditCardHolderName = reader["PatientCreditCardHolderName"]?.ToString(),

                                    ReceiptCardRemarks = reader["ReceiptCardRemarks"]?.ToString(),

                                    CurrencyACode = reader["CurrencyACode"]?.ToString(),

                                    CurrencyRateConversion = reader["CurrencyRateConversion"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["CurrencyRateConversion"]),

                                    AmtInForeignCurrency = reader["AmtInForeignCurrency"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["AmtInForeignCurrency"]),

                                    PatientCreditCardNumber = reader["PatientCreditCardNumber"]?.ToString(),

                                    PatientCreditCardExpDt = reader["PatientCreditCardExpDt"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["PatientCreditCardExpDt"]),

                                    Status = reader["Status"] == DBNull.Value
                                        ? "Open"
                                        : Convert.ToInt32(reader["Status"]) == 1
                                            ? "Posted"
                                            : Convert.ToInt32(reader["Status"]) == -1
                                                ? "Failed"
                                                : "Open",

                                    FailureReason = reader["FailureReason"]?.ToString(),

                                    FileName = reader["FileName"]?.ToString(),

                                    ImportedDate = reader["ImportedDate"] == DBNull.Value
                                        ? null
                                        : Convert.ToDateTime(reader["ImportedDate"])
                                });
                            }
                        }
                    }
                }

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        private DataTable CreateARDataTable(ImportARInput items)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("TransactionID", typeof(int));
            tbl.Columns.Add("InvoiceType", typeof(string));
            tbl.Columns.Add("TransactionType", typeof(string));
            tbl.Columns.Add("TransactionIncomeGroup", typeof(string));
            tbl.Columns.Add("ApexTransactionNumber", typeof(string));
            tbl.Columns.Add("ApexTransactionDate", typeof(DateTime));

            tbl.Columns.Add("ApexPatientCode", typeof(string));
            tbl.Columns.Add("ApexTPACode", typeof(string));
            tbl.Columns.Add("ApexInsuCode", typeof(string));
            tbl.Columns.Add("ApexInstCode", typeof(string));

            tbl.Columns.Add("ApexReportingDoctor", typeof(string));
            tbl.Columns.Add("ApexReferringDoctor", typeof(string));
            tbl.Columns.Add("ApexReportingDoctorDept", typeof(string));
            tbl.Columns.Add("ApexReferringDoctorDept", typeof(string));

            tbl.Columns.Add("IncomeGroupServiceCount", typeof(int));

            tbl.Columns.Add("IncomeGrossAmount", typeof(decimal));
            tbl.Columns.Add("IncomePolicyConcAmount", typeof(decimal));
            tbl.Columns.Add("IncomeAddlConcAmount", typeof(decimal));
            tbl.Columns.Add("IncomeNetAmount", typeof(decimal));
            tbl.Columns.Add("IncomePatientAmount", typeof(decimal));
            tbl.Columns.Add("IncomeInstAmount", typeof(decimal));
            tbl.Columns.Add("IncomeInsuAmount", typeof(decimal));

            tbl.Columns.Add("PatientInsuCardNumber", typeof(string));
            tbl.Columns.Add("PatientEmployeeCode", typeof(string));

            tbl.Columns.Add("Paymode", typeof(string));
            tbl.Columns.Add("PaymodeGateway", typeof(string));
            tbl.Columns.Add("PaymodeAmount", typeof(decimal));

            tbl.Columns.Add("PaymentRefNo", typeof(string));

            tbl.Columns.Add("DenialCode", typeof(string));

            tbl.Columns.Add("AmtInLocalCurrency", typeof(string));

            tbl.Columns.Add("RecordEnteredBy", typeof(string));
            tbl.Columns.Add("RecordStatus", typeof(string));
            tbl.Columns.Add("PostedFlag", typeof(string));

            tbl.Columns.Add("GPRefNo", typeof(string));

            tbl.Columns.Add("ReferenceDocNumber", typeof(string));
            tbl.Columns.Add("ReferenceDocDate", typeof(string));

            tbl.Columns.Add("PatientName", typeof(string));

            tbl.Columns.Add("ServiceCategory", typeof(string));

            tbl.Columns.Add("ApexTransferDate", typeof(DateTime));
            tbl.Columns.Add("AXTransferDate", typeof(DateTime));

            tbl.Columns.Add("PatientVATAmt", typeof(decimal));
            tbl.Columns.Add("InsuranceVATAmt", typeof(decimal));
            tbl.Columns.Add("CorporateVATAmt", typeof(decimal));

            tbl.Columns.Add("VATPerc", typeof(decimal));

            tbl.Columns.Add("VATType", typeof(string));
            tbl.Columns.Add("VATLink", typeof(string));

            tbl.Columns.Add("ServiceCode", typeof(string));
            tbl.Columns.Add("ServiceName", typeof(string));

            tbl.Columns.Add("CPTCode", typeof(string));

            tbl.Columns.Add("OrgnBranchCode", typeof(string));

            tbl.Columns.Add("Verified", typeof(bool));

            tbl.Columns.Add("PatientCreditCardHolderName", typeof(string));
            tbl.Columns.Add("ReceiptCardRemarks", typeof(string));
            tbl.Columns.Add("CurrencyACode", typeof(string));
            tbl.Columns.Add("CurrencyRateConversion", typeof(decimal));
            tbl.Columns.Add("AmtInForeignCurrency", typeof(decimal));

            tbl.Columns.Add("PatientCreditCardNumber", typeof(string));
            tbl.Columns.Add("PatientCreditCardExpDt", typeof(DateTime));

            items.data?.ForEach(ur => tbl.Rows.Add(

            ur.TransactionID > 0 ? ur.TransactionID : DBNull.Value,

            ur.InvoiceType ?? "",

            ur.TransactionType ?? "",

            ur.TransactionIncomeGroup ?? "",

            ur.ApexTransactionNumber ?? "",

            ur.ApexTransactionDate.HasValue
                ? ur.ApexTransactionDate
                : DBNull.Value,

            ur.ApexPatientCode ?? "",

            ur.ApexTPACode ?? "",

            ur.ApexInsuCode ?? "",

            ur.ApexInstCode ?? "",

            ur.ApexReportingDoctor ?? "",

            ur.ApexReferringDoctor ?? "",

            ur.ApexReportingDoctorDept ?? "",

            ur.ApexReferringDoctorDept ?? "",

            ur.IncomeGroupServiceCount ?? (object)DBNull.Value,

            ur.IncomeGrossAmount ?? (object)DBNull.Value,

            ur.IncomePolicyConcAmount ?? (object)DBNull.Value,

            ur.IncomeAddlConcAmount ?? (object)DBNull.Value,

            ur.IncomeNetAmount ?? (object)DBNull.Value,

            ur.IncomePatientAmount ?? (object)DBNull.Value,

            ur.IncomeInstAmount ?? (object)DBNull.Value,

            ur.IncomeInsuAmount ?? (object)DBNull.Value,

            ur.PatientInsuCardNumber ?? "",

            ur.PatientEmployeeCode ?? "",

            ur.Paymode ?? "",

            ur.PaymodeGateway ?? "",

            ur.PaymodeAmount ?? (object)DBNull.Value,

            ur.PaymentRefNo ?? "",

            ur.DenialCode ?? "",

            ur.AmtInLocalCurrency ?? "",

            ur.RecordEnteredBy ?? "",

            ur.RecordStatus ?? "",

            ur.PostedFlag ?? "",

            ur.GPRefNo ?? "",

            ur.ReferenceDocNumber ?? "",

            ur.ReferenceDocDate ?? "",

            ur.PatientName ?? "",

            ur.ServiceCategory ?? "",

            ur.ApexTransferDate.HasValue
                ? ur.ApexTransferDate
                : DBNull.Value,

            ur.AXTransferDate.HasValue
                ? ur.AXTransferDate
                : DBNull.Value,

            ur.PatientVATAmt ?? (object)DBNull.Value,

            ur.InsuranceVATAmt ?? (object)DBNull.Value,

            ur.CorporateVATAmt ?? (object)DBNull.Value,

            ur.VATPerc ?? (object)DBNull.Value,

            ur.VATType ?? "",

            ur.VATLink ?? "",

            ur.ServiceCode ?? "",

            ur.ServiceName ?? "",

            ur.CPTCode ?? "",

            ur.OrgnBranchCode ?? "",

            ur.Verified ?? false,

            ur.PatientCreditCardHolderName ?? "",

            ur.ReceiptCardRemarks ?? "",

            ur.CurrencyACode ?? "",

            ur.CurrencyRateConversion ?? (object)DBNull.Value,

            ur.AmtInForeignCurrency ?? (object)DBNull.Value,
            ur.PatientCreditCardNumber ?? "",
            ur.PatientCreditCardExpDt.HasValue
                ? ur.PatientCreditCardExpDt
                : DBNull.Value

        ));

            tbl.AcceptChanges();

            return tbl;
        }
    }
}
