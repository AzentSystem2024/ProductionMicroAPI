using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportChartOfAccountsService
    {
        //public List<ImportItemLog> GetAllItemLog();
        //public List<InsertImportItemLogEntry> GetAllItemLogEntry(ImportItemLog itemLog);
        public bool Import(ImportAccountsInput Vinput);
    }
}
