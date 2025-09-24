using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPurchaseOrderService
    {
        public List<SupplierList> GetSupplier(SupplierInput input);
        public Int32 Insert(PurchaseOrderHeader worksheet);
        public List<PurchaseOrderHeader> GetPOList(Int32 intUserID);
        public Int32 Update(PurchaseOrderHeader worksheet);
        public Int32 Verify(PurchaseOrderHeader worksheet);
        public Int32 Approval(PurchaseOrderHeader worksheet);
        public List<ItemList> GetItemList(ItemInput input);
        public PurchaseOrderHeader GetPurchaseOrder(int id);
        PurchaseDoc GetLastDocNo();
        public bool Delete(int id);

    }
}
