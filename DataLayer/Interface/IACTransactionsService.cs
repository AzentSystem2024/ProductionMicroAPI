using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IACTransactionsService
    {
        JournalResponse InsertJournal(JournalHeader journal);
        JournalResponse UpdateJournal(JournalUpdateHeader header);
        JournalListResponse GetJournalVoucherList();
        JournalResponse GetJournalById(int id);
        VoucherResponse GetLastVoucherNo();
        JournalResponse DeleteJournal(int id);
        JournalResponse JournalApproval(int transId, bool isApproved);


    }
}
