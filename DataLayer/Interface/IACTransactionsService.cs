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
        JournalResponse commit(JournalUpdateHeader header);

        DebitNoteResponse SaveDebitNote(AC_DebitNote model);
        DebitNoteResponse UpdateDebitNote(AC_DebitNoteUpdate model);
        DebitNoteListResponse GetDebitNoteList();
        AC_DebitNoteSelect GetDebitNoteById(int id);
        DebitNoteResponse Commit(AC_DebitNoteUpdate model);
        DocNoResponse GetLastDocNo();
        DebitNoteResponse Delete(int id);
        DebitInvoiceResponse GetPendingInvoiceList(DebitInvoiceRequest request);

    }
}
