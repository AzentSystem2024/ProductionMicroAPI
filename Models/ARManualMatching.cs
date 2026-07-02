using Newtonsoft.Json;

namespace MicroApi.Models
{
    
    public class ARReceiptInput
    {
        public int? CompanyID { get; set; }
    }

    public class ARReceiptResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ARReceiptData> data { get; set; }
    }

    public class ARReceiptData
    {
        public int ReceiptID { get; set; }
        public int ReceiptDetailID { get; set; }
        public string ReceiptNo { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }
        public string ReferenceNo { get; set; }
        public string ServiceCode { get; set; }
        public decimal Amount { get; set; } 
        public decimal RejectedAmount { get; set; }
        public string RejectedReason { get; set; }
    }

    public class ARInvoiceInput
    {
        public string ReferenceNo { get; set; }
    }

    public class ARInvoiceResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ARInvoiceData> data { get; set; }
    }

    public class ARInvoiceData
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }
        public string ServiceCode { get; set; }
        public decimal Amount { get; set; }
      
    }

    public class ManualProcessResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
    }

    public class ManualProcessInput
    {
        public string ReceiptDetailID { get; set; }
        public List<ManualProcessInvoiceData> data { get; set; }
    }
    
    public class ManualProcessInvoiceData
    {
        public int InvoiceID { get; set; }
        public decimal? RejectedAmount { get; set; }
        public decimal? ReceivedAomount { get; set; }
    }
   


}
