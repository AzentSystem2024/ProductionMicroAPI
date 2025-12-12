using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAC_CreditNoteService
    {
        CreditNoteResponse SaveCreditNote(AC_CreditNote model);
        CreditNoteResponse UpdateCreditNote(AC_CreditNoteUpdate model);
        CreditNoteListResponse GetCreditNoteList();
        AC_CreditNoteSelect GetCreditNoteById(int id);
        CreditNoteResponse Commit(AC_CreditNoteUpdate model);
        DocResponse GetLastDocNo(CreditVoucherRequest request);
        CreditNoteResponse Delete(int id);
        CreditNoteInvoiceListResponse GetCreditNoteInvoiceList(Pendingrequest request);
        List<Credithis> GetcreditHis(CreditHisRequest request);
    }
}
