using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDelivery_NoteService
    {
        public Int32 Insert(Delivery_Note deliverynote);
        public Int32 Update(Delivery_NoteUpdate deliverynote);
        public List<SODetail> GetSO();
        public Delivery_Note_List_Response GetDeliveryNoteList();
        Delivery_Note_Select_Response GetDeliveryNoteById(int id);
        public Int32 Approve(Delivery_NoteUpdate deliverynote);
        public bool Delete(int id);
        DeliveryDoc GetLastDocNo();


    }
}
