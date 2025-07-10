using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAC_CreditNoteService
    {
        CreditNoteResponse SaveCreditNote(AC_CreditNote model);
        CreditNoteResponse UpdateCreditNote(AC_CreditNoteUpdate model);
        CreditNoteListResponse GetCreditNoteList();
        AC_CreditNoteSelect GetCreditNoteById(int id);
        CreditNoteResponse CommitCreditNote(CreditNoteCommitRequest request);
        DocResponse GetLastDocNo();
        CreditNoteResponse Delete(int id);
        CreditNoteInvoiceListResponse GetCreditNoteInvoiceList();

    }
}
