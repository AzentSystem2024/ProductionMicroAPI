using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUomService
    {
        public List<Uom> GetAllUom(UOMListReq request);
        public bool Insert(Uom uom);
        public Uom GetItems(int id);
        public bool Update(Uom uom);
        public bool DeleteUom(int id);
    }
}
