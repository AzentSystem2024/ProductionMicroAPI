using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscPurchInvoiceService
    {
        public Int32 Insert(MiscPurchInvoice model);
        public Int32 Update(MiscPurchInvoice model);
        public Int32 Commit(MiscPurchInvoice model);
        public List<MiscPurchList> GetMiscPurchList(PurchListRequest request);
        public MiscPurchInvoice GetMiscPurchById(int id);
        MiscPurchResponse Delete(int id);
    }
}
