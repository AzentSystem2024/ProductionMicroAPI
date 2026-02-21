using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportItemLogService
    {
        public List<ImportItemLog> GetAllItemLog();
        public List<InsertImportItemLogEntry> GetAllItemLogEntry(ImportItemLog itemLog);
        public bool Insert(ImportItemLog importItemLog);
    }
}
