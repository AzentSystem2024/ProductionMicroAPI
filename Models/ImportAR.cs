using Newtonsoft.Json;

namespace MicroApi.Models
{
    public class ImportARData
    {
        public int? ID { get; set; }

        public int? TransactionID { get; set; }

        public string InvoiceType { get; set; }

        public string TransactionType { get; set; }

        public string TransactionIncomeGroup { get; set; }

        public string ApexTransactionNumber { get; set; }

        public DateTime? ApexTransactionDate { get; set; }

        public string ApexPatientCode { get; set; }

        public string ApexTPACode { get; set; }

        public string ApexInsuCode { get; set; }

        public string ApexInstCode { get; set; }

        public string ApexReportingDoctor { get; set; }

        public string ApexReferringDoctor { get; set; }

        public string ApexReportingDoctorDept { get; set; }

        public string ApexReferringDoctorDept { get; set; }

        public int? IncomeGroupServiceCount { get; set; }

        public decimal? IncomeGrossAmount { get; set; }

        public decimal? IncomePolicyConcAmount { get; set; }

        public decimal? IncomeAddlConcAmount { get; set; }

        public decimal? IncomeNetAmount { get; set; }

        public decimal? IncomePatientAmount { get; set; }

        public decimal? IncomeInstAmount { get; set; }

        public decimal? IncomeInsuAmount { get; set; }

        public string PatientInsuCardNumber { get; set; }

        public string PatientEmployeeCode { get; set; }

        public string Paymode { get; set; }

        public string PaymodeGateway { get; set; }

        public decimal? PaymodeAmount { get; set; }

        public string PaymentRefNo { get; set; }

        public string DenialCode { get; set; }

        public string AmtInLocalCurrency { get; set; }

        public string RecordEnteredBy { get; set; }

        public string RecordStatus { get; set; }

        public string PostedFlag { get; set; }

        public string GPRefNo { get; set; }

        public string ReferenceDocNumber { get; set; }

        public string ReferenceDocDate { get; set; }

        public string PatientName { get; set; }

        public string ServiceCategory { get; set; }

        public DateTime? ApexTransferDate { get; set; }

        public DateTime? AXTransferDate { get; set; }

        public decimal? PatientVATAmt { get; set; }

        public decimal? InsuranceVATAmt { get; set; }

        public decimal? CorporateVATAmt { get; set; }

        public decimal? VATPerc { get; set; }

        public string VATType { get; set; }

        public string VATLink { get; set; }

        public string ServiceCode { get; set; }

        public string ServiceName { get; set; }

        public string CPTCode { get; set; }

        public string OrgnBranchCode { get; set; }

        public bool? Verified { get; set; }

        public string PatientCreditCardHolderName { get; set; }

        public string ReceiptCardRemarks { get; set; }

        public string CurrencyACode { get; set; }

        public decimal? CurrencyRateConversion { get; set; }

        public decimal? AmtInForeignCurrency { get; set; }
    }

    public class ImportARLog
    {
        public int ID { get; set; }
        public int DocNo { get; set; }
        public string FileName { get; set; }    
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public DateTime ImportedTime { get; set; }
        public string ImportedBy { get; set; }
    }


    public class ImportARInput
    {
        public int? CompanyID { get; set; }
        public int? UserID { get; set; }
        public string? BatchNo { get; set; }
        public string? FileName { get; set; }
        public int? Action { get; set; }
        public List<ImportARData>? data { get; set; }
    }

    public class ImportARResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ImportARLog> data { get; set; }
    }

    public class viewImportARDataResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ImportARData> data { get; set; }
    }

    public class viewImportARInput
    {
        public int LogID { get; set; }
    }

    public class ImportArColumnsResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ImportArColumns> data { get; set; }
    }
    public class ImportArColumns
    {
        public string ColumnName { get; set; }
        public string ColumnTitle { get; set; }
    }
}
