using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPurchaseInvoiceService
    {
        public List<PIDropdownData> GetPendingPoList(PIDropdownInput input);
        public GRNDetailResponce GetSelectedPoDetailS(GRNDetailInput input);
        public PurchHeader GetPurchaseInvoiceById(int id);
        public Int32 Insert(PurchHeader purchHeader);
        public Int32 Update(PurchHeader purchHeader);
        public Int32 Verify(PurchHeader purchHeader);
        public Int32 Approve(PurchHeader purchHeader);
        public bool Delete(int id);
        public List<PurchaseInvoice> GetPurchaseInvoiceList();
    }
}
