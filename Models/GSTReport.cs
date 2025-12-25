namespace MicroApi.Models
{
    public class GSTReport
    {
        public string GSTIN { get; set; }
        public string RECEIVER_NAME { get; set; }
        public int TRANS_ID { get; set; }
        public string DOC_TYPE { get; set; }        
        public string DOC_NAME { get; set; }        
        public string DOC_NO { get; set; }          
        public DateTime DOC_DATE { get; set; }
        public decimal INVOICE_AMOUNT { get; set; }
        public string PLACE_OF_SUPPLY { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal TOTAL_GST { get; set; }
        public decimal TAXABLE_AMOUNT { get; set; }
    }
    public class GSTReportRequest
    {
        public int COMPANY_ID { get; set; }
        public int FIN_ID { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
    }
    public class GSTReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GSTReport> DATA { get; set; }
    }
    public class GSTReportB2CL
    {
        public string RECEIVER_NAME { get; set; }
        public int TRANS_ID { get; set; }
        public string DOC_TYPE { get; set; }
        public string DOC_NAME { get; set; }
        public string DOC_NO { get; set; }
        public DateTime DOC_DATE { get; set; }
        public decimal INVOICE_AMOUNT { get; set; }
        public string PLACE_OF_SUPPLY { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal TOTAL_GST { get; set; }
        public decimal TAXABLE_AMOUNT { get; set; }
    }
    public class GSTB2CLReportResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GSTReportB2CL> DATA { get; set; }
    }
}
