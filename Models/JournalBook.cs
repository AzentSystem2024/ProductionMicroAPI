namespace MicroApi.Models
{
    public class JournalBook
    {
        public int TransID { get; set; }
        public DateTime Date { get; set; }
        public string DocumentType { get; set; }
        public string VoucherNo { get; set; }
        public string Particulars { get; set; }
        public string Remarks { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
    public class JournalBookRequest
    {
        public int CompanyId { get; set; }
        public int FinId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
