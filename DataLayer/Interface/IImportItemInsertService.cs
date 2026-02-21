using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportItemInsertService
    {
        public bool Insert(List<InsertImportItem> insertImportItems);
    }
}
