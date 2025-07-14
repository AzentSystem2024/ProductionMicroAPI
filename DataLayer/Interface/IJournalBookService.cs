using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IJournalBookService
    {
        List<JournalBook> GetJournalBookData(int companyId, int finId, DateTime dateFrom, DateTime dateTo);
    }
}
