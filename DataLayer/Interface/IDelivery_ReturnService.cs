using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDelivery_ReturnService
    {
        public Int32 Insert(Delivery_Return deliveryreturn);
        public Int32 Update(Delivery_ReturnUpdate deliveryreturn);
        public List<DeliverynoteDetail> GetDN(DNRequest request);
        public DeliveryRtnListResponse GetDeliveryRtnList();
        public DeliveryRtnSelectResponse GetDeliveryRtnById(int id);
    }
}
