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
        public int ClaimUID { get; set; }
        public string FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string ClaimNumber { get; set; }
        public string ReceiverID { get; set; }
        public string PayerID { get; set; }
        public string PatientID { get; set; }
        public string EncounterType { get; set; }
        public string EncounterStartDate { get; set; }
        public string StartType { get; set; }
        public string EncounterEndDate { get; set; }
        public string EndType { get; set; }
        public string MemberID { get; set; }
        public string EmiratesID { get; set; }
        public string TransactionDate { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? PatientShare { get; set; }
        public decimal? NetAmount { get; set; }
        public string XMLFileName { get; set; }
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
