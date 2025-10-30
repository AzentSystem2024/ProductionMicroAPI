using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ICustomerAddressService
    {
        public int Insert(CustomerAddress address);
        public int Update(CustomerAddressUpdate address);
        public CustomerAddressUpdate GetById(int id);
        public List<CustomerAddressUpdate> GetAllCustomers();
        public bool DeleteCustomers(int id);
    }
}
