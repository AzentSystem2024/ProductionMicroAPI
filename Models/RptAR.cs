using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroApi.Models
{
    public class RptARResponseOutput
    {
        public string flag { get; set; }
        public string message { get; set; }
        public RPTARHeader header { get; set; }

    }

    public class RPTARHeader
    {
        public string ReportID { get; set; }
        public List<ReportColumns> ReportColumns { get; set; }
        public List<RptARData> ReportData { get; set; }

    }

    public class RptARData
    {
        public string? InvoiceType { get; set; }
        public string? TransactionType { get; set; }
        public string? TransactionIncomeGroup { get; set; }
        public string? ApexTransactionNumber { get; set; }
        public string? ApexTransactionDate { get; set; }
        public string? ApexPatientCode { get; set; }
        public string? ApexTPACode { get; set; }
        public string? ApexInsuCode { get; set; }
        public string? ApexInstCode { get; set; }
        public string? ApexReportingDoctor { get; set; }
        public string? ApexReferringDoctor { get; set; }
        public string? ApexReportingDoctorDept { get; set; }
        public string? ApexReferringDoctorDept { get; set; }
        public decimal? IncomeGrossAmount { get; set; }
        public decimal? IncomePolicyConcAmount { get; set; }
        public decimal? IncomeAddlConcAmount { get; set; }

        public decimal? IncomeNetAmount { get; set; }
        public decimal? IncomePatientAmount { get; set; }
        public decimal? IncomeInstAmount { get; set; }
        public decimal? IncomeInsuAmount { get; set; }

        public string? ServiceCategory { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? CPTCode { get; set; }
        public string? OrgnBranchCode { get; set; }
        public string? Paymode { get; set; }
        public string? PaymodeGateway { get; set; }
        public string? PaymodeAmount { get; set; }
        public string? PaymentRefNo { get; set; }
        public decimal? DeniedAmount { get; set; }
        public string? DenialCode { get; set; }

        public string? PaymentApexTransctionNumber { get; set; }
        public string? PaymentApexTransactionDate { get; set; }
    }

    
    public class RptARInput
    {

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class ReportColumns
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public string Type { get; set; }
        public bool Visibility { get; set; }
        public bool Group { get; set; }
        public bool Summary { get; set; }
        public string Band { get; set; }
    }

}
