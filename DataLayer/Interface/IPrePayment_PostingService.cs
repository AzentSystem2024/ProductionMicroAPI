using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPrePayment_PostingService
    {
        PrePayment_RequestResponse GetPrePaymentPendingList(PrePayment_PostingRequest request);
        PrepaymentPostingResponse Save(PrePayment_Posting model);
        PrepaymentPostingResponse Edit(PrePayment_PostingEdit model);
        PrePayment_PostingListResponse GetPrePaymentList(PrepaytListRequest request);
        PostingSelectResponse GetPrePaymentById(int id);
        PrepaymentPostingResponse commit(PrePayment_PostingEdit model);
        PrepaymentPostingResponse Delete(int id);

    }
}
