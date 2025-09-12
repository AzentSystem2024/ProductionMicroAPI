using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IReasonsService
    {
        public List<Reasons> GetAllReasons();
        public bool Insert(Reasons reasons);
        public bool Update(Reasons reasons);
        public bool DeleteReasons(int id);
        public Reasons GetItems(int id);
    }
}
