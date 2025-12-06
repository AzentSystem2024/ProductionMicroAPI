using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISupplierService
    {
        public List<Suppliers> GetAllSuppliers();
        public bool SaveData(Suppliers supplier);
        public Suppliers GetItems(int id);
        public bool DeleteSupplier(int id);
        public List<Supp_stateName> Getsupplist();
    }
}
