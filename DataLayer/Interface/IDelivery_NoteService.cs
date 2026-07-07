using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDelivery_NoteService
    {
        public Int32 Insert(Delivery_Note deliverynote);
        public Int32 Update(Delivery_NoteUpdate deliverynote);
        public List<SODetail> GetSO(DeliveryRequest request);
        public Delivery_Note_List_Response GetDeliveryNoteList(DeliveryNoteListRequest request);
        Delivery_Note_Select_Response GetDeliveryNoteById(int id);
        public Int32 Approve(Delivery_NoteUpdate deliverynote);
        public Int32 DNVerify(Delivery_NoteUpdate deliverynote);
        public bool Delete(int id);
        DeliveryDoc GetLastDocNo();
        public List<Custdetail> GetCustdetail(DeliveryRequest request);
        public List<PENDINGSTOCKITEMS> GetItems(GETDNITEM request);
        public Int32 Save(DNSave deliverynote);
        public Int32 Edit(DNSave deliverynote);
        DNSelect_Response DNSelectByID(int id);
        public DeliveryNoteListResponse GetDNoteList(DeliveryNoteListRequest request);
        public Int32 Commit(DNSave deliverynote);
        public Int32 Verify(DNSave deliverynote);
        public bool DNDelete(int id);
    }
}
