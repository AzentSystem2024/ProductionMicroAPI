using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPurchaseReturnService
    {
        public List<GRN_List> GetGrn(Input input);
        public List<GRN_DATA> GetGrnDetails(GrnInput input);
        public Int32 Insert(PurchaseReturn purchaseReturn);
        public Int32 Update(PurchaseReturn purchaseReturn);
        public PurchaseReturn GetPurchaseReturn(int id);
        public PurchaseReturnListResponse List();
        public bool Delete(int id);
        public Int32 Verify(PurchaseReturn purchaseReturn);
        public Int32 Approve(PurchaseReturn purchaseReturn);
        PurchaseInvoiceDetailResponse GetPurchaseInvoiceDetail(InvoiceInput request);
    }
}
