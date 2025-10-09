using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IWorksheetItemPropertyService
    {
        public List<WorksheetItem> GetAllWorksheetItem();
        public bool Insert(WorksheetItem worksheet);
        public bool Update(WorksheetItem worksheet);
        public bool DeleteItemProperty(int id);
        public bool Verify(WorksheetItem worksheet);
        public bool Approval(WorksheetItem worksheet);
        public WorksheetItem GetItems(int id);
        public List<ItemStoreProperties> GetItemPropertyList();
        public List<WorksheetItem> GetAllWorksheetItemPrice();
        public Int32 InsertPrice(WorksheetItem worksheet);
        public bool UpdatePrice(WorksheetItem worksheet);
        public WorksheetItem GetPriceItem(int id);
        public List<ItemPriceProperties> GetItemPriceList();
        public bool DeleteItemPrice(int id);
        public bool VerifyPrice(WorksheetItem worksheet);
        public bool ApprovalPrice(WorksheetItem worksheet);
        public List<WorksheetItem> GetAllWorksheetPromotionSchema();
        public bool InsertPromotion(WorksheetItem worksheet);
        public bool UpdatePromotion(WorksheetItem worksheet);
        public bool DeletePromotion(int id);
        public bool VerifyPromotion(WorksheetItem worksheet);
        public bool ApprovePromotion(WorksheetItem worksheet);
        public WorksheetItem GetPromotionItem(int id);
    }
}
